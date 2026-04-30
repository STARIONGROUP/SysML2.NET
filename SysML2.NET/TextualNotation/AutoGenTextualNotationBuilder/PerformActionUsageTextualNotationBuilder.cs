// -------------------------------------------------------------------------------------------------
// <copyright file="PerformActionUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="PerformActionUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage" /> element
    /// </summary>
    public static partial class PerformActionUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule PerformActionUsageDeclaration
        /// <para>PerformActionUsageDeclaration:PerformActionUsage=(ownedRelationship+=OwnedReferenceSubsettingFeatureSpecializationPart?|'action'UsageDeclaration)ValuePart?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPerformActionUsageDeclaration(SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
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
                stringBuilder.Append("action ");
                UsageTextualNotationBuilder.BuildUsageDeclaration(poco, cursorCache, stringBuilder);
            }

            stringBuilder.Append(' ');

            if (poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.Direction.HasValue || poco.IsDerived || poco.IsAbstract || poco.IsConstant || poco.IsOrdered || poco.IsEnd || poco.importedMembership.Count != 0 || poco.IsComposite || poco.IsPortion || poco.IsVariable || poco.IsSufficient || poco.unioningType.Count != 0 || poco.intersectingType.Count != 0 || poco.differencingType.Count != 0 || poco.featuringType.Count != 0 || poco.ownedTypeFeaturing.Count != 0)
            {
                FeatureTextualNotationBuilder.BuildValuePart(poco, cursorCache, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule StatePerformActionUsage
        /// <para>StatePerformActionUsage:PerformActionUsage=PerformActionUsageDeclarationActionBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildStatePerformActionUsage(SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildPerformActionUsageDeclaration(poco, cursorCache, stringBuilder);
            TypeTextualNotationBuilder.BuildActionBody(poco, cursorCache, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TransitionPerformActionUsage
        /// <para>TransitionPerformActionUsage:PerformActionUsage=PerformActionUsageDeclaration('{'ActionBodyItem*'}')?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTransitionPerformActionUsage(SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildPerformActionUsageDeclaration(poco, cursorCache, stringBuilder);

            if (poco.OwnedRelationship.Count != 0 || poco.importedMembership.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.Direction.HasValue || poco.IsDerived || poco.IsAbstract || poco.IsVariation || poco.IsConstant || poco.IsOrdered || poco.IsEnd || poco.isReference || poco.IsIndividual || poco.PortionKind.HasValue || poco.IsComposite || poco.IsPortion || poco.IsVariable || poco.IsSufficient || poco.unioningType.Count != 0 || poco.intersectingType.Count != 0 || poco.differencingType.Count != 0 || poco.featuringType.Count != 0 || poco.ownedTypeFeaturing.Count != 0)
            {
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");
                var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                while (ownedRelationshipCursor.Current != null)
                {
                    TypeTextualNotationBuilder.BuildActionBodyItem(poco, cursorCache, stringBuilder);
                }

                stringBuilder.AppendLine("}");
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PerformActionUsage
        /// <para>PerformActionUsage=OccurrenceUsagePrefix'perform'PerformActionUsageDeclarationActionBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPerformActionUsage(SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, cursorCache, stringBuilder);
            stringBuilder.Append("perform ");
            BuildPerformActionUsageDeclaration(poco, cursorCache, stringBuilder);
            TypeTextualNotationBuilder.BuildActionBody(poco, cursorCache, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
