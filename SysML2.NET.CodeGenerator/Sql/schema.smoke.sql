-- Smoke test for SysML2.NET.CodeGenerator/Sql/schema.golden.sql
--
-- Scenario, exercising the load-bearing claim of the design:
--
--   c1: create Package "Old" and a PartUsage "wheel" owned by it.
--   c2: RENAME the package to "New". The PartUsage is NOT touched — no new element_version row
--       for it — yet its derived qualifiedName MUST change from Old::wheel to New::wheel.
--       This is the case that proves derived state cannot live on element_version.
--   c3: delete the PartUsage (tombstone).
--
-- Everything below runs on a clean database and RAISE EXCEPTIONs on any wrong answer.

SET search_path = sysml2, public;

INSERT INTO sysml2.model_version (id, name, source_fingerprint) VALUES
    (1, 'smoke-release-1', 'smoke:v1'),
    (2, 'smoke-release-2', 'smoke:v2')
ON CONFLICT (id) DO NOTHING;

-- The FROZEN registry ids of the three metaclasses this test uses (see ClassKindRegistry) —
-- identical to the generated schema's seeds, so this INSERT is a no-op there and every
-- hard-coded id in validate_references_at_commit() means the same thing in both contexts.
INSERT INTO sysml2.class_kind (id, name, is_abstract, introduced_in) VALUES
    (116, 'OwningMembership', false, 1),
    (117, 'Package',          false, 1),
    (120, 'PartUsage',        false, 1)
ON CONFLICT (id) DO NOTHING;

INSERT INTO sysml2.project (id, name, created) VALUES
    ('11111111-0000-0000-0000-000000000000', 'SmokeProject', '2026-01-01T00:00:00Z');

INSERT INTO sysml2.commit (id, project_id, created, description, model_version_id) VALUES
    ('c1111111-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000', '2026-01-01T10:00:00Z', 'create', 1),
    ('c2222222-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000', '2026-01-01T11:00:00Z', 'rename package', 1),
    ('c3333333-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000', '2026-01-01T12:00:00Z', 'delete wheel', 1);

INSERT INTO sysml2.commit_parent (commit_id, parent_commit_id, ordinal) VALUES
    ('c2222222-0000-0000-0000-000000000000', 'c1111111-0000-0000-0000-000000000000', 0),
    ('c3333333-0000-0000-0000-000000000000', 'c2222222-0000-0000-0000-000000000000', 0);

INSERT INTO sysml2.branch (id, project_id, name, head_commit_id) VALUES
    ('b1111111-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000', 'main',
     'c2222222-0000-0000-0000-000000000000');

UPDATE sysml2.project SET default_branch_id = 'b1111111-0000-0000-0000-000000000000';

-- Identities: the stable @id of each element, independent of version — TYPED (§4): the
-- metaclass is invariant across versions, so it lives on the identity.
INSERT INTO sysml2.data_identity (id, project_id, class_kind) VALUES
    ('e1111111-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000',
     (SELECT id FROM sysml2.class_kind WHERE name = 'Package')),
    ('e2222222-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000',
     (SELECT id FROM sysml2.class_kind WHERE name = 'PartUsage'));

----------------------------------------------------------------------------------------------
-- c1 — create both elements
----------------------------------------------------------------------------------------------

INSERT INTO sysml2.element_version
    (project_id, version_id, identity_id, commit_id, class_kind, tombstone,
     element_id, declared_name, is_implied_included, stored_json)
VALUES
    ('11111111-0000-0000-0000-000000000000', 'a1111111-0000-0000-0000-000000000000',
     'e1111111-0000-0000-0000-000000000000', 'c1111111-0000-0000-0000-000000000000', (SELECT id FROM sysml2.class_kind WHERE name = 'Package'), false,
     'e1111111-0000-0000-0000-000000000000', 'Old', false,
     '{"@id":"e1111111-0000-0000-0000-000000000000","@type":"Package","declaredName":"Old"}'),

    ('11111111-0000-0000-0000-000000000000', 'a2222222-0000-0000-0000-000000000000',
     'e2222222-0000-0000-0000-000000000000', 'c1111111-0000-0000-0000-000000000000', (SELECT id FROM sysml2.class_kind WHERE name = 'PartUsage'), false,
     'e2222222-0000-0000-0000-000000000000', 'wheel', false,
     '{"@id":"e2222222-0000-0000-0000-000000000000","@type":"PartUsage","declaredName":"wheel"}');

-- PartUsage participates in type_version / feature_version / usage_version / occurrence_usage_version
INSERT INTO sysml2.type_version    VALUES ('11111111-0000-0000-0000-000000000000', 'a2222222-0000-0000-0000-000000000000', false, false);
INSERT INTO sysml2.feature_version VALUES ('11111111-0000-0000-0000-000000000000', 'a2222222-0000-0000-0000-000000000000',
                                     NULL, false, false, false, false, false, false, true, false);
INSERT INTO sysml2.usage_version   VALUES ('11111111-0000-0000-0000-000000000000', 'a2222222-0000-0000-0000-000000000000', false);
INSERT INTO sysml2.occurrence_usage_version VALUES ('11111111-0000-0000-0000-000000000000', 'a2222222-0000-0000-0000-000000000000', false, NULL);

INSERT INTO sysml2.derived_version
    (project_id, derived_id, identity_id, commit_id, owner, qualified_name, name, derived_json)
VALUES
    ('11111111-0000-0000-0000-000000000000', 'd1111111-0000-0000-0000-000000000000',
     'e1111111-0000-0000-0000-000000000000', 'c1111111-0000-0000-0000-000000000000',
     NULL, 'Old', 'Old', '{"qualifiedName":"Old","name":"Old","owner":null}'),

    ('11111111-0000-0000-0000-000000000000', 'd2222222-0000-0000-0000-000000000000',
     'e2222222-0000-0000-0000-000000000000', 'c1111111-0000-0000-0000-000000000000',
     'e1111111-0000-0000-0000-000000000000', 'Old::wheel', 'wheel',
     '{"qualifiedName":"Old::wheel","name":"wheel","owner":"e1111111-0000-0000-0000-000000000000"}');

