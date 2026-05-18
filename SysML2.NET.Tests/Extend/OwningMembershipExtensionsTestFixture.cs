// -------------------------------------------------------------------------------------------------
// <copyright file="OwningMembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class OwningMembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwnedMemberElement()
        {
            Assert.That(() => ((IOwningMembership)null).ComputeOwnedMemberElement(), Throws.TypeOf<ArgumentNullException>());

            // OwnedRelatedElement.Count == 0 → IncompleteModelException
            var emptyMembership = new OwningMembership();
            Assert.That(() => emptyMembership.ComputeOwnedMemberElement(), Throws.TypeOf<IncompleteModelException>());

            // OwnedRelatedElement.Count == 1 → returns that element
            var container = new Namespace();
            var singleMembership = new OwningMembership();
            var ownedElement = new Definition { DeclaredName = "SingleElement" };
            container.AssignOwnership(singleMembership, ownedElement);
            Assert.That(singleMembership.ComputeOwnedMemberElement(), Is.SameAs(ownedElement));

            // OwnedRelatedElement.Count > 1 → IncompleteModelException
            var multiMembership = new OwningMembership();
            ((IContainedRelationship)multiMembership).OwnedRelatedElement.Add(new Definition());
            ((IContainedRelationship)multiMembership).OwnedRelatedElement.Add(new Definition());
            Assert.That(() => multiMembership.ComputeOwnedMemberElement(), Throws.TypeOf<IncompleteModelException>());
        }

        [Test]
        public void VerifyComputeOwnedMemberElementId()
        {
            Assert.That(() => ((IOwningMembership)null).ComputeOwnedMemberElementId(), Throws.TypeOf<ArgumentNullException>());

            // OwnedRelatedElement.Count != 1 → IncompleteModelException propagated from ComputeOwnedMemberElement
            var emptyMembership = new OwningMembership();

            Assert.That(() => emptyMembership.ComputeOwnedMemberElementId(), Throws.TypeOf<IncompleteModelException>());

            // Populated: one owned element → returns that element's ElementId
            var container = new Namespace();
            var membership = new OwningMembership();
            var ownedElement = new Definition { ElementId = "test-element-id-42" };
            container.AssignOwnership(membership, ownedElement);

            Assert.That(membership.ComputeOwnedMemberElementId(), Is.EqualTo(ownedElement.ElementId));
        }

        [Test]
        public void ComputeOwnedMemberName_ThrowsArgumentNullException()
        {
            Assert.That(() => ((IOwningMembership)null).ComputeOwnedMemberName(), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void ComputeOwnedMemberShortName_ThrowsArgumentNullException()
        {
            Assert.That(() => ((IOwningMembership)null).ComputeOwnedMemberShortName(), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void VerifyComputeRedefinedPathOperation()
        {
            Assert.That(() => ((IOwningMembership)null).ComputeRedefinedPathOperation(), Throws.TypeOf<ArgumentNullException>());

            // OwnedRelatedElement.Count != 1 → IncompleteModelException propagated from ComputeOwnedMemberElement
            var emptyMembership = new OwningMembership();

            Assert.That(() => emptyMembership.ComputeRedefinedPathOperation(), Throws.TypeOf<IncompleteModelException>());

            // qualifiedName != null → returns qualifiedName + "/owningMembership"
            // Wire: owning package → named child namespace → named member definition
            var rootPackage = new Namespace();
            var childNamespace = new Namespace { DeclaredName = "Pkg" };
            var childMembership = new OwningMembership();
            rootPackage.AssignOwnership(childMembership, childNamespace);

            var member = new Definition { DeclaredName = "Member" };
            var memberMembership = new OwningMembership();
            childNamespace.AssignOwnership(memberMembership, member);

            Assert.That(memberMembership.ComputeRedefinedPathOperation(), Is.EqualTo("Pkg::Member/owningMembership"));

            // qualifiedName == null → delegates to RelationshipExtensions.ComputeRedefinedPathOperation
            // Wire: an OwningMembership with an unnamed owned element (no owning namespace → qualifiedName is null)
            var unnamedContainer = new Namespace();
            var unnamedMembership = new OwningMembership();
            var unnamedElement = new Definition();
            unnamedContainer.AssignOwnership(unnamedMembership, unnamedElement);

            var expectedPath = ((IRelationship)unnamedMembership).ComputeRedefinedPathOperation();

            Assert.That(unnamedMembership.ComputeRedefinedPathOperation(), Is.EqualTo(expectedPath));
        }
    }
}
