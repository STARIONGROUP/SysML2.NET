------------------------------------------------------------------------------------------------
-- SysML2.NET — PostgreSQL persistence schema (golden reference)
--
-- This file is the HAND-WRITTEN GOLDEN for the schema that
-- SysML2.NET.CodeGenerator/Templates/Uml/core-sql-schema-2.hbs must emit.
-- The generator's output is diffed against this file. Where a section is marked
-- [GENERATED], the template emits it from Resources/KerML_only_xmi.uml +
-- Resources/SysML_only_xmi.uml; a representative excerpt is shown here.
--
-- Target: PostgreSQL 16+ (declarative partitioning, FKs to partitioned tables,
--         jsonb, MERGE, WITH RECURSIVE).
--
-- Design summary (see SysML2.NET.CodeGenerator/SQLSCHEMA.md for the rationale):
--
--   * The metamodel has 175 metaclasses / 12,963 flattened properties, but only
--     2,698 of those are STORED (`{ get; set; }`); the other 9,582 are DERIVED
--     (`{ get; internal set; }`). The stored surface reduces to 97 declarations
--     across 49 metaclasses.
--   * Stored state is APPEND-ONLY and immutable (commits are immutable per
--     OMG Systems Modeling API & Services v1.0 Clause 7.1.2).
--   * A derived value is a function of (version, snapshot) — NOT of the version
--     alone: renaming a Namespace changes a child's qualifiedName without
--     changing the child's version. Derived state therefore lives in a SECOND
--     append-only stream keyed by (identity, commit).
--   * Referential integrity targets DATA IDENTITIES, never versions. A SysML2
--     reference points at a stable element @id, not at a particular version of it.
------------------------------------------------------------------------------------------------

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE SCHEMA IF NOT EXISTS sysml2;

SET search_path = sysml2, public;

------------------------------------------------------------------------------------------------
-- 1. ENUM TYPES                                                                    [GENERATED]
--
-- Labels are the lowercase form of the C# literal, matching the JSON wire format:
-- SysML2.NET.Serializer.Json/Core/AutoGenSerializer/FeatureSerializer.cs:162 writes
-- `Direction.Value.ToString().ToLower()`. Deserialization is case-insensitive.
------------------------------------------------------------------------------------------------

CREATE TYPE sysml2.feature_direction_kind        AS ENUM ('in', 'inout', 'out');
CREATE TYPE sysml2.portion_kind                  AS ENUM ('timeslice', 'snapshot');
CREATE TYPE sysml2.requirement_constraint_kind   AS ENUM ('assumption', 'requirement');
CREATE TYPE sysml2.state_subaction_kind          AS ENUM ('entry', 'do', 'exit');
CREATE TYPE sysml2.transition_feature_kind       AS ENUM ('trigger', 'guard', 'effect');
CREATE TYPE sysml2.trigger_kind                  AS ENUM ('when', 'at', 'after');
CREATE TYPE sysml2.visibility_kind               AS ENUM ('private', 'protected', 'public');

------------------------------------------------------------------------------------------------
-- 2. METAMODEL CATALOGS                                                            [GENERATED]
--
-- class_kind interns the 167 concrete metaclass names to a smallint. Every element row
-- carries the smallint, not a VARCHAR(100) — at 1M+ elements that is the difference between
-- a 2-byte and a ~20-byte column on the hottest table in the database.
--
-- property_catalog is the bridge between the OMG API and this schema. The Query service
-- translates PrimitiveConstraint.property (an API-level property NAME, e.g. "qualifiedName")
-- into SQL by looking up WHERE that property lives. Without this table, filtering on derived
-- properties — which Derived Property Full Conformance (Clause 2) requires — is not possible.
------------------------------------------------------------------------------------------------

CREATE TABLE sysml2.class_kind (
    id           smallint     NOT NULL,
    name         text         NOT NULL,   -- the API @type value, e.g. 'PartUsage'
    is_abstract  boolean      NOT NULL,
    PRIMARY KEY (id),
    UNIQUE (name)
);

-- Which subtype tables an instance of a given class_kind participates in. Derived from the
-- metaclass's transitive supertype closure, restricted to storage-introducing supertypes.
-- FlowUsage has SIX entries — it inherits both Feature and Relationship storage, because
-- Connector is simultaneously a Feature and a Relationship. The inheritance graph is a DAG,
-- not a chain; this table is what makes that tractable.
CREATE TABLE sysml2.class_kind_table (
    class_kind   smallint     NOT NULL REFERENCES sysml2.class_kind (id),
    table_name   text         NOT NULL,
    ordinal      smallint     NOT NULL,   -- join order, shallowest supertype first
    PRIMARY KEY (class_kind, table_name)
);

CREATE TYPE sysml2.storage_location AS ENUM (
    'column',        -- a real column on element_version or a subtype table
    'link_table',    -- an ordered multi-valued reference or string
    'derived',       -- materialized into derived_version (column or derived_json key)
    'alias'          -- a redeclaration ('new') of an inherited property; no storage of its own
);

CREATE TABLE sysml2.property_catalog (
    class_kind      smallint            NOT NULL REFERENCES sysml2.class_kind (id),
    property_name   text                NOT NULL,   -- the API name, e.g. 'qualifiedName', 'ownedRelationship'
    location        sysml2.storage_location NOT NULL,
    table_name      text                NULL,       -- for 'column' / 'link_table'
    column_name     text                NULL,       -- for 'column'; or the derived_version column
    json_key        text                NULL,       -- for 'derived' values that live in derived_json
    is_reference    boolean             NOT NULL,   -- value type vs. element reference
    is_collection   boolean             NOT NULL,
    is_ordered      boolean             NOT NULL,
    lower_bound     integer             NOT NULL,
    upper_bound     integer             NOT NULL,   -- -1 == unbounded
    PRIMARY KEY (class_kind, property_name)
);

CREATE INDEX ix_property_catalog_name ON sysml2.property_catalog (property_name);

-- [GENERATED] Representative rows — the template emits all 167 classes / 12,963 properties.
--
-- INSERT INTO sysml2.class_kind (id, name, is_abstract) VALUES
--     (1, 'Element', true), (2, 'Relationship', true), ..., (94, 'PartUsage', false), ...;
--
-- INSERT INTO sysml2.class_kind_table (class_kind, table_name, ordinal) VALUES
--     (94, 'element_version', 0), (94, 'type_v', 1), (94, 'feature_v', 2),
--     (94, 'usage_v', 3), (94, 'occurrence_usage_v', 4);
--
-- INSERT INTO sysml2.property_catalog VALUES
--     (94, 'declaredName',      'column',     'element_version',   'declared_name',  NULL, false, false, false, 0,  1),
--     (94, 'isVariation',       'column',     'usage_v',           'is_variation',   NULL, false, false, false, 1,  1),
--     (94, 'ownedRelationship', 'link_table', 'element_owned_relationship', NULL,    NULL, true,  true,  true,  0, -1),
--     (94, 'qualifiedName',     'derived',    'derived_version',   'qualified_name', NULL, false, false, false, 0,  1),
--     (94, 'ownedFeature',      'derived',    'derived_version',   NULL, 'ownedFeature', true, true, true, 0, -1);

------------------------------------------------------------------------------------------------
-- 3. PIM — PROJECTS, COMMITS, BRANCHES, TAGS                                      [HAND-WRITTEN]
--
-- Models Clause 7.1.1/7.1.2 of OMG Systems Modeling API and Services v1.0.
--
-- The C# PIM DTOs (SysML2.NET/PIM/DTO/) match this model: Commit carries `Change` and a
-- multi-valued ordered `PreviousCommit`; Branch/Tag redefine CommitReference.ReferencedCommit;
-- CommitReference.Deleted is nullable. `VersionedData` is deliberately NOT a DTO property —
-- it is derived, unbounded, and resolved here by branch_head / resolve_commit_state (§9).
------------------------------------------------------------------------------------------------