----------------------------------------------------------------------------------------------
-- c2 — rename the Package ONLY. No new element_version row for the PartUsage.
--      But the PartUsage DOES get a new derived_version row: it is in the impact radius.
----------------------------------------------------------------------------------------------

INSERT INTO sysml2.element_version
    (project_id, version_id, identity_id, commit_id, class_kind, tombstone,
     element_id, declared_name, is_implied_included, stored_json)
VALUES
    ('11111111-0000-0000-0000-000000000000', 'a3333333-0000-0000-0000-000000000000',
     'e1111111-0000-0000-0000-000000000000', 'c2222222-0000-0000-0000-000000000000', (SELECT id FROM sysml2.class_kind WHERE name = 'Package'), false,
     'e1111111-0000-0000-0000-000000000000', 'New', false,
     '{"@id":"e1111111-0000-0000-0000-000000000000","@type":"Package","declaredName":"New"}');

INSERT INTO sysml2.derived_version
    (project_id, derived_id, identity_id, commit_id, owner, qualified_name, name, derived_json)
VALUES
    ('11111111-0000-0000-0000-000000000000', 'd3333333-0000-0000-0000-000000000000',
     'e1111111-0000-0000-0000-000000000000', 'c2222222-0000-0000-0000-000000000000',
     NULL, 'New', 'New', '{"qualifiedName":"New","name":"New","owner":null}'),

    -- the impact radius: the child's derived values changed although the child did not
    ('11111111-0000-0000-0000-000000000000', 'd4444444-0000-0000-0000-000000000000',
     'e2222222-0000-0000-0000-000000000000', 'c2222222-0000-0000-0000-000000000000',
     'e1111111-0000-0000-0000-000000000000', 'New::wheel', 'wheel',
     '{"qualifiedName":"New::wheel","name":"wheel","owner":"e1111111-0000-0000-0000-000000000000"}');

INSERT INTO sysml2.branch_head (project_id, branch_id, identity_id, version_id, derived_id) VALUES
    ('11111111-0000-0000-0000-000000000000', 'b1111111-0000-0000-0000-000000000000',
     'e1111111-0000-0000-0000-000000000000', 'a3333333-0000-0000-0000-000000000000', 'd3333333-0000-0000-0000-000000000000'),
    ('11111111-0000-0000-0000-000000000000', 'b1111111-0000-0000-0000-000000000000',
     'e2222222-0000-0000-0000-000000000000', 'a2222222-0000-0000-0000-000000000000', 'd4444444-0000-0000-0000-000000000000');

----------------------------------------------------------------------------------------------
-- c3 — delete the PartUsage (DataVersion.payload = null)
----------------------------------------------------------------------------------------------

INSERT INTO sysml2.element_version
    (project_id, version_id, identity_id, commit_id, class_kind, tombstone)
VALUES
    ('11111111-0000-0000-0000-000000000000', 'a4444444-0000-0000-0000-000000000000',
     'e2222222-0000-0000-0000-000000000000', 'c3333333-0000-0000-0000-000000000000', (SELECT id FROM sysml2.class_kind WHERE name = 'PartUsage'), true);

----------------------------------------------------------------------------------------------
-- ASSERTIONS
----------------------------------------------------------------------------------------------

DO $$
DECLARE
    proj      constant uuid := '11111111-0000-0000-0000-000000000000';
    wheel     constant uuid := 'e2222222-0000-0000-0000-000000000000';
    c1        constant uuid := 'c1111111-0000-0000-0000-000000000000';
    c2        constant uuid := 'c2222222-0000-0000-0000-000000000000';
    c3        constant uuid := 'c3333333-0000-0000-0000-000000000000';
    actual    text;
    actual_id uuid;
    row_count int;
