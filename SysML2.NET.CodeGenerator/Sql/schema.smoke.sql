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

INSERT INTO sysml2.class_kind (id, name, is_abstract) VALUES
    (1, 'Package',   false),
    (2, 'PartUsage', false);

INSERT INTO sysml2.project (id, name, created) VALUES
    ('11111111-0000-0000-0000-000000000000', 'SmokeProject', '2026-01-01T00:00:00Z');

INSERT INTO sysml2.commit (id, project_id, created, description) VALUES
    ('c1111111-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000', '2026-01-01T10:00:00Z', 'create'),
    ('c2222222-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000', '2026-01-01T11:00:00Z', 'rename package'),
    ('c3333333-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000', '2026-01-01T12:00:00Z', 'delete wheel');

INSERT INTO sysml2.commit_parent (commit_id, parent_commit_id, ordinal) VALUES
    ('c2222222-0000-0000-0000-000000000000', 'c1111111-0000-0000-0000-000000000000', 0),
    ('c3333333-0000-0000-0000-000000000000', 'c2222222-0000-0000-0000-000000000000', 0);

INSERT INTO sysml2.branch (id, project_id, name, head_commit_id) VALUES
    ('b1111111-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000', 'main',
     'c2222222-0000-0000-0000-000000000000');

UPDATE sysml2.project SET default_branch_id = 'b1111111-0000-0000-0000-000000000000';

-- Identities: the stable @id of each element, independent of version.
INSERT INTO sysml2.data_identity (id, project_id) VALUES
    ('e1111111-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000'),  -- the Package
    ('e2222222-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000');  -- the PartUsage

----------------------------------------------------------------------------------------------
-- c1 — create both elements
----------------------------------------------------------------------------------------------

INSERT INTO sysml2.element_version
    (project_id, version_id, identity_id, commit_id, class_kind, tombstone,
     element_id, declared_name, is_implied_included, stored_json)
VALUES
    ('11111111-0000-0000-0000-000000000000', 'a1111111-0000-0000-0000-000000000000',
     'e1111111-0000-0000-0000-000000000000', 'c1111111-0000-0000-0000-000000000000', 1, false,
     'e1111111-0000-0000-0000-000000000000', 'Old', false,
     '{"@id":"e1111111-0000-0000-0000-000000000000","@type":"Package","declaredName":"Old"}'),

    ('11111111-0000-0000-0000-000000000000', 'a2222222-0000-0000-0000-000000000000',
     'e2222222-0000-0000-0000-000000000000', 'c1111111-0000-0000-0000-000000000000', 2, false,
     'e2222222-0000-0000-0000-000000000000', 'wheel', false,
     '{"@id":"e2222222-0000-0000-0000-000000000000","@type":"PartUsage","declaredName":"wheel"}');

-- PartUsage participates in type_v / feature_v / usage_v / occurrence_usage_v
INSERT INTO sysml2.type_v    VALUES ('11111111-0000-0000-0000-000000000000', 'a2222222-0000-0000-0000-000000000000', false, false);
INSERT INTO sysml2.feature_v VALUES ('11111111-0000-0000-0000-000000000000', 'a2222222-0000-0000-0000-000000000000',
                                     NULL, false, false, false, false, false, false, true, false);
INSERT INTO sysml2.usage_v   VALUES ('11111111-0000-0000-0000-000000000000', 'a2222222-0000-0000-0000-000000000000', false);
INSERT INTO sysml2.occurrence_usage_v VALUES ('11111111-0000-0000-0000-000000000000', 'a2222222-0000-0000-0000-000000000000', false, NULL);

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
     'e1111111-0000-0000-0000-000000000000', 'c2222222-0000-0000-0000-000000000000', 1, false,
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
     'e2222222-0000-0000-0000-000000000000', 'c3333333-0000-0000-0000-000000000000', 2, true);

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
    INSERT INTO sysml2.commit (id, project_id, created)
    VALUES ('c9999999-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000',
            '2020-01-01T00:00:00Z');   -- older than its parent-to-be

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

INSERT INTO sysml2.commit (id, project_id, created, description) VALUES
    ('c4444444-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000', '2026-01-01T10:30:00Z', 'concurrent rename on side branch'),
    ('c5555555-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000', '2026-01-01T13:00:00Z', 'merge c2 + c4');

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
     'e1111111-0000-0000-0000-000000000000', 'c4444444-0000-0000-0000-000000000000', 1, false,
     'e1111111-0000-0000-0000-000000000000', 'Other', false,
     '{"@id":"e1111111-0000-0000-0000-000000000000","@type":"Package","declaredName":"Other"}'),

    -- c5: the merge resolves the conflict to "Merged" in its own change set
    ('11111111-0000-0000-0000-000000000000', 'a6666666-0000-0000-0000-000000000000',
     'e1111111-0000-0000-0000-000000000000', 'c5555555-0000-0000-0000-000000000000', 1, false,
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

INSERT INTO sysml2.commit (id, project_id, created, description) VALUES
    ('c6666666-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000', '2026-01-01T11:30:00Z', 'sibling A'),
    ('c7777777-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000', '2026-01-01T11:30:00Z', 'sibling B'),
    ('c8888888-0000-0000-0000-000000000000', '11111111-0000-0000-0000-000000000000', '2026-01-01T14:00:00Z', 'merge without restating');

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
     'e1111111-0000-0000-0000-000000000000', 'c6666666-0000-0000-0000-000000000000', 1, false,
     'e1111111-0000-0000-0000-000000000000', 'SiblingA', false,
     '{"@id":"e1111111-0000-0000-0000-000000000000","@type":"Package","declaredName":"SiblingA"}'),

    ('11111111-0000-0000-0000-000000000000', 'a8888888-0000-0000-0000-000000000000',
     'e1111111-0000-0000-0000-000000000000', 'c7777777-0000-0000-0000-000000000000', 1, false,
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
