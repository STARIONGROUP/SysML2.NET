// -------------------------------------------------------------------------------------------------
// <copyright file="AnnotatingElementExtensionsTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Dal.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    using SysML2.NET.Dal;
    using SysML2.NET.Extensions;

    /// <summary>
    /// Suite of tests for the <see cref="AnnotatingElementExtensions"/> class
    /// </summary>
    [TestFixture]
    public class AnnotatingElementExtensionsTestFixture
    {
        [Test]
        public void Verify_that_ToDto_works_as_expected()
        {
            var annotation = new Annotation
            {
                Id = Guid.NewGuid()
            };

            var ownedMemberShip = new Membership
            {
                Id = Guid.NewGuid()
            };

            var owningMemberShip = new Membership
            {
                Id = Guid.NewGuid()
            };

            var poco = new AnnotatingElement
            {
                Id = Guid.NewGuid(),
                AliasIds = new List<string> { "alias_1", "alias_2" },
                DeclaredName = "declared name",
                DeclaredShortName = "declared shortname",
                ElementId = "element id",
                IsImpliedIncluded = true
            };

            ((IContainedElement)poco).OwnedRelationship.Add(ownedMemberShip);
            ((IContainedElement)poco).OwningRelationship = owningMemberShip;
            
            var dto = poco.ToDto();

            Assert.That(dto.Id, Is.EqualTo(poco.Id));
            Assert.That(dto.AliasIds, Is.EquivalentTo(poco.AliasIds));
            Assert.That(dto.DeclaredName, Is.EqualTo(poco.DeclaredName));
            Assert.That(dto.DeclaredShortName, Is.EqualTo(poco.DeclaredShortName));
            Assert.That(dto.ElementId, Is.EqualTo(poco.ElementId));
            Assert.That(dto.IsImpliedIncluded, Is.EqualTo(poco.IsImpliedIncluded));
            Assert.That(dto.OwnedRelationship.Single(), Is.EqualTo(poco.OwnedRelationship.Single().Id));
            Assert.That(dto.OwningRelationship, Is.EqualTo(poco.OwningRelationship.Id));
        }
    }
}
