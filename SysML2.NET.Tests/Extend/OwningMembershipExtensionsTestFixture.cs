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
        public void ComputeOwnedMemberElementId_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IOwningMembership)null).ComputeOwnedMemberElementId(), Throws.TypeOf<NotSupportedException>());
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
    }
}
