// ------------------------------------------------------------------------------------------------
// <copyright file="SuccessionTextualNotationBuilder.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.TextualNotation.Writers
{

    using SysML2.NET.Core.POCO.Kernel.Connectors;

    /// <summary>
    /// Hand-coded part of the <see cref="SuccessionTextualNotationBuilder" />
    /// </summary>
    public static partial class SuccessionTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule SuccessionDeclaration
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.ISuccession" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>
        /// SuccessionDeclaration : Succession =
        ///     FeatureDeclaration ( 'first' ownedRelationship += ConnectorEndMember 'then' ownedRelationship += ConnectorEndMember )?
        ///   | ( isSufficient ?= 'all' )? ( 'first'? ownedRelationship += ConnectorEndMember 'then' ownedRelationship += ConnectorEndMember )?
        ///
        /// Auto-gen delegates entirely to this method, which in turn delegates to the shared
        /// <see cref="SharedTextualNotationBuilder.BuildTwoEndedConnectorDeclarationHandCoded"/> helper —
        /// the rule shares its body with <c>BindingConnectorDeclaration</c> save for the two end-keyword literals.
        /// </remarks>
        private static void BuildSuccessionDeclarationHandCoded(ISuccession poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            SharedTextualNotationBuilder.BuildTwoEndedConnectorDeclarationHandCoded(poco, "first ", "then ", writerContext, stringBuilder);
        }
    }
}
