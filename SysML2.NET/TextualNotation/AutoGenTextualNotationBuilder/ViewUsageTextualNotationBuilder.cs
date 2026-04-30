// -------------------------------------------------------------------------------------------------
// <copyright file="ViewUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="ViewUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Views.IViewUsage" /> element
    /// </summary>
    public static partial class ViewUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule ViewBody
        /// <para>ViewBody:ViewUsage=';'|'{'ViewBodyItem*'}'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Views.IViewUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildViewBody(SysML2.NET.Core.POCO.Systems.Views.IViewUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            if (poco.OwnedRelationship.Count == 0)
            {
                stringBuilder.AppendLine(";");
            }
            else
            {
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");
                var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                while (ownedRelationshipCursor.Current != null)
                {
                    BuildViewBodyItem(poco, cursorCache, stringBuilder);
                }
                stringBuilder.AppendLine("}");
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ViewBodyItem
        /// <para>ViewBodyItem:ViewUsage=DefinitionBodyItem|ownedRelationship+=ElementFilterMember|ownedRelationship+=ViewRenderingMember|ownedRelationship+=Expose</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Views.IViewUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildViewBodyItem(SysML2.NET.Core.POCO.Systems.Views.IViewUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.Views.IViewRenderingMembership viewRenderingMembership)
            {
                ViewRenderingMembershipTextualNotationBuilder.BuildViewRenderingMember(viewRenderingMembership, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Packages.IElementFilterMembership elementFilterMembership)
            {
                ElementFilterMembershipTextualNotationBuilder.BuildElementFilterMember(elementFilterMembership, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.Views.IExpose expose)
            {
                ExposeTextualNotationBuilder.BuildExpose(expose, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else
            {
                TypeTextualNotationBuilder.BuildDefinitionBodyItem(poco, cursorCache, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ViewUsage
        /// <para>ViewUsage=OccurrenceUsagePrefix'view'UsageDeclaration?ValuePart?ViewBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Views.IViewUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildViewUsage(SysML2.NET.Core.POCO.Systems.Views.IViewUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, cursorCache, stringBuilder);
            stringBuilder.Append("view ");

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || poco.IsOrdered)
            {
                UsageTextualNotationBuilder.BuildUsageDeclaration(poco, cursorCache, stringBuilder);
            }

            if (poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.Direction.HasValue || poco.IsDerived || poco.IsAbstract || poco.IsConstant || poco.IsOrdered || poco.IsEnd || poco.importedMembership.Count != 0 || poco.IsComposite || poco.IsPortion || poco.IsVariable || poco.IsSufficient || poco.unioningType.Count != 0 || poco.intersectingType.Count != 0 || poco.differencingType.Count != 0 || poco.featuringType.Count != 0 || poco.ownedTypeFeaturing.Count != 0)
            {
                FeatureTextualNotationBuilder.BuildValuePart(poco, cursorCache, stringBuilder);
            }
            BuildViewBody(poco, cursorCache, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
