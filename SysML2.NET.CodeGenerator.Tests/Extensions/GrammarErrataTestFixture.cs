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
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Extensions;

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

            // A grammar carrying none of the corrected productions is returned untouched.
            const string unrelated = "Foo : Bar =\r\n      Baz";

            Assert.That(GrammarErrata.ApplyProductions(unrelated), Is.EqualTo(unrelated));

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
