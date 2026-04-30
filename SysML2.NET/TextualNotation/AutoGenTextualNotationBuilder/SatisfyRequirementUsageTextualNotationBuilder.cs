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
    using System.Linq;
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
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSatisfyRequirementUsage(SysML2.NET.Core.POCO.Systems.Requirements.ISatisfyRequirementUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, cursorCache, stringBuilder);
            stringBuilder.Append("assert ");
            stringBuilder.Append(" not ");

            stringBuilder.Append(' ');
            stringBuilder.Append("satisfy ");
            if (poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.IReferenceSubsetting>().Any())
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IReferenceSubsetting elementAsReferenceSubsetting)
                    {
                        ReferenceSubsettingTextualNotationBuilder.BuildOwnedReferenceSubsetting(elementAsReferenceSubsetting, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();


                if (poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || poco.IsOrdered)
                {
                    FeatureTextualNotationBuilder.BuildFeatureSpecializationPart(poco, cursorCache, stringBuilder);
                }
            }
            else
            {
                stringBuilder.Append("requirement ");
                UsageTextualNotationBuilder.BuildUsageDeclaration(poco, cursorCache, stringBuilder);
            }

            stringBuilder.Append(' ');

            if (poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.Direction.HasValue || poco.IsDerived || poco.IsAbstract || poco.IsConstant || poco.IsOrdered || poco.IsEnd || poco.importedMembership.Count != 0 || poco.IsComposite || poco.IsPortion || poco.IsVariable || poco.IsSufficient || poco.unioningType.Count != 0 || poco.intersectingType.Count != 0 || poco.differencingType.Count != 0 || poco.featuringType.Count != 0 || poco.ownedTypeFeaturing.Count != 0)
            {
                FeatureTextualNotationBuilder.BuildValuePart(poco, cursorCache, stringBuilder);
            }

            if (ownedRelationshipCursor.Current != null)
            {
                stringBuilder.Append("by ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.Requirements.ISubjectMembership elementAsSubjectMembership)
                    {
                        SubjectMembershipTextualNotationBuilder.BuildSatisfactionSubjectMember(elementAsSubjectMembership, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

                stringBuilder.Append(' ');
            }

            TypeTextualNotationBuilder.BuildRequirementBody(poco, cursorCache, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