BEGIN
    -- 1. At c1 the wheel's qualifiedName is Old::wheel
    SELECT dv.qualified_name INTO actual
    FROM sysml2.resolve_commit_state(proj, c1) r
    JOIN sysml2.derived_version dv ON dv.project_id = proj AND dv.derived_id = r.derived_id
    WHERE r.identity_id = wheel;

    IF actual IS DISTINCT FROM 'Old::wheel' THEN
        RAISE EXCEPTION 'FAIL 1: qualifiedName at c1 = %, expected Old::wheel', actual;
    END IF;
    RAISE NOTICE 'PASS 1: qualifiedName at c1 = Old::wheel';

    -- 2. At c2 the qualifiedName is New::wheel -- WITHOUT the wheel having a new version
    SELECT dv.qualified_name, r.version_id INTO actual, actual_id
    FROM sysml2.resolve_commit_state(proj, c2) r
    JOIN sysml2.derived_version dv ON dv.project_id = proj AND dv.derived_id = r.derived_id
    WHERE r.identity_id = wheel;

    IF actual IS DISTINCT FROM 'New::wheel' THEN
        RAISE EXCEPTION 'FAIL 2a: qualifiedName at c2 = %, expected New::wheel', actual;
    END IF;
    RAISE NOTICE 'PASS 2a: qualifiedName at c2 = New::wheel (derived changed)';

    IF actual_id IS DISTINCT FROM 'a2222222-0000-0000-0000-000000000000'::uuid THEN
        RAISE EXCEPTION 'FAIL 2b: wheel version at c2 = %, expected the ORIGINAL a2222222 (unchanged)', actual_id;
    END IF;
    RAISE NOTICE 'PASS 2b: wheel still resolves to its ORIGINAL version a2222222 (stored unchanged)';

    -- 3. At c3 the wheel is gone (tombstone), and only the Package survives
    SELECT count(*) INTO row_count
    FROM sysml2.resolve_commit_state(proj, c3) r
    WHERE r.identity_id = wheel;

    IF row_count <> 0 THEN
        RAISE EXCEPTION 'FAIL 3a: wheel still present at c3 (tombstone not honoured)';
    END IF;

    SELECT count(*) INTO row_count FROM sysml2.resolve_commit_state(proj, c3);
    IF row_count <> 1 THEN
        RAISE EXCEPTION 'FAIL 3b: expected 1 surviving element at c3, got %', row_count;
    END IF;
    RAISE NOTICE 'PASS 3: wheel tombstoned at c3; 1 element survives';

    -- 4. Branch-head read path returns stored || derived as one merged payload
    SELECT sysml2.get_element_at_branch_head('b1111111-0000-0000-0000-000000000000', wheel) ->> 'qualifiedName'
    INTO actual;

    IF actual IS DISTINCT FROM 'New::wheel' THEN
        RAISE EXCEPTION 'FAIL 4a: branch-head payload qualifiedName = %, expected New::wheel', actual;
    END IF;

    SELECT sysml2.get_element_at_branch_head('b1111111-0000-0000-0000-000000000000', wheel) ->> '@type'
    INTO actual;

    IF actual IS DISTINCT FROM 'PartUsage' THEN
        RAISE EXCEPTION 'FAIL 4b: branch-head payload @type = %, expected PartUsage', actual;
    END IF;
    RAISE NOTICE 'PASS 4: branch-head read merges stored_json (@type) with derived_json (qualifiedName)';

    -- 5. get_elements_at_commit returns merged payloads for the whole snapshot
    SELECT count(*) INTO row_count FROM sysml2.get_elements_at_commit(proj, c2);
    IF row_count <> 2 THEN
        RAISE EXCEPTION 'FAIL 5: expected 2 elements at c2, got %', row_count;
    END IF;
    RAISE NOTICE 'PASS 5: get_elements_at_commit(c2) returns 2 merged payloads';
END;
$$;

-- 6. The monotonic-commit trigger must reject a parent edge that goes backwards in time.
--    The snapshot resolver orders by commit.created, so a violation would silently produce the
--    WRONG snapshot rather than an error. Prove the trigger fires.
DO $$
BEGIN
    INSERT INTO sysml2.commit (id, project_id, created, model_version_id)
    VALUES ('c9999999-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000',
            '2020-01-01T00:00:00Z', 1);   -- older than its parent-to-be

    BEGIN
        INSERT INTO sysml2.commit_parent (commit_id, parent_commit_id, ordinal)
        VALUES ('c9999999-0000-0000-0000-000000000000', 'c1111111-0000-0000-0000-000000000000', 0);

        RAISE EXCEPTION 'FAIL 6: non-monotonic commit parent was ACCEPTED';
    EXCEPTION WHEN check_violation THEN
        RAISE NOTICE 'PASS 6: non-monotonic commit parent rejected by trg_commit_parent_monotonic';
    END;
END;
$$;

-- 7. Referential integrity targets IDENTITIES, not versions: a reference to an unknown element
--    must be rejected.
DO $$
BEGIN
    BEGIN
        INSERT INTO sysml2.element_owned_relationship (project_id, version_id, ordinal, target_identity)
        VALUES ('11111111-0000-0000-0000-000000000000', 'a1111111-0000-0000-0000-000000000000', 0,
                'dddddddd-dead-dead-dead-dddddddddddd');

        RAISE EXCEPTION 'FAIL 7: dangling reference to a non-existent data_identity was ACCEPTED';
    EXCEPTION WHEN foreign_key_violation THEN
        RAISE NOTICE 'PASS 7: dangling element reference rejected by FK to data_identity';
    END;
END;
$$;

----------------------------------------------------------------------------------------------
-- MERGE SCENARIO — the DAG fold's hardest case.
--
--   c1 ──> c2 (rename Package to "New") ──> c3 (delete wheel)
--    \                                  \
--     \                                  ──> c5 (MERGE: parents c2 + c4,
--      ──> c4 (rename Package to "Other")        resolves Package to "Merged")
--
-- Clause 7.1.2: "A Commit must resolve all conflicts in its parent Commits" — the merge carries
-- the resolution in its OWN change set, which is exactly why newest-ancestor-wins is correct.
-- Note c3 is NOT an ancestor of c5, so the wheel deleted at c3 must still be ALIVE at c5.
----------------------------------------------------------------------------------------------

INSERT INTO sysml2.commit (id, project_id, created, description, model_version_id) VALUES
    ('c4444444-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000', '2026-01-01T10:30:00Z', 'concurrent rename on side branch', 1),
    ('c5555555-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000', '2026-01-01T13:00:00Z', 'merge c2 + c4', 1);

INSERT INTO sysml2.commit_parent (commit_id, parent_commit_id, ordinal) VALUES
    ('c4444444-0000-0000-0000-000000000000', 'c1111111-0000-0000-0000-000000000000', 0),
    ('c5555555-0000-0000-0000-000000000000', 'c2222222-0000-0000-0000-000000000000', 0),
    ('c5555555-0000-0000-0000-000000000000', 'c4444444-0000-0000-0000-000000000000', 1);

INSERT INTO sysml2.element_version
    (project_id, version_id, identity_id, commit_id, class_kind, tombstone,
     element_id, declared_name, is_implied_included, stored_json)
