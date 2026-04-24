// -------------------------------------------------------------------------------------------------
// <copyright file="ExhibitStateUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.States;

    /// <summary>
    /// Hand-coded part of the <see cref="ExhibitStateUsageTextualNotationBuilder" />
    /// </summary>
    public static partial class ExhibitStateUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the <c>(…)</c> alternation inside the
        /// <c>ExhibitStateUsage</c> rule.
        /// <para><c>ExhibitStateUsage = OccurrenceUsagePrefix 'exhibit'
        /// ( ownedRelationship += OwnedReferenceSubsetting FeatureSpecializationPart?
        /// | 'state' UsageDeclaration )
        /// ValuePart? StateUsageBody</c></para>
        /// </summary>
        /// <param name="poco">The <see cref="IExhibitStateUsage"/> being serialised</param>
        /// <param name="cursorCache">The <see cref="ICursorCache"/> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder"/> that contains the entire textual notation</param>
        private static void BuildExhibitStateUsageHandCoded(IExhibitStateUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
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
                stringBuilder.Append("state ");
                UsageTextualNotationBuilder.BuildUsageDeclaration(poco, cursorCache, stringBuilder);
            }
        }
    }
}
