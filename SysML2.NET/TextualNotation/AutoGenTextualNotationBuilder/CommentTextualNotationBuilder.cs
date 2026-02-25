// -------------------------------------------------------------------------------------------------
// <copyright file="CommentTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="CommentTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Root.Annotations.IComment" /> element
    /// </summary>
    public static partial class CommentTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule Comment
        /// <para>Comment=('comment'Identification('about'ownedRelationship+=Annotation(','ownedRelationship+=Annotation)*)?)?('locale'locale=STRING_VALUE)?body=REGULAR_COMMENT</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Annotations.IComment" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildComment(SysML2.NET.Core.POCO.Root.Annotations.IComment poco, StringBuilder stringBuilder)
        {
            // Group Element
            stringBuilder.Append("comment ");
            // non Terminal : Identification; Found rule Identification:Element=('<'declaredShortName=NAME'>')?(declaredName=NAME)? 
            ElementTextualNotationBuilder.BuildIdentification(poco, stringBuilder);
            // Group Element
            stringBuilder.Append("about ");
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // Group Element
            stringBuilder.Append(", ");
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement



            // Group Element
            stringBuilder.Append("locale ");
            // Assignment Element : locale = SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property locale value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement

            // Assignment Element : body = SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property body value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