VALUES
    -- c4: the side branch renames the Package to "Other" (conflicts with c2's "New")
    ('11111111-0000-0000-0000-000000000000', 'a5555555-0000-0000-0000-000000000000',
     'e1111111-0000-0000-0000-000000000000', 'c4444444-0000-0000-0000-000000000000', (SELECT id FROM sysml2.class_kind WHERE name = 'Package'), false,
     'e1111111-0000-0000-0000-000000000000', 'Other', false,
     '{"@id":"e1111111-0000-0000-0000-000000000000","@type":"Package","declaredName":"Other"}'),

    -- c5: the merge resolves the conflict to "Merged" in its own change set
    ('11111111-0000-0000-0000-000000000000', 'a6666666-0000-0000-0000-000000000000',
     'e1111111-0000-0000-0000-000000000000', 'c5555555-0000-0000-0000-000000000000', (SELECT id FROM sysml2.class_kind WHERE name = 'Package'), false,
     'e1111111-0000-0000-0000-000000000000', 'Merged', false,
     '{"@id":"e1111111-0000-0000-0000-000000000000","@type":"Package","declaredName":"Merged"}');

DO $$
DECLARE
    proj      constant uuid := '11111111-0000-0000-0000-000000000000';
    package   constant uuid := 'e1111111-0000-0000-0000-000000000000';
    wheel     constant uuid := 'e2222222-0000-0000-0000-000000000000';
    c5        constant uuid := 'c5555555-0000-0000-0000-000000000000';
    actual    text;
    actual_id uuid;
    row_count int;
BEGIN
    -- 8a. At the merge, the Package resolves to the MERGE's own resolution, not either parent's
    SELECT ev.declared_name, r.version_id INTO actual, actual_id
    FROM sysml2.resolve_commit_state(proj, c5) r
    JOIN sysml2.element_version ev ON ev.project_id = proj AND ev.version_id = r.version_id
    WHERE r.identity_id = package;

    IF actual IS DISTINCT FROM 'Merged'
       OR actual_id IS DISTINCT FROM 'a6666666-0000-0000-0000-000000000000'::uuid THEN
        RAISE EXCEPTION 'FAIL 8a: Package at merge = % (version %), expected Merged (a6666666)', actual, actual_id;
    END IF;
    RAISE NOTICE 'PASS 8a: merge resolution wins over both parents (Package = Merged)';

    -- 8b. The wheel was deleted on c3, but c3 is NOT an ancestor of the merge — it must be
    --     alive at c5, still at its original version from c1.
    SELECT r.version_id INTO actual_id
    FROM sysml2.resolve_commit_state(proj, c5) r
    WHERE r.identity_id = wheel;

    IF actual_id IS DISTINCT FROM 'a2222222-0000-0000-0000-000000000000'::uuid THEN
        RAISE EXCEPTION 'FAIL 8b: wheel at merge = %, expected original a2222222 (c3 is not an ancestor)', actual_id;
    END IF;
    RAISE NOTICE 'PASS 8b: wheel alive at merge (deletion on c3 correctly out of scope)';

    -- 8c. Exactly 2 elements at the merge; and the wheel still carries its newest derived row
    --     (d4444444, written at c2 — reachable through the c2 parent).
    SELECT count(*) INTO row_count FROM sysml2.resolve_commit_state(proj, c5);
    IF row_count <> 2 THEN
        RAISE EXCEPTION 'FAIL 8c: expected 2 elements at merge, got %', row_count;
    END IF;

    SELECT r.derived_id INTO actual_id
    FROM sysml2.resolve_commit_state(proj, c5) r
    WHERE r.identity_id = wheel;

    IF actual_id IS DISTINCT FROM 'd4444444-0000-0000-0000-000000000000'::uuid THEN
        RAISE EXCEPTION 'FAIL 8c: wheel derived at merge = %, expected d4444444 (from c2 side)', actual_id;
    END IF;
    RAISE NOTICE 'PASS 8c: merge snapshot complete; derived state folds across the DAG';
END;
$$;
----------------------------------------------------------------------------------------------
-- OVERLAY SCENARIO — branch_head as sparse divergence over a base checkpoint.
--
-- Checkpoint c2, fork branch b2 from it with ZERO overlay rows, then diverge by deleting the
-- wheel on b2 only. Reads must merge overlay-over-checkpoint; branch deletion must remove only
-- the overlay.
----------------------------------------------------------------------------------------------

DO $$
DECLARE
    proj      constant uuid := '11111111-0000-0000-0000-000000000000';
    c2        constant uuid := 'c2222222-0000-0000-0000-000000000000';
    b2        constant uuid := 'b2222222-0000-0000-0000-000000000000';
    wheel     constant uuid := 'e2222222-0000-0000-0000-000000000000';
    row_count bigint;
    actual    text;
BEGIN
    row_count := sysml2.build_commit_checkpoint(proj, c2);

    IF row_count <> 2 THEN
        RAISE EXCEPTION 'FAIL 9a: checkpoint at c2 materialized % rows, expected 2', row_count;
    END IF;
    RAISE NOTICE 'PASS 9a: build_commit_checkpoint(c2) materialized 2 rows';

    INSERT INTO sysml2.branch (id, project_id, name, head_commit_id, base_commit_id)
    VALUES (b2, proj, 'overlay-branch', c2, c2);

    -- zero overlay rows written: branch creation is O(1), reads come from the checkpoint
    SELECT sysml2.get_element_at_branch_head(b2, wheel) ->> 'qualifiedName' INTO actual;

    IF actual IS DISTINCT FROM 'New::wheel' THEN
        RAISE EXCEPTION 'FAIL 9b: empty-overlay read = %, expected New::wheel from the base checkpoint', actual;
    END IF;
    RAISE NOTICE 'PASS 9b: empty overlay reads through to the base checkpoint';

    SELECT count(*) INTO row_count FROM sysml2.get_elements_at_branch_head(b2);

    IF row_count <> 2 THEN
        RAISE EXCEPTION 'FAIL 9c: set read over empty overlay returned %, expected 2', row_count;
    END IF;
    RAISE NOTICE 'PASS 9c: set read over empty overlay returns the full checkpoint state';

    -- diverge: delete the wheel ON THIS BRANCH ONLY (tombstone overlay row pointing at the
    -- tombstone element_version written at c3)
    INSERT INTO sysml2.branch_head (project_id, branch_id, identity_id, version_id, derived_id, is_tombstone)
    VALUES (proj, b2, wheel, 'a4444444-0000-0000-0000-000000000000', NULL, true);

    IF sysml2.get_element_at_branch_head(b2, wheel) IS NOT NULL THEN
        RAISE EXCEPTION 'FAIL 9d: tombstoned overlay row did not mask the checkpoint';
    END IF;
    RAISE NOTICE 'PASS 9d: overlay tombstone masks the checkpoint row';

    SELECT count(*) INTO row_count FROM sysml2.get_elements_at_branch_head(b2);

    IF row_count <> 1 THEN
        RAISE EXCEPTION 'FAIL 9e: set read after overlay tombstone returned %, expected 1', row_count;
    END IF;
    RAISE NOTICE 'PASS 9e: set read merges overlay divergence over the checkpoint';

    -- branch deletion removes ONLY the overlay; the shared checkpoint stays
    DELETE FROM sysml2.branch WHERE id = b2;

    SELECT count(*) INTO row_count FROM sysml2.branch_head bh WHERE bh.project_id = proj AND bh.branch_id = b2;

    IF row_count <> 0 THEN
        RAISE EXCEPTION 'FAIL 9f: branch deletion left % overlay rows behind', row_count;
    END IF;

    SELECT count(*) INTO row_count FROM sysml2.commit_checkpoint cc WHERE cc.project_id = proj AND cc.commit_id = c2;

    IF row_count <> 2 THEN
        RAISE EXCEPTION 'FAIL 9f: branch deletion damaged the shared checkpoint (% rows left)', row_count;
    END IF;
    RAISE NOTICE 'PASS 9f: branch deletion removed only the overlay; shared checkpoint intact';
END;
$$;

----------------------------------------------------------------------------------------------
-- DETERMINISM SCENARIO — sibling commits sharing a timestamp.
--
-- The monotonicity invariant orders parent edges only; siblings c6/c7 legitimately share
-- created. A merge that (illegally) fails to restate the conflict must still resolve
-- DETERMINISTICALLY: the id DESC tiebreaker picks c7 (greater id) on every run.
----------------------------------------------------------------------------------------------

INSERT INTO sysml2.commit (id, project_id, created, description, model_version_id) VALUES
    ('c6666666-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000', '2026-01-01T11:30:00Z', 'sibling A', 1),
    ('c7777777-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000', '2026-01-01T11:30:00Z', 'sibling B', 1),
    ('c8888888-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000', '2026-01-01T14:00:00Z', 'merge without restating', 1);

INSERT INTO sysml2.commit_parent (commit_id, parent_commit_id, ordinal) VALUES
    ('c6666666-0000-0000-0000-000000000000', 'c1111111-0000-0000-0000-000000000000', 0),
    ('c7777777-0000-0000-0000-000000000000', 'c1111111-0000-0000-0000-000000000000', 0),
    ('c8888888-0000-0000-0000-000000000000', 'c6666666-0000-0000-0000-000000000000', 0),
    ('c8888888-0000-0000-0000-000000000000', 'c7777777-0000-0000-0000-000000000000', 1);

INSERT INTO sysml2.element_version
    (project_id, version_id, identity_id, commit_id, class_kind, tombstone,
     element_id, declared_name, is_implied_included, stored_json)
VALUES
    ('11111111-0000-0000-0000-000000000000', 'a7777777-0000-0000-0000-000000000000',
     'e1111111-0000-0000-0000-000000000000', 'c6666666-0000-0000-0000-000000000000', (SELECT id FROM sysml2.class_kind WHERE name = 'Package'), false,
     'e1111111-0000-0000-0000-000000000000', 'SiblingA', false,
     '{"@id":"e1111111-0000-0000-0000-000000000000","@type":"Package","declaredName":"SiblingA"}'),

    ('11111111-0000-0000-0000-000000000000', 'a8888888-0000-0000-0000-000000000000',
     'e1111111-0000-0000-0000-000000000000', 'c7777777-0000-0000-0000-000000000000', (SELECT id FROM sysml2.class_kind WHERE name = 'Package'), false,
     'e1111111-0000-0000-0000-000000000000', 'SiblingB', false,
     '{"@id":"e1111111-0000-0000-0000-000000000000","@type":"Package","declaredName":"SiblingB"}');

DO $$
DECLARE
    proj      constant uuid := '11111111-0000-0000-0000-000000000000';
    package   constant uuid := 'e1111111-0000-0000-0000-000000000000';
    c8        constant uuid := 'c8888888-0000-0000-0000-000000000000';
    first_id  uuid;
    second_id uuid;
BEGIN
    SELECT r.version_id INTO first_id
    FROM sysml2.resolve_commit_state(proj, c8) r
    WHERE r.identity_id = package;

    SELECT r.version_id INTO second_id
    FROM sysml2.resolve_commit_state(proj, c8) r
    WHERE r.identity_id = package;

    IF first_id IS DISTINCT FROM 'a8888888-0000-0000-0000-000000000000'::uuid
       OR second_id IS DISTINCT FROM first_id THEN
        RAISE EXCEPTION 'FAIL 10a: timestamp tie resolved to % then % — expected a8888888 (c7, greater id) both times', first_id, second_id;
    END IF;
    RAISE NOTICE 'PASS 10a: sibling-timestamp tie resolves deterministically (id DESC winner)';

    SELECT r.version_id INTO first_id
    FROM sysml2.resolve_element_at_commit(proj, c8, package) r;

    IF first_id IS DISTINCT FROM 'a8888888-0000-0000-0000-000000000000'::uuid THEN
        RAISE EXCEPTION 'FAIL 10b: resolve_element_at_commit tie winner = %, expected a8888888', first_id;
    END IF;
    RAISE NOTICE 'PASS 10b: single-element resolver agrees with the full fold on the tie';
END;
$$;

----------------------------------------------------------------------------------------------
-- MULTI-VERSION SCENARIO — commit-stamped metamodel releases.
--
-- The registry seeds are idempotent; a CONVERSION COMMIT (single parent, higher release) is
-- the only way up; downgrades and mixed-release merges must be rejected by
-- trg_commit_parent_version rather than silently mixing payload shapes.
----------------------------------------------------------------------------------------------

DO $$
DECLARE
    proj        constant uuid := '11111111-0000-0000-0000-000000000000';
    c2          constant uuid := 'c2222222-0000-0000-0000-000000000000';
    c3          constant uuid := 'c3333333-0000-0000-0000-000000000000';
    c4          constant uuid := 'c4444444-0000-0000-0000-000000000000';
    conv        constant uuid := 'ca111111-0000-0000-0000-000000000000';   -- conversion commit, release 2
    down        constant uuid := 'ca222222-0000-0000-0000-000000000000';   -- downgrade attempt, release 1
    mixed       constant uuid := 'ca333333-0000-0000-0000-000000000000';   -- mixed-release merge attempt
    combo       constant uuid := 'ca444444-0000-0000-0000-000000000000';   -- convert+merge combo attempt
    count_before int;
    count_after  int;
BEGIN
    -- 11a. The registry seeds are idempotent: re-applying them to a populated database is a
    --      no-op, not a corruption (the old FRESH-INSTALLS-ONLY trap is gone by construction).
    SELECT count(*) INTO count_before FROM sysml2.class_kind;

    INSERT INTO sysml2.class_kind (id, name, is_abstract, introduced_in) VALUES
        (116, 'OwningMembership', false, 1),
        (117, 'Package',          false, 1),
        (120, 'PartUsage',        false, 1)
    ON CONFLICT (id) DO NOTHING;

    SELECT count(*) INTO count_after FROM sysml2.class_kind;

    IF count_after <> count_before THEN
        RAISE EXCEPTION 'FAIL 11a: seed re-apply changed class_kind from % to % rows', count_before, count_after;
    END IF;
    RAISE NOTICE 'PASS 11a: class_kind seed re-apply is idempotent';

    -- 11b. A conversion commit — single parent, HIGHER release — is accepted.
    INSERT INTO sysml2.commit (id, project_id, created, description, model_version_id)
    VALUES (conv, proj, '2026-01-01T15:00:00Z', 'conversion to release 2', 2);

    INSERT INTO sysml2.commit_parent (commit_id, parent_commit_id, ordinal)
    VALUES (conv, c3, 0);

    RAISE NOTICE 'PASS 11b: conversion commit (release 1 -> 2, single parent) accepted';

    -- 11c. A DOWNGRADE — child in an older release than its parent — is rejected.
    INSERT INTO sysml2.commit (id, project_id, created, description, model_version_id)
    VALUES (down, proj, '2026-01-01T15:30:00Z', 'illegal downgrade', 1);

    BEGIN
        INSERT INTO sysml2.commit_parent (commit_id, parent_commit_id, ordinal)
        VALUES (down, conv, 0);

        RAISE EXCEPTION 'FAIL 11c: downgrade commit parent was ACCEPTED';
    EXCEPTION WHEN check_violation THEN
        RAISE NOTICE 'PASS 11c: downgrade rejected by trg_commit_parent_version';
    END;

    -- 11d. A MERGE across releases — parents 2 and 1 — is rejected the moment the second
    --      parent edge lands, regardless of insertion order.
    INSERT INTO sysml2.commit (id, project_id, created, description, model_version_id)
    VALUES (mixed, proj, '2026-01-01T16:00:00Z', 'illegal mixed-release merge', 2);

    BEGIN
        INSERT INTO sysml2.commit_parent (commit_id, parent_commit_id, ordinal)
        VALUES (mixed, conv, 0),
               (mixed, c2, 1);

        RAISE EXCEPTION 'FAIL 11d: mixed-release merge was ACCEPTED';
    EXCEPTION WHEN check_violation THEN
        RAISE NOTICE 'PASS 11d: mixed-release merge rejected (convert first, then merge)';
    END;

    -- 11e. A convert+merge COMBO — both parents in release 1, child claiming release 2 — is
    --      also rejected: the conversion must be its own single-parent commit.
    INSERT INTO sysml2.commit (id, project_id, created, description, model_version_id)
    VALUES (combo, proj, '2026-01-01T16:30:00Z', 'illegal convert+merge combo', 2);

    BEGIN
        INSERT INTO sysml2.commit_parent (commit_id, parent_commit_id, ordinal)
        VALUES (combo, c2, 0),
               (combo, c4, 1);

        RAISE EXCEPTION 'FAIL 11e: convert+merge combo was ACCEPTED';
    EXCEPTION WHEN check_violation THEN
        RAISE NOTICE 'PASS 11e: convert+merge combo rejected (conversion must be single-parent)';
    END;
END;
$$;

----------------------------------------------------------------------------------------------
-- TYPED-IDENTITY & REFERENCE-VALIDATION SCENARIO
--
-- The composite FK makes a version that claims a different metaclass than its identity
-- impossible; validate_references_at_commit() reports what FKs cannot: 'wrong-type' (via the
-- typed identity) and 'dangling' (target not alive in the commit's snapshot).
----------------------------------------------------------------------------------------------

DO $$
DECLARE
    proj      constant uuid := '11111111-0000-0000-0000-000000000000';
    e1        constant uuid := 'e1111111-0000-0000-0000-000000000000';
    e2        constant uuid := 'e2222222-0000-0000-0000-000000000000';
    c2        constant uuid := 'c2222222-0000-0000-0000-000000000000';
    c12       constant uuid := 'cb111111-0000-0000-0000-000000000000';   -- parentless probe commit
    e3        constant uuid := 'e3333333-0000-0000-0000-000000000000';   -- Package, refs e2 (wrong type)
    e4        constant uuid := 'e4444444-0000-0000-0000-000000000000';   -- Package, refs e5 (dangling)
    e5        constant uuid := 'e5555555-0000-0000-0000-000000000000';   -- OwningMembership, never given a version
    row_count int;
BEGIN
    INSERT INTO sysml2.commit (id, project_id, created, description, model_version_id)
    VALUES (c12, proj, '2026-01-01T17:00:00Z', 'reference-validation probe', 1);

    INSERT INTO sysml2.data_identity (id, project_id, class_kind) VALUES
        (e3, proj, (SELECT id FROM sysml2.class_kind WHERE name = 'Package')),
        (e4, proj, (SELECT id FROM sysml2.class_kind WHERE name = 'Package')),
        (e5, proj, (SELECT id FROM sysml2.class_kind WHERE name = 'OwningMembership'));

    -- 12a. The composite FK rejects a version claiming a different metaclass than its identity
    --      (e1 is a Package identity; the row claims PartUsage).
    BEGIN
        INSERT INTO sysml2.element_version
            (project_id, version_id, identity_id, commit_id, class_kind, tombstone,
             element_id, declared_name, is_implied_included, stored_json)
        VALUES
            (proj, 'ab111111-0000-0000-0000-000000000000', e1, c12,
             (SELECT id FROM sysml2.class_kind WHERE name = 'PartUsage'), false,
             'e1111111-0000-0000-0000-000000000000', 'Imposter', false, '{}');

        RAISE EXCEPTION 'FAIL 12a: version with a different class_kind than its identity was ACCEPTED';
    EXCEPTION WHEN foreign_key_violation THEN
        RAISE NOTICE 'PASS 12a: typed identity rejects a version whose class_kind contradicts its identity';
    END;

    -- 12b. The healthy snapshot at c2 validates clean.
    SELECT count(*) INTO row_count FROM sysml2.validate_references_at_commit(proj, c2);

    IF row_count <> 0 THEN
        RAISE EXCEPTION 'FAIL 12b: healthy snapshot reported % reference problems', row_count;
    END IF;
    RAISE NOTICE 'PASS 12b: healthy snapshot validates clean';

    -- 12c. A wrong-type reference (owning_relationship -> a PartUsage, not a Relationship) and
    --      a dangling reference (owning_relationship -> a Membership identity with no live
    --      version at c12) are both reported, and nothing else.
    INSERT INTO sysml2.element_version
        (project_id, version_id, identity_id, commit_id, class_kind, tombstone,
         element_id, declared_name, is_implied_included, owning_relationship, stored_json)
    VALUES
        (proj, 'ab222222-0000-0000-0000-000000000000', e3, c12,
         (SELECT id FROM sysml2.class_kind WHERE name = 'Package'), false,
         'e3333333-0000-0000-0000-000000000000', 'WrongTypedRef', false, e2, '{}'),

        (proj, 'ab333333-0000-0000-0000-000000000000', e4, c12,
         (SELECT id FROM sysml2.class_kind WHERE name = 'Package'), false,
         'e4444444-0000-0000-0000-000000000000', 'DanglingRef', false, e5, '{}');

    SELECT count(*) INTO row_count FROM sysml2.validate_references_at_commit(proj, c12);

    IF row_count <> 2 THEN
        RAISE EXCEPTION 'FAIL 12c: expected exactly 2 reference problems at c12, got %', row_count;
    END IF;

    SELECT count(*) INTO row_count
    FROM sysml2.validate_references_at_commit(proj, c12) validation
    WHERE (validation.source_identity = e3 AND validation.target_identity = e2 AND validation.problem = 'wrong-type')
       OR (validation.source_identity = e4 AND validation.target_identity = e5 AND validation.problem = 'dangling');

    IF row_count <> 2 THEN
        RAISE EXCEPTION 'FAIL 12c: the 2 problems are not the expected wrong-type/dangling pair';
    END IF;
    RAISE NOTICE 'PASS 12c: validate_references_at_commit reports wrong-type and dangling, nothing else';
END;
$$;

----------------------------------------------------------------------------------------------
-- INCREMENTAL-VALIDATION SCENARIO — the O(change set) tier.
--
--   c13 (parent c2): adds membership e7 and package e6 referencing it — a HEALTHY commit.
--   c14 (parent c13): tombstones e7 ONLY. The incremental tier must catch the reverse
--   direction: live, UNCHANGED e6 is left dangling — the case naive change-set validation
--   misses — and must agree with the full audit pass.
----------------------------------------------------------------------------------------------

DO $$
DECLARE
    proj      constant uuid := '11111111-0000-0000-0000-000000000000';
    c2        constant uuid := 'c2222222-0000-0000-0000-000000000000';
    c12       constant uuid := 'cb111111-0000-0000-0000-000000000000';
    c13       constant uuid := 'cc111111-0000-0000-0000-000000000000';
    c14       constant uuid := 'cd111111-0000-0000-0000-000000000000';
    e3        constant uuid := 'e3333333-0000-0000-0000-000000000000';
    e4        constant uuid := 'e4444444-0000-0000-0000-000000000000';
    e5        constant uuid := 'e5555555-0000-0000-0000-000000000000';
    e6        constant uuid := 'e6666666-0000-0000-0000-000000000000';   -- Package, refs e7
    e7        constant uuid := 'e7777777-0000-0000-0000-000000000000';   -- OwningMembership, tombstoned at c14
    row_count int;
BEGIN
    -- 13a. The incremental tier reports c12's own outgoing problems — and exactly those.
    SELECT count(*) INTO row_count
    FROM sysml2.validate_references_in_commit(proj, c12) validation
    WHERE (validation.source_identity = e3 AND validation.target_identity = 'e2222222-0000-0000-0000-000000000000' AND validation.problem = 'wrong-type')
       OR (validation.source_identity = e4 AND validation.target_identity = e5 AND validation.problem = 'dangling');

    IF row_count <> 2
       OR (SELECT count(*) FROM sysml2.validate_references_in_commit(proj, c12)) <> 2 THEN
        RAISE EXCEPTION 'FAIL 13a: incremental tier does not report exactly c12''s wrong-type/dangling pair';
    END IF;
    RAISE NOTICE 'PASS 13a: incremental tier catches the change set''s outgoing problems';

    -- 13b. A healthy commit validates clean incrementally.
    INSERT INTO sysml2.commit (id, project_id, created, description, model_version_id)
    VALUES (c13, proj, '2026-01-01T18:00:00Z', 'add membership + referencing package', 1);

    INSERT INTO sysml2.commit_parent (commit_id, parent_commit_id, ordinal) VALUES (c13, c2, 0);

    INSERT INTO sysml2.data_identity (id, project_id, class_kind) VALUES
        (e6, proj, (SELECT id FROM sysml2.class_kind WHERE name = 'Package')),
        (e7, proj, (SELECT id FROM sysml2.class_kind WHERE name = 'OwningMembership'));

    INSERT INTO sysml2.element_version
        (project_id, version_id, identity_id, commit_id, class_kind, tombstone,
         element_id, declared_name, is_implied_included, owning_relationship, stored_json)
    VALUES
        (proj, 'ac111111-0000-0000-0000-000000000000', e7, c13,
         (SELECT id FROM sysml2.class_kind WHERE name = 'OwningMembership'), false,
         'e7777777-0000-0000-0000-000000000000', 'm', false, NULL, '{}'),

        (proj, 'ac222222-0000-0000-0000-000000000000', e6, c13,
         (SELECT id FROM sysml2.class_kind WHERE name = 'Package'), false,
         'e6666666-0000-0000-0000-000000000000', 'Owned', false, e7, '{}');

    SELECT count(*) INTO row_count FROM sysml2.validate_references_in_commit(proj, c13);

    IF row_count <> 0 THEN
        RAISE EXCEPTION 'FAIL 13b: healthy commit reported % incremental problems', row_count;
    END IF;
    RAISE NOTICE 'PASS 13b: healthy commit validates clean incrementally';

    -- 13c. Tombstoning e7 breaks UNCHANGED e6's reference: the incremental tier must catch
    --      the reverse direction, and must agree with the full audit pass.
    INSERT INTO sysml2.commit (id, project_id, created, description, model_version_id)
    VALUES (c14, proj, '2026-01-01T19:00:00Z', 'delete the membership only', 1);

    INSERT INTO sysml2.commit_parent (commit_id, parent_commit_id, ordinal) VALUES (c14, c13, 0);

    INSERT INTO sysml2.element_version
        (project_id, version_id, identity_id, commit_id, class_kind, tombstone)
    VALUES
        (proj, 'ac333333-0000-0000-0000-000000000000', e7, c14,
         (SELECT id FROM sysml2.class_kind WHERE name = 'OwningMembership'), true);

    SELECT count(*) INTO row_count
    FROM sysml2.validate_references_in_commit(proj, c14) validation
    WHERE validation.source_identity = e6 AND validation.target_identity = e7 AND validation.problem = 'dangling';

    IF row_count <> 1
       OR (SELECT count(*) FROM sysml2.validate_references_in_commit(proj, c14)) <> 1 THEN
        RAISE EXCEPTION 'FAIL 13c: tombstone''s reverse-direction dangling reference not reported exactly once';
    END IF;

    SELECT count(*) INTO row_count FROM sysml2.validate_references_at_commit(proj, c14);

    IF row_count <> 1 THEN
        RAISE EXCEPTION 'FAIL 13c: full audit pass disagrees with the incremental tier (% rows)', row_count;
    END IF;
    RAISE NOTICE 'PASS 13c: incremental tier catches the reverse direction and agrees with the full pass';
END;
$$;

----------------------------------------------------------------------------------------------
-- COMMIT-IMMUTABILITY SCENARIO
--
-- trg_commit_parent_monotonic proves the commit DAG acyclic via strict timestamps, and the
-- resolvers' fold orders by commit.created — both proofs hold only if a commit row can never
-- change after its parent edges are accepted. Prove trg_commit_immutable fires, on the
-- load-bearing column (created) and on any other column (description): the row is frozen
-- wholesale, per the Clause 7.1.2 mutability table.
----------------------------------------------------------------------------------------------

DO $$
BEGIN
    -- 14a. The load-bearing column: a retroactive change of created would silently break the
    --      DAG acyclicity proof and the "newest ancestor wins" fold.
    BEGIN
        UPDATE sysml2.commit
           SET created = created + interval '1 hour'
         WHERE id = 'c1111111-0000-0000-0000-000000000000';

        RAISE EXCEPTION 'FAIL 14a: UPDATE of commit.created was ACCEPTED';
    EXCEPTION WHEN check_violation THEN
        RAISE NOTICE 'PASS 14a: commit.created UPDATE rejected by trg_commit_immutable';
    END;

    -- 14b. Not just the load-bearing columns: the whole row is immutable.
    BEGIN
        UPDATE sysml2.commit
           SET description = 'rewritten history'
         WHERE id = 'c1111111-0000-0000-0000-000000000000';

        RAISE EXCEPTION 'FAIL 14b: UPDATE of commit.description was ACCEPTED';
    EXCEPTION WHEN check_violation THEN
        RAISE NOTICE 'PASS 14b: commit row is immutable wholesale (description UPDATE rejected)';
    END;
END;
$$;
