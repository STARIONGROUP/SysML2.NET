// ------------------------------------------------------------------------------------------------
// <copyright file="GraphicalNotationSpecificationVisitorTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Tests.Grammar
{
    using System.IO;

    using Antlr4.Runtime;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Grammar;
    using SysML2.NET.CodeGenerator.Grammar.Model;

    [TestFixture]
    public class GraphicalNotationSpecificationVisitorTestFixture
    {
        [Test]
        public void VerifyCanParseGrammar()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "datamodel", "SysML-graphical-bnf.kgbnf");

            var parser = new kgbnfParser(new CommonTokenStream(new kgbnfLexer(CharStreams.fromPath(filePath))));
            var tree = parser.specification();
            var specification = (GraphicalNotationSpecification)new GraphicalNotationSpecificationVisitor().Visit(tree);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(parser.NumberOfSyntaxErrors, Is.Zero);
                Assert.That(specification.Rules, Is.Not.Empty);
            }
        }
    }
}
