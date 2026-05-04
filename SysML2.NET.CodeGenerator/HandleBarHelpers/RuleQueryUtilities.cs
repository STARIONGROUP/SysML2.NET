// -------------------------------------------------------------------------------------------------
// <copyright file="RuleQueryUtilities.cs" company="Starion Group S.A.">
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

    using SysML2.NET.CodeGenerator.Grammar.Model;

    using uml4net;
    using uml4net.CommonStructure;
    using uml4net.Extensions;
    using uml4net.StructuredClassifiers;

    /// <summary>
    /// Static utility query methods for rule inspection and element analysis
    /// </summary>
    internal static class RuleQueryUtilities
    {
        /// <summary>
        /// Determines whether a grammar rule represents an empty membership shape.
        /// </summary>
        /// <param name="ruleName">The non-terminal name to test</param>
        /// <param name="allRules">All available rules for lookup</param>
        /// <returns>True if the rule conforms to the empty-membership shape</returns>
        internal static bool IsEmptyMembershipRule(string ruleName, IReadOnlyList<TextualNotationRule> allRules)
        {
            if (string.IsNullOrEmpty(ruleName) || !ruleName.StartsWith("Empty", StringComparison.Ordinal))
            {
                return false;
            }

            var rule = allRules.SingleOrDefault(x => x.RuleName == ruleName);

            if (rule == null)
            {
                return false;
            }

            foreach (var alternative in rule.Alternatives)
            {
                foreach (var element in alternative.Elements)
                {
                    if (element is AssignmentElement { Value: NonTerminalElement valueNonTerminal }
                        && valueNonTerminal.Name.StartsWith("Empty", StringComparison.Ordinal))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Resolves the UML <see cref="IClass" /> targeted by a non-terminal rule reference.
        /// </summary>
        /// <param name="nonTerminal">The <see cref="NonTerminalElement" /> whose target class is sought</param>
        /// <param name="cache">The <see cref="IXmiElementCache" /> used to look up the class</param>
        /// <param name="allRules">All available rules</param>
        /// <returns>The resolved <see cref="IClass" />, or <c>null</c> if not found</returns>
        internal static IClass ResolveRuleTargetClass(NonTerminalElement nonTerminal, IXmiElementCache cache, IReadOnlyList<TextualNotationRule> allRules)
        {
            var rule = allRules.SingleOrDefault(x => x.RuleName == nonTerminal.Name);

            var typeTarget = rule != null
                ? rule.EffectiveTarget
                : nonTerminal.Name;

            return FindClass(cache, typeTarget);
        }

        /// <summary>
        /// Looks up a UML <see cref="IClass" /> by name from the XMI element cache.
        /// </summary>
        /// <param name="cache">The <see cref="IXmiElementCache" /> to search</param>
        /// <param name="typeName">The UML class name to find</param>
        /// <returns>The matching <see cref="IClass" />, or <c>null</c> if not found</returns>
        internal static IClass FindClass(IXmiElementCache cache, string typeName)
        {
            return cache.Values.OfType<INamedElement>().SingleOrDefault(x => x.Name == typeName) as IClass;
        }

        /// <summary>
        /// Looks up a UML <see cref="INamedElement" /> by name from the XMI element cache.
        /// </summary>
        /// <param name="cache">The <see cref="IXmiElementCache" /> to search</param>
        /// <param name="typeName">The element name to find</param>
        /// <returns>The matching <see cref="INamedElement" />, or <c>null</c> if not found</returns>
        internal static INamedElement FindNamedElement(IXmiElementCache cache, string typeName)
        {
            return cache.Values.OfType<INamedElement>().SingleOrDefault(x => x.Name == typeName);
        }

        /// <summary>
        /// Extracts all boolean property names assigned via the conditional operator from a grammar rule.
        /// </summary>
        /// <param name="rule">The <see cref="TextualNotationRule" /> to inspect</param>
        /// <param name="allRules">All available rules for recursive lookup</param>
        /// <returns>A list of boolean property names</returns>
        internal static List<string> QueryBooleanAssignmentProperties(TextualNotationRule rule, IReadOnlyList<TextualNotationRule> allRules)
        {
            var result = new List<string>();
            CollectBooleanAssignmentProperties(rule.Alternatives.SelectMany(x => x.Elements).ToList(), allRules, result, new HashSet<string>());
            return result;
        }

        /// <summary>
        /// Recursively collects boolean <c>?=</c> assignment property names from a list of <see cref="RuleElement" />
        /// </summary>
        /// <param name="elements">The elements to inspect</param>
        /// <param name="allRules">All available rules for resolving NonTerminal references</param>
        /// <param name="result">The accumulated list of boolean property names</param>
        /// <param name="visited">Set of already-visited rule names to prevent infinite recursion</param>
        internal static void CollectBooleanAssignmentProperties(IReadOnlyList<RuleElement> elements, IReadOnlyList<TextualNotationRule> allRules, List<string> result, HashSet<string> visited)
        {
            foreach (var element in elements)
            {
                switch (element)
                {
                    case AssignmentElement { Operator: "?=" } assignment:
                        result.Add(assignment.Property);
                        break;
                    case GroupElement groupElement:
                        foreach (var groupAlternative in groupElement.Alternatives)
                        {
                            CollectBooleanAssignmentProperties(groupAlternative.Elements, allRules, result, visited);
                        }

                        break;
                    case NonTerminalElement nonTerminal:
                        var referencedRule = allRules.SingleOrDefault(x => x.RuleName == nonTerminal.Name);

                        if (referencedRule != null && visited.Add(referencedRule.RuleName))
                        {
                            CollectBooleanAssignmentProperties(referencedRule.Alternatives.SelectMany(x => x.Elements).ToList(), allRules, result, visited);
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Orders a collection of <see cref="NonTerminalElement" /> based on the inheritance ordering, to build switch expression
        /// </summary>
        /// <param name="nonTerminalElements">The collection of <see cref="NonTerminalElement" /> to order</param>
        /// <param name="cache">The <see cref="IXmiElementCache" /></param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <returns>
        /// The collection of ordered <see cref="NonTerminalElement" /> with the associated <see cref="IClass" />
        /// </returns>
        internal static List<(NonTerminalElement RuleElement, IClass UmlClass)> OrderElementsByInheritance(List<NonTerminalElement> nonTerminalElements, IXmiElementCache cache, RuleGenerationContext ruleGenerationContext)
        {
            var mapping = new List<(NonTerminalElement RuleElement, IClass UmlClass)>();
            var elementClass = cache.Values.Single(x => x is IClass { Name: "Element" });

            foreach (var nonTerminalElement in nonTerminalElements)
            {
                var referencedRule = ruleGenerationContext.AllRules.Single(x => x.RuleName == nonTerminalElement.Name);
                var referencedClassName = referencedRule.EffectiveTarget;
                var referencedClass = (IClass)(cache.Values.SingleOrDefault(x => x is IClass umlClass && umlClass.Name == referencedClassName) ?? elementClass);
                mapping.Add((nonTerminalElement, referencedClass));
            }

            // Capture original indices for stable tie-breaking before sorting
            var originalIndices = mapping.Select((item, itemIndex) => (item.RuleElement, itemIndex))
                .ToDictionary(x => x.RuleElement, x => x.itemIndex);

            mapping.Sort((a, b) =>
            {
                // Push the current class to the end (used as default case)
                var aIsNamed = a.UmlClass == ruleGenerationContext.NamedElementToGenerate;
                var bIsNamed = b.UmlClass == ruleGenerationContext.NamedElementToGenerate;

                if (aIsNamed && !bIsNamed)
                {
                    return 1;
                }

                if (bIsNamed && !aIsNamed)
                {
                    return -1;
                }

                if (a.UmlClass == b.UmlClass)
                {
                    return 0;
                }

                // Sort by inheritance depth (more specific types first).
                var depthA = a.UmlClass.QueryAllGeneralClassifiers().Count();
                var depthB = b.UmlClass.QueryAllGeneralClassifiers().Count();

                if (depthA != depthB)
                {
                    return depthB.CompareTo(depthA);
                }

                // Same depth: preserve original grammar order for stability
                return originalIndices[a.RuleElement].CompareTo(originalIndices[b.RuleElement]);
            });

            return mapping;
        }
    }
}
