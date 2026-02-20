// -------------------------------------------------------------------------------------------------
// <copyright file="AssertConstraintUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="AssertConstraintUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Constraints.AssertConstraintUsage" /> element
    /// </summary>
    public class AssertConstraintUsageTextualNotationBuilder : TextualNotationBuilder<SysML2.NET.Core.POCO.Systems.Constraints.AssertConstraintUsage>
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="AssertConstraintUsageTextualNotationBuilder"/>
        /// </summary>
        /// <param name="facade">The <see cref="ITextualNotationBuilderFacade"/> used to query textual notation of referenced <see cref="IElement"/></param>
        public AssertConstraintUsageTextualNotationBuilder(ITextualNotationBuilderFacade facade) : base(facade)
        {
        }

        /// <summary>
        /// Builds the Textual Notation string for the provided <see cref="SysML2.NET.Core.POCO.Systems.Constraints.AssertConstraintUsage"/>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Constraints.AssertConstraintUsage"/> from which the textual notation should be build</param>
        /// <returns>The built textual notation string</returns>
        public override string BuildTextualNotation(SysML2.NET.Core.POCO.Systems.Constraints.AssertConstraintUsage poco)
        {
            var stringBuilder = new StringBuilder();
            // Rule definition : AssertConstraintUsage=OccurrenceUsagePrefix'assert'(isNegated?='not')?(ownedRelationship+=OwnedReferenceSubsettingFeatureSpecializationPart?|'constraint'ConstraintUsageDeclaration)CalculationBody

            // non Terminal : OccurrenceUsagePrefix; Found rule OccurrenceUsagePrefix:OccurrenceUsage=BasicUsagePrefix(isIndividual?='individual')?(portionKind=PortionKind{isPortion=true})?UsageExtensionKeyword*


            stringBuilder.Append("assert ");
            // Group Element 
            // Group Element 
            // non Terminal : CalculationBody; Found rule CalculationBody:Type=';'|'{'CalculationBodyPart'}'



            return stringBuilder.ToString();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
