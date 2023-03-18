// -------------------------------------------------------------------------------------------------
// <copyright file="AnnotatingElementExtensionsTestFixture.cs" company="RHEA System S.A.">
// 
//   Copyright 2022-2023 RHEA System S.A.
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

	using SysML2.NET.Core.POCO;
	using SysML2.NET.Dal;
	
	/// <summary>
	/// Suite of tests for the <see cref="AnnotatingElementExtensions"/> class
	/// </summary>
	[TestFixture]
	public class AnnotatingElementExtensionsTestFixture
	{
		[Test]
		public void Verify_that_ToDto_works_as_expected()
		{
			var annotation = new Core.POCO.Annotation
			{
				Id = Guid.NewGuid()
			};

			var relationship = new Core.POCO.Relationship
			{
				Id = Guid.NewGuid()
			};

			var owningRMembership = new Core.POCO.Relationship
            {
				Id = Guid.NewGuid()
			};

			var poco = new Core.POCO.AnnotatingElement
			{
				Id = Guid.NewGuid(),
				AliasIds = new List<string> { "alias_1", "alias_2" },
				Annotation = new List<Annotation> { annotation },
				Name = "declared name",
				ShortName = "declared shortname",
				ElementId = "element id",
				IsImpliedIncluded = true,
				OwnedRelationship = new List<Relationship>{ relationship },
				OwningRelationship = owningRMembership
            };

			var dto = poco.ToDto();

			Assert.That(dto.Id, Is.EqualTo(poco.Id));
			Assert.That(dto.AliasIds, Is.EquivalentTo(poco.AliasIds));
			Assert.That(dto.Annotation.Single(),  Is.EqualTo(poco.Annotation.Single().Id));
			Assert.That(dto.Name, Is.EqualTo(poco.Name));
			Assert.That(dto.ShortName, Is.EqualTo(poco.ShortName));
			Assert.That(dto.ElementId, Is.EqualTo(poco.ElementId));
			Assert.That(dto.IsImpliedIncluded, Is.EqualTo(poco.IsImpliedIncluded));
			Assert.That(dto.OwnedRelationship.Single(), Is.EqualTo(poco.OwnedRelationship.Single().Id));
			Assert.That(dto.OwningRelationship, Is.EqualTo(poco.OwningRelationship.Id));
		}
	}
}