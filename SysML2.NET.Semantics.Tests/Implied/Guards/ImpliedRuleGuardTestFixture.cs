// -------------------------------------------------------------------------------------------------
// <copyright file="ImpliedRuleGuardTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Semantics.Tests.Implied.Guards
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Associations;
    using SysML2.NET.Core.POCO.Kernel.Classes;
    using SysML2.NET.Core.POCO.Kernel.Connectors;
    using SysML2.NET.Core.POCO.Kernel.DataTypes;
    using SysML2.NET.Core.POCO.Kernel.Structures;
    using SysML2.NET.Core.POCO.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.Connections;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Semantics.Implied;
    using SysML2.NET.Semantics.Implied.Guards;

    [TestFixture]
    public class ImpliedRuleGuardTestFixture
    {
        [Test]
        public void VerifyFeatureDataValueSpecializationGuard()
        {
            var guard = Generated("checkFeatureDataValueSpecialization");

            var typedByDataType = new Feature { Id = Guid.NewGuid() };
            Type(typedByDataType, new DataType { Id = Guid.NewGuid(), DeclaredName = "Real" });

            var typedByClass = new Feature { Id = Guid.NewGuid() };
            Type(typedByClass, new Class { Id = Guid.NewGuid(), DeclaredName = "Widget" });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(guard.ConstraintName, Is.EqualTo("checkFeatureDataValueSpecialization"));
                Assert.That(guard.Applies(typedByDataType), Is.True);
                Assert.That(guard.Applies(typedByClass), Is.False);
                Assert.That(guard.Applies(new Feature { Id = Guid.NewGuid() }), Is.False);
                Assert.That(guard.Applies(new Class { Id = Guid.NewGuid() }), Is.False);
                Assert.That(() => guard.Applies(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyConnectionUsageBinarySpecializationGuard()
        {
            var guard = Generated("checkConnectionUsageBinarySpecialization");

            var binary = new ConnectionUsage { Id = Guid.NewGuid() };
            AddEnds(binary, 2);

            var nary = new ConnectionUsage { Id = Guid.NewGuid() };
            AddEnds(nary, 3);

            var unary = new ConnectionUsage { Id = Guid.NewGuid() };
            AddEnds(unary, 1);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(guard.ConstraintName, Is.EqualTo("checkConnectionUsageBinarySpecialization"));
                Assert.That(guard.Applies(binary), Is.True);

                // The OCL is an EXACT count, so an n-ary connection must decline, not merely a unary one.
                Assert.That(guard.Applies(nary), Is.False);
                Assert.That(guard.Applies(unary), Is.False);
                Assert.That(guard.Applies(new ConnectionUsage { Id = Guid.NewGuid() }), Is.False);
                Assert.That(guard.Applies(new Feature { Id = Guid.NewGuid() }), Is.False);
                Assert.That(() => guard.Applies(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyActionUsageOwnedActionSpecializationGuard()
        {
            var guard = Generated("checkActionUsageOwnedActionSpecialization");

            var ownedByPartUsage = CreateActionUsage(true, new PartUsage { Id = Guid.NewGuid() });
            var ownedByPartDefinition = CreateActionUsage(true, new PartDefinition { Id = Guid.NewGuid() });
            var notComposite = CreateActionUsage(false, new PartUsage { Id = Guid.NewGuid() });
            var ownedByNonPart = CreateActionUsage(true, new ActionUsage { Id = Guid.NewGuid() });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(guard.ConstraintName, Is.EqualTo("checkActionUsageOwnedActionSpecialization"));
                Assert.That(guard.Applies(ownedByPartUsage), Is.True);
                Assert.That(guard.Applies(ownedByPartDefinition), Is.True);

                // Both conjuncts matter: composite alone, or a part owner alone, is not enough.
                Assert.That(guard.Applies(notComposite), Is.False);
                Assert.That(guard.Applies(ownedByNonPart), Is.False);

                // owningType is null when the ActionUsage is not owned by a Type at all.
                Assert.That(guard.Applies(new ActionUsage { Id = Guid.NewGuid(), IsComposite = true }), Is.False);
                Assert.That(guard.Applies(new Feature { Id = Guid.NewGuid() }), Is.False);
                Assert.That(() => guard.Applies(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyFeatureEndSpecializationGuard()
        {
            var guard = new FeatureEndSpecializationGuard();

            var associationEnd = CreateEnd(true, new Association { Id = Guid.NewGuid() });
            var connectorEnd = CreateEnd(true, new Connector { Id = Guid.NewGuid() });
            var notAnEnd = CreateEnd(false, new Association { Id = Guid.NewGuid() });
            var ownedByPlainType = CreateEnd(true, new Class { Id = Guid.NewGuid() });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(guard.ConstraintName, Is.EqualTo("checkFeatureEndSpecialization"));
                Assert.That(guard.Applies(associationEnd), Is.True);
                Assert.That(guard.Applies(connectorEnd), Is.True);

                // Both conjuncts matter: an end owned by a plain Type, or a non-end owned by an Association.
                Assert.That(guard.Applies(notAnEnd), Is.False);
                Assert.That(guard.Applies(ownedByPlainType), Is.False);
                Assert.That(guard.Applies(new Feature { Id = Guid.NewGuid(), IsEnd = true }), Is.False);
                Assert.That(() => guard.Applies(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyConnectorBinaryObjectSpecializationGuard()
        {
            var guard = new ConnectorBinaryObjectSpecializationGuard();

            var binaryStructure = CreateConnector(2, new AssociationStructure { Id = Guid.NewGuid() });
            var binaryPlainAssociation = CreateConnector(2, new Association { Id = Guid.NewGuid() });
            var naryStructure = CreateConnector(3, new AssociationStructure { Id = Guid.NewGuid() });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(guard.ConstraintName, Is.EqualTo("checkConnectorBinaryObjectSpecialization"));
                Assert.That(guard.Applies(binaryStructure), Is.True);

                // A binary Connector typed by a plain Association carries a DIFFERENT library Specialization.
                Assert.That(guard.Applies(binaryPlainAssociation), Is.False);
                Assert.That(guard.Applies(naryStructure), Is.False);
                Assert.That(guard.Applies(new Feature { Id = Guid.NewGuid() }), Is.False);
                Assert.That(() => guard.Applies(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyOccurrenceDefinitionIndividualSpecializationGuard()
        {
            var guard = Generated("checkOccurrenceDefinitionIndividualSpecialization");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(guard.ConstraintName, Is.EqualTo("checkOccurrenceDefinitionIndividualSpecialization"));
                Assert.That(guard.Applies(new OccurrenceDefinition { Id = Guid.NewGuid(), IsIndividual = true }), Is.True);
                Assert.That(guard.Applies(new OccurrenceDefinition { Id = Guid.NewGuid(), IsIndividual = false }), Is.False);
                Assert.That(guard.Applies(new Feature { Id = Guid.NewGuid() }), Is.False);
                Assert.That(() => guard.Applies(null), Throws.TypeOf<ArgumentNullException>());
            }
        }


        [Test]
        public void VerifyConnectorBinarySpecializationGuard()
        {
            var guard = new ConnectorBinarySpecializationGuard();

            var binary = CreateConnector(2, new Association { Id = Guid.NewGuid() });
            var nary = CreateConnector(3, new Association { Id = Guid.NewGuid() });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(guard.ConstraintName, Is.EqualTo("checkConnectorBinarySpecialization"));
                Assert.That(guard.Applies(binary), Is.True);
                Assert.That(guard.Applies(nary), Is.False);
                Assert.That(guard.Applies(new Connector { Id = Guid.NewGuid() }), Is.False);

                // A non-Connector must decline — this also disproves the CA1508 claim that the merged
                // pattern is "always true".
                Assert.That(guard.Applies(new Feature { Id = Guid.NewGuid() }), Is.False);
                Assert.That(() => guard.Applies(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyConnectorObjectSpecializationGuard()
        {
            var guard = new ConnectorObjectSpecializationGuard();

            var structureTyped = CreateConnector(3, new AssociationStructure { Id = Guid.NewGuid() });
            var plainAssociation = CreateConnector(3, new Association { Id = Guid.NewGuid() });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(guard.ConstraintName, Is.EqualTo("checkConnectorObjectSpecialization"));

                // Unlike its binary counterpart the end count is irrelevant here.
                Assert.That(guard.Applies(structureTyped), Is.True);
                Assert.That(guard.Applies(plainAssociation), Is.False);
                Assert.That(guard.Applies(new Connector { Id = Guid.NewGuid() }), Is.False);
                Assert.That(guard.Applies(new Feature { Id = Guid.NewGuid() }), Is.False);
                Assert.That(() => guard.Applies(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyFeaturePortionSpecializationGuard()
        {
            var guard = new FeaturePortionSpecializationGuard();

            var portionOwnedByClass = CreatePortion(true, new Class { Id = Guid.NewGuid() }, typedByClass: true);
            var notAPortion = CreatePortion(false, new Class { Id = Guid.NewGuid() }, typedByClass: true);
            var notClassTyped = CreatePortion(true, new Class { Id = Guid.NewGuid() }, typedByClass: false);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(guard.ConstraintName, Is.EqualTo("checkFeaturePortionSpecialization"));
                Assert.That(guard.Applies(portionOwnedByClass), Is.True);

                // Every conjunct of the constraint is load-bearing.
                Assert.That(guard.Applies(notAPortion), Is.False);
                Assert.That(guard.Applies(notClassTyped), Is.False);
                Assert.That(guard.Applies(new Feature { Id = Guid.NewGuid(), IsPortion = true }), Is.False);
                Assert.That(() => guard.Applies(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        private static Feature CreatePortion(bool isPortion, IElement owner, bool typedByClass)
        {
            var portion = new Feature { Id = Guid.NewGuid(), IsPortion = isPortion };

            if (typedByClass)
            {
                Type(portion, new Class { Id = Guid.NewGuid() });
            }

            var membership = new FeatureMembership { Id = Guid.NewGuid() };
            ((IContainedRelationship)membership).OwnedRelatedElement.Add(portion);
            ((IContainedElement)owner).OwnedRelationship.Add(membership);

            return portion;
        }

        private static IImpliedRuleGuard Generated(string constraintName)
        {
            var guard = GeneratedImpliedRuleGuards.All.SingleOrDefault(candidate => candidate.ConstraintName == constraintName);

            Assert.That(guard, Is.Not.Null, $"'{constraintName}' is expected to be generated from its guard OCL.");

            return guard;
        }

        private static Feature CreateEnd(bool isEnd, IElement owner)
        {
            var end = new Feature { Id = Guid.NewGuid(), IsEnd = isEnd };

            var membership = new FeatureMembership { Id = Guid.NewGuid() };
            ((IContainedRelationship)membership).OwnedRelatedElement.Add(end);
            ((IContainedElement)owner).OwnedRelationship.Add(membership);

            return end;
        }

        private static Connector CreateConnector(int endCount, IElement association)
        {
            var connector = new Connector { Id = Guid.NewGuid() };

            AddEnds(connector, endCount);

            var featureTyping = new FeatureTyping { Id = Guid.NewGuid(), TypedFeature = connector, Type = (IType)association };
            ((IContainedElement)connector).OwnedRelationship.Add(featureTyping);

            return connector;
        }

        private static ActionUsage CreateActionUsage(bool isComposite, IElement owner)
        {
            var actionUsage = new ActionUsage { Id = Guid.NewGuid(), IsComposite = isComposite };

            var membership = new FeatureMembership { Id = Guid.NewGuid() };
            ((IContainedRelationship)membership).OwnedRelatedElement.Add(actionUsage);
            ((IContainedElement)owner).OwnedRelationship.Add(membership);

            return actionUsage;
        }

        private static void Type(IFeature feature, IType type)
        {
            var featureTyping = new FeatureTyping { Id = Guid.NewGuid(), TypedFeature = feature, Type = type };
            ((IContainedElement)feature).OwnedRelationship.Add(featureTyping);
        }

        private static void AddEnds(IElement owner, int count)
        {
            for (var endIndex = 0; endIndex < count; endIndex++)
            {
                var end = new Feature { Id = Guid.NewGuid(), IsEnd = true };
                var membership = new EndFeatureMembership { Id = Guid.NewGuid() };
                ((IContainedRelationship)membership).OwnedRelatedElement.Add(end);
                ((IContainedElement)owner).OwnedRelationship.Add(membership);
            }
        }
    }
}
