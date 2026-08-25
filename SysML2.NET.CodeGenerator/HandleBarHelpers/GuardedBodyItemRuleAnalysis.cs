// -------------------------------------------------------------------------------------------------
// <copyright file="GuardedBodyItemRuleAnalysis.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.HandleBarHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using SysML2.NET.CodeGenerator.Grammar.Model;

    /// <summary>
    /// Grammar-structural analysis that derives which body-item rules require the guarded loop form —
    /// an <c>IsValidFor{Rule}</c> predicate instead of a bare cursor null-test — replacing the former
    /// hand-maintained rule-name allowlist in <c>RuleProcessor.IsGuardedBodyItemRule</c>.
    /// </summary>
    /// <remarks>
    /// A loop over <c>X*</c> needs the guarded form when both hold:
    /// <list type="number">
    /// <item><description>the loop is brace-positioned (<c>'{' X* …</c>) — exactly the emission shapes
    /// (terminal-vs-body, optional body group) whose while-condition falls back to a bare
    /// <c>Current != null</c>. Loops elsewhere (e.g. <c>CalculationBodyPart</c>'s
    /// <c>CalculationBodyItem*</c>, <c>VariantReference</c>'s <c>FeatureSpecialization*</c>) are already
    /// bounded by a type-derived condition from <c>ResolveCollectionWhileTypeCondition</c> or the
    /// content-type guard, and</description></item>
    /// <item><description>a later element of the same production consumes the same collection property,
    /// so an unguarded loop would swallow it. The consumer can follow the loop directly
    /// (<c>CaseBody</c>'s trailing <c>( ownedRelationship += ResultExpressionMember )?</c>) or follow a
    /// bare non-terminal chain whose consumption tail is the loop (<c>PortDefinition</c>'s trailing
    /// <c>ownedRelationship += ConjugatedPortDefinitionMember</c> after
    /// <c>Definition → DefinitionBody → '{' DefinitionBodyItem* '}'</c>).</description></item>
    /// </list>
    /// <para>The set is then closed over bare dispatcher arms: when a guarded rule has a bare
    /// single-non-terminal alternative (<c>CaseBodyItem → CalculationBodyItem → ActionBodyItem</c>), the
    /// arm's builder runs against the same cursor population during the guarded loop, and the guarded
    /// rule's <c>IsValidFor</c> necessarily delegates to the arm's — so the arm's own loops take the
    /// guarded form too.</para>
    /// </remarks>
    public static class GuardedBodyItemRuleAnalysis
    {
        /// <summary>
        /// Memoizes computed guarded-rule sets per grammar. Keyed on the first rule OBJECT of the rule
        /// list: every <see cref="RuleGenerationContext" /> copies rule references from one master list,
        /// so the first rule's identity identifies the grammar across contexts.
        /// </summary>
        private static readonly ConditionalWeakTable<TextualNotationRule, HashSet<string>> ComputedSetsByFirstRule = new();

        /// <summary>
        /// Returns the guarded body-item rule names for <paramref name="allRules" />, computing them once
        /// per grammar and serving subsequent calls from the memo.
        /// </summary>
        /// <param name="allRules">All available <see cref="TextualNotationRule" /> of the merged grammar</param>
        /// <returns>The set of rule names whose loops require the guarded form</returns>
        public static HashSet<string> ComputeCached(IReadOnlyList<TextualNotationRule> allRules)
        {
            return allRules.Count == 0 ? [] : ComputedSetsByFirstRule.GetValue(allRules[0], _ => Compute(allRules));
        }

        /// <summary>
        /// Computes the guarded body-item rule names for <paramref name="allRules" /> from the grammar
        /// structure alone.
        /// </summary>
        /// <param name="allRules">All available <see cref="TextualNotationRule" /> of the merged grammar</param>
        /// <returns>The set of rule names whose loops require the guarded form</returns>
        public static HashSet<string> Compute(IReadOnlyList<TextualNotationRule> allRules)
        {
            var analysisContext = new AnalysisContext(allRules);
            var guardedRuleNames = new HashSet<string>(StringComparer.Ordinal);

            foreach (var alternative in allRules.SelectMany(rule => rule.Alternatives))
            {
                AnalyseSequence(alternative.Elements, analysisContext, guardedRuleNames);
            }

            CloseOverBareDispatcherArms(analysisContext, guardedRuleNames);

            return guardedRuleNames;
        }

        /// <summary>
        /// Scans one element sequence for brace-positioned loops (direct, or exposed as the consumption
        /// tail of a bare non-terminal chain) that are followed by a consumer of the same collection
        /// property, and records the threatened loop rules as guarded.
        /// </summary>
        /// <param name="elements">The sequence of <see cref="RuleElement" /> of one alternative</param>
        /// <param name="analysisContext">The <see cref="AnalysisContext" /> for rule lookups</param>
        /// <param name="guardedRuleNames">The accumulated set of guarded rule names</param>
        private static void AnalyseSequence(List<RuleElement> elements, AnalysisContext analysisContext, HashSet<string> guardedRuleNames)
        {
            for (var elementIndex = 0; elementIndex < elements.Count; elementIndex++)
            {
                var element = elements[elementIndex];

                if (element is GroupElement groupElement)
                {
                    foreach (var groupAlternative in groupElement.Alternatives)
                    {
                        AnalyseSequence(groupAlternative.Elements, analysisContext, guardedRuleNames);
                    }
                }

                var previousElement = elementIndex > 0 ? elements[elementIndex - 1] : null;
                var exposedLoops = CollectExposedLoops(element, previousElement, analysisContext, []);

                if (exposedLoops.Count == 0)
                {
                    continue;
                }

                var followingElements = elements.Skip(elementIndex + 1).ToList();

                foreach (var exposedLoop in exposedLoops.Where(loop => loop.ConsumedProperties
                    .Any(propertyName => followingElements.Any(followingElement => ConsumesProperty(followingElement, propertyName, analysisContext)))))
                {
                    guardedRuleNames.Add(exposedLoop.LoopRuleName);
                }
            }
        }

        /// <summary>
        /// Collects the brace-positioned loops that <paramref name="element" /> exposes to elements that
        /// follow it: a collection non-terminal directly preceded by <c>'{'</c> exposes itself; a bare
        /// non-terminal or group exposes the loops at the consumption tail of its rule tree.
        /// </summary>
        /// <param name="element">The <see cref="RuleElement" /> to inspect</param>
        /// <param name="previousElement">The element preceding <paramref name="element" /> in its sequence, or <see langword="null" /></param>
        /// <param name="analysisContext">The <see cref="AnalysisContext" /> for rule lookups</param>
        /// <param name="visitedRuleNames">Rule names already visited on this chain, to break reference cycles</param>
        /// <returns>The exposed loops with the collection properties their iterations consume</returns>
        private static List<(string LoopRuleName, IReadOnlyCollection<string> ConsumedProperties)> CollectExposedLoops(RuleElement element, RuleElement previousElement, AnalysisContext analysisContext, HashSet<string> visitedRuleNames)
        {
            switch (element)
            {
                case NonTerminalElement { IsCollection: true } loopNonTerminal:
                {
                    if (previousElement is not TerminalElement { Value: "{" })
                    {
                        return [];
                    }

                    var consumedProperties = analysisContext.GetCollectionPropertyNames(loopNonTerminal.Name);

                    return consumedProperties.Count > 0 ? [(loopNonTerminal.Name, consumedProperties)] : [];
                }

                case NonTerminalElement bareNonTerminal:
                {
                    var referencedRule = analysisContext.FindRule(bareNonTerminal.Name);

                    if (referencedRule == null || !visitedRuleNames.Add(referencedRule.RuleName))
                    {
                        return [];
                    }

                    return referencedRule.Alternatives
                        .SelectMany(alternative => CollectTailExposedLoops(alternative.Elements, analysisContext, visitedRuleNames))
                        .ToList();
                }

                case GroupElement groupElement:
                    return groupElement.Alternatives
                        .SelectMany(alternative => CollectTailExposedLoops(alternative.Elements, analysisContext, visitedRuleNames))
                        .ToList();

                default:
                    return [];
            }
        }

        /// <summary>
        /// Collects the loops exposed at the consumption tail of an element sequence by walking it
        /// backwards: cursor-irrelevant elements (terminals, scalar/boolean assignments) are transparent,
        /// optional cursor-consuming elements are collected and passed through (they may not consume at
        /// runtime), and the first mandatory cursor-consuming element ends the walk.
        /// </summary>
        /// <param name="elements">The sequence of <see cref="RuleElement" /> of one alternative</param>
        /// <param name="analysisContext">The <see cref="AnalysisContext" /> for rule lookups</param>
        /// <param name="visitedRuleNames">Rule names already visited on this chain, to break reference cycles</param>
        /// <returns>The exposed loops with the collection properties their iterations consume</returns>
        private static List<(string LoopRuleName, IReadOnlyCollection<string> ConsumedProperties)> CollectTailExposedLoops(List<RuleElement> elements, AnalysisContext analysisContext, HashSet<string> visitedRuleNames)
        {
            var exposedLoops = new List<(string LoopRuleName, IReadOnlyCollection<string> ConsumedProperties)>();

            for (var elementIndex = elements.Count - 1; elementIndex >= 0; elementIndex--)
            {
                var element = elements[elementIndex];

                if (!IsCursorRelevant(element, analysisContext))
                {
                    continue;
                }

                var previousElement = elementIndex > 0 ? elements[elementIndex - 1] : null;
                exposedLoops.AddRange(CollectExposedLoops(element, previousElement, analysisContext, visitedRuleNames));

                if (!element.IsOptional)
                {
                    break;
                }
            }

            return exposedLoops;
        }

        /// <summary>
        /// Extends the guarded set over bare dispatcher arms until a fixpoint: each bare
        /// single-non-terminal alternative of a guarded rule becomes guarded itself.
        /// </summary>
        /// <param name="analysisContext">The <see cref="AnalysisContext" /> for rule lookups</param>
        /// <param name="guardedRuleNames">The guarded rule names, extended in place</param>
        private static void CloseOverBareDispatcherArms(AnalysisContext analysisContext, HashSet<string> guardedRuleNames)
        {
            var pendingRuleNames = new Queue<string>(guardedRuleNames);

            while (pendingRuleNames.Count > 0)
            {
                var guardedRule = analysisContext.FindRule(pendingRuleNames.Dequeue());

                if (guardedRule == null)
                {
                    continue;
                }

                // The trailing Where filters on HashSet.Add, which returns true ONLY for a name not already
                // guarded — so the same call both records the name and selects the ones still to expand,
                // which is exactly the fixpoint condition. Safe as a filter despite mutating: the sequence
                // being enumerated is the rule's alternatives, not the set being written, and deferred
                // evaluation preserves the per-item ordering the equivalent if-body had.
                foreach (var newlyGuardedArmName in guardedRule.Alternatives
                    .Where(alternative => alternative.Elements.Count == 1)
                    .Select(alternative => alternative.Elements[0])
                    .OfType<NonTerminalElement>()
                    .Where(nonTerminal => !nonTerminal.IsCollection)
                    .Select(nonTerminal => nonTerminal.Name)
                    .Where(guardedRuleNames.Add))
                {
                    pendingRuleNames.Enqueue(newlyGuardedArmName);
                }
            }
        }

        /// <summary>
        /// Determines whether <paramref name="element" /> can consume cursor elements at all: a
        /// <c>+=</c> assignment, a non-terminal whose rule tree contains <c>+=</c> assignments, or a
        /// group containing either.
        /// </summary>
        /// <param name="element">The <see cref="RuleElement" /> to inspect</param>
        /// <param name="analysisContext">The <see cref="AnalysisContext" /> for rule lookups</param>
        /// <returns><see langword="true" /> when the element consumes from a cursor</returns>
        private static bool IsCursorRelevant(RuleElement element, AnalysisContext analysisContext)
        {
            return element switch
            {
                AssignmentElement assignmentElement => assignmentElement.Operator == "+=",
                NonTerminalElement nonTerminalElement => analysisContext.GetCollectionPropertyNames(nonTerminalElement.Name).Count > 0,
                GroupElement groupElement => groupElement.Alternatives.Any(alternative => alternative.Elements.Any(groupedElement => IsCursorRelevant(groupedElement, analysisContext))),
                _ => false,
            };
        }

        /// <summary>
        /// Determines whether <paramref name="element" /> consumes elements from the collection property
        /// <paramref name="propertyName" /> — directly via a <c>+=</c> assignment, or through a
        /// referenced rule tree or group that does.
        /// </summary>
        /// <param name="element">The <see cref="RuleElement" /> to inspect</param>
        /// <param name="propertyName">The collection property name to match</param>
        /// <param name="analysisContext">The <see cref="AnalysisContext" /> for rule lookups</param>
        /// <returns><see langword="true" /> when the element consumes from the named property</returns>
        private static bool ConsumesProperty(RuleElement element, string propertyName, AnalysisContext analysisContext)
        {
            return element switch
            {
                AssignmentElement assignmentElement => assignmentElement.Operator == "+=" && string.Equals(assignmentElement.Property, propertyName, StringComparison.OrdinalIgnoreCase),
                NonTerminalElement nonTerminalElement => analysisContext.GetCollectionPropertyNames(nonTerminalElement.Name).Contains(propertyName),
                GroupElement groupElement => groupElement.Alternatives.Any(alternative => alternative.Elements.Any(groupedElement => ConsumesProperty(groupedElement, propertyName, analysisContext))),
                _ => false,
            };
        }

        /// <summary>
        /// Rule lookup and memoization shared by one <see cref="Compute" /> run: rules indexed by name,
        /// and each rule's transitively consumed collection property names computed once.
        /// </summary>
        private sealed class AnalysisContext
        {
            /// <summary>
            /// The rules of the merged grammar, as passed to <see cref="Compute" />.
            /// </summary>
            private readonly IReadOnlyList<TextualNotationRule> allRules;

            /// <summary>
            /// The rules indexed by <see cref="TextualNotationRule.RuleName" />.
            /// </summary>
            private readonly Dictionary<string, TextualNotationRule> rulesByName;

            /// <summary>
            /// Memo of <see cref="TextualNotationRule.QueryCollectionPropertyNames" /> per rule name.
            /// </summary>
            private readonly Dictionary<string, IReadOnlyCollection<string>> collectionPropertyNamesByRuleName = [];

            /// <summary>
            /// Initializes a new instance of the <see cref="AnalysisContext" /> class.
            /// </summary>
            /// <param name="allRules">All available <see cref="TextualNotationRule" /> of the merged grammar</param>
            public AnalysisContext(IReadOnlyList<TextualNotationRule> allRules)
            {
                this.allRules = allRules;
                this.rulesByName = allRules.ToDictionary(rule => rule.RuleName, StringComparer.Ordinal);
            }

            /// <summary>
            /// Looks up a <see cref="TextualNotationRule" /> by name.
            /// </summary>
            /// <param name="ruleName">The grammar rule name to find</param>
            /// <returns>The matching rule, or <see langword="null" /> when the name resolves to no rule</returns>
            public TextualNotationRule FindRule(string ruleName)
            {
                return this.rulesByName.GetValueOrDefault(ruleName);
            }

            /// <summary>
            /// Returns the collection property names the named rule transitively consumes, computing them
            /// once per rule name.
            /// </summary>
            /// <param name="ruleName">The grammar rule name</param>
            /// <returns>The consumed collection property names; empty when the name resolves to no rule</returns>
            public IReadOnlyCollection<string> GetCollectionPropertyNames(string ruleName)
            {
                if (this.collectionPropertyNamesByRuleName.TryGetValue(ruleName, out var propertyNames))
                {
                    return propertyNames;
                }

                propertyNames = this.FindRule(ruleName)?.QueryCollectionPropertyNames(this.allRules) ?? [];
                this.collectionPropertyNamesByRuleName[ruleName] = propertyNames;

                return propertyNames;
            }
        }
    }
}
