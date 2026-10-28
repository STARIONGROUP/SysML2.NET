// ------------------------------------------------------------------------------------------------
// <copyright file="InterfaceUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.TextualNotation.Writers
{
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Systems.Interfaces;

    /// <summary>
    /// Hand-coded part of the <see cref="InterfaceUsageTextualNotationBuilder" />
    /// </summary>
    public static partial class InterfaceUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceUsageDeclaration
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>
        /// InterfaceUsageDeclaration : InterfaceUsage =
        ///     UsageDeclaration ValuePart? ( 'connect' InterfacePart )?
        ///   | InterfacePart
        ///
        /// Auto-gen delegates entirely to this method.
        /// </remarks>
        private static void BuildInterfaceUsageDeclarationHandCoded(IInterfaceUsage poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            var hasDeclaration = !string.IsNullOrWhiteSpace(poco.DeclaredShortName)
                                 || !string.IsNullOrWhiteSpace(poco.DeclaredName)
                                 || ownedRelationshipCursor.Current is ISpecialization
                                 || ownedRelationshipCursor.Current is IConjugation;

            if (hasDeclaration)
            {
                // Alt 1: UsageDeclaration ValuePart? ('connect' InterfacePart)?
                UsageTextualNotationBuilder.BuildUsageDeclaration(poco, writerContext, stringBuilder);
                FeatureTextualNotationBuilder.BuildValuePart(poco, writerContext, stringBuilder);

                if (poco.OwnedRelationship.OfType<IEndFeatureMembership>().Any())
                {
                    stringBuilder.Append("connect ");
                    BuildInterfacePart(poco, writerContext, stringBuilder);
                }
            }
            else
            {
                // Alt 2: InterfacePart
                BuildInterfacePart(poco, writerContext, stringBuilder);
            }
        }
    }
}