CREATE TABLE sysml2.project (
    id                  uuid        NOT NULL,
    name                text        NULL,
    description         text        NULL,
    resource_identifier text        NULL,
    created             timestamptz NOT NULL DEFAULT now(),
    default_branch_id   uuid        NULL,   -- FK added after branch exists (circular)
    PRIMARY KEY (id)
);

CREATE TABLE sysml2.commit (
    id             uuid        NOT NULL,
    project_id     uuid        NOT NULL REFERENCES sysml2.project (id) ON DELETE CASCADE,
    created        timestamptz NOT NULL DEFAULT now(),
    description    text        NULL,
    PRIMARY KEY (id)
);

CREATE INDEX ix_commit_project_created ON sysml2.commit (project_id, created DESC);

-- Commit.previousCommit is a SET — merges have multiple parents. The DAG lives here.
CREATE TABLE sysml2.commit_parent (
    commit_id         uuid     NOT NULL REFERENCES sysml2.commit (id) ON DELETE CASCADE,
    parent_commit_id  uuid     NOT NULL REFERENCES sysml2.commit (id),
    ordinal           smallint NOT NULL,
    PRIMARY KEY (commit_id, parent_commit_id)
);

CREATE INDEX ix_commit_parent_parent ON sysml2.commit_parent (parent_commit_id);

-- Clause 7.1.2 invariant: "Version histories must monotonically increase in time" — for commit C,
-- C.created is strictly newer than D.created for any D in C.previousCommit. The snapshot resolver
-- in §8 RELIES on this: it picks the version from the newest ancestor commit. Enforce it, because
-- a violation silently produces the wrong snapshot rather than an error.
CREATE OR REPLACE FUNCTION sysml2.assert_commit_monotonic()
RETURNS trigger
LANGUAGE plpgsql
AS $$
DECLARE
    child_created  timestamptz;
    parent_created timestamptz;
BEGIN
    SELECT created INTO child_created  FROM sysml2.commit WHERE id = NEW.commit_id;
    SELECT created INTO parent_created FROM sysml2.commit WHERE id = NEW.parent_commit_id;

    IF child_created <= parent_created THEN
        RAISE EXCEPTION
            'commit % (created %) is not strictly newer than its parent % (created %)',
            NEW.commit_id, child_created, NEW.parent_commit_id, parent_created
            USING ERRCODE = 'check_violation';
    END IF;

    RETURN NEW;
END;
$$;

CREATE TRIGGER trg_commit_parent_monotonic
    AFTER INSERT ON sysml2.commit_parent
    FOR EACH ROW
    EXECUTE FUNCTION sysml2.assert_commit_monotonic();

-- Branch and Tag are both CommitReference. Branch is mutable + destructible; Tag is immutable +
-- destructible (Clause 7.1.2 mutability table).
CREATE TABLE sysml2.branch (
    id              uuid        NOT NULL,
    project_id      uuid        NOT NULL REFERENCES sysml2.project (id) ON DELETE CASCADE,
    name            text        NULL,
    description     text        NULL,
    head_commit_id  uuid        NOT NULL REFERENCES sysml2.commit (id),

    -- The base of the branch_head OVERLAY (see §9): a commit for which a commit_checkpoint has
    -- been materialized (service-enforced invariant). branch_head then stores ONLY the
    -- identities that DIVERGE from that checkpoint, making branch creation and deletion
    -- O(divergence) instead of O(model). NULL means the overlay is the complete head state
    -- (bootstrap / small-project mode).
    base_commit_id  uuid        NULL REFERENCES sysml2.commit (id),

    created         timestamptz NOT NULL DEFAULT now(),
    deleted         timestamptz NULL,
    PRIMARY KEY (id),
    UNIQUE (project_id, name)
);

ALTER TABLE sysml2.project
    ADD CONSTRAINT project_default_branch_fk
    FOREIGN KEY (default_branch_id) REFERENCES sysml2.branch (id) DEFERRABLE INITIALLY DEFERRED;

CREATE TABLE sysml2.tag (
    id                 uuid        NOT NULL,
    project_id         uuid        NOT NULL REFERENCES sysml2.project (id) ON DELETE CASCADE,
    name               text        NULL,
    description        text        NULL,
    tagged_commit_id   uuid        NOT NULL REFERENCES sysml2.commit (id),
    created            timestamptz NOT NULL DEFAULT now(),
    deleted            timestamptz NULL,
    PRIMARY KEY (id),
    UNIQUE (project_id, name)
);

CREATE TABLE sysml2.project_usage (
    id                     uuid NOT NULL,
    project_id             uuid NOT NULL REFERENCES sysml2.project (id) ON DELETE CASCADE,
    used_project_id        uuid NOT NULL REFERENCES sysml2.project (id),
    used_project_commit_id uuid NOT NULL REFERENCES sysml2.commit (id),
    PRIMARY KEY (id)
);

------------------------------------------------------------------------------------------------
-- 4. DATA IDENTITY
--
-- DataIdentity is the version-independent identity of an element — the stable @id that every
-- SysML2 reference points at.
--
-- This table is deliberately NOT partitioned and its PK is the bare uuid: ProjectUsage lets an
-- element in project A reference an element in project B, so a (project_id, id) composite key
-- would make every cross-project reference un-FK-able. Project scoping is enforced by the
-- service layer via project_usage, not by the FK.
------------------------------------------------------------------------------------------------

-- DELETION IS EXPLICIT, NOT CASCADED. At scale (~1M identities per project), FK cascades are a
-- trap: a cascade executes per-row deletes filtered on the FK column ALONE, which no index in
-- this schema leads with — every cascaded identity would trigger scans of the largest tables.
-- Project deletion is therefore an ordered, batched, per-table procedure (each statement prunes
-- to one partition and uses a PK prefix):
--
--     DELETE FROM sysml2.<link/subtype tables>  WHERE project_id = $1;
--     DELETE FROM sysml2.derived_version        WHERE project_id = $1;
--     DELETE FROM sysml2.element_version        WHERE project_id = $1;
--     DELETE FROM sysml2.branch_head            WHERE project_id = $1;
--     DELETE FROM sysml2.commit_checkpoint      WHERE project_id = $1;
--     DELETE FROM sysml2.data_identity          WHERE project_id = $1;
--     DELETE FROM sysml2.project                WHERE id = $1;      -- cascades only PIM rows
--
-- The remaining NO ACTION FKs guarantee the procedure cannot leave dangling references — they
-- block out-of-order deletion instead of silently scanning.
CREATE TABLE sysml2.data_identity (
    id         uuid NOT NULL,
    project_id uuid NOT NULL REFERENCES sysml2.project (id),
    PRIMARY KEY (id)
);

CREATE INDEX ix_data_identity_project ON sysml2.data_identity (project_id);

------------------------------------------------------------------------------------------------
-- 5. STORED ELEMENT STATE — append-only                                          [HAND-WRITTEN]
--
-- One row per (element, commit-in-which-it-changed). Never UPDATEd, never DELETEd. This is the
-- write hot path: a commit is a pure COPY of new rows.
--
-- Element's own six stored properties are folded in here rather than into a separate table —
-- every element has them, so a join would be pure overhead.
--
-- stored_json is a DELIBERATE DENORMALIZATION: the pre-serialized JSON of the element's stored
-- half. The normalized columns and link tables remain the system of record (and carry the FKs);
-- stored_json exists so that a read never has to reassemble an element from six tables. See §9.
------------------------------------------------------------------------------------------------

