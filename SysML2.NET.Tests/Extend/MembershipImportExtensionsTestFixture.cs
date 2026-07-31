// -------------------------------------------------------------------------------------------------
// <copyright file="MembershipImportExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Tests.Extend
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class MembershipImportExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeRedefinedImportedMembershipsOperation()
        {
            Assert.That(() => ((IMembershipImport)null).ComputeRedefinedImportedMembershipsOperation([]), Throws.TypeOf<ArgumentNullException>());

            // Not recursive: importedElement IS a Namespace but recursion is suppressed -> [importedMembership] only.
            var notRecursiveMemberNamespace = new Namespace();
            var notRecursiveImportedMembership = new Membership { MemberElement = notRecursiveMemberNamespace };
            var notRecursiveSubject = new MembershipImport { ImportedMembership = notRecursiveImportedMembership, IsRecursive = false };

            var notRecursiveResult = notRecursiveSubject.ComputeRedefinedImportedMembershipsOperation([]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(notRecursiveResult, Has.Count.EqualTo(1));
                Assert.That(notRecursiveResult, Does.Contain(notRecursiveImportedMembership));
            }

            // Recursive, but importedElement is NOT a Namespace (a Comment) -> [importedMembership] only.
            var nonNamespaceImportedMembership = new Membership { MemberElement = new Comment() };
            var nonNamespaceSubject = new MembershipImport { ImportedMembership = nonNamespaceImportedMembership, IsRecursive = true };

            var nonNamespaceResult = nonNamespaceSubject.ComputeRedefinedImportedMembershipsOperation([]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(nonNamespaceResult, Has.Count.EqualTo(1));
                Assert.That(nonNamespaceResult, Does.Contain(nonNamespaceImportedMembership));
            }

            // Recursive + importedElement IS a Namespace but that Namespace is in excluded -> [importedMembership] only.
            var excludedNamespace = new Namespace();
            var excludedImportedMembership = new Membership { MemberElement = excludedNamespace };
            var excludedSubject = new MembershipImport { ImportedMembership = excludedImportedMembership, IsRecursive = true };

            var excludedResult = excludedSubject.ComputeRedefinedImportedMembershipsOperation([excludedNamespace]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(excludedResult, Has.Count.EqualTo(1));
                Assert.That(excludedResult, Does.Contain(excludedImportedMembership));
            }

            // Recursive + importedElement IS a Namespace with a public visible membership, not excluded ->
            // importedMembership FIRST, then the imported namespace's visibleMemberships appended.
            var importedNamespace = new Namespace();
            var visibleMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            importedNamespace.AssignOwnership(visibleMembership, new Namespace());

            var recursiveImportedMembership = new Membership { MemberElement = importedNamespace };
            var recursiveSubject = new MembershipImport { ImportedMembership = recursiveImportedMembership, IsRecursive = true, IsImportAll = false };

            var recursiveResult = recursiveSubject.ComputeRedefinedImportedMembershipsOperation([]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(recursiveResult[0], Is.SameAs(recursiveImportedMembership));
                Assert.That(recursiveResult, Does.Contain(visibleMembership));
            }
        }
    }
}
