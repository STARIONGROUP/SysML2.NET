// ------------------------------------------------------------------------------------------------
// <copyright file="ConnectionUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Connections;

    /// <summary>
    /// Hand-coded part of the <see cref="ConnectionUsageTextualNotationBuilder" />
    /// </summary>
    public static partial class ConnectionUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule ConnectionUsage
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Connections.IConnectionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>
        /// ConnectionUsage =
        ///     OccurrenceUsagePrefix                              — emitted by auto-gen BEFORE this
        ///     ( 'connection' UsageDeclaration ValuePart?
        ///       ( 'connect' ConnectorPart )?
        ///     | 'connect' ConnectorPart )
        ///     UsageBody                                          — emitted by auto-gen AFTER this
        ///
        /// Auto-gen emits OccurrenceUsagePrefix before and UsageBody after.
        /// </remarks>
        private static void BuildConnectionUsageHandCoded(IConnectionUsage poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            var hasDeclaration = !string.IsNullOrWhiteSpace(poco.DeclaredShortName)
                                 || !string.IsNullOrWhiteSpace(poco.DeclaredName)
                                 || ownedRelationshipCursor.Current is ISpecialization
                                 || ownedRelationshipCursor.Current is IConjugation;

            var hasConnectorPart = poco.OwnedRelationship.OfType<IEndFeatureMembership>().Count() >= 2;

            if (hasDeclaration || !hasConnectorPart)
            {
                stringBuilder.Append("connection ");
                UsageTextualNotationBuilder.BuildUsageDeclaration(poco, writerContext, stringBuilder);
                FeatureTextualNotationBuilder.BuildValuePart(poco, writerContext, stringBuilder);

                if (hasConnectorPart)
                {
                    stringBuilder.Append("connect ");
                    BuildConnectorPart(poco, writerContext, stringBuilder);
                }
            }
            else
            {
                // Alt 2: 'connect' ConnectorPart
                stringBuilder.Append("connect ");
                BuildConnectorPart(poco, writerContext, stringBuilder);
            }
        }

    }
}
