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
    using System.Linq;

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

            var (specification, syntaxErrorCount) = Parse(CharStreams.fromPath(filePath));
            var rules = specification.Rules;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(syntaxErrorCount, Is.Zero);
                Assert.That(rules, Is.Not.Empty);
                Assert.That(rules.Where(rule => rule.RuleName == "compartment").Select(rule => rule.IsPartial), Is.Not.Empty.And.All.True);
            }

            var element = rules.Single(rule => rule.RuleName == "element");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(element.IsPartial, Is.False);
                Assert.That(element.Alternatives, Has.Count.EqualTo(3));
                Assert.That(element.Alternatives[1].Elements[0], Is.TypeOf<GraphicalNonTerminalElement>().With.Property("Name").EqualTo("general-element"));
            }

            var generalCompartment = rules.Single(rule => rule.RuleName == "general-compartment").Alternatives[0].Elements;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(generalCompartment, Has.Count.EqualTo(2));
                Assert.That(generalCompartment[0], Is.TypeOf<GraphicalImageElement>().With.Property("Source").EqualTo("images/general-compartment.svg"));
                Assert.That(generalCompartment[1], Is.TypeOf<GraphicalNonTerminalElement>().With.Property("IsTextualReference").False);
            }

            Assert.That(rules.Single(rule => rule.RuleName == "expose-r").Alternatives[0].Elements[0],
                Is.TypeOf<GraphicalImageElement>().With.Property("Source").EqualTo("images/expose-r.svg"));

            var usageCompartmentElements = rules.Single(rule => rule.RuleName == "usage-cp").Alternatives[0].Elements;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(usageCompartmentElements[0], Is.TypeOf<GraphicalNonTerminalElement>().With.Property("IsTextualReference").True);
                Assert.That(usageCompartmentElements[1], Is.TypeOf<GraphicalNonTerminalElement>().With.Property("Name").EqualTo("ValuePart"));
                Assert.That(usageCompartmentElements[1].Suffix, Is.EqualTo("?"));
            }

            var naryDependency = rules.Single(rule => rule.RuleName == "n-ary-dependency").Alternatives[0].Elements;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(naryDependency[0], Is.TypeOf<GraphicalEdgeEndElement>().With.Property("Name").EqualTo("n-ary-association-dot"));
                Assert.That(naryDependency[1], Is.TypeOf<GraphicalGroupElement>().With.Property("Suffix").EqualTo("+"));
                Assert.That(((GraphicalGroupElement)naryDependency[1]).Alternatives[0].Elements[1], Is.TypeOf<GraphicalEdgeEndElement>());
            }

            Assert.That(rules.Single(rule => rule.RuleName == "ellipsis-at-lower-left-corner").Alternatives[0].Elements[0],
                Is.TypeOf<GraphicalTerminalElement>().With.Property("Value").EqualTo("..."));

            (_, syntaxErrorCount) = Parse(CharStreams.fromString("broken = ( element\n"));

            Assert.That(syntaxErrorCount, Is.GreaterThan(0));
        }

        private static (GraphicalNotationSpecification Specification, int SyntaxErrorCount) Parse(ICharStream stream)
        {
            var parser = new kgbnfParser(new CommonTokenStream(new kgbnfLexer(stream)));
            var tree = parser.specification();
            var specification = (GraphicalNotationSpecification)new GraphicalNotationSpecificationVisitor().Visit(tree);

            return (specification, parser.NumberOfSyntaxErrors);
        }
    }
}
