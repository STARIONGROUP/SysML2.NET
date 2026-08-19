# IMPACT-RADIUS.md — Design sketch for the derived-property impact-radius engine

> **Status: design sketch, not built.** This document works out service-layer obligation
> §15.1 of `SysML2.NET.CodeGenerator/SQLSCHEMA-GUIDE.md` — the component the guide calls
> "the hard one" and "where the correctness bugs of the whole system will live." It is the
> reference for whoever implements the engine; decisions recorded here are proposals, not
> commitments, except where marked **contract**.
>
> Companion documents: `SysML2.NET.CodeGenerator/SQLSCHEMA.md` (compact schema reference),
> `SysML2.NET.CodeGenerator/SQLSCHEMA-GUIDE.md` / `SQLSCHEMA-GUIDE.nl.md` (architecture
> guide — read §4 Axiom 2, §9, §15 and §18 first).

---

## 1. Problem statement and contract

**Input:** a change set Δ at a prospective commit C on branch B — the set of element
versions being written (creations, modifications, tombstones), plus the branch-head state
the commit builds on.

**Output (contract):** exactly the set of `derived_version` rows to write at C — one row
per element whose derived values *actually differ* at C from their previously resolved
values, containing the complete recomputed derived state for that element (all of its
derived properties, since `derived_json` is written whole-row).

Two failure directions, deliberately asymmetric in severity:

- **A missed invalidation** (row not written although values changed) is **silent
  corruption**: reads and queries at C serve stale derived values with full-conformance
  confidence. This is the failure class the design must make structurally unlikely.
- **A superfluous recomputation** (candidate considered although nothing changed) costs
  only CPU — and is then eliminated by diff pruning before it costs storage.

Therefore the guiding rule everywhere below: **over-approximate candidates, never
under-approximate; prune by value equality, never by guesswork.**

## 2. The core observation: ~325 derived properties, ~5 propagation kinds

Writing bespoke invalidation logic for 325 derived properties is neither feasible nor
necessary. Classified by *how a change reaches them*, they collapse into a handful of
propagation kinds — and the schema already carries a reverse-lookup index for each
(SQLSCHEMA-GUIDE §8.3 noted these indexes exist precisely for this analysis):

| Kind | Example properties | Propagation | Carrying index |
|---|---|---|---|
| **K1 self / one-hop** | most of the 325: `memberName` fallbacks, `constraintDefinition`, typing shortcuts | the element itself, plus elements it directly references | (PKs) |
| **K2 ownership-down** | `qualifiedName`, `isLibraryElement`, `path` | a change at N invalidates N's entire owned subtree | `element_owned_relationship` (+ `owning_relationship` back-pointers) |
| **K3 specialization-down** | `feature`, `inheritedMembership`, `membership`, `input`/`output` | a change to a Type invalidates all its transitive SPECIFIC types | `ix_specialization_version_general` (+ `subclassification`, `feature_typing` variants) |
| **K4 import-closure** | `importedMembership`, `member` | a change to namespace N invalidates every (transitively, when `isRecursive`) importing namespace | `ix_namespace_import_version_imported_namespace`, `ix_membership_import_version_imported_membership` |
| **K5 reverse-reference** | `documentation`, `ownedAnnotation`, `textualRepresentation` | a change to an annotating element invalidates the annotated element | `ix_annotation_version_annotated_element`, the `ix_*_target` family |

Kinds compose: a rename at N triggers K2 from N; adding a Specialization triggers K3 from
its `general`; the closures may cascade into each other (a K3-invalidated namespace-typed
member can seed K4). The engine must run expansion to a fixed point across kinds — in
practice shallow, because the kinds' outputs mostly re-trigger only K1.

## 3. The `derived_dependency` catalog

**Proposal:** a generated-plus-curated catalog — same generated-from-the-metamodel nature as
the per-release model-version descriptors (guide §12.2) — that makes the engine data-driven
instead of 325 hand-coded branches:

```sql
CREATE TABLE sysml2.derived_dependency (
    trigger_class_kind  smallint NOT NULL,   -- metaclass whose STORED property changed
    trigger_property    text     NOT NULL,   -- the stored property (or '*' for create/delete)
    kind                text     NOT NULL,   -- 'K1'..'K5'
    affected_property   text     NOT NULL,   -- the derived property to recompute
    affected_class_kind smallint NULL,       -- narrows the candidate type when applicable
    PRIMARY KEY (trigger_class_kind, trigger_property, kind, affected_property)
);
```

How to populate it:

1. **Machine-assisted first pass:** the OCL derivation bodies live in the XMI (the same
   source the `Compute*` methods were implemented from). Each derivation body reads a set
   of navigation paths; *inverting* those paths yields the triggers. E.g. `qualifiedName`
   reads `owningNamespace.qualifiedName` and the owner's member names → inverted: a change
   to `declaredName`/`declaredShortName` or to ownership at N invalidates `qualifiedName`
   K2-down from N.
