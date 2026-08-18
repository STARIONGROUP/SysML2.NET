// -------------------------------------------------------------------------------------------------
// <copyright file="OwnershipTreeLibraryTypeIndexTestFixture.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Tests.Implied
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Semantics.Implied;

    [TestFixture]
    public class OwnershipTreeLibraryTypeIndexTestFixture
    {
        private Package libraryPackage;

        private Classifier occurrence;

        private Feature suboccurrences;

        [SetUp]
        public void SetUp()
        {
            this.libraryPackage = new Package { Id = Guid.NewGuid(), DeclaredName = "Occurrences" };
            this.occurrence = new Classifier { Id = Guid.NewGuid(), DeclaredName = "Occurrence" };
            this.suboccurrences = new Feature { Id = Guid.NewGuid(), DeclaredName = "suboccurrences" };

            Own(this.libraryPackage, this.occurrence);
            Own(this.occurrence, this.suboccurrences);
        }

        [Test]
        public void VerifyBuild()
        {
            var index = OwnershipTreeLibraryTypeIndex.Build([this.libraryPackage]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(index.TryGetType("Occurrences::Occurrence", out var resolvedOccurrence), Is.True);
                Assert.That(resolvedOccurrence, Is.SameAs(this.occurrence));

                Assert.That(index.TryGetType("Occurrences::Occurrence::suboccurrences", out var resolvedFeature), Is.True);
                Assert.That(resolvedFeature, Is.SameAs(this.suboccurrences));

                Assert.That(index.TryGetType("Occurrences::Absent", out var missing), Is.False);
                Assert.That(missing, Is.Null);

                Assert.That(index.TryGetType(null, out _), Is.False);
                Assert.That(index.TryGetType(string.Empty, out _), Is.False);
                Assert.That(index.TryGetType("   ", out _), Is.False);

                // The Package itself is a Namespace, not a Type, so it is walked but not indexed.
                Assert.That(index.TryGetType("Occurrences", out _), Is.False);
            }
        }

        [Test]
        public void VerifyBuildWithNullAndEmptyInput()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => OwnershipTreeLibraryTypeIndex.Build(null), Throws.TypeOf<ArgumentNullException>());
                Assert.That(OwnershipTreeLibraryTypeIndex.Build([]).Count, Is.Zero);
                Assert.That(OwnershipTreeLibraryTypeIndex.Build([null]).Count, Is.Zero);
            }
        }

        [Test]
        public void VerifyBuildWithCyclicOwnership()
        {
            var cyclic = new Package { Id = Guid.NewGuid(), DeclaredName = "Cyclic" };
            var inner = new Classifier { Id = Guid.NewGuid(), DeclaredName = "Inner" };

            Own(cyclic, inner);
            Own(inner, cyclic);

            var index = OwnershipTreeLibraryTypeIndex.Build([cyclic]);

            Assert.That(index.TryGetType("Cyclic::Inner", out var resolved), Is.True);
            Assert.That(resolved, Is.SameAs(inner));
        }

        private static void Own(INamespace owner, IElement ownedElement)
        {
            var membership = new OwningMembership { Id = Guid.NewGuid() };
            ((IContainedRelationship)membership).OwnedRelatedElement.Add(ownedElement);
            ((IContainedElement)owner).OwnedRelationship.Add(membership);
        }
    }
}