CREATE TABLE sysml2.element_version (
    project_id           uuid       NOT NULL,
    version_id           uuid       NOT NULL,   -- DataVersion.id
    identity_id          uuid       NOT NULL REFERENCES sysml2.data_identity (id),
    commit_id            uuid       NOT NULL REFERENCES sysml2.commit (id),
    class_kind           smallint   NOT NULL REFERENCES sysml2.class_kind (id),

    -- tombstone == true is DataVersion.payload = null, i.e. a deletion (Clause 7.1.2)
    tombstone            boolean    NOT NULL DEFAULT false,

    -- Element (the only metaclass whose stored properties are folded into the core table)
    -- element_id is text, NOT uuid, deliberately: KerML declares Element::elementId : String.
    -- Only standard-library elements are normatively required to use name-based (version 5,
    -- SHA-1) UUIDs; user-model elementIds carry no format constraint, so uuid would reject
    -- spec-valid data.
    element_id           text       NULL,
    declared_name        text       NULL,
    declared_short_name  text       NULL,
    is_implied_included  boolean    NULL,
    owning_relationship  uuid       NULL REFERENCES sysml2.data_identity (id),

    stored_json          jsonb      NULL,

    PRIMARY KEY (project_id, version_id),

    CONSTRAINT element_version_tombstone_empty
        CHECK (NOT tombstone OR (stored_json IS NULL AND element_id IS NULL)),
    CONSTRAINT element_version_payload_present
        CHECK (tombstone OR (stored_json IS NOT NULL AND element_id IS NOT NULL AND is_implied_included IS NOT NULL))
) PARTITION BY HASH (project_id);

-- Clause 7.1.2: "DataVersion.identity is unique among records listed in Commit.change."
CREATE UNIQUE INDEX ux_element_version_identity_commit
    ON sysml2.element_version (project_id, identity_id, commit_id);

-- lz4 beats the pglz default by a wide margin at this write volume; set BEFORE the partitions
-- are created (§12) so every leaf inherits it.
ALTER TABLE sysml2.element_version ALTER COLUMN stored_json SET COMPRESSION lz4;

CREATE INDEX ix_element_version_commit     ON sysml2.element_version (project_id, commit_id);
CREATE INDEX ix_element_version_class_kind ON sysml2.element_version (project_id, class_kind);
CREATE INDEX ix_element_version_owning_rel ON sysml2.element_version (project_id, owning_relationship)
    WHERE owning_relationship IS NOT NULL;

------------------------------------------------------------------------------------------------
-- 6. LINK TABLES — ordered multi-valued properties                                 [GENERATED]
--
-- The entire metamodel has only SIX distinct multi-valued reference properties and ONE
-- multi-valued value property. Every one of them is isOrdered=true, so `ordinal` is part of
-- the key rather than a set.
--
--   Element::ownedRelationship        (composite)   × 167 classes
--   Relationship::ownedRelatedElement (composite)   ×  62 classes
--   Relationship::source                            ×  62 classes
--   Relationship::target                            ×  62 classes
--   Dependency::client                              ×   1 class
--   Dependency::supplier                            ×   1 class
--   Element::aliasIds                 (string)      × 167 classes
--
-- target_identity FKs to data_identity, NOT to element_version — a reference names an element,
-- not a version of it.
--
-- GENERATOR NOTE: core-sql-schema-2.hbs derives these mechanically. Every reference-valued link
-- table gets an ix_{table}_target reverse-lookup index (this file shows the same rule).
------------------------------------------------------------------------------------------------

CREATE TABLE sysml2.element_owned_relationship (
    project_id      uuid NOT NULL,
    version_id      uuid NOT NULL,
    ordinal         int  NOT NULL,
    target_identity uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id, ordinal),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE INDEX ix_element_owned_relationship_target
    ON sysml2.element_owned_relationship (project_id, target_identity);

