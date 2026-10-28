// ------------------------------------------------------------------------------------------------
// <copyright file="FlowTextualNotationBuilder.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.TextualNotation.Writers
{

    using SysML2.NET.Core.POCO.Kernel.Interactions;

    /// <summary>
    /// Hand-coded part of the <see cref="FlowTextualNotationBuilder" />
    /// </summary>
    public static partial class FlowTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule FlowDeclaration
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Interactions.IFlow" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>
        /// FlowDeclaration : Flow =
        ///     FeatureDeclaration ValuePart?
        ///     ( 'of' ownedRelationship += PayloadFeatureMember )?
        ///     ( 'from' ownedRelationship += FlowEndMember 'to' ownedRelationship += FlowEndMember )?
        ///   | ( isSufficient ?= 'all' )?
        ///     ownedRelationship += FlowEndMember 'to' ownedRelationship += FlowEndMember
        ///
        /// Auto-gen emits FeaturePrefix + 'flow ' before and TypeBody after this method. Body delegates
        /// to the shared <see cref="SharedTextualNotationBuilder.BuildFlowDeclarationHandCoded"/> helper —
        /// the rule shares its body with <c>SuccessionFlow</c>'s <c>FlowDeclaration</c>.
        /// </remarks>
        private static void BuildFlowDeclarationHandCoded(IFlow poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            SharedTextualNotationBuilder.BuildFlowDeclarationHandCoded(poco, writerContext, stringBuilder);
        }
    }
}
