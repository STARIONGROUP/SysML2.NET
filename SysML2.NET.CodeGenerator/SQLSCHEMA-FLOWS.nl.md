# SQLSCHEMA-FLOWS.nl.md — Functionele flows door de SysML2 persistence layer

*Nederlandse vertaling van `SysML2.NET.CodeGenerator/SQLSCHEMA-FLOWS.md`. Technische termen,
tabel-, kolom-, functie- en bestandsnamen zijn bewust Engels gelaten; de sectiestructuur is
identiek aan het Engelse origineel.*

Dit document loopt de **belangrijkste use cases** van een SysML v2 model repository langs —
project aanmaken, een wijziging committen, branchen, mergen, taggen, queryen, verwijderen — en
benoemt per use case de **technische moving parts** die erbij betrokken zijn, in de volgorde
waarin ze geraakt worden, en **waarom ze bestaan**. Het is de "waar is alles voor, en wanneer
gebruik ik het"-begeleider van:

- `SysML2.NET.CodeGenerator/SQLSCHEMA.md` — de compacte design reference (tabellen, cijfers, audit)
- `SysML2.NET.CodeGenerator/SQLSCHEMA-GUIDE.md` — de uitgebreide architectural guide (het *waarom* in de diepte; Nederlandse versie: `SQLSCHEMA-GUIDE.nl.md`)
- `SysML2.NET.CodeGenerator/IMPACT-RADIUS.md` — de design sketch van de derived-property engine
- `SysML2.NET.CodeGenerator/Sql/schema.smoke.sql` — het uitvoerbare bewijs van elke flow hieronder (59 `PASS` assertions)

Elke flow verwijst naar de smoke-test assertions die hem bewijzen (`PASS 9a–9f`, enz.) en naar
de guide-sectie die hem in de diepte uitlegt (`guide §10.2`, enz.). Wil je een flow *zien*
lopen, open dan `schema.smoke.sql` bij de genoemde PASS en lees de SQL erboven.

---

## Inhoudsopgave

