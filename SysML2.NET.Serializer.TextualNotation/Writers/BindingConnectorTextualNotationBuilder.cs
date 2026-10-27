// ------------------------------------------------------------------------------------------------
// <copyright file="BindingConnectorTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// Hand-coded part of the <see cref="BindingConnectorTextualNotationBuilder" />
    /// </summary>
    public static partial class BindingConnectorTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule BindingConnectorDeclaration
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.IBindingConnector" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>
        /// BindingConnectorDeclaration : BindingConnector =
        ///     FeatureDeclaration ( 'of' ownedRelationship += ConnectorEndMember '=' ownedRelationship += ConnectorEndMember )?
        ///   | ( isSufficient ?= 'all' )? ( 'of'? ownedRelationship += ConnectorEndMember '=' ownedRelationship += ConnectorEndMember )?
        ///
        /// Auto-gen delegates entirely to this method, which in turn delegates to the shared
        /// <see cref="SharedTextualNotationBuilder.BuildTwoEndedConnectorDeclarationHandCoded"/> helper —
        /// the rule shares its body with <c>SuccessionDeclaration</c> save for the two end-keyword literals.
        /// </remarks>
        private static void BuildBindingConnectorDeclarationHandCoded(IBindingConnector poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            SharedTextualNotationBuilder.BuildTwoEndedConnectorDeclarationHandCoded(poco, "of ", "= ", writerContext, stringBuilder);
        }
    }
}
