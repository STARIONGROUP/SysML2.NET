// -------------------------------------------------------------------------------------------------
// <copyright file="ChainSubsettingRuleTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Semantics.Tests.Implied.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Connectors;
    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.States;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Extensions;
    using SysML2.NET.Semantics.Implied;
    using SysML2.NET.Semantics.Implied.Rules;

    [TestFixture]
    public class ChainSubsettingRuleTestFixture
    {
        private const string OutgoingLink = "ControlPerformances::DecisionPerformance::outgoingHBLink";

        private const string IncomingLink = "ControlPerformances::MergePerformance::incomingHBLink";

        private ImpliedRelationshipFactory factory;

        private ILibraryTypeIndex libraryTypeIndex;

        private Dictionary<string, IType> libraryFeatures;

        [SetUp]
        public void SetUp()
        {
            this.factory = new ImpliedRelationshipFactory();

            this.libraryFeatures = new Dictionary<string, IType>
            {
                [OutgoingLink] = new Feature { Id = Guid.NewGuid(), DeclaredName = "outgoingHBLink" },
                [IncomingLink] = new Feature { Id = Guid.NewGuid(), DeclaredName = "incomingHBLink" }
            };

            this.libraryTypeIndex = new StubIndex(this.libraryFeatures);
        }

        [Test]
        public void VerifyCreateImpliedFeatureChain()
        {
            var first = new Feature { Id = Guid.NewGuid() };
            var second = new Feature { Id = Guid.NewGuid() };

            var chain = this.factory.CreateImpliedFeatureChain(first, second);

            using (Assert.EnterMultipleScope())
            {
                // The chain's whole meaning is the ORDER of its chainingFeatures.
                Assert.That(chain.chainingFeature, Is.EqualTo([first, second]));
                Assert.That(chain.ownedFeatureChaining.All(chaining => chaining.IsImplied), Is.True);

                Assert.That(() => this.factory.CreateImpliedFeatureChain(null, second), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => this.factory.CreateImpliedFeatureChain(first, null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyDecisionNodeOutgoingSuccessionSpecializationRule()
        {
            var rule = new DecisionNodeOutgoingSuccessionSpecializationRule(this.libraryTypeIndex, this.factory);

            var decisionNode = new DecisionNode { Id = Guid.NewGuid() };
            var succession = BuildSuccession(decisionNode, new ActionUsage { Id = Guid.NewGuid() });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rule.ConstraintName, Is.EqualTo("checkDecisionNodeOutgoingSuccessionSpecialization"));

                var subsettings = rule.Apply(succession).OfType<ISubsetting>().ToList();
                Assert.That(subsettings, Has.Count.EqualTo(1));

                // The Succession subsets the chain, and the chain is [node, link] IN THAT ORDER.
                Assert.That(subsettings[0].SubsettingFeature, Is.SameAs(succession));
                Assert.That(subsettings[0].SubsettedFeature.chainingFeature,
                    Is.EqualTo([(IFeature)decisionNode, (IFeature)this.libraryFeatures[OutgoingLink]]));

                // A Succession leaving something else is not this constraint's business.
                var plain = BuildSuccession(new ActionUsage { Id = Guid.NewGuid() }, new ActionUsage { Id = Guid.NewGuid() });
                Assert.That(rule.Apply(plain), Is.Empty);

                Assert.That(rule.Apply(new Feature { Id = Guid.NewGuid() }), Is.Empty);
                Assert.That(() => rule.Apply(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyMergeNodeIncomingSuccessionSpecializationRule()
        {
            var rule = new MergeNodeIncomingSuccessionSpecializationRule(this.libraryTypeIndex, this.factory);

            var mergeNode = new MergeNode { Id = Guid.NewGuid() };
            var succession = BuildSuccession(new ActionUsage { Id = Guid.NewGuid() }, mergeNode);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rule.ConstraintName, Is.EqualTo("checkMergeNodeIncomingSuccessionSpecialization"));

                var subsettings = rule.Apply(succession).OfType<ISubsetting>().ToList();
                Assert.That(subsettings, Has.Count.EqualTo(1));
                Assert.That(subsettings[0].SubsettedFeature.chainingFeature,
                    Is.EqualTo([(IFeature)mergeNode, (IFeature)this.libraryFeatures[IncomingLink]]));

                Assert.That(() => rule.Apply(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyFeatureChainExpressionResultSpecializationRule()
        {
            var rule = new FeatureChainExpressionResultSpecializationRule(this.factory);

            // `a.b` — the source is the input parameter, the target is the Feature reached through it, so
            // the chain the result subsets is [source, target].
            var expression = new FeatureChainExpression { Id = Guid.NewGuid() };

            var sourceParameter = new Feature { Id = Guid.NewGuid(), Direction = FeatureDirectionKind.In };
            var targetFeature = new Feature { Id = Guid.NewGuid() };
            Own(sourceParameter, targetFeature, new FeatureMembership { Id = Guid.NewGuid() });
            Own(expression, sourceParameter, new FeatureMembership { Id = Guid.NewGuid() });

            var result = new Feature { Id = Guid.NewGuid() };
            Own(expression, result, new ReturnParameterMembership { Id = Guid.NewGuid() });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rule.ConstraintName, Is.EqualTo("checkFeatureChainExpressionResultSpecialization"));

                var subsettings = rule.Apply(expression).OfType<ISubsetting>().ToList();
                Assert.That(subsettings, Has.Count.EqualTo(1));
                Assert.That(subsettings[0].SubsettingFeature, Is.SameAs(result));
                Assert.That(subsettings[0].SubsettedFeature.chainingFeature,
                    Is.EqualTo([(IFeature)sourceParameter, (IFeature)targetFeature]));

                // No input parameter means no chain to state, so nothing is implied.
                var bare = new FeatureChainExpression { Id = Guid.NewGuid() };
                Own(bare, new Feature { Id = Guid.NewGuid() }, new ReturnParameterMembership { Id = Guid.NewGuid() });
                Assert.That(rule.Apply(bare), Is.Empty);

                Assert.That(rule.Apply(new Feature { Id = Guid.NewGuid() }), Is.Empty);
                Assert.That(() => rule.Apply(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyTransitionUsagePayloadSpecializationRule()
        {
            var rule = new TransitionUsagePayloadSpecializationRule(this.factory);

            var transitionUsage = new TransitionUsage { Id = Guid.NewGuid() };

            // The trigger carries the payload the transition's second input parameter must subset.
            var accepter = new AcceptActionUsage { Id = Guid.NewGuid() };
            var payload = new ReferenceUsage { Id = Guid.NewGuid(), Direction = FeatureDirectionKind.In };
            Own(accepter, payload, new FeatureMembership { Id = Guid.NewGuid() });
            Own(transitionUsage, accepter, new TransitionFeatureMembership { Id = Guid.NewGuid(), Kind = TransitionFeatureKind.Trigger });

            // inputParameter(2) is 1-BASED, so two directed parameters are needed and the SECOND is the one.
            var firstParameter = new Feature { Id = Guid.NewGuid(), Direction = FeatureDirectionKind.In };
            var payloadParameter = new Feature { Id = Guid.NewGuid(), Direction = FeatureDirectionKind.In };
            Own(transitionUsage, firstParameter, new FeatureMembership { Id = Guid.NewGuid() });
            Own(transitionUsage, payloadParameter, new FeatureMembership { Id = Guid.NewGuid() });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rule.ConstraintName, Is.EqualTo("checkTransitionUsagePayloadSpecialization"));

                var subsettings = rule.Apply(transitionUsage).OfType<ISubsetting>().ToList();
                Assert.That(subsettings, Has.Count.EqualTo(1));
                Assert.That(subsettings[0].SubsettingFeature, Is.SameAs(payloadParameter), "the SECOND input parameter, not the first");
                Assert.That(subsettings[0].SubsettedFeature.chainingFeature,
                    Is.EqualTo([(IFeature)accepter, (IFeature)payload]));

                // An untriggered transition has no payload to bind.
                Assert.That(rule.Apply(new TransitionUsage { Id = Guid.NewGuid() }), Is.Empty);

                Assert.That(rule.Apply(new Feature { Id = Guid.NewGuid() }), Is.Empty);
                Assert.That(() => rule.Apply(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyUnresolvedLinkThrows()
        {
            var rule = new DecisionNodeOutgoingSuccessionSpecializationRule(new StubIndex(new Dictionary<string, IType>()), this.factory);

            var succession = BuildSuccession(new DecisionNode { Id = Guid.NewGuid() }, new ActionUsage { Id = Guid.NewGuid() });

            Assert.That(() => rule.Apply(succession), Throws.TypeOf<UnresolvedLibraryTypeException>());
        }

        private static void Own(IElement owner, IElement owned, IRelationship membership)
        {
            ((IContainedRelationship)membership).OwnedRelatedElement.Add(owned);
            ((IContainedElement)owner).OwnedRelationship.Add(membership);
        }

        private static Succession BuildSuccession(IFeature source, IFeature target)
        {
            // relatedFeature derives through the connector ends, each of which reaches its participant by an
            // owned ReferenceSubsetting — so a Succession cannot be stated by assigning Source/Target.
            var succession = new Succession { Id = Guid.NewGuid() };

            foreach (var participant in new[] { source, target })
            {
                var end = new Feature { Id = Guid.NewGuid(), IsEnd = true };
                end.AssignOwnership(new ReferenceSubsetting { Id = Guid.NewGuid(), ReferencedFeature = participant });
                succession.AssignOwnership(new EndFeatureMembership { Id = Guid.NewGuid() }, end);
            }

            return succession;
        }

        private sealed class StubIndex : ILibraryTypeIndex
        {
            private readonly IDictionary<string, IType> typesByQualifiedName;

            public StubIndex(IDictionary<string, IType> typesByQualifiedName)
            {
                this.typesByQualifiedName = typesByQualifiedName;
            }

            public bool TryGetType(string qualifiedName, out IType type) => this.typesByQualifiedName.TryGetValue(qualifiedName, out type);
        }
    }
}