2. **Curated second pass:** OCL parsing will not be airtight; every row is reviewed by a
   human, **grounded via the Hypha plugin** (per this repository's CLAUDE.md mandate for
   all derived-property semantics: `hypha:metamodel-lookup` for structure,
   `hypha:spec-citation` where the OCL needs interpretation).
3. **The safety valve:** any derived property that cannot be confidently classified gets a
   catch-all row that maps its triggers to **full recompute** (section 7). Unclassified
   must never mean unhandled.

The catalog ships like the model-version descriptors: emitted by the generator where
derivable, with the curated layer version-controlled beside it. Whether it lands as a
database table or as generated C# is open decision 6 — note that its former sibling
`property_catalog` was ultimately dropped from the database in favor of generated C#.

## 4. The pipeline

```
Δ (change set at head H)
  │ 1. SEED        look up (trigger → kind, affected) pairs in derived_dependency
  ▼
seeds {(kind, root, affected-properties)}
  │ 2. EXPAND      recursive CTEs over the reverse indexes, evaluated against the
  ▼                overlay-resolved head state; iterate kinds to fixed point; dedupe
candidate set C  ──────────────► |C| is a §15.15 monitoring signal (alert on spikes)
  │ 3. EVALUATE    lazy DAL session over get_element_at_branch_head; stratified order;
  ▼                run only the Compute* methods for the affected properties' element
recomputed derived state per candidate
  │ 4. DIFF        compare against each candidate's currently resolved derived row;
  ▼                drop candidates whose values did not change (prune, and CUT OFF — §5)
rows that actually changed
  │ 5. WRITE       bulk insert derived_version (COPY / multi-row; R5 GIN pending-list
  ▼                session bump) + branch_head overlay updates, inside the commit txn
commit C complete
```

Notes per stage:

- **Expansion is SQL work, not .NET work.** A K2 expansion is one recursive CTE over
  `element_owned_relationship` from the seed roots, resolved through the branch overlay
  (overlay row wins over base checkpoint — same COALESCE pattern as the read path §11).
  The database is strictly better at this than object traversal.
- **Stratified evaluation** exploits the closures' structure: process K2 candidates in
  ownership order (parents before children) so `qualifiedName(child) =
  qualifiedName(parent) + '::' + escapedName` is O(1) per node instead of O(depth);
  process K3 candidates in topological order over the specialization DAG (supertypes
  first) so `feature` folds reuse the parents' results.
- **The lazy DAL session** is what keeps memory bounded by radius-plus-fringe instead of
  model size: candidates plus whatever the `Compute*` methods navigate ("support") are
  materialized on demand from the head state and memoized for the duration of the commit.
  The existing `SysML2.NET.Dal` assembler/factory infrastructure is the natural host.
- **Evaluate only what was triggered.** A K2 hit on an element requires recomputing its
  K2-affected properties — not all 325. But since `derived_json` is written whole-row, a
  *written* row carries the full derived state: recompute the untriggered properties from
  the previous row's values (they are by construction unchanged) rather than re-deriving
  them.

## 5. Early cutoff — the worst-case killer

The single most valuable optimization, borrowed from incremental-computation engines
(Adapton; Salsa, which rust-analyzer is built on): **when a candidate's recomputed values
equal its previous values, do not propagate through it.**

Concretely for K2: rename element N, recompute N's subtree top-down; if a child's
`qualifiedName` comes out unchanged (rename to an effectively identical name, or a child
whose qualified name was overridden by a different mechanism), the entire subtree below
that child is pruned from the radius. The theoretical worst case (root rename ≈ whole
model) remains possible, but the *actual* cost becomes proportional to the *actual*
change — which is what users experience.

This requires interleaving stages 3 and 4 per stratum (evaluate a level, diff it, expand
only through the changed nodes) rather than running them as strict phases. The pipeline
diagram above should be read with that refinement for K2/K3/K4.

## 6. Concurrency and merge integration

- **Commits:** the engine slots into the optimistic pattern of SQLSCHEMA-GUIDE §18.3(1):
  compute the radius *before* taking the branch CAS (against the expected parent head);
  on CAS success write; on CAS failure recompute only the delta against the new head and
  retry. Never hold the branch lock during a large evaluation.
