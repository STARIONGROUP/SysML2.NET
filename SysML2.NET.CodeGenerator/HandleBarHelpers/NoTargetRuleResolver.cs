// -------------------------------------------------------------------------------------------------
// <copyright file="NoTargetRuleResolver.cs" company="Starion Group S.A.">
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

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.Grammar.Model;

    using uml4net.CommonStructure;
    using uml4net.Extensions;
    using uml4net.StructuredClassifiers;

    /// <summary>
    /// Handles resolution and classification of grammar rules that have no explicit target element type
    /// </summary>
    internal static class NoTargetRuleResolver
    {
        /// <summary>
        /// Resolves the effective target class for a no-target rule by analyzing its assignments.
        /// </summary>
        /// <param name="rule">The <see cref="TextualNotationRule" /> to analyze</param>
        /// <param name="allRules">All available grammar rules</param>
        /// <param name="cacheSource">An <see cref="IClass" /> providing access to the UML model cache</param>
        /// <returns>The resolved <see cref="IClass" />, or null if not resolvable</returns>
        internal static IClass ResolveEffectiveTarget(TextualNotationRule rule, IReadOnlyList<TextualNotationRule> allRules, IClass cacheSource)
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

            return matchingClasses
                .OrderBy(c => c.QueryAllGeneralClassifiers().Count)
                .ThenBy(c => c.Name, StringComparer.Ordinal)
                .First();
        }

        /// <summary>
        /// Determines whether a no-target rule should be lifted into the shared builder class.
        /// </summary>
        /// <param name="rule">The rule to test</param>
        /// <param name="cacheSource">Any <see cref="IClass" /> from the loaded model used to access <c>Cache</c></param>
        /// <returns><c>true</c> when the rule should be generated into the shared builder</returns>
        internal static bool IsSharedRule(TextualNotationRule rule, IClass cacheSource)
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
        /// Recursively walks a no-target rule and collects property names and required supertypes.
        /// </summary>
        /// <param name="rule">The rule to inspect</param>
        /// <param name="allRules">All available rules for resolving sub-rule references</param>
        /// <param name="properties">The accumulated set of property names</param>
        /// <param name="requiredSupertypes">The accumulated set of class names that the effective target must derive from or equal</param>
        /// <param name="visited">Set of already-visited rule names (infinite-recursion guard)</param>
        internal static void CollectPropertiesInNoTargetScope(TextualNotationRule rule, IReadOnlyList<TextualNotationRule> allRules, HashSet<string> properties, HashSet<string> requiredSupertypes, HashSet<string> visited)
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
        /// Recursively walks a list of <see cref="RuleElement" /> in the current no-target scope.
        /// </summary>
        /// <param name="elements">The elements to inspect</param>
        /// <param name="allRules">All available rules for resolving sub-rule references</param>
        /// <param name="properties">The accumulated set of property names</param>
        /// <param name="requiredSupertypes">The accumulated set of class names that the effective target must derive from or equal</param>
        /// <param name="visited">Set of already-visited rule names (infinite-recursion guard)</param>
        internal static void CollectPropertiesInNoTargetScopeFromElements(IEnumerable<RuleElement> elements, IReadOnlyList<TextualNotationRule> allRules, HashSet<string> properties, HashSet<string> requiredSupertypes, HashSet<string> visited)
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
                            CollectPropertiesInNoTargetScope(referenced, allRules, properties, requiredSupertypes, visited);
                        }
                        else
                        {
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
        /// Determines whether a rule contains at least one assignment.
        /// </summary>
        /// <param name="rule">The rule to inspect</param>
        /// <returns><c>true</c> if any assignment appears in the rule body</returns>
        internal static bool RuleHasAnyAssignment(TextualNotationRule rule)
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
        /// Recursively tests whether a list of <see cref="RuleElement" /> contains any assignment.
        /// </summary>
        /// <param name="elements">The elements to inspect</param>
        /// <returns><c>true</c> if any assignment is found</returns>
        internal static bool ContainsAnyAssignment(IEnumerable<RuleElement> elements)
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
    }
}
