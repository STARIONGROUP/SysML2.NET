// -------------------------------------------------------------------------------------------------
// <copyright file="AllocationUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Allocations;

    /// <summary>
    /// Hand-coded part of the <see cref="AllocationUsageTextualNotationBuilder" />
    /// </summary>
    public static partial class AllocationUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule AllocationUsageDeclaration
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Allocations.IAllocationUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>
        /// AllocationUsageDeclaration : AllocationUsage =
        ///     'allocation' UsageDeclaration ( 'allocate' ConnectorPart )?
        ///   | 'allocate' ConnectorPart
        ///
        /// Auto-gen delegates entirely to this method.
        /// </remarks>
        private static void BuildAllocationUsageDeclarationHandCoded(IAllocationUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            var hasDeclaration = !string.IsNullOrWhiteSpace(poco.DeclaredShortName)
                                 || !string.IsNullOrWhiteSpace(poco.DeclaredName)
                                 || ownedRelationshipCursor.Current is ISpecialization
                                 || ownedRelationshipCursor.Current is IConjugation;

            if (hasDeclaration)
            {
                // Alt 1: 'allocation' UsageDeclaration ('allocate' ConnectorPart)?
                stringBuilder.Append("allocation ");
                UsageTextualNotationBuilder.BuildUsageDeclaration(poco, cursorCache, stringBuilder);

                if (poco.OwnedRelationship.OfType<IEndFeatureMembership>().Any())
                {
                    stringBuilder.Append("allocate ");
                    ConnectionUsageTextualNotationBuilder.BuildConnectorPart(poco, cursorCache, stringBuilder);
                }
            }
            else
            {
                // Alt 2: 'allocate' ConnectorPart
                stringBuilder.Append("allocate ");
                ConnectionUsageTextualNotationBuilder.BuildConnectorPart(poco, cursorCache, stringBuilder);
            }
        }
    }
}
