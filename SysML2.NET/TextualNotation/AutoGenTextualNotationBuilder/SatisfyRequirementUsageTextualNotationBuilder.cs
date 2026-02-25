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
    /// The <see cref="SatisfyRequirementUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Requirements.ISatisfyRequirementUsage" /> element
    /// </summary>
    public static partial class SatisfyRequirementUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule SatisfyRequirementUsage
        /// <para>SatisfyRequirementUsage=OccurrenceUsagePrefix'assert'(isNegated?='not')'satisfy'(ownedRelationship+=OwnedReferenceSubsettingFeatureSpecializationPart?|'requirement'UsageDeclaration)ValuePart?('by'ownedRelationship+=SatisfactionSubjectMember)?RequirementBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Requirements.ISatisfyRequirementUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSatisfyRequirementUsage(SysML2.NET.Core.POCO.Systems.Requirements.ISatisfyRequirementUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : OccurrenceUsagePrefix; Found rule OccurrenceUsagePrefix:OccurrenceUsage=BasicUsagePrefix(isIndividual?='individual')?(portionKind=PortionKind{isPortion=true})?UsageExtensionKeyword* 
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, stringBuilder);
            stringBuilder.Append("assert ");
            // Group Element
            // Assignment Element : isNegated ?= SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement
            // If property isNegated value is set, print SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement

            stringBuilder.Append("satisfy ");
            // Group Element
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            // non Terminal : ValuePart; Found rule ValuePart:Feature=ownedRelationship+=FeatureValue 
            FeatureTextualNotationBuilder.BuildValuePart(poco, stringBuilder);
            // Group Element
            stringBuilder.Append("by ");
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement

            // non Terminal : RequirementBody; Found rule RequirementBody:Type=';'|'{'RequirementBodyItem*'}' 
            TypeTextualNotationBuilder.BuildRequirementBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