- [0. De cast: elk moving part in één tabel](#0-de-cast-elk-moving-part-in-één-tabel)
- [1. De twee ideeën waar al het andere uit volgt](#1-de-twee-ideeën-waar-al-het-andere-uit-volgt)
- [2. Het schema installeren](#2-het-schema-installeren)
- [3. Een project aanmaken](#3-een-project-aanmaken)
- [4. Elementen aanmaken (de eerste commit)](#4-elementen-aanmaken-de-eerste-commit)
- [5. Een element wijzigen (een normale commit)](#5-een-element-wijzigen-een-normale-commit)
- [6. Een element verwijderen](#6-een-element-verwijderen)
- [7. Het model lezen](#7-het-model-lezen)
- [8. Een branch aanmaken](#8-een-branch-aanmaken)
- [9. Committen op een branch, gelijktijdig met anderen](#9-committen-op-een-branch-gelijktijdig-met-anderen)
- [10. Twee branches mergen](#10-twee-branches-mergen)
- [11. Een commit taggen](#11-een-commit-taggen)
- [12. Een branch of tag verwijderen](#12-een-branch-of-tag-verwijderen)
- [13. Checkpoints: de achtergrond-onderhoudslus](#13-checkpoints-de-achtergrond-onderhoudslus)
- [14. Het model valideren](#14-het-model-valideren)
- [15. Het model queryen](#15-het-model-queryen)
- [16. Twee commits diffen](#16-twee-commits-diffen)
- [17. Een ander project gebruiken (cross-project references)](#17-een-ander-project-gebruiken-cross-project-references)
- [18. Upgraden naar een nieuwe metamodel release](#18-upgraden-naar-een-nieuwe-metamodel-release)
- [19. Een project verwijderen](#19-een-project-verwijderen)
- [20. Het metamodel wijzigen: het schema opnieuw genereren](#20-het-metamodel-wijzigen-het-schema-opnieuw-genereren)
- [21. De testpiramide: welke test bewijst wat](#21-de-testpiramide-welke-test-bewijst-wat)
- [22. Wat de service layer nog moet bouwen](#22-wat-de-service-layer-nog-moet-bouwen)

---

## 0. De cast: elk moving part in één tabel

Vóór de flows eerst de inventaris. Alles in het schema hoort bij één van vier lagen; de kolom
"Wanneer geraakt" is het korte antwoord op "wanneer gebruik ik dit?".

| Laag | Onderdeel | Wat het is | Wanneer geraakt |
|---|---|---|---|
| **A. PIM / versioning** (handgeschreven) | `project` | Het container-record. Draagt `default_branch_id` en `target_model_version_id` (upgrade policy). | project aanmaken / hernoemen / verwijderen |
| | `commit` | Eén onveranderlijke knoop in de historie. `created` + `model_version_id` zijn dragend. | elke commit; nooit ge-update (`trg_commit_immutable`) |
| | `commit_parent` | De DAG-edges — een commit heeft een *set* geordende parents. | elke commit (1 edge), elke merge (2+ edges) |
| | `branch` | Het enige muteerbare: `head_commit_id` verschuift bij elke commit; `base_commit_id` verankert de overlay. | branch aanmaken, commit, compaction, soft delete |
| | `tag` | Een bevroren pointer naar een commit. | taggen, tag soft-deleten |
| | `query` | Een opgeslagen spec Query-definitie als JSON. | query opslaan / uitvoeren |
| | `project_usage` | "Project A gebruikt project B op commit C". | cross-project import |
| | `data_identity` | De stabiele `@id` van een element plus zijn **onveranderlijke metaclass** (`class_kind`). Het FK-doel van elke reference. | element aanmaken (eenmalig, voor altijd) |
| | `model_version`, `class_kind` | De append-only registry seeds: release-ordinalen en bevroren metaclass-ids. | installatie; metamodel upgrade |
| **B. Stored element state** (append-only) | `element_version` | Eén rij per (element, commit) die het wijzigde. Bevat de eigen kolommen van Element, `tombstone`, en `stored_json` (de vooraf geserialiseerde stored helft van de payload). | elke create / change / delete van een element |
| | 47 `*_version` subtype-tabellen | De genormaliseerde stored kolommen van elke storage-declarerende metaclass (`feature_version`, `usage_version`, …). FK's en constraints leven hier. | zelfde transactie als `element_version` |
| | 7 link-tabellen | Geordende meerwaardige stored properties (`element_owned_relationship`, `relationship_target`, …). Gesleuteld op *version*. | zelfde transactie; ook reverse lookup via `ix_*_target` |
| **C. Derived element state** (append-only) | `derived_version` | Eén rij per (element, commit) waarvan de **derived** waarden veranderden — de impact radius. Zes gepromoveerde kolommen (`owner`, `qualified_name`, `name`, …) plus `derived_json`. | elke commit, voor de change set ÉN de elementen die hij raakt |
| **D. Snapshot resolution + read path** | `commit_checkpoint` + `commit_checkpoint_registry` | Een volledig gematerialiseerde snapshot op één commit (één rij per levend element), en een marker van één rij per checkpoint. | achtergrond-cadans; branch-fork bases |
| | `branch_head` | De sparse **overlay**: alleen de divergentie van de branch ten opzichte van zijn base checkpoint. | elke commit (upsert), branch delete (purge) |
| | `resolve_commit_state()`, `resolve_element_at_commit()` | De fold: het `versionedData`-algoritme van de spec, begrensd door checkpoints. | historische reads, diff, validatie, checkpoint build |
| | `get_element_at_branch_head()`, `get_elements_at_branch_head()`, `get_element_at_commit()`, `get_elements_at_commit()` | De API read-functies: `stored_json || derived_json`. | elke GET van een element |
| | `build_commit_checkpoint()` | Materialiseert een checkpoint vanuit de resolver, idempotent. | achtergrond-cadans |
| | `validate_references_in_commit()`, `validate_references_at_commit()` | Tweelaagse dangling/wrong-type reference validation. | per commit (incrementeel); periodieke audit (volledig) |
| | `vw_*` flattening views (167) | Reconstrueren de DTO-rijvorm per metaclass. | ad-hoc SQL, rapportage, debugging |
| **Triggers** | `trg_commit_immutable` | Weigert elke UPDATE van `commit`. | bewaakt elke flow |
| | `trg_commit_parent_monotonic` | Een child moet strikt nieuwer zijn dan elke parent. Is tegelijk het acycliciteitsbewijs. | elke commit / merge |
| | `trg_commit_parent_version` | Geen downgrade; merge-parents moeten de release van de merge delen. | commit, merge, conversion commit |
| **.NET-kant** | `SysML2.NET/PIM/DTO/*` | `Project`, `Commit`, `Branch`, `Tag`, `DataVersion`, `DataIdentity`, `Query`, `ProjectUsage` — de API-records. | de wire shapes van de REST API |
| | `SysML2.NET/Core/AutoGenEnum/ClassKind.cs` | De gegenereerde `enum ClassKind : short` met de bevroren registry-waarden. | elke write die `class_kind` stempelt |
| | `SysML2.NET/Extend/*.Compute*` | De 366 derived-property berekeningen — de engine die `derived_version` vult. | commit-tijd (impact radius) |
| | `SysML2.NET.Serializer.Json` | Produceert `stored_json` (en splitst payloads in een stored / derived helft). | commit-tijd |

---

## 1. De twee ideeën waar al het andere uit volgt

Elke flow hieronder is een gevolg van twee axioma's (guide §4). Houd ze in gedachten en de
vorm van elke flow wordt voorspelbaar.

**Axioma 1 — references benoemen identities, nooit versions.** Een SysML-reference wijst naar
een stabiele element-`@id`. Onder branching is "bestaat het doel?" alleen te beantwoorden
tegen de identity, dus *elke* reference-kolom in het schema is een FK naar
`data_identity(id)`. Of het doel *levend is op commit C* is helemaal geen FK-vraag; het is een
validatie per commit (`validate_references_*`, flow 14).

**Axioma 2 — een derived waarde is een functie van (identity, commit), niet van (version).**
Package `Old` hernoemen naar `New` verandert de `qualifiedName` van elke afstammeling zonder
de stored state van enige afstammeling aan te raken. Daarom is derived state een **tweede
append-only stream** (`derived_version`), opgelost door dezelfde fold als stored state — nooit
een kolom op `element_version`. Smoke PASS 2a/2b is het bewijs: na de rename is de derived
`qualifiedName` van het child veranderd terwijl het child nog steeds naar zijn *oorspronkelijke*
version-rij resolvet.

Twee verdere gevolgen bepalen het write path:

- **Append-only is het concurrency-model** (guide §18.1). `commit`, `commit_parent`,
  `element_version`, `derived_version`, `commit_checkpoint` worden nooit ge-update. Alle
  muteerbare state is samengeperst in één `branch`-rij per branch plus zijn overlay. Readers
  blokkeren writers nooit; writers op verschillende branches conflicteren nooit; writers op
  *dezelfde* branch conflicteren op precies één rij, wat de juiste semantiek is voor een
  seriële historie.
- **Reads moeten goedkoop zijn, dus writes betalen** (guide §9.1). Derived waarden worden
  vooraf berekend op commit-tijd, een element serveren is één jsonb-concat over PK-probes, en
  de historische fold is begrensd door checkpoints.

---

## 2. Het schema installeren

**Use case:** een lege database opzetten.

| Stap | Onderdeel | Waarom |
|---|---|---|
| 1 | Start PostgreSQL 18 (17 werkt ook) met `max_locks_per_transaction >= 4096` | 58 gepartitioneerde tabellen × 16 hash partitions = 928 leaves; DDL over het hele schema put de standaard 64 locks uit. Geverifieerd: de installatie faalt zonder. |
| 2 | Pas `SysML2.NET.CodeGenerator/Sql/schema2.generated.sql` toe | De generator-output: enum types, registry seeds, PIM-tabellen, element/derived tabellen, link- + subtype-tabellen, views, resolvers, validators, partities. |
| 3 | De registry seeds draaien als `ON CONFLICT (id) DO NOTHING` | Idempotent: veilig opnieuw toe te passen bij een upgrade; `class_kind`-ids zijn voor altijd bevroren door `ClassKindRegistry.cs`. PASS 11a. |

Meer is niet nodig; het schema is zelfstandig. De .NET-fixtures doen precies dit in een
Testcontainer (`PostgreSqlSchemaTestHost.cs`), waarbij ze het schema in-process genereren uit
het UML-model zodat registry drift tegelijk wordt gevangen.

---

## 3. Een project aanmaken

**Use case:** `POST /projects`.

| Stap | Onderdeel | Waarom |
|---|---|---|
| 1 | `INSERT project (id, name, created)` | De container. `target_model_version_id` blijft NULL (= onbeperkte release policy) tenzij de operator er een zet. |
| 2 | Maak de eerste commit (flow 4) en de default branch die ernaar wijst | Een project zonder branch heeft geen leesbare head. |
| 3 | `UPDATE project SET default_branch_id = …` | De FK `project → branch → project` is circulair, dus hij is `DEFERRABLE INITIALLY DEFERRED`; maak beide in één transactie aan. |

PASS 19a/19b dekken list, read en rename (`project` is muteerbaar volgens Clause 7.1.2). De
setup van de smoke test (regels 29–45) is de minimale versie van deze flow.

**Bootstrap-opmerking:** een gloednieuwe branch heeft `base_commit_id = NULL`, wat betekent
"de overlay *is* de volledige head state" (guide §10.2). Die modus is O(model) en alleen
acceptabel tot het eerste checkpoint bestaat; monitoring-signaal §15.15 alarmeert als een
levende branch er nog in zit.

---

## 4. Elementen aanmaken (de eerste commit)

**Use case:** `POST /projects/{p}/commits` met een change set van nieuwe elementen.

Dit is de canonieke **commit-transactie** (guide §15.3). Elke latere flow die schrijft is er
een variatie op.

| Stap | Onderdeel | Waarom |
|---|---|---|
| 1 | Lees de `head_commit_id` van de branch (de verwachte parent) | De parent waartegen de client bouwde — nodig voor de CAS in stap 8. |
| 2 | `INSERT data_identity (id, project_id, class_kind)` — één per **nieuw** element | Munt de stabiele `@id`, getypeerd met de metaclass die nooit zal veranderen. Bestaande elementen hebben de hunne al. |
| 3 | `INSERT commit (…, created = clock_timestamp(), model_version_id)` | `clock_timestamp()`, niet `now()`: een transactie die begon vóórdat de huidige head committe zou anders *eerder* stempelen dan zijn parent en de monotonicity trigger raken (guide §18.2). |
| 4 | `INSERT commit_parent (commit, parent, ordinal 0)` | `trg_commit_parent_monotonic` valideert child nieuwer dan parent (PASS 6); `trg_commit_parent_version` valideert de release (PASS 11c). |
| 5 | `INSERT element_version` — één rij per element in de change set, met `class_kind`, de eigen kolommen van Element, en `stored_json` | De system-of-record rij plus het vooraf geserialiseerde read model. De composite FK `(identity_id, class_kind)` maakt een version die de metaclass van zijn identity tegenspreekt onmogelijk (PASS 12a). `ux_element_version_identity_commit` dwingt één version per element per commit af. |
| 6 | `INSERT <subtype>_version`-rijen voor elke storage-declarerende voorouder (een `PartUsage` → `type_version`, `feature_version`, `usage_version`, `occurrence_usage_version`) + link-tabelrijen (`element_owned_relationship`, …) | Genormaliseerde kolommen dragen de FK's, NOT NULLs, DEFAULTs en reverse-lookup indexes. In welke tabellen een metaclass deelneemt komt uit de gegenereerde descriptors (guide §12.2). |
| 7 | `INSERT derived_version` — voor de change set **en** elk element waarvan de derived waarden veranderden (de impact radius), met de zes gepromoveerde kolommen geëxtraheerd | Axioma 2. Berekend door `SysML2.NET/Extend/*.Compute*` tegen de parent-snapshot + change set; het engine-ontwerp staat in `IMPACT-RADIUS.md`. Voor de eerste commit is de radius gewoon de nieuwe elementen. |
| 8 | `UPDATE branch SET head_commit_id = :new WHERE id = :b AND head_commit_id = :expected` — rowcount 0 → ROLLBACK, 409 | De **compare-and-swap** (guide §18.2, normatief). Zonder verliezen twee gelijktijdige committers stilzwijgend één commit. |
| 9 | `INSERT branch_head … ON CONFLICT (project_id, branch_id, identity_id) DO UPDATE` — één upsert per change-set element | Houdt overlay = divergentie van de base, O(changeset). |
| 10 | `COMMIT` | Eén transactie: pure inserts + één branch-rij update. |
| 11 | *(na de commit, synchroon)* `validate_references_in_commit(project, commit)` | O(change set); bewaakt de acceptatie (flow 14). |

De smoke test schrijft dit met de hand op regels 59–123 (c1 en c2); de `ReplayHistoryAsync`
van de benchmark (`SqlSchemaBenchmarkTestFixture.cs:413`) doet het vanuit .NET met
`NpgsqlBatch` en authentieke `SysML2.NET.Serializer.Json`-payloads; de `bench_try_commit` van
de concurrency suite (`schema.concurrency.setup.sql`) is hetzelfde protocol onder een race van
16 clients.

**De "child toevoegen"-koppeling** (guide §15.8–9): "voeg child B toe aan package A" schrijft
een nieuwe B, een nieuwe `OwningMembership` M, **én een nieuwe version van A** — omdat de
`ownedRelationship`-lijst van A stored, geordende state is. Eén ownership-feit leeft op vier
plekken (de lijst van A, M's `owning_related_element`, M's `ownedRelatedElement`-lijst, B's
`owning_relationship`) en de service moet alle vier coherent houden binnen de change set. Dit
is ook waarom populaire containers contention points zijn (flow 9).

---

## 5. Een element wijzigen (een normale commit)

**Use case:** een package hernoemen; de `direction` van een feature aanpassen; een element
verplaatsen.

Identiek aan flow 4, behalve:

- Geen nieuwe `data_identity`-rij — het element heeft er al een. Identities worden eenmalig,
  voor altijd, aangemaakt.
- `element_version` krijgt alleen voor het gewijzigde element een **nieuwe rij** (nieuwe
  `version_id`, zelfde `identity_id`). Niets wordt ge-update; de oude rij blijft voor de
  historie.
- `derived_version` krijgt rijen voor het element **en zijn impact radius**. Het
  smoke-scenario is het archetype: c2 hernoemt package `Old` → `New`; `element_version` krijgt
  één rij (de package), `derived_version` krijgt er twee (de package *en het onaangeroerde
  child*, waarvan de `qualifiedName` nu `New::wheel` is). PASS 2a/2b, guide §16.1.
- De overlay-upsert vervangt de `version_id` / `derived_id` pointers van het element op de
  branch.

Worst case om te kennen: een namespace dicht bij de root hernoemen invalideert `qualifiedName`
voor het grootste deel van het model, met een burst van ~1M `derived_version`-rijen als
gevolg. Dat is inherent aan de semantiek van de spec; het schema maakt het overleefbaar (lz4,
GIN pending-list tuning — audit R5), en de `Root_rename_burst_with_concurrent_reads` van de
benchmark meet het met lopende readers. Voorkeurspatroon voor de service: bereken de derived
delta *vóór* het nemen van de branch lock, daarna de CAS (guide §18.3.1).

---

## 6. Een element verwijderen

**Use case:** een part verwijderen met alles wat het bezit.

| Stap | Onderdeel | Waarom |
|---|---|---|
| 1 | Bereken de **owned closure**: het element, zijn memberships, alle transitieve children | Spec-invariant 3: een verwijdering moet iets verwijderen; en het schema slaat zonder morren een half verwijderde boom op (Axioma 1 — FK's checken bestaan, niet liveness). Alleen de service garandeert de volledigheid van de closure (guide §15.11). |
| 2 | Voor elk: `INSERT element_version (…, tombstone = true)` zonder payload | Een verwijdering is een **gebeurtenis in de historie**, niet de afwezigheid van data: `DataVersion.payload = null` → `tombstone = true`. De twee CHECK constraints maken tombstones en payload-rijen wederzijds uitsluitende vormen. |
| 3 | Een nieuwe version van de **owner** met de membership verwijderd uit zijn `ownedRelationship`-lijst | Dezelfde koppeling als in flow 4. |
| 4 | Overlay-upsert met `is_tombstone = true` | Maskeert de rij van het base checkpoint voor deze branch zonder het checkpoint aan te raken (PASS 9d). |
| 5 | `validate_references_in_commit` | Vangt de *omgekeerde* richting: een levend, ongewijzigd element elders dat nog naar de verwijderde identity verwijst (PASS 13c). |

De resolver laat getombstonede identities uit elke snapshot weg (PASS 3). De identity-rij zelf
blijft — references vanuit andere branches en vanuit de historie hebben hem nog nodig.

---

## 7. Het model lezen

**Use case:** `GET /projects/{p}/branches/{b}/elements/{e}` (de heetste query in het systeem)
en zijn broertjes.

| Read | Functie | Pad | Kosten |
|---|---|---|---|
| Eén element op een branch head | `get_element_at_branch_head(branch, identity)` | `branch` → overlay-rij? anders base `commit_checkpoint`-rij → `element_version.stored_json \|\| derived_version.derived_json` | 4 PK-probes op één partitie; gemeten 0,06 ms exec, 15/16 partities nooit uitgevoerd |
| Alle elementen op een branch head | `get_elements_at_branch_head(branch)` | base checkpoint min overlaid identities (anti-join) `UNION ALL` levende overlay | O(model); ~1,1 s bij 1M → **pagineren** (flow 7b) |
| Eén element op een historische commit | `get_element_at_commit(project, commit, identity)` → `resolve_element_at_commit` | loop `commit_parent` terug tot het dichtstbijzijnde checkpoint, gefilterd op één identity | O(doorlopen ancestry); 2–4 ms bij 500 commits vanaf checkpoint |
| Alle elementen op een historische commit | `get_elements_at_commit(project, commit)` → `resolve_commit_state` | de volledige fold (hieronder) | 185 ms bij 200k; 1,1 s bij 1M |

Waarom de branch-head read bij `branch` begint in plaats van direct op `branch_head` te
filteren: `branch` levert `project_id` op, en elke gepartitioneerde tabel in het schema heeft
`project_id` nodig om naar één partitie te prunen en zijn PK te gebruiken. De oorspronkelijke
versie filterde op kale uuids en scande alle 16 partities (audit R2). Regel: **elke query op
een gepartitioneerde tabel waarvan de enige sleutels kale uuids zijn, is per constructie
defect.**

Waarom de read één SQL-statement is: één statement ziet één MVCC-snapshot, dus een reader kan
nooit "nieuwe base pointer + oude overlay" halverwege een compaction waarnemen (guide §18.1).
Opsplitsen in twee round-trips verliest die garantie.

**De fold** (`resolve_commit_state`, guide §10.4), in woorden: is deze commit gecheckpoint? Zo
niet, loop recursief de parents af en stop bij gecheckpointe commits; neem voor elk element
dat binnen dat venster gewijzigd is de nieuwste version (`ORDER BY created DESC, id DESC`);
neem voor al het andere de rij van het grens-checkpoint; laat tombstones weg; herhaal voor de
derived stream. Het is de eigen `versionedData`-OCL van de spec (guide §6.2), uitgevoerd via
indexen.

### 7b. Pagineren

Set-reads zijn O(model), dus de API moet pagineren (guide §16.5). Twee regels en één guard:

1. **Veranker aan een commit, nooit aan HEAD.** Resolve branch → commit één keer, zet de
   commit-id in het page token. Commits zijn onveranderlijk, dus pagina's zijn voor altijd
   torn-free zonder server-side cursor. (Pagineren op HEAD terwijl een collega commit levert
   een gescheurde collectie op — §18.3.2.)
2. **Keyset, nooit OFFSET.** `WHERE identity_id > :last ORDER BY identity_id LIMIT n` — beide
   benen van de set-read functie eindigen in hun PK op `identity_id`, dus elke pagina is een
   range scan.
3. **De inlining guard.** `get_elements_at_branch_head` is een single-`SELECT` `LANGUAGE sql
   STABLE` functie, zodat PostgreSQL hem inlinet en het keyset-predicaat de index scans
   bereikt. Toont het plan ooit `Function Scan on get_elements_at_branch_head`, dan is het
   pagineren stilzwijgend gedegradeerd tot O(model) per pagina. De benchmark assert de
   planvorm (`Keyset_page_is_index_priced_and_inlining_holds`).

---

## 8. Een branch aanmaken

**Use case:** `POST /projects/{p}/branches` vanaf een commit.

| Geval | Wat er gebeurt | Kosten |
|---|---|---|
| Fork op een **gecheckpointe** commit | `INSERT branch (…, head_commit_id = c, base_commit_id = c)` — **nul** overlay-rijen | O(1): 1,2–1,8 ms gemeten, onafhankelijk van de modelgrootte (PASS 9b) |
| Fork op een **niet-gecheckpointe** commit | base = dichtstbijzijnde gecheckpointe voorouder; schrijf de (checkpoint → fork) delta naar `branch_head` | O(delta) |
| Fork voordat er een checkpoint bestaat | `base_commit_id = NULL`, de overlay bevat alles | O(model) — alleen bootstrap |

Waarom deze vorm: het naïeve "één rij per (branch, element)" was het oorspronkelijke ontwerp,
en 500 branches × 1M elementen = 500M rijen per project. De overlay slaat alleen
**divergentie** op, en honderden branches delen dezelfde paar checkpoints (guide §10.2, audit
R1). De `CreateBranchFleetAsync` van de benchmark maakt zo 500 branches aan.

Invariant die de service bezit: `base_commit_id` moet altijd naar een **gecheckpointe** commit
wijzen (guide §15.4) — daarom checkpoint de cadanspolicy altijd branch-fork bases.

Branchnamen zijn uniek **alleen onder levende branches** (`ux_branch_project_name_live`), dus
een naam die door een soft delete is uitgefaseerd kan meteen hergebruikt worden (flow 12).

---

## 9. Committen op een branch, gelijktijdig met anderen

**Use case:** veel gebruikers aan het editen; meerdere op dezelfde branch.

De commit-transactie van flow 4 bevat het protocol al; deze flow legt uit wat het oplevert en
wat niet.

**Verschillende branches conflicteren nooit.** De muteerbare state van elke branch is zijn
eigen `branch`-rij en zijn eigen overlay-rijen. Gemeten: 16 writers op 16 branches,
2.200–3.500 commits/s, 0 conflicten (`SpreadBranches_OneWriterPerBranch_NoConflicts`).

**Dezelfde branch: precies één winnaar per head-waarde.** De CAS `UPDATE branch … WHERE
head_commit_id = :expected` laat precies één van N racende transacties door; verliezers zien
rowcount 0, rollen terug, geven 409 terug, en de client rebaset. Gemeten: 16 writers die zonder
denktijd op één branch hameren, ~1.000 pogingen/s, 83% conflict rate (het bewuste worst
case), 0 fouten, 0 deadlocks, heads strikt lineair, verliezers schreven niets
(`HotBranchRace_SixteenWriters_ExactlyOneWinnerPerHead`; verifier-invarianten C1–C5 in
`schema.concurrency.verify.sql`).

**Readers blokkeren nooit.** Tijdens de write storm bleven reads op ~1,2 ms tegenover 0,8 ms
idle (`ReadsDuringWriteStorm_ReadersNeverBlocked`). Dit is MVCC op append-only tabellen; gewoon
READ COMMITTED volstaat overal, niets heeft SERIALIZABLE nodig.

**Lock-volgorde.** De `branch`-rij als eerste aanraken in elke writer (CAS of `SELECT … FOR
UPDATE`) geeft alle writers dezelfde lock-volgorde, wat gratis deadlock-preventie is.
Compaction (flow 13) moet dezelfde lock nemen.

**Wat de client doet bij 409 — de rebase** (service-werk, guide §15.8–10, §18.3.6):

- Lees de nieuwe head opnieuw, three-way merge de change set ertegen.
- Additieve, disjuncte collectiewijzigingen (twee gebruikers voegden elk een child toe aan
  dezelfde package) **moeten** automatisch mergen onder een deterministische
  ordeningspolicy; anders wordt elke gedeelde container een conflictmagneet, omdat elke
  child-toevoeging de parent versioneert.
- Conflictdetectie moet containment-aware zijn: een getombstonede package conflicteert met
  elke edit *eronder*, ook al zijn de identities disjunct.
- Probeer de commit opnieuw met de nieuwe verwachte parent.

Kleine lettertjes: de monotonicity trigger weigert twee commits in dezelfde microseconde op één
lineage — herstempel en probeer opnieuw; laat de database `created` toekennen in
multi-server deployments.

### 9b. Samenwerken op één branch: actueel blijven en change sets

**Use case:** tien mensen op dezelfde branch, elk vijf changes per minuut, een uur lang. Drie
vragen: houdt het stand, hoe blijft elke client actueel, en kan een client "wat is er
veranderd" ophalen in plaats van het model opnieuw te lezen.

**Houdt het stand — het rekensommetje.** Die belasting is 50 commits/min, 3.000 commits in het
uur, gemiddeld één commit per ~1,2 s. Tegenover de gemeten getallen:

| Grootheid | Het scenario | Gemeten |
|---|---|---|
| Commit rate op één branch | ~0,8/s | ~1.000 attempts/s, 16 writers, geen think time (hot-branch race) |
| Commits na een uur | 3.000 | de benchmark replay draait er 20.000 (`SYSML2_BENCH_COMMITS`) |
| Head read terwijl anderen schrijven | | ~1,2 ms, nooit geblokkeerd (reads-under-storm) |
| Checkpoints die de cadence triggert | ~15 (één per 200 commits) | 12–15 s per stuk bij 1M elementen, onder een seconde voor een 10k-model, buiten het commit-pad |
| Overlay rows na het uur | begrensd door de *distinct* aangeraakte identities (upsert) | compactiedrempel ~100k rows / ~10% van het model |

De throughput zit ruim onder één procent van het hot-branch-plafond, en niets groeit mee met
de lengte van de history: head reads lopen via de overlay (flow 7) en kosten bij commit 3.000
hetzelfde als bij commit 1; historische reads lopen hooguit ~200 commits terug naar het
dichtstbijzijnde checkpoint (flow 13). Eén piek om te kennen: het hernoemen van een
hoog-niveau package schrijft een `derived_version`-row voor elk element eronder (de
root-rename burst in de benchmark) — O(model), niet O(change set), maar zeldzaam bij deze rate.

**Wat er echt toe doet: stale heads en gedeelde containers.** De CAS van flow 9 heeft de
verwachte head nodig. Met elke 1,2 s een commit en een gebruiker die tien seconden over een
edit doet, komt vrijwel elke commit binnen met een stale head en krijgt een 409 — en dat is
normaal, geen fout. De service **moet dus automatisch rebasen**: lees de head opnieuw, three-way
merge de change set, probeer opnieuw. Dat een retry nogmaals botst is zeldzaam (commit rate ×
transactieduur, ruim onder één procent), dus één retry volstaat vrijwel altijd en gebruikers
zien de 409 nooit. De tweede regel bepaalt of de branch überhaupt bruikbaar is: een child
toevoegen aan een package versioneert de package, dus tien mensen die dezelfde package vullen
raken allemaal dezelfde parent identity. Additieve, disjuncte collection changes **moeten
auto-mergen** onder een deterministische ordening (guide §18.3.6, verplichting 4 in flow 22);
noemt de merge dat een conflict, dan wordt elke gedeelde container een conflictmagneet.

**Actueel blijven — het sync-protocol.** Het schema is pull-based, net als de SysML v2 API; de
onderdelen bestaan, de service verbindt ze:

| Stap | Onderdeel | Waarom |
|---|---|---|
| 1 | **Detecteren.** De client onthoudt de head waarmee hij voor het laatst gesynchroniseerd heeft; poll `SELECT head_commit_id FROM branch WHERE id = :b` | Geïndexeerde read onder een milliseconde, dus pollen om de 1–2 s is goedkoop. Voor push: de API-server die de CAS won kent de nieuwe head al en kan die uitzenden (SignalR / WebSocket). PostgreSQL `LISTEN/NOTIFY` wordt pas interessant met meerdere API-server-instanties; het schema heeft vandaag zo'n trigger niet, bewust. |
| 2 | **Lopen.** Volg `commit_parent` van de nieuwe head terug naar de bekende head van de client | Op één branch houdt de CAS de heads strikt lineair (invariant C2), dus dit is een rechte keten — de `known..new`-range van de client. |
| 3 | **De change sets ophalen** van die commits (hieronder), via `ix_element_version_commit` en `ix_derived_version_commit` | O(som van de change sets), onafhankelijk van de modelgrootte. |
| 4 | **Lokaal toepassen.** Vervang elk element op `identity_id`, behandel `tombstone = true` als verwijdering, overschrijf de derived waarden van elke identity in de impact radius | Het lokale model is nu gelijk aan de snapshot op de nieuwe head zonder iets anders opnieuw op te halen — Axioma 1: identities zijn stabiel over versions heen. |
| 5 | **Terugvallen** op de diff van twee commits (flow 16) of een verse set read van de branch head (flow 7) | Als de client ver achterloopt, of als er een merge commit (twee parents) in de range zit — dan is de keten geen keten meer. |

**Change sets — ja, native.** Een commit schrijft alleen rows voor de elementen die hij
veranderde, één version per element per commit (spec Clause 7.1.2, invariant 4), en dat is
precies de `Commit.change`-lijst van DataVersions uit de spec. Twee queries, beide bediend
door de speciale commit-indexen:

```sql
-- wat de gebruiker veranderde in commit :c (stored state, tombstones inbegrepen)
SELECT identity_id, version_id, class_kind, tombstone, stored_json
FROM   sysml2.element_version
WHERE  project_id = :p AND commit_id = :c;

-- wat als gevolg daarvan veranderde: de impact radius (qualifiedName, owner, ...)
SELECT identity_id, qualified_name, name, owner, derived_json
FROM   sysml2.derived_version
WHERE  project_id = :p AND commit_id = :c;
```

De tweede query is het deel dat een naïeve change set mist (Axioma 2): een client die alleen
de stored rows toepast houdt stale qualified names voor alles onder een hernoemde package —
het uitgewerkte voorbeeld van guide §16.1, waar het hernoemen van P een derived row schrijft
voor het onaangeraakte W. Voor een willekeurig paar commits in plaats van een keten geeft
flow 16 added / removed / changed per identity (PASS 25a).

Kleine lettertjes: de gemeten getallen komen van pgbench op PG18 op één machine met het
smoke-model; echte commits dragen JSON-payloads, subtype- en link-rows en derived rows, dus ze
zijn zwaarder per commit — de marge blijft twee tot drie ordes van grootte.

---

## 10. Twee branches mergen

**Use case:** `feature` mergen in `main`.

| Stap | Onderdeel | Waarom |
|---|---|---|
| 1 | Resolve beide heads naar snapshots (flow 7) en vind de gemeenschappelijke voorouder | Input voor de three-way merge. |
| 2 | Bereken conflicten (edits aan hetzelfde element, containment-aware, cycli, dubbele namen) en hun resoluties | Guide §15.10, §15.12. Twee afzonderlijk geldige branches kunnen mergen tot een ongeldig model (bijv. B specialiseert C op de ene branch, C specialiseert B op de andere). |
| 3 | Bouw een commit waarvan de change set **elke conflictresolutie herhaalt** | Spec-invariant 2 (Clause 7.1.2): een merge lost alle conflicten op in zijn eigen change set. Dit is wat "nieuwste voorouder wint" correct maakt: door monotoniciteit is de merge de nieuwste commit in zijn ancestry, dus zijn herhaling verslaat beide parents (PASS 8a). |
| 4 | Herbereken derived state **op de gemergde snapshot**, niet door de derived rijen van beide branches te verenigen | Cross-branch interacties (de ene branch voegt een Specialization toe, de andere een feature aan het doel ervan) leveren derived wijzigingen op die geen van beide branches ooit zag (guide §15.13). |
| 5 | Commit-transactie van flow 4 met **twee** `commit_parent`-edges (`ordinal` 0 = eerste parent, 1 = tweede) | De DAG. `ordinal` behoudt de "first parent"-semantiek precies zoals Git. `trg_commit_parent_version` eist alle parents in de release van de merge zelf (PASS 11d/11e). |
| 6 | CAS op de head van `main`, overlay-upserts voor de herhaalde change set | Zoals elke commit. |

Wat de resolver garandeert: elementen die verwijderd zijn op een branch die **geen** voorouder
van de merge is, blijven levend in de snapshot van de merge (PASS 8b — het wiel dat op c3
verwijderd is, leeft op c5), en derived state foldt over beide parent-lineages (PASS 8c).
Guide §16.2 loopt het voorbeeld door.

Wat er gebeurt als een merge onrechtmatig een herhaling overslaat: monotoniciteit ordent
siblings niet, dus `created DESC` alleen zou niet-deterministisch kiezen. De `id DESC`
tiebreaker (audit R13) maakt het antwoord *stabiel* — min of meer fout voor een onrechtmatige
input, maar testbaar en identiek bij elke read en op elke replica (PASS 10a/10b). Herhalen is
de plicht van de service; de tiebreaker is alleen het vangnet.

---

## 11. Een commit taggen

**Use case:** `POST /projects/{p}/tags` — "v1.0 is commit c2".

| Stap | Onderdeel | Waarom |
|---|---|---|
| 1 | `INSERT tag (id, project_id, name, tagged_commit_id)` | Een bevroren pointer; FK naar `commit` zodat hij nooit kan bungelen (PASS 15a/15b). |
| 2 | Lees het model op de tag | `get_elements_at_commit(project, tagged_commit_id)` — een historische read, flow 7. |

Tags zijn onveranderlijk (geen head om te verschuiven) en vernietigbaar (flow 12). Overweeg
een checkpoint te bouwen op veelgelezen getagde commits (release baselines) zodat historische
reads en pagineren daar rechtstreeks van de checkpoint-PK draaien (flow 13).

`Branch.Head` en `Tag.TaggedCommit` herdefiniëren beide `CommitReference.ReferencedCommit`
aan de .NET-kant (`SysML2.NET/PIM/DTO/CommitReference.cs`); de tabellen spiegelen dat met
dezelfde kolomvorm.

---

## 12. Een branch of tag verwijderen

**Use case:** `DELETE /projects/{p}/branches/{b}`.

De API-delete is een **soft delete plus een cache purge** (guide §6.3, normatief):

| Stap | Onderdeel | Waarom |
|---|---|---|
| 1 | `UPDATE branch SET deleted = now() WHERE id = :b` | `Branch.deleted` is een spec-property: verwijdering is een *vastgelegde gebeurtenis*. Het auditverhaal (naam, levensduur, laatste head) blijft bewaard, en omdat commits onvernietigbaar zijn blijven de commits van de branch in de DAG. |
| 2 | `DELETE FROM branch_head WHERE project_id = :p AND branch_id = :b` | De overlay is een herbouwbare cache en het enige deel waarvan de omvang ertoe doet. O(divergentie), index-backed via `ix_branch_head_branch`. |
| 3 | De naam is meteen herbruikbaar | `ux_branch_project_name_live` is partieel op `deleted IS NULL`; dubbele *levende* namen worden nog steeds geweigerd (PASS 27a/27b). |

Hetzelfde protocol voor tags (PASS 27c). Een harde `DELETE FROM branch` is het
administratieve purge-pad, niet het API-pad. Eén eerlijke beperking: geen enkel spec-record
draagt een actor, dus *wie* verwijderde is op deze laag niet auditeerbaar.

---

## 13. Checkpoints: de achtergrond-onderhoudslus

**Use case:** reads snel houden terwijl de historie groeit. Niet door de gebruiker getriggerd
— een cadanspolicy aan de service-kant (guide §10.1, §15.2), altijd asynchroon, **nooit op het
commit-pad**.

| Trigger | Actie | Waarom |
|---|---|---|
| ≥200 commits sinds de dichtstbijzijnde gecheckpointe voorouder op een lineage, **of** cumulatieve change-set omvang sindsdien ≥25% van het model | `SELECT build_commit_checkpoint(project, commit)` | Begrenst hoe ver een resolver ooit loopt. O(model) per stuk (12–15 s bij 1M, gemeten), dus churn-gebaseerd, niet elke-N-commits. |
| Een branch wordt geforkt | checkpoint de fork base | De overlay-invariant: `base_commit_id` moet gecheckpoint zijn. |
| Een conversion commit (flow 18) | checkpoint hem | Zodat folds zelden een release-grens oversteken. |
| De overlay van een branch overschrijdt ~10% van het model of ~100k rijen | **compaction**: checkpoint de branch head, verwijs `base_commit_id` om, truncate de overlay — onder de branch lock | Houdt set-reads en de anti-join klein. Moet dezelfde branch lock nemen als commits, anders beschrijft de overlay uiteindelijk divergentie van de verkeerde base. |
| Een checkpoint waar geen branch op baseert en dat de historische ladder niet nodig heeft | verwijder het: eerst de registry-rij, dan de rijen | Elk bewaard checkpoint van 1M elementen is ~0,2 GB. |

`build_commit_checkpoint` is `INSERT … SELECT FROM resolve_commit_state` plus de registry-rij
in één statement, `ON CONFLICT DO NOTHING` — idempotent, dus twee workers die hetzelfde
checkpoint bouwen verspillen alleen wat werk, en het bestaat naast levende commits omdat het
alleen onveranderlijke historie leest.

Waarom `commit_checkpoint_registry` überhaupt bestaat: de resolvers vragen bij elke
recursiestap "is deze commit gecheckpoint?". Daarvoor `commit_checkpoint` proben leek
index-vriendelijk, maar de planner zag `n_distinct(commit_id) ≈ 1` en koos per stap een seq
scan — een historische read van één element duurde 3,5 s. Een registry van één rij per
checkpoint maakt de probe een PK-lookup: 1,8–4 ms (guide §10.3). Algemene les: een
EXISTS-probe in een tabel die fijner gesleuteld is dan de gestelde vraag is een
statistiekenval.

De benchmark speelt 2.000 commits af met elk kwart een checkpoint (`ReplayHistoryAsync`) en
meet daarna reads op verschillende afstanden van een checkpoint.

---

## 14. Het model valideren

**Use case:** "is deze commit referentieel gezond?" — dangling en wrong-type references.

FK's bewijzen dat een reference naar een **bestaande identity** wijst. Ze kunnen niet bewijzen
dat het doel **levend is op commit C**, noch dat zijn metaclass toegestaan is voor de
verwijzende property. Beide worden beantwoord door gegenereerde functies, één `UNION ALL`-arm
per stored reference-kolom (42), die elk `'dangling'` of `'wrong-type'` rapporteren (guide §7):

| Laag | Functie | Wanneer | Kosten |
|---|---|---|---|
| Incrementeel | `validate_references_in_commit(project, commit)` | **Elke commit**, synchroon, bewaakt de acceptatie | O(change set): de uitgaande references van de change set **plus de omgekeerde richting die zijn tombstones breken** (een levende, ongewijzigde houder die bungelend achterblijft — het geval dat naïeve validatie mist). 77–86 ms voor 101 rijen tegen een project van 1M. PASS 13a–13c. |
| Volledige audit | `validate_references_at_commit(project, commit)` | Periodiek (de checkpoint-cadans is een natuurlijk ritme) als vangnet | O(snapshot × log historie): de snapshot wordt eerst gematerialiseerd in een ge-ANALYZE'de temp table, zodat de planner nooit de volledige historie joint. 2,5–4,3 s bij 1M. PASS 12b/12c. |

Bewust functies, geen constraints: de spec staat tijdelijk bungelende references toe, en
cross-project liveness hangt af van `project_usage` (flow 17). De benchmark draait beide lagen
op een authentieke load van 100k elementen (`Reference_validation_is_clean_at_both_tiers`).

Naast references is de service ook **semantische** validatie na een merge verschuldigd
(dubbele namen, specialization- / ownership-cycli — guide §15.12).

---

## 15. Het model queryen

**Use case:** `POST /projects/{p}/queries` (opslaan), `GET …/queries/{q}/results`
(uitvoeren), of een ad-hoc query vanuit de API.

**Opslaan** (PASS 23a–23c): `INSERT query (…, query_json)` met de eigen JSON van de spec
`{select, where, orderBy, scope}`. Niets in de database interpreteert het; het is muteerbaar en
vernietigbaar volgens de PUT/DELETE van de API. De ruwe spec-vorm opslaan houdt het
release-agnostisch — het wordt op uitvoertijd gecompileerd tegen de release van de
*uitvoerende commit*.

**Uitvoeren** (guide §16.4, PASS 24a): de query translator van de service resolvet elke
`PrimitiveConstraint.property`-naam via de gegenereerde **model-version descriptor** (guide
§12.2, de per-release C# die de oude `property_catalog`-tabel verving) en emitteert SQL over
de snapshot:

| Property routeert naar | Geëmitteerd predicaat |
|---|---|
| een stored kolom op `element_version` (`declaredName`) | `ev.declared_name = $v` |
| een stored kolom op een subtype-tabel (`isVariation`) | join `usage_version`, `u.is_variation = $v` |
| een **gepromoveerde** derived kolom (`qualifiedName`, `name`, `owner`, …) | join `derived_version`, `dv.qualified_name LIKE $v` — een echte btree + statistieken |
| elke andere derived property | `dv.derived_json @> '{…}'` — de GIN-fallback |
| een link-tabel (`ownedRelationship`) | `EXISTS (SELECT 1 FROM element_owned_relationship …)` |
| de metaclass zelf | `ev.class_kind = <ClassKind.PartUsage>` |

De row source is `get_elements_at_branch_head` (ge-inlined, flow 7b) of een geresolvede
commit. Derived properties zijn volwaardige query-doelen juist omdat ze op commit-tijd vooraf
berekend zijn (Clause 2 full conformance).

Twee kant-en-klare vormen die de smoke test bewijst: `GET /roots` is een query over de
gepromoveerde kolom `owner IS NULL` (PASS 17); "welke relationships verwijzen naar element X"
is een reverse lookup via `ix_<link>_target` gejoind aan de snapshot (PASS 18a/18b).

---

## 16. Twee commits diffen

**Use case:** `GET /projects/{p}/commits/{c}/diff?baseCommitId=…`.

Een `FULL JOIN` van twee geresolvede snapshots op `identity_id`: alleen aan één kant aanwezig =
toegevoegd / verwijderd; aan beide kanten aanwezig met een andere `version_id` = gewijzigd.
Tussen c1 en c2 van het smoke-model verschilt alleen de hernoemde package; het onaangeroerde
child resolvet aan beide kanten naar **dezelfde** version-rij (PASS 25a). Checkpoint
historische commits die vaak gedifft zullen worden eerst (flow 13).

---

## 17. Een ander project gebruiken (cross-project references)

**Use case:** project A importeert een library-project B op de release-commit van B.

| Stap | Onderdeel | Waarom |
|---|---|---|
| 1 | `INSERT project_usage (id, project_id = A, used_project_id = B, used_project_commit_id = cB)` | Pint B vast **op een commit**; FK's garanderen dat noch het project noch de commit bungelt (PASS 16a/16b). |
| 2 | Elementen in A verwijzen rechtstreeks naar identities in B | De PK van `data_identity` is de kale uuid, niet `(project_id, id)` — bewust, zodat cross-project references FK-baar zijn (guide §7). |
| 3 | Liveness van B's doelen wordt gecheckt tegen `cB`, niet tegen de head van B | Resolutie aan de service-kant via `project_usage`; de validators type-checken cross-project doelen via de getypeerde identity. |

Prijs van de kale-uuid PK (guide §18.3.3): `@id`s zijn instance-globaal. Klonen over projecten
heen of een kpar importeren met door de client geleverde ids moet verse ids munten of de
PK-overtreding vertalen naar een duidelijke 409. Een inkomende `project_usage` blokkeert ook
het verwijderen van het gebruikte project (flow 19).

---

## 18. Upgraden naar een nieuwe metamodel release

**Use case:** de OMG publiceert een nieuwe release; sommige projecten gaan ernaartoe, andere
nog niet.

Meerdere releases bestaan naast elkaar in één database omdat de stempel **per commit** is
(guide §6.4): `commit.model_version_id` is de waarheid over in welke release de payloads van
een commit geschreven zijn; een branch "is" de release van zijn head;
`project.target_model_version_id` is de upgrade-*policy* van de operator (hoogste release die
nieuwe commits mogen gebruiken; NULL = onbeperkt).

| Stap | Onderdeel | Waarom |
|---|---|---|
| 1 | De operator voegt de release toe aan `ClassKindRegistry.cs` (nieuw ordinaal; nieuwe metaclasses na de hoogste id; vervallen klassen afgesloten met `removed_in`), genereert opnieuw (flow 20), past het nieuwe schema toe | Het fysieke schema is de **superset** over alle releases: nieuwe subtype-tabellen, nieuwe nullable kolommen, niets wordt gedropt. Seeds zijn idempotent. |
| 2 | De operator verhoogt `project.target_model_version_id` | Staat branches van dat project toe te upgraden. |
| 3 | De service schrijft een **conversion commit**: één parent, stempel opgehoogd, met herhaling van precies de elementen waarvan de vorm tussen de releases veranderde | Upgraden is een commit, geen migratie. O(gewijzigde vormen), niet O(model): oude rijen van ongewijzigde elementen blijven geldig onder de nieuwe release. `trg_commit_parent_version` accepteert hem (PASS 11b). Hertypeert de conversie een element, update dan `data_identity.class_kind` in dezelfde transactie. |
| 4 | Forceer een checkpoint op de conversion commit | Zodat folds zelden een release-grens oversteken. |
| 5 | Alles daarna is een normale commit in de nieuwe release | |

Wat de trigger weigert: downgrades (PASS 11c), een branch in release 1 mergen in een head in
release 2 — "eerst converteren, dan mergen" (PASS 11d), en een commit die tegelijk converteert
*en* merget (PASS 11e). De service moet hier duidelijke fouten van maken in plaats van de
exceptie van de trigger door te geven.

De `ClassKind`-enum (`SysML2.NET/Core/AutoGenEnum/ClassKind.cs`) wordt uit dezelfde registry
gegenereerd, dus .NET en de database zijn het per constructie eens over de ids;
`ClassKindEnumGeneratorTestFixture` faalt bij drift.

---

## 19. Een project verwijderen

**Use case:** `DELETE /projects/{p}`.

Geen cascade. Identity-FK's zijn met opzet `NO ACTION`: een cascade voert per-rij deletes uit
die alleen op de FK-kolom filteren, en geen enkele index in het schema begint met een kale
identity-kolom, dus een gecascadeerde projectverwijdering zou de grootste tabellen één keer per
identity seq-scannen (audit R3). In plaats daarvan een **geordende procedure per tabel**,
waarbij elk statement naar één partitie prunet (gedocumenteerd bij de `data_identity`-DDL;
PASS 26a–26c):

```sql
-- 0. inkomende project_usage vanuit ANDERE projecten moet eerst weg zijn (26a blokkeert anders)
DELETE FROM sysml2.element_owned_relationship  WHERE project_id = :p;   -- en de andere link-tabellen
DELETE FROM sysml2.derived_version             WHERE project_id = :p;
DELETE FROM sysml2.element_version             WHERE project_id = :p;   -- subtype-tabellen cascaden vanuit deze composite FK
DELETE FROM sysml2.branch_head                 WHERE project_id = :p;
DELETE FROM sysml2.commit_checkpoint           WHERE project_id = :p;
DELETE FROM sysml2.commit_checkpoint_registry  WHERE project_id = :p;   -- NO ACTION FK naar commit: deze stap overslaan faalt luid (26b)
DELETE FROM sysml2.data_identity               WHERE project_id = :p;
DELETE FROM sysml2.project                     WHERE id = :p;           -- commit, commit_parent, branch, tag, query cascaden vanaf hier
```

De overgebleven `NO ACTION`-FK's zijn het vangnet: verwijderen in de verkeerde volgorde faalt
luid in plaats van stilzwijgend te scannen. Die ruil — expliciete procedure plus luide guard,
in plaats van gemakkelijk plus catastrofaal — is de algemene FK-filosofie van het schema.

---

## 20. Het metamodel wijzigen: het schema opnieuw genereren

**Use case:** de UML XMI is gewijzigd (nieuwe release, of een fix), en het schema moet volgen.

| Stap | Onderdeel | Waarom |
|---|---|---|
| 1 | Bewerk `Resources/KerML_only_xmi.uml` / `Resources/SysML_only_xmi.uml` | De single source of truth. |
| 2 | Voeg toe aan `ClassKindRegistry.cs` als er metaclasses zijn toegevoegd / verwijderd | Ids zijn voor altijd bevroren; de generator **faalt** bij model ↔ registry drift in plaats van te hernummeren. |
| 3 | Draai `SQLSchemaGeneratorTestFixture` | Drijft `SQLSchemaGenerator.cs` aan → rendert `Templates/Uml/core-sql-schema-2.hbs`: handgeschreven secties letterlijk (PIM, resolvers), `[GENERATED]`-secties via de acht helpers in `SqlSchemaHelpers.cs` (enums, registry seeds, link- + subtype-tabellen, views, validators, partitielijst) met de census in `SqlSchemaExtensions.cs`. Output: `Sql/schema2.generated.sql`. |
| 4 | Draai `ClassKindEnumGeneratorTestFixture` | Genereert `ClassKind.cs` opnieuw uit dezelfde registry. |
| 5 | Draai de `Integration`-fixtures (`SqlSchemaSmokeTestFixture`, `SqlSchemaConcurrencyTestFixture`) | Installeert het gegenereerde schema op een PostgreSQL 18 Testcontainer en draait de smoke van 59 assertions + de concurrency suite. |
| 6 | Draai `/sync-schema-guides` | De documentatieset (`SQLSCHEMA.md`, beide guides, glossaria) en het Schema Atlas-artifact moeten het schema volgen. CLAUDE.md maakt dit verplicht. |

Census-regels die de generator vastlegt (en die met de hand makkelijk fout gaan, guide §3):
gedeclareerde properties worden berekend als `QueryAllProperties(class)` min die van de
generals — nooit uit `OwnedAttribute`, omdat association ends eigendom zijn van de
association; alleen **same-name** redefinitions zijn storage-vrij; een metaclass krijgt een
subtype-tabel als en slechts als hij ≥1 enkelwaardige stored property declareert.

---

## 21. De testpiramide: welke test bewijst wat

Alles onder `SysML2.NET.CodeGenerator.Tests/Generators/UmlHandleBarsGenerators/` tenzij
anders vermeld.

| Test | Vereist | Bewijst |
|---|---|---|
| `SQLSchemaGeneratorTestFixture` | niets | De generator rendert uit de XMI en de output komt overeen met de ingecheckte `schema2.generated.sql`. |
| `ClassKindEnumGeneratorTestFixture` | niets | De gecompileerde `ClassKind`-enum komt overeen met de registry (geen drift). |
| `SqlSchemaSmokeTestFixture` (`Integration`) | Docker | Genereert het schema in-process, installeert het, draait `schema.smoke.sql`; het verwachte aantal PASS wordt uit het script zelf gelezen. **Elke flow in dit document, end to end.** |
| `SqlSchemaConcurrencyTestFixture` (`Integration`) | Docker | Flows 9 en 9b onder echte concurrency: hot-branch race, spread branches, reads tijdens een write storm; invarianten C1–C5 (één winnaar per head, lineaire ketens, verliezers schrijven niets, overlay-coherentie). |
| `SqlSchemaBenchmarkTestFixture` (`Benchmark`) | Docker | Flows 4, 5, 7, 7b, 8, 13, 14 op schaal met authentieke serializer-payloads: load throughput, commit-latency, checkpoint builds, branch fleet, reads op elke afstand, planvorm van de keyset-pagina, beide validatielagen, root-rename burst, UUIDv4 vs v7, bloat. Latencies worden gerapporteerd, invarianten ge-assert. Knoppen: `SYSML2_BENCH_ELEMENTS`, `SYSML2_BENCH_COMMITS`, `SYSML2_BENCH_BRANCHES`. |
| `schema.smoke.sql` rechtstreeks via `psql` | PostgreSQL | Dezelfde 59 assertions, ook te draaien tegen het handgeschreven `schema.golden.sql` — beide schema's moeten identiek slagen. |
| `schema.concurrency.*.sql` via `pgbench` | PostgreSQL | De SQL-only route naar de concurrency suite. |
| `SysML2.NET.Serializer.Json.Tests` PIM-fixtures | niets | De .NET-records round-trippen (multi-parent `previousCommit`, `DataVersion` met null payload = tombstone, `Query.orderBy`, `ProjectUsage.usedProjectCommit`). |

Kaart van de smoke-assertions, per flow:

| PASS | Flow |
|---|---|
| 1, 2a/2b, 3, 4, 5 | 4–7: aanmaken, rename-rimpeling (Axioma 2), tombstone, samengevoegde read |
| 6, 14a/14b | commit-invarianten: monotonicity trigger, onveranderlijkheid |
| 7, 12a | Axioma 1: FK naar identity; getypeerde identity |
| 8a–8c, 10a/10b | 10: merge, verwijdering op niet-voorouder, tiebreaker |
| 9a–9f | 8, 12, 13: checkpoint, O(1) branch, read-through, tombstone-masker, overlay-only delete |
| 11a–11e | 2, 18: idempotente seeds, conversion commit, release-regels |
| 12b/12c, 13a–13c | 14: volledige audit, incrementele laag, omgekeerde richting |
| 15a–15c, 27c | 11, 12: tag-levenscyclus |
| 16a/16b, 26a | 17: project usage |
| 17, 18a/18b, 23a–24a | 15: roots, reverse lookup, levenscyclus + uitvoering van stored query |
| 19a–22b | PIM-record reads: project, branch, commit, change set, DataVersion, catalogi |
| 25a | 16: diff |
| 26a–26c | 19: geordende projectverwijdering |
| 27a/27b | 12: branch soft delete, naamhergebruik |

---

## 22. Wat de service layer nog moet bouwen

Het schema is bewust niet zelfrijdend. Dit zijn de onderdelen die de flows hierboven
*veronderstellen* en die boven de database leven (guide §15, in volgorde van prioriteit):

1. **De impact-radius engine** (flow 5) — welke elementen hun derived waarden door een change
   set ongeldig zien worden, herberekend via `SysML2.NET/Extend/*.Compute*`. Ontwerp:
   `IMPACT-RADIUS.md`. Hier zullen de correctheidsbugs van het systeem leven; dit verdient de
   beste tests.
2. **De commit-transactie + het CAS-protocol** (flows 4, 9), met `clock_timestamp()`-stempeling
   en herstempelen-en-opnieuw-proberen bij microseconde-botsingen.
3. **Checkpoint-cadans, retentie, compaction, de base-commit invariant** (flow 13) en het
   **soft-delete protocol voor refs** (flow 12), allemaal asynchroon.
4. **Three-way merge** met auto-merge van additieve collectiewijzigingen, containment-aware
   conflictdetectie, volledigheid van subtree-deletes, semantische validatie na de merge
   inclusief cyclus-guards, en merge impact radius op de gemergde state (flows 6, 9, 10).
5. **Bedrading van de reference validation** (flow 14) — incrementeel per commit, periodiek de
   volledige audit, cross-project resolutie via `project_usage`.
6. **De query translator** over de gegenereerde model-version descriptors (flow 15), en de
   descriptor-generator zelf.
7. **Release-conversie** (flow 18) en de startup-assertie dat de `class_kind`-tabel overeenkomt
   met de gecompileerde `ClassKind`-enum.
8. **Monitoring** van de operationele drempels (overlay-omvang, checkpoint-afstand, NULL bases,
   omvang van derived bursts, CAS-conflict rate, seq-scan tellers, paging-inlining) — guide
   §15.15.
9. **Paginering** verankerd aan commits met keyset-cursors (flow 7b).
10. **Tenant-isolatie** — vandaag geen op de database, bij besluit; PostgreSQL row-level
    security op `project_id` is de natuurlijke verhardingsstap.
11. **Client-synchronisatie** (flow 9b) — head polling of push, change-set replay per
    commit (stored rows *én* impact-radius rows), automatische rebase bij een 409 zodat
    gebruikers die nooit zien; valt terug op de diff (flow 16) als de range geen keten is.
