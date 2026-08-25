// -------------------------------------------------------------------------------------------------
// <copyright file="GrammarErrataTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Tests.Extensions
{
    using System.IO;
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.Grammar;

    [TestFixture]
    public class GrammarErrataTestFixture
    {
        /// <summary>
        /// The production the <c>CaseBodyItem</c> erratum corrects, quoted exactly as the grammar carries it.
        /// </summary>
        private const string CaseBodyItemOriginal = "CaseBodyItem : Type =\r\n      ActionBodyItem";

        [Test]
        public void VerifyApplyProductions()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(GrammarErrata.ApplyProductions(null), Is.Null);
                Assert.That(GrammarErrata.ApplyProductions(string.Empty), Is.Empty);
                Assert.That(GrammarErrata.ApplyProductions("   "), Is.EqualTo("   "));
            }

            // A grammar carrying none of the corrected productions keeps its content; only line endings
            // are normalised, so the correction layer behaves identically on every platform.
            const string unrelated = "Foo : Bar =\r\n      Baz";

            Assert.That(GrammarErrata.ApplyProductions(unrelated), Is.EqualTo("Foo : Bar =\n      Baz"));

            var corrected = GrammarErrata.ApplyProductions(CaseBodyItemOriginal);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(corrected, Is.Not.EqualTo(CaseBodyItemOriginal));
                Assert.That(corrected, Does.Contain("CalculationBodyItem"));
                Assert.That(corrected, Does.Not.Contain("      ActionBodyItem"));

                // Re-applying a correction to already-corrected text is a no-op: the Original stops matching.
                Assert.That(GrammarErrata.ApplyProductions(corrected), Is.EqualTo(corrected));
            }

            // The correction is applied verbatim wherever it appears, leaving surrounding text intact.
            var embedded = GrammarErrata.ApplyProductions($"// leading\r\n{CaseBodyItemOriginal}\r\n// trailing");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(embedded, Does.StartWith("// leading"));
                Assert.That(embedded, Does.EndWith("// trailing"));
                Assert.That(embedded, Does.Contain("CalculationBodyItem"));
            }
        }

        /// <summary>
        /// Pins the line-ending independence of the production corrections. A multi-line <c>Original</c>
        /// used to be written with <c>\r\n</c>, so it matched a CRLF working tree (Windows, with
        /// <c>core.autocrlf=true</c>) and matched NOTHING on a LF checkout (Linux CI) — the same commit
        /// then generated different builders on the two platforms, and the divergence surfaced only as an
        /// unrelated downstream test failure.
        /// </summary>
        [Test]
        public void VerifyApplyProductionsIsLineEndingIndependent()
        {
            var appliedToCrLf = GrammarErrata.ApplyProductions("// leading\r\nCaseBodyItem : Type =\r\n      ActionBodyItem\r\n// trailing");
            var appliedToLf = GrammarErrata.ApplyProductions("// leading\nCaseBodyItem : Type =\n      ActionBodyItem\n// trailing");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(appliedToCrLf, Is.EqualTo(appliedToLf),
                    "The same grammar must correct identically whether it was checked out with CRLF or LF endings.");
                Assert.That(appliedToLf, Does.Contain("CalculationBodyItem"),
                    "The CaseBodyItem correction must apply to a LF checkout — this is the case that silently no-opped on Linux CI.");
                Assert.That(appliedToCrLf, Does.Contain("CalculationBodyItem"),
                    "The CaseBodyItem correction must apply to a CRLF checkout.");
            }
        }

        /// <summary>
        /// Asserts that every recorded erratum still matches the grammar it corrects, by loading the real
        /// KEBNF files through the production loader and then querying what stayed unapplied.
        /// </summary>
        /// <remarks>
        /// An erratum that matches nothing is silently inert — the generator only writes a console note
        /// (<c>UmlCoreTextualNotationBuilderGenerator</c>), so nothing fails and the missing correction shows
        /// up much later as wrong generated code. Two causes are both worth catching here: OMG fixed the
        /// defect upstream and the entry should be pruned, or the entry stopped matching for a mechanical
        /// reason such as line endings.
        /// <para><c>AppliedRuleNames</c> is static and accumulates across the run, so this assertion is
        /// order-independent: earlier fixtures can only ever mark MORE entries applied, never fewer.</para>
        /// </remarks>
        [Test]
        public void VerifyEveryErratumStillMatchesTheGrammar()
        {
            var textualRulesFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "datamodel");

            GrammarLoader.LoadTextualNotationSpecification(Path.Combine(textualRulesFolder, "KerML-textual-bnf.kebnf"));
            GrammarLoader.LoadTextualNotationSpecification(Path.Combine(textualRulesFolder, "SysML-textual-bnf.kebnf"));

            var unapplied = GrammarErrata.QueryUnappliedErrata();

            Assert.That(unapplied, Is.Empty,
                $"Erratum/errata matched nothing against the real grammar and are silently inert: {string.Join(", ", unapplied.Select(erratum => erratum.RuleName))}");
        }

        [Test]
        public void VerifyQueryUnappliedErrata()
        {
            // ApplyProductions above marks the CaseBodyItem entry applied, so whatever remains must be
            // reportable: every stale entry has to carry the rule name and the reason it was recorded.
            var unapplied = GrammarErrata.QueryUnappliedErrata();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(unapplied, Is.Not.Null);
                Assert.That(unapplied.All(erratum => !string.IsNullOrWhiteSpace(erratum.RuleName)), Is.True);
                Assert.That(unapplied.All(erratum => !string.IsNullOrWhiteSpace(erratum.Justification)), Is.True);
            }
        }

        [Test]
        public void VerifyApplyTarget()
        {
            using (Assert.EnterMultipleScope())
            {
                // A target the grammar states itself always wins — an erratum only fills a gap.
                Assert.That(GrammarErrata.ApplyTarget("LiteralReal", "AlreadyStated"), Is.EqualTo("AlreadyStated"));

                // A rule with no erratum keeps its (absent) target.
                Assert.That(GrammarErrata.ApplyTarget("NoSuchRule", null), Is.Null);
                Assert.That(GrammarErrata.ApplyTarget(null, null), Is.Null);
                Assert.That(GrammarErrata.ApplyTarget("   ", null), Is.Null);

                // The KEBNF names this rule LiteralReal, but the metaclass is LiteralRational.
                Assert.That(GrammarErrata.ApplyTarget("LiteralReal", null), Is.EqualTo("LiteralRational"));
            }
        }
    }
}