CREATE TABLE sysml2.element_alias_ids (
    project_id uuid NOT NULL,
    version_id uuid NOT NULL,
    ordinal    int  NOT NULL,
    value      text NOT NULL,
    PRIMARY KEY (project_id, version_id, ordinal),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.relationship_owned_related_element (
    project_id      uuid NOT NULL,
    version_id      uuid NOT NULL,
    ordinal         int  NOT NULL,
    target_identity uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id, ordinal),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE INDEX ix_relationship_owned_related_element_target
    ON sysml2.relationship_owned_related_element (project_id, target_identity);

CREATE TABLE sysml2.relationship_source (
    project_id      uuid NOT NULL,
    version_id      uuid NOT NULL,
    ordinal         int  NOT NULL,
    target_identity uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id, ordinal),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE INDEX ix_relationship_source_target
    ON sysml2.relationship_source (project_id, target_identity);

CREATE TABLE sysml2.relationship_target (
    project_id      uuid NOT NULL,
    version_id      uuid NOT NULL,
    ordinal         int  NOT NULL,
    target_identity uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id, ordinal),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE INDEX ix_relationship_target_target
    ON sysml2.relationship_target (project_id, target_identity);

CREATE TABLE sysml2.dependency_client (
    project_id      uuid NOT NULL,
    version_id      uuid NOT NULL,
    ordinal         int  NOT NULL,
    target_identity uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id, ordinal),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE INDEX ix_dependency_client_target
    ON sysml2.dependency_client (project_id, target_identity);

CREATE TABLE sysml2.dependency_supplier (
    project_id      uuid NOT NULL,
    version_id      uuid NOT NULL,
    ordinal         int  NOT NULL,
    target_identity uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id, ordinal),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE INDEX ix_dependency_supplier_target
    ON sysml2.dependency_supplier (project_id, target_identity);

------------------------------------------------------------------------------------------------
-- 7. SUBTYPE TABLES — one per storage-introducing metaclass                        [GENERATED]
--
-- 47 tables. A metaclass gets a table iff it DECLARES at least one stored scalar or single
-- reference property of its own. Redeclarations (`new` in C#, redefinition in UML) do NOT get a
-- column — they resolve to the ancestor's column. There are 9 of them:
--
--   CollectExpression::operator, SelectExpression::operator, FeatureChainExpression::operator,
--   IndexExpression::operator          -> operator_expression_v.operator
--   ConnectionDefinition::isSufficient -> type_v.is_sufficient
--   EnumerationDefinition::isVariation -> definition_v.is_variation
--   Expose::isImportAll, Expose::visibility -> import_v.is_import_all / import_v.visibility
--   FramedConcernMembership::kind,
--   RequirementVerificationMembership::kind -> requirement_constraint_membership_v.kind
--
-- Every table is keyed by (project_id, version_id) and co-partitioned with element_version, so
-- the join is partition-local. NOT NULL is safe on [1..1] properties: a row only exists here if
-- the element's class actually inherits this metaclass.
--
-- Dependency introduces no scalar properties (client/supplier are link tables), so it has no
-- subtype table.
--
-- GENERATOR NOTE: core-sql-schema-2.hbs derives these tables mechanically and emits two things
-- this hand-curated file only shows selectively:
--   * a DEFAULT clause for every column whose UML property declares a default value in the XMI
--     (e.g. the Feature booleans DEFAULT false, Membership::visibility DEFAULT 'public',
--     Import::visibility DEFAULT 'private');
--   * an ix_{table}_{column} reverse-lookup index on EVERY data_identity-referencing column,
--     not just the four hot ones annotated below. The specialization graph indexes remain the
--     load-bearing ones for derived-property impact analysis.
------------------------------------------------------------------------------------------------

-- Root: KerML Core
CREATE TABLE sysml2.relationship_v (
    project_id            uuid    NOT NULL,
    version_id            uuid    NOT NULL,
    is_implied            boolean NOT NULL,
    owning_related_element uuid   NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.type_v (
    project_id    uuid    NOT NULL,
    version_id    uuid    NOT NULL,
    is_abstract   boolean NOT NULL,
    is_sufficient boolean NOT NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.feature_v (
    project_id   uuid    NOT NULL,
    version_id   uuid    NOT NULL,
    direction    sysml2.feature_direction_kind NULL,
    is_composite boolean NOT NULL,
    is_constant  boolean NOT NULL,
    is_derived   boolean NOT NULL,
    is_end       boolean NOT NULL,
    is_ordered   boolean NOT NULL,
    is_portion   boolean NOT NULL,
    is_unique    boolean NOT NULL,
    is_variable  boolean NOT NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.membership_v (
    project_id         uuid NOT NULL,
    version_id         uuid NOT NULL,
    member_element     uuid NOT NULL REFERENCES sysml2.data_identity (id),
    member_name        text NULL,
    member_short_name  text NULL,
    -- KerML: Membership::visibility defaults to public
    visibility         sysml2.visibility_kind NOT NULL DEFAULT 'public',
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE INDEX ix_membership_v_member_element ON sysml2.membership_v (project_id, member_element);

CREATE TABLE sysml2.import_v (
    project_id     uuid    NOT NULL,
    version_id     uuid    NOT NULL,
    is_import_all  boolean NOT NULL DEFAULT false,
    is_recursive   boolean NOT NULL DEFAULT false,
    -- KerML: Import::visibility defaults to PRIVATE (unlike Membership), and a top-level
    -- Import owned by a root Namespace MUST be private.
    visibility     sysml2.visibility_kind NOT NULL DEFAULT 'private',
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.membership_import_v (
    project_id          uuid NOT NULL,
    version_id          uuid NOT NULL,
    imported_membership uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.namespace_import_v (
    project_id         uuid NOT NULL,
    version_id         uuid NOT NULL,
    imported_namespace uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

-- Specialization and its 8 refinements. Each carries its own pair of endpoint references, which
-- SUBSET Relationship::source/target but are stored independently (isDerived=false in the model).
CREATE TABLE sysml2.specialization_v (
    project_id uuid NOT NULL,
    version_id uuid NOT NULL,
    general    uuid NOT NULL REFERENCES sysml2.data_identity (id),
    specific   uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

-- The specialization graph is walked by Type::allSupertypes / inheritedMembership / feature.
-- These two indexes are what make the derived-property impact analysis (§10) affordable.
CREATE INDEX ix_specialization_v_general  ON sysml2.specialization_v (project_id, general);
CREATE INDEX ix_specialization_v_specific ON sysml2.specialization_v (project_id, specific);

CREATE TABLE sysml2.subclassification_v (
    project_id      uuid NOT NULL,
    version_id      uuid NOT NULL,
    subclassifier   uuid NOT NULL REFERENCES sysml2.data_identity (id),
    superclassifier uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.subsetting_v (
    project_id         uuid NOT NULL,
    version_id         uuid NOT NULL,
    subsetted_feature  uuid NOT NULL REFERENCES sysml2.data_identity (id),
    subsetting_feature uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.redefinition_v (
    project_id          uuid NOT NULL,
    version_id          uuid NOT NULL,
    redefined_feature   uuid NOT NULL REFERENCES sysml2.data_identity (id),
    redefining_feature  uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.reference_subsetting_v (
    project_id         uuid NOT NULL,
    version_id         uuid NOT NULL,
    referenced_feature uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.cross_subsetting_v (
    project_id      uuid NOT NULL,
    version_id      uuid NOT NULL,
    crossed_feature uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.feature_typing_v (
    project_id    uuid NOT NULL,
    version_id    uuid NOT NULL,
    type          uuid NOT NULL REFERENCES sysml2.data_identity (id),
    typed_feature uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE INDEX ix_feature_typing_v_type ON sysml2.feature_typing_v (project_id, type);

CREATE TABLE sysml2.conjugated_port_typing_v (
    project_id               uuid NOT NULL,
    version_id               uuid NOT NULL,
    conjugated_port_definition uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.conjugation_v (
    project_id      uuid NOT NULL,
    version_id      uuid NOT NULL,
    conjugated_type uuid NOT NULL REFERENCES sysml2.data_identity (id),
    original_type   uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.port_conjugation_v (
    project_id               uuid NOT NULL,
    version_id               uuid NOT NULL,
    original_port_definition uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.disjoining_v (
    project_id      uuid NOT NULL,
    version_id      uuid NOT NULL,
    disjoining_type uuid NOT NULL REFERENCES sysml2.data_identity (id),
    type_disjoined  uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.differencing_v (
    project_id        uuid NOT NULL,
    version_id        uuid NOT NULL,
    differencing_type uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.intersecting_v (
    project_id         uuid NOT NULL,
    version_id         uuid NOT NULL,
    intersecting_type  uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.unioning_v (
    project_id    uuid NOT NULL,
    version_id    uuid NOT NULL,
    unioning_type uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.feature_chaining_v (
    project_id       uuid NOT NULL,
    version_id       uuid NOT NULL,
    chaining_feature uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.feature_inverting_v (
    project_id         uuid NOT NULL,
    version_id         uuid NOT NULL,
    feature_inverted   uuid NOT NULL REFERENCES sysml2.data_identity (id),
    inverting_feature  uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.type_featuring_v (
    project_id      uuid NOT NULL,
    version_id      uuid NOT NULL,
    feature_of_type uuid NOT NULL REFERENCES sysml2.data_identity (id),
    featuring_type  uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.feature_value_v (
    project_id uuid    NOT NULL,
    version_id uuid    NOT NULL,
    is_default boolean NOT NULL,
    is_initial boolean NOT NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.annotation_v (
    project_id        uuid NOT NULL,
    version_id        uuid NOT NULL,
    annotated_element uuid NOT NULL REFERENCES sysml2.data_identity (id),
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE INDEX ix_annotation_v_annotated_element
    ON sysml2.annotation_v (project_id, annotated_element);

CREATE TABLE sysml2.comment_v (
    project_id uuid NOT NULL,
    version_id uuid NOT NULL,
    body       text NOT NULL,
    locale     text NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.textual_representation_v (
    project_id uuid NOT NULL,
    version_id uuid NOT NULL,
    body       text NOT NULL,
    language   text NOT NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.library_package_v (
    project_id  uuid    NOT NULL,
    version_id  uuid    NOT NULL,
    is_standard boolean NOT NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

-- Expressions. Note that `operator` is declared ONCE on OperatorExpression; CollectExpression,
-- SelectExpression, FeatureChainExpression and IndexExpression merely redeclare it.
CREATE TABLE sysml2.operator_expression_v (
    project_id uuid NOT NULL,
    version_id uuid NOT NULL,
    operator   text NOT NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

-- `value` is a DIFFERENT TYPE on each of the four Literal metaclasses. This is the single
-- clearest reason the schema cannot collapse to one wide table.
CREATE TABLE sysml2.literal_boolean_v (
    project_id uuid    NOT NULL,
    version_id uuid    NOT NULL,
    value      boolean NOT NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.literal_integer_v (
    project_id uuid    NOT NULL,
    version_id uuid    NOT NULL,
    value      integer NOT NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.literal_rational_v (
    project_id uuid             NOT NULL,
    version_id uuid             NOT NULL,
    value      double precision NOT NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.literal_string_v (
    project_id uuid NOT NULL,
    version_id uuid NOT NULL,
    value      text NOT NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.invariant_v (
    project_id uuid    NOT NULL,
    version_id uuid    NOT NULL,
    is_negated boolean NOT NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

-- SysML layer
CREATE TABLE sysml2.definition_v (
    project_id   uuid    NOT NULL,
    version_id   uuid    NOT NULL,
    is_variation boolean NOT NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.usage_v (
    project_id   uuid    NOT NULL,
    version_id   uuid    NOT NULL,
    is_variation boolean NOT NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

-- isIndividual is declared independently on OccurrenceDefinition and OccurrenceUsage — they sit
-- on the two parallel Definition/Usage branches and neither inherits from the other.
CREATE TABLE sysml2.occurrence_definition_v (
    project_id    uuid    NOT NULL,
    version_id    uuid    NOT NULL,
    is_individual boolean NOT NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.occurrence_usage_v (
    project_id    uuid    NOT NULL,
    version_id    uuid    NOT NULL,
    is_individual boolean NOT NULL,
    portion_kind  sysml2.portion_kind NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.state_definition_v (
    project_id  uuid    NOT NULL,
    version_id  uuid    NOT NULL,
    is_parallel boolean NOT NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.state_usage_v (
    project_id  uuid    NOT NULL,
    version_id  uuid    NOT NULL,
    is_parallel boolean NOT NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.requirement_definition_v (
    project_id uuid NOT NULL,
    version_id uuid NOT NULL,
    req_id     text NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.requirement_usage_v (
    project_id uuid NOT NULL,
    version_id uuid NOT NULL,
    req_id     text NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

-- `kind` is a DIFFERENT ENUM TYPE on each of these four. Same argument as Literal::value.
CREATE TABLE sysml2.requirement_constraint_membership_v (
    project_id uuid NOT NULL,
    version_id uuid NOT NULL,
    kind       sysml2.requirement_constraint_kind NOT NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.state_subaction_membership_v (
    project_id uuid NOT NULL,
    version_id uuid NOT NULL,
    kind       sysml2.state_subaction_kind NOT NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.transition_feature_membership_v (
    project_id uuid NOT NULL,
    version_id uuid NOT NULL,
    kind       sysml2.transition_feature_kind NOT NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

CREATE TABLE sysml2.trigger_invocation_expression_v (
    project_id uuid NOT NULL,
    version_id uuid NOT NULL,
    kind       sysml2.trigger_kind NOT NULL,
    PRIMARY KEY (project_id, version_id),
    FOREIGN KEY (project_id, version_id)
        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE
) PARTITION BY HASH (project_id);

------------------------------------------------------------------------------------------------
-- 8. DERIVED ELEMENT STATE — the second append-only stream                       [HAND-WRITTEN]
--
-- THE KEY INSIGHT OF THIS SCHEMA.
--
-- A derived value is a function of (version, SNAPSHOT), not of the version alone. Rename a
-- Namespace and every descendant's qualifiedName changes, even though no descendant's version
-- row changed. So derived state CANNOT hang off element_version — it is keyed by
-- (identity, commit), exactly like a version, and resolves through the SAME fold.
--
-- A row is written only for elements whose derived values ACTUALLY CHANGED at that commit —
-- the "impact radius" of the change set. A leaf edit writes one row. A Namespace rename writes
-- one row per descendant. Adding a supertype to a widely-specialized Type writes one row per
-- member of its specialization-descendant closure. This is inherent, not a flaw in the design;
-- OMG Clause 2 warns of exactly this: "the values of derived properties of a given Element may
-- be affected by commits that do not directly change that Element."
--
-- The six hot derived properties are promoted to real columns so the Query service can filter
-- and ORDER BY them (Clause 2, Derived Property Full Conformance: derived properties "can be
-- used in Query structures as PrimitiveConstraint properties"). The remaining ~325 distinct
-- derived property names live in derived_json.
------------------------------------------------------------------------------------------------

CREATE TABLE sysml2.derived_version (
    project_id         uuid    NOT NULL,
    derived_id         uuid    NOT NULL,
    identity_id        uuid    NOT NULL REFERENCES sysml2.data_identity (id),
    commit_id          uuid    NOT NULL REFERENCES sysml2.commit (id),

    -- promoted hot derived properties (all 167 metaclasses declare these via Element)
    owner              uuid    NULL REFERENCES sysml2.data_identity (id),
    owning_namespace   uuid    NULL REFERENCES sysml2.data_identity (id),
    qualified_name     text    NULL,
    name               text    NULL,
    short_name         text    NULL,
    is_library_element boolean NOT NULL DEFAULT false,

    -- everything else: ~325 distinct derived property names
    derived_json       jsonb   NOT NULL,

    PRIMARY KEY (project_id, derived_id)
) PARTITION BY HASH (project_id);

ALTER TABLE sysml2.derived_version ALTER COLUMN derived_json SET COMPRESSION lz4;

CREATE UNIQUE INDEX ux_derived_version_identity_commit
    ON sysml2.derived_version (project_id, identity_id, commit_id);

CREATE INDEX ix_derived_version_commit         ON sysml2.derived_version (project_id, commit_id);
CREATE INDEX ix_derived_version_owner          ON sysml2.derived_version (project_id, owner);
CREATE INDEX ix_derived_version_qualified_name ON sysml2.derived_version (project_id, qualified_name);

-- Ad-hoc PrimitiveConstraint filtering on a derived property that did NOT get promoted to a
-- column falls back to a jsonb containment probe. GIN keeps that from being a sequential scan.
CREATE INDEX ix_derived_version_json ON sysml2.derived_version USING gin (derived_json jsonb_path_ops);

------------------------------------------------------------------------------------------------
-- 9. SNAPSHOT RESOLUTION                                                         [HAND-WRITTEN]
--
-- Commit.versionedData is DERIVED: fold `change` over the transitive previousCommit closure
-- (Clause 7.1.2). Doing that fold on every read does not scale, so:
--
--   * branch_head is a sparse OVERLAY: it stores ONLY the identities that DIVERGE from the
--     branch's base checkpoint (branch.base_commit_id, §3). The head state of a branch is
--     overlay-row-if-present, else checkpoint-row. Updated incrementally on commit with the
--     change-set rows: O(changeset). Branch creation from a checkpointed commit writes ZERO
--     rows; branch deletion deletes only the divergence. At the design scale (100-500 live
--     branches x 1M elements) a fully-materialized per-branch head would be ~500M rows per
--     project — the overlay is what makes hundreds of branches affordable.
--     A deletion on the branch is masked by an overlay row with is_tombstone = true (it also
--     points at the tombstone element_version row). base_commit_id NULL = the overlay IS the
--     complete head state (bootstrap / small-project mode).
--     COMPACTION (service policy): when an overlay exceeds ~10% of the model or ~100k rows,
--     materialize a checkpoint at the branch head, repoint base_commit_id, delete the overlay.
--
--   * commit_checkpoint materializes the full fold for selected commits. It serves two masters:
--     the base of every branch overlay, and the bound on how far resolve_commit_state() walks.
--     CADENCE (service policy): checkpoint a commit when EITHER ~200 commits have passed since
--     the nearest checkpointed ancestor on that lineage OR the cumulative change-set size since
--     it exceeds ~25% of the model — plus always at branch-fork bases. Checkpoints are O(model)
--     rows each, so cadence must be churn-based, never "every N commits" alone. Retention: drop
--     checkpoints referenced by no branch.base_commit_id (delete the registry row FIRST, then
--     the rows), keeping a sparse historical ladder.
--
--   * resolve_commit_state() is the general fallback for an arbitrary commit;
--     resolve_element_at_commit() is the single-element variant (O(ancestry), not O(model)).
------------------------------------------------------------------------------------------------

CREATE TABLE sysml2.branch_head (
    project_id   uuid    NOT NULL,
    branch_id    uuid    NOT NULL REFERENCES sysml2.branch (id) ON DELETE CASCADE,
    identity_id  uuid    NOT NULL REFERENCES sysml2.data_identity (id),
    version_id   uuid    NOT NULL,
    derived_id   uuid    NULL,

    -- true = the element is DELETED on this branch relative to the base checkpoint; the row
    -- masks the checkpoint row on read. Denormalizes element_version.tombstone so the set-read
    -- anti-join never has to visit element_version for masked identities.
    is_tombstone boolean NOT NULL DEFAULT false,

    PRIMARY KEY (project_id, branch_id, identity_id)
) PARTITION BY HASH (project_id);

-- Supports the ON DELETE CASCADE from branch: a cascade filters on branch_id ALONE, and the PK
-- leads with project_id — without this index every branch deletion would sequentially scan all
-- partitions. (Cheap to maintain now that the table only holds divergence.)
CREATE INDEX ix_branch_head_branch ON sysml2.branch_head (branch_id);

-- One row per CHECKPOINT (not per identity): the resolvers' "is this commit checkpointed?"
-- probe hits this tiny table, never commit_checkpoint itself. Probing commit_checkpoint for
-- existence is a planner trap: all of a checkpoint's ~1M rows share one (project_id, commit_id)
-- value, so n_distinct estimates make the index look useless and the EXISTS degenerates into a
-- repeated sequential scan of the whole partition (measured: 500 probes = 100M rows filtered).
CREATE TABLE sysml2.commit_checkpoint_registry (
    project_id uuid        NOT NULL,
    commit_id  uuid        NOT NULL REFERENCES sysml2.commit (id),
    created    timestamptz NOT NULL DEFAULT now(),
    PRIMARY KEY (project_id, commit_id)
);

CREATE TABLE sysml2.commit_checkpoint (
    project_id  uuid NOT NULL,
    commit_id   uuid NOT NULL REFERENCES sysml2.commit (id) ON DELETE CASCADE,
    identity_id uuid NOT NULL REFERENCES sysml2.data_identity (id),
    version_id  uuid NOT NULL,
    derived_id  uuid NULL,
    PRIMARY KEY (project_id, commit_id, identity_id)
) PARTITION BY HASH (project_id);

-- The general resolver. Walks the commit DAG back from :commit_id (stopping at the nearest
-- checkpoint) and picks, for each identity, the version from the NEWEST ancestor commit.
--
-- "Newest" is well-defined only because Clause 7.1.2 guarantees monotonically increasing commit
-- timestamps along every parent edge — which trg_commit_parent_monotonic enforces. For a merge
-- commit, the merge itself carries the conflict resolution in its own change set, so it is
-- correctly the newest and wins.
--
-- SIBLING commits (parallel branches, later merged) are NOT ordered by the monotonicity
-- invariant and may share a timestamp; the `id DESC` tiebreaker makes the fold DETERMINISTIC
-- in that case (an arbitrary-but-stable winner beats a nondeterministic one). A merge that
-- restates its conflicts — as Clause 7.1.2 requires — never reaches the tiebreaker.
CREATE OR REPLACE FUNCTION sysml2.resolve_commit_state(p_project_id uuid, p_commit_id uuid)
RETURNS TABLE (identity_id uuid, version_id uuid, derived_id uuid)
LANGUAGE sql
STABLE
AS $$
    WITH RECURSIVE
    checkpoint AS (
        SELECT ccr.commit_id
        FROM sysml2.commit_checkpoint_registry ccr
        WHERE ccr.project_id = p_project_id AND ccr.commit_id = p_commit_id
    ),
    ancestry AS (
        SELECT c.id, c.created, (SELECT count(*) > 0 FROM checkpoint) AS at_checkpoint
        FROM sysml2.commit c
        WHERE c.id = p_commit_id

        UNION

        SELECT parent.id, parent.created,
               EXISTS (SELECT 1 FROM sysml2.commit_checkpoint_registry ccr
                       WHERE ccr.project_id = p_project_id AND ccr.commit_id = parent.id)
        FROM ancestry a
        JOIN sysml2.commit_parent cp ON cp.commit_id = a.id
        JOIN sysml2.commit parent    ON parent.id = cp.parent_commit_id
        WHERE NOT a.at_checkpoint
    ),
    folded AS (
        SELECT DISTINCT ON (ev.identity_id)
               ev.identity_id,
               ev.version_id,
               ev.tombstone
        FROM ancestry a
        JOIN sysml2.element_version ev
          ON ev.project_id = p_project_id AND ev.commit_id = a.id
        ORDER BY ev.identity_id, a.created DESC, a.id DESC
    ),
    checkpoint_state AS (
        SELECT cc.identity_id, cc.version_id, cc.derived_id
        FROM sysml2.commit_checkpoint cc
        JOIN ancestry a ON a.id = cc.commit_id AND a.at_checkpoint
        WHERE cc.project_id = p_project_id
    ),
    checkpointed AS (
        SELECT cs.identity_id, cs.version_id
        FROM checkpoint_state cs
        WHERE NOT EXISTS (SELECT 1 FROM folded f WHERE f.identity_id = cs.identity_id)
    ),
    resolved AS (
        SELECT f.identity_id, f.version_id FROM folded f WHERE NOT f.tombstone
        UNION ALL
        SELECT c.identity_id, c.version_id FROM checkpointed c
    ),
    -- Derived state folds over the SAME walked ancestry, in one pass (no per-row probe).
    -- An identity whose latest derived row predates the walked window falls back to the
    -- checkpoint's derived_id — without that fallback, derived state older than the
    -- checkpoint would silently resolve to NULL.
    derived_folded AS (
        SELECT DISTINCT ON (dv.identity_id)
               dv.identity_id,
               dv.derived_id
        FROM ancestry a
        JOIN sysml2.derived_version dv
          ON dv.project_id = p_project_id AND dv.commit_id = a.id
        ORDER BY dv.identity_id, a.created DESC, a.id DESC
    )
    SELECT r.identity_id,
           r.version_id,
           COALESCE(df.derived_id, cs.derived_id) AS derived_id
    FROM resolved r
    LEFT JOIN derived_folded df ON df.identity_id = r.identity_id
    LEFT JOIN checkpoint_state cs ON cs.identity_id = r.identity_id;
$$;

-- Single-element variant of the resolver: same ancestry walk, but the fold arms are filtered to
-- one identity, so the cost is O(walked ancestry) index probes — NOT O(model). This is the
-- backing for GET /projects/{p}/commits/{c}/elements/{e}; without it a one-element historical
-- read would fold the entire model.
CREATE OR REPLACE FUNCTION sysml2.resolve_element_at_commit(p_project_id uuid, p_commit_id uuid, p_identity_id uuid)
RETURNS TABLE (identity_id uuid, version_id uuid, derived_id uuid)
LANGUAGE sql
STABLE
AS $$
    WITH RECURSIVE
    checkpoint AS (
        SELECT ccr.commit_id
        FROM sysml2.commit_checkpoint_registry ccr
        WHERE ccr.project_id = p_project_id AND ccr.commit_id = p_commit_id
    ),
    ancestry AS (
        SELECT c.id, c.created, (SELECT count(*) > 0 FROM checkpoint) AS at_checkpoint
        FROM sysml2.commit c
        WHERE c.id = p_commit_id

        UNION

        SELECT parent.id, parent.created,
               EXISTS (SELECT 1 FROM sysml2.commit_checkpoint_registry ccr
                       WHERE ccr.project_id = p_project_id AND ccr.commit_id = parent.id)
        FROM ancestry a
        JOIN sysml2.commit_parent cp ON cp.commit_id = a.id
        JOIN sysml2.commit parent    ON parent.id = cp.parent_commit_id
        WHERE NOT a.at_checkpoint
    ),
    folded AS (
        SELECT ev.identity_id, ev.version_id, ev.tombstone
        FROM ancestry a
        JOIN sysml2.element_version ev
          ON ev.project_id = p_project_id AND ev.commit_id = a.id AND ev.identity_id = p_identity_id
        ORDER BY a.created DESC, a.id DESC
        LIMIT 1
    ),
    checkpointed AS (
        SELECT cc.identity_id, cc.version_id, cc.derived_id
        FROM sysml2.commit_checkpoint cc
        JOIN ancestry a ON a.id = cc.commit_id AND a.at_checkpoint
        WHERE cc.project_id = p_project_id AND cc.identity_id = p_identity_id
    ),
    resolved AS (
        SELECT f.identity_id, f.version_id FROM folded f WHERE NOT f.tombstone
        UNION ALL
        SELECT c.identity_id, c.version_id FROM checkpointed c
        WHERE NOT EXISTS (SELECT 1 FROM folded)
    ),
    derived_folded AS (
        SELECT dv.derived_id
        FROM ancestry a
        JOIN sysml2.derived_version dv
          ON dv.project_id = p_project_id AND dv.commit_id = a.id AND dv.identity_id = p_identity_id
        ORDER BY a.created DESC, a.id DESC
        LIMIT 1
    )
    SELECT r.identity_id,
           r.version_id,
           COALESCE((SELECT df.derived_id FROM derived_folded df),
                    (SELECT c.derived_id FROM checkpointed c)) AS derived_id
    FROM resolved r;
$$;

-- Materializes the full fold of a commit as a checkpoint (idempotent). O(model) by design —
-- run it ASYNCHRONOUSLY per the cadence policy in the §9 banner, never on the commit path.
-- Returns the number of rows written.
CREATE OR REPLACE FUNCTION sysml2.build_commit_checkpoint(p_project_id uuid, p_commit_id uuid)
RETURNS bigint
LANGUAGE sql
VOLATILE
AS $$
    WITH inserted AS (
        INSERT INTO sysml2.commit_checkpoint (project_id, commit_id, identity_id, version_id, derived_id)
        SELECT p_project_id, p_commit_id, r.identity_id, r.version_id, r.derived_id
        FROM sysml2.resolve_commit_state(p_project_id, p_commit_id) r
        ON CONFLICT (project_id, commit_id, identity_id) DO NOTHING
        RETURNING 1
    ),
    -- same statement, so checkpoint rows + registry row become visible atomically; the EXISTS
    -- dependency also skips registering a checkpoint that materialized zero rows
    registered AS (
        INSERT INTO sysml2.commit_checkpoint_registry (project_id, commit_id)
        SELECT p_project_id, p_commit_id
        WHERE EXISTS (SELECT 1 FROM inserted)
        ON CONFLICT (project_id, commit_id) DO NOTHING
    )
    SELECT count(*) FROM inserted;
$$;

------------------------------------------------------------------------------------------------
-- 10. READ PATH                                                                  [HAND-WRITTEN]
--
-- GET /projects/{p}/commits/{c}/elements/{e} reduces to ONE jsonb concat over a handful of PK
-- lookups. No joins across 6 subtype tables, no recursion, no derived computation. That is the
-- entire point of splitting stored_json from derived_json.
--
-- EVERY function here resolves project_id by joining through `branch` FIRST. Filtering a
-- partitioned table on a bare uuid (branch_id / identity_id / version_id) without project_id
-- defeats partition pruning (all 16 leaves visited) AND cannot use the PKs (project_id-leading,
-- and PG16/17 has no btree skip scan) — the hottest query would silently become the worst one.
--
-- The normalized columns of §5-§7 are the system of record and carry the referential integrity;
-- stored_json / derived_json are the read model built from them at commit time.
------------------------------------------------------------------------------------------------

-- Branch-head single-element read over the OVERLAY: the overlay row wins (a tombstoned overlay
-- row masks the base); otherwise fall back to the base checkpoint row.
CREATE OR REPLACE FUNCTION sysml2.get_element_at_branch_head(p_branch_id uuid, p_identity_id uuid)
RETURNS jsonb
LANGUAGE sql
STABLE
AS $$
    SELECT ev.stored_json || COALESCE(dv.derived_json, '{}'::jsonb)
    FROM sysml2.branch b
    LEFT JOIN sysml2.branch_head bh
      ON bh.project_id = b.project_id AND bh.branch_id = b.id AND bh.identity_id = p_identity_id
    LEFT JOIN sysml2.commit_checkpoint cc
      ON cc.project_id = b.project_id AND cc.commit_id = b.base_commit_id
     AND cc.identity_id = p_identity_id AND bh.identity_id IS NULL
    JOIN sysml2.element_version ev
      ON ev.project_id = b.project_id AND ev.version_id = COALESCE(bh.version_id, cc.version_id)
    LEFT JOIN sysml2.derived_version dv
      ON dv.project_id = b.project_id AND dv.derived_id = COALESCE(bh.derived_id, cc.derived_id)
    WHERE b.id = p_branch_id
      AND bh.is_tombstone IS NOT TRUE
      AND NOT ev.tombstone;
$$;

-- Branch-head set read: base checkpoint minus overlaid identities, plus the live overlay.
CREATE OR REPLACE FUNCTION sysml2.get_elements_at_branch_head(p_branch_id uuid)
RETURNS TABLE (identity_id uuid, payload jsonb)
LANGUAGE sql
STABLE
AS $$
    WITH head AS (
        SELECT b.project_id, bh.identity_id, bh.version_id, bh.derived_id
        FROM sysml2.branch b
        JOIN sysml2.branch_head bh
          ON bh.project_id = b.project_id AND bh.branch_id = b.id
        WHERE b.id = p_branch_id
          AND NOT bh.is_tombstone

        UNION ALL

        SELECT b.project_id, cc.identity_id, cc.version_id, cc.derived_id
        FROM sysml2.branch b
        JOIN sysml2.commit_checkpoint cc
          ON cc.project_id = b.project_id AND cc.commit_id = b.base_commit_id
        WHERE b.id = p_branch_id
          AND NOT EXISTS (SELECT 1
                          FROM sysml2.branch_head masked
                          WHERE masked.project_id = cc.project_id
                            AND masked.branch_id = b.id
                            AND masked.identity_id = cc.identity_id)
    )
    SELECT h.identity_id,
           ev.stored_json || COALESCE(dv.derived_json, '{}'::jsonb)
    FROM head h
    JOIN sysml2.element_version ev
      ON ev.project_id = h.project_id AND ev.version_id = h.version_id
    LEFT JOIN sysml2.derived_version dv
      ON dv.project_id = h.project_id AND dv.derived_id = h.derived_id
    WHERE NOT ev.tombstone;
$$;

CREATE OR REPLACE FUNCTION sysml2.get_elements_at_commit(p_project_id uuid, p_commit_id uuid)
RETURNS TABLE (identity_id uuid, payload jsonb)
LANGUAGE sql
STABLE
AS $$
    SELECT r.identity_id,
           ev.stored_json || COALESCE(dv.derived_json, '{}'::jsonb)
    FROM sysml2.resolve_commit_state(p_project_id, p_commit_id) r
    JOIN sysml2.element_version ev
      ON ev.project_id = p_project_id AND ev.version_id = r.version_id
    LEFT JOIN sysml2.derived_version dv
      ON dv.project_id = p_project_id AND dv.derived_id = r.derived_id;
$$;

CREATE OR REPLACE FUNCTION sysml2.get_element_at_commit(p_project_id uuid, p_commit_id uuid, p_identity_id uuid)
RETURNS jsonb
LANGUAGE sql
STABLE
AS $$
    SELECT ev.stored_json || COALESCE(dv.derived_json, '{}'::jsonb)
    FROM sysml2.resolve_element_at_commit(p_project_id, p_commit_id, p_identity_id) r
    JOIN sysml2.element_version ev
      ON ev.project_id = p_project_id AND ev.version_id = r.version_id
    LEFT JOIN sysml2.derived_version dv
      ON dv.project_id = p_project_id AND dv.derived_id = r.derived_id;
$$;

------------------------------------------------------------------------------------------------
-- 11. PER-METACLASS FLATTENING VIEWS                                               [GENERATED]
--
-- One view per concrete metaclass (167), reconstructing the full DTO row shape by LEFT JOINing
-- exactly the subtype tables in that metaclass's supertype closure. These serve the Query service
-- and any consumer that wants columns rather than jsonb.
--
-- Representative excerpt — the template emits all 167:
--
-- CREATE VIEW sysml2.v_part_usage AS
--     SELECT ev.project_id, ev.version_id, ev.identity_id, ev.commit_id,
--            ev.element_id, ev.declared_name, ev.declared_short_name, ev.is_implied_included,
--            ev.owning_relationship,
--            t.is_abstract, t.is_sufficient,
--            f.direction, f.is_composite, f.is_constant, f.is_derived, f.is_end,
--            f.is_ordered, f.is_portion, f.is_unique, f.is_variable,
--            u.is_variation,
--            ou.is_individual, ou.portion_kind
--     FROM sysml2.element_version ev
--     JOIN sysml2.type_v            t  USING (project_id, version_id)
--     JOIN sysml2.feature_v         f  USING (project_id, version_id)
--     JOIN sysml2.usage_v           u  USING (project_id, version_id)
--     JOIN sysml2.occurrence_usage_v ou USING (project_id, version_id)
--     WHERE ev.class_kind = 94 AND NOT ev.tombstone;
--
-- CREATE VIEW sysml2.v_flow_usage AS ...  -- SIX subtype tables: Connector is both a Feature
--                                          -- and a Relationship, so relationship_v joins too.

------------------------------------------------------------------------------------------------
-- 12. PARTITIONS
--
-- Every element-scoped table is PARTITION BY HASH (project_id) with the same modulus, so they are
-- co-located: a join between element_version and a subtype table stays partition-local, and every
-- project-scoped API call prunes to a single partition.
--
-- 16 is a starting point. The generator emits this loop; tune the modulus to the deployment.
--
-- !! OPERATIONAL REQUIREMENT — max_locks_per_transaction !!
--
-- 58 partitioned tables x 16 partitions = 928 leaf partitions, and Postgres CLONES every foreign
-- key onto every leaf: 2,629 FK constraints in total. Any single transaction that touches the
-- whole schema (CREATE SCHEMA, DROP SCHEMA CASCADE, a migration, pg_dump --schema-only) needs a
-- lock per object and will fail on the default max_locks_per_transaction = 64 with:
--
--     ERROR: out of shared memory
--     HINT:  You might need to increase "max_locks_per_transaction".
--
-- Deploy with at least:
--     max_locks_per_transaction = 4096
--
-- This does NOT affect the hot path: a project-scoped query prunes to one partition at plan time
-- and locks a handful of objects. It is a DDL/administration constraint only. Verified against
-- PostgreSQL 17 — the schema fails to install without it.
--
-- If raising the setting is not an option, halve the modulus (16 -> 8) and/or drop partitioning
-- from the 47 subtype tables, keeping it only on element_version, derived_version, branch_head,
-- commit_checkpoint and the 7 link tables. That trades partition-local subtype joins for a far
-- smaller catalog.
------------------------------------------------------------------------------------------------

DO $$
DECLARE
    partitioned_table text;
    partition_index   int;
    partition_count   constant int := 16;
BEGIN
    FOREACH partitioned_table IN ARRAY ARRAY[
        'element_version', 'derived_version', 'branch_head', 'commit_checkpoint',
        'element_owned_relationship', 'element_alias_ids',
        'relationship_owned_related_element', 'relationship_source', 'relationship_target',
        'dependency_client', 'dependency_supplier',
        'relationship_v', 'type_v', 'feature_v', 'membership_v', 'import_v',
        'membership_import_v', 'namespace_import_v', 'specialization_v', 'subclassification_v',
        'subsetting_v', 'redefinition_v', 'reference_subsetting_v', 'cross_subsetting_v',
        'feature_typing_v', 'conjugated_port_typing_v', 'conjugation_v', 'port_conjugation_v',
        'disjoining_v', 'differencing_v', 'intersecting_v', 'unioning_v',
        'feature_chaining_v', 'feature_inverting_v', 'type_featuring_v', 'feature_value_v',
        'annotation_v', 'comment_v', 'textual_representation_v', 'library_package_v',
        'operator_expression_v', 'literal_boolean_v', 'literal_integer_v', 'literal_rational_v',
        'literal_string_v', 'invariant_v',
        'definition_v', 'usage_v', 'occurrence_definition_v', 'occurrence_usage_v',
        'state_definition_v', 'state_usage_v', 'requirement_definition_v', 'requirement_usage_v',
        'requirement_constraint_membership_v', 'state_subaction_membership_v',
        'transition_feature_membership_v', 'trigger_invocation_expression_v'
    ]
    LOOP
        FOR partition_index IN 0 .. partition_count - 1 LOOP
            EXECUTE format(
                'CREATE TABLE sysml2.%I PARTITION OF sysml2.%I FOR VALUES WITH (MODULUS %s, REMAINDER %s)',
                partitioned_table || '_p' || partition_index,
                partitioned_table,
                partition_count,
                partition_index);

            -- Storage parameters cannot be set on a partitioned parent — only on leaf partitions.
            -- The write profiles differ and so must the tuning:
            --
            --   * branch_head is UPSERT-heavy (overlay rows are updated on every commit to the
            --     branch and deleted on compaction/branch-delete): fillfactor 90 leaves HOT
            --     headroom so the per-commit updates don't bloat the PK, and dead-tuple-driven
            --     vacuum applies.
            --
            --   * everything else is APPEND-ONLY: dead-tuple thresholds never fire usefully, so
            --     drive vacuum off INSERT counts (keeps the visibility map current for index-only
            --     scans) and analyze off a threshold sized for bulk commit traffic — absolute-5000
            --     analyze on a 60M-row leaf would sample continuously during imports.
            IF partitioned_table = 'branch_head' THEN

                EXECUTE format(
                    'ALTER TABLE sysml2.%I SET ('
                        || 'fillfactor = 90, '
                        || 'autovacuum_analyze_scale_factor = 0.0, '
                        || 'autovacuum_analyze_threshold = 50000)',
                    partitioned_table || '_p' || partition_index);

            ELSE

                EXECUTE format(
                    'ALTER TABLE sysml2.%I SET ('
                        || 'autovacuum_vacuum_insert_scale_factor = 0.0, '
                        || 'autovacuum_vacuum_insert_threshold = 100000, '
                        || 'autovacuum_analyze_scale_factor = 0.0, '
                        || 'autovacuum_analyze_threshold = 50000)',
                    partitioned_table || '_p' || partition_index);

            END IF;
        END LOOP;
    END LOOP;
END;
$$;

------------------------------------------------------------------------------------------------
-- 13. MODEL VERSION                                                                [GENERATED]
------------------------------------------------------------------------------------------------

CREATE OR REPLACE FUNCTION sysml2.query_model_version()
RETURNS text
LANGUAGE sql
IMMUTABLE
AS $$
    SELECT '{{model-version}}'::text;
$$;