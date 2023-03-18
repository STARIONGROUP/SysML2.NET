// -------------------------------------------------------------------------------------------------
// <copyright file="EcoreExtensionsTestFixture.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Tests.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using ECoreNetto;
    using NUnit.Framework;
    using SysML2.NET.CodeGenerator.Extensions;

    [TestFixture]
    public class EcoreExtensionsTestFixture
    {
        private EPackage ePackage;

        [SetUp]
        public void SetUp()
        {
            this.ePackage = DataModelLoader.Load();
        }

        [Test]
        public void Verify_that_QueryDocumentation_returns_expected_result()
        {
            var expected = new List<string>
            {
                "A Usage is a usage of a Definition. A Usage may only be an ownedFeature of a Definition or another",
                "Usage.A Usage may have nestedUsages that model features that apply in the context of the",
                "owningUsage. A Usage may also have Definitions nested in it, but this has no semantic significance,",
                "other than the nested scoping resulting from the Usage being considered as a Namespace for any",
                "nested Definitions.However, if a Usage has isVariation = true, then it represents a variation point",
                "Usage. In this case, all of its members must be variant Usages, related to the Usage by",
                "VariantMembership Relationships. Rather than being features of the Usage, variant Usages model",
                "different concrete alternatives that can be chosen to fill in for the variation point Usage.variant",
                "= variantMembership.ownedVariantUsagevariantMembership =",
                "ownedMembership->selectByKind(VariantMembership)not isVariation implies",
                "variantMembership->isEmpty()isVariation implies variantMembership = ownedMembershipisReference = not",
                "isCompositeowningVariationUsage <> null implies    specializes(owningVariationUsage)isVariation",
                "implies    not ownedSpecialization.specific->exists(isVariation)owningVariationDefinition <> null",
                "implies    specializes(owningVariationDefinition)directedUsage =",
                "directedFeature->selectByKind(Usage)nestedAction =",
                "nestedUsage->selectByKind(ActionUsage)nestedAllocation =",
                "nestedUsage->selectByKind(AllocationUsage)nestedAnalysisCase =",
                "nestedUsage->selectByKind(AnalysisCaseUsage)nestedAttribute =",
                "nestedUsage->selectByKind(AttributeUsage)nestedCalculation =",
                "nestedUsage->selectByKind(CalculationUsage)nestedCase =",
                "nestedUsage->selectByKind(CaseUsage)nestedConcern =",
                "nestedUsage->selectByKind(ConcernUsage)nestedConnection =",
                "nestedUsage->selectByKind(ConnectorAsUsage)nestedConstraint =",
                "nestedUsage->selectByKind(ConstraintUsage)ownedNested =",
                "nestedUsage->selectByKind(EnumerationUsage)nestedFlow =",
                "nestedUsage->selectByKind(FlowUsage)nestedInterface =",
                "nestedUsage->selectByKind(ReferenceUsage)nestedItem =",
                "nestedUsage->selectByKind(ItemUsage)nestedMetadata =",
                "nestedUsage->selectByKind(MetadataUsage)nestedOccurrence =",
                "nestedUsage->selectByKind(OccurrenceUsage)nestedPart =",
                "nestedUsage->selectByKind(PartUsage)nestedPort = nestedUsage->selectByKind(PortUsage)nestedReference",
                "= nestedUsage->selectByKind(ReferenceUsage)nestedRendering =",
                "nestedUsage->selectByKind(RenderingUsage)nestedRequirement =",
                "nestedUsage->selectByKind(RequirementUsage)nestedState =",
                "nestedUsage->selectByKind(StateUsage)nestedTransition =",
                "nestedUsage->selectByKind(TransitionUsage)nestedUsage =",
                "ownedFeature->selectByKind(Usage)nestedUseCase =",
                "nestedUsage->selectByKind(UseCaseUsage)nestedVerificationCase =",
                "nestedUsage->selectByKind(VerificationCaseUsage)nestedView =",
                "nestedUsage->selectByKind(ViewUsage)nestedViewpoint = nestedUsage->selectByKind(ViewpointUsage)usage",
                "= feature->selectByKind(Usage)owningType <> null implies    (owningType.oclIsKindOf(Definition) or  ",
                "ownigType.oclIsKindOf(Usage))",
            };

            var eClass = this.ePackage.EClassifiers.OfType<EClass>().Single(x => x.Name == "Usage");

            var documentation = eClass.QueryDocumentation();

            Assert.That(documentation, Is.EquivalentTo(expected));
        }

        [Test]
        public void Verify_that_QueryDocumentation_gracefully_returns()
        {
            var classifiers = this.ePackage.EClassifiers;

            Assert.That(() =>
            {
                foreach (var eEnum in classifiers)
                {
                    var docs = eEnum.QueryDocumentation();
                }
            }, Throws.Nothing);
        }
    }
}
