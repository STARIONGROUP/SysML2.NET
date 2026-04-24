// -------------------------------------------------------------------------------------------------
// <copyright file="EventOccurrenceUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.TextualNotation
{
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Occurrences;

    /// <summary>
    /// Hand-coded part of the <see cref="EventOccurrenceUsageTextualNotationBuilder" />
    /// </summary>
    public static partial class EventOccurrenceUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the <c>(…)</c> alternation inside the
        /// <c>EventOccurrenceUsage</c> rule.
        /// <para><c>EventOccurrenceUsage = OccurrenceUsagePrefix 'event'
        /// ( ownedRelationship += OwnedReferenceSubsetting FeatureSpecializationPart?
        /// | 'occurrence' UsageDeclaration? )
        /// UsageCompletion</c></para>
        /// <para>Note: <c>UsageDeclaration</c> is optional in the second alternative. Emit it only
        /// when the usage carries identification or specialization content.</para>
        /// </summary>
        /// <param name="poco">The <see cref="IEventOccurrenceUsage"/> being serialised</param>
        /// <param name="cursorCache">The <see cref="ICursorCache"/> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder"/> that contains the entire textual notation</param>
        private static void BuildEventOccurrenceUsageHandCoded(IEventOccurrenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (poco.OwnedRelationship.OfType<IReferenceSubsetting>().Any())
            {
                if (ownedRelationshipCursor.Current is IReferenceSubsetting referenceSubsetting)
                {
                    ReferenceSubsettingTextualNotationBuilder.BuildOwnedReferenceSubsetting(referenceSubsetting, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                }

                if (ownedRelationshipCursor.Current is ISpecialization)
                {
                    FeatureTextualNotationBuilder.BuildFeatureSpecialization(poco, cursorCache, stringBuilder);
                }
            }
            else
            {
                stringBuilder.Append("occurrence ");

                if (HasUsageDeclarationContent(poco))
                {
                    UsageTextualNotationBuilder.BuildUsageDeclaration(poco, cursorCache, stringBuilder);
                }
            }
        }

        /// <summary>
        /// Determines whether a usage carries any <c>UsageDeclaration</c> content
        /// (identification fields or owned specialization relationships). Used to decide
        /// whether to emit an optional <c>UsageDeclaration</c> fragment.
        /// </summary>
        /// <param name="usage">The <see cref="IUsage"/> to test</param>
        /// <returns><c>true</c> when the usage has a declared name or at least one specialization</returns>
        private static bool HasUsageDeclarationContent(IUsage usage)
        {
            return !string.IsNullOrWhiteSpace(usage.DeclaredName)
                || !string.IsNullOrWhiteSpace(usage.DeclaredShortName)
                || usage.OwnedRelationship.OfType<ISpecialization>().Any();
        }
    }
}
