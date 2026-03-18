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
    using System.Collections.Generic;
    using System.Linq;
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
            // Handle collection Non Terminal 
            for (var ownedRelationshipIndex = 0; ownedRelationshipIndex < poco.OwnedRelationship.Count; ownedRelationshipIndex++)
            {
                ownedRelationshipIndex = UsageTextualNotationBuilder.BuildUsageExtensionKeyword(poco, ownedRelationshipIndex, stringBuilder);
            }
            ConstraintUsageTextualNotationBuilder.BuildConstraintUsageDeclaration(poco, stringBuilder);
            TypeTextualNotationBuilder.BuildRequirementBody(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule RequirementVerificationUsage
        /// <para>RequirementVerificationUsage:RequirementUsage=ownedRelationship+=OwnedReferenceSubsettingFeatureSpecialization*RequirementBody|(UsageExtensionKeyword*'requirement'|UsageExtensionKeyword+)ConstraintUsageDeclarationRequirementBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildRequirementVerificationUsage(SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage poco, int elementIndex, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule RequirementUsage
        /// <para>RequirementUsage=OccurrenceUsagePrefix'requirement'ConstraintUsageDeclarationRequirementBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRequirementUsage(SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage poco, StringBuilder stringBuilder)
        {
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, stringBuilder);
            stringBuilder.Append("requirement ");
            ConstraintUsageTextualNotationBuilder.BuildConstraintUsageDeclaration(poco, stringBuilder);
            TypeTextualNotationBuilder.BuildRequirementBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
