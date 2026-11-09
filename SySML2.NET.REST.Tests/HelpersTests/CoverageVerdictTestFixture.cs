// ------------------------------------------------------------------------------------------------
// <copyright file="CoverageVerdictTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.HelpersTests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;

    using NUnit.Framework;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO.Core.Features;
    using SysML2.NET.Core.DTO.Kernel.Packages;
    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.Core.Root.Namespaces;

    using SySML2.NET.REST.Tests.Helpers;

    [TestFixture]
    public class CoverageVerdictTestFixture
    {
        private OpenApiDocument openApi;

        [SetUp]
        public void SetUp()
        {
            this.openApi = OpenApiDocument.Load(Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "ptc-25-02-30.json"));
        }

        [Test]
        public void VerifyCoverageVerdict()
        {
            var packageId = Guid.NewGuid();
            var firstCommit = Guid.NewGuid();
            var secondCommit = Guid.NewGuid();

            (Guid CommitId, IData Data)[] packagesOnly =
            [
                (firstCommit, new Package { Id = packageId, DeclaredName = "A" }),
                (secondCommit, new Package { Id = packageId, DeclaredName = "B" }),
                (secondCommit, null)
            ];

            var getProjects = this.Operation("GET /projects");
            var getProject = this.Operation("GET /projects/{projectId}");
            var deleteProject = this.Operation("DELETE /projects/{projectId}");

            EndpointCall[] calls =
            [
                new(getProjects, "GET /projects", HttpStatusCode.OK),
                new(getProject, "GET /projects/p", HttpStatusCode.OK),
                new(getProject, "GET /projects/r", HttpStatusCode.InternalServerError),
                new(deleteProject, "DELETE /projects/r", HttpStatusCode.InternalServerError),
                new(null, "GET /unknown", HttpStatusCode.NotFound),
                new(null, "GET /unknown", HttpStatusCode.NotFound)
            ];

            var verdict = new CoverageVerdict(packagesOnly, calls, this.openApi);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(verdict.MissingMetaclasses, Does.Contain(nameof(OwningMembership)).And.Not.Contain(nameof(Package)));
                Assert.That(verdict.UnsetProperties, Does.Not.Contain("Element.DeclaredName").And.Contain("Element.DeclaredShortName"));
                Assert.That(verdict.UnchangedProperties, Does.Not.Contain("Element.DeclaredName").And.Contain("Element.DeclaredShortName"));
                Assert.That(verdict.UnsetProperties, Does.Contain("Element.ElementId").And.Not.Contain("Expose.Visibility"), "a fixed value is never unset, an unchangeable one may be");
                Assert.That(verdict.UnchangedProperties, Does.Not.Contain("Element.ElementId").And.Not.Contain("Expose.Visibility"));
                Assert.That(CoverageVerdict.FixedProperties.Keys.Concat(CoverageVerdict.UnchangeableProperties.Keys), Is.SubsetOf(DtoMetadata.DeclaredProperties.Select(property => property.Name)), "every exemption names a declared property");
                Assert.That(verdict.MissingLiterals, Does.Contain("VisibilityKind.Private"));
                Assert.That(verdict.MissingLiterals, Has.None.StartsWith("ClassKind."));
                Assert.That(verdict.UncoveredOperations, Has.None.StartsWith("GET /projects ("));
                Assert.That(verdict.UncoveredOperations, Does.Contain("GET /projects/{projectId} (no 4xx)").And.Not.Contain("GET /projects/{projectId} (no 2xx)"));
                Assert.That(verdict.UncoveredOperations, Does.Contain("DELETE /projects/{projectId} (no 2xx)"));
                Assert.That(verdict.UnmatchedRequests, Is.EqualTo((string[])["GET /unknown"]));
            }

            (Guid CommitId, IData Data)[] withMembership =
            [
                .. packagesOnly,
                (secondCommit, new OwningMembership { Id = Guid.NewGuid(), Visibility = VisibilityKind.Private })
            ];

            EndpointCall[] withNotFound = [.. calls, new(getProject, "GET /projects/q", HttpStatusCode.NotFound)];

            var completer = new CoverageVerdict(withMembership, withNotFound, this.openApi);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(completer.MissingMetaclasses, Does.Not.Contain(nameof(OwningMembership)));
                Assert.That(completer.UnsetProperties, Does.Not.Contain("Membership.Visibility"));
                Assert.That(completer.MissingLiterals, Does.Not.Contain("VisibilityKind.Private"));
                Assert.That(completer.UncoveredOperations, Has.None.StartsWith("GET /projects/{projectId} ("));
            }

            (Guid CommitId, IData Data)[] distinctElements =
            [
                (firstCommit, new Package { Id = Guid.NewGuid(), DeclaredName = "X" }),
                (firstCommit, new Package { Id = Guid.NewGuid(), DeclaredName = "Y" }),
                (firstCommit, new OwningMembership { Id = Guid.NewGuid() }),
                (firstCommit, new Feature { Id = Guid.NewGuid(), Direction = null })
            ];

            var distinct = new CoverageVerdict(distinctElements, [], this.openApi);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(distinct.UnchangedProperties, Does.Contain("Element.DeclaredName"));
                Assert.That(distinct.MissingLiterals, Does.Not.Contain("VisibilityKind.Public").And.Contain("VisibilityKind.Private"));
                Assert.That(distinct.MissingLiterals, Does.Contain("FeatureDirectionKind.In").And.Contain("FeatureDirectionKind.Inout").And.Contain("FeatureDirectionKind.Out"));
                Assert.That(distinct.UnsetProperties, Does.Contain("Feature.Direction"));
            }
        }

        private OpenApiOperation Operation(string text) => this.openApi.Operations.Single(operation => operation.ToString() == text);
    }
}
