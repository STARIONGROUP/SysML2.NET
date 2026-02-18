// -------------------------------------------------------------------------------------------------
// <copyright file="TextualNotationSpecificationVisitorTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Tests.Grammar
{
    using System;
    using System.IO;

    using Antlr4.Runtime;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Grammar;
    using SysML2.NET.CodeGenerator.Grammar.Model;

    [TestFixture]
    public class TextualNotationSpecificationVisitorTestFixture
    {
        [Test]
        [TestCase("KerML-textual-bnf.kebnf")]
        [TestCase("SysML-textual-bnf.kebnf")]
        public void VerifyCanParseGrammar(string modelName)
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "datamodel",modelName );

            var stream = CharStreams.fromPath(filePath);
            var lexer = new kebnfLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new kebnfParser(tokens);

            var tree = parser.specification();
            var explorer = new TextualNotationSpecificationVisitor();
            var result =  (TextualNotationSpecification)explorer.Visit(tree);
            var rules = result.Rules;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rules, Is.Not.Null);
                Assert.That(rules, Is.Not.Empty);
            }
            
            Console.WriteLine($"Found {rules.Count} rules");
        }
    }
}
