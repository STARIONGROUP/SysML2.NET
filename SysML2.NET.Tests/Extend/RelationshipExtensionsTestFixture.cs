// ------------------------------------------------------------------------------------------------
// <copyright file="RelationshipExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Tests.Extend
{
    using System;
    using System.Linq;

    using Moq;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;

    [TestFixture]
    public class RelationshipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeRelatedElement()
        {
            Assert.That(() => ((IRelationship)null).ComputeRelatedElement(), Throws.TypeOf<ArgumentNullException>());

            var relationship = new Mock<IRelationship>();
            relationship.Setup(x => x.Source).Returns([]);
            relationship.Setup(x => x.Target).Returns([]);

            Assert.That(relationship.Object.ComputeRelatedElement(), Is.Empty);

            var sourceDefinition = new Definition();
            relationship.Setup(x => x.Source).Returns([sourceDefinition]);

            Assert.That(relationship.Object.ComputeRelatedElement(), Is.EquivalentTo([sourceDefinition]));

            var targetDefinition = new Definition();
            relationship.Setup(x => x.Target).Returns([targetDefinition]);

            Assert.That(relationship.Object.ComputeRelatedElement(), Is.EquivalentTo([sourceDefinition, targetDefinition]));

            var secondSource = new Definition();
            relationship.Setup(x => x.Source).Returns([sourceDefinition, secondSource]);
            Assert.That(relationship.Object.ComputeRelatedElement(), Is.EquivalentTo([sourceDefinition, secondSource, targetDefinition]));

            // OCL union semantics on OrderedSet: an element shared between Source and Target must not be duplicated.
            var sharedElement = new Definition();
            relationship.Setup(x => x.Source).Returns([sourceDefinition, sharedElement]);
            relationship.Setup(x => x.Target).Returns([sharedElement]);

            var dedupedResult = relationship.Object.ComputeRelatedElement();

            Assert.That(dedupedResult.Count(element => ReferenceEquals(element, sharedElement)), Is.EqualTo(1));
            Assert.That(dedupedResult, Has.Count.EqualTo(2));
            Assert.That(dedupedResult[0], Is.SameAs(sourceDefinition));
            Assert.That(dedupedResult[1], Is.SameAs(sharedElement));
        }

        [Test]
        public void VerifyComputeRedefinedLibraryNamespaceOperation()
        {
            Assert.That(() => ((IRelationship)null).ComputeRedefinedLibraryNamespaceOperation(), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void VerifyComputeRedefinedPathOperation()
        {
            Assert.That(() => ((IRelationship)null).ComputeRedefinedPathOperation(), Throws.TypeOf<ArgumentNullException>());

            var membership = new OwningMembership()
            {
                DeclaredName = "name"
            };

            Assert.That(((IRelationship)membership).ComputeRedefinedPathOperation(), Is.Empty);
        }
    }
}
