// -------------------------------------------------------------------------------------------------
// <copyright file="GrammarLoader.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Grammar
{
    using System.IO;

    using Antlr4.Runtime;

    using SysML2.NET.CodeGenerator.Grammar.Model;

    /// <summary>
    /// Provides direct access to <see cref="TextualNotationSpecification"/> by providing file path
    /// </summary>
    public static class GrammarLoader
    {
        /// <summary>
        /// Loads the <see cref="TextualNotationSpecification" /> 
        /// </summary>
        /// <param name="fileUri">The string uri that locates the KEBNF file to load</param>
        /// <returns>The loaded <see cref="TextualNotationSpecification"/></returns>
        /// <exception cref="FileNotFoundException">If the <paramref name="fileUri"/> does not locate an existing <see cref="File"/></exception>
        public static TextualNotationSpecification LoadTextualNotationSpecification(string fileUri)
        {
            if (!File.Exists(fileUri))
            {
                throw new FileNotFoundException("File not found", fileUri);
            }
            
            var stream = CharStreams.fromPath(fileUri);
            var lexer = new kebnfLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new kebnfParser(tokens);

            var tree = parser.specification();
            var explorer = new TextualNotationSpecificationVisitor();
            return (TextualNotationSpecification)explorer.Visit(tree);
        }
    }
}
