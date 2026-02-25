// -------------------------------------------------------------------------------------------------
// <copyright file="MultiplicityRangeTextualNotationBuilder.cs" company="Starion Group S.A.">
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.TextualNotation
{
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="MultiplicityRangeTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Kernel.Multiplicities.IMultiplicityRange" /> element
    /// </summary>
    public static partial class MultiplicityRangeTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedMultiplicityRange
        /// <para>OwnedMultiplicityRange:MultiplicityRange=MultiplicityBounds</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Multiplicities.IMultiplicityRange" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedMultiplicityRange(SysML2.NET.Core.POCO.Kernel.Multiplicities.IMultiplicityRange poco, StringBuilder stringBuilder)
        {
            // non Terminal : MultiplicityBounds; Found rule MultiplicityBounds:MultiplicityRange='['(ownedRelationship+=MultiplicityExpressionMember'..')?ownedRelationship+=MultiplicityExpressionMember']' 
            BuildMultiplicityBounds(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MultiplicityBounds
        /// <para>MultiplicityBounds:MultiplicityRange='['(ownedRelationship+=MultiplicityExpressionMember'..')?ownedRelationship+=MultiplicityExpressionMember']'</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Multiplicities.IMultiplicityRange" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMultiplicityBounds(SysML2.NET.Core.POCO.Kernel.Multiplicities.IMultiplicityRange poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append("[ ");
            // Group Element
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            stringBuilder.Append(".. ");

            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            stringBuilder.Append("] ");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MultiplicityRange
        /// <para>MultiplicityRange='['(ownedRelationship+=MultiplicityExpressionMember'..')?ownedRelationship+=MultiplicityExpressionMember']'</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Multiplicities.IMultiplicityRange" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMultiplicityRange(SysML2.NET.Core.POCO.Kernel.Multiplicities.IMultiplicityRange poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append("[ ");
            // Group Element
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            stringBuilder.Append(".. ");

            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            stringBuilder.Append("] ");

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
