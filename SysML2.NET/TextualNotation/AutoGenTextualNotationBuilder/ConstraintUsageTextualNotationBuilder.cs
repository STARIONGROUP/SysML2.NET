// -------------------------------------------------------------------------------------------------
// <copyright file="ConstraintUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="ConstraintUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Constraints.IConstraintUsage" /> element
    /// </summary>
    public static partial class ConstraintUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule ConstraintUsageDeclaration
        /// <para>ConstraintUsageDeclaration:ConstraintUsage=UsageDeclarationValuePart?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Constraints.IConstraintUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildConstraintUsageDeclaration(SysML2.NET.Core.POCO.Systems.Constraints.IConstraintUsage poco, StringBuilder stringBuilder)
        {
            UsageTextualNotationBuilder.BuildUsageDeclaration(poco, stringBuilder);
            FeatureTextualNotationBuilder.BuildValuePart(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule RequirementConstraintUsage
        /// <para>RequirementConstraintUsage:ConstraintUsage=ownedRelationship+=OwnedReferenceSubsettingFeatureSpecializationPart?RequirementBody|(UsageExtensionKeyword*'constraint'|UsageExtensionKeyword+)ConstraintUsageDeclarationCalculationBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Constraints.IConstraintUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRequirementConstraintUsage(SysML2.NET.Core.POCO.Systems.Constraints.IConstraintUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ConstraintUsage
        /// <para>ConstraintUsage=OccurrenceUsagePrefix'constraint'ConstraintUsageDeclarationCalculationBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Constraints.IConstraintUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildConstraintUsage(SysML2.NET.Core.POCO.Systems.Constraints.IConstraintUsage poco, StringBuilder stringBuilder)
        {
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, stringBuilder);
            stringBuilder.Append("constraint ");
            BuildConstraintUsageDeclaration(poco, stringBuilder);
            TypeTextualNotationBuilder.BuildCalculationBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
