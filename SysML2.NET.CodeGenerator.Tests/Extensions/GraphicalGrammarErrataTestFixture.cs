// ------------------------------------------------------------------------------------------------
// <copyright file="GraphicalGrammarErrataTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Tests.Extensions
{
    using System.IO;
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.Grammar;

    [TestFixture]
    public class GraphicalGrammarErrataTestFixture
    {
        [Test]
        public void VerifyQueryUnappliedErrata()
        {
            GrammarLoader.LoadGraphicalNotationSpecification(Path.Combine(TestContext.CurrentContext.TestDirectory, "datamodel", "SysML-graphical-bnf.kgbnf"));

            var unapplied = GraphicalGrammarErrata.QueryUnappliedErrata();

            Assert.That(unapplied, Is.Empty, $"Errata matched nothing in the graphical BNF: {string.Join(", ", unapplied.Select(erratum => erratum.RuleName))}");
        }
    }
}
