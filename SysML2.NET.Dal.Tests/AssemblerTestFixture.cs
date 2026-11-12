// ------------------------------------------------------------------------------------------------
// <copyright file="AssemblerTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
        public void Synchronize_WithDetachedAndDeletedElements_RemovesOnlyTheDeletedOnes()
        {
            var ownerId = Guid.Parse("1b7c1f14-9a2e-4c8d-8f3b-2c6e0d5a4b91");
            var membershipId = Guid.Parse("2c8d2f25-8b3f-4d9e-9a4c-3d7f1e6b5c02");
            var packageId = Guid.Parse("3d9e3f36-7c4a-4eaf-8b5d-4e802f7c6d13");

            var ownerDto = new Core.DTO.Kernel.Packages.Package
            {
                Id = ownerId,
                DeclaredName = "the owner",
                ElementId = ownerId.ToString(),
                OwnedRelationship = [membershipId]
            };

            var membershipDto = new Core.DTO.Root.Namespaces.OwningMembership
            {
                Id = membershipId,
                OwnedRelatedElement = [packageId],
                OwningRelatedElement = ownerId
            };

            var packageDto = new Core.DTO.Kernel.Packages.Package
            {
                Id = packageId,
                DeclaredName = "the owned package",
                ElementId = packageId.ToString(),
                OwningRelationship = membershipId
            };

            this.assembler.Synchronize([ownerDto, membershipDto, packageDto]);

            Core.POCO.Kernel.Packages.Package ownerPoco = null;

            if (this.assembler.Cache.TryGetValue(ownerId, out this.lazyPoco))
            {
                ownerPoco = (Core.POCO.Kernel.Packages.Package)this.lazyPoco.Value;
            }

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.assembler.Cache, Has.Count.EqualTo(3));
                Assert.That(ownerPoco.OwnedRelationship, Has.Count.EqualTo(1));
            }

            var membershipPoco = (Core.POCO.Root.Namespaces.OwningMembership)this.assembler.Cache[membershipId].Value;
            var packagePoco = this.assembler.Cache[packageId].Value;

            Assert.That(() => this.assembler.Synchronize([], null), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => this.assembler.Synchronize([ownerDto], [ownerId]), Throws.TypeOf<ArgumentException>());

            ownerDto.OwnedRelationship = [];
            membershipDto.OwningRelatedElement = null;

            this.assembler.Synchronize([ownerDto, membershipDto]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(ownerPoco.OwnedRelationship, Has.Count.EqualTo(0));
                Assert.That(membershipPoco.OwningRelatedElement, Is.Null);
                Assert.That(this.assembler.Cache[membershipId].Value, Is.SameAs(membershipPoco));
                Assert.That(membershipPoco.OwnedRelatedElement, Is.EqualTo([packagePoco]));
                Assert.That(this.assembler.Cache, Has.Count.EqualTo(3));
            }

            packageDto.OwningRelationship = null;

            this.assembler.Synchronize([packageDto], [membershipId]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.assembler.Cache.ContainsKey(membershipId), Is.False);
                Assert.That(packagePoco.OwningRelationship, Is.Null);
                Assert.That(this.assembler.Cache[packageId].Value, Is.SameAs(packagePoco));
                Assert.That(this.assembler.Cache, Has.Count.EqualTo(2));
            }

            Assert.That(() => this.assembler.Synchronize([], [membershipId]), Throws.Nothing);

            this.assembler.Synchronize([], [ownerId, packageId]);

            Assert.That(this.assembler.Cache, Has.Count.EqualTo(0));
        }

        [Test]
        public void Synchronize_WithDeletedOwnedElement_DetachesItFromItsSurvivingOwner()
        {
            var ownerDto = new Core.DTO.Kernel.Packages.Package { Id = Guid.NewGuid() };
            var membershipDto = new Core.DTO.Root.Namespaces.OwningMembership { Id = Guid.NewGuid(), OwningRelatedElement = ownerDto.Id };
            var memberDto = new Core.DTO.Kernel.Packages.Package { Id = Guid.NewGuid(), OwningRelationship = membershipDto.Id };
            membershipDto.OwnedRelatedElement = [memberDto.Id];
            ownerDto.OwnedRelationship = [membershipDto.Id];

            this.assembler.Synchronize([ownerDto, membershipDto, memberDto]);

            var ownerPoco = this.assembler.Cache[ownerDto.Id].Value;
            var membershipPoco = (Core.POCO.Root.Namespaces.OwningMembership)this.assembler.Cache[membershipDto.Id].Value;

            this.assembler.Synchronize([], [memberDto.Id]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(membershipPoco.OwnedRelatedElement, Has.Count.EqualTo(0));
                Assert.That(ownerPoco.OwnedRelationship, Is.EqualTo([membershipPoco]));
                Assert.That(this.assembler.Cache, Has.Count.EqualTo(2));
            }

            this.assembler.Synchronize([], [membershipDto.Id]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(ownerPoco.OwnedRelationship, Has.Count.EqualTo(0));
                Assert.That(this.assembler.Cache.ContainsKey(ownerDto.Id), Is.True);
                Assert.That(this.assembler.Cache, Has.Count.EqualTo(1));
            }
        }

        [Test]
        public void Synchronize_WithReorderedAndMovedOwnedRelationships_KeepsThePocoInstances()
        {
            var firstOwnerDto = new Core.DTO.Kernel.Packages.Package { Id = Guid.NewGuid() };
            var secondOwnerDto = new Core.DTO.Kernel.Packages.Package { Id = Guid.NewGuid() };
            var membershipDtos = new List<Core.DTO.Root.Namespaces.OwningMembership>();
            var memberDtos = new List<Core.DTO.Kernel.Packages.Package>();

            for (var memberIndex = 0; memberIndex < 3; memberIndex++)
            {
                var membershipDto = new Core.DTO.Root.Namespaces.OwningMembership { Id = Guid.NewGuid(), OwningRelatedElement = firstOwnerDto.Id };
                var memberDto = new Core.DTO.Kernel.Packages.Package { Id = Guid.NewGuid(), OwningRelationship = membershipDto.Id };
                membershipDto.OwnedRelatedElement = [memberDto.Id];
                membershipDtos.Add(membershipDto);
                memberDtos.Add(memberDto);
            }

            firstOwnerDto.OwnedRelationship = [..membershipDtos.Select(membershipDto => membershipDto.Id)];

            this.assembler.Synchronize([firstOwnerDto, secondOwnerDto, ..membershipDtos, ..memberDtos]);

            var firstOwner = (Core.POCO.Kernel.Packages.Package)this.assembler.Cache[firstOwnerDto.Id].Value;
            var secondOwner = (Core.POCO.Kernel.Packages.Package)this.assembler.Cache[secondOwnerDto.Id].Value;
            var memberships = membershipDtos.Select(membershipDto => this.assembler.Cache[membershipDto.Id].Value).ToList();
            var secondMember = this.assembler.Cache[memberDtos[1].Id].Value;

            Assert.That(firstOwner.OwnedRelationship, Is.EqualTo(memberships));

            firstOwnerDto.OwnedRelationship = [membershipDtos[2].Id, membershipDtos[0].Id, membershipDtos[1].Id];

            this.assembler.Synchronize([firstOwnerDto]);

            Assert.That(firstOwner.OwnedRelationship, Is.EqualTo([memberships[2], memberships[0], memberships[1]]));

            firstOwnerDto.OwnedRelationship = [membershipDtos[2].Id, membershipDtos[0].Id];
            secondOwnerDto.OwnedRelationship = [membershipDtos[1].Id];
            membershipDtos[1].OwningRelatedElement = secondOwnerDto.Id;

            this.assembler.Synchronize([firstOwnerDto, secondOwnerDto, membershipDtos[1]]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(firstOwner.OwnedRelationship, Is.EqualTo([memberships[2], memberships[0]]));
                Assert.That(secondOwner.OwnedRelationship, Is.EqualTo([memberships[1]]));
                Assert.That(this.assembler.Cache[membershipDtos[1].Id].Value, Is.SameAs(memberships[1]));
                Assert.That(this.assembler.Cache[memberDtos[1].Id].Value, Is.SameAs(secondMember));
                Assert.That(secondOwner.ownedMember, Is.EqualTo([secondMember]));
                Assert.That(this.assembler.Cache, Has.Count.EqualTo(8));
            }

            membershipDtos[0].OwningRelatedElement = secondOwnerDto.Id;

            this.assembler.Synchronize([membershipDtos[0]]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(firstOwner.OwnedRelationship, Is.EqualTo([memberships[2]]));
                Assert.That(secondOwner.OwnedRelationship, Is.EqualTo([memberships[1], memberships[0]]));
            }

            secondOwnerDto.OwnedRelationship = [membershipDtos[0].Id];

            this.assembler.Synchronize([secondOwnerDto]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(secondOwner.OwnedRelationship, Is.EqualTo([memberships[0]]));
                Assert.That(((Core.POCO.Root.Elements.IRelationship)memberships[1]).OwningRelatedElement, Is.Null);
                Assert.That(this.assembler.Cache[membershipDtos[1].Id].Value, Is.SameAs(memberships[1]));
                Assert.That(this.assembler.Cache[memberDtos[1].Id].Value, Is.SameAs(secondMember));
                Assert.That(this.assembler.Cache, Has.Count.EqualTo(8));
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
