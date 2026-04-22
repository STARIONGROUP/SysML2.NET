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

                            var referencedTerminalElement = assignmentElements.Select(x => x.Value).OfType<NonTerminalElement>().ToList();

                            if (referencedTerminalElement.Count != assignmentElements.Count)
                            {
                                var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                                writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                                return;
                            }

                            referencedTerminalElement.Add(nonTerminalElement);

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

                            var mappedElements = OrderElementsByInheritance(referencedTerminalElement, umlClass.Cache, ruleGenerationContext);

                            writer.WriteSafeString($"switch({cursorVarName}.Current){Environment.NewLine}");
                            writer.WriteSafeString($"{{{Environment.NewLine}");

                            foreach (var mappedElement in mappedElements)
                            {
                                writer.WriteSafeString($"case {mappedElement.UmlClass.QueryFullyQualifiedTypeName()} {mappedElement.UmlClass.Name.LowerCaseFirstLetter()}:{Environment.NewLine}");
                                ruleGenerationContext.CurrentVariableName = mappedElement.UmlClass.Name.LowerCaseFirstLetter();
                                ProcessNonTerminalElement(writer, mappedElement.UmlClass, mappedElement.RuleElement, ruleGenerationContext);
                                writer.WriteSafeString($"break;{Environment.NewLine}");
                            }

                            writer.WriteSafeString($"{Environment.NewLine}}}");
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
                            writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
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
                        var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                        writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                        break;
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

                    var variableName = "poco";

                    if (ruleGenerationContext.CallerRule is AssignmentElement assignmentElement)
                    {
                        var cursorToUse = ruleGenerationContext.DefinedCursors.Single(x => x.ApplicableRuleElements.Contains(assignmentElement));
                        variableName = $"{cursorToUse.CursorVariableName}.Current";
                    }

                    writer.WriteSafeString($"switch({variableName}){Environment.NewLine}");
                    writer.WriteSafeString("{");

                    // Determine the last element that would be a default case (only one default allowed)
                    var defaultElement = mappedNonTerminalElements
                        .LastOrDefault(x => x.UmlClass == ruleGenerationContext.NamedElementToGenerate && !whenGuards.ContainsKey(x.RuleElement));

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

                        ProcessNonTerminalElement(writer, orderedNonTerminalElement.UmlClass, orderedNonTerminalElement.RuleElement, ruleGenerationContext);

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
                            writer.WriteSafeString($"{Environment.NewLine}while({cursorToUse.CursorVariableName}.Current != null){Environment.NewLine}");
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
                        writer.WriteSafeString("// Have to handle group collection"); // => hand code !
                    }
                    else
                    {
                        ProcessAlternatives(writer, umlClass, groupElement.Alternatives, ruleGenerationContext);
                    }

                    if (!groupElement.IsOptional && !ruleGenerationContext.IsNextElementNewLineTerminal())
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

                        if (!isPartOfMultipleAlternative && assignmentElement.Container is not GroupElement { IsCollection: true } && assignmentElement.Container is not GroupElement { IsOptional: true })
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
                            writer.WriteSafeString($"stringBuilder.Append(poco.{targetPropertyName});");
                        }
                        else if (targetProperty.QueryIsBool())
                        {
                            if (assignmentElement.Value is TerminalElement terminalElement)
                            {
                                if (!isPartOfMultipleAlternative && assignmentElement.Container is not GroupElement)
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

            if (isForProperty && !isPartOfMultipleAlternative)
            {
                writer.WriteSafeString($"{Environment.NewLine}if ({ruleGenerationContext.CurrentVariableName} != null){Environment.NewLine}");
                writer.WriteSafeString($"{{{Environment.NewLine}");
            }

            if (nonTerminalElement.IsCollection)
            {
                EmitCollectionNonTerminalLoop(writer, umlClass, nonTerminalElement, referencedRule, typeTarget, ruleGenerationContext);

                if (isForProperty && !isPartOfMultipleAlternative)
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
                    if (targetType is IClass targetClass && (umlClass.QueryAllGeneralClassifiers().Contains(targetClass) || !ruleGenerationContext.CurrentVariableName.Contains("poco")))
                    {
                        if (ruleGenerationContext.CallerRule is AssignmentElement )
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

                        if (ruleGenerationContext.CallerRule is AssignmentElement)
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

            if (isForProperty && !isPartOfMultipleAlternative)
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

                        if (perItemCall != null)
                        {
                            // Emit while loop calling the per-item builder method
                            writer.WriteSafeString($"while ({cursorVariableName}.Current != null){Environment.NewLine}");
                            writer.WriteSafeString($"{{{Environment.NewLine}");
                            writer.WriteSafeString(perItemCall);
                            writer.WriteSafeString($"{Environment.NewLine}}}{Environment.NewLine}");
                        }
                        else
                        {
                            // Type incompatible: inline the referenced rule's alternatives inside the while loop
                            writer.WriteSafeString($"while ({cursorVariableName}.Current != null){Environment.NewLine}");
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

            // Fallback for unresolvable cases — delegate to handcoded method
            var handCodedRuleName = nonTerminalElement.TextualNotationRule?.RuleName ?? nonTerminalElement.Name;
            writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);{Environment.NewLine}");
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
