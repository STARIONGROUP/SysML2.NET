// -------------------------------------------------------------------------------------------------
// <copyright file="ElementExtensionsTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Tests.Extensions
{
    using System;
    using Core.POCO.Core.Features;
    using Core.POCO.Core.Types;
    using Core.POCO.Systems.Parts;
    using NET.Extensions;
    using NUnit.Framework;

    [TestFixture]
    public class ElementExtensionsTestFixture
    {
        private PartDefinition source;
        private FeatureMembership bridgeRelationship;
        private Feature target;

        [SetUp]
        public void SetUp()
        {
            source = new PartDefinition();
            bridgeRelationship = new FeatureMembership();
            target = new Feature();
        }

        [Test]
        public void AssignOwnership_WithNullSource_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ElementExtensions.AssignOwnership(null, bridgeRelationship, target));
        }

        [Test]
        public void AssignOwnership_WithNullBridge_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ElementExtensions.AssignOwnership(source, null, target));
        }

        [Test]
        public void AssignOwnership_WithNullTarget_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ElementExtensions.AssignOwnership(source, bridgeRelationship, null));
        }

        [Test]
        public void AssignOwnership_WithSourceEqualsTarget_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => ElementExtensions.AssignOwnership(source, bridgeRelationship, source));
        }

        [Test]
        public void AssignOwnership_WithBridgeEqualsTarget_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => ElementExtensions.AssignOwnership(source, bridgeRelationship, bridgeRelationship));
        }

        [Test]
        public void AssignOwnership_WithValidParameters_AssignsOwnershipCorrectly()
        {
            ElementExtensions.AssignOwnership(source, bridgeRelationship, target);

            Assert.That(bridgeRelationship.OwningRelatedElement, Is.EqualTo(source));
            Assert.That(source.OwnedRelationship, Does.Contain(bridgeRelationship));
            Assert.That(bridgeRelationship.OwnedRelatedElement, Does.Contain(target));
        }
    }
}
