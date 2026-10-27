// ------------------------------------------------------------------------------------------------
// <copyright file="ConjugatedPortTypingTextualNotationBuilder.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.TextualNotation.Writers
{

    using SysML2.NET.Core.POCO.Systems.Ports;

    /// <summary>
    /// Hand-coded part of the <see cref="ConjugatedPortTypingTextualNotationBuilder"/>
    /// </summary>
    public static partial class ConjugatedPortTypingTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule ConjugatedPortTyping.
        /// <remarks>ConjugatedPortTyping:ConjugatedPortTyping='~'originalPortDefinition=~[QualifiedName]</remarks>
        /// <para>The grammar names the referenced property <c>originalPortDefinition</c>, which does not
        /// resolve against <see cref="IConjugatedPortTyping"/>. The metamodel exposes the same value as the
        /// derived <c>portDefinition</c>, documented as "the originalPortDefinition of the
        /// conjugatedPortDefinition of this ConjugatedPortTyping" and derived by
        /// <c>portDefinition = conjugatedPortDefinition.originalPortDefinition</c> — so that property is the
        /// faithful source for the <c>[QualifiedName]</c>.</para>
        /// <para>The leading <c>'~'</c> token is emitted by the generated wrapper; this method contributes
        /// only the qualified name, giving <c>~FuelPort</c>.</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Ports.IConjugatedPortTyping" /> from which the rule should be build</param>
        /// <param name="writerContext"> The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that contains the entire textual notation</param>
        private static void BuildConjugatedPortTypingHandCoded(IConjugatedPortTyping poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            if (poco.portDefinition != null)
            {
                SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder, poco.portDefinition, writerContext, poco);
            }
        }
    }
}
