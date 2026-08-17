// -------------------------------------------------------------------------------------------------
// <copyright file="RedefinitionRuleTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Associations;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Kernel.Interactions;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Core.Systems.States;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Semantics.Implied;
    using SysML2.NET.Semantics.Implied.Rules;

    [TestFixture]
    public class RedefinitionRuleTestFixture
    {
        private ImpliedRelationshipFactory factory;

        [SetUp]
        public void SetUp()
        {
            this.factory = new ImpliedRelationshipFactory();
        }

        [Test]
        public void VerifyFeatureEndRedefinitionRule()
        {
            var rule = new FeatureEndRedefinitionRule(this.factory);

            var supertype = new Association { Id = Guid.NewGuid() };
            var supertypeFirstEnd = AddEnd(supertype);
            var supertypeSecondEnd = AddEnd(supertype);

            var subtype = new Association { Id = Guid.NewGuid() };
            var firstEnd = AddEnd(subtype);
            var secondEnd = AddEnd(subtype);

            Specialize(subtype, supertype);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rule.ConstraintName, Is.EqualTo("checkFeatureEndRedefinition"));

                // The correspondence is positional: end 1 redefines end 1, end 2 redefines end 2.
                Assert.That(RedefinedBy(rule, firstEnd), Is.EqualTo(new[] { supertypeFirstEnd }));
                Assert.That(RedefinedBy(rule, secondEnd), Is.EqualTo(new[] { supertypeSecondEnd }));

                Assert.That(() => rule.Apply(null), Throws.TypeOf<ArgumentNullException>());
                Assert.That(rule.Apply(new Feature { Id = Guid.NewGuid(), IsEnd = true }), Is.Empty);
                Assert.That(rule.Apply(new Feature { Id = Guid.NewGuid() }), Is.Empty);
            }
        }

        [Test]
        public void VerifyFeatureEndRedefinitionRuleWhenTheSupertypeHasFewerEnds()
        {
            var rule = new FeatureEndRedefinitionRule(this.factory);

            var supertype = new Association { Id = Guid.NewGuid() };
            var supertypeOnlyEnd = AddEnd(supertype);

            var subtype = new Association { Id = Guid.NewGuid() };
            var firstEnd = AddEnd(subtype);
            var secondEnd = AddEnd(subtype);

            Specialize(subtype, supertype);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(RedefinedBy(rule, firstEnd), Is.EqualTo(new[] { supertypeOnlyEnd }));

                // The OCL guards with `endFeature->size() >= i`, so a supertype without an end at this
                // position contributes nothing rather than throwing.
                Assert.That(rule.Apply(secondEnd), Is.Empty);
            }
        }

        [Test]
        public void VerifyFeatureResultRedefinitionRule()
        {
            var rule = new FeatureResultRedefinitionRule(this.factory);

            var supertype = new Function { Id = Guid.NewGuid() };
            var supertypeResult = AddResult(supertype);

            var subtype = new Function { Id = Guid.NewGuid() };
            var result = AddResult(subtype);

            Specialize(subtype, supertype);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rule.ConstraintName, Is.EqualTo("checkFeatureResultRedefinition"));
                Assert.That(RedefinedBy(rule, result), Is.EqualTo(new[] { supertypeResult }));

                // A Feature owned by the Function but which is NOT its result is out of scope.
                var ordinary = new Feature { Id = Guid.NewGuid() };
                Own(subtype, ordinary);
                Assert.That(rule.Apply(ordinary), Is.Empty);

                Assert.That(() => rule.Apply(null), Throws.TypeOf<ArgumentNullException>());
                Assert.That(rule.Apply(new Feature { Id = Guid.NewGuid() }), Is.Empty);
            }
        }

        [Test]
        public void VerifyLibraryRedefinitionRules()
        {
            var payload = new Feature { Id = Guid.NewGuid(), DeclaredName = "payload" };
            var viewRendering = new Feature { Id = Guid.NewGuid(), DeclaredName = "viewRendering" };
            var entryAction = new Feature { Id = Guid.NewGuid(), DeclaredName = "entryAction" };
            var exitAction = new Feature { Id = Guid.NewGuid(), DeclaredName = "exitAction" };
            var loopVar = new Feature { Id = Guid.NewGuid(), DeclaredName = "var" };

            var index = new StubLibraryTypeIndex(new Dictionary<string, IType>
            {
                ["Transfers::Transfer::payload"] = payload,
                ["Views::View::viewRendering"] = viewRendering,
                ["States::StateAction::entryAction"] = entryAction,
                ["States::StateAction::exitAction"] = exitAction,
                ["Actions::ForLoopAction::var"] = loopVar
            });

            var payloadFeature = new PayloadFeature { Id = Guid.NewGuid() };

            var stateSubaction = new ActionUsage { Id = Guid.NewGuid() };
            var membership = new StateSubactionMembership { Id = Guid.NewGuid(), Kind = StateSubactionKind.Entry };
            ((IContainedRelationship)membership).OwnedRelatedElement.Add(stateSubaction);
            ((IContainedElement)new ActionUsage { Id = Guid.NewGuid() }).OwnedRelationship.Add(membership);

            using (Assert.EnterMultipleScope())
            {
                var payloadRule = new PayloadFeatureRedefinitionRule(index, this.factory);
                Assert.That(RedefinedBy(payloadRule, payloadFeature), Is.EqualTo(new[] { payload }));
                Assert.That(payloadRule.Apply(new Feature { Id = Guid.NewGuid() }), Is.Empty);
                Assert.That(() => payloadRule.Apply(null), Throws.TypeOf<ArgumentNullException>());

                // The kind of the owning membership selects which library action is redefined.
                var stateRule = new ActionUsageStateActionRedefinitionRule(index, this.factory);
                Assert.That(RedefinedBy(stateRule, stateSubaction), Is.EqualTo(new[] { entryAction }));

                membership.Kind = StateSubactionKind.Exit;
                Assert.That(RedefinedBy(stateRule, stateSubaction), Is.EqualTo(new[] { exitAction }));

                // An ActionUsage owned any other way is out of scope.
                Assert.That(stateRule.Apply(new ActionUsage { Id = Guid.NewGuid() }), Is.Empty);

                var renderingRule = new RenderingUsageRedefinitionRule(index, this.factory);
                Assert.That(renderingRule.Apply(new RenderingUsage { Id = Guid.NewGuid() }), Is.Empty);

                var forLoopRule = new ForLoopActionUsageVarRedefinitionRule(index, this.factory);
                Assert.That(forLoopRule.Apply(new ForLoopActionUsage { Id = Guid.NewGuid() }), Is.Empty);

                // An unindexed library Feature must fail loudly rather than silently omit the Redefinition.
                var emptyIndex = new StubLibraryTypeIndex(new Dictionary<string, IType>());
                Assert.That(() => new PayloadFeatureRedefinitionRule(emptyIndex, this.factory).Apply(payloadFeature),
                    Throws.TypeOf<UnresolvedLibraryTypeException>());
            }
        }

        /// <summary>
        /// Set B is complete except for one constraint. checkConstructorExpressionResultFeatureRedefinition
        /// asserts a CARDINALITY over redefinitions that already exist —
        /// <c>f.ownedRedefinition.redefinedFeature-&gt;intersection(features)-&gt;size() = 1</c> — rather than
        /// naming which Feature must be redefined. It is a well-formedness check, not a source of implied
        /// Relationships, so it stays uncovered rather than being satisfied by an invented correspondence.
        /// </summary>
        [Test]
        public void VerifyEveryRedefinitionConstraintIsCoveredOrDeliberatelyNot()
        {
            var index = new StubLibraryTypeIndex(new Dictionary<string, IType>());

            IImpliedRelationshipRule[] rules =
            [
                new FeatureEndRedefinitionRule(this.factory),
                new FeatureResultRedefinitionRule(this.factory),
                new FeatureParameterRedefinitionRule(this.factory),
                new RequirementUsageObjectiveRedefinitionRule(this.factory),
                new AssignmentActionUsageReferentRedefinitionRule(this.factory),
                new FeatureChainExpressionSourceTargetRedefinitionRule(this.factory),
                new PayloadFeatureRedefinitionRule(index, this.factory),
                new RenderingUsageRedefinitionRule(index, this.factory),
                new ActionUsageStateActionRedefinitionRule(index, this.factory),
                new ForLoopActionUsageVarRedefinitionRule(index, this.factory),
                new AssignmentActionUsageStartingAtRedefinitionRule(index, this.factory),
                new AssignmentActionUsageAccessedFeatureRedefinitionRule(index, this.factory),
                new FeatureChainExpressionTargetRedefinitionRule(index, this.factory),
                new FeatureFlowFeatureRedefinitionRule(index, this.factory)
            ];

            var covered = rules.Select(rule => rule.ConstraintName).ToList();

            var redefinitionConstraints = ImpliedRelationshipTable.AllConstraintNames
                .Where(constraintName => constraintName.EndsWith("Redefinition", StringComparison.Ordinal))
                .ToList();

            var uncovered = redefinitionConstraints.Except(covered, StringComparer.Ordinal).ToList();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(redefinitionConstraints, Has.Count.EqualTo(15));
                Assert.That(covered, Has.Count.EqualTo(14));
                Assert.That(uncovered, Is.EqualTo(new[] { "checkConstructorExpressionResultFeatureRedefinition" }));
            }
        }

        private sealed class StubLibraryTypeIndex : ILibraryTypeIndex
        {
            private readonly IDictionary<string, IType> typesByQualifiedName;

            public StubLibraryTypeIndex(IDictionary<string, IType> typesByQualifiedName)
            {
                this.typesByQualifiedName = typesByQualifiedName;
            }

            public bool TryGetType(string qualifiedName, out IType type) => this.typesByQualifiedName.TryGetValue(qualifiedName, out type);
        }

        private static IReadOnlyList<IFeature> RedefinedBy(IImpliedRelationshipRule rule, IElement element)
        {
            return [..rule.Apply(element).OfType<IRedefinition>().Select(redefinition => redefinition.RedefinedFeature)];
        }

        private static Feature AddEnd(IType owner)
        {
            var end = new Feature { Id = Guid.NewGuid(), IsEnd = true };
            Own(owner, end, new EndFeatureMembership { Id = Guid.NewGuid() });

            return end;
        }

        private static Feature AddResult(IType owner)
        {
            var result = new Feature { Id = Guid.NewGuid() };
            Own(owner, result, new ReturnParameterMembership { Id = Guid.NewGuid() });

            return result;
        }

        private static void Own(IElement owner, IElement owned, IRelationship membership = null)
        {
            var relationship = membership ?? new FeatureMembership { Id = Guid.NewGuid() };
            ((IContainedRelationship)relationship).OwnedRelatedElement.Add(owned);
            ((IContainedElement)owner).OwnedRelationship.Add(relationship);
        }

        private static void Specialize(IClassifier specific, IClassifier general)
        {
            var subclassification = new Subclassification
            {
                Id = Guid.NewGuid(),
                Subclassifier = specific,
                Superclassifier = general
            };

            ((IContainedElement)specific).OwnedRelationship.Add(subclassification);
        }
    }
}
