// -------------------------------------------------------------------------------------------------
// <copyright file="VariationSpecializationRuleTestFixture.cs" company="Starion Group S.A.">
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
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Semantics.Implied;
    using SysML2.NET.Semantics.Implied.Rules;

    [TestFixture]
    public class VariationSpecializationRuleTestFixture
    {
        private ImpliedRelationshipFactory factory;

        private VariationUsageSpecializationRule usageRule;

        private VariationDefinitionSpecializationRule definitionRule;

        [SetUp]
        public void SetUp()
        {
            this.factory = new ImpliedRelationshipFactory();
            this.usageRule = new VariationUsageSpecializationRule(this.factory);
            this.definitionRule = new VariationDefinitionSpecializationRule(this.factory);
        }

        [Test]
        public void VerifyApplyForVariationUsage()
        {
            // variation part p { variant part p1; }  ->  member feature p1 subsets p
            var variation = new PartUsage { Id = Guid.NewGuid(), DeclaredName = "p", IsVariation = true };
            var variant = new PartUsage { Id = Guid.NewGuid(), DeclaredName = "p1" };

            OwnAsVariant(variation, variant);

            var implied = this.usageRule.Apply(variant);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(implied, Has.Count.EqualTo(1));
                Assert.That(implied[0], Is.InstanceOf<ISubsetting>());

                var subsetting = (ISubsetting)implied[0];
                Assert.That(subsetting.IsImplied, Is.True);
                Assert.That(subsetting.SubsettingFeature, Is.SameAs(variant));
                Assert.That(subsetting.SubsettedFeature, Is.SameAs(variation));

                // The Definition rule must decline: the owner is a Usage, not a Definition.
                Assert.That(this.definitionRule.Apply(variant), Is.Empty);
            }
        }

        [Test]
        public void VerifyApplyForVariationDefinition()
        {
            // variation part def P { variant part p1; }  ->  member feature p1 : P
            var variation = new PartDefinition { Id = Guid.NewGuid(), DeclaredName = "P", IsVariation = true };
            var variant = new PartUsage { Id = Guid.NewGuid(), DeclaredName = "p1" };

            OwnAsVariant(variation, variant);

            var implied = this.definitionRule.Apply(variant);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(implied, Has.Count.EqualTo(1));
                Assert.That(implied[0], Is.InstanceOf<IFeatureTyping>());

                var featureTyping = (IFeatureTyping)implied[0];
                Assert.That(featureTyping.IsImplied, Is.True);
                Assert.That(featureTyping.TypedFeature, Is.SameAs(variant));
                Assert.That(featureTyping.Type, Is.SameAs(variation));

                // A Definition is a Classifier, not a Usage, so the Usage rule must decline.
                Assert.That(this.usageRule.Apply(variant), Is.Empty);
            }
        }

        [Test]
        public void VerifyApplyDeclinesNonVariants()
        {
            var owner = new PartUsage { Id = Guid.NewGuid(), DeclaredName = "owner" };
            var ordinary = new PartUsage { Id = Guid.NewGuid(), DeclaredName = "ordinary" };

            // An ordinary OwningMembership is NOT a VariantMembership, so neither rule applies.
            var membership = new OwningMembership { Id = Guid.NewGuid() };
            ((IContainedRelationship)membership).OwnedRelatedElement.Add(ordinary);
            ((IContainedElement)owner).OwnedRelationship.Add(membership);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.usageRule.Apply(ordinary), Is.Empty);
                Assert.That(this.definitionRule.Apply(ordinary), Is.Empty);
                Assert.That(this.usageRule.Apply(new Feature { Id = Guid.NewGuid() }), Is.Empty);

                Assert.That(() => this.usageRule.Apply(null), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => this.definitionRule.Apply(null), Throws.TypeOf<ArgumentNullException>());

                Assert.That(() => new VariationUsageSpecializationRule(null), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => new VariationDefinitionSpecializationRule(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyConstraintName()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.usageRule.ConstraintName, Is.EqualTo("checkUsageVariationUsageSpecialization"));
                Assert.That(this.definitionRule.ConstraintName, Is.EqualTo("checkUsageVariationDefinitionSpecialization"));

                // Both names must exist in the generated manifest, otherwise the rule covers nothing.
                Assert.That(ImpliedRelationshipTable.AllConstraintNames, Contains.Item(this.usageRule.ConstraintName));
                Assert.That(ImpliedRelationshipTable.AllConstraintNames, Contains.Item(this.definitionRule.ConstraintName));
                Assert.That(ImpliedRelationshipTable.NotCovered.Any(entry => entry.Contains(this.usageRule.ConstraintName)), Is.True);
            }
        }

        private static void OwnAsVariant(INamespace variation, IElement variant)
        {
            var membership = new VariantMembership { Id = Guid.NewGuid() };
            ((IContainedRelationship)membership).OwnedRelatedElement.Add(variant);
            ((IContainedElement)variation).OwnedRelationship.Add(membership);
        }
    }
}
