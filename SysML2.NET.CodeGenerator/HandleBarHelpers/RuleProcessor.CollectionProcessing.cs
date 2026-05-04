// -------------------------------------------------------------------------------------------------
// <copyright file="RuleProcessor.CollectionProcessing.cs" company="Starion Group S.A.">
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
    /// Collection loop emission, type resolution, and optional condition logic
    /// </summary>
    internal sealed partial class RuleProcessor
    {
        /// <summary>
        /// Emits a while loop for a collection non-terminal.
        /// </summary>
        private void EmitCollectionNonTerminalLoop(EncodedTextWriter writer, IClass umlClass, NonTerminalElement nonTerminalElement, TextualNotationRule referencedRule, string typeTarget, RuleGenerationContext ruleGenerationContext)
        {
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

                        var perItemCall = ResolveBuilderCall(umlClass, nonTerminalElement, typeTarget, ruleGenerationContext);

                        var whileTypeExclusion = ResolveCollectionWhileTypeCondition(cursorVariableName, umlClass, referencedRule, ruleGenerationContext);

                        string whileCondition;

                        if (!string.IsNullOrWhiteSpace(whileTypeExclusion))
                        {
                            whileCondition = whileTypeExclusion;
                        }
                        else
                        {
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
                                        var refRule = ruleGenerationContext.FindRule(valueNonTerminal.Name);
                                        var targetName = refRule != null ? refRule.EffectiveTarget : null;

                                        if (targetName != null)
                                        {
                                            var targetClass = RuleQueryUtilities.FindClass(umlClass.Cache, targetName);
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
                            writer.WriteSafeString($"while ({whileCondition}){Environment.NewLine}");
                            writer.WriteSafeString($"{{{Environment.NewLine}");
                            writer.WriteSafeString(perItemCall);
                            writer.WriteSafeString($"{Environment.NewLine}}}{Environment.NewLine}");
                        }
                        else
                        {
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

            var handCodedRuleName = nonTerminalElement.TextualNotationRule?.RuleName ?? nonTerminalElement.Name;

            EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext, deduplicate: true);
            writer.WriteSafeString(Environment.NewLine);
        }

        /// <summary>
        /// Resolves the type condition for a collection while loop.
        /// </summary>
        private string ResolveCollectionWhileTypeCondition(string cursorVariableName, IClass umlClass, TextualNotationRule collectionRule, RuleGenerationContext ruleGenerationContext)
        {
            var siblings = ruleGenerationContext.CurrentSiblingElements;
            var currentIndex = ruleGenerationContext.CurrentElementIndex;

            if (siblings == null || currentIndex + 1 >= siblings.Count)
            {
                return "";
            }

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
                    var itemRule = ruleGenerationContext.FindRule(assignmentNonTerminals[0].Name);
                    var itemTypeTarget = itemRule != null ? itemRule.EffectiveTarget : null;

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

            var nextRule = ruleGenerationContext.FindRule(nextNonTerminal.Name);
            var nextTypeTarget = nextRule != null ? nextRule.EffectiveTarget : null;

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

        /// <summary>
        /// Resolves a content-aware type guard for collection while loops.
        /// </summary>
        private string ResolveContentTypeGuard(string cursorVariableName, TextualNotationRule referencedRule, string outerPropertyName, IClass umlClass, RuleGenerationContext ruleGenerationContext)
        {
            if (referencedRule == null)
            {
                return null;
            }

            var outerTargetName = referencedRule.EffectiveTarget;
            var outerTargetClass = RuleQueryUtilities.FindClass(umlClass.Cache, outerTargetName);

            if (outerTargetClass == null)
            {
                return null;
            }

            var allClassesInHierarchy = new List<IClass> { outerTargetClass };
            allClassesInHierarchy.AddRange(outerTargetClass.QueryAllGeneralClassifiers().OfType<IClass>());

            var compositeProperties = allClassesInHierarchy
                .SelectMany(c => c.OwnedAttribute)
                .Where(p => p.IsComposite && !p.IsDerived)
                .ToList();

            var complementaryProperty = compositeProperties
                .FirstOrDefault(p => !string.Equals(p.Name, outerPropertyName, StringComparison.OrdinalIgnoreCase));

            if (complementaryProperty == null)
            {
                var samePropertyAssignment = referencedRule.Alternatives
                    .SelectMany(alt => alt.Elements)
                    .OfType<AssignmentElement>()
                    .FirstOrDefault(a => (a.Operator == "+=" || a.Operator == "=")
                                         && string.Equals(a.Property, outerPropertyName, StringComparison.OrdinalIgnoreCase)
                                         && a.Value is NonTerminalElement);

                if (samePropertyAssignment?.Value is NonTerminalElement innerNonTerminal)
                {
                    var innerRule = ruleGenerationContext.FindRule(innerNonTerminal.Name);

                    if (innerRule != null)
                    {
                        return ResolveContentTypeGuard(cursorVariableName, innerRule, outerPropertyName, umlClass, ruleGenerationContext);
                    }
                }

                return null;
            }

            var contentAssignment = referencedRule.Alternatives
                .SelectMany(alt => alt.Elements)
                .OfType<AssignmentElement>()
                .FirstOrDefault(a => (a.Operator == "+=" || a.Operator == "=")
                                     && string.Equals(a.Property, complementaryProperty.Name, StringComparison.OrdinalIgnoreCase)
                                     && a.Value is NonTerminalElement);

            if (contentAssignment == null)
            {
                return null;
            }

            var contentNonTerminal = (NonTerminalElement)contentAssignment.Value;
            var contentRule = ruleGenerationContext.FindRule(contentNonTerminal.Name);

            var contentTargetName = contentRule != null
                ? contentRule.EffectiveTarget
                : contentNonTerminal.Name;

            var contentTargetClass = RuleQueryUtilities.FindClass(umlClass.Cache, contentTargetName);

            if (contentTargetClass == null)
            {
                return null;
            }

            var outerTypeName = outerTargetClass.QueryFullyQualifiedTypeName();
            var contentTypeName = contentTargetClass.QueryFullyQualifiedTypeName();
            var complementaryAccessor = complementaryProperty.QueryPropertyNameBasedOnUmlProperties();
            var guardVarName = $"{outerTargetClass.Name.LowerCaseFirstLetter()}Guard";

            return $"{cursorVariableName}.Current is {outerTypeName} {guardVarName} && {guardVarName}.{complementaryAccessor}.OfType<{contentTypeName}>().Any()";
        }

        /// <summary>
        /// Resolves the builder method call string for a non-terminal element.
        /// </summary>
        private string ResolveBuilderCall(IClass umlClass, NonTerminalElement nonTerminalElement, string typeTarget, RuleGenerationContext ruleGenerationContext)
        {
            if (typeTarget == ruleGenerationContext.NamedElementToGenerate.Name)
            {
                return $"Build{nonTerminalElement.Name}({ruleGenerationContext.CurrentVariableName}, cursorCache, stringBuilder);";
            }

            var targetType = RuleQueryUtilities.FindNamedElement(umlClass.Cache, typeTarget);

            if (targetType is IClass targetClass)
            {
                if (umlClass.QueryAllGeneralClassifiers().Contains(targetClass))
                {
                    return $"{targetType.Name}TextualNotationBuilder.Build{nonTerminalElement.Name}({ruleGenerationContext.CurrentVariableName}, cursorCache, stringBuilder);";
                }

                return null;
            }

            return $"Build{nonTerminalElement.Name}({ruleGenerationContext.CurrentVariableName}, cursorCache, stringBuilder);";
        }

        /// <summary>
        /// Generates an inline condition expression for an optional non-terminal reference.
        /// </summary>
        private string GenerateInlineOptionalCondition(TextualNotationRule referencedRule, IClass targetClass, IReadOnlyList<TextualNotationRule> allRules, string variableName)
        {
            var propertyNames = referencedRule.QueryAllReferencedPropertyNames(allRules);

            if (propertyNames.Count == 0)
            {
                return null;
            }

            var allProperties = targetClass.QueryAllProperties();
            var conditionParts = new List<string>();

            foreach (var propertyName in propertyNames)
            {
                var property = allProperties.FirstOrDefault(x => string.Equals(x.Name, propertyName, StringComparison.OrdinalIgnoreCase));

                if (property == null)
                {
                    continue;
                }

                var umlPropertyName = property.QueryPropertyNameBasedOnUmlProperties();

                if (property.QueryIsEnumerable())
                {
                    conditionParts.Add($"{variableName}.{umlPropertyName}.Count != 0");
                }
                else
                {
                    conditionParts.Add(property.QueryIfStatementContentForNonEmpty(variableName));
                }
            }

            return conditionParts.Count != 0 ? string.Join(" || ", conditionParts) : null;
        }

        /// <summary>
        /// Emits an optional condition wrapping block for an optional NonTerminal element.
        /// </summary>
        private bool TryEmitOptionalCondition(EncodedTextWriter writer, NonTerminalElement nonTerminalElement, TextualNotationRule referencedRule, IClass targetClass, RuleGenerationContext ruleGenerationContext, string variableName)
        {
            if (!nonTerminalElement.IsOptional || nonTerminalElement.IsCollection)
            {
                return false;
            }

            if (referencedRule == null)
            {
                return false;
            }

            var condition = GenerateInlineOptionalCondition(referencedRule, targetClass, ruleGenerationContext.AllRules, variableName);

            if (condition == null)
            {
                return false;
            }

            writer.WriteSafeString($"{Environment.NewLine}if ({condition}){Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");
            return true;
        }
    }
}
