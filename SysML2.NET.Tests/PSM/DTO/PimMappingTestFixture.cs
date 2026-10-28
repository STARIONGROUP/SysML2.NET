// ------------------------------------------------------------------------------------------------
// <copyright file="PimMappingTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Tests.PSM.DTO
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using NUnit.Framework;

    using SysML2.NET.PIM;
    using SysML2.NET.PSM.DTO;

    [TestFixture]
    public class PimMappingTestFixture
    {
        private static readonly string[] TwoAliases = ["the first alias", "the second alias"];

        private static readonly DateTime Timestamp = new(1976, 8, 20, 0, 0, 0, DateTimeKind.Utc);

        [Test]
        public void VerifyMap()
        {
            Assert.That(() => PimMapper.MapAll<Project, PIM.DTO.Project>(null), Throws.TypeOf<ArgumentNullException>());
            Assert.That(PimMapper.Map<Project, PIM.DTO.Project>(null), Is.Null);

            var record = new PIM.DTO.Project { Id = Guid.NewGuid(), Name = "the project" };
            var mapped = PimMapper.Map<Project, PIM.DTO.Project>(record);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(mapped, Is.Not.Null);
                Assert.That(mapped.Id, Is.EqualTo(record.Id));
                Assert.That(PimMapper.MapAll<Project, PIM.DTO.Project>([record, record]), Has.Count.EqualTo(2));
                Assert.That(PimMapper.MapAll<Project, PIM.DTO.Project>([]), Has.Count.EqualTo(0));
            }
        }

        [Test]
        public void VerifyProjectRoundTrip()
        {
            Assert.That(() => new Project().FromPim(null), Throws.TypeOf<ArgumentNullException>());

            var record = new PIM.DTO.Project
            {
                Id = Guid.NewGuid(),
                Alias = [.. TwoAliases],
                Created = Timestamp,
                DefaultBranch = Guid.NewGuid(),
                Description = "the description",
                Name = "the project",
                Commits = [Guid.NewGuid()],
                Queries = [Guid.NewGuid()]
            };

            var result = RoundTrip<Project, PIM.DTO.Project>(record);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Id, Is.EqualTo(record.Id));
                Assert.That(result.Alias, Is.EqualTo(TwoAliases));
                Assert.That(result.Created, Is.EqualTo(record.Created));
                Assert.That(result.DefaultBranch, Is.EqualTo(record.DefaultBranch));
                Assert.That(result.Description, Is.EqualTo(record.Description));
                Assert.That(result.Name, Is.EqualTo(record.Name));
                Assert.That(result.Commits, Has.Count.EqualTo(0));
                Assert.That(result.Queries, Has.Count.EqualTo(0));
            }

            var request = RoundTrip<ProjectRequest, PIM.DTO.Project>(record);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(request.Name, Is.EqualTo(record.Name));
                Assert.That(request.DefaultBranch, Is.EqualTo(record.DefaultBranch));
                Assert.That(request.Id, Is.EqualTo(Guid.Empty));
            }
        }

        [Test]
        public void VerifyCommitRoundTrip()
        {
            Assert.That(() => new Commit().FromPim(null), Throws.TypeOf<ArgumentNullException>());

            var record = new PIM.DTO.Commit
            {
                Id = Guid.NewGuid(),
                Alias = [.. TwoAliases],
                Created = Timestamp,
                Description = "the description",
                Name = "the commit",
                OwningProject = Guid.NewGuid(),
                Change = []
            };

            Assert.That(RoundTrip<Commit, PIM.DTO.Commit>(record).PreviousCommits, Has.Count.EqualTo(0));

            record.PreviousCommits = [Guid.NewGuid(), Guid.NewGuid()];

            var result = RoundTrip<Commit, PIM.DTO.Commit>(record);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Id, Is.EqualTo(record.Id));
                Assert.That(result.Alias, Is.EqualTo(TwoAliases));
                Assert.That(result.Created, Is.EqualTo(record.Created));
                Assert.That(result.OwningProject, Is.EqualTo(record.OwningProject));
                Assert.That(result.PreviousCommits, Is.EqualTo(record.PreviousCommits));
                Assert.That(result.Change, Has.Count.EqualTo(0));
            }
        }

        [Test]
        public void VerifyBranchRoundTrip()
        {
            Assert.That(() => new Branch().FromPim(null), Throws.TypeOf<ArgumentNullException>());

            var record = new PIM.DTO.Branch
            {
                Id = Guid.NewGuid(),
                Alias = [.. TwoAliases],
                Created = Timestamp,
                Deleted = Timestamp,
                Description = "the description",
                Head = Guid.NewGuid(),
                Name = "the branch",
                OwningProject = Guid.NewGuid()
            };

            var model = new Branch();
            model.FromPim(record);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(model.Head, Is.EqualTo(record.Head));
                Assert.That(model.ReferencedCommit, Is.EqualTo(record.Head));
            }

            var result = model.ToPim();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Id, Is.EqualTo(record.Id));
                Assert.That(result.Head, Is.EqualTo(record.Head));
                Assert.That(result.Created, Is.EqualTo(record.Created));
                Assert.That(result.Deleted, Is.EqualTo(record.Deleted));
                Assert.That(result.OwningProject, Is.EqualTo(record.OwningProject));
                Assert.That(RoundTrip<BranchRequest, PIM.DTO.Branch>(record).Head, Is.EqualTo(record.Head));
            }
        }

        [Test]
        public void VerifyTagRoundTrip()
        {
            Assert.That(() => new Tag().FromPim(null), Throws.TypeOf<ArgumentNullException>());

            var record = new PIM.DTO.Tag
            {
                Id = Guid.NewGuid(),
                Alias = [.. TwoAliases],
                Created = Timestamp,
                Deleted = Timestamp,
                Description = "the description",
                Name = "the tag",
                OwningProject = Guid.NewGuid(),
                TaggedCommit = Guid.NewGuid()
            };

            var model = new Tag();
            model.FromPim(record);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(model.TaggedCommit, Is.EqualTo(record.TaggedCommit));
                Assert.That(model.ReferencedCommit, Is.EqualTo(record.TaggedCommit));
            }

            var result = model.ToPim();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Id, Is.EqualTo(record.Id));
                Assert.That(result.TaggedCommit, Is.EqualTo(record.TaggedCommit));
                Assert.That(result.OwningProject, Is.EqualTo(record.OwningProject));
                Assert.That(RoundTrip<TagRequest, PIM.DTO.Tag>(record).TaggedCommit, Is.EqualTo(record.TaggedCommit));
            }
        }

        [Test]
        public void VerifyQueryRoundTrip()
        {
            Assert.That(() => new Query().FromPim(null), Throws.TypeOf<ArgumentNullException>());

            var record = new PIM.DTO.Query
            {
                Id = Guid.NewGuid(),
                Alias = [.. TwoAliases],
                Description = "the description",
                Name = "the query",
                Project = Guid.NewGuid(),
                Select = ["name"],
                OrderBy = ["name"],
                Scope = [Guid.NewGuid()]
            };

            Assert.That(RoundTrip<Query, PIM.DTO.Query>(record).Where, Is.Null);

            var leaf = new PIM.DTO.PrimitiveConstraint
            {
                Property = "mass",
                Operator = Operator.lessthanorequalto,
                Inverse = true,
                Value = ["4", null]
            };

            record.Where = leaf;

            var result = RoundTrip<Query, PIM.DTO.Query>(record);
            var mappedLeaf = result.Where as PIM.DTO.PrimitiveConstraint;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Project, Is.EqualTo(record.Project));
                Assert.That(result.Select, Is.EqualTo(["name"]));
                Assert.That(mappedLeaf, Is.Not.Null);
                Assert.That(mappedLeaf.Property, Is.EqualTo("mass"));
                Assert.That(mappedLeaf.Operator, Is.EqualTo(Operator.lessthanorequalto));
                Assert.That(mappedLeaf.Inverse, Is.True);
                Assert.That(mappedLeaf.Value, Is.EqualTo(["4", null]));
                Assert.That(result.OrderBy, Has.Count.EqualTo(0));
                Assert.That(result.Scope, Has.Count.EqualTo(0));
            }

            var requestResult = RoundTrip<QueryRequest, PIM.DTO.Query>(record);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(requestResult.Where, Is.TypeOf<PIM.DTO.PrimitiveConstraint>());
                Assert.That(requestResult.Project, Is.EqualTo(Guid.Empty));
            }
        }

        [Test]
        public void VerifyCompositeConstraintRoundTrip()
        {
            Assert.That(() => new CompositeConstraint().FromPim(null), Throws.TypeOf<ArgumentNullException>());

            var record = new PIM.DTO.CompositeConstraint { Operator = JoinOperator.OR };

            Assert.That(RoundTrip<CompositeConstraint, PIM.DTO.CompositeConstraint>(record).Constraint, Has.Count.EqualTo(0));

            var leaf = new PIM.DTO.PrimitiveConstraint { Property = "name", Operator = Operator.instanceOf };
            var nested = new PIM.DTO.CompositeConstraint { Operator = JoinOperator.AND };
            record.Constraint = [leaf, nested];

            var result = RoundTrip<CompositeConstraint, PIM.DTO.CompositeConstraint>(record);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Operator, Is.EqualTo(JoinOperator.OR));
                Assert.That(result.Constraint, Has.Count.EqualTo(2));
                Assert.That(result.Constraint[0], Is.TypeOf<PIM.DTO.PrimitiveConstraint>());
                Assert.That(result.Constraint[1], Is.TypeOf<PIM.DTO.CompositeConstraint>());
                Assert.That(((PIM.DTO.CompositeConstraint)result.Constraint[1]).Operator, Is.EqualTo(JoinOperator.AND));
                Assert.That(RoundTrip<CompositeConstraintRequest, PIM.DTO.CompositeConstraint>(record).Operator, Is.EqualTo(JoinOperator.OR));
            }
        }

        [Test]
        public void VerifyPrimitiveConstraintRoundTrip()
        {
            Assert.That(() => new PrimitiveConstraint().FromPim(null), Throws.TypeOf<ArgumentNullException>());

            foreach (var comparisonOperator in Enum.GetValues<Operator>())
            {
                var record = new PIM.DTO.PrimitiveConstraint
                {
                    Property = "mass",
                    Operator = comparisonOperator,
                    Value = ["4"]
                };

                using (Assert.EnterMultipleScope())
                {
                    Assert.That(RoundTrip<PrimitiveConstraint, PIM.DTO.PrimitiveConstraint>(record).Operator, Is.EqualTo(comparisonOperator));
                    Assert.That(RoundTrip<PrimitiveConstraintRequest, PIM.DTO.PrimitiveConstraint>(record).Operator, Is.EqualTo(comparisonOperator));
                }
            }
        }

        [Test]
        public void VerifyDataVersionRoundTrip()
        {
            Assert.That(() => new DataVersion().FromPim(null), Throws.TypeOf<ArgumentNullException>());

            var record = new PIM.DTO.DataVersion
            {
                Id = Guid.NewGuid(),
                Alias = [.. TwoAliases],
                Description = "the description",
                Name = "the version",
                Commit = Guid.NewGuid()
            };

            Assert.That(RoundTrip<DataVersion, PIM.DTO.DataVersion>(record).Identity, Is.Null);

            var identity = new PIM.DTO.DataIdentity { Id = Guid.NewGuid(), Name = "the identity" };
            record.Identity = identity;

            var result = RoundTrip<DataVersion, PIM.DTO.DataVersion>(record);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Id, Is.EqualTo(record.Id));
                Assert.That(result.Identity.Id, Is.EqualTo(identity.Id));
                Assert.That(result.Identity.Name, Is.EqualTo(identity.Name));
                Assert.That(result.Payload, Is.Null);
                Assert.That(result.Commit, Is.EqualTo(Guid.Empty));
            }
        }

        [Test]
        public void VerifyDataDifferenceRoundTrip()
        {
            Assert.That(() => new DataDifference().FromPim(null), Throws.TypeOf<ArgumentNullException>());

            var record = new PIM.DTO.DataDifference();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(RoundTrip<DataDifference, PIM.DTO.DataDifference>(record).BaseData, Is.Null);
                Assert.That(RoundTrip<DataDifference, PIM.DTO.DataDifference>(record).CompareData, Is.Null);
            }

            record.BaseData = Guid.NewGuid();
            record.CompareData = Guid.NewGuid();

            var result = RoundTrip<DataDifference, PIM.DTO.DataDifference>(record);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.BaseData, Is.EqualTo(record.BaseData));
                Assert.That(result.CompareData, Is.EqualTo(record.CompareData));
            }
        }

        [Test]
        public void VerifyExternalDataRoundTrip()
        {
            Assert.That(() => new ExternalData().FromPim(null), Throws.TypeOf<ArgumentNullException>());

            var record = new PIM.DTO.ExternalData { Id = Guid.NewGuid() };

            Assert.That(RoundTrip<ExternalData, PIM.DTO.ExternalData>(record).ResourceIdentifier, Is.Null);

            record.ResourceIdentifier = "https://www.stariongroup.eu/Resource?q=1";

            using (Assert.EnterMultipleScope())
            {
                Assert.That(RoundTrip<ExternalData, PIM.DTO.ExternalData>(record).ResourceIdentifier, Is.EqualTo(record.ResourceIdentifier));
                Assert.That(RoundTrip<ExternalData, PIM.DTO.ExternalData>(record).Id, Is.EqualTo(record.Id));
                Assert.That(RoundTrip<ExternalDataRequest, PIM.DTO.ExternalData>(record).ResourceIdentifier, Is.EqualTo(record.ResourceIdentifier));
            }
        }

        [Test]
        public void VerifyExternalRelationshipRoundTrip()
        {
            Assert.That(() => new ExternalRelationship().FromPim(null), Throws.TypeOf<ArgumentNullException>());

            var record = new PIM.DTO.ExternalRelationship
            {
                Id = Guid.NewGuid(),
                ElementEnd = Guid.NewGuid(),
                ExternalDataEnd = Guid.NewGuid(),
                Language = "OCL",
                Specification = "the specification"
            };

            var result = RoundTrip<ExternalRelationship, PIM.DTO.ExternalRelationship>(record);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Id, Is.EqualTo(record.Id));
                Assert.That(result.ElementEnd, Is.EqualTo(record.ElementEnd));
                Assert.That(result.ExternalDataEnd, Is.EqualTo(record.ExternalDataEnd));
                Assert.That(result.Language, Is.EqualTo(record.Language));
                Assert.That(result.Specification, Is.EqualTo(record.Specification));
                Assert.That(RoundTrip<ExternalRelationshipRequest, PIM.DTO.ExternalRelationship>(record).ElementEnd, Is.EqualTo(record.ElementEnd));
            }
        }

        [Test]
        public void VerifyProjectUsageRoundTrip()
        {
            Assert.That(() => new ProjectUsage().FromPim(null), Throws.TypeOf<ArgumentNullException>());

            var record = new PIM.DTO.ProjectUsage
            {
                Id = Guid.NewGuid(),
                UsedProjectCommit = Guid.NewGuid(),
                UsedProject = Guid.NewGuid()
            };

            var result = RoundTrip<ProjectUsage, PIM.DTO.ProjectUsage>(record);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Id, Is.EqualTo(record.Id));
                Assert.That(result.UsedProjectCommit, Is.EqualTo(record.UsedProjectCommit));
                Assert.That(result.UsedProject, Is.EqualTo(record.UsedProject));
                Assert.That(RoundTrip<ProjectUsageRequest, PIM.DTO.ProjectUsage>(record).UsedProject, Is.EqualTo(record.UsedProject));
            }
        }

        [Test]
        public void VerifyDataIdentityRoundTrip()
        {
            Assert.That(() => new DataIdentity().FromPim(null), Throws.TypeOf<ArgumentNullException>());

            var record = new PIM.DTO.DataIdentity
            {
                Id = Guid.NewGuid(),
                Alias = [.. TwoAliases],
                Description = "the description",
                Name = "the identity"
            };

            var result = RoundTrip<DataIdentity, PIM.DTO.DataIdentity>(record);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Id, Is.EqualTo(record.Id));
                Assert.That(result.Alias, Is.EqualTo(TwoAliases));
                Assert.That(result.Description, Is.EqualTo(record.Description));
                Assert.That(result.Name, Is.EqualTo(record.Name));
                Assert.That(RoundTrip<DataIdentityRequest, PIM.DTO.DataIdentity>(record).Id, Is.EqualTo(record.Id));
            }
        }

        [Test]
        public void VerifyContractCoverage()
        {
            var models = typeof(IResponse).Assembly.GetTypes()
                .Where(type => type.Namespace == "SysML2.NET.PSM.DTO" && type.IsClass && !type.IsAbstract)
                .Where(type => !type.IsDefined(typeof(CompilerGeneratedAttribute), false))
                .ToList();

            var mappable = models
                .Where(type => type.GetInterfaces().Any(contract =>
                    contract.IsGenericType && contract.GetGenericTypeDefinition() == typeof(IPimMappable<>)))
                .Select(type => type.Name)
                .ToList();

            var unmapped = models.Select(type => type.Name).Except(mappable).ToList();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(mappable, Has.Count.EqualTo(23));
                Assert.That(unmapped, Is.EquivalentTo(["Error", "CommitRequest", "DataVersionRequest", "MergeOutcome"]));
                Assert.That(mappable, Does.Contain("PrimitiveConstraint"));
            }
        }

        private static TRecord RoundTrip<TPsm, TRecord>(TRecord record)
            where TPsm : class, IPimMappable<TRecord>, new()
            where TRecord : class
        {
            return PimMapper.Map<TPsm, TRecord>(record).ToPim();
        }
    }
}
