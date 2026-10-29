-- Concurrency-suite VERIFICATION — run after any pgbench scenario. Asserts the invariants
-- the CAS protocol (guide §18.2) promises under real concurrency, and reports the tallies.
-- Raises on any violation; prints PASS notices like schema.smoke.sql.

DO $$
DECLARE
    proj          constant uuid := '11111111-0000-0000-0000-00000000cc01';
    seed_commit   constant uuid := 'c0000000-0000-0000-0000-00000000cc01';
    chain_total   bigint;
    rooted        bigint;
    parented      bigint;
    attempts      bigint;
    orphans       bigint;
    multi_parents bigint;
    changed_rows  bigint;
    stray_overlay bigint;
BEGIN
    -- C1. Every bench commit has AT MOST one parent: the protocol never accidentally merges.
    SELECT count(*) INTO multi_parents
    FROM (SELECT cp.commit_id
          FROM sysml2.commit_parent cp
          JOIN sysml2.commit c ON c.id = cp.commit_id
          WHERE c.project_id = proj
          GROUP BY cp.commit_id
          HAVING count(*) > 1) m;

    IF multi_parents <> 0 THEN
        RAISE EXCEPTION 'FAIL C1: % commits have multiple parents', multi_parents;
    END IF;
    RAISE NOTICE 'PASS C1: no accidental merges — every parented commit has exactly one parent';

    -- Materialize the 16 head-to-root chains once; every remaining check reads this table.
    CREATE TEMP TABLE bench_chains ON COMMIT DROP AS
    WITH RECURSIVE chain AS (
        SELECT b.id AS branch_id, b.head_commit_id AS commit_id
        FROM sysml2.branch b
        WHERE b.project_id = '11111111-0000-0000-0000-00000000cc01'

        UNION ALL

        SELECT chain.branch_id, cp.parent_commit_id
        FROM chain
        JOIN sysml2.commit_parent cp ON cp.commit_id = chain.commit_id
    )
    SELECT branch_id, commit_id FROM chain;

    -- C2. Per branch: the head walks back to the seed commit over a strictly linear chain.
    SELECT count(DISTINCT branch_id) INTO rooted
    FROM bench_chains WHERE commit_id = seed_commit;

    IF rooted <> 16 THEN
        RAISE EXCEPTION 'FAIL C2: only % of 16 branch heads reach the seed commit', rooted;
    END IF;
    RAISE NOTICE 'PASS C2: all 16 heads walk back to the seed commit';

    -- C3. No lost updates and no cross-links: the chains partition the parented commits —
    --     the summed chain length (minus the 16 seed entries) equals the parent-edge count.
    SELECT count(*) - 16 INTO chain_total FROM bench_chains;

    SELECT count(*) INTO parented
    FROM sysml2.commit_parent cp
    JOIN sysml2.commit c ON c.id = cp.commit_id
    WHERE c.project_id = proj;

    IF chain_total <> parented THEN
        RAISE EXCEPTION 'FAIL C3: % parent edges but % chain positions — a commit was lost or cross-linked', parented, chain_total;
    END IF;
    RAISE NOTICE 'PASS C3: chains partition all % winning commits — no lost updates', parented;

    -- C4. Exactly the winners wrote data: one element_version per won CAS; losers left only
    --     a parentless orphan commit (the loss tally) and nothing else.
    SELECT count(*) - 1000 INTO changed_rows
    FROM sysml2.element_version ev WHERE ev.project_id = proj;

    SELECT count(*) INTO attempts
    FROM sysml2.commit c WHERE c.project_id = proj AND c.description = 'bench';

    SELECT count(*) INTO orphans
    FROM sysml2.commit c
    WHERE c.project_id = proj AND c.id <> seed_commit
      AND NOT EXISTS (SELECT 1 FROM sysml2.commit_parent cp WHERE cp.commit_id = c.id);

    IF changed_rows <> parented THEN
        RAISE EXCEPTION 'FAIL C4: % element writes for % winning commits — a loser wrote data or a winner did not', changed_rows, parented;
    END IF;

    IF attempts <> parented + orphans THEN
        RAISE EXCEPTION 'FAIL C4: attempts (%) <> wins (%) + losses (%)', attempts, parented, orphans;
    END IF;

    RAISE NOTICE 'PASS C4: attempts % = wins % + CAS losses % (conflict rate %%%); losers wrote nothing',
        attempts, parented, orphans, round(100.0 * orphans / GREATEST(attempts, 1), 1);

    -- C5. Overlay coherence: every overlay row's version was written by a commit on its own
    --     branch's chain.
    SELECT count(*) INTO stray_overlay
    FROM sysml2.branch_head bh
    JOIN sysml2.element_version ev
      ON ev.project_id = bh.project_id AND ev.version_id = bh.version_id
    WHERE bh.project_id = proj
      AND NOT EXISTS (SELECT 1
                      FROM bench_chains bc
                      WHERE bc.branch_id = bh.branch_id AND bc.commit_id = ev.commit_id);

    IF stray_overlay <> 0 THEN
        RAISE EXCEPTION 'FAIL C5: % overlay rows point at versions outside their branch chain', stray_overlay;
    END IF;
    RAISE NOTICE 'PASS C5: every overlay row belongs to its own branch''s chain';

    DROP TABLE bench_chains;
END;
$$;
