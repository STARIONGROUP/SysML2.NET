-- pgbench script — HOT-BRANCH scenario: every client races the commit protocol on branch 1.
-- Run: pgbench -n -c 16 -j 4 -T 30 -f schema.concurrency.hot.sql <db>
\set seed random(1, 1000000000)
SELECT sysml2.bench_try_commit(1, :seed);
