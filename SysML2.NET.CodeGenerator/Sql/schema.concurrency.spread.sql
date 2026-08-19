-- pgbench script — SPREAD scenario: each client commits on its own branch (contention is
-- branch-local by design; this measures the uncontended ceiling of the same protocol).
-- Run: pgbench -n -c 16 -j 4 -T 30 -f schema.concurrency.spread.sql <db>
\set seed random(1, 1000000000)
\set bidx :client_id % 16 + 1
SELECT sysml2.bench_try_commit(:bidx, :seed);
