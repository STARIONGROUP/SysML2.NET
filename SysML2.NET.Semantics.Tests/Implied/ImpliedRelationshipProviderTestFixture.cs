// -------------------------------------------------------------------------------------------------
// <copyright file="ImpliedRelationshipProviderTestFixture.cs" company="Starion Group S.A.">
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
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Semantics.Implied;
    using SysML2.NET.Semantics.Implied.Rules;

    [TestFixture]
    public class ImpliedRelationshipProviderTestFixture
    {
        private ImpliedRelationshipFactory factory;

        private PartUsage variation;

        private PartUsage variant;

        [SetUp]
        public void SetUp()
        {
            this.factory = new ImpliedRelationshipFactory();

            this.variation = new PartUsage { Id = Guid.NewGuid(), DeclaredName = "p", IsVariation = true };
            this.variant = new PartUsage { Id = Guid.NewGuid(), DeclaredName = "p1" };

            var membership = new VariantMembership { Id = Guid.NewGuid() };
            ((IContainedRelationship)membership).OwnedRelatedElement.Add(this.variant);
            ((IContainedElement)this.variation).OwnedRelationship.Add(membership);
        }

        [Test]
        public void VerifyGetImpliedSpecializations()
        {
            var provider = this.CreateProvider();

            var specializations = provider.GetImpliedSpecializations(this.variant);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(specializations, Has.Count.EqualTo(1));
                Assert.That(specializations[0], Is.InstanceOf<ISubsetting>());
                Assert.That(specializations[0].General, Is.SameAs(this.variation));

                // Library specializations are off by default, so nothing table-driven contributes.
                Assert.That(specializations.All(specialization => specialization.IsImplied), Is.True);

                // The model must be untouched: no implied Relationship is attached anywhere.
                Assert.That(this.variant.OwnedRelationship, Is.Empty);
                Assert.That(this.variation.OwnedRelationship.OfType<ISpecialization>(), Is.Empty);
                Assert.That(this.variant.IsImpliedIncluded, Is.False);

                // Memoised: the same instances come back on a second call.
                Assert.That(provider.GetImpliedSpecializations(this.variant), Is.SameAs(specializations));

                Assert.That(() => provider.GetImpliedSpecializations(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyGetImpliedRelationships()
        {
            var provider = this.CreateProvider();

            var relationships = provider.GetImpliedRelationships(this.variant);

            using (Assert.EnterMultipleScope())
            {
                // The Subsetting is reported once, through the Specialization arm, not twice.
                Assert.That(relationships, Has.Count.EqualTo(1));
                Assert.That(relationships[0], Is.InstanceOf<ISubsetting>());

                Assert.That(provider.GetImpliedRelationships(this.variation), Is.Empty);
                Assert.That(() => provider.GetImpliedRelationships(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyGetImpliedRedefinitions()
        {
            var provider = this.CreateProvider();

            using (Assert.EnterMultipleScope())
            {
                // No Redefinition rule is registered yet, so none is produced.
                Assert.That(provider.GetImpliedRedefinitions(this.variant), Is.Empty);
                Assert.That(() => provider.GetImpliedRedefinitions(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyNotCoveredConstraints()
        {
            var withRules = this.CreateProvider();
            var withoutRules = new ImpliedRelationshipProvider(
                OwnershipTreeLibraryTypeIndex.Build([]),
                new ImpliedRuleGuardRegistry([]),
                this.factory,
                new ImpliedSpecializationReducer(),
                new ImpliedRelationshipOptions(),
                []);

            using (Assert.EnterMultipleScope())
            {
                // A registered rule removes its constraint from the manifest.
                Assert.That(withRules.NotCoveredConstraints.Any(entry => entry.Contains("checkUsageVariationUsageSpecialization")), Is.False);
                Assert.That(withoutRules.NotCoveredConstraints.Any(entry => entry.Contains("checkUsageVariationUsageSpecialization")), Is.True);

                Assert.That(withRules.IsConstraintCovered("checkUsageVariationUsageSpecialization"), Is.True);
                Assert.That(withoutRules.IsConstraintCovered("checkUsageVariationUsageSpecialization"), Is.False);

                // With library specializations off, everything table-driven is honestly reported as uncovered.
                Assert.That(withRules.NotCoveredConstraints, Has.Count.GreaterThan(ImpliedRelationshipTable.NotCovered.Count));
                Assert.That(withRules.IsConstraintCovered("checkPortUsageSpecialization"), Is.False);
                Assert.That(withRules.IsConstraintCovered(null), Is.False);
            }
        }

        [Test]
        public void VerifyGetImpliedSpecializationsThrowsWhenAGuardIsMissing()
        {
            // Enabling the library-specialization rules exercises the 63 conditional rows. None has a guard
            // registered yet, so the first one reached must fail loudly rather than be applied as if it were
            // unconditional — applying it would inject Specializations the model does not require.
            var provider = this.CreateProvider(new ImpliedRelationshipOptions { EnableLibrarySpecializations = true });

            Assert.That(() => provider.GetImpliedSpecializations(this.variant), Throws.TypeOf<MissingImpliedRuleGuardException>());
        }

        [Test]
        public void VerifyGetImpliedSpecializationsThrowsWhenALibraryTypeIsMissing()
        {
            // A Classifier's constraints target library Types (Occurrences::Occurrence and friends). With an
            // empty index — the shape produced by loading only the libraries a model happens to import —
            // resolution must fail loudly rather than silently omit the Specialization.
            var provider = this.CreateProvider(new ImpliedRelationshipOptions { EnableLibrarySpecializations = true });

            var classifier = new PartDefinition { Id = Guid.NewGuid(), DeclaredName = "P" };

            Assert.That(() => provider.GetImpliedSpecializations(classifier), Throws.TypeOf<UnresolvedLibraryTypeException>()
                .Or.TypeOf<MissingImpliedRuleGuardException>());
        }

        private ImpliedRelationshipProvider CreateProvider(ImpliedRelationshipOptions options)
        {
            return new ImpliedRelationshipProvider(
                OwnershipTreeLibraryTypeIndex.Build([]),
                new ImpliedRuleGuardRegistry([]),
                this.factory,
                new ImpliedSpecializationReducer(),
                options,
                [new VariationUsageSpecializationRule(this.factory), new VariationDefinitionSpecializationRule(this.factory)]);
        }

        private ImpliedRelationshipProvider CreateProvider()
        {
            return new ImpliedRelationshipProvider(
                OwnershipTreeLibraryTypeIndex.Build([]),
                new ImpliedRuleGuardRegistry([]),
                this.factory,
                new ImpliedSpecializationReducer(),
                new ImpliedRelationshipOptions(),
                [new VariationUsageSpecializationRule(this.factory), new VariationDefinitionSpecializationRule(this.factory)]);
        }
    }
}
