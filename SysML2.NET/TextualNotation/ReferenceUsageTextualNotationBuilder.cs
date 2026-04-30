// -------------------------------------------------------------------------------------------------
// <copyright file="ReferenceUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;

    /// <summary>
    /// Hand-coded part of the <see cref="ReferenceUsageTextualNotationBuilder" />
    /// </summary>
    public static partial class ReferenceUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule PayloadParameter
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>
        /// PayloadParameter : ReferenceUsage =
        ///     PayloadFeature
        ///   | Identification PayloadFeatureSpecializationPart? TriggerValuePart
        ///
        /// Alt 2 applies when the reference usage has identification AND a trigger-style
        /// feature value. Otherwise, delegate to PayloadFeature (Alt 1).
        /// </remarks>
        private static void BuildPayloadParameterHandCoded(IReferenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var hasIdentification = !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName);
            var hasTriggerValue = poco.OwnedRelationship.OfType<IFeatureValue>().Any();

            if (hasIdentification && hasTriggerValue)
            {
                // Alt 2: Identification PayloadFeatureSpecializationPart? TriggerValuePart
                ElementTextualNotationBuilder.BuildIdentification(poco, cursorCache, stringBuilder);

                var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

                if (ownedRelationshipCursor.Current is ISpecialization)
                {
                    FeatureTextualNotationBuilder.BuildPayloadFeatureSpecializationPart(poco, cursorCache, stringBuilder);
                }

                FeatureTextualNotationBuilder.BuildTriggerValuePart(poco, cursorCache, stringBuilder);
            }
            else
            {
                // Alt 1: PayloadFeature
                FeatureTextualNotationBuilder.BuildPayloadFeature(poco, cursorCache, stringBuilder);
            }
        }
    }
}
