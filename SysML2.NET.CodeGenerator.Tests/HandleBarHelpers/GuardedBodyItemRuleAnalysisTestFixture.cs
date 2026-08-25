// -------------------------------------------------------------------------------------------------
// <copyright file="GuardedBodyItemRuleAnalysisTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Tests.HandleBarHelpers
{
    using System;
    using System.IO;
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Grammar;
    using SysML2.NET.CodeGenerator.Grammar.Model;
    using SysML2.NET.CodeGenerator.HandleBarHelpers;

    /// <summary>
    /// Test fixture for the <see cref="GuardedBodyItemRuleAnalysis" /> class
    /// </summary>
    [TestFixture]
    public class GuardedBodyItemRuleAnalysisTestFixture
    {
        /// <summary>
        /// The merged KerML + SysML rule set, SysML rules taking precedence by name, exactly as the
        /// textual notation builder generator merges them.
        /// </summary>
        private TextualNotationSpecification textualNotationSpecification;

        /// <summary>
        /// Loads and merges the KerML and SysML KEBNF grammars.
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var textualRulesFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "datamodel");
            var kermlRules = GrammarLoader.LoadTextualNotationSpecification(Path.Combine(textualRulesFolder, "KerML-textual-bnf.kebnf"));
            var sysmlRules = GrammarLoader.LoadTextualNotationSpecification(Path.Combine(textualRulesFolder, "SysML-textual-bnf.kebnf"));

            var combinedRules = new TextualNotationSpecification();
            combinedRules.Rules.AddRange(sysmlRules.Rules);

            foreach (var rule in kermlRules.Rules.Where(rule => combinedRules.Rules.All(existingRule => existingRule.RuleName != rule.RuleName)))
            {
                combinedRules.Rules.Add(rule);
            }

            this.textualNotationSpecification = combinedRules;
        }

        /// <summary>
        /// Calibrates the structural predicate against the <c>IsGuardedBodyItemRule</c> allowlist it was
        /// meant to replace, pinning the measured relationship between the two rather than an equivalence
        /// the grammar cannot support.
        /// </summary>
        /// <remarks>
        /// The predicate models the TRAILING-CONSUMER hazard: a brace-positioned <c>X*</c> loop followed by
        /// a further consumer of the same cursor. That is real — <c>CaseBodyItem</c> (the rule's own
        /// <c>( ResultExpressionMember )?</c>) and <c>DefinitionBodyItem</c> (<c>PortDefinition</c>'s
        /// trailing <c>ConjugatedPortDefinitionMember</c>) are both found — and it also finds the two
        /// dispatcher arms those rules delegate to.
        /// <para>It does NOT reproduce the allowlist, and cannot: <c>InterfaceBodyItem</c> is correctly
        /// absent, because <c>InterfaceBody</c> is the last element of both <c>InterfaceDefinition</c> and
        /// <c>InterfaceUsage</c> and so has no trailing consumer at all. Its guard is nonetheless
        /// load-bearing for the SECOND hazard the allowlist encodes — the item dispatcher declines an
        /// unmatched element without advancing the cursor, so an unguarded loop spins — which is a runtime
        /// property of the hand-coded dispatcher, not a grammar property this analysis can see.</para>
        /// </remarks>
        [Test]
        public void VerifyCompute()
        {
            var guardedRuleNames = GuardedBodyItemRuleAnalysis.Compute(this.textualNotationSpecification.Rules);

            Console.WriteLine($"Computed guarded body-item rules ({guardedRuleNames.Count}): {string.Join(", ", guardedRuleNames.OrderBy(name => name, StringComparer.Ordinal))}");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(guardedRuleNames, Does.Contain("CaseBodyItem"),
                    "CaseBody's `'{' CaseBodyItem* ( ownedRelationship += ResultExpressionMember )? '}'` is the canonical trailing-consumer threat and must be detected.");
                Assert.That(guardedRuleNames, Does.Contain("DefinitionBodyItem"),
                    "PortDefinition's trailing `ownedRelationship += ConjugatedPortDefinitionMember` threatens the DefinitionBodyItem loop reached through Definition -> DefinitionBody.");
                Assert.That(guardedRuleNames, Does.Contain("ActionBodyItem"),
                    "ActionBodyItem is reached as a bare dispatcher arm of the threatened CaseBodyItem -> CalculationBodyItem chain.");
                Assert.That(guardedRuleNames, Does.Contain("CalculationBodyItem"),
                    "CalculationBodyItem is the bare dispatcher arm between CaseBodyItem and ActionBodyItem and shares their cursor population.");
                Assert.That(guardedRuleNames, Does.Not.Contain("InterfaceBodyItem"),
                    "InterfaceBody is the last element of both InterfaceDefinition and InterfaceUsage, so the trailing-consumer analysis must report no threat — its guard covers the separate non-advancing-dispatcher hazard instead.");
            }
        }
    }
}
