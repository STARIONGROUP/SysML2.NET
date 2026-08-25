// -------------------------------------------------------------------------------------------------
// <copyright file="AssemblerTestFixture.cs" company="Starion Group S.A.">
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
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

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
        public void VerifySynchronize()
        {
            Assert.That(() => this.assembler.Synchronize(null), Throws.TypeOf<ArgumentNullException>());

            Assert.That(() => this.assembler.Synchronize([]), Throws.Nothing);

            Assert.That(this.assembler.Cache, Has.Count.EqualTo(0));

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
                OwnedRelatedElement = new List<Guid> { packageDto.Id },
                OwningRelatedElement =  featureDto.Id ,
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

            Assert.That(featurePoco.Id, Is.EqualTo(featureDto.Id));
            Assert.That(featurePoco.DeclaredName, Is.EqualTo(featureDto.DeclaredName));
            Assert.That(featurePoco.DeclaredShortName, Is.EqualTo(featureDto.DeclaredShortName));

            if (this.assembler.Cache.TryGetValue(membershipDto.Id, out this.lazyPoco))
            {
                membershipPoco = (Core.POCO.Root.Namespaces.Membership)this.lazyPoco.Value;
            }

            Assert.That(membershipPoco.OwningRelatedElement, Is.EqualTo(featurePoco));
            
            Core.POCO.Root.Elements.IRelationship relation = membershipPoco;

            Assert.That(membershipPoco.MemberElement, Is.Null);
            
            relation.Target = [featurePoco, membershipPoco];

            using (Assert.EnterMultipleScope())
            {
                Assert.That(membershipPoco.MemberElement, Is.EqualTo(featurePoco));
                Assert.That(relation.Target, Is.EquivalentTo([featurePoco]));
            }
            
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

        [Test]
        public void Synchronize_WithDuplicateIdentifiers_KeepsTheFirstDto()
        {
            var identifier = Guid.Parse("4a2e6c2e-3d6b-4a1f-9a6f-7f0d1a2b3c4d");

            var packageDto = new Core.DTO.Kernel.Packages.Package
            {
                Id = identifier,
                DeclaredName = "the first package",
                ElementId = identifier.ToString()
            };

            var duplicateDto = new Core.DTO.Kernel.Packages.Package
            {
                Id = identifier,
                DeclaredName = "the duplicate package",
                ElementId = identifier.ToString()
            };

            Assert.That(() => this.assembler.Synchronize([packageDto, duplicateDto]), Throws.Nothing);

            Core.POCO.Kernel.Packages.Package packagePoco = null;

            if (this.assembler.Cache.TryGetValue(identifier, out this.lazyPoco))
            {
                packagePoco = (Core.POCO.Kernel.Packages.Package)this.lazyPoco.Value;
            }

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.assembler.Cache, Has.Count.EqualTo(1));
                Assert.That(packagePoco.DeclaredName, Is.EqualTo("the first package"));
            }
        }

        [Test]
        public void Synchronize_WithGrowingDtoSequence_DoesNotRescanTheSequencePerElement()
        {
            var smallModel = new EnumerationCountingElements(64);
            var largeModel = new EnumerationCountingElements(1024);

            var smallAssembler = new Assembler();
            var largeAssembler = new Assembler();

            smallAssembler.Synchronize(smallModel);
            largeAssembler.Synchronize(largeModel);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(smallAssembler.Cache, Has.Count.EqualTo(smallModel.Count));
                Assert.That(largeAssembler.Cache, Has.Count.EqualTo(largeModel.Count));
                Assert.That(smallModel.EnumerationCount, Is.LessThanOrEqualTo(4));
                Assert.That(largeModel.EnumerationCount, Is.EqualTo(smallModel.EnumerationCount));
            }
        }

        private sealed class EnumerationCountingElements : IReadOnlyList<Core.DTO.Root.Elements.IElement>
        {
            private readonly List<Core.DTO.Root.Elements.IElement> elements;

            public EnumerationCountingElements(int size)
            {
                this.elements = Enumerable.Range(0, size)
                    .Select(index => (Core.DTO.Root.Elements.IElement)new Core.DTO.Kernel.Packages.Package
                    {
                        Id = Guid.NewGuid(),
                        DeclaredName = $"package {index.ToString(CultureInfo.InvariantCulture)}",
                        ElementId = index.ToString(CultureInfo.InvariantCulture)
                    })
                    .ToList();
            }

            public int EnumerationCount { get; private set; }

            public int Count => this.elements.Count;

            public Core.DTO.Root.Elements.IElement this[int index] => this.elements[index];

            public IEnumerator<Core.DTO.Root.Elements.IElement> GetEnumerator()
            {
                this.EnumerationCount++;

                return this.elements.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        }
    }
}
