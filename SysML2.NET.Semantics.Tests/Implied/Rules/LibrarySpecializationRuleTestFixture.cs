// -------------------------------------------------------------------------------------------------
// <copyright file="LibrarySpecializationRuleTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Semantics.Implied;
    using SysML2.NET.Semantics.Implied.Rules;

    [TestFixture]
    public class LibrarySpecializationRuleTestFixture
    {
        /// <summary>
        /// Every library Feature the four condition-selected constraints can target.
        /// </summary>
        private static readonly string[] LibraryTargets =
        [
            "Constraints::assertedConstraintChecks",
            "Constraints::negatedConstraintChecks",
            "Requirements::satisfiedRequirementChecks",
            "Requirements::notSatisfiedRequirementChecks",
            "Actions::ifThenActions",
            "Actions::ifThenElseActions",
            "Performances::trueEvaluations",
            "Performances::falseEvaluations",
            "Performances::constructorEvaluations"
        ];

        private ImpliedRelationshipFactory factory;

        private ILibraryTypeIndex libraryTypeIndex;

        private Dictionary<string, IType> libraryFeaturesByQualifiedName;

        [SetUp]
        public void SetUp()
        {
            this.factory = new ImpliedRelationshipFactory();

            this.libraryFeaturesByQualifiedName = LibraryTargets.ToDictionary(
                qualifiedName => qualifiedName,
                qualifiedName => (IType)new Feature { Id = Guid.NewGuid(), DeclaredName = qualifiedName });

            this.libraryTypeIndex = new StubLibraryTypeIndex(this.libraryFeaturesByQualifiedName);
        }

        [Test]
        public void VerifyAssertConstraintUsageSpecializationRule()
        {
            var rule = new AssertConstraintUsageSpecializationRule(this.libraryTypeIndex, this.factory);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rule.ConstraintName, Is.EqualTo("checkAssertConstraintUsageSpecialization"));

                Assert.That(SubsettedBy(rule, new AssertConstraintUsage { Id = Guid.NewGuid() }),
                    Is.EqualTo([this.Library("Constraints::assertedConstraintChecks")]));

                Assert.That(SubsettedBy(rule, new AssertConstraintUsage { Id = Guid.NewGuid(), IsNegated = true }),
                    Is.EqualTo([this.Library("Constraints::negatedConstraintChecks")]));

                // A SatisfyRequirementUsage IS an AssertConstraintUsage but has its own constraint, so this
                // rule must stand down or the element would imply two unrelated library subsettings.
                Assert.That(rule.Apply(new SatisfyRequirementUsage { Id = Guid.NewGuid() }), Is.Empty);

                Assert.That(rule.Apply(new Feature { Id = Guid.NewGuid() }), Is.Empty);
                Assert.That(() => rule.Apply(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifySatisfyRequirementUsageSpecializationRule()
        {
            var rule = new SatisfyRequirementUsageSpecializationRule(this.libraryTypeIndex, this.factory);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rule.ConstraintName, Is.EqualTo("checkSatisfyRequirementUsageSpecialization"));

                Assert.That(SubsettedBy(rule, new SatisfyRequirementUsage { Id = Guid.NewGuid() }),
                    Is.EqualTo([this.Library("Requirements::satisfiedRequirementChecks")]));

                Assert.That(SubsettedBy(rule, new SatisfyRequirementUsage { Id = Guid.NewGuid(), IsNegated = true }),
                    Is.EqualTo([this.Library("Requirements::notSatisfiedRequirementChecks")]));

                // The plain AssertConstraintUsage is the other rule's business.
                Assert.That(rule.Apply(new AssertConstraintUsage { Id = Guid.NewGuid() }), Is.Empty);

                Assert.That(() => rule.Apply(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyIfActionUsageSpecializationRule()
        {
            var rule = new IfActionUsageSpecializationRule(this.libraryTypeIndex, this.factory);

            // elseAction is inputParameter(3), so a three-parameter if action is the one with an else branch.
            var withElse = new IfActionUsage { Id = Guid.NewGuid() };
            AddInputParameter(withElse, new Feature { Id = Guid.NewGuid() });
            AddInputParameter(withElse, new Feature { Id = Guid.NewGuid() });
            AddInputParameter(withElse, new ActionUsage { Id = Guid.NewGuid() });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rule.ConstraintName, Is.EqualTo("checkIfActionUsageSpecialization"));

                Assert.That(SubsettedBy(rule, new IfActionUsage { Id = Guid.NewGuid() }),
                    Is.EqualTo([this.Library("Actions::ifThenActions")]));

                Assert.That(SubsettedBy(rule, withElse),
                    Is.EqualTo([this.Library("Actions::ifThenElseActions")]));

                Assert.That(rule.Apply(new Feature { Id = Guid.NewGuid() }), Is.Empty);
                Assert.That(() => rule.Apply(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyInvariantSpecializationRule()
        {
            var rule = new InvariantSpecializationRule(this.libraryTypeIndex, this.factory);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rule.ConstraintName, Is.EqualTo("checkInvariantSpecialization"));

                Assert.That(SubsettedBy(rule, new Invariant { Id = Guid.NewGuid() }),
                    Is.EqualTo([this.Library("Performances::trueEvaluations")]));

                Assert.That(SubsettedBy(rule, new Invariant { Id = Guid.NewGuid(), IsNegated = true }),
                    Is.EqualTo([this.Library("Performances::falseEvaluations")]));

                Assert.That(rule.Apply(new Feature { Id = Guid.NewGuid() }), Is.Empty);
                Assert.That(() => rule.Apply(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyConstructorExpressionResultSpecializationRule()
        {
            var rule = new ConstructorExpressionResultSpecializationRule(this.factory);

            // KerML 1.0 §8.4.4.9.4: a FeatureTyping when the instantiatedType is a Classifier, a Subsetting
            // when it is a Feature. The OCL (result.specializes(instantiatedType)) does not say which.
            var ontoClassifier = BuildConstructorExpression(new Classifier { Id = Guid.NewGuid() }, out var classifierResult);
            var ontoFeature = BuildConstructorExpression(new Feature { Id = Guid.NewGuid() }, out var featureResult);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rule.ConstraintName, Is.EqualTo("checkConstructorExpressionResultSpecialization"));

                var typing = rule.Apply(ontoClassifier).OfType<IFeatureTyping>().ToList();
                Assert.That(typing, Has.Count.EqualTo(1));
                Assert.That(typing[0].TypedFeature, Is.SameAs(classifierResult));

                var subsetting = rule.Apply(ontoFeature).OfType<ISubsetting>().ToList();
                Assert.That(subsetting, Has.Count.EqualTo(1));
                Assert.That(subsetting[0].SubsettingFeature, Is.SameAs(featureResult));

                // No instantiatedType and no result are both legitimately silent, not errors.
                Assert.That(rule.Apply(new ConstructorExpression { Id = Guid.NewGuid() }), Is.Empty);
                Assert.That(rule.Apply(new Feature { Id = Guid.NewGuid() }), Is.Empty);
                Assert.That(() => rule.Apply(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyConstructorExpressionSpecializationRule()
        {
            var rule = new ConstructorExpressionSpecializationRule(this.libraryTypeIndex, this.factory);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rule.ConstraintName, Is.EqualTo("checkConstructorExpressionSpecialization"));

                Assert.That(SubsettedBy(rule, new ConstructorExpression { Id = Guid.NewGuid() }),
                    Is.EqualTo([this.Library("Performances::constructorEvaluations")]));

                Assert.That(rule.Apply(new Feature { Id = Guid.NewGuid() }), Is.Empty);
                Assert.That(() => rule.Apply(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyInvocationExpressionSpecializationRule()
        {
            var rule = new InvocationExpressionSpecializationRule(this.factory);

            // KerML 1.0 §8.4.4.9.5: ALWAYS a FeatureTyping, whether the instantiatedType is a Classifier or
            // a Feature — unlike the ConstructorExpression result, where the kind depends on it.
            var ontoClassifier = BuildInvocationExpression(new Classifier { Id = Guid.NewGuid() });
            var ontoFeature = BuildInvocationExpression(new Feature { Id = Guid.NewGuid() });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rule.ConstraintName, Is.EqualTo("checkInvocationExpressionSpecialization"));

                Assert.That(rule.Apply(ontoClassifier).OfType<IFeatureTyping>().Count(), Is.EqualTo(1));
                Assert.That(rule.Apply(ontoFeature).OfType<IFeatureTyping>().Count(), Is.EqualTo(1));
                Assert.That(rule.Apply(ontoFeature).OfType<ISubsetting>(), Is.Empty);

                Assert.That(rule.Apply(new InvocationExpression { Id = Guid.NewGuid() }), Is.Empty);
                Assert.That(rule.Apply(new Feature { Id = Guid.NewGuid() }), Is.Empty);
                Assert.That(() => rule.Apply(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyFeatureReferenceExpressionResultSpecializationRule()
        {
            var rule = new FeatureReferenceExpressionResultSpecializationRule(this.factory);

            var referent = new Feature { Id = Guid.NewGuid() };

            var expression = new FeatureReferenceExpression { Id = Guid.NewGuid() };
            var result = new Feature { Id = Guid.NewGuid() };
            Own(expression, result, new ReturnParameterMembership { Id = Guid.NewGuid() });
            ((IContainedElement)expression).OwnedRelationship.Add(new Membership { Id = Guid.NewGuid(), MemberElement = referent });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rule.ConstraintName, Is.EqualTo("checkFeatureReferenceExpressionResultSpecialization"));

                var subsetting = rule.Apply(expression).OfType<ISubsetting>().ToList();
                Assert.That(subsetting, Has.Count.EqualTo(1));
                Assert.That(subsetting[0].SubsettingFeature, Is.SameAs(result));
                Assert.That(subsetting[0].SubsettedFeature, Is.SameAs(referent));

                Assert.That(rule.Apply(new FeatureReferenceExpression { Id = Guid.NewGuid() }), Is.Empty);
                Assert.That(rule.Apply(new Feature { Id = Guid.NewGuid() }), Is.Empty);
                Assert.That(() => rule.Apply(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyUnresolvedLibraryTypeThrows()
        {
            // The library name is part of the rule, not of the model, so failing to resolve it is a
            // configuration fault the caller must see rather than an element that simply implies nothing.
            var rule = new InvariantSpecializationRule(new StubLibraryTypeIndex(new Dictionary<string, IType>()), this.factory);

            Assert.That(() => rule.Apply(new Invariant { Id = Guid.NewGuid() }), Throws.TypeOf<UnresolvedLibraryTypeException>());
        }

        private static ConstructorExpression BuildConstructorExpression(IType instantiatedType, out IFeature result)
        {
            var constructorExpression = new ConstructorExpression { Id = Guid.NewGuid() };

            result = new Feature { Id = Guid.NewGuid() };
            Own(constructorExpression, result, new ReturnParameterMembership { Id = Guid.NewGuid() });

            // instantiatedType is the member of the first ownedMembership that is NOT a FeatureMembership —
            // the `alias of T` of KerML 1.0 §8.4.4.9.4 — so a plain Membership, not a typing.
            ((IContainedElement)constructorExpression).OwnedRelationship.Add(new Membership { Id = Guid.NewGuid(), MemberElement = instantiatedType });

            return constructorExpression;
        }

        private static InvocationExpression BuildInvocationExpression(IType instantiatedType)
        {
            var invocationExpression = new InvocationExpression { Id = Guid.NewGuid() };
            ((IContainedElement)invocationExpression).OwnedRelationship.Add(new Membership { Id = Guid.NewGuid(), MemberElement = instantiatedType });

            return invocationExpression;
        }

        private static void Own(IElement owner, IElement owned, IRelationship relationship)
        {
            ((IContainedRelationship)relationship).OwnedRelatedElement.Add(owned);
            ((IContainedElement)owner).OwnedRelationship.Add(relationship);
        }

        private static void AddInputParameter(IElement owner, IFeature parameter)
        {
            parameter.Direction = FeatureDirectionKind.In;

            var membership = new FeatureMembership { Id = Guid.NewGuid() };
            ((IContainedRelationship)membership).OwnedRelatedElement.Add(parameter);
            ((IContainedElement)owner).OwnedRelationship.Add(membership);
        }

        private IFeature Library(string qualifiedName) => (IFeature)this.libraryFeaturesByQualifiedName[qualifiedName];

        private static IReadOnlyList<IFeature> SubsettedBy(IImpliedRelationshipRule rule, IElement element)
        {
            return [..rule.Apply(element).OfType<ISubsetting>().Select(subsetting => subsetting.SubsettedFeature)];
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
    }
}
