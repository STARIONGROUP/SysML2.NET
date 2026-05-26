// -------------------------------------------------------------------------------------------------
// <copyright file="ImportExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using Moq;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    using IElement = SysML2.NET.Core.POCO.Root.Elements.IElement;

    [TestFixture]
    public class ImportExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeImportedElement()
        {
            Assert.That(() => ((IImport)null).ComputeImportedElement(), Throws.TypeOf<ArgumentNullException>());

            // Wildcard arm: an IImport that is neither an INamespaceImport nor an IMembershipImport
            // returns null. Use Moq because IImport is abstract and has no concrete non-subtype POCO.
            var abstractImport = new Mock<IImport>().Object;

            Assert.That(abstractImport.ComputeImportedElement(), Is.Null);

            // INamespaceImport arm: returns the ImportedNamespace.
            var importedNamespace = new Namespace();
            var namespaceImport = new NamespaceImport { ImportedNamespace = importedNamespace };

            Assert.That(namespaceImport.ComputeImportedElement(), Is.SameAs(importedNamespace));

            // INamespaceImport with no ImportedNamespace set → null.
            var emptyNamespaceImport = new NamespaceImport();

            Assert.That(emptyNamespaceImport.ComputeImportedElement(), Is.Null);

            // IMembershipImport arm: returns the ImportedMembership.MemberElement.
            var ownerNamespace = new Namespace();
            var memberElement = new Namespace();
            var importedMembership = new OwningMembership();
            ownerNamespace.AssignOwnership(importedMembership, memberElement);

            var membershipImport = new MembershipImport { ImportedMembership = importedMembership };

            Assert.That(membershipImport.ComputeImportedElement(), Is.SameAs(memberElement));

            // IMembershipImport with no ImportedMembership set → null (null-conditional propagates).
            var emptyMembershipImport = new MembershipImport();

            Assert.That(emptyMembershipImport.ComputeImportedElement(), Is.Null);

            // IMembershipImport whose ImportedMembership has a null MemberElement → null.
            // A real OwningMembership with zero OwnedRelatedElement throws IncompleteModelException
            // from ComputeOwnedMemberElement's invariant guard, so use a Moq'd IMembership to
            // produce a null MemberElement directly without violating the model invariant.
            var nullMemberMembership = new Mock<IMembership>();
            nullMemberMembership.SetupGet(membership => membership.MemberElement).Returns((IElement)null);
            var membershipImportNoMember = new MembershipImport { ImportedMembership = nullMemberMembership.Object };

            Assert.That(membershipImportNoMember.ComputeImportedElement(), Is.Null);
        }

        [Test]
        public void VerifyComputeImportOwningNamespace()
        {
            Assert.That(() => ((IImport)null).ComputeImportOwningNamespace(), Throws.TypeOf<ArgumentNullException>());

            // No owner wired → null.
            var orphanImport = new NamespaceImport();

            Assert.That(orphanImport.ComputeImportOwningNamespace(), Is.Null);

            // Owner wired via AssignOwnership → returns the owning namespace.
            var owningNamespace = new Namespace();
            var ownedImport = new NamespaceImport();
            owningNamespace.AssignOwnership(ownedImport);

            Assert.That(ownedImport.ComputeImportOwningNamespace(), Is.SameAs(owningNamespace));
        }

        [Test]
        public void VerifyComputeImportedMembershipsOperation()
        {
            // Import.importedMemberships is abstract in the UML; both concrete subclasses
            // (NamespaceImport, MembershipImport) redefine it, and their POCO partials route
            // IImport.ImportedMemberships(...) directly to ComputeRedefinedImportedMembershipsOperation
            // on the matching subtype. This static extension on the abstract base is therefore
            // unreachable at runtime and is documented as a deliberate NotSupportedException guard.
            Assert.That(
                () => ((IImport)null).ComputeImportedMembershipsOperation([]),
                Throws.TypeOf<NotSupportedException>());

            Assert.That(
                () => new NamespaceImport().ComputeImportedMembershipsOperation([]),
                Throws.TypeOf<NotSupportedException>());

            Assert.That(
                () => new MembershipImport().ComputeImportedMembershipsOperation([]),
                Throws.TypeOf<NotSupportedException>());
        }
    }
}
