// -------------------------------------------------------------------------------------------------
// <copyright file="SatisfyRequirementUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.TextualNotation
{
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="SatisfyRequirementUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Requirements.SatisfyRequirementUsage" /> element
    /// </summary>
    public class SatisfyRequirementUsageTextualNotationBuilder : TextualNotationBuilder<SysML2.NET.Core.POCO.Systems.Requirements.SatisfyRequirementUsage>
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="SatisfyRequirementUsageTextualNotationBuilder"/>
        /// </summary>
        /// <param name="facade">The <see cref="ITextualNotationBuilderFacade"/> used to query textual notation of referenced <see cref="IElement"/></param>
        public SatisfyRequirementUsageTextualNotationBuilder(ITextualNotationBuilderFacade facade) : base(facade)
        {
        }

        /// <summary>
        /// Builds the Textual Notation string for the provided <see cref="SysML2.NET.Core.POCO.Systems.Requirements.SatisfyRequirementUsage"/>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Requirements.SatisfyRequirementUsage"/> from which the textual notation should be build</param>
        /// <returns>The built textual notation string</returns>
        public override string BuildTextualNotation(SysML2.NET.Core.POCO.Systems.Requirements.SatisfyRequirementUsage poco)
        {
            var stringBuilder = new StringBuilder();
            // Rule definition : SatisfyRequirementUsage=OccurrenceUsagePrefix'assert'(isNegated?='not')'satisfy'(ownedRelationship+=OwnedReferenceSubsettingFeatureSpecializationPart?|'requirement'UsageDeclaration)ValuePart?('by'ownedRelationship+=SatisfactionSubjectMember)?RequirementBody

            // non Terminal : OccurrenceUsagePrefix; Found rule OccurrenceUsagePrefix:OccurrenceUsage=BasicUsagePrefix(isIndividual?='individual')?(portionKind=PortionKind{isPortion=true})?UsageExtensionKeyword*


            stringBuilder.Append("assert ");
            // Group Element 
            stringBuilder.Append("satisfy ");
            // Group Element 
            // non Terminal : ValuePart; Found rule ValuePart:Feature=ownedRelationship+=FeatureValue


            // Group Element 
            // non Terminal : RequirementBody; Found rule RequirementBody:Type=';'|'{'RequirementBodyItem*'}'



            return stringBuilder.ToString();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
