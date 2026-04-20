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

                        foreach (var textualRuleElement in elements)
                        {
                            var previousCaller = ruleGenerationContext.CallerRule;
                            ProcessRuleElement(writer, umlClass, textualRuleElement, ruleGenerationContext);
                            ruleGenerationContext.CallerRule = previousCaller;
                        }
                    }
                    else
                    {   
                        writer.WriteSafeString($"{Environment.NewLine}if(BuildGroupConditionFor{alternative.TextualNotationRule.RuleName}(poco))");
                        writer.WriteSafeString($"{Environment.NewLine}{{{Environment.NewLine}");

                        foreach (var textualRuleElement in elements)
                        {
                            ProcessRuleElement(writer, umlClass, textualRuleElement, ruleGenerationContext);
                        }
                    }
                        
                    writer.WriteSafeString($"stringBuilder.Append(' ');{Environment.NewLine}");
                    writer.WriteSafeString($"}}{Environment.NewLine}");
                }
                else
                {
                    foreach (var textualRuleElement in elements)
                    {
                        var previousCaller = ruleGenerationContext.CallerRule;
                        ProcessRuleElement(writer, umlClass, textualRuleElement, ruleGenerationContext, isPartOfMultipleAlternative);
                        ruleGenerationContext.CallerRule = previousCaller;
                    }    
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
                                writer.WriteSafeString("throw new System.NotSupportedException(\"Assignment Element with something else than NonTerminalElement not supported\");");
                                return;
                            }

                            referencedTerminalElement.Add(nonTerminalElement);
                            
                            var targetProperty = umlClass.QueryAllProperties().Single(x => string.Equals(x.Name, assignmentElements[0].Property));

                            const string indexName = "elementIndex";
                            const string variableName = "elements";
                            const string elementName = $"{variableName}Element";
                            
                            writer.WriteSafeString($"var {elementName} = poco.{targetProperty.QueryPropertyNameBasedOnUmlProperties()}[{indexName}];{Environment.NewLine}");
                            writer.WriteSafeString($"{Environment.NewLine}switch({elementName}){Environment.NewLine}");
                            writer.WriteSafeString($"{{{Environment.NewLine}");

                            var mappedElements = OrderElementsByInheritance(referencedTerminalElement, umlClass.Cache, ruleGenerationContext);
                            
                            foreach (var assignmentElement in assignmentElements)
                            {
                                var mappedElement = mappedElements.Single(x => x.RuleElement == assignmentElement.Value);
                                
                                writer.WriteSafeString($"case {mappedElement.UmlClass.QueryFullyQualifiedTypeName(targetInterface:mappedElement.UmlClass.IsAbstract)} {mappedElement.UmlClass.Name.LowerCaseFirstLetter()}:{Environment.NewLine}");
                                ruleGenerationContext.CurrentVariableName = mappedElement.UmlClass.Name.LowerCaseFirstLetter();
                                ProcessNonTerminalElement(writer, mappedElement.UmlClass, mappedElement.RuleElement, ruleGenerationContext);
                                writer.WriteSafeString($"break;{Environment.NewLine}");
                            }
                            
                            var mappedElementForNonTerminal = mappedElements.Single(x => x.RuleElement == nonTerminalElement);
                            writer.WriteSafeString($"case {mappedElementForNonTerminal.UmlClass.QueryFullyQualifiedTypeName()} {mappedElementForNonTerminal.UmlClass.Name.LowerCaseFirstLetter()}:{Environment.NewLine}");

                            if (mappedElementForNonTerminal.UmlClass == umlClass)
                            {
                                writer.WriteSafeString($"{indexName} = Build{nonTerminalElement.Name}({mappedElementForNonTerminal.UmlClass.Name.LowerCaseFirstLetter()}, {indexName}, stringBuilder);");
                            }
                            else
                            {
                                writer.WriteSafeString($"{indexName} = {mappedElementForNonTerminal.UmlClass.Name}TextualNotationBuilder.Build{nonTerminalElement.Name}({mappedElementForNonTerminal.UmlClass.Name.LowerCaseFirstLetter()}, {indexName}, stringBuilder);");
                            }
                            
                            writer.WriteSafeString($"{Environment.NewLine}break;{Environment.NewLine}");
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
                            writer.WriteSafeString($"throw new System.NotSupportedException(\"Multiple alternatives processing {string.Join(',', types.Select(x => x.Name))} not implemented yet\");");
                        }
                    }
                }
                else
                {
                    writer.WriteSafeString("throw new System.NotSupportedException(\"Multiple alternatives not implemented yet\");");
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
                    writer.WriteSafeString($"stringBuilder.Append(\" {terminalElement.Value} \");");
                    break;
                case NonTerminalElement:
                {
                    var nonTerminalElements = alternatives.SelectMany(x => x.Elements).OfType<NonTerminalElement>().ToList();
                    var mappedNonTerminalElements = OrderElementsByInheritance(nonTerminalElements, umlClass.Cache, ruleGenerationContext);

                    if (mappedNonTerminalElements.Select(x => x.UmlClass).Distinct().Count() != mappedNonTerminalElements.Count)
                    {
                        writer.WriteSafeString("throw new System.NotSupportedException(\"Multiple alternatives with same referenced rule type not implemented yet\");");
                        break;
                    }

                    var variableName = "poco";

                    if (ruleGenerationContext.CallerRule is AssignmentElement assignmentElement)
                    {
                        var cursorToUse = ruleGenerationContext.DefinedCursors.Single(x => x.ApplicableRuleElements.Contains(assignmentElement));
                        variableName = $"{cursorToUse.CursorVariableName}.Current";
                    }

                    writer.WriteSafeString($"switch({variableName}){Environment.NewLine}");
                    writer.WriteSafeString("{");

                    foreach (var orderedNonTerminalElement in mappedNonTerminalElements)
                    {
                        var previousVariableName = ruleGenerationContext.CurrentVariableName;

                        if (orderedNonTerminalElement.UmlClass == ruleGenerationContext.NamedElementToGenerate)
                        {
                            writer.WriteSafeString($"default:{Environment.NewLine}");
                            ruleGenerationContext.CurrentVariableName = variableName;
                        }
                        else
                        {
                            writer.WriteSafeString($"case {orderedNonTerminalElement.UmlClass.QueryFullyQualifiedTypeName(targetInterface: orderedNonTerminalElement.UmlClass.IsAbstract)} poco{orderedNonTerminalElement.UmlClass.Name}:{Environment.NewLine}");
                            ruleGenerationContext.CurrentVariableName = $"poco{orderedNonTerminalElement.UmlClass.Name}";
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
                                    writer.WriteSafeString($"case {orderedElement.UmlClass.QueryFullyQualifiedTypeName(targetInterface: orderedElement.UmlClass.IsAbstract)} {orderedElement.UmlClass.Name.LowerCaseFirstLetter()}:{Environment.NewLine}");
                                }
                                else
                                {
                                    writer.WriteSafeString($"case {orderedElement.UmlClass.QueryFullyQualifiedTypeName(targetInterface: orderedElement.UmlClass.IsAbstract)} {orderedElement.UmlClass.Name.LowerCaseFirstLetter()} when {orderedElement.UmlClass.Name.LowerCaseFirstLetter()}.IsValidFor{orderedElement.RuleElement.Name}():{Environment.NewLine}");
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
                                    writer.WriteSafeString($"case {orderedElement.UmlClass.QueryFullyQualifiedTypeName(targetInterface: orderedElement.UmlClass.IsAbstract)} {orderedElement.UmlClass.Name.LowerCaseFirstLetter()}:{Environment.NewLine}");
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
                    writer.WriteSafeString($"throw new System.NotSupportedException(\"Multiple alternatives with only {firstRuleElement.GetType().Name} not implemented yet\");");
                    break;
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
            
            mapping.Sort((a, b) =>
            {
                if (a.UmlClass == ruleGenerationContext.NamedElementToGenerate)
                {
                    return 1;
                }

                if (a.UmlClass == b.UmlClass)
                {
                    return 0;
                }

                if (a.UmlClass.QueryIsSubclassOf(b.UmlClass))
                {
                    return -1;
                }

                return b.UmlClass.QueryIsSubclassOf(a.UmlClass) ? 1 : 0;
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
                    var valueToAdd = terminalElement.Value;

                    if (valueToAdd.Length > 1)
                    {
                        valueToAdd += ' ';
                    }

                    writer.WriteSafeString($"stringBuilder.Append(\"{valueToAdd}\");");
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

                    if (!groupElement.IsOptional)
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
                        writer.WriteSafeString("stringBuilder.Append(' ');");
                    }
                    else
                    {
                        writer.WriteSafeString("throw new System.NotSupportedException(\"Value Literal different than QualifiedName not supported\");");
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
                        writer.WriteSafeString("throw new System.NotSupportedException(\"Assigment of enumerable with non NonTerminalElement not supported yet\");");
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
                                        writer.WriteSafeString("stringBuilder.Append(' ');");
                                    }
                                    else
                                    {
                                        writer.WriteSafeString($"{Environment.NewLine}if (poco.{targetPropertyName} != null){Environment.NewLine}");
                                        writer.WriteSafeString($"{{{Environment.NewLine}");
                                        writer.WriteSafeString($"stringBuilder.Append(poco.{targetPropertyName}.qualifiedName);{Environment.NewLine}");
                                        writer.WriteSafeString("stringBuilder.Append(' ');");
                                        writer.WriteSafeString($"{Environment.NewLine}}}");
                                    }

                                    break;
                                default:
                                    writer.WriteSafeString("throw new System.NotSupportedException(\"Assigment of reference element not supported yet for this case\");");
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
                writer.WriteSafeString($"// Handle collection Non Terminal {Environment.NewLine}");
                
                if (referencedRule is not { IsDispatcherRule: true })
                {
                    if (nonTerminalElement.TextualNotationRule.IsDispatcherRule)
                    {
                        writer.WriteSafeString($"Build{ nonTerminalElement.Name}Internal({ruleGenerationContext.CurrentVariableName}, elementIndex, stringBuilder);");
                    }
                    else
                    {
                        writer.WriteSafeString($"Build{ nonTerminalElement.Name}Internal({ruleGenerationContext.CurrentVariableName}, stringBuilder);");
                    }
                }
                else
                {
                    var assignmentProperty = referencedRule.GetAssignmentElementNeedingDispatcher().Property;
                    
                    writer.WriteSafeString($"for(var {assignmentProperty}Index = 0; {assignmentProperty}Index < {ruleGenerationContext.CurrentVariableName}.{assignmentProperty.CapitalizeFirstLetter()}.Count; {assignmentProperty}Index++){Environment.NewLine}");
                    writer.WriteSafeString($"{{{Environment.NewLine}");
                }
            }

            if (typeTarget != ruleGenerationContext.NamedElementToGenerate.Name)
            {
                var targetType = umlClass.Cache.Values.OfType<INamedElement>().SingleOrDefault(x => x.Name == typeTarget);
                                
                if (targetType != null)
                {
                    if (targetType is IClass targetClass && (umlClass.QueryAllGeneralClassifiers().Contains(targetClass) || !ruleGenerationContext.CurrentVariableName.Contains("poco")))
                    {
                        if (ruleGenerationContext.CallerRule is AssignmentElement assignmentElement && assignmentElement.TextualNotationRule.IsDispatcherRule)
                        {
                            var castedVariableName = $"elementAs{targetClass.Name}";
                            writer.WriteSafeString($"{Environment.NewLine}if ({ruleGenerationContext.CurrentVariableName} is {targetClass.QueryFullyQualifiedTypeName()} {castedVariableName}){Environment.NewLine}");
                            ruleGenerationContext.CurrentVariableName = castedVariableName;
                            writer.WriteSafeString($"{{{Environment.NewLine}");
                        }

                        if (referencedRule?.IsDispatcherRule == true)
                        {
                            if (nonTerminalElement.IsCollection)
                            {
                                var indexName = $"{referencedRule.GetAssignmentElementNeedingDispatcher().Property}Index";
                                writer.WriteSafeString($"{indexName} = {targetType.Name}TextualNotationBuilder.Build{nonTerminalElement.Name}({ruleGenerationContext.CurrentVariableName}, {indexName}, stringBuilder);");
                            }
                            else
                            {
                                writer.WriteSafeString($"{targetType.Name}TextualNotationBuilder.Build{nonTerminalElement.Name}({ruleGenerationContext.CurrentVariableName}, 0, stringBuilder);");
                            }
                        }
                        else
                        {
                            writer.WriteSafeString($"{targetType.Name}TextualNotationBuilder.Build{nonTerminalElement.Name}({ruleGenerationContext.CurrentVariableName}, stringBuilder);");
                        }

                        if (ruleGenerationContext.CallerRule is AssignmentElement { TextualNotationRule: {IsDispatcherRule: true} })
                        {
                            writer.WriteSafeString($"{Environment.NewLine}}}");
                        }
                    }
                    else
                    {
                        var previousCaller = ruleGenerationContext.CallerRule;
                        ruleGenerationContext.CallerRule = nonTerminalElement;
                        var previousName = ruleGenerationContext.CurrentVariableName;

                        if (referencedRule?.IsDispatcherRule == true)
                        {
                            ruleGenerationContext.CurrentVariableName = $"{previousName}.{referencedRule.GetAssignmentElementNeedingDispatcher().Property.CapitalizeFirstLetter()}";
                        }

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

                    if (referencedRule?.IsDispatcherRule == true)
                    {
                        ruleGenerationContext.CurrentVariableName = $"{previousName}.{referencedRule.GetAssignmentElementNeedingDispatcher().Property.CapitalizeFirstLetter()}";
                    }

                    ProcessAlternatives(writer, umlClass, referencedRule?.Alternatives, ruleGenerationContext, isPartOfMultipleAlternative);
                    ruleGenerationContext.CallerRule = previousCaller;
                    ruleGenerationContext.CurrentVariableName = previousName;
                }
            }
            else
            {
                if (referencedRule?.IsDispatcherRule == true)
                {
                    if (nonTerminalElement.IsCollection)
                    {
                        var indexName = $"{referencedRule.GetAssignmentElementNeedingDispatcher().Property}Index";
                        writer.WriteSafeString($"{indexName} = Build{nonTerminalElement.Name}({ruleGenerationContext.CurrentVariableName}, {indexName}, stringBuilder);");
                    }
                    else
                    {
                        writer.WriteSafeString($"Build{nonTerminalElement.Name}({ruleGenerationContext.CurrentVariableName}, 0, stringBuilder);");
                    }
                }
                else
                {
                    writer.WriteSafeString($"Build{ nonTerminalElement.Name}({ruleGenerationContext.CurrentVariableName}, stringBuilder);");
                }
            }

            if (nonTerminalElement.IsCollection && referencedRule?.IsDispatcherRule == true)
            {
                writer.WriteSafeString($"{Environment.NewLine}}}");
            }

            if (isForProperty && !isPartOfMultipleAlternative)
            {
                writer.WriteSafeString($"{Environment.NewLine}}}");
            }
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
        }
    }
}
