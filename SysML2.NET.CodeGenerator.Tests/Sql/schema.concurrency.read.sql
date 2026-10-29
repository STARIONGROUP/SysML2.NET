-- pgbench script — READER scenario: single-element branch-head reads on the hot branch.
-- Run CONCURRENTLY with the hot scenario to measure the MVCC promise: readers never block
-- on writers, and read latency stays flat under a write storm.
-- Run: pgbench -n -c 8 -j 2 -T 30 -f schema.concurrency.read.sql <db>
\set n random(0, 999)
SELECT sysml2.bench_read(:n);
