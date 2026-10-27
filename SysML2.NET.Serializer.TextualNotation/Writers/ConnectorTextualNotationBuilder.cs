// ------------------------------------------------------------------------------------------------
// <copyright file="ConnectorTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.Connectors;

    /// <summary>
    /// Hand-coded part of the <see cref="ConnectorTextualNotationBuilder" />
    /// </summary>
    public static partial class ConnectorTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule BinaryConnectorDeclaration
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.IConnector" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>
        /// BinaryConnectorDeclaration : Connector =
        ///     ( FeatureDeclaration? 'from' | isSufficient ?= 'all' 'from'? )?
        ///     ownedRelationship += ConnectorEndMember 'to' ownedRelationship += ConnectorEndMember
        ///
        /// Auto-gen emits the two ConnectorEndMember + 'to' AFTER this method.
        /// This method handles only the optional preamble: FeatureDeclaration? + 'from' or 'all' + 'from'?.
        /// </remarks>
        private static void BuildBinaryConnectorDeclarationHandCoded(IConnector poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            var hasDeclaration = !string.IsNullOrWhiteSpace(poco.DeclaredShortName)
                                 || !string.IsNullOrWhiteSpace(poco.DeclaredName)
                                 || ownedRelationshipCursor.Current is ISpecialization
                                 || ownedRelationshipCursor.Current is IConjugation;

            if (hasDeclaration)
            {
                FeatureTextualNotationBuilder.BuildFeatureDeclaration(poco, writerContext, stringBuilder);
                stringBuilder.Append("from ");
            }
            else if (poco.IsSufficient)
            {
                stringBuilder.Append("all from ");
            }
            // else: no preamble — entire group is optional per grammar, emit nothing
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Connector
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.IConnector" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>
        /// Connector = FeaturePrefix 'connector' ( FeatureDeclaration? ValuePart? | ConnectorDeclaration ) TypeBody
        ///
        /// Auto-gen emits FeaturePrefix + 'connector ' before and TypeBody after this method.
        /// This method handles: ( FeatureDeclaration? ValuePart? | ConnectorDeclaration )
        /// </remarks>
        private static void BuildConnectorHandCoded(IConnector poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            if (poco.OwnedRelationship.OfType<IEndFeatureMembership>().Any())
            {
                // ConnectorDeclaration — dispatches to BinaryConnectorDeclaration or NaryConnectorDeclaration
                BuildConnectorDeclaration(poco, writerContext, stringBuilder);
            }
            else
            {
                // FeatureDeclaration? ValuePart?
                FeatureTextualNotationBuilder.BuildFeatureDeclaration(poco, writerContext, stringBuilder);
                FeatureTextualNotationBuilder.BuildValuePart(poco, writerContext, stringBuilder);
            }
        }
    }
}
