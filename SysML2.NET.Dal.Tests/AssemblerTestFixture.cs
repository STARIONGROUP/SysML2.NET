// -------------------------------------------------------------------------------------------------
// <copyright file="AssemblerTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2025 Starion Group S.A.
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
    
    using NUnit.Framework;

	using SysML2.NET.Dal;

	/// <summary>
	/// Suite of tests for the <see cref="Assembler"/> class
	/// </summary>
	[TestFixture]
	public class AssemblerTestFixture
	{
		private Assembler assembler;

        private Lazy<Core.POCO.Root.Elements.IElement> lazyPoco;

        [SetUp]
		public void Setup()
		{
			this.assembler = new Assembler();
		}

        [Test]
        public void Verify_that_synchronize_Works_as_Expected()
        {
            var dtos = new List<Core.DTO.Root.Elements.IElement>();

            var packageDto = new SysML2.NET.Core.DTO.Kernel.Packages.Package
            {
                Id = Guid.Parse("86082bb1-ac56-4080-9b04-2804be48cacb"),
                DeclaredName = "a package",
                ElementId = "86082bb1-ac56-4080-9b04-2804be48cacb",
            };

            var featureDto = new SysML2.NET.Core.DTO.Core.Features.Feature
            {
                Id = Guid.Parse("e1e89f3a-5863-4f7a-b9c5-5779d73630dd"),
                DeclaredName = "some feature",
                DeclaredShortName = "sf",
                ElementId = "e1e89f3a-5863-4f7a-b9c5-5779d73630dd"
            };

            var membershipDto = new Core.DTO.Root.Namespaces.Membership
            {
                Id = Guid.Parse("215054ad-eb1d-45f6-8537-d43a3470e73c"),
                OwnedRelatedElement = new List<Guid> { packageDto.Id, featureDto.Id }
            };
            
            dtos.Add(packageDto);
            dtos.Add(featureDto);
            dtos.Add(membershipDto);

            this.assembler.Synchronize(dtos);

            Core.POCO.Core.Features.Feature featurePoco = null;
            Core.POCO.Root.Namespaces.Membership membershipPoco = null;

            if (this.assembler.Cache.TryGetValue(featureDto.Id, out this.lazyPoco))
            {
                featurePoco = (Core.POCO.Core.Features.Feature)this.lazyPoco.Value;
            }

            using (Assert.EnterMultipleScope())
            {
                Assert.That(featurePoco.Id, Is.EqualTo(featureDto.Id));
                Assert.That(featurePoco.DeclaredName, Is.EqualTo(featureDto.DeclaredName));
                Assert.That(featurePoco.DeclaredShortName, Is.EqualTo(featureDto.DeclaredShortName));
            }

            if (this.assembler.Cache.TryGetValue(membershipDto.Id, out this.lazyPoco))
            {
                membershipPoco = (Core.POCO.Root.Namespaces.Membership)this.lazyPoco.Value;
            }

            Assert.That(membershipPoco.OwnedRelatedElement.Contains(featurePoco), Is.True);

            dtos.Clear();
            
            featureDto.DeclaredName = "some updated feature";

            dtos.Add(featureDto);

            this.assembler.Synchronize(dtos);
            
            if (this.assembler.Cache.TryGetValue(featureDto.Id, out this.lazyPoco))
            {
                featurePoco = (Core.POCO.Core.Features.Feature)this.lazyPoco.Value;
            }
            
            Assert.That(featurePoco.DeclaredName, Is.EqualTo("some updated feature"));
        }
    }
}
