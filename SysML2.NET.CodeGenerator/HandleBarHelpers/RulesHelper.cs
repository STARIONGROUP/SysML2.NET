// -------------------------------------------------------------------------------------------------
// <copyright file="RulesHelper.cs" company="Starion Group S.A.">
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

    using uml4net;
    using uml4net.Classification;
    using uml4net.CommonStructure;
    using uml4net.Extensions;
    using uml4net.StructuredClassifiers;

    /// <summary>
    /// Provides textual notation rules related helper for <see cref="IHandlebars"/>
    /// </summary>
    public static class RulesHelper
    {
        /// <summary>
        /// Register this helper
        /// </summary>
        /// <param name="handlebars">The <see cref="IHandlebars"/> context with which the helper needs to be registered</param>
        public static void RegisterRulesHelper(this IHandlebars handlebars)
        {
            handlebars.RegisterHelper("RulesHelper.ContainsAnyDispatcherRules", (_, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new ArgumentException("RulesHelper.ContainsAnyDispatcherRules expects to have 3 arguments");
                }
                
                return arguments[0] is not List<TextualNotationRule> allRules 
                    ? throw new ArgumentException("RulesHelper.ContainsAnyDispatcherRules expects a list of TextualNotationRule as only argument") 
                    : allRules.Any(x => x.IsDispatcherRule);
            });
            
            handlebars.RegisterHelper("RulesHelper.WriteRule", (writer, _, arguments) =>
            {
                if (arguments.Length != 3)
                {
                    throw new ArgumentException("RulesHelper.WriteRule expects to have 3 arguments");
                }

                if (arguments[0] is not TextualNotationRule textualRule)
                {
                    throw new ArgumentException("RulesHelper.WriteRule expects TextualNotationRule as first argument");
                }

                if (arguments[1] is not INamedElement namedElement)
                {
                    throw new ArgumentException("RulesHelper.WriteRule expects INamedElement as second argument");
                }

                if (arguments[2] is not List<TextualNotationRule> allRules)
                {
                    throw new ArgumentException("RulesHelper.WriteRule expects a list of TextualNotationRule as third argument");
                }

                if (namedElement is IClass umlClass)
                {
                    var ruleGenerationContext = new RuleGenerationContext(namedElement)
                    {
                        CurrentVariableName = "poco"
                    };
                    
                    ruleGenerationContext.AllRules.AddRange(allRules);
                    ProcessAlternatives(writer, umlClass, textualRule.Alternatives, ruleGenerationContext);
                }
            });
        }

        /// <summary>
        /// Processes a collection of a <see cref="Alternatives"/>
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass"/></param>
        /// <param name="alternatives">The collection of alternatives to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext"/></param>
        /// <param name="isPartOfMultipleAlternative">Asserts that the current <see cref="AssignmentElement"/> is part of a multiple alternative process</param>
        private static void ProcessAlternatives(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext, bool isPartOfMultipleAlternative = false)
        {
            ruleGenerationContext.DefinedCursors ??= [];
            
            if (alternatives.Count == 1)
            {
                var alternative = alternatives.ElementAt(0);
                var elements = alternative.Elements;
                DeclareAllRequiredCursors(writer, umlClass, alternative, ruleGenerationContext);
                
                if (ruleGenerationContext.CallerRule is { IsOptional: true, IsCollection: false })
                {
                    var targetPropertiesName = elements.OfType<AssignmentElement>().Select(x => x.Property).Distinct().ToList();
                    var allProperties = umlClass.QueryAllProperties();

                    if (targetPropertiesName.Count > 0)
                    {
                        writer.WriteSafeString(Environment.NewLine);
                        writer.WriteSafeString("if(");

                        var ifStatementContent = new List<string>();

                        foreach (var targetPropertyName in targetPropertiesName)
                        {
                            var property = allProperties.Single(x => string.Equals(x.Name, targetPropertyName, StringComparison.OrdinalIgnoreCase));

                            if (property.QueryIsEnumerable())
                            {
                                var assigment = elements.OfType<AssignmentElement>().First(x => x.Property == targetPropertyName);
                                var iterator = ruleGenerationContext.DefinedCursors.FirstOrDefault(x => x.ApplicableRuleElements.Contains(assigment));
                                ifStatementContent.Add(iterator == null ? $"BuildGroupConditionFor{assigment.TextualNotationRule.RuleName}(poco)" : property.QueryIfStatementContentForNonEmpty(iterator.CursorVariableName));
                            }
                            else
                            {
                                ifStatementContent.Add(property.QueryIfStatementContentForNonEmpty("poco"));
                            }
                        }

                        writer.WriteSafeString(string.Join(" && ", ifStatementContent));
                        writer.WriteSafeString($"){Environment.NewLine}");
                        writer.WriteSafeString($"{{{Environment.NewLine}");

                        var previousSiblings = ruleGenerationContext.CurrentSiblingElements;
                        var previousIndex = ruleGenerationContext.CurrentElementIndex;
                        ruleGenerationContext.CurrentSiblingElements = elements;

                        for (var elementIndex = 0; elementIndex < elements.Count; elementIndex++)
                        {
                            ruleGenerationContext.CurrentElementIndex = elementIndex;
                            var previousCaller = ruleGenerationContext.CallerRule;
                            ProcessRuleElement(writer, umlClass, elements[elementIndex], ruleGenerationContext);
                            ruleGenerationContext.CallerRule = previousCaller;
                        }

                        ruleGenerationContext.CurrentSiblingElements = previousSiblings;
                        ruleGenerationContext.CurrentElementIndex = previousIndex;
                    }
                    else
                    {
                        var nonTerminalElements = elements.OfType<NonTerminalElement>().ToList();
                        var inlineConditionParts = new List<string>();

                        foreach (var nonTerminal in nonTerminalElements)
                        {
                            var referencedRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == nonTerminal.Name);

                            if (referencedRule != null)
                            {
                                var condition = GenerateInlineOptionalCondition(referencedRule, umlClass, ruleGenerationContext.AllRules, "poco");

                                if (condition != null)
                                {
                                    inlineConditionParts.Add(condition);
                                }
                            }
                        }

                        if (inlineConditionParts.Count > 0)
                        {
                            writer.WriteSafeString($"{Environment.NewLine}if ({string.Join(" || ", inlineConditionParts)}){Environment.NewLine}");
                        }
                        else
                        {
                            writer.WriteSafeString($"{Environment.NewLine}if (BuildGroupConditionFor{alternative.TextualNotationRule.RuleName}(poco)){Environment.NewLine}");
                        }

                        writer.WriteSafeString($"{{{Environment.NewLine}");

                        var previousSiblings = ruleGenerationContext.CurrentSiblingElements;
                        var previousIndex = ruleGenerationContext.CurrentElementIndex;
                        ruleGenerationContext.CurrentSiblingElements = elements;

                        for (var elementIndex = 0; elementIndex < elements.Count; elementIndex++)
                        {
                            ruleGenerationContext.CurrentElementIndex = elementIndex;
                            ProcessRuleElement(writer, umlClass, elements[elementIndex], ruleGenerationContext);
                        }

                        ruleGenerationContext.CurrentSiblingElements = previousSiblings;
                        ruleGenerationContext.CurrentElementIndex = previousIndex;
                    }

                    if (!ruleGenerationContext.IsNextElementNewLineTerminal() && !ruleGenerationContext.IsLastElement())
                    {
                        writer.WriteSafeString($"stringBuilder.Append(' ');{Environment.NewLine}");
                    }

                    writer.WriteSafeString($"}}{Environment.NewLine}");
                }
                else
                {
                    var previousSiblings = ruleGenerationContext.CurrentSiblingElements;
                    var previousIndex = ruleGenerationContext.CurrentElementIndex;
                    ruleGenerationContext.CurrentSiblingElements = elements;

                    for (var elementIndex = 0; elementIndex < elements.Count; elementIndex++)
                    {
                        ruleGenerationContext.CurrentElementIndex = elementIndex;
                        var previousCaller = ruleGenerationContext.CallerRule;
                        ProcessRuleElement(writer, umlClass, elements[elementIndex], ruleGenerationContext, isPartOfMultipleAlternative);
                        ruleGenerationContext.CallerRule = previousCaller;
                    }

                    ruleGenerationContext.CurrentSiblingElements = previousSiblings;
                    ruleGenerationContext.CurrentElementIndex = previousIndex;
                }
            }
            else
            {
                if (alternatives.All(x => x.Elements.Count == 1))
                {
                    if (alternatives.ElementAt(0).Elements[0].TextualNotationRule.IsMultiCollectionAssignment)
                    {
                        ProcessMultiCollectionAssignment(writer, umlClass, alternatives, ruleGenerationContext);
                        return;
                    }

                    var types = alternatives.SelectMany(x => x.Elements).Select(x => x.GetType()).Distinct().ToList();
                    
                    if(types.Count == 1)
                    {
                        ProcessUnitypedAlternativesWithOneElement(writer, umlClass, alternatives, ruleGenerationContext);
                    }
                    else
                    {
                        if (types.SequenceEqual([typeof(AssignmentElement), typeof(NonTerminalElement)]))
                        {
                            foreach (var alternative in alternatives)
                            {
                                DeclareAllRequiredCursors(writer, umlClass, alternative, ruleGenerationContext);
                            }

                            for (var alternativeIndex = 0; alternativeIndex < alternatives.Count; alternativeIndex++)
                            {
                                var ruleElement = alternatives.ElementAt(alternativeIndex).Elements[0];

                                if (alternativeIndex != 0)
                                {
                                    writer.WriteSafeString("else");
                                }

                                switch (ruleElement)
                                {
                                    case AssignmentElement assignmentElement:
                                        var targetProperty = umlClass.QueryAllProperties().Single(x => string.Equals(x.Name, assignmentElement.Property));

                                        if (alternativeIndex != 0)
                                        {
                                            writer.WriteSafeString(" ");
                                        }

                                        if (targetProperty.QueryIsEnumerable())
                                        {
                                            DeclareAllRequiredCursors(writer, umlClass, alternatives.ElementAt(0), ruleGenerationContext);

                                            var iterator = ruleGenerationContext.DefinedCursors.Single(x => x.ApplicableRuleElements.Contains(assignmentElement));

                                            writer.WriteSafeString($"if({targetProperty.QueryIfStatementContentForNonEmpty(iterator.CursorVariableName)}){Environment.NewLine}");
                                            writer.WriteSafeString($"{{{Environment.NewLine}");
                                        }
                                        else
                                        {
                                            writer.WriteSafeString($"{Environment.NewLine}if({targetProperty.QueryIfStatementContentForNonEmpty("poco")}){Environment.NewLine}");
                                            writer.WriteSafeString($"{{{Environment.NewLine}");
                                        }

                                        ProcessAssignmentElement(writer, umlClass, ruleGenerationContext, assignmentElement, true);

                                        writer.WriteSafeString($"{Environment.NewLine}}}");
                                        break;

                                    case NonTerminalElement nonTerminalElement:
                                        writer.WriteSafeString($"{{{Environment.NewLine}");
                                        ProcessNonTerminalElement(writer, umlClass, nonTerminalElement, ruleGenerationContext);
                                        writer.WriteSafeString($"{Environment.NewLine}}}");
                                        break;
                                }
                            }
                        }
                        else if (types.SequenceEqual([typeof(NonTerminalElement), typeof(AssignmentElement)]))
                        {
                            var nonTerminalElement = (NonTerminalElement)alternatives.ElementAt(0).Elements[0];
                            var assignmentElements = alternatives.SelectMany(x => x.Elements).OfType<AssignmentElement>().ToList();

                            var referencedAssignmentNonTerminals = assignmentElements.Select(x => x.Value).OfType<NonTerminalElement>().ToList();

                            if (referencedAssignmentNonTerminals.Count != assignmentElements.Count)
                            {
                                var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                                writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                                return;
                            }

                            // Mixed NonTerminal + AssignmentElement dispatch:
                            // - AssignmentElements (+=) target cursor elements → if (cursor.Current is {Type}) { process + Move() }
                            // - NonTerminal targets the declaring class → else { BuildXxx(poco, ...) }
                            var targetProperty = umlClass.QueryAllProperties().Single(x => string.Equals(x.Name, assignmentElements[0].Property));
                            var cursorVarName = $"{targetProperty.Name.LowerCaseFirstLetter()}Cursor";
                            var existingCursor = ruleGenerationContext.DefinedCursors.FirstOrDefault(x => x.IsCursorValidForProperty(targetProperty));

                            if (existingCursor == null)
                            {
                                writer.WriteSafeString($"var {cursorVarName} = cursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{targetProperty.QueryPropertyNameBasedOnUmlProperties()});{Environment.NewLine}");
                                var cursorDef = new CursorDefinition { DefinedForProperty = targetProperty };

                                foreach (var assignmentElement in assignmentElements)
                                {
                                    cursorDef.ApplicableRuleElements.Add(assignmentElement);
                                }

                                ruleGenerationContext.DefinedCursors.Add(cursorDef);
                            }
                            else
                            {
                                cursorVarName = existingCursor.CursorVariableName;
                            }

                            // Generate if/else chain: assignment types checked on cursor, NonTerminal called with poco
                            var assignmentMappedElements = OrderElementsByInheritance(referencedAssignmentNonTerminals, umlClass.Cache, ruleGenerationContext);

                            for (var assignmentIndex = 0; assignmentIndex < assignmentMappedElements.Count; assignmentIndex++)
                            {
                                var mappedElement = assignmentMappedElements[assignmentIndex];

                                if (assignmentIndex > 0)
                                {
                                    writer.WriteSafeString("else ");
                                }

                                var assignmentVarName = mappedElement.UmlClass.Name.LowerCaseFirstLetter();
                                writer.WriteSafeString($"if ({cursorVarName}.Current is {mappedElement.UmlClass.QueryFullyQualifiedTypeName()} {assignmentVarName}){Environment.NewLine}");
                                writer.WriteSafeString($"{{{Environment.NewLine}");

                                var previousVariableName = ruleGenerationContext.CurrentVariableName;
                                ruleGenerationContext.CurrentVariableName = assignmentVarName;
                                ProcessNonTerminalElement(writer, mappedElement.UmlClass, mappedElement.RuleElement, ruleGenerationContext);
                                ruleGenerationContext.CurrentVariableName = previousVariableName;

                                writer.WriteSafeString($"{Environment.NewLine}{cursorVarName}.Move();{Environment.NewLine}");
                                writer.WriteSafeString($"}}{Environment.NewLine}");
                            }

                            // NonTerminal fallback: called with poco, not cursor element
                            writer.WriteSafeString($"else{Environment.NewLine}");
                            writer.WriteSafeString($"{{{Environment.NewLine}");

                            var nonTerminalReferencedRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == nonTerminalElement.Name);
                            var nonTerminalTypeTarget = nonTerminalReferencedRule != null
                                ? (nonTerminalReferencedRule.TargetElementName ?? nonTerminalReferencedRule.RuleName)
                                : umlClass.Name;
                            var nonTerminalCall = ResolveBuilderCall(umlClass, nonTerminalElement, nonTerminalTypeTarget, ruleGenerationContext);

                            if (nonTerminalCall != null)
                            {
                                writer.WriteSafeString(nonTerminalCall);
                            }
                            else
                            {
                                var previousCaller = ruleGenerationContext.CallerRule;
                                var previousName = ruleGenerationContext.CurrentVariableName;
                                ruleGenerationContext.CallerRule = nonTerminalElement;
                                ProcessAlternatives(writer, umlClass, nonTerminalReferencedRule?.Alternatives, ruleGenerationContext);
                                ruleGenerationContext.CallerRule = previousCaller;
                                ruleGenerationContext.CurrentVariableName = previousName;
                            }

                            writer.WriteSafeString($"{Environment.NewLine}}}{Environment.NewLine}");
                        }
                        else if (alternatives.ElementAt(0).Elements[0] is TerminalElement terminalElement && alternatives.ElementAt(1).Elements[0] is AssignmentElement assignmentElement)
                        {
                            var targetProperty = umlClass.QueryAllProperties().Single(x => string.Equals(x.Name, assignmentElement.Property));
                            
                            writer.WriteSafeString($"if(!{targetProperty.QueryIfStatementContentForNonEmpty("poco")}){Environment.NewLine}");
                            writer.WriteSafeString($"{{{Environment.NewLine}");
                            ProcessRuleElement(writer, umlClass, terminalElement, ruleGenerationContext);
                            writer.WriteSafeString($"{Environment.NewLine}}}");
                            writer.WriteSafeString("else");
                            writer.WriteSafeString($"{Environment.NewLine}{{{Environment.NewLine}");
                            ProcessAssignmentElement(writer, umlClass, ruleGenerationContext, assignmentElement, true);
                            writer.WriteSafeString($"{Environment.NewLine}}}");
                        }
                        else
                        {
                            var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;

                            if (ruleGenerationContext.EmittedHandCodedCalls.Add(handCodedRuleName))
                            {
                                writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                            }
                        }
                    }
                }
                else
                {
                    // When all alternatives consist exclusively of terminal elements (and optionally non-parsing assignments), handle via code-gen
                    if (alternatives.All(alt => alt.Elements.Count > 0 && alt.Elements.All(element => element is TerminalElement or NonParsingAssignmentElement)))
                    {
                        var nonParsingAssignments = alternatives
                            .SelectMany(alt => alt.Elements.OfType<NonParsingAssignmentElement>())
                            .ToList();

                        if (nonParsingAssignments.Count == 0)
                        {
                            // Pure terminal alternatives — emit the first alternative
                            var firstAlternative = alternatives.ElementAt(0);

                            foreach (var terminalOnly in firstAlternative.Elements.Cast<TerminalElement>())
                            {
                                WriteTerminalAppend(writer, terminalOnly.Value);
                            }
                        }
                        else
                        {
                            // Terminal + non-parsing assignment alternatives — generate a switch on the assigned property
                            var assignmentPropertyName = nonParsingAssignments[0].PropertyName;
                            var targetProperty = umlClass.QueryAllProperties().SingleOrDefault(x => string.Equals(x.Name, assignmentPropertyName, StringComparison.OrdinalIgnoreCase));

                            if (targetProperty != null)
                            {
                                var targetPropertyName = targetProperty.QueryPropertyNameBasedOnUmlProperties();

                                writer.WriteSafeString($"switch ({ruleGenerationContext.CurrentVariableName ?? "poco"}.{targetPropertyName}){Environment.NewLine}");
                                writer.WriteSafeString($"{{{Environment.NewLine}");

                                foreach (var alternative in alternatives)
                                {
                                    var nonParsingAssignment = alternative.Elements.OfType<NonParsingAssignmentElement>().Single();
                                    var terminals = alternative.Elements.OfType<TerminalElement>().ToList();
                                    var enumValueName = nonParsingAssignment.Value.Trim('\'').CapitalizeFirstLetter();

                                    writer.WriteSafeString($"case {targetProperty.Type.QueryFullyQualifiedTypeName()}.{enumValueName}:{Environment.NewLine}");

                                    foreach (var terminal in terminals)
                                    {
                                        WriteTerminalAppend(writer, terminal.Value);
                                    }

                                    writer.WriteSafeString($"{Environment.NewLine}break;{Environment.NewLine}");
                                }

                                writer.WriteSafeString($"}}{Environment.NewLine}");
                            }
                            else
                            {
                                // Fallback: emit first alternative terminals
                                var firstAlternative = alternatives.ElementAt(0);

                                foreach (var terminalOnly in firstAlternative.Elements.OfType<TerminalElement>())
                                {
                                    WriteTerminalAppend(writer, terminalOnly.Value);
                                }
                            }
                        }

                        return;
                    }

                    // Detect pattern: property=[QualifiedName] | property=NonTerminal{containment+=property}
                    // e.g., type=[QualifiedName]|type=OwnedFeatureChain{ownedRelatedElement+=type}
                    // Runtime: if the referenced value is owned → call the chain builder, else → output qualifiedName
                    if (alternatives.Count == 2)
                    {
                        var qualifiedNameAlt = alternatives.FirstOrDefault(alt =>
                            alt.Elements.Count == 1
                            && alt.Elements[0] is AssignmentElement { Value: ValueLiteralElement qualifiedNameLiteral }
                            && qualifiedNameLiteral.QueryIsQualifiedName());

                        var chainAlt = alternatives.FirstOrDefault(alt =>
                            alt.Elements.Count >= 2
                            && alt.Elements[0] is AssignmentElement { Value: NonTerminalElement }
                            && alt.Elements.OfType<NonParsingAssignmentElement>().Any());

                        if (qualifiedNameAlt != null && chainAlt != null)
                        {
                            var qualifiedNameAssignment = (AssignmentElement)qualifiedNameAlt.Elements[0];
                            var chainAssignment = (AssignmentElement)chainAlt.Elements[0];
                            var chainNonTerminal = (NonTerminalElement)chainAssignment.Value;
                            var containmentAssignment = chainAlt.Elements.OfType<NonParsingAssignmentElement>().First();

                            var propertyName = qualifiedNameAssignment.Property;
                            var allProperties = umlClass.QueryAllProperties();
                            var targetProperty = allProperties.SingleOrDefault(x =>
                                string.Equals(x.Name, propertyName, StringComparison.OrdinalIgnoreCase));
                            var containmentProperty = allProperties.SingleOrDefault(x =>
                                string.Equals(x.Name, containmentAssignment.PropertyName, StringComparison.OrdinalIgnoreCase));

                            if (targetProperty != null && containmentProperty != null)
                            {
                                var variableName = ruleGenerationContext.CurrentVariableName ?? "poco";
                                var resolvedPropertyName = targetProperty.QueryPropertyNameBasedOnUmlProperties();
                                var resolvedContainmentName = containmentProperty.QueryPropertyNameBasedOnUmlProperties();

                                // Resolve the chain NonTerminal's target type
                                var referencedRule = ruleGenerationContext.AllRules
                                    .SingleOrDefault(x => x.RuleName == chainNonTerminal.Name);
                                var typeTarget = referencedRule != null
                                    ? (referencedRule.TargetElementName ?? referencedRule.RuleName)
                                    : umlClass.Name;

                                var chainTargetClass = umlClass.Cache.Values.OfType<INamedElement>()
                                    .SingleOrDefault(x => x.Name == typeTarget) as IClass;

                                if (chainTargetClass != null)
                                {
                                    var chainTypeName = chainTargetClass.QueryFullyQualifiedTypeName();
                                    var chainVarName = $"chained{resolvedPropertyName}As{chainTargetClass.Name}";

                                    string builderCallString;

                                    if (typeTarget == ruleGenerationContext.NamedElementToGenerate.Name)
                                    {
                                        builderCallString = $"Build{chainNonTerminal.Name}({chainVarName}, cursorCache, stringBuilder);";
                                    }
                                    else
                                    {
                                        builderCallString = $"{typeTarget}TextualNotationBuilder.Build{chainNonTerminal.Name}({chainVarName}, cursorCache, stringBuilder);";
                                    }

                                    writer.WriteSafeString($"if ({variableName}.{resolvedContainmentName}.Contains({variableName}.{resolvedPropertyName}) && {variableName}.{resolvedPropertyName} is {chainTypeName} {chainVarName}){Environment.NewLine}");
                                    writer.WriteSafeString($"{{{Environment.NewLine}");
                                    writer.WriteSafeString($"{builderCallString}{Environment.NewLine}");
                                    writer.WriteSafeString($"}}{Environment.NewLine}");
                                    writer.WriteSafeString($"else if ({variableName}.{resolvedPropertyName} != null){Environment.NewLine}");
                                    writer.WriteSafeString($"{{{Environment.NewLine}");
                                    writer.WriteSafeString($"stringBuilder.Append({variableName}.{resolvedPropertyName}.qualifiedName);{Environment.NewLine}");
                                    writer.WriteSafeString($"stringBuilder.Append(' ');{Environment.NewLine}");
                                    writer.WriteSafeString($"}}{Environment.NewLine}");

                                    return;
                                }
                            }
                        }
                    }

                    // Multi-element alternatives (e.g., ';' | '{' NamespaceBodyElement* '}')
                    // Detect pattern: first alternative is terminal-only, second has collection assignment
                    var firstAlt = alternatives.ElementAt(0);
                    var hasTerminalOnlyFirstAlt = firstAlt.Elements.Count == 1 && firstAlt.Elements[0] is TerminalElement;

                    if (hasTerminalOnlyFirstAlt && alternatives.Count == 2)
                    {
                        var secondAlt = alternatives.ElementAt(1);
                        var collectionAssignments = secondAlt.Elements.OfType<AssignmentElement>().Where(x => x.Operator == "+=").ToList();
                        var groupsWithCollectionAssignments = secondAlt.Elements.OfType<GroupElement>()
                            .SelectMany(g => g.Alternatives.SelectMany(a => a.Elements.OfType<AssignmentElement>().Where(x => x.Operator == "+=")))
                            .ToList();

                        var allCollectionAssignments = collectionAssignments.Concat(groupsWithCollectionAssignments).ToList();

                        if (allCollectionAssignments.Count > 0)
                        {
                            // Determine the collection property to check for emptiness
                            var collectionProperty = allCollectionAssignments[0].Property;
                            var targetProperty = umlClass.QueryAllProperties().SingleOrDefault(x => string.Equals(x.Name, collectionProperty, StringComparison.OrdinalIgnoreCase));
                            var terminalValue = ((TerminalElement)firstAlt.Elements[0]).Value;

                            if (targetProperty != null)
                            {
                                writer.WriteSafeString($"if(poco.{targetProperty.QueryPropertyNameBasedOnUmlProperties()}.Count == 0){Environment.NewLine}");
                                writer.WriteSafeString($"{{{Environment.NewLine}");
                                writer.WriteSafeString($"stringBuilder.AppendLine(\"{terminalValue}\");{Environment.NewLine}");
                                writer.WriteSafeString($"}}{Environment.NewLine}");
                                writer.WriteSafeString($"else{Environment.NewLine}");
                                writer.WriteSafeString($"{{{Environment.NewLine}");

                                // Process the second alternative elements
                                DeclareAllRequiredCursors(writer, umlClass, secondAlt, ruleGenerationContext);

                                foreach (var element in secondAlt.Elements)
                                {
                                    ProcessRuleElement(writer, umlClass, element, ruleGenerationContext);
                                }

                                writer.WriteSafeString($"}}{Environment.NewLine}");
                            }
                            else
                            {
                                var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                                writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                            }
                        }
                        else
                        {
                            // Check for collection NonTerminal elements (e.g., ';' | '{' Items* '}')
                            // The body items are expressed as a standalone NonTerminal with IsCollection, not as AssignmentElements
                            var collectionNonTerminals = secondAlt.Elements.OfType<NonTerminalElement>().Where(x => x.IsCollection).ToList();

                            if (collectionNonTerminals.Count > 0)
                            {
                                // Resolve the collection property through the NonTerminal's rule tree
                                var nonTerminalRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == collectionNonTerminals[0].Name);

                                if (nonTerminalRule != null)
                                {
                                    var collectionPropertyNames = nonTerminalRule.QueryCollectionPropertyNames(ruleGenerationContext.AllRules);

                                    if (collectionPropertyNames.Count > 0)
                                    {
                                        var collectionPropertyName = collectionPropertyNames.First();
                                        var targetProperty = umlClass.QueryAllProperties().SingleOrDefault(x => string.Equals(x.Name, collectionPropertyName, StringComparison.OrdinalIgnoreCase));
                                        var terminalValue = ((TerminalElement)firstAlt.Elements[0]).Value;

                                        if (targetProperty != null)
                                        {
                                            var propertyAccessName = targetProperty.QueryPropertyNameBasedOnUmlProperties();

                                            writer.WriteSafeString($"if(poco.{propertyAccessName}.Count == 0){Environment.NewLine}");
                                            writer.WriteSafeString($"{{{Environment.NewLine}");
                                            writer.WriteSafeString($"stringBuilder.AppendLine(\"{terminalValue}\");{Environment.NewLine}");
                                            writer.WriteSafeString($"}}{Environment.NewLine}");
                                            writer.WriteSafeString($"else{Environment.NewLine}");
                                            writer.WriteSafeString($"{{{Environment.NewLine}");

                                            // Process non-collection elements (terminals like '{', '}' and optional assignments like isParallel?='parallel')
                                            // and handle the collection NonTerminal via cursor-based dispatch (builder internally advances)
                                            foreach (var element in secondAlt.Elements)
                                            {
                                                if (element is NonTerminalElement { IsCollection: true })
                                                {
                                                    var cursorVarName = $"{targetProperty.Name.LowerCaseFirstLetter()}Cursor";
                                                    writer.WriteSafeString($"var {cursorVarName} = cursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{propertyAccessName});{Environment.NewLine}");

                                                    var collectionNonTerminal = (NonTerminalElement)element;
                                                    var referencedRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == collectionNonTerminal.Name);
                                                    var typeTarget = referencedRule != null
                                                        ? (referencedRule.TargetElementName ?? referencedRule.RuleName)
                                                        : umlClass.Name;

                                                    var perItemCall = ResolveBuilderCall(umlClass, collectionNonTerminal, typeTarget, ruleGenerationContext);

                                                    writer.WriteSafeString($"while ({cursorVarName}.Current != null){Environment.NewLine}");
                                                    writer.WriteSafeString($"{{{Environment.NewLine}");

                                                    if (perItemCall != null)
                                                    {
                                                        // Dispatcher builder internally advances the cursor — no outer Move()
                                                        writer.WriteSafeString(perItemCall);
                                                    }
                                                    else
                                                    {
                                                        var previousCaller = ruleGenerationContext.CallerRule;
                                                        var previousName = ruleGenerationContext.CurrentVariableName;
                                                        ruleGenerationContext.CallerRule = collectionNonTerminal;
                                                        ProcessAlternatives(writer, umlClass, referencedRule?.Alternatives, ruleGenerationContext);
                                                        ruleGenerationContext.CallerRule = previousCaller;
                                                        ruleGenerationContext.CurrentVariableName = previousName;
                                                    }

                                                    writer.WriteSafeString($"{Environment.NewLine}}}{Environment.NewLine}");
                                                }
                                                else
                                                {
                                                    ProcessRuleElement(writer, umlClass, element, ruleGenerationContext);
                                                }
                                            }

                                            writer.WriteSafeString($"}}{Environment.NewLine}");
                                        }
                                        else
                                        {
                                            var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                                            writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                                        }
                                    }
                                    else
                                    {
                                        var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                                        writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                                    }
                                }
                                else
                                {
                                    var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                                    writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                                }
                            }
                            else
                            {
                                // Check for non-collection NonTerminal elements (e.g., ';' | '{' CalculationBodyPart '}')
                                // Same body pattern but with a single sub-rule call instead of a while loop
                                var nonCollectionNonTerminals = secondAlt.Elements.OfType<NonTerminalElement>().Where(x => !x.IsCollection).ToList();

                                if (nonCollectionNonTerminals.Count > 0)
                                {
                                    var nonTerminalRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == nonCollectionNonTerminals[0].Name);

                                    if (nonTerminalRule != null)
                                    {
                                        var collectionPropertyNames = nonTerminalRule.QueryCollectionPropertyNames(ruleGenerationContext.AllRules);

                                        if (collectionPropertyNames.Count > 0)
                                        {
                                            var collectionPropertyName = collectionPropertyNames.First();
                                            var targetProperty = umlClass.QueryAllProperties().SingleOrDefault(x => string.Equals(x.Name, collectionPropertyName, StringComparison.OrdinalIgnoreCase));
                                            var terminalValue = ((TerminalElement)firstAlt.Elements[0]).Value;

                                            if (targetProperty != null)
                                            {
                                                var propertyAccessName = targetProperty.QueryPropertyNameBasedOnUmlProperties();

                                                writer.WriteSafeString($"if(poco.{propertyAccessName}.Count == 0){Environment.NewLine}");
                                                writer.WriteSafeString($"{{{Environment.NewLine}");
                                                writer.WriteSafeString($"stringBuilder.AppendLine(\"{terminalValue}\");{Environment.NewLine}");
                                                writer.WriteSafeString($"}}{Environment.NewLine}");
                                                writer.WriteSafeString($"else{Environment.NewLine}");
                                                writer.WriteSafeString($"{{{Environment.NewLine}");

                                                foreach (var element in secondAlt.Elements)
                                                {
                                                    if (element is NonTerminalElement { IsCollection: false } singleNonTerminal)
                                                    {
                                                        var cursorVarName = $"{targetProperty.Name.LowerCaseFirstLetter()}Cursor";
                                                        writer.WriteSafeString($"var {cursorVarName} = cursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{propertyAccessName});{Environment.NewLine}");

                                                        var referencedRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == singleNonTerminal.Name);
                                                        var typeTarget = referencedRule != null
                                                            ? (referencedRule.TargetElementName ?? referencedRule.RuleName)
                                                            : umlClass.Name;

                                                        var perItemCall = ResolveBuilderCall(umlClass, singleNonTerminal, typeTarget, ruleGenerationContext);

                                                        writer.WriteSafeString($"if ({cursorVarName}.Current != null){Environment.NewLine}");
                                                        writer.WriteSafeString($"{{{Environment.NewLine}");

                                                        if (perItemCall != null)
                                                        {
                                                            writer.WriteSafeString(perItemCall);
                                                        }
                                                        else
                                                        {
                                                            var previousCaller = ruleGenerationContext.CallerRule;
                                                            var previousName = ruleGenerationContext.CurrentVariableName;
                                                            ruleGenerationContext.CallerRule = singleNonTerminal;
                                                            ProcessAlternatives(writer, umlClass, referencedRule?.Alternatives, ruleGenerationContext);
                                                            ruleGenerationContext.CallerRule = previousCaller;
                                                            ruleGenerationContext.CurrentVariableName = previousName;
                                                        }

                                                        writer.WriteSafeString($"{Environment.NewLine}}}{Environment.NewLine}");
                                                    }
                                                    else
                                                    {
                                                        ProcessRuleElement(writer, umlClass, element, ruleGenerationContext);
                                                    }
                                                }

                                                writer.WriteSafeString($"}}{Environment.NewLine}");
                                            }
                                            else
                                            {
                                                var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                                                writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                                            }
                                        }
                                        else
                                        {
                                            var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                                            writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                                        }
                                    }
                                    else
                                    {
                                        var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                                        writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                                    }
                                }
                                else
                                {
                                    var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                                    writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (TryHandleOperatorLiteralAlternation(writer, umlClass, alternatives, ruleGenerationContext))
                        {
                            return;
                        }

                        if (TryHandleEmptyVsNonEmptyMembership(writer, umlClass, alternatives, ruleGenerationContext))
                        {
                            return;
                        }

                        if (TryHandlePocoTypeDispatchWithCompoundAlternatives(writer, umlClass, alternatives, ruleGenerationContext))
                        {
                            return;
                        }

                        if (TryHandleReferenceOrInline(writer, umlClass, alternatives, ruleGenerationContext))
                        {
                            return;
                        }

                        var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;

                        if (ruleGenerationContext.EmittedHandCodedCalls.Add(handCodedRuleName))
                        {
                            writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Pattern B — detects and emits an alternation where each alternative starts with
        /// <c>operator = X</c> and <c>X</c> is either a literal terminal or a non-terminal whose
        /// RHS is a pure literal alternation (e.g. <c>ClassificationTestOperator = 'istype'|'hastype'|'@'</c>,
        /// <c>CastOperator = 'as'</c>, <c>MetaCastOperator = 'meta'</c>). Emits a <c>switch</c> on
        /// <c>poco.Operator</c> with one <c>case</c> per literal, processing each alternative's tail
        /// elements (the trailing <c>ownedRelationship+=...</c> assignment, etc.) inside the matching branch.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write output</param>
        /// <param name="umlClass">The current <see cref="IClass"/></param>
        /// <param name="alternatives">The collection of alternatives to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext"/></param>
        /// <returns><c>true</c> if the pattern matched and code was emitted; <c>false</c> otherwise</returns>
        private static bool TryHandleOperatorLiteralAlternation(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
        {
            if (alternatives.Count < 2)
            {
                return false;
            }

            var operatorBranches = new List<(Alternatives Alternative, List<string> Literals)>();

            foreach (var alternative in alternatives)
            {
                if (alternative.Elements.Count == 0
                    || alternative.Elements[0] is not AssignmentElement { Property: "operator", Operator: "=" } operatorAssignment)
                {
                    return false;
                }

                var literals = ExtractLiteralAlternation(operatorAssignment.Value, ruleGenerationContext.AllRules);

                if (literals == null || literals.Count == 0)
                {
                    return false;
                }

                // Each alternative's trailing elements (after the operator= assignment) must be cleanly processable
                // by the existing emitter — otherwise the inlined body would reference unresolved properties or
                // builders. Skip Pattern B in that case so the caller falls back to Build{Rule}HandCoded.
                if (!AreAlternativeTailElementsProcessable(alternative.Elements, umlClass, ruleGenerationContext))
                {
                    return false;
                }

                operatorBranches.Add((alternative, literals));
            }

            // All alternatives match — declare any cursors needed by the per-branch tails, then emit the switch.
            foreach (var alternative in alternatives)
            {
                DeclareAllRequiredCursors(writer, umlClass, alternative, ruleGenerationContext);
            }

            var variableName = ruleGenerationContext.CurrentVariableName ?? "poco";
            writer.WriteSafeString($"switch ({variableName}.Operator){Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");

            foreach (var (alternative, literals) in operatorBranches)
            {
                foreach (var literal in literals)
                {
                    writer.WriteSafeString($"case \"{literal}\":{Environment.NewLine}");
                }

                writer.WriteSafeString($"stringBuilder.Append({variableName}.Operator);{Environment.NewLine}");
                writer.WriteSafeString($"stringBuilder.Append(' ');{Environment.NewLine}");

                var previousSiblings = ruleGenerationContext.CurrentSiblingElements;
                var previousIndex = ruleGenerationContext.CurrentElementIndex;
                ruleGenerationContext.CurrentSiblingElements = alternative.Elements;

                for (var elementIndex = 1; elementIndex < alternative.Elements.Count; elementIndex++)
                {
                    ruleGenerationContext.CurrentElementIndex = elementIndex;
                    ProcessRuleElement(writer, umlClass, alternative.Elements[elementIndex], ruleGenerationContext);
                }

                ruleGenerationContext.CurrentSiblingElements = previousSiblings;
                ruleGenerationContext.CurrentElementIndex = previousIndex;

                writer.WriteSafeString($"break;{Environment.NewLine}");
            }

            writer.WriteSafeString($"}}{Environment.NewLine}");
            return true;
        }

        /// <summary>
        /// Pattern A — detects and emits an alternation in which both alternatives consume a single
        /// element from the same cursor property via <c>+=</c>, where one referenced rule wraps an
        /// <c>EmptyUsage</c> (canonical form: <c>Empty*Member : … = ownedRelatedElement += EmptyXxx</c>)
        /// and the other wraps a real element. The two alternatives are otherwise identical at the
        /// .NET cursor type level (e.g. both produce an <c>IParameterMembership</c>), so they cannot
        /// be discriminated by type alone — the discriminator is whether the membership's
        /// <c>OwnedRelatedElement</c> collection is empty.
        /// <para>
        /// Conservative scope: each alternative's elements must be only <see cref="TerminalElement"/>s
        /// plus exactly one <see cref="AssignmentElement"/> with operator <c>+=</c>. Alternatives with
        /// nested groups, multiple <c>+=</c>, or any other element shape are not handled — the caller
        /// then falls back to <c>Build{Rule}HandCoded</c>.
        /// </para>
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write output</param>
        /// <param name="umlClass">The current <see cref="IClass"/></param>
        /// <param name="alternatives">The collection of alternatives to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext"/></param>
        /// <returns><c>true</c> if the pattern matched and code was emitted; <c>false</c> otherwise</returns>
        private static bool TryHandleEmptyVsNonEmptyMembership(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
        {
            if (alternatives.Count != 2)
            {
                return false;
            }

            var branches = new List<(Alternatives Alternative, AssignmentElement Assignment, NonTerminalElement NonTerminal, bool IsEmpty)>();
            string sharedProperty = null;

            foreach (var alternative in alternatives)
            {
                // Only TerminalElements and AssignmentElements are allowed in each alternative
                if (alternative.Elements.Any(element => element is not TerminalElement && element is not AssignmentElement))
                {
                    return false;
                }

                var collectionAssignments = alternative.Elements
                    .OfType<AssignmentElement>()
                    .Where(assignment => assignment.Operator == "+=")
                    .ToList();

                if (collectionAssignments.Count != 1)
                {
                    return false;
                }

                // No `=`, `?=`, or other assignment shapes are allowed beyond the one `+=`
                if (alternative.Elements.OfType<AssignmentElement>().Count() != 1)
                {
                    return false;
                }

                var assignment = collectionAssignments[0];

                if (assignment.Value is not NonTerminalElement nonTerminal)
                {
                    return false;
                }

                if (sharedProperty == null)
                {
                    sharedProperty = assignment.Property;
                }
                else if (sharedProperty != assignment.Property)
                {
                    return false;
                }

                var isEmpty = IsEmptyMembershipRule(nonTerminal.Name, ruleGenerationContext.AllRules);
                branches.Add((alternative, assignment, nonTerminal, isEmpty));
            }

            // Exactly one branch must be the Empty membership; the other must be non-Empty.
            if (branches.Count(b => b.IsEmpty) != 1 || branches.Count(b => !b.IsEmpty) != 1)
            {
                return false;
            }

            var emptyBranch = branches.Single(b => b.IsEmpty);
            var nonEmptyBranch = branches.Single(b => !b.IsEmpty);

            // Both alternatives must resolve to the same .NET cursor type
            // (e.g. both EmptyParameterMember and ExpressionParameterMember target ParameterMembership).
            var emptyTarget = ResolveRuleTargetClass(emptyBranch.NonTerminal, umlClass.Cache, ruleGenerationContext.AllRules);
            var nonEmptyTarget = ResolveRuleTargetClass(nonEmptyBranch.NonTerminal, umlClass.Cache, ruleGenerationContext.AllRules);

            if (emptyTarget == null || nonEmptyTarget == null || emptyTarget != nonEmptyTarget)
            {
                return false;
            }

            // Derive the discriminator property from the empty-membership rule's body.
            // The empty rule wraps its element via a specific property (e.g. `ownedRelatedElement += EmptyUsage`).
            // At runtime, that property having Count == 0 distinguishes the empty membership from a non-empty one.
            var emptyRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == emptyBranch.NonTerminal.Name);
            var emptyRuleAssignment = emptyRule?.Alternatives
                .SelectMany(alternative => alternative.Elements)
                .OfType<AssignmentElement>()
                .FirstOrDefault(assignment => assignment.Operator == "+=");

            if (emptyRuleAssignment == null)
            {
                return false;
            }

            var discriminatorProperty = emptyTarget.QueryAllProperties()
                .SingleOrDefault(property => string.Equals(property.Name, emptyRuleAssignment.Property, StringComparison.OrdinalIgnoreCase));

            if (discriminatorProperty == null)
            {
                return false;
            }

            var discriminatorPropertyName = discriminatorProperty.QueryPropertyNameBasedOnUmlProperties();

            // Declare cursors for both alternatives (idempotent — cursor is shared by property).
            foreach (var alternative in alternatives)
            {
                DeclareAllRequiredCursors(writer, umlClass, alternative, ruleGenerationContext);
            }

            var cursor = ruleGenerationContext.DefinedCursors.FirstOrDefault(c => c.ApplicableRuleElements.Contains(emptyBranch.Assignment));

            if (cursor == null)
            {
                return false;
            }

            var typeName = emptyTarget.QueryFullyQualifiedTypeName();
            var cursorVarName = cursor.CursorVariableName;

            writer.WriteSafeString($"if ({cursorVarName}.Current is {typeName} {{ {discriminatorPropertyName}.Count: 0 }}){Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");
            EmitAlternativeBody(writer, umlClass, emptyBranch.Alternative, ruleGenerationContext);
            writer.WriteSafeString($"}}{Environment.NewLine}");
            writer.WriteSafeString($"else if ({cursorVarName}.Current is {typeName}){Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");
            EmitAlternativeBody(writer, umlClass, nonEmptyBranch.Alternative, ruleGenerationContext);
            writer.WriteSafeString($"}}{Environment.NewLine}");

            return true;
        }

        /// <summary>
        /// Pattern C — detects and emits a poco-runtime-type dispatch where each alternative is
        /// either a bare <see cref="NonTerminalElement"/> or a compound <c>NonTerminal Terminal+</c>
        /// (NonTerminal followed by trailing <see cref="TerminalElement"/>s only). Each NonTerminal
        /// must target a UML <see cref="IClass"/> that is the rule's declaring class or one of its
        /// subclasses; dispatch happens via a <c>switch (poco)</c> on the runtime .NET type.
        /// <para>
        /// This generalises the existing pure-NonTerminal poco-type dispatch in
        /// <see cref="ProcessUnitypedAlternativesWithOneElement"/> by allowing each alternative to
        /// emit additional trailing terminals after the inner sub-rule call. Example:
        /// <c>StateActionUsage : ActionUsage = EmptyActionUsage ';' | StatePerformActionUsage | …</c>
        /// where the first alternative is compound (<c>NonTerminal ';'</c>).
        /// </para>
        /// <para>
        /// Conservative scope: alternatives may not contain <see cref="AssignmentElement"/>,
        /// <see cref="GroupElement"/>, <see cref="NonParsingAssignmentElement"/>, or
        /// <see cref="ValueLiteralElement"/>; only the leading NonTerminal + trailing Terminals.
        /// Target classes must be distinct across alternatives so the dispatch is unambiguous.
        /// </para>
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write output</param>
        /// <param name="umlClass">The current <see cref="IClass"/></param>
        /// <param name="alternatives">The collection of alternatives to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext"/></param>
        /// <returns><c>true</c> if the pattern matched and code was emitted; <c>false</c> otherwise</returns>
        private static bool TryHandlePocoTypeDispatchWithCompoundAlternatives(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
        {
            if (alternatives.Count < 2)
            {
                return false;
            }

            // At least one alternative must be compound (>1 element). For pure-bare-NonTerminal
            // rules the existing ProcessUnitypedAlternativesWithOneElement path handles dispatch.
            if (alternatives.All(alternative => alternative.Elements.Count == 1))
            {
                return false;
            }

            // Validate shape and collect (NonTerminal, target IClass, trailing terminals) per alternative.
            var branches = new List<(Alternatives Alternative, NonTerminalElement NonTerminal, IClass TargetClass)>();

            foreach (var alternative in alternatives)
            {
                if (alternative.Elements.Count == 0
                    || alternative.Elements[0] is not NonTerminalElement leadingNonTerminal)
                {
                    return false;
                }

                for (var elementIndex = 1; elementIndex < alternative.Elements.Count; elementIndex++)
                {
                    if (alternative.Elements[elementIndex] is not TerminalElement trailingTerminal)
                    {
                        return false;
                    }

                    // Optional terminals (e.g. `','?`) cannot be unparsed without rule-specific
                    // domain knowledge: in the unparse direction we have no record of whether the
                    // terminal was present in the original input. Skip the pattern and let the
                    // rule fall back to HandCoded. Example: `OwnedExpression ','?` in
                    // `SequenceExpressionList` — the existing HandCoded body chooses not to emit
                    // the trailing comma at all.
                    if (trailingTerminal.IsOptional)
                    {
                        return false;
                    }
                }

                var targetClass = ResolveRuleTargetClass(leadingNonTerminal, umlClass.Cache, ruleGenerationContext.AllRules);

                if (targetClass == null)
                {
                    return false;
                }

                // Target class must be the rule's declaring class or one of its subclasses
                // (i.e. the dispatch is downward from poco's static type).
                if (targetClass != umlClass && !targetClass.QueryAllGeneralClassifiers().Contains(umlClass))
                {
                    return false;
                }

                branches.Add((alternative, leadingNonTerminal, targetClass));
            }

            // Distinct target classes required; otherwise the switch cases would conflict.
            if (branches.Select(branch => branch.TargetClass).Distinct().Count() != branches.Count)
            {
                return false;
            }

            // Order: most-specific subclasses first; the alternative whose target is the rule's own
            // declaring class is the default fallback.
            var defaultBranch = branches.FirstOrDefault(branch => branch.TargetClass == umlClass);
            var orderedBranches = branches
                .Where(branch => branch.TargetClass != umlClass)
                .OrderByDescending(branch => branch.TargetClass.QueryAllGeneralClassifiers().Count())
                .ToList();

            writer.WriteSafeString($"switch (poco){Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");

            foreach (var (alternative, leadingNonTerminal, targetClass) in orderedBranches)
            {
                var caseVarName = $"poco{targetClass.Name}";
                writer.WriteSafeString($"case {targetClass.QueryFullyQualifiedTypeName()} {caseVarName}:{Environment.NewLine}");
                EmitCompoundPocoTypeBranch(writer, umlClass, leadingNonTerminal, alternative, targetClass, caseVarName, ruleGenerationContext);
                writer.WriteSafeString($"break;{Environment.NewLine}");
            }

            if (defaultBranch.NonTerminal != null)
            {
                writer.WriteSafeString($"default:{Environment.NewLine}");
                EmitCompoundPocoTypeBranch(writer, umlClass, defaultBranch.NonTerminal, defaultBranch.Alternative, defaultBranch.TargetClass, "poco", ruleGenerationContext);
                writer.WriteSafeString($"break;{Environment.NewLine}");
            }

            writer.WriteSafeString($"}}{Environment.NewLine}");
            return true;
        }

        /// <summary>
        /// Pattern D — detects and emits the "reference-or-inline" two-alternative shape commonly used
        /// across SysML2 usage rules (AssertConstraint, SatisfyRequirement, IncludeUseCase, ExhibitState,
        /// EventOccurrence, PerformActionUsageDeclaration). Alt 1 references an existing element via
        /// <c>ownedRelationship += OwnedReferenceSubsetting</c> followed by <c>FeatureSpecializationPart?</c>;
        /// Alt 2 introduces an inline declaration via one or more leading keyword terminals followed by
        /// the inline declaration sub-rule.
        /// <para>Discriminator at runtime: <c>poco.OwnedRelationship.OfType&lt;IReferenceSubsetting&gt;().Any()</c>.
        /// True → emit Alt 1 (reference + optional specialization); false → emit Alt 2
        /// (keyword(s) + declaration).</para>
        /// <para>Each branch reuses <see cref="EmitAlternativeBody"/> which delegates to
        /// <see cref="ProcessRuleElement"/>, so the literal grammar elements drive the emission. The
        /// runtime discriminator type (<c>IReferenceSubsetting</c>) is resolved from the rule's grammar
        /// rather than hardcoded, so the pattern remains correct for any future rule of the same shape.</para>
        /// <para>Conservative scope: alternatives may not contain <see cref="GroupElement"/>s,
        /// <see cref="NonParsingAssignmentElement"/>s, or <see cref="ValueLiteralElement"/>s; Alt 1 must
        /// start with the reference-subsetting <c>+=</c> assignment; Alt 2 must be terminals followed by a
        /// single non-terminal. The 4 "with-extension-keyword" rules (RequirementVerificationUsage,
        /// RequirementConstraintUsage, FramedConcernUsage, ViewRenderingUsage) have a different shape and
        /// are intentionally not handled here.</para>
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write output</param>
        /// <param name="umlClass">The current <see cref="IClass"/></param>
        /// <param name="alternatives">The collection of alternatives to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext"/></param>
        /// <returns><c>true</c> if the pattern matched and code was emitted; <c>false</c> otherwise</returns>
        private static bool TryHandleReferenceOrInline(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
        {
            if (alternatives.Count != 2)
            {
                return false;
            }

            var altList = alternatives.ToList();
            var alt1 = altList[0];
            var alt2 = altList[1];

            // Alt 1: first element must be a `+= NonTerminal` assignment whose target class is resolvable,
            //        and any subsequent elements must be NonTerminals only (e.g. FeatureSpecializationPart?).
            if (alt1.Elements.Count == 0
                || alt1.Elements[0] is not AssignmentElement { Operator: "+=", Value: NonTerminalElement firstNonTerminal } firstAssignment)
            {
                return false;
            }

            var referenceTargetClass = ResolveRuleTargetClass(firstNonTerminal, umlClass.Cache, ruleGenerationContext.AllRules);

            if (referenceTargetClass == null)
            {
                return false;
            }

            // Resolve the collection property that Alt 1's += targets, so we can emit a model-agnostic
            // `poco.{Property}.OfType<{TargetType}>().Any()` discriminator without hardcoding names.
            var collectionProperty = umlClass.QueryAllProperties()
                .SingleOrDefault(property => string.Equals(property.Name, firstAssignment.Property, StringComparison.OrdinalIgnoreCase));

            if (collectionProperty == null || !collectionProperty.QueryIsEnumerable())
            {
                return false;
            }

            for (var elementIndex = 1; elementIndex < alt1.Elements.Count; elementIndex++)
            {
                if (alt1.Elements[elementIndex] is not NonTerminalElement)
                {
                    return false;
                }
            }

            // Alt 2: zero or more leading TerminalElements (no optional terminals) followed by exactly one NonTerminalElement.
            if (alt2.Elements.Count == 0)
            {
                return false;
            }

            var alt2NonTerminalIndex = -1;

            for (var elementIndex = 0; elementIndex < alt2.Elements.Count; elementIndex++)
            {
                var element = alt2.Elements[elementIndex];

                switch (element)
                {
                    case TerminalElement terminalElement:
                        if (terminalElement.IsOptional)
                        {
                            return false;
                        }

                        if (alt2NonTerminalIndex >= 0)
                        {
                            return false;
                        }

                        break;
                    case NonTerminalElement:
                        if (alt2NonTerminalIndex >= 0)
                        {
                            return false;
                        }

                        alt2NonTerminalIndex = elementIndex;
                        break;
                    default:
                        return false;
                }
            }

            if (alt2NonTerminalIndex < 0)
            {
                return false;
            }

            DeclareAllRequiredCursors(writer, umlClass, alt1, ruleGenerationContext);

            var variableName = ruleGenerationContext.CurrentVariableName ?? "poco";
            var collectionPropertyAccessor = collectionProperty.QueryPropertyNameBasedOnUmlProperties();
            var referenceTypeName = referenceTargetClass.QueryFullyQualifiedTypeName();

            writer.WriteSafeString($"if ({variableName}.{collectionPropertyAccessor}.OfType<{referenceTypeName}>().Any()){Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");
            EmitAlternativeBody(writer, umlClass, alt1, ruleGenerationContext);
            writer.WriteSafeString($"}}{Environment.NewLine}");
            writer.WriteSafeString($"else{Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");
            EmitAlternativeBody(writer, umlClass, alt2, ruleGenerationContext);
            writer.WriteSafeString($"}}{Environment.NewLine}");

            return true;
        }

        /// <summary>
        /// Emits the body of a single Pattern C branch: a call to the leading NonTerminal's builder
        /// followed by any trailing terminal literals from the alternative.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write output</param>
        /// <param name="umlClass">The current <see cref="IClass"/> (rule's declaring class)</param>
        /// <param name="leadingNonTerminal">The first element of the alternative — the sub-rule reference</param>
        /// <param name="alternative">The full alternative (used for trailing terminal access)</param>
        /// <param name="targetClass">The resolved target <see cref="IClass"/> for the leading non-terminal</param>
        /// <param name="variableName">The C# variable carrying the cast value (or <c>"poco"</c> for the default branch)</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext"/></param>
        private static void EmitCompoundPocoTypeBranch(EncodedTextWriter writer, IClass umlClass, NonTerminalElement leadingNonTerminal, Alternatives alternative, IClass targetClass, string variableName, RuleGenerationContext ruleGenerationContext)
        {
            string builderCall;

            if (targetClass == umlClass)
            {
                builderCall = $"Build{leadingNonTerminal.Name}({variableName}, cursorCache, stringBuilder);";
            }
            else
            {
                builderCall = $"{targetClass.Name}TextualNotationBuilder.Build{leadingNonTerminal.Name}({variableName}, cursorCache, stringBuilder);";
            }

            writer.WriteSafeString($"{builderCall}{Environment.NewLine}");

            for (var elementIndex = 1; elementIndex < alternative.Elements.Count; elementIndex++)
            {
                if (alternative.Elements[elementIndex] is TerminalElement trailingTerminal)
                {
                    WriteTerminalAppend(writer, trailingTerminal.Value);
                    writer.WriteSafeString(Environment.NewLine);
                }
            }
        }

        /// <summary>
        /// Emits the body of a single alternative — terminals plus the one <c>+=</c> assignment —
        /// reusing the existing <see cref="ProcessRuleElement"/> infrastructure. The caller is
        /// responsible for any surrounding discriminator <c>if</c>/<c>else</c> guard.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write output</param>
        /// <param name="umlClass">The current <see cref="IClass"/></param>
        /// <param name="alternative">The <see cref="Alternatives"/> whose elements are emitted</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext"/></param>
        private static void EmitAlternativeBody(EncodedTextWriter writer, IClass umlClass, Alternatives alternative, RuleGenerationContext ruleGenerationContext)
        {
            var previousSiblings = ruleGenerationContext.CurrentSiblingElements;
            var previousIndex = ruleGenerationContext.CurrentElementIndex;
            ruleGenerationContext.CurrentSiblingElements = alternative.Elements;

            for (var elementIndex = 0; elementIndex < alternative.Elements.Count; elementIndex++)
            {
                ruleGenerationContext.CurrentElementIndex = elementIndex;
                ProcessRuleElement(writer, umlClass, alternative.Elements[elementIndex], ruleGenerationContext);
            }

            ruleGenerationContext.CurrentSiblingElements = previousSiblings;
            ruleGenerationContext.CurrentElementIndex = previousIndex;
        }

        /// <summary>
        /// Determines whether a grammar rule represents an "empty membership" — one whose body
        /// dispatches to a single <c>Empty*</c>-named non-terminal that wraps an <see cref="EmptyUsage"/>.
        /// The naming convention (e.g. <c>EmptyParameterMember</c>, <c>EmptyResultMember</c>,
        /// <c>EmptyFeatureMember</c>) is a stable contract in both KEBNF files; the implementation
        /// also verifies the structural shape so that an accidentally-named rule in the future
        /// would not be mis-classified.
        /// </summary>
        /// <param name="ruleName">The non-terminal name to test</param>
        /// <param name="allRules">All available rules, used to look up <paramref name="ruleName"/></param>
        /// <returns><c>true</c> if the rule conforms to the empty-membership shape</returns>
        private static bool IsEmptyMembershipRule(string ruleName, IReadOnlyList<TextualNotationRule> allRules)
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
        /// Resolves the UML <see cref="IClass"/> targeted by a non-terminal rule reference, using
        /// the <c>TargetElementName</c> declared by the rule (or the rule's own name as a fallback).
        /// Returns <c>null</c> when the target class is not present in the cache.
        /// </summary>
        /// <param name="nonTerminal">The <see cref="NonTerminalElement"/> whose target class is sought</param>
        /// <param name="cache">The <see cref="IXmiElementCache"/> used to look up the class</param>
        /// <param name="allRules">All available rules</param>
        /// <returns>The resolved <see cref="IClass"/>, or <c>null</c> if not found</returns>
        private static IClass ResolveRuleTargetClass(NonTerminalElement nonTerminal, IXmiElementCache cache, IReadOnlyList<TextualNotationRule> allRules)
        {
            var rule = allRules.SingleOrDefault(x => x.RuleName == nonTerminal.Name);
            var typeTarget = rule != null
                ? (rule.TargetElementName ?? rule.RuleName)
                : nonTerminal.Name;

            return cache.Values.OfType<INamedElement>().SingleOrDefault(x => x.Name == typeTarget) as IClass;
        }

        /// <summary>
        /// Validates that every <c>+=NonTerminal</c> assignment in an alternative's tail (i.e. all elements
        /// after the leading <c>operator = X</c> assignment) resolves to a target class that exists in the
        /// UML cache. If any tail assignment references a rule whose target class is missing
        /// (for example <c>TypeResultMember : ResultParameterMembership = …</c> when no
        /// <c>ResultParameterMembership</c> class exists), the standard emitter would inline the rule's
        /// body without a proper cursor cast, producing uncompilable references to non-existent properties
        /// or builders. In that case the caller should skip the matching pattern and fall back to
        /// <c>Build{Rule}HandCoded</c>.
        /// </summary>
        /// <param name="elements">The elements of an alternative — the leading operator assignment is skipped</param>
        /// <param name="umlClass">The current <see cref="IClass"/></param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext"/></param>
        /// <returns><c>true</c> if every trailing <c>+=NonTerminal</c> can be processed safely</returns>
        private static bool AreAlternativeTailElementsProcessable(IReadOnlyList<RuleElement> elements, IClass umlClass, RuleGenerationContext ruleGenerationContext)
        {
            for (var elementIndex = 1; elementIndex < elements.Count; elementIndex++)
            {
                if (elements[elementIndex] is not AssignmentElement { Operator: "+=", Value: NonTerminalElement nonTerminal })
                {
                    continue;
                }

                var referencedRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == nonTerminal.Name);
                var typeTarget = referencedRule != null
                    ? (referencedRule.TargetElementName ?? referencedRule.RuleName)
                    : nonTerminal.Name;

                if (typeTarget == ruleGenerationContext.NamedElementToGenerate.Name)
                {
                    continue;
                }

                var targetClassExists = umlClass.Cache.Values.OfType<INamedElement>().Any(x => x.Name == typeTarget);

                if (!targetClassExists)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Extracts the set of literal terminal values that a grammar value may take, used by
        /// <see cref="TryHandleOperatorLiteralAlternation"/> to recognise the right-hand side of an
        /// <c>operator = X</c> assignment as a closed set of literals.
        /// </summary>
        /// <param name="value">The right-hand-side <see cref="RuleElement"/> of the assignment</param>
        /// <param name="allRules">All available rules, used to resolve <see cref="NonTerminalElement"/> references</param>
        /// <returns>
        /// A list of literal strings (without surrounding quotes) when the value is a terminal,
        /// a non-quoted value literal, or a non-terminal whose RHS is a pure literal alternation;
        /// <c>null</c> otherwise.
        /// </returns>
        private static List<string> ExtractLiteralAlternation(RuleElement value, IReadOnlyList<TextualNotationRule> allRules)
        {
            switch (value)
            {
                case TerminalElement terminal:
                    return [terminal.Value];

                case NonTerminalElement nonTerminal:
                    var referencedRule = allRules.SingleOrDefault(x => x.RuleName == nonTerminal.Name);

                    if (referencedRule == null)
                    {
                        return null;
                    }

                    var literals = new List<string>();

                    foreach (var ruleAlternative in referencedRule.Alternatives)
                    {
                        if (ruleAlternative.Elements.Count != 1 || ruleAlternative.Elements[0] is not TerminalElement nestedTerminal)
                        {
                            return null;
                        }

                        literals.Add(nestedTerminal.Value);
                    }

                    return literals.Count == 0 ? null : literals;

                default:
                    return null;
            }
        }

        /// <summary>
        /// Processes <see cref="Alternatives"/> for a <see cref="TextualNotationRule"/> that is a multicollection assignment
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass"/></param>
        /// <param name="alternatives">The collection of alternatives to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext"/></param>
        private static void ProcessMultiCollectionAssignment(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
        {
            // Multi-collection assignment: alternatives assign += to different properties
            // Each alternative is processed with cursor-based access on its respective property
            var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
            writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
        }

        /// <summary>
        /// Process multiple <see cref="Alternatives"/> when all of them only have one <see cref="RuleElement"/>
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass"/></param>
        /// <param name="alternatives">The collection of alternatives to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext"/></param>
        private static void ProcessUnitypedAlternativesWithOneElement(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
        {
            var firstRuleElement = alternatives.ElementAt(0).Elements[0];

            switch (firstRuleElement)
            {
                case TerminalElement terminalElement:
                    WriteTerminalAppendWithLeadingSpace(writer, terminalElement.Value);
                    break;
                case NonTerminalElement:
                {
                    var nonTerminalElements = alternatives.SelectMany(x => x.Elements).OfType<NonTerminalElement>().ToList();
                    var mappedNonTerminalElements = OrderElementsByInheritance(nonTerminalElements, umlClass.Cache, ruleGenerationContext);

                    // Build a lookup of duplicate UML classes to determine which cases need when guards
                    var duplicateClasses = mappedNonTerminalElements
                        .GroupBy(x => x.UmlClass)
                        .Where(g => g.Count() > 1)
                        .ToDictionary(g => g.Key, g => g.ToList());

                    // For each duplicate group, resolve boolean ?= properties to use as when guards.
                    // Compute properties unique to each element vs all others in the group.
                    var whenGuards = new Dictionary<NonTerminalElement, string>();

                    foreach (var duplicateGroup in duplicateClasses)
                    {
                        var allProperties = duplicateGroup.Key.QueryAllProperties();

                        // Compute boolean ?= properties for each element in the group
                        var elementBoolProps = new List<(NonTerminalElement RuleElement, List<string> BoolProps)>();

                        foreach (var element in duplicateGroup.Value)
                        {
                            var referencedRule = ruleGenerationContext.AllRules.Single(x => x.RuleName == element.RuleElement.Name);
                            var booleanProperties = QueryBooleanAssignmentProperties(referencedRule, ruleGenerationContext.AllRules);
                            elementBoolProps.Add((element.RuleElement, booleanProperties));
                        }

                        // Build the union of all other elements' properties for each element
                        for (var elementIndex = 0; elementIndex < elementBoolProps.Count; elementIndex++)
                        {
                            var current = elementBoolProps[elementIndex];
                            var othersProperties = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                            for (var otherIndex = 0; otherIndex < elementBoolProps.Count; otherIndex++)
                            {
                                if (otherIndex != elementIndex)
                                {
                                    foreach (var prop in elementBoolProps[otherIndex].BoolProps)
                                    {
                                        othersProperties.Add(prop);
                                    }
                                }
                            }

                            var uniqueProperties = current.BoolProps.Where(p => !othersProperties.Contains(p)).ToList();

                            if (uniqueProperties.Count > 0)
                            {
                                var guardParts = new List<string>();

                                foreach (var boolProp in uniqueProperties)
                                {
                                    var umlProperty = allProperties.FirstOrDefault(x => string.Equals(x.Name, boolProp, StringComparison.OrdinalIgnoreCase));

                                    if (umlProperty != null)
                                    {
                                        guardParts.Add($"{{0}}.{umlProperty.QueryPropertyNameBasedOnUmlProperties()}");
                                    }
                                }

                                if (guardParts.Count > 0)
                                {
                                    whenGuards[current.RuleElement] = guardParts[0];
                                }
                            }
                        }

                        // Ensure exactly one element in the group has no when guard (the fallback).
                        // If all elements have guards, remove the guard from the one with the most unique properties
                        // (the most general rule, which should match when no specific guard applies).
                        var elementsWithGuards = duplicateGroup.Value.Where(e => whenGuards.ContainsKey(e.RuleElement)).ToList();

                        if (elementsWithGuards.Count == duplicateGroup.Value.Count)
                        {
                            // All elements have guards — remove the guard from the element with fewest boolean properties (most generic)
                            var fallbackElement = duplicateGroup.Value
                                .OrderBy(element => elementBoolProps
                                    .Single(bp => bp.RuleElement == element.RuleElement).BoolProps.Count)
                                .First();
                            whenGuards.Remove(fallbackElement.RuleElement);
                        }

                        // Reorder the duplicate group: elements with when guards first, general (no guard) last
                        duplicateGroup.Value.Sort((a, b) =>
                        {
                            var aHasGuard = whenGuards.ContainsKey(a.RuleElement);
                            var bHasGuard = whenGuards.ContainsKey(b.RuleElement);

                            if (aHasGuard && !bHasGuard) return -1;
                            if (!aHasGuard && bHasGuard) return 1;
                            return 0;
                        });
                    }

                    // Check for unresolvable duplicates: more than one element without a when guard in a group
                    var hasUnresolvableDuplicates = duplicateClasses.Any(group =>
                        group.Value.Count(element => !whenGuards.ContainsKey(element.RuleElement)) > 1);

                    if (hasUnresolvableDuplicates)
                    {
                        // For unresolvable duplicate groups, use IsValidFor{RuleName}() as when guards
                        foreach (var duplicateGroup in duplicateClasses)
                        {
                            var unguardedElements = duplicateGroup.Value
                                .Where(element => !whenGuards.ContainsKey(element.RuleElement))
                                .ToList();

                            if (unguardedElements.Count > 1)
                            {
                                // Add IsValidFor guards to all but the last unguarded element (fallback)
                                for (var elementIndex = 0; elementIndex < unguardedElements.Count - 1; elementIndex++)
                                {
                                    var element = unguardedElements[elementIndex];
                                    whenGuards[element.RuleElement] = $"{{0}}.IsValidFor{element.RuleElement.Name}()";
                                }
                            }
                        }
                    }

                    // Rebuild the overall ordered list respecting the reordered duplicate groups
                    var reorderedElements = new List<(NonTerminalElement RuleElement, IClass UmlClass)>();
                    var processedDuplicateClasses = new HashSet<IClass>();

                    foreach (var element in mappedNonTerminalElements)
                    {
                        if (duplicateClasses.TryGetValue(element.UmlClass, out var duplicateGroup))
                        {
                            if (processedDuplicateClasses.Add(element.UmlClass))
                            {
                                // Insert all elements of this duplicate group in their reordered sequence
                                reorderedElements.AddRange(duplicateGroup);
                            }
                        }
                        else
                        {
                            reorderedElements.Add(element);
                        }
                    }

                    mappedNonTerminalElements = reorderedElements;

                    // Re-sort to ensure proper switch ordering: more specific types first,
                    // default case last. This prevents a superclass case (e.g., IType) from
                    // catching everything before guarded subclass cases (e.g., IFeature when ...).
                    var defaultElement = mappedNonTerminalElements
                        .LastOrDefault(x => x.UmlClass == ruleGenerationContext.NamedElementToGenerate && !whenGuards.ContainsKey(x.RuleElement));

                    mappedNonTerminalElements.Sort((a, b) =>
                    {
                        var aIsDefault = defaultElement.RuleElement != null && a.RuleElement == defaultElement.RuleElement;
                        var bIsDefault = defaultElement.RuleElement != null && b.RuleElement == defaultElement.RuleElement;

                        if (aIsDefault && !bIsDefault) return 1;
                        if (bIsDefault && !aIsDefault) return -1;

                        var depthA = a.UmlClass.QueryAllGeneralClassifiers().Count();
                        var depthB = b.UmlClass.QueryAllGeneralClassifiers().Count();

                        return depthB.CompareTo(depthA);
                    });

                    var variableName = "poco";

                    if (ruleGenerationContext.CallerRule is AssignmentElement assignmentElement)
                    {
                        var cursorToUse = ruleGenerationContext.DefinedCursors.Single(x => x.ApplicableRuleElements.Contains(assignmentElement));
                        variableName = $"{cursorToUse.CursorVariableName}.Current";
                    }

                    // Detect degenerate switch: if the first non-default case type is a supertype
                    // of the generating class, it will always match, making subsequent cases unreachable.
                    // This happens when alternatives like (SuperclassingPart | ConjugationPart) both
                    // target ancestor types of the POCO — the discriminator should be cursor-based,
                    // not POCO-type-based. Fall back to HandCoded in this case.
                    var generatingClass = ruleGenerationContext.NamedElementToGenerate as IClass;

                    if (variableName == "poco" && generatingClass != null)
                    {
                        var firstNonDefault = mappedNonTerminalElements.FirstOrDefault(x =>
                            defaultElement.RuleElement == null || x.RuleElement != defaultElement.RuleElement);

                        if (firstNonDefault.UmlClass != null
                            && firstNonDefault.UmlClass != generatingClass
                            && generatingClass.QueryAllGeneralClassifiers().Contains(firstNonDefault.UmlClass)
                            && !whenGuards.Any())
                        {
                            var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule?.RuleName ?? "Unknown";

                            if (ruleGenerationContext.EmittedHandCodedCalls.Add(handCodedRuleName))
                            {
                                writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                            }

                            break;
                        }
                    }

                    writer.WriteSafeString($"switch({variableName}){Environment.NewLine}");
                    writer.WriteSafeString("{");

                    // defaultElement was already determined above during the re-sort

                    foreach (var orderedNonTerminalElement in mappedNonTerminalElements)
                    {
                        var previousVariableName = ruleGenerationContext.CurrentVariableName;
                        var hasWhenGuard = whenGuards.TryGetValue(orderedNonTerminalElement.RuleElement, out var guardTemplate);

                        if (orderedNonTerminalElement.RuleElement == defaultElement.RuleElement && !hasWhenGuard)
                        {
                            writer.WriteSafeString($"default:{Environment.NewLine}");
                            ruleGenerationContext.CurrentVariableName = variableName;
                        }
                        else if (hasWhenGuard)
                        {
                            // Case with when guard for disambiguation
                            var guardVarName = $"poco{orderedNonTerminalElement.UmlClass.Name}{orderedNonTerminalElement.RuleElement.Name}";
                            var resolvedGuard = string.Format(guardTemplate, guardVarName);
                            writer.WriteSafeString($"case {orderedNonTerminalElement.UmlClass.QueryFullyQualifiedTypeName()} {guardVarName} when {resolvedGuard}:{Environment.NewLine}");
                            ruleGenerationContext.CurrentVariableName = guardVarName;
                        }
                        else
                        {
                            var caseVarName = $"poco{orderedNonTerminalElement.UmlClass.Name}";
                            writer.WriteSafeString($"case {orderedNonTerminalElement.UmlClass.QueryFullyQualifiedTypeName()} {caseVarName}:{Environment.NewLine}");
                            ruleGenerationContext.CurrentVariableName = caseVarName;
                        }

                        var previousCaller = ruleGenerationContext.CallerRule;
                        ruleGenerationContext.CallerRule = orderedNonTerminalElement.RuleElement;

                        ProcessNonTerminalElement(writer, orderedNonTerminalElement.UmlClass, orderedNonTerminalElement.RuleElement, ruleGenerationContext);

                        ruleGenerationContext.CallerRule = previousCaller;
                        ruleGenerationContext.CurrentVariableName = previousVariableName;
                        writer.WriteSafeString($"{Environment.NewLine}break;{Environment.NewLine}");
                    }

                    writer.WriteSafeString($"}}{Environment.NewLine}");

                    if (ruleGenerationContext.CallerRule is AssignmentElement assignmentElementForMove)
                    {
                        var cursorForMove = ruleGenerationContext.DefinedCursors.Single(x => x.ApplicableRuleElements.Contains(assignmentElementForMove));
                        writer.WriteSafeString($"{cursorForMove.CursorVariableName}.Move();{Environment.NewLine}");
                    }

                    break;
                }
                case AssignmentElement:
                    var assignmentElements = alternatives.SelectMany(x => x.Elements).OfType<AssignmentElement>().ToList();
                    var propertiesTarget = assignmentElements.Select(x => x.Property).Distinct().ToList();

                    if (propertiesTarget.Count == 1)
                    {
                        var targetProperty = umlClass.QueryAllProperties().Single(x => string.Equals(x.Name, propertiesTarget[0]));

                        if (assignmentElements.All(x => x.Operator == "+=") && assignmentElements.All(x => x.Value is NonTerminalElement))
                        {
                            // Dispatcher rule: multiple alternatives all += to same property
                            // Use cursor-based switch on cursor.Current type
                            var orderElementsByInheritance = OrderElementsByInheritance(assignmentElements.Select(x => x.Value).OfType<NonTerminalElement>().ToList(), umlClass.Cache, ruleGenerationContext);

                            // Declare cursor for this property via cursorCache
                            var cursorVarName = $"{targetProperty.Name.LowerCaseFirstLetter()}Cursor";
                            var existingCursor = ruleGenerationContext.DefinedCursors.FirstOrDefault(x => x.IsCursorValidForProperty(targetProperty));

                            if (existingCursor == null)
                            {
                                writer.WriteSafeString($"var {cursorVarName} = cursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{targetProperty.QueryPropertyNameBasedOnUmlProperties()});{Environment.NewLine}");
                                var cursorDef = new CursorDefinition { DefinedForProperty = targetProperty };

                                foreach (var assignmentElement in assignmentElements)
                                {
                                    cursorDef.ApplicableRuleElements.Add(assignmentElement);
                                }

                                ruleGenerationContext.DefinedCursors.Add(cursorDef);
                            }
                            else
                            {
                                cursorVarName = existingCursor.CursorVariableName;

                                foreach (var assignmentElement in assignmentElements)
                                {
                                    existingCursor.ApplicableRuleElements.Add(assignmentElement);
                                }
                            }

                            writer.WriteSafeString($"switch({cursorVarName}.Current){Environment.NewLine}");
                            writer.WriteSafeString($"{{{Environment.NewLine}");

                            foreach (var orderedElement in orderElementsByInheritance)
                            {
                                var numberOfElementOfSameType = orderElementsByInheritance.Count(x => x.UmlClass == orderedElement.UmlClass);

                                if (numberOfElementOfSameType == 1)
                                {
                                    writer.WriteSafeString($"case {orderedElement.UmlClass.QueryFullyQualifiedTypeName()} {orderedElement.UmlClass.Name.LowerCaseFirstLetter()}:{Environment.NewLine}");
                                }
                                else
                                {
                                    writer.WriteSafeString($"case {orderedElement.UmlClass.QueryFullyQualifiedTypeName()} {orderedElement.UmlClass.Name.LowerCaseFirstLetter()} when {orderedElement.UmlClass.Name.LowerCaseFirstLetter()}.IsValidFor{orderedElement.RuleElement.Name}():{Environment.NewLine}");
                                }

                                var previousVariableName = ruleGenerationContext.CurrentVariableName;
                                ruleGenerationContext.CurrentVariableName = orderedElement.UmlClass.Name.LowerCaseFirstLetter();
                                ProcessNonTerminalElement(writer, orderedElement.UmlClass, orderedElement.RuleElement, ruleGenerationContext);
                                ruleGenerationContext.CurrentVariableName = previousVariableName;
                                writer.WriteSafeString($"{Environment.NewLine}{cursorVarName}.Move();{Environment.NewLine}");
                                writer.WriteSafeString($"break;{Environment.NewLine}");
                            }

                            // Safety default: always advance the cursor even when no case matches.
                            // This prevents infinite loops when the dispatcher is called from a while loop.
                            writer.WriteSafeString($"default:{Environment.NewLine}");
                            writer.WriteSafeString($"{cursorVarName}.Move();{Environment.NewLine}");
                            writer.WriteSafeString($"break;{Environment.NewLine}");
                            writer.WriteSafeString($"}}{Environment.NewLine}");
                        }
                        else
                        {
                            // Mixed operator alternatives on same property - use cursor-based switch
                            var orderElementsByInheritance = OrderElementsByInheritance(assignmentElements.Select(x => x.Value).OfType<NonTerminalElement>().ToList(), umlClass.Cache, ruleGenerationContext);

                            var cursorVarName = $"{targetProperty.Name.LowerCaseFirstLetter()}Cursor";
                            var existingCursor = ruleGenerationContext.DefinedCursors.FirstOrDefault(x => x.IsCursorValidForProperty(targetProperty));

                            if (existingCursor == null)
                            {
                                writer.WriteSafeString($"var {cursorVarName} = cursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{targetProperty.QueryPropertyNameBasedOnUmlProperties()});{Environment.NewLine}");
                                var cursorDef = new CursorDefinition { DefinedForProperty = targetProperty };

                                foreach (var assignmentElement in assignmentElements)
                                {
                                    cursorDef.ApplicableRuleElements.Add(assignmentElement);
                                }

                                ruleGenerationContext.DefinedCursors.Add(cursorDef);
                            }
                            else
                            {
                                cursorVarName = existingCursor.CursorVariableName;
                            }

                            writer.WriteSafeString($"if({cursorVarName}.Current != null){Environment.NewLine}");
                            writer.WriteSafeString($"{{{Environment.NewLine}");
                            writer.WriteSafeString($"switch({cursorVarName}.Current){Environment.NewLine}");
                            writer.WriteSafeString($"{{{Environment.NewLine}");

                            foreach (var orderedElement in orderElementsByInheritance)
                            {
                                if (orderedElement.UmlClass.Name == "Element")
                                {
                                    writer.WriteSafeString($"case {{ }} {orderedElement.UmlClass.Name.LowerCaseFirstLetter()}:{Environment.NewLine}");
                                }
                                else
                                {
                                    writer.WriteSafeString($"case {orderedElement.UmlClass.QueryFullyQualifiedTypeName()} {orderedElement.UmlClass.Name.LowerCaseFirstLetter()}:{Environment.NewLine}");
                                }

                                var previousVariableName = ruleGenerationContext.CurrentVariableName;
                                ruleGenerationContext.CurrentVariableName = orderedElement.UmlClass.Name.LowerCaseFirstLetter();
                                ProcessNonTerminalElement(writer, orderedElement.UmlClass, orderedElement.RuleElement, ruleGenerationContext);
                                ruleGenerationContext.CurrentVariableName = previousVariableName;
                                writer.WriteSafeString($"{Environment.NewLine}break;{Environment.NewLine}");
                            }

                            writer.WriteSafeString($"}}{Environment.NewLine}");
                            writer.WriteSafeString($"{cursorVarName}.Move();{Environment.NewLine}");
                            writer.WriteSafeString($"}}{Environment.NewLine}");
                        }
                    }
                    else
                    {
                        foreach (var alternative in alternatives)
                        {
                            DeclareAllRequiredCursors(writer, umlClass, alternative, ruleGenerationContext);
                        }

                        var properties = umlClass.QueryAllProperties();

                        for (var alternativeIndex = 0; alternativeIndex < alternatives.Count; alternativeIndex++)
                        {
                            if (alternativeIndex != 0)
                            {
                                writer.WriteSafeString("else ");
                            }

                            var assignment = assignmentElements[alternativeIndex];
                            var targetProperty = properties.Single(x => string.Equals(x.Name, assignment.Property));

                            var iterator = ruleGenerationContext.DefinedCursors.SingleOrDefault(x => x.ApplicableRuleElements.Contains(assignment));

                            if (assignment.Operator != "+=")
                            {
                                writer.WriteSafeString($"if({targetProperty.QueryIfStatementContentForNonEmpty(iterator?.CursorVariableName ?? "poco")}){Environment.NewLine}");
                            }

                            writer.WriteSafeString($"{{{Environment.NewLine}");
                            ProcessAssignmentElement(writer, umlClass, ruleGenerationContext, assignment, true);
                            writer.WriteSafeString($"{Environment.NewLine}}}{Environment.NewLine}");
                        }
                    }

                    break;
                default:
                {
                    var defaultHandCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                    writer.WriteSafeString($"Build{defaultHandCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                    break;
                }
            }
        }

        /// <summary>
        /// Extracts all boolean property names (assigned via <c>?=</c>) from the elements of a grammar rule.
        /// These properties can be used as <c>when</c> guards to disambiguate switch cases that share the same UML class type.
        /// </summary>
        /// <param name="rule">The <see cref="TextualNotationRule"/> to inspect</param>
        /// <param name="allRules">All available rules for recursive resolution of NonTerminal elements</param>
        /// <returns>A list of property names that are boolean-assigned in this rule</returns>
        private static List<string> QueryBooleanAssignmentProperties(TextualNotationRule rule, IReadOnlyList<TextualNotationRule> allRules)
        {
            var result = new List<string>();
            CollectBooleanAssignmentProperties(rule.Alternatives.SelectMany(x => x.Elements).ToList(), allRules, result, new HashSet<string>());
            return result;
        }

        /// <summary>
        /// Recursively collects boolean <c>?=</c> assignment property names from a list of <see cref="RuleElement"/>
        /// </summary>
        /// <param name="elements">The elements to inspect</param>
        /// <param name="allRules">All available rules for resolving NonTerminal references</param>
        /// <param name="result">The accumulated list of boolean property names</param>
        /// <param name="visited">Set of already-visited rule names to prevent infinite recursion</param>
        private static void CollectBooleanAssignmentProperties(IReadOnlyList<RuleElement> elements, IReadOnlyList<TextualNotationRule> allRules, List<string> result, HashSet<string> visited)
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
        /// Orders a collection of <see cref="NonTerminalElement"/> based on the inheritance ordering, to build switch expression
        /// </summary>
        /// <param name="nonTerminalElements">The collection of <see cref="NonTerminalElement"/> to order</param>
        /// <param name="cache">The <see cref="IXmiElementCache"/></param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext"/></param>
        /// <returns>The collection of ordered <see cref="NonTerminalElement"/> with the associated <see cref="IClass"/></returns>
        private static List<(NonTerminalElement RuleElement, IClass UmlClass)> OrderElementsByInheritance(List<NonTerminalElement> nonTerminalElements, IXmiElementCache cache, RuleGenerationContext ruleGenerationContext)
        {
            var mapping = new List<(NonTerminalElement RuleElement, IClass UmlClass)>();
            var elementClass = cache.Values.Single(x => x is IClass { Name: "Element" });
            
            foreach (var nonTerminalElement in nonTerminalElements)
            {
                var referencedRule = ruleGenerationContext.AllRules.Single(x => x.RuleName == nonTerminalElement.Name);
                var referencedClassName = referencedRule.TargetElementName ?? referencedRule.RuleName;
                var referencedClass =(IClass)(cache.Values.SingleOrDefault(x => x is IClass umlClass && umlClass.Name == referencedClassName)?? elementClass);
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

                if (aIsNamed && !bIsNamed) return 1;
                if (bIsNamed && !aIsNamed) return -1;

                if (a.UmlClass == b.UmlClass) return 0;

                // Sort by inheritance depth (more specific types first).
                // Any subclass has strictly more ancestors than its superclass,
                // so depth-based ordering is transitive and correct for switch case ordering.
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

        /// <summary>
        /// Declares all required cursors for all <see cref="RuleElement"/> present inside an <see cref="Alternatives"/>
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass"/></param>
        /// <param name="alternatives">The <see cref="Alternatives"/> to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext"/></param>
        private static void DeclareAllRequiredCursors(EncodedTextWriter writer, IClass umlClass, Alternatives alternatives, RuleGenerationContext ruleGenerationContext)
        {
            foreach (var ruleElement in alternatives.Elements)
            {
                switch (ruleElement)
                {
                    case AssignmentElement assignmentElement:
                        DeclareCursorIfRequired(writer, umlClass, assignmentElement, ruleGenerationContext);
                        break;
                    case GroupElement groupElement:
                        foreach (var groupElementAlternative in groupElement.Alternatives)
                        {
                            DeclareAllRequiredCursors(writer, umlClass, groupElementAlternative, ruleGenerationContext);
                        }
                        
                        break;
                }
            }
        }

        /// <summary>
        /// Processes a <see cref="RuleElement"/>
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass"/></param>
        /// <param name="textualRuleElement">The <see cref="RuleElement"/> to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext"/></param>
        /// <param name="isPartOfMultipleAlternative"></param>
        /// <exception cref="ArgumentException">If the type of the <see cref="RuleElement"/> is not supported</exception>
        private static void ProcessRuleElement(EncodedTextWriter writer, IClass umlClass, RuleElement textualRuleElement, RuleGenerationContext ruleGenerationContext, bool isPartOfMultipleAlternative = false)
        {
            switch (textualRuleElement)
            {
                case TerminalElement terminalElement:
                    WriteTerminalAppend(writer, terminalElement.Value);
                    break;
                case NonTerminalElement nonTerminalElement:
                    if (ruleGenerationContext.CallerRule is NonTerminalElement { Container: AssignmentElement assignmentElementContainer })
                    {
                        var textualBuilderClass = ruleGenerationContext.NamedElementToGenerate as IClass;
                        var assignedProperty = textualBuilderClass.QueryAllProperties().SingleOrDefault(x => x.Name == assignmentElementContainer.Property);
                        ruleGenerationContext.CurrentVariableName = assignedProperty == null ? "poco" : $"poco.{assignedProperty.QueryPropertyNameBasedOnUmlProperties()}";
                    }
                    else
                    {
                        ruleGenerationContext.CurrentVariableName = "poco";
                    }

                    ProcessNonTerminalElement(writer, umlClass, nonTerminalElement,ruleGenerationContext, isPartOfMultipleAlternative);

                    break;
                case GroupElement groupElement:
                    ruleGenerationContext.CallerRule = groupElement ;

                    if (groupElement.IsCollection && groupElement.Alternatives.Count == 1)
                    {
                        var assignmentRule = groupElement.Alternatives.SelectMany(x => x.Elements).FirstOrDefault(x => x is AssignmentElement { Value: NonTerminalElement } || x is AssignmentElement {Value: ValueLiteralElement});

                        if (assignmentRule is AssignmentElement assignmentElement)
                        {
                            var cursorToUse = ruleGenerationContext.DefinedCursors.Single(x => x.ApplicableRuleElements.Contains(assignmentElement));

                            // Resolve the assignment's target type to emit a type-guarded while condition.
                            // This prevents greedy consumption of non-matching elements (Golden Rule: Move() ↔ += of matching type only).
                            var groupTypeGuard = "";

                            if (assignmentElement.Value is NonTerminalElement valueNonTerminal)
                            {
                                var referencedRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == valueNonTerminal.Name);
                                var typeTarget = referencedRule != null ? (referencedRule.TargetElementName ?? referencedRule.RuleName) : null;

                                if (typeTarget != null)
                                {
                                    var targetClass = umlClass.Cache.Values.OfType<INamedElement>().SingleOrDefault(x => x.Name == typeTarget) as IClass;

                                    if (targetClass != null)
                                    {
                                        groupTypeGuard = $" && {cursorToUse.CursorVariableName}.Current is {targetClass.QueryFullyQualifiedTypeName()}";
                                    }
                                }
                            }

                            writer.WriteSafeString($"{Environment.NewLine}while({cursorToUse.CursorVariableName}.Current != null{groupTypeGuard}){Environment.NewLine}");
                        }

                        writer.WriteSafeString($"{{{Environment.NewLine}");
                        ProcessAlternatives(writer, umlClass, groupElement.Alternatives, ruleGenerationContext);

                        if (assignmentRule is AssignmentElement assignmentElementForMove)
                        {
                            var cursorToUse = ruleGenerationContext.DefinedCursors.Single(x => x.ApplicableRuleElements.Contains(assignmentElementForMove));
                            writer.WriteSafeString($"{cursorToUse.CursorVariableName}.Move();{Environment.NewLine}");
                        }

                        writer.WriteSafeString($"{Environment.NewLine}}}");
                    }
                    else if (groupElement.IsCollection)
                    {
                        // Collection group with += assignments: generate a while loop with cursor-based switch
                        // e.g., (ownedRelationship+=DefinitionMember|ownedRelationship+=AliasMember|ownedRelationship+=Import)*
                        var groupAssignments = groupElement.Alternatives
                            .SelectMany(alternative => alternative.Elements)
                            .OfType<AssignmentElement>()
                            .Where(assignment => assignment.Operator == "+=")
                            .ToList();

                        var groupNonTerminals = groupAssignments
                            .Select(assignment => assignment.Value)
                            .OfType<NonTerminalElement>()
                            .ToList();

                        if (groupAssignments.Count > 0 && groupNonTerminals.Count == groupAssignments.Count)
                        {
                            var groupPropertyName = groupAssignments[0].Property;
                            var groupTargetProperty = umlClass.QueryAllProperties().SingleOrDefault(x => string.Equals(x.Name, groupPropertyName, StringComparison.OrdinalIgnoreCase));

                            if (groupTargetProperty != null)
                            {
                                var groupCursorVarName = $"{groupTargetProperty.Name.LowerCaseFirstLetter()}Cursor";
                                var existingGroupCursor = ruleGenerationContext.DefinedCursors.FirstOrDefault(x => x.IsCursorValidForProperty(groupTargetProperty));

                                if (existingGroupCursor == null)
                                {
                                    var groupPropertyAccessName = groupTargetProperty.QueryPropertyNameBasedOnUmlProperties();
                                    writer.WriteSafeString($"var {groupCursorVarName} = cursorCache.GetOrCreateCursor(poco.Id, \"{groupTargetProperty.Name}\", poco.{groupPropertyAccessName});{Environment.NewLine}");
                                    var groupCursorDef = new CursorDefinition { DefinedForProperty = groupTargetProperty };

                                    foreach (var groupAssignment in groupAssignments)
                                    {
                                        groupCursorDef.ApplicableRuleElements.Add(groupAssignment);
                                    }

                                    ruleGenerationContext.DefinedCursors.Add(groupCursorDef);
                                }
                                else
                                {
                                    groupCursorVarName = existingGroupCursor.CursorVariableName;
                                }

                                var groupOrderedElements = OrderElementsByInheritance(groupNonTerminals, umlClass.Cache, ruleGenerationContext);

                                writer.WriteSafeString($"while ({groupCursorVarName}.Current != null){Environment.NewLine}");
                                writer.WriteSafeString($"{{{Environment.NewLine}");
                                writer.WriteSafeString($"switch ({groupCursorVarName}.Current){Environment.NewLine}");
                                writer.WriteSafeString($"{{{Environment.NewLine}");

                                foreach (var groupOrderedElement in groupOrderedElements)
                                {
                                    var groupCaseVarName = groupOrderedElement.UmlClass.Name.LowerCaseFirstLetter();
                                    writer.WriteSafeString($"case {groupOrderedElement.UmlClass.QueryFullyQualifiedTypeName()} {groupCaseVarName}:{Environment.NewLine}");

                                    var previousVariableName = ruleGenerationContext.CurrentVariableName;
                                    var previousCaller = ruleGenerationContext.CallerRule;
                                    ruleGenerationContext.CurrentVariableName = groupCaseVarName;
                                    ruleGenerationContext.CallerRule = groupOrderedElement.RuleElement;
                                    ProcessNonTerminalElement(writer, groupOrderedElement.UmlClass, groupOrderedElement.RuleElement, ruleGenerationContext);
                                    ruleGenerationContext.CurrentVariableName = previousVariableName;
                                    ruleGenerationContext.CallerRule = previousCaller;

                                    writer.WriteSafeString($"{Environment.NewLine}break;{Environment.NewLine}");
                                }

                                writer.WriteSafeString($"}}{Environment.NewLine}");
                                writer.WriteSafeString($"{groupCursorVarName}.Move();{Environment.NewLine}");
                                writer.WriteSafeString($"}}{Environment.NewLine}");
                            }
                            else
                            {
                                var handCodedRuleName = groupElement.TextualNotationRule?.RuleName ?? "Unknown";
                                writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                            }
                        }
                        else
                        {
                            var handCodedRuleName = groupElement.TextualNotationRule?.RuleName ?? "Unknown";
                            writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                        }
                    }
                    else
                    {
                        ProcessAlternatives(writer, umlClass, groupElement.Alternatives, ruleGenerationContext);
                    }

                    if (!groupElement.IsOptional && !ruleGenerationContext.IsNextElementNewLineTerminal() && !ruleGenerationContext.IsLastElement())
                    {
                        writer.WriteSafeString($"{Environment.NewLine}stringBuilder.Append(' ');");
                    }

                    break;
                case AssignmentElement assignmentElement:
                    ProcessAssignmentElement(writer, umlClass, ruleGenerationContext, assignmentElement, isPartOfMultipleAlternative);
                    break;
                case NonParsingAssignmentElement nonParsingAssignmentElement:
                    writer.WriteSafeString($"// NonParsing Assignment Element : {nonParsingAssignmentElement.PropertyName} {nonParsingAssignmentElement.Operator} {nonParsingAssignmentElement.Value} => Does not have to be process");
                    break;
                case ValueLiteralElement valueLiteralElement:
                    if (valueLiteralElement.QueryIsQualifiedName())
                    {
                        writer.WriteSafeString($"stringBuilder.Append({ruleGenerationContext.CurrentVariableName}.qualifiedName);{Environment.NewLine}");

                        if (!ruleGenerationContext.IsNextElementNewLineTerminal())
                        {
                            writer.WriteSafeString("stringBuilder.Append(' ');");
                        }
                    }
                    else
                    {
                        var handCodedRuleName = textualRuleElement.TextualNotationRule?.RuleName ?? "Unknown";
                        writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                    }

                    break;
                default:
                    throw new ArgumentException("Unknown element type");
            }

            writer.WriteSafeString(Environment.NewLine);
        }

        /// <summary>
        /// Processes an <see cref="AssignmentElement"/>
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass"/></param>
        /// <param name="assignmentElement">The <see cref="AssignmentElement"/> to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext"/></param>
        /// <param name="isPartOfMultipleAlternative">Asserts that the current <see cref="AssignmentElement"/> is part of a multiple alternative process</param>
        private static void ProcessAssignmentElement(EncodedTextWriter writer, IClass umlClass, RuleGenerationContext ruleGenerationContext, AssignmentElement assignmentElement, bool isPartOfMultipleAlternative = false)
        {
            var properties = umlClass.QueryAllProperties();
            var targetProperty = properties.SingleOrDefault(x => string.Equals(x.Name, assignmentElement.Property, StringComparison.OrdinalIgnoreCase));

            if (targetProperty != null)
            {
                if (targetProperty.QueryIsEnumerable())
                {
                    if (assignmentElement.Value is NonTerminalElement nonTerminalElement)
                    {
                        var cursorToUse = ruleGenerationContext.DefinedCursors.Single(x => x.ApplicableRuleElements.Contains(assignmentElement));
                        var usedVariable = $"{cursorToUse.CursorVariableName}.Current";

                        var previousVariableName = ruleGenerationContext.CurrentVariableName;
                        ruleGenerationContext.CurrentVariableName = usedVariable;
                        var previousCaller = ruleGenerationContext.CallerRule;
                        ruleGenerationContext.CallerRule = assignmentElement;
                        ProcessNonTerminalElement(writer, umlClass, nonTerminalElement, ruleGenerationContext);
                        ruleGenerationContext.CurrentVariableName = previousVariableName;
                        ruleGenerationContext.CallerRule = previousCaller;

                        // Golden Rule: Move() ↔ +=. Suppress only for collection groups (the loop emits its own Move).
                        // For optional groups, the surrounding optional handler already wraps the body in
                        // `if (cursor.Current is X) { … }`, so emitting Move() here keeps the advance inside that
                        // guard — it only fires when the element was actually consumed.
                        if (!isPartOfMultipleAlternative && assignmentElement.Container is not GroupElement { IsCollection: true })
                        {
                            writer.WriteSafeString($"{cursorToUse.CursorVariableName}.Move();{Environment.NewLine}");
                        }
                    }
                    else if (assignmentElement.Value is GroupElement groupElement)
                    {
                        var previousCaller = ruleGenerationContext.CallerRule;
                        ruleGenerationContext.CallerRule = assignmentElement;
                        ProcessAlternatives(writer, umlClass, groupElement.Alternatives, ruleGenerationContext);
                        ruleGenerationContext.CallerRule = previousCaller;
                    }
                    else if (assignmentElement.Value is ValueLiteralElement valueLiteralElement && valueLiteralElement.QueryIsQualifiedName())
                    {
                        var cursorToUse = ruleGenerationContext.DefinedCursors.Single(x => x.ApplicableRuleElements.Contains(assignmentElement));

                        writer.WriteSafeString($"{Environment.NewLine}if({cursorToUse.CursorVariableName}.Current != null){Environment.NewLine}");
                        writer.WriteSafeString($"{{{Environment.NewLine}");
                        writer.WriteSafeString($"stringBuilder.Append({cursorToUse.CursorVariableName}.Current.qualifiedName);{Environment.NewLine}");
                        writer.WriteSafeString($"{cursorToUse.CursorVariableName}.Move();{Environment.NewLine}");
                        writer.WriteSafeString("}");
                    }
                    else
                    {
                        var handCodedRuleName = assignmentElement.TextualNotationRule?.RuleName ?? "Unknown";
                        writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                    }
                }
                else
                {
                    if (assignmentElement.IsOptional)
                    {
                        writer.WriteSafeString($"{Environment.NewLine}if({targetProperty.QueryIfStatementContentForNonEmpty("poco")}){Environment.NewLine}");
                        writer.WriteSafeString($"{{{Environment.NewLine}");
                        writer.WriteSafeString($"stringBuilder.Append(poco.{targetProperty.Name.CapitalizeFirstLetter()});{Environment.NewLine}");
                        writer.WriteSafeString("}");
                    }
                    else
                    {
                        var targetPropertyName = targetProperty.QueryPropertyNameBasedOnUmlProperties();

                        if (targetProperty.QueryIsString())
                        {
                            if (assignmentElement.Value is NonTerminalElement { Name: "REGULAR_COMMENT" })
                            {
                                writer.WriteSafeString($"SharedTextualNotationBuilder.AppendRegularComment(stringBuilder, poco.{targetPropertyName});");
                            }
                            else
                            {
                                writer.WriteSafeString($"stringBuilder.Append(poco.{targetPropertyName});");
                            }
                        }
                        else if (targetProperty.QueryIsBool())
                        {
                            if (assignmentElement.Value is TerminalElement terminalElement)
                            {
                                if (!isPartOfMultipleAlternative && assignmentElement.Container is not GroupElement {IsOptional:true})
                                {
                                    writer.WriteSafeString($"if({targetProperty.QueryIfStatementContentForNonEmpty("poco")}){Environment.NewLine}");
                                    writer.WriteSafeString($"{{{Environment.NewLine}");
                                    writer.WriteSafeString($"stringBuilder.Append(\" {terminalElement.Value} \");{Environment.NewLine}");
                                    writer.WriteSafeString('}');
                                }
                                else
                                {
                                    writer.WriteSafeString($"stringBuilder.Append(\" {terminalElement.Value} \");");
                                }
                            }
                            else
                            {
                                writer.WriteSafeString($"stringBuilder.Append(poco.{targetPropertyName}.ToString().ToLower());");
                            }
                        }
                        else if (targetProperty.QueryIsEnum())
                        {
                            writer.WriteSafeString($"stringBuilder.Append(poco.{targetPropertyName}.ToString().ToLower());");
                        }
                        else if (targetProperty.QueryIsReferenceType())
                        {
                            switch (assignmentElement.Value)
                            {
                                case NonTerminalElement nonTerminalElement:
                                {
                                    var previousCaller = ruleGenerationContext.CallerRule;
                                    ruleGenerationContext.CallerRule = nonTerminalElement;
                                    ruleGenerationContext.CurrentVariableName = $"poco.{targetPropertyName}";
                                    ProcessNonTerminalElement(writer, targetProperty.Type as IClass, nonTerminalElement, ruleGenerationContext, isPartOfMultipleAlternative);
                                    ruleGenerationContext.CurrentVariableName = "poco";
                                    ruleGenerationContext.CallerRule = previousCaller;
                                    break;
                                }
                                case ValueLiteralElement valueLiteralElement when valueLiteralElement.QueryIsQualifiedName():
                                    if (isPartOfMultipleAlternative)
                                    {
                                        writer.WriteSafeString($"stringBuilder.Append(poco.{targetPropertyName}.qualifiedName);{Environment.NewLine}");

                                        if (!ruleGenerationContext.IsNextElementNewLineTerminal())
                                        {
                                            writer.WriteSafeString("stringBuilder.Append(' ');");
                                        }
                                    }
                                    else
                                    {
                                        writer.WriteSafeString($"{Environment.NewLine}if (poco.{targetPropertyName} != null){Environment.NewLine}");
                                        writer.WriteSafeString($"{{{Environment.NewLine}");
                                        writer.WriteSafeString($"stringBuilder.Append(poco.{targetPropertyName}.qualifiedName);{Environment.NewLine}");

                                        if (!ruleGenerationContext.IsNextElementNewLineTerminal())
                                        {
                                            writer.WriteSafeString("stringBuilder.Append(' ');");
                                        }
                                        writer.WriteSafeString($"{Environment.NewLine}}}");
                                    }

                                    break;
                                default:
                                    var handCodedRuleName = assignmentElement.TextualNotationRule?.RuleName ?? "Unknown";
                                    writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                                    break;
                            }
                        }
                        else
                        {
                            writer.WriteSafeString($"stringBuilder.Append(poco.{targetPropertyName}.ToString());");
                        }
                    }
                }
            }
            else
            {
                writer.WriteSafeString($"Build{assignmentElement.Property.CapitalizeFirstLetter()}(poco, cursorCache, stringBuilder);");
            }
        }

        /// <summary>
        /// Process a <see cref="NonTerminalElement"/>
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass"/></param>
        /// <param name="nonTerminalElement">The <see cref="NonTerminalElement"/> to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext"/></param>
        /// <param name="isPartOfMultipleAlternative">Asserts that the current <see cref="NonTerminalElement"/> is part of a multiple alternative process</param>
        private static void ProcessNonTerminalElement(EncodedTextWriter writer, IClass umlClass, NonTerminalElement nonTerminalElement, RuleGenerationContext ruleGenerationContext, bool isPartOfMultipleAlternative = false)
        {
            var referencedRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == nonTerminalElement.Name);

            string typeTarget;

            if (referencedRule == null)
            {
                typeTarget = umlClass.Name;
            }
            else
            {
                typeTarget = referencedRule.TargetElementName ?? referencedRule.RuleName;
            }

            var isForProperty = ruleGenerationContext.CurrentVariableName.Contains('.');

            var emitPropertyNullGuard = isForProperty && !isPartOfMultipleAlternative;

            if (emitPropertyNullGuard)
            {
                writer.WriteSafeString($"{Environment.NewLine}if ({ruleGenerationContext.CurrentVariableName} != null){Environment.NewLine}");
                writer.WriteSafeString($"{{{Environment.NewLine}");
            }

            if (nonTerminalElement.IsCollection)
            {
                EmitCollectionNonTerminalLoop(writer, umlClass, nonTerminalElement, referencedRule, typeTarget, ruleGenerationContext);

                if (emitPropertyNullGuard)
                {
                    writer.WriteSafeString($"{Environment.NewLine}}}");
                }

                return;
            }

            if (typeTarget != ruleGenerationContext.NamedElementToGenerate.Name)
            {
                var targetType = umlClass.Cache.Values.OfType<INamedElement>().SingleOrDefault(x => x.Name == typeTarget);

                if (targetType != null)
                {
                    if (targetType is IClass targetClass && (
                        umlClass.QueryAllGeneralClassifiers().Contains(targetClass)      // upward cast: umlClass IS-A targetClass
                        || (targetClass.QueryAllGeneralClassifiers().Contains(umlClass)  // downward cast: targetClass IS-A umlClass
                            && ruleGenerationContext.CurrentVariableName.Contains('.'))  //   ONLY for property access; direct poco access is handled by inlining the body (else branch)
                        || targetClass == umlClass                                       // same type
                        || !ruleGenerationContext.CurrentVariableName.Contains("poco")))
                    {
                        // Emit a cast when the current type is not guaranteed to be the target type:
                        // - CallerRule is AssignmentElement (processing an element of a += collection)
                        // - Downward cast via property access (e.g., poco.Prop is ITarget x)
                        var needsDownwardCast = targetClass != umlClass && !umlClass.QueryAllGeneralClassifiers().Contains(targetClass);
                        var emitCast = ruleGenerationContext.CallerRule is AssignmentElement || needsDownwardCast;

                        if (emitCast)
                        {
                            var castedVariableName = $"elementAs{targetClass.Name}";
                            writer.WriteSafeString($"{Environment.NewLine}if ({ruleGenerationContext.CurrentVariableName} is {targetClass.QueryFullyQualifiedTypeName()} {castedVariableName}){Environment.NewLine}");
                            ruleGenerationContext.CurrentVariableName = castedVariableName;
                            writer.WriteSafeString($"{{{Environment.NewLine}");
                        }

                        var emittedCondition = TryEmitOptionalCondition(writer, nonTerminalElement, referencedRule, targetClass, ruleGenerationContext, ruleGenerationContext.CurrentVariableName);

                        writer.WriteSafeString($"{targetType.Name}TextualNotationBuilder.Build{nonTerminalElement.Name}({ruleGenerationContext.CurrentVariableName}, cursorCache, stringBuilder);");

                        if (emittedCondition)
                        {
                            writer.WriteSafeString($"{Environment.NewLine}}}");
                        }

                        if (emitCast)
                        {
                            writer.WriteSafeString($"{Environment.NewLine}}}");
                        }
                    }
                    else
                    {
                        var previousCaller = ruleGenerationContext.CallerRule;
                        ruleGenerationContext.CallerRule = nonTerminalElement;
                        var previousName = ruleGenerationContext.CurrentVariableName;

                        ProcessAlternatives(writer, umlClass, referencedRule?.Alternatives, ruleGenerationContext, isPartOfMultipleAlternative);
                        ruleGenerationContext.CallerRule = previousCaller;
                        ruleGenerationContext.CurrentVariableName = previousName;
                    }
                }
                else
                {
                    if (IsSharedNoTargetRule(referencedRule, umlClass))
                    {
                        EmitSharedNoTargetRuleCall(writer, umlClass, nonTerminalElement, referencedRule, ruleGenerationContext);
                    }
                    else
                    {
                        var previousCaller = ruleGenerationContext.CallerRule;
                        ruleGenerationContext.CallerRule = nonTerminalElement;
                        var previousName = ruleGenerationContext.CurrentVariableName;

                        ProcessAlternatives(writer, umlClass, referencedRule?.Alternatives, ruleGenerationContext, isPartOfMultipleAlternative);
                        ruleGenerationContext.CallerRule = previousCaller;
                        ruleGenerationContext.CurrentVariableName = previousName;
                    }
                }
            }
            else
            {
                // When referencedRule is null, this is a handcoded stub (non-existing grammar rule).
                // Handcoded stubs take the parent POCO, not the cursor element.
                var variableToUse = referencedRule != null ? ruleGenerationContext.CurrentVariableName : "poco";

                var emittedSameClassCondition = TryEmitOptionalCondition(writer, nonTerminalElement, referencedRule, umlClass, ruleGenerationContext, variableToUse);

                writer.WriteSafeString($"Build{nonTerminalElement.Name}({variableToUse}, cursorCache, stringBuilder);");

                if (emittedSameClassCondition)
                {
                    writer.WriteSafeString($"{Environment.NewLine}}}");
                }
            }

            if (emitPropertyNullGuard)
            {
                writer.WriteSafeString($"{Environment.NewLine}}}");
            }
        }

        /// <summary>
        /// Emits a while loop for a collection NonTerminal (e.g., <c>ActionBodyItem*</c>).
        /// Resolves which POCO collection property the rule consumes by recursively tracing through
        /// NonTerminal references, then emits cursor creation + while loop + per-item builder call.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass"/></param>
        /// <param name="nonTerminalElement">The <see cref="NonTerminalElement"/> with <c>*</c> or <c>+</c> suffix</param>
        /// <param name="referencedRule">The resolved <see cref="TextualNotationRule"/> for the NonTerminal, or <c>null</c></param>
        /// <param name="typeTarget">The resolved target type name for the builder class</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext"/></param>
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
                                        var targetName = refRule != null ? (refRule.TargetElementName ?? refRule.RuleName) : null;

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
                                whileCondition = $"{cursorVariableName}.Current != null && {cursorVariableName}.Current is {assignmentTargetTypes[0]}";
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
        /// Resolves a type condition clause for the while loop in a collection NonTerminal loop.
        /// When the collection has sibling elements after it, the while loop must stop before consuming those elements.
        /// <para>
        /// The primary approach is a <b>positive</b> condition based on the collection item's own assignment target type
        /// (e.g., <c>UsageExtensionKeyword</c> assigns to <c>PrefixMetadataMember:OwningMembership</c> → <c>while (cursor.Current is IOwningMembership)</c>).
        /// </para>
        /// <para>
        /// Falls back to a <b>negative</b> exclusion based on the next sibling's target type when the positive approach
        /// is not available (e.g., <c>CalculationBodyItem* ResultExpressionMember</c> → <c>while (cursor.Current is not IResultExpressionMembership)</c>).
        /// </para>
        /// </summary>
        /// <param name="cursorVariableName">The cursor variable name used in the while condition</param>
        /// <param name="umlClass">The related <see cref="IClass"/></param>
        /// <param name="collectionRule">The resolved <see cref="TextualNotationRule"/> for the collection NonTerminal</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext"/></param>
        /// <returns>A type condition clause string, or empty string if no condition is needed</returns>
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
                    var itemTypeTarget = itemRule != null ? (itemRule.TargetElementName ?? itemRule.RuleName) : null;

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
            var nextTypeTarget = nextRule != null ? (nextRule.TargetElementName ?? nextRule.RuleName) : null;

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
        /// Resolves the builder method call string for a NonTerminal element, handling type targeting
        /// (e.g., when <c>ActionBodyItem : Type =</c> means the builder lives on <c>TypeTextualNotationBuilder</c>).
        /// Returns <c>null</c> when the target type is not compatible with the current <c>poco</c> variable
        /// (e.g., when the rule targets a subclass like <c>Package</c> but <c>poco</c> is <c>INamespace</c>).
        /// </summary>
        /// <param name="umlClass">The related <see cref="IClass"/></param>
        /// <param name="nonTerminalElement">The <see cref="NonTerminalElement"/> to resolve</param>
        /// <param name="typeTarget">The resolved target type name</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext"/></param>
        /// <returns>A C# call expression string, or <c>null</c> if the types are incompatible</returns>
        private static string ResolveBuilderCall(IClass umlClass, NonTerminalElement nonTerminalElement, string typeTarget, RuleGenerationContext ruleGenerationContext)
        {
            if (typeTarget == ruleGenerationContext.NamedElementToGenerate.Name)
            {
                return $"Build{nonTerminalElement.Name}({ruleGenerationContext.CurrentVariableName}, cursorCache, stringBuilder);";
            }

            var targetType = umlClass.Cache.Values.OfType<INamedElement>().SingleOrDefault(x => x.Name == typeTarget);

            if (targetType is IClass targetClass)
            {
                // Check type compatibility: the target class must be the same as or a superclass of the current class
                // e.g., Type is a superclass of AcceptActionUsage → compatible (IAcceptActionUsage IS an IType)
                // e.g., Package is a subclass of Namespace → NOT compatible (INamespace is NOT an IPackage)
                if (umlClass.QueryAllGeneralClassifiers().Contains(targetClass))
                {
                    return $"{targetType.Name}TextualNotationBuilder.Build{nonTerminalElement.Name}({ruleGenerationContext.CurrentVariableName}, cursorCache, stringBuilder);";
                }

                // Type is not compatible — caller should inline the rule alternatives
                return null;
            }

            return $"Build{nonTerminalElement.Name}({ruleGenerationContext.CurrentVariableName}, cursorCache, stringBuilder);";
        }

        /// <summary>
        /// Declares a cursor to perform iteration over a collection, if required to declare it.
        /// Uses <c>cursorCache.GetOrCreateCursor</c> to share cursor state across method calls.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass"/></param>
        /// <param name="assignmentElement">The <see cref="AssignmentElement"/> to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext"/></param>
        private static void DeclareCursorIfRequired(EncodedTextWriter writer, IClass umlClass, AssignmentElement assignmentElement, RuleGenerationContext ruleGenerationContext)
        {
            var allProperties = umlClass.QueryAllProperties();
            var targetProperty = allProperties.SingleOrDefault(x => string.Equals(x.Name, assignmentElement.Property, StringComparison.OrdinalIgnoreCase));

            if (targetProperty == null || !targetProperty.QueryIsEnumerable())
            {
                return;
            }

            // Check if a cursor already exists for this property
            if (ruleGenerationContext.DefinedCursors.SingleOrDefault(x => x.IsCursorValidForProperty(targetProperty) || x.ApplicableRuleElements.Contains(assignmentElement)) is { } alreadyDefinedCursor)
            {
                alreadyDefinedCursor.ApplicableRuleElements.Add(assignmentElement);
                return;
            }

            switch (assignmentElement.Value)
            {
                case NonTerminalElement:
                case ValueLiteralElement:
                case GroupElement:
                {
                    var cursorToUse = new CursorDefinition
                    {
                        DefinedForProperty = targetProperty
                    };

                    cursorToUse.ApplicableRuleElements.Add(assignmentElement);

                    var propertyAccessName = targetProperty.QueryPropertyNameBasedOnUmlProperties();
                    writer.WriteSafeString($"var {cursorToUse.CursorVariableName} = cursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{propertyAccessName});");
                    writer.WriteSafeString(Environment.NewLine);
                    ruleGenerationContext.DefinedCursors.Add(cursorToUse);
                    break;
                }
                case AssignmentElement containedAssignment:
                    DeclareCursorIfRequired(writer, umlClass, containedAssignment, ruleGenerationContext);
                    break;
            }
        }

        /// <summary>
        /// Terminals that should be emitted using <c>AppendLine</c> instead of <c>Append</c>,
        /// because they represent structural delimiters that end a line in the textual notation.
        /// </summary>
        private static readonly HashSet<string> NewLineTerminals = ["{", "}", ";"];

        /// <summary>
        /// Terminals after which no trailing space should be emitted,
        /// because the next element is directly adjacent (e.g., content inside angle brackets, closing angle bracket, or the <c>~</c> prefix operator).
        /// </summary>
        private static readonly HashSet<string> NoTrailingSpaceTerminals = ["<", ">", "~"];

        /// <summary>
        /// Writes the <c>stringBuilder.Append</c> or <c>stringBuilder.AppendLine</c> call for a terminal value,
        /// applying formatting rules derived from the SysML v2 textual notation conventions:
        /// <list type="bullet">
        ///   <item><c>{</c>, <c>}</c>, <c>;</c> are emitted with <c>AppendLine</c> (newline after)</item>
        ///   <item><c>&lt;</c>, <c>&gt;</c>, and <c>~</c> are emitted with no trailing space (adjacent to surrounding content)</item>
        ///   <item><c>,</c> is emitted with a trailing space</item>
        ///   <item>Multi-character terminals (keywords) are emitted with a trailing space</item>
        ///   <item>Other single-character terminals are emitted as-is</item>
        /// </list>
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write into output content</param>
        /// <param name="terminalValue">The terminal string value to emit</param>
        private static void WriteTerminalAppend(EncodedTextWriter writer, string terminalValue)
        {
            if (NewLineTerminals.Contains(terminalValue))
            {
                writer.WriteSafeString($"stringBuilder.AppendLine(\"{terminalValue}\");");
                return;
            }

            if (NoTrailingSpaceTerminals.Contains(terminalValue))
            {
                writer.WriteSafeString($"stringBuilder.Append(\"{terminalValue}\");");
                return;
            }

            if (terminalValue.Length > 1 || terminalValue == ",")
            {
                writer.WriteSafeString($"stringBuilder.Append(\"{terminalValue} \");");
                return;
            }

            writer.WriteSafeString($"stringBuilder.Append(\"{terminalValue}\");");
        }

        /// <summary>
        /// Writes a terminal value with a leading space, used for keyword-like terminals that appear
        /// as alternatives in untyped multi-alternative rules (e.g., <c>'~' | 'conjugates'</c>).
        /// The leading space ensures visual separation from the preceding element.
        /// Structural terminals (<c>{</c>, <c>}</c>, <c>;</c>) still use <c>AppendLine</c>.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write into output content</param>
        /// <param name="terminalValue">The terminal string value to emit</param>
        private static void WriteTerminalAppendWithLeadingSpace(EncodedTextWriter writer, string terminalValue)
        {
            if (NewLineTerminals.Contains(terminalValue))
            {
                writer.WriteSafeString($"stringBuilder.AppendLine(\"{terminalValue}\");");
                return;
            }

            if (NoTrailingSpaceTerminals.Contains(terminalValue))
            {
                writer.WriteSafeString($"stringBuilder.Append(\" {terminalValue}\");");
                return;
            }

            writer.WriteSafeString($"stringBuilder.Append(\" {terminalValue} \");");
        }

        /// <summary>
        /// Generates an inline condition string for an optional NonTerminal by recursively resolving
        /// the referenced rule's property references and building a compound boolean expression.
        /// </summary>
        /// <param name="referencedRule">The <see cref="TextualNotationRule"/> that the optional NonTerminal references</param>
        /// <param name="targetClass">The <see cref="IClass"/> on which properties are resolved (the class the referenced rule targets)</param>
        /// <param name="allRules">All available rules for recursive resolution</param>
        /// <param name="variableName">The variable name to use in the condition (e.g., <c>poco</c> or <c>poco.ownedMemberFeature</c>)</param>
        /// <returns>The condition string (e.g., <c>!string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.OwnedRelationship.Count != 0</c>), or <c>null</c> if no properties could be resolved</returns>
        private static string GenerateInlineOptionalCondition(TextualNotationRule referencedRule, IClass targetClass, IReadOnlyList<TextualNotationRule> allRules, string variableName)
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
        /// When the referenced rule has resolvable properties, emits an inline condition.
        /// Otherwise emits no condition (the call proceeds unconditionally).
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write into output content</param>
        /// <param name="nonTerminalElement">The optional <see cref="NonTerminalElement"/></param>
        /// <param name="referencedRule">The resolved <see cref="TextualNotationRule"/>, or <c>null</c> for handcoded stubs</param>
        /// <param name="targetClass">The <see cref="IClass"/> on which properties are resolved (the class the referenced rule targets)</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext"/></param>
        /// <param name="variableName">The variable name to use in the condition (e.g., <c>poco</c> or <c>poco.ownedMemberFeature</c>)</param>
        /// <returns><c>true</c> if a condition block was opened (caller must close it), <c>false</c> otherwise</returns>
        private static bool TryEmitOptionalCondition(EncodedTextWriter writer, NonTerminalElement nonTerminalElement, TextualNotationRule referencedRule, IClass targetClass, RuleGenerationContext ruleGenerationContext, string variableName)
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

        /// <summary>
        /// The name of the shared builder class that hosts all no-target rules that do not
        /// have a matching UML class (e.g. <c>FeaturePrefix</c>).
        /// </summary>
        public const string SharedBuilderClassName = "SharedTextualNotationBuilder";

        /// <summary>
        /// Resolves the effective target <see cref="IClass"/> for a rule that declares no target type
        /// and has no matching UML class of the same name. The effective target is the narrowest
        /// <see cref="IClass"/> that declares or inherits every property the rule assigns (including
        /// properties assigned transitively by other no-target sub-rules it references).
        /// </summary>
        /// <param name="rule">The no-target <see cref="TextualNotationRule"/></param>
        /// <param name="allRules">All available rules (for resolving sub-rule references)</param>
        /// <param name="cacheSource">Any <see cref="IClass"/> from the loaded model used to access <c>Cache</c></param>
        /// <returns>The narrowest <see cref="IClass"/> owning all collected properties, or <c>Element</c> as a fallback when the rule assigns no property, or <c>null</c> if no matching class exists</returns>
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
        /// Recursively walks a list of <see cref="RuleElement"/> in the current no-target scope and
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
        /// Determines whether a no-target rule should be lifted into the shared builder class.
        /// Shared lifting applies to rules that:
        /// <list type="bullet">
        ///   <item>declare no target type,</item>
        ///   <item>do not share a name with any <see cref="IClass"/> in the UML model (handled by the per-class grouping path),</item>
        ///   <item>are not purely lexical tokens (SysML convention: <c>ALL_CAPS</c> names such as <c>NAME</c>, <c>STRING_VALUE</c>),</item>
        ///   <item>assign at least one POCO property somewhere in their body — rules without any assignment (pure token-class groups like <c>BinaryOperator</c>, <c>UnaryOperator</c>, <c>QualifiedName</c>) have no runtime state to drive and are instead handled by their call sites.</item>
        /// </list>
        /// </summary>
        /// <param name="rule">The rule to test</param>
        /// <param name="cacheSource">Any <see cref="IClass"/> from the loaded model used to access <c>Cache</c></param>
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
        /// Determines whether a rule — directly or via any nested <see cref="GroupElement"/> — contains
        /// at least one <see cref="AssignmentElement"/> or <see cref="NonParsingAssignmentElement"/>.
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
        /// Recursively tests whether a list of <see cref="RuleElement"/> contains any
        /// <see cref="AssignmentElement"/> or <see cref="NonParsingAssignmentElement"/>, descending
        /// through <see cref="GroupElement"/> boundaries but NOT through <see cref="NonTerminalElement"/>
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
        /// Emits a call to the shared no-target rule builder. When the caller's <paramref name="umlClass"/>
        /// does not already derive from the rule's effective target, an explicit cast is emitted.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write output</param>
        /// <param name="umlClass">The caller's <see cref="IClass"/></param>
        /// <param name="nonTerminalElement">The <see cref="NonTerminalElement"/> being processed</param>
        /// <param name="referencedRule">The referenced shared no-target <see cref="TextualNotationRule"/></param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext"/></param>
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

        /// <summary>
        /// Keeps tracks of defined cursor for enumerable <see cref="IProperty"/>
        /// </summary>
        private class CursorDefinition
        {
            /// <summary>
            /// Gets or sets the <see cref="IProperty"/> that have to have a cursor defined
            /// </summary>
            public IProperty DefinedForProperty { get; init; }

            /// <summary>
            /// Gets the name of the variable defined for the cursor
            /// </summary>
            public string CursorVariableName => $"{this.DefinedForProperty.Name.LowerCaseFirstLetter()}Cursor";

            /// <summary>
            /// Provides a collection of <see cref="AssignmentElement"/> that will use the defined cursor
            /// </summary>
            public HashSet<AssignmentElement> ApplicableRuleElements { get; } = [];

            /// <summary>
            /// Asserts that the current <see cref="CursorDefinition"/> is valid for an <see cref="IProperty"/>
            /// </summary>
            /// <param name="property">The specific <see cref="IProperty"/></param>
            /// <returns>True if the <see cref="CursorDefinition"/> is valid for the provided property</returns>
            public bool IsCursorValidForProperty(IProperty property)
            {
                return property == this.DefinedForProperty;
            }
        }

        /// <summary>
        /// Provides context over the rule generation history
        /// </summary>
        private class RuleGenerationContext
        {
            /// <summary>
            /// Gets or sets the collection of <see cref="CursorDefinition" />
            /// </summary>
            public List<CursorDefinition> DefinedCursors { get; set; }

            /// <summary>
            /// Gets the collection of all available <see cref="TextualNotationRule"/>
            /// </summary>
            public List<TextualNotationRule> AllRules { get; } = [];

            /// <summary>
            /// Gets or sets the <see cref="RuleElement"/> that called other rule
            /// </summary>
            public RuleElement CallerRule { get; set; }

            /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
            public RuleGenerationContext(INamedElement namedElementToGenerate)
            {
                this.NamedElementToGenerate = namedElementToGenerate;
            }

            /// <summary>
            /// Gets the <see cref="INamedElement"/> that is used in the current generation
            /// </summary>
            public INamedElement NamedElementToGenerate { get; }

            /// <summary>
            /// Gets or sets the current name of the variable to process
            /// </summary>
            public string CurrentVariableName { get; set; }

            /// <summary>
            /// Gets or sets the sibling elements of the current processing context,
            /// used to determine whether a trailing space should be suppressed.
            /// </summary>
            public IReadOnlyList<RuleElement> CurrentSiblingElements { get; set; }

            /// <summary>
            /// Gets or sets the index of the element currently being processed
            /// within <see cref="CurrentSiblingElements"/>.
            /// </summary>
            public int CurrentElementIndex { get; set; }

            /// <summary>
            /// Determines whether the next sibling element is a terminal that uses <c>AppendLine</c>
            /// (e.g., <c>{</c>, <c>}</c>, <c>;</c>), in which case a trailing space would be unnecessary.
            /// </summary>
            /// <returns><c>true</c> if the next sibling is a newline terminal; <c>false</c> otherwise</returns>
            public bool IsNextElementNewLineTerminal()
            {
                if (this.CurrentSiblingElements == null)
                {
                    return false;
                }

                var nextIndex = this.CurrentElementIndex + 1;

                if (nextIndex >= this.CurrentSiblingElements.Count)
                {
                    return false;
                }

                return this.CurrentSiblingElements[nextIndex] is TerminalElement { Value: "{" or "}" or ";" };
            }

            /// <summary>
            /// Gets the set of HandCoded method names that have already been emitted during the
            /// current rule's code generation. Used to suppress duplicate HandCoded fallback calls
            /// when multiple unresolvable grammar segments of the same rule each fall back to
            /// <c>Build{Rule}HandCoded</c> (e.g., a group and a trailing collection non-terminal).
            /// </summary>
            public HashSet<string> EmittedHandCodedCalls { get; } = [];

            /// <summary>
            /// Determines whether the current element is the last in the sibling list,
            /// meaning no more content follows and a trailing space would be unnecessary.
            /// </summary>
            /// <returns><c>true</c> if this is the last element; <c>false</c> otherwise</returns>
            public bool IsLastElement()
            {
                if (this.CurrentSiblingElements == null)
                {
                    return false;
                }

                return this.CurrentElementIndex + 1 >= this.CurrentSiblingElements.Count;
            }
        }
    }
}
