// -------------------------------------------------------------------------------------------------
// <copyright file="RulesHelper.NoTargetRules.cs" company="Starion Group S.A.">
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

    using HandlebarsDotNet;

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.Grammar.Model;

    using uml4net.CommonStructure;
    using uml4net.Extensions;
    using uml4net.StructuredClassifiers;

    /// <summary>
    /// Handlers for rules with no explicit target element type
    /// </summary>
    public static partial class RulesHelper
    {
        /// <summary>
        /// Resolves the effective target class for a no-target rule by analyzing its assignments.
        /// </summary>
        /// <param name="rule">The <see cref="TextualNotationRule" /> to analyze</param>
        /// <param name="allRules">All available grammar rules</param>
        /// <param name="cacheSource">An <see cref="IClass" /> providing access to the UML model cache</param>
        /// <returns>The resolved <see cref="IClass" />, or null if not resolvable</returns>
        public static IClass ResolveNoTargetRuleEffectiveTarget(TextualNotationRule rule, IReadOnlyList<TextualNotationRule> allRules, IClass cacheSource)
        {
            ArgumentNullException.ThrowIfNull(rule);
            ArgumentNullException.ThrowIfNull(allRules);
            ArgumentNullException.ThrowIfNull(cacheSource);

            var properties = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var requiredSupertypes = new HashSet<string>(StringComparer.Ordinal);
            var visited = new HashSet<string>();
            CollectPropertiesInNoTargetScope(rule, allRules, properties, requiredSupertypes, visited);

            var candidateClasses = cacheSource.Cache.Values.OfType<IClass>().ToList();

            if (properties.Count == 0 && requiredSupertypes.Count == 0)
            {
                return candidateClasses.FirstOrDefault(x => x.Name == "Element");
            }

            var matchingClasses = candidateClasses
                .Where(c => properties.All(p => c.QueryAllProperties().Any(q => string.Equals(q.Name, p, StringComparison.OrdinalIgnoreCase))))
                .Where(c => requiredSupertypes.All(s => c.Name == s || c.QueryAllGeneralClassifiers().Any(g => g.Name == s)))
                .ToList();

            if (matchingClasses.Count == 0)
            {
                return null;
            }

            // Pick the most general (highest in the hierarchy) class that still owns every
            // required property AND derives from every required sub-rule target — that's the
            // broadest signature the shared builder can adopt so every caller can pass its poco
            // without a downcast. A class that is a general classifier of every other candidate
            // has the smallest ancestor count (closer to the root) and therefore appears first.
            return matchingClasses
                .OrderBy(c => c.QueryAllGeneralClassifiers().Count)
                .ThenBy(c => c.Name, StringComparer.Ordinal)
                .First();
        }

        /// <summary>
        /// Determines whether a no-target rule should be lifted into the shared builder class.
        /// Shared lifting applies to rules that:
        /// <list type="bullet">
        ///  <item>declare no target type,</item>
        ///  <item>do not share a name with any <see cref="IClass" /> in the UML model (handled by the per-class grouping path),</item>
        ///  <item>
        ///  are not purely lexical tokens (SysML convention: <c>ALL_CAPS</c> names such as <c>NAME</c>, <c>STRING_VALUE</c>
        ///  ),
        ///  </item>
        ///  <item>
        ///  assign at least one POCO property somewhere in their body — rules without any assignment (pure token-class groups like
        ///  <c>BinaryOperator</c>, <c>UnaryOperator</c>, <c>QualifiedName</c>) have no runtime state to drive and are instead handled by their call sites.
        ///  </item>
        /// </list>
        /// </summary>
        /// <param name="rule">The rule to test</param>
        /// <param name="cacheSource">Any <see cref="IClass" /> from the loaded model used to access <c>Cache</c></param>
        /// <returns><c>true</c> when the rule should be generated into the shared builder</returns>
        public static bool IsSharedNoTargetRule(TextualNotationRule rule, IClass cacheSource)
        {
            if (rule == null || cacheSource == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(rule.TargetElementName))
            {
                return false;
            }

            if (rule.RuleName.IsAllUpperSnake())
            {
                return false;
            }

            if (cacheSource.Cache.Values.OfType<INamedElement>().Any(x => x.Name == rule.RuleName))
            {
                return false;
            }

            return RuleHasAnyAssignment(rule);
        }

        /// <summary>
        /// Recursively walks a no-target rule and collects both the names of every property it
        /// assigns (directly or via any no-target sub-rule it references) and the names of every
        /// targeted sub-rule it calls directly (so the resolved effective target must derive from
        /// each of those targets). Targeted sub-rules are NOT descended into because their
        /// assignments belong to their own target scope, not the current scope.
        /// </summary>
        /// <param name="rule">The rule to inspect</param>
        /// <param name="allRules">All available rules for resolving sub-rule references</param>
        /// <param name="properties">The accumulated set of property names</param>
        /// <param name="requiredSupertypes">The accumulated set of class names that the effective target must derive from or equal</param>
        /// <param name="visited">Set of already-visited rule names (infinite-recursion guard)</param>
        private static void CollectPropertiesInNoTargetScope(TextualNotationRule rule, IReadOnlyList<TextualNotationRule> allRules, HashSet<string> properties, HashSet<string> requiredSupertypes, HashSet<string> visited)
        {
            if (!visited.Add(rule.RuleName))
            {
                return;
            }

            foreach (var alternative in rule.Alternatives)
            {
                CollectPropertiesInNoTargetScopeFromElements(alternative.Elements, allRules, properties, requiredSupertypes, visited);
            }
        }

        /// <summary>
        /// Recursively walks a list of <see cref="RuleElement" /> in the current no-target scope and
        /// collects every property name it assigns, as well as the target class name of every
        /// targeted sub-rule it calls directly. Does not descend into targeted sub-rules.
        /// </summary>
        /// <param name="elements">The elements to inspect</param>
        /// <param name="allRules">All available rules for resolving sub-rule references</param>
        /// <param name="properties">The accumulated set of property names</param>
        /// <param name="requiredSupertypes">The accumulated set of class names that the effective target must derive from or equal</param>
        /// <param name="visited">Set of already-visited rule names (infinite-recursion guard)</param>
        private static void CollectPropertiesInNoTargetScopeFromElements(IEnumerable<RuleElement> elements, IReadOnlyList<TextualNotationRule> allRules, HashSet<string> properties, HashSet<string> requiredSupertypes, HashSet<string> visited)
        {
            foreach (var element in elements)
            {
                switch (element)
                {
                    case AssignmentElement assignmentElement:
                        properties.Add(assignmentElement.Property);
                        break;
                    case NonParsingAssignmentElement nonParsingAssignment:
                        properties.Add(nonParsingAssignment.PropertyName);
                        break;
                    case NonTerminalElement nonTerminal:
                        var referenced = allRules.SingleOrDefault(x => x.RuleName == nonTerminal.Name);

                        if (referenced == null)
                        {
                            break;
                        }

                        if (string.IsNullOrWhiteSpace(referenced.TargetElementName))
                        {
                            // No-target sub-rule: its assignments apply to the caller's poco, so recurse.
                            CollectPropertiesInNoTargetScope(referenced, allRules, properties, requiredSupertypes, visited);
                        }
                        else
                        {
                            // Targeted sub-rule called directly on poco: the resolved effective target
                            // must be assignable to the sub-rule's target type.
                            requiredSupertypes.Add(referenced.TargetElementName);
                        }

                        break;
                    case GroupElement group:
                        foreach (var groupAlternative in group.Alternatives)
                        {
                            CollectPropertiesInNoTargetScopeFromElements(groupAlternative.Elements, allRules, properties, requiredSupertypes, visited);
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Determines whether a rule — directly or via any nested <see cref="GroupElement" /> — contains
        /// at least one <see cref="AssignmentElement" /> or <see cref="NonParsingAssignmentElement" />.
        /// Rules that contain no assignment at all are "token-class" dispatchers whose entire body is
        /// a set of terminal/NonTerminal alternatives (e.g. <c>BinaryOperator</c>, <c>UnaryOperator</c>,
        /// <c>QualifiedName</c>); they have no runtime state to serialize and are left to each caller
        /// to emit from its own POCO.
        /// </summary>
        /// <param name="rule">The rule to inspect</param>
        /// <returns><c>true</c> if any assignment appears in the rule body</returns>
        private static bool RuleHasAnyAssignment(TextualNotationRule rule)
        {
            foreach (var alternative in rule.Alternatives)
            {
                if (ContainsAnyAssignment(alternative.Elements))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Recursively tests whether a list of <see cref="RuleElement" /> contains any
        /// <see cref="AssignmentElement" /> or <see cref="NonParsingAssignmentElement" />, descending
        /// through <see cref="GroupElement" /> boundaries but NOT through <see cref="NonTerminalElement" />
        /// references (which live in their own rule scope).
        /// </summary>
        /// <param name="elements">The elements to inspect</param>
        /// <returns><c>true</c> if any assignment is found</returns>
        private static bool ContainsAnyAssignment(IEnumerable<RuleElement> elements)
        {
            foreach (var element in elements)
            {
                switch (element)
                {
                    case AssignmentElement:
                    case NonParsingAssignmentElement:
                        return true;
                    case GroupElement group:
                        foreach (var groupAlternative in group.Alternatives)
                        {
                            if (ContainsAnyAssignment(groupAlternative.Elements))
                            {
                                return true;
                            }
                        }

                        break;
                }
            }

            return false;
        }

        /// <summary>
        /// Emits a call to the shared no-target rule builder. When the caller's <paramref name="umlClass" />
        /// does not already derive from the rule's effective target, an explicit cast is emitted.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The caller's <see cref="IClass" /></param>
        /// <param name="nonTerminalElement">The <see cref="NonTerminalElement" /> being processed</param>
        /// <param name="referencedRule">The referenced shared no-target <see cref="TextualNotationRule" /></param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        private static void EmitSharedNoTargetRuleCall(EncodedTextWriter writer, IClass umlClass, NonTerminalElement nonTerminalElement, TextualNotationRule referencedRule, RuleGenerationContext ruleGenerationContext)
        {
            var effectiveTarget = ResolveNoTargetRuleEffectiveTarget(referencedRule, ruleGenerationContext.AllRules, umlClass);

            string variableExpression;

            if (effectiveTarget == null || effectiveTarget == umlClass || umlClass.QueryAllGeneralClassifiers().Contains(effectiveTarget))
            {
                variableExpression = ruleGenerationContext.CurrentVariableName;
            }
            else
            {
                variableExpression = $"({effectiveTarget.QueryFullyQualifiedTypeName()}){ruleGenerationContext.CurrentVariableName}";
            }

            var emittedCondition = effectiveTarget != null
                                   && TryEmitOptionalCondition(writer, nonTerminalElement, referencedRule, effectiveTarget, ruleGenerationContext, ruleGenerationContext.CurrentVariableName);

            writer.WriteSafeString($"{SharedBuilderClassName}.Build{nonTerminalElement.Name}({variableExpression}, cursorCache, stringBuilder);");

            if (emittedCondition)
            {
                writer.WriteSafeString($"{Environment.NewLine}}}");
            }
        }
    }
}
