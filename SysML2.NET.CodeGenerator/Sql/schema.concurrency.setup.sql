-- Concurrency-suite SETUP for the SysML2 PostgreSQL schema — run AFTER installing the
-- generated schema (SysML2.NET.CodeGenerator/Sql/schema2.generated.sql) on a scratch database.
--
-- Seeds one project with 1,000 elements, a checkpointed base commit and 16 branches, and
-- installs two TEST-ONLY helper functions the pgbench scripts drive:
--
--   * sysml2.bench_try_commit(branch_index, seed) — one full commit-protocol attempt against
--     branch N. Returns true when the compare-and-swap won, false when it lost (the 409 case).
--   * sysml2.bench_read(n) — one branch-head element read, the read path under write load.
--
-- PROTOCOL NOTE (deliberate deviation from guide §18.2's optimistic ordering): pgbench cannot
-- express "roll back my own writes on CAS failure", so bench_try_commit performs the CAS
-- FIRST. Under READ COMMITTED this is the lock-then-verify variant: the winner holds the
-- branch-row lock while writing its (tiny) change set; a loser blocks on the row lock, then
-- re-evaluates the WHERE against the new head and updates 0 rows. The CONTENTION semantics —
-- exactly one winner per head value, losers detect atomically — are identical to §18.2; only
-- the lock-hold window differs (production computes derived state BEFORE locking).
--
-- TIMESTAMP NOTE (normative for services, guide §18.2): commit.created is stamped with
-- clock_timestamp(), NOT the transaction-start now(). Under concurrency a transaction that
-- started BEFORE the current head committed would otherwise stamp its commit EARLIER than
-- its parent and be rejected by trg_commit_parent_monotonic.

SET search_path = sysml2, public;

INSERT INTO sysml2.project (id, name) VALUES
    ('11111111-0000-0000-0000-00000000cc01', 'ConcurrencyBench');

INSERT INTO sysml2.commit (id, project_id, created, description, model_version_id) VALUES
    ('c0000000-0000-0000-0000-00000000cc01', '11111111-0000-0000-0000-00000000cc01',
     '2026-01-01T00:00:00Z', 'seed', 1);

INSERT INTO sysml2.data_identity (id, project_id, class_kind)
SELECT md5('cid' || i)::uuid, '11111111-0000-0000-0000-00000000cc01',
       (SELECT id FROM sysml2.class_kind WHERE name = 'Package')
FROM generate_series(0, 999) i;

INSERT INTO sysml2.element_version
    (project_id, version_id, identity_id, commit_id, class_kind, tombstone,
     element_id, declared_name, is_implied_included, stored_json)
SELECT '11111111-0000-0000-0000-00000000cc01', md5('v0' || i)::uuid, md5('cid' || i)::uuid,
       'c0000000-0000-0000-0000-00000000cc01',
       (SELECT id FROM sysml2.class_kind WHERE name = 'Package'), false,
       md5('cid' || i), 'seed_' || i, false,
       jsonb_build_object('@id', md5('cid' || i), '@type', 'Package', 'declaredName', 'seed_' || i)
FROM generate_series(0, 999) i;

SELECT sysml2.build_commit_checkpoint('11111111-0000-0000-0000-00000000cc01',
                                      'c0000000-0000-0000-0000-00000000cc01');

-- 16 branches, all based on the checkpointed seed commit (empty overlays).
INSERT INTO sysml2.branch (id, project_id, name, head_commit_id, base_commit_id)
SELECT md5('branch' || i)::uuid, '11111111-0000-0000-0000-00000000cc01',
       'bench-' || i,
       'c0000000-0000-0000-0000-00000000cc01', 'c0000000-0000-0000-0000-00000000cc01'
FROM generate_series(1, 16) i;

ANALYZE;

-- TEST-ONLY: one commit-protocol attempt. Not part of the production schema.
CREATE OR REPLACE FUNCTION sysml2.bench_try_commit(p_branch_index int, p_seed bigint)
RETURNS boolean
LANGUAGE plpgsql
AS $$
DECLARE
    proj       constant uuid := '11111111-0000-0000-0000-00000000cc01';
    v_branch   uuid := md5('branch' || p_branch_index)::uuid;   -- v_ prefix: plain names shadow column names in ON CONFLICT
    expected   uuid;
    new_commit uuid := gen_random_uuid();
    v_identity uuid := md5('cid' || (p_seed % 1000))::uuid;
    new_ver    uuid := gen_random_uuid();
    won        int;
BEGIN
    SELECT head_commit_id INTO expected FROM sysml2.branch WHERE id = v_branch;

    -- The new commit must exist before the branch FK can point at it. A LOSING attempt
    -- therefore leaves one parentless orphan commit behind — the verify script counts
    -- orphans as the loss tally, so no extra contention point is needed for stats.
    INSERT INTO sysml2.commit (id, project_id, created, description, model_version_id)
    VALUES (new_commit, proj, clock_timestamp(), 'bench', 1);

    UPDATE sysml2.branch
       SET head_commit_id = new_commit
     WHERE id = v_branch AND head_commit_id = expected;

    GET DIAGNOSTICS won = ROW_COUNT;

    IF won = 0 THEN
        RETURN false;   -- the 409 path: caller rebases (next pgbench iteration)
    END IF;

    INSERT INTO sysml2.commit_parent (commit_id, parent_commit_id, ordinal)
    VALUES (new_commit, expected, 0);

    INSERT INTO sysml2.element_version
        (project_id, version_id, identity_id, commit_id, class_kind, tombstone,
         element_id, declared_name, is_implied_included, stored_json)
    VALUES
        (proj, new_ver, v_identity, new_commit,
         (SELECT id FROM sysml2.class_kind WHERE name = 'Package'), false,
         v_identity::text, 'r' || p_seed, false,
         jsonb_build_object('@id', v_identity::text, '@type', 'Package', 'declaredName', 'r' || p_seed));

    INSERT INTO sysml2.branch_head (project_id, branch_id, identity_id, version_id, derived_id, is_tombstone)
    VALUES (proj, v_branch, v_identity, new_ver, NULL, false)
    ON CONFLICT (project_id, branch_id, identity_id)
    DO UPDATE SET version_id = EXCLUDED.version_id, derived_id = NULL, is_tombstone = false;

    RETURN true;
END;
$$;

-- TEST-ONLY: one branch-head read against branch 1 (the hot branch).
CREATE OR REPLACE FUNCTION sysml2.bench_read(p_n int)
RETURNS jsonb
LANGUAGE sql
STABLE
AS $$
    SELECT sysml2.get_element_at_branch_head(md5('branch1')::uuid, md5('cid' || p_n)::uuid);
$$;
