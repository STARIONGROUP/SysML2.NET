// -------------------------------------------------------------------------------------------------
// <copyright file="NamespaceImportExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class NamespaceImportExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeRedefinedImportedMembershipsOperation()
        {
            Assert.That(() => ((INamespaceImport)null).ComputeRedefinedImportedMembershipsOperation([]), Throws.TypeOf<ArgumentNullException>());

            // No importedNamespace -> nothing to import.
            Assert.That(new NamespaceImport().ComputeRedefinedImportedMembershipsOperation([]), Is.Empty);

            var importedNamespace = new Namespace { DeclaredName = "imported" };
            var visibleMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            importedNamespace.AssignOwnership(visibleMembership, new Namespace { DeclaredName = "member" });

            var subject = new NamespaceImport { ImportedNamespace = importedNamespace };

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeRedefinedImportedMembershipsOperation([]), Is.EquivalentTo([visibleMembership]));

                // `if excluded->includes(importedNamespace) then Sequence{}` — the OCL's first branch. This
                // is the circularity guard of KerML §8.2.3.5.1 ("an implementation must avoid re-processing
                // a Namespace that has already been visited"), so it is load-bearing: without it a
                // Namespace that imports one of its own ancestors re-enters that ancestor indefinitely.
                Assert.That(subject.ComputeRedefinedImportedMembershipsOperation([importedNamespace]), Is.Empty);

                // An unrelated Namespace in the excluded set must not suppress the import.
                Assert.That(subject.ComputeRedefinedImportedMembershipsOperation([new Namespace()]), Is.EquivalentTo([visibleMembership]));
            }
        }
    }
}
