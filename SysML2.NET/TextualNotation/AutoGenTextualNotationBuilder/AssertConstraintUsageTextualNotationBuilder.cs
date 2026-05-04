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
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="AssertConstraintUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Constraints.IAssertConstraintUsage" /> element
    /// </summary>
    public static partial class AssertConstraintUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule AssertConstraintUsage
        /// <para>AssertConstraintUsage=OccurrenceUsagePrefix'assert'(isNegated?='not')?(ownedRelationship+=OwnedReferenceSubsettingFeatureSpecializationPart?|'constraint'ConstraintUsageDeclaration)CalculationBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Constraints.IAssertConstraintUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildAssertConstraintUsage(SysML2.NET.Core.POCO.Systems.Constraints.IAssertConstraintUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, writerContext, stringBuilder);
            stringBuilder.Append("assert ");

            if (poco.IsNegated)
            {
                stringBuilder.Append(" not ");
                stringBuilder.Append(' ');
            }

            if (poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.IReferenceSubsetting>().Any())
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IReferenceSubsetting elementAsReferenceSubsetting)
                    {
                        ReferenceSubsettingTextualNotationBuilder.BuildOwnedReferenceSubsetting(elementAsReferenceSubsetting, writerContext, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();


                if (poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || poco.IsOrdered)
                {
                    FeatureTextualNotationBuilder.BuildFeatureSpecializationPart(poco, writerContext, stringBuilder);
                }
            }
            else
            {
                stringBuilder.Append("constraint ");
                ConstraintUsageTextualNotationBuilder.BuildConstraintUsageDeclaration(poco, writerContext, stringBuilder);
            }

            stringBuilder.Append(' ');
            TypeTextualNotationBuilder.BuildCalculationBody(poco, writerContext, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
