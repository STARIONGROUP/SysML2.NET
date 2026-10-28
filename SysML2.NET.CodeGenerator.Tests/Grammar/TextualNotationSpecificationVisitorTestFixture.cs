// ------------------------------------------------------------------------------------------------
// <copyright file="TextualNotationSpecificationVisitorTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Tests.Grammar
{
    using System;
    using System.IO;
    using System.Linq;

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
                Assert.That(rules.DistinctBy(x => x.RuleName), Is.EquivalentTo(rules));
            }
            
            Console.WriteLine($"Found {rules.Count} rules");
        }
    }
}