- **Pathological radii** (a true root rename at 1M elements): measured COPY throughput
  puts the write at roughly 35–60 s — an intrinsic bulk operation (R5 budget). Consider
  surfacing it in the API/UX as a confirmable operation ("this affects ~940,000 elements
  — proceed?") rather than hiding a minute-long commit.
- **Merges (contract, from §15.13):** the radius of a merge commit is computed against
  the **merged snapshot**, never as the union of the two branches' derived results —
  cross-branch interactions (branch 1 adds a Specialization, branch 2 adds a feature to
  its target) produce derived changes neither branch ever saw.
- **Cycle guards (contract, from §15.12):** a merge can create ownership or
  specialization cycles that no single branch contained. Every closure walk in the engine
  carries a visited-set; on cycle detection the commit is rejected with a model-validation
  error — the walk must never hang, and `qualifiedName` of a cyclic containment is
  undefined anyway.
- **Conversion commits (§6.4, obligation §15.16):** the release upgrade is a second client
  of this engine. Its seed is not a user change set but the *version-diff* between two
  metamodel releases (every element whose metaclass or properties changed shape); expansion,
  evaluation, and bulk write then proceed identically, followed by a forced
  `commit_checkpoint` on the conversion commit.

## 7. The oracle — build the safety net first

The correctness strategy is **differential testing against a trivially correct oracle**,
and it should be built *before* the engine:

- **The oracle** is the full recompute: evaluate every derived property of every element
  of the model at the new state, diff against the previous state, emit changed rows.
  Slow, simple, obviously correct.
- **The property test:** for arbitrary change sets Δ on test models,
  `engine(Δ) ≡ oracle(before, after)`. Run in CI on small models with randomized
  ("fuzzed") mutations — creates, renames, moves, retypes, deletes, specialization edits,
  import edits, and adversarial mixes. Any `derived_dependency` row that is too narrow
  (the dangerous, silent direction of section 1) fails this test immediately.
- **The production fallback:** keep the oracle callable at runtime. Commits whose
  triggers hit a catch-all row (section 3) or exceed a sanity bound run the oracle
  path — slow-but-correct over fast-but-silently-wrong, consistent with the schema's
  design philosophy throughout.
- **Golden scenarios** per propagation kind as regression anchors, mirroring the style of
  `schema.smoke.sql`: the rename (K2 — already PASS 2a/2b at the SQL level), a supertype
  feature addition (K3), a recursive import (K4), an annotation edit (K5), and the merge
  cross-interaction of §15.13.

## 8. Performance budget and monitoring

- Radius **evaluation** dominates for large K3 folds; radius **write** dominates for
  large K2 subtrees (measured: derived bulk insert ≈ 30k rows/s through the live GIN —
  re-measure with production-size `derived_json`, per SQLSCHEMA.md).
- `|C|` per commit is a first-class monitoring signal (SQLSCHEMA-GUIDE §15.15): alert
  when it exceeds a few percent of model size; spikes identify hot-rename usage patterns
  and candidate-rule bugs alike.
- Track *prune ratio* (candidates dropped by diff) — a persistently low ratio means the
  dependency catalog is too coarse; a ratio near 1.0 with large `|C|` means early cutoff
  (section 5) is not engaging.

## 9. What already exists to build on

| Asset | Role in the engine |
|---|---|
| `SysML2.NET/Extend/*.Compute*` (366 implemented) | the evaluation functions |
| Reverse-lookup indexes (§8.3) | the closure expansions of section 2 |
| Model-version descriptors (guide §12.2) | template and sibling for `derived_dependency`; they enumerate every derived property per metaclass, per release |
| The generator pipeline (§17) | emits the machine-derivable part of the catalog |
| `SysML2.NET.Dal` assembler/factory | host for the lazy DAL session |
| Hypha plugin (CLAUDE.md mandate) | grounding for the curated classification pass |
| §15.15 monitoring hooks | `|C|`, prune ratio, GIN pending-list health |

## 10. Open decisions for the implementer

1. **Catalog derivation depth** — how much OCL inversion to automate versus classify by
   hand (325 one-time rows is tractable by hand; automation pays off at metamodel
   upgrades).
2. **Fixed-point iteration bound** — prove or bound the kind-cascade depth (expected ≤ 3);
   define the escape to the oracle path if exceeded.
3. **Whole-model in memory for small projects** — below some size (say 50k elements) the
   lazy session is overhead; a threshold switch to full in-memory evaluation may win.
4. **Confirmable-bulk UX** — where the "this affects N elements" confirmation lives
   (API 202 + dry-run endpoint?) and its threshold.
5. **Derived recompute for passthrough-level providers** — none needed (SQLSCHEMA-GUIDE
   §9.4); the engine is a full-conformance component and should be cleanly absent at the
   other conformance levels.
6. **Catalog carrier: database table or generated C#** — the schema's own catalogs went the
   generated-C# route (the model-version descriptors replaced `property_catalog`, guide
   §12.2), and with multi-version support the dependency rules are per-release too; a
   database table would need release columns to say the same thing. Lean generated-C# unless
   an in-database consumer emerges.
