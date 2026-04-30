// -------------------------------------------------------------------------------------------------
// <copyright file="RulesHelper.CollectionProcessing.cs" company="Starion Group S.A.">
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
    /// Collection loop emission and type-guard condition resolution
    /// </summary>
    public static partial class RulesHelper
    {
        /// <summary>
        /// Emits a while loop for a collection non-terminal, resolving type-guarded conditions
        /// and dispatching to per-item builder calls or inlined alternatives.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="nonTerminalElement">The collection <see cref="NonTerminalElement" /></param>
        /// <param name="referencedRule">The resolved grammar rule, or null</param>
        /// <param name="typeTarget">The resolved target type name</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        private static void EmitCollectionNonTerminalLoop(EncodedTextWriter writer, IClass umlClass, NonTerminalElement nonTerminalElement, TextualNotationRule referencedRule, string typeTarget, RuleGenerationContext ruleGenerationContext)
        {
            // Resolve which collection property this NonTerminal ultimately consumes
            if (referencedRule != null)
            {
                var collectionPropertyNames = referencedRule.QueryCollectionPropertyNames(ruleGenerationContext.AllRules);

                if (collectionPropertyNames.Count == 1)
                {
                    var propertyName = collectionPropertyNames.Single();
                    var allProperties = umlClass.QueryAllProperties();
                    var targetProperty = allProperties.SingleOrDefault(x => string.Equals(x.Name, propertyName, StringComparison.OrdinalIgnoreCase));

                    if (targetProperty != null && targetProperty.QueryIsEnumerable())
                    {
                        // Ensure cursor is declared
                        var existingCursor = ruleGenerationContext.DefinedCursors.SingleOrDefault(x => x.IsCursorValidForProperty(targetProperty));
                        string cursorVariableName;

                        if (existingCursor != null)
                        {
                            cursorVariableName = existingCursor.CursorVariableName;
                        }
                        else
                        {
                            var cursorDefinition = new CursorDefinition { DefinedForProperty = targetProperty };
                            var propertyAccessName = targetProperty.QueryPropertyNameBasedOnUmlProperties();
                            writer.WriteSafeString($"var {cursorDefinition.CursorVariableName} = cursorCache.GetOrCreateCursor({ruleGenerationContext.CurrentVariableName}.Id, \"{targetProperty.Name}\", {ruleGenerationContext.CurrentVariableName}.{propertyAccessName});{Environment.NewLine}");
                            ruleGenerationContext.DefinedCursors.Add(cursorDefinition);
                            cursorVariableName = cursorDefinition.CursorVariableName;
                        }

                        // Resolve the correct builder class name for the per-item call
                        var perItemCall = ResolveBuilderCall(umlClass, nonTerminalElement, typeTarget, ruleGenerationContext);

                        // When a collection NonTerminal is followed by another element sharing the same cursor,
                        // the while condition must exclude the next sibling's type to avoid consuming its elements.
                        // e.g., CalculationBodyItem* ResultExpressionMember → while (current is not IResultExpressionMembership)
                        var whileTypeExclusion = ResolveCollectionWhileTypeCondition(cursorVariableName, umlClass, referencedRule, ruleGenerationContext);

                        // Build the full while condition: merged pattern to avoid "merge into pattern" warnings.
                        // When no sibling-based type exclusion is available, use a positive type guard
                        // based on the collection item's assignment target type to prevent consuming
                        // elements belonging to subsequent grammar segments (unbounded cursor drain).
                        string whileCondition;

                        if (!string.IsNullOrWhiteSpace(whileTypeExclusion))
                        {
                            whileCondition = whileTypeExclusion;
                        }
                        else
                        {
                            // Try to resolve a positive type guard from the referenced rule's assignment targets.
                            // Only apply the guard when ALL alternatives are pure += assignments (no bare
                            // NonTerminal or Group elements that could match different cursor types).
                            var allElements = referencedRule?.Alternatives.SelectMany(alt => alt.Elements).ToList();

                            var hasNonAssignmentElements = allElements?.Any(element =>
                                element is NonTerminalElement or GroupElement) == true;

                            List<string> assignmentTargetTypes = null;

                            if (!hasNonAssignmentElements)
                            {
                                assignmentTargetTypes = allElements?
                                    .OfType<AssignmentElement>()
                                    .Where(assignmentElement => assignmentElement.Operator == "+=" && assignmentElement.Value is NonTerminalElement)
                                    .Select(assignmentElement =>
                                    {
                                        var valueNonTerminal = (NonTerminalElement)assignmentElement.Value;
                                        var refRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == valueNonTerminal.Name);
                                        var targetName = refRule != null ? refRule.TargetElementName ?? refRule.RuleName : null;

                                        if (targetName != null)
                                        {
                                            var targetClass = umlClass.Cache.Values.OfType<INamedElement>().SingleOrDefault(x => x.Name == targetName) as IClass;
                                            return targetClass?.QueryFullyQualifiedTypeName();
                                        }

                                        return null;
                                    })
                                    .Where(typeName => typeName != null)
                                    .Distinct()
                                    .ToList();
                            }

                            if (assignmentTargetTypes?.Count == 1)
                            {
                                // When the target type is a broad base type (e.g., IOwningMembership),
                                // also resolve the inner content type from the rule's ownedRelatedElement
                                // assignments. This prevents consuming elements that match the outer type
                                // but don't contain the expected content (e.g., a PackageMember is an
                                // IOwningMembership but doesn't contain a MetadataUsage).
                                var contentTypeGuard = ResolveContentTypeGuard(cursorVariableName, referencedRule, propertyName, umlClass, ruleGenerationContext);

                                if (!string.IsNullOrWhiteSpace(contentTypeGuard))
                                {
                                    whileCondition = contentTypeGuard;
                                }
                                else
                                {
                                    whileCondition = $"{cursorVariableName}.Current != null && {cursorVariableName}.Current is {assignmentTargetTypes[0]}";
                                }
                            }
                            else
                            {
                                whileCondition = $"{cursorVariableName}.Current != null";
                            }
                        }

                        if (perItemCall != null)
                        {
                            // The per-item call is a dispatcher builder (e.g., BuildNamespaceBodyElement(poco, ...))
                            // that internally advances the cursor in its switch cases. No outer Move() to avoid double advance.
                            writer.WriteSafeString($"while ({whileCondition}){Environment.NewLine}");
                            writer.WriteSafeString($"{{{Environment.NewLine}");
                            writer.WriteSafeString(perItemCall);
                            writer.WriteSafeString($"{Environment.NewLine}}}{Environment.NewLine}");
                        }
                        else
                        {
                            // Type incompatible: inline the referenced rule's alternatives inside the while loop.
                            // The inlined alternatives handle cursor advancement (via += dispatch Move()).
                            writer.WriteSafeString($"while ({whileCondition}){Environment.NewLine}");
                            writer.WriteSafeString($"{{{Environment.NewLine}");

                            var previousCaller = ruleGenerationContext.CallerRule;
                            var previousName = ruleGenerationContext.CurrentVariableName;
                            ruleGenerationContext.CallerRule = nonTerminalElement;

                            ProcessAlternatives(writer, umlClass, referencedRule.Alternatives, ruleGenerationContext);

                            ruleGenerationContext.CallerRule = previousCaller;
                            ruleGenerationContext.CurrentVariableName = previousName;

                            writer.WriteSafeString($"{Environment.NewLine}}}{Environment.NewLine}");
                        }

                        return;
                    }
                }
            }

            // Fallback for unresolvable cases — delegate to handcoded method.
            // Skip if a HandCoded call for the same rule was already emitted (avoids double-call
            // when both a group and a trailing collection non-terminal fall back to HandCoded).
            var handCodedRuleName = nonTerminalElement.TextualNotationRule?.RuleName ?? nonTerminalElement.Name;

            if (ruleGenerationContext.EmittedHandCodedCalls.Add(handCodedRuleName))
            {
                writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);{Environment.NewLine}");
            }
        }

        /// <summary>
        /// Resolves a content-aware type guard for a collection while loop. When the outer
        /// assignment targets one composite property (e.g., <c>ownedRelationship</c>), this method
        /// looks at the referenced rule's assignments on the complementary composite property
        /// (e.g., <c>ownedRelatedElement</c>) to determine the expected inner content type.
        /// This generates a compound condition that validates both the outer type AND the inner
        /// content, preventing consumption of elements that match the outer type but contain
        /// different content (e.g., a PackageMember is an IOwningMembership but doesn't contain
        /// a MetadataUsage).
        /// </summary>
        /// <param name="cursorVariableName">The cursor variable name</param>
        /// <param name="referencedRule">The grammar rule being referenced in the collection</param>
        /// <param name="outerPropertyName">The property name of the outer assignment (e.g., "ownedRelationship")</param>
        /// <param name="umlClass">The UML class providing the type cache</param>
        /// <param name="ruleGenerationContext">The current generation context</param>
        /// <returns>A compound while condition string, or null if no content guard can be resolved</returns>
        /// <summary>
        /// Resolves the type condition for a collection while loop using positive or negative type matching.
        /// </summary>
        /// <param name="cursorVariableName">The cursor variable name</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="collectionRule">The grammar rule for the collection non-terminal</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <returns>A type condition clause string, or empty if no condition is needed</returns>
        private static string ResolveCollectionWhileTypeCondition(string cursorVariableName, IClass umlClass, TextualNotationRule collectionRule, RuleGenerationContext ruleGenerationContext)
        {
            var siblings = ruleGenerationContext.CurrentSiblingElements;
            var currentIndex = ruleGenerationContext.CurrentElementIndex;

            if (siblings == null || currentIndex + 1 >= siblings.Count)
            {
                return "";
            }

            // Primary approach: positive condition from the collection item's += assignment target type.
            // Only applicable when ALL alternatives are += assignments (no mixed NonTerminal alternatives).
            if (collectionRule != null)
            {
                var allElements = collectionRule.Alternatives.SelectMany(alternative => alternative.Elements).ToList();

                var assignmentNonTerminals = allElements
                    .OfType<AssignmentElement>()
                    .Where(assignment => assignment.Operator == "+=")
                    .Select(assignment => assignment.Value)
                    .OfType<NonTerminalElement>()
                    .ToList();

                var hasOnlyAssignments = allElements.All(element => element is AssignmentElement or NonParsingAssignmentElement);

                if (assignmentNonTerminals.Count > 0 && hasOnlyAssignments)
                {
                    // Use the first assignment's target type as the positive condition
                    var itemRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == assignmentNonTerminals[0].Name);
                    var itemTypeTarget = itemRule != null ? itemRule.TargetElementName ?? itemRule.RuleName : null;

                    if (itemTypeTarget != null)
                    {
                        var itemTargetClass = umlClass.Cache.Values.OfType<INamedElement>()
                            .SingleOrDefault(x => x.Name == itemTypeTarget) as IClass;

                        if (itemTargetClass != null)
                        {
                            return $"{cursorVariableName}.Current is {itemTargetClass.QueryFullyQualifiedTypeName()}";
                        }
                    }
                }
            }

            // Fallback: negative exclusion based on the next sibling's target type
            var nextSibling = siblings[currentIndex + 1];
            NonTerminalElement nextNonTerminal = null;

            switch (nextSibling)
            {
                case NonTerminalElement nonTerminal:
                    nextNonTerminal = nonTerminal;
                    break;
                case AssignmentElement { Value: NonTerminalElement assignmentNonTerminal }:
                    nextNonTerminal = assignmentNonTerminal;
                    break;
                case GroupElement groupElement:
                    nextNonTerminal = groupElement.Alternatives
                        .SelectMany(alternative => alternative.Elements)
                        .OfType<AssignmentElement>()
                        .Select(assignment => assignment.Value)
                        .OfType<NonTerminalElement>()
                        .FirstOrDefault();

                    break;
            }

            if (nextNonTerminal == null)
            {
                return "";
            }

            var nextRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == nextNonTerminal.Name);
            var nextTypeTarget = nextRule != null ? nextRule.TargetElementName ?? nextRule.RuleName : null;

            if (nextTypeTarget == null)
            {
                return "";
            }

            var nextTargetClass = umlClass.Cache.Values.OfType<INamedElement>()
                .SingleOrDefault(x => x.Name == nextTypeTarget) as IClass;

            if (nextTargetClass == null)
            {
                return "";
            }

            return $"{cursorVariableName}.Current is not null and not {nextTargetClass.QueryFullyQualifiedTypeName()}";
        }
    }
}
