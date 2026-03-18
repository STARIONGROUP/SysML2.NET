// -------------------------------------------------------------------------------------------------
// <copyright file="ConjugatedPortTypingTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using System.Text;

    using SysML2.NET.Core.POCO.Systems.Ports;

    /// <summary>
    /// Hand-coded part of the <see cref="ConjugatedPortTypingTextualNotationBuilder"/>
    /// </summary>
    public static partial class ConjugatedPortTypingTextualNotationBuilder
    {
        /// <summary>
        /// Build the originalPortDefinition=~[QualifiedName] rule part
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Ports.IConjugatedPortTyping" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildOriginalPortDefinition(IConjugatedPortTyping poco, StringBuilder stringBuilder)
        {
        }
    }
}
