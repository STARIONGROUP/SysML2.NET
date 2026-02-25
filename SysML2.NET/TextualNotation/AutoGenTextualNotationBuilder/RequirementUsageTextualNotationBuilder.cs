// -------------------------------------------------------------------------------------------------
// <copyright file="RequirementUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="RequirementUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage" /> element
    /// </summary>
    public static partial class RequirementUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule ObjectiveRequirementUsage
        /// <para>ObjectiveRequirementUsage:RequirementUsage=UsageExtensionKeyword*ConstraintUsageDeclarationRequirementBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildObjectiveRequirementUsage(SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : UsageExtensionKeyword; Found rule UsageExtensionKeyword:Usage=ownedRelationship+=PrefixMetadataMember 
            UsageTextualNotationBuilder.BuildUsageExtensionKeyword(poco, stringBuilder);
            // non Terminal : ConstraintUsageDeclaration; Found rule ConstraintUsageDeclaration:ConstraintUsage=UsageDeclarationValuePart? 
            ConstraintUsageTextualNotationBuilder.BuildConstraintUsageDeclaration(poco, stringBuilder);
            // non Terminal : RequirementBody; Found rule RequirementBody:Type=';'|'{'RequirementBodyItem*'}' 
            TypeTextualNotationBuilder.BuildRequirementBody(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule RequirementVerificationUsage
        /// <para>RequirementVerificationUsage:RequirementUsage=ownedRelationship+=OwnedReferenceSubsettingFeatureSpecialization*RequirementBody|(UsageExtensionKeyword*'requirement'|UsageExtensionKeyword+)ConstraintUsageDeclarationRequirementBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRequirementVerificationUsage(SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule RequirementUsage
        /// <para>RequirementUsage=OccurrenceUsagePrefix'requirement'ConstraintUsageDeclarationRequirementBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRequirementUsage(SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : OccurrenceUsagePrefix; Found rule OccurrenceUsagePrefix:OccurrenceUsage=BasicUsagePrefix(isIndividual?='individual')?(portionKind=PortionKind{isPortion=true})?UsageExtensionKeyword* 
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, stringBuilder);
            stringBuilder.Append("requirement ");
            // non Terminal : ConstraintUsageDeclaration; Found rule ConstraintUsageDeclaration:ConstraintUsage=UsageDeclarationValuePart? 
            ConstraintUsageTextualNotationBuilder.BuildConstraintUsageDeclaration(poco, stringBuilder);
            // non Terminal : RequirementBody; Found rule RequirementBody:Type=';'|'{'RequirementBodyItem*'}' 
            TypeTextualNotationBuilder.BuildRequirementBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
