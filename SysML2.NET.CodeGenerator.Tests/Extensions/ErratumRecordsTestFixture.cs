// -------------------------------------------------------------------------------------------------
// <copyright file="ErratumRecordsTestFixture.cs" company="Starion Group S.A.">
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
    using System;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Extensions;

    /// <summary>
    /// Covers the records that carry a correction or invariant, all of which refuse to be constructed
    /// without a justification — the property that keeps the tables auditable.
    /// </summary>
    [TestFixture]
    public class ErratumRecordsTestFixture
    {
        [Test]
        public void VerifyOclErratum()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => new OclErratum(null, "replacement", "justification"), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new OclErratum("   ", "replacement", "justification"), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new OclErratum("original", null, "justification"), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new OclErratum("original", "   ", "justification"), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new OclErratum("original", "replacement", null), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new OclErratum("original", "replacement", "   "), Throws.TypeOf<ArgumentException>());
            }

            var erratum = new OclErratum("original", "replacement", "justification");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(erratum.Original, Is.EqualTo("original"));
                Assert.That(erratum.Replacement, Is.EqualTo("replacement"));
                Assert.That(erratum.Justification, Is.EqualTo("justification"));
            }
        }

        [Test]
        public void VerifyGrammarErratum()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => new GrammarErratum(null, "target", "justification"), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new GrammarErratum("   ", "target", "justification"), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new GrammarErratum("rule", null, "justification"), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new GrammarErratum("rule", "   ", "justification"), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new GrammarErratum("rule", "target", null), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new GrammarErratum("rule", "target", "   "), Throws.TypeOf<ArgumentException>());
            }

            var erratum = new GrammarErratum("rule", "target", "justification");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(erratum.RuleName, Is.EqualTo("rule"));
                Assert.That(erratum.TargetElementName, Is.EqualTo("target"));
                Assert.That(erratum.Justification, Is.EqualTo("justification"));
            }
        }

        [Test]
        public void VerifyGrammarProductionErratum()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => new GrammarProductionErratum(null, "original", "replacement", "justification"), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new GrammarProductionErratum("   ", "original", "replacement", "justification"), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new GrammarProductionErratum("rule", null, "replacement", "justification"), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new GrammarProductionErratum("rule", "   ", "replacement", "justification"), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new GrammarProductionErratum("rule", "original", null, "justification"), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new GrammarProductionErratum("rule", "original", "   ", "justification"), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new GrammarProductionErratum("rule", "original", "replacement", null), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new GrammarProductionErratum("rule", "original", "replacement", "   "), Throws.TypeOf<ArgumentException>());
            }

            var erratum = new GrammarProductionErratum("rule", "original", "replacement", "justification");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(erratum.RuleName, Is.EqualTo("rule"));
                Assert.That(erratum.Original, Is.EqualTo("original"));
                Assert.That(erratum.Replacement, Is.EqualTo("replacement"));
                Assert.That(erratum.Justification, Is.EqualTo("justification"));
            }
        }

        [Test]
        public void VerifyNotationInvariant()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => new NotationInvariant(null, "metamodelName", "justification"), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new NotationInvariant("   ", "metamodelName", "justification"), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new NotationInvariant("name", null, "justification"), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new NotationInvariant("name", "   ", "justification"), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new NotationInvariant("name", "metamodelName", null), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new NotationInvariant("name", "metamodelName", "   "), Throws.TypeOf<ArgumentException>());
            }

            var invariant = new NotationInvariant("name", "metamodelName", "justification");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(invariant.Name, Is.EqualTo("name"));
                Assert.That(invariant.MetamodelName, Is.EqualTo("metamodelName"));
                Assert.That(invariant.Justification, Is.EqualTo("justification"));
            }
        }
    }
}
