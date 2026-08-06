// -------------------------------------------------------------------------------------------------
// <copyright file="ConjugatedPortTypingTextualNotationBuilder.cs" company="Starion Group S.A.">
// 
//    Copyright (C) 2022-2026 Starion Group S.A.
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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
