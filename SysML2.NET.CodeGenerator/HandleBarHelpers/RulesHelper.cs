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
                    var ruleGenerationContext = new RuleGenerationContext(namedElement);
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
        private static void ProcessAlternatives(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
        {
            ruleGenerationContext.DefinedIterators ??= [];
            
            if (alternatives.Count == 1)
            {
                var alternative = alternatives.ElementAt(0);
                var elements = alternative.Elements;
                DeclareAllRequiredIterators(writer, umlClass, alternative, ruleGenerationContext);
                
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
                                var iterator = ruleGenerationContext.DefinedIterators.FirstOrDefault(x => x.ApplicableRuleElements.Contains(assigment));
                                ifStatementContent.Add(iterator == null ? $"BuildGroupConditionFor{assigment.TextualNotationRule.RuleName}(poco)" : property.QueryIfStatementContentForNonEmpty(iterator.IteratorVariableName));
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
                        ProcessRuleElement(writer, umlClass, textualRuleElement, ruleGenerationContext);
                        ruleGenerationContext.CallerRule = previousCaller;
                    }    
                }
            }
            else
            {
                if (alternatives.All(x => x.Elements.Count == 1))
                {
                    var types = alternatives.SelectMany(x => x.Elements).Select(x => x.GetType()).Distinct().ToList();
                    
                    if(types.Count == 1)
                    {
                        ProcessUnitypedAlternativesWithOneElement(writer, umlClass, alternatives, ruleGenerationContext);
                    }
                    else
                    {
                        
                        writer.WriteSafeString($"throw new System.NotSupportedException(\"Multiple alternatives with only one of the different type not implemented yet - {string.Join(',', types.Select(x => x.Name))}\");");
                    }
                }
                else
                {
                    writer.WriteSafeString("throw new System.NotSupportedException(\"Multiple alternatives not implemented yet\");");
                }
            }
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
                        var iteratorToUse = ruleGenerationContext.DefinedIterators.Single(x => x.ApplicableRuleElements.Contains(assignmentElement));
                        variableName = $"{iteratorToUse.IteratorVariableName}.Current";
                        writer.WriteSafeString($"{iteratorToUse.IteratorVariableName}.MoveNext();{Environment.NewLine}{Environment.NewLine}");
                    }

                    writer.WriteSafeString($"switch({variableName}){Environment.NewLine}");
                    writer.WriteSafeString("{");

                    foreach (var orderedNonTerminalElement in mappedNonTerminalElements)
                    {
                        if (orderedNonTerminalElement.UmlClass == ruleGenerationContext.NamedElementToGenerate)
                        {
                            writer.WriteSafeString($"default:{Environment.NewLine}");
                            ProcessNonTerminalElement(writer, orderedNonTerminalElement.UmlClass, orderedNonTerminalElement.RuleElement, variableName, ruleGenerationContext);
                        }
                        else
                        {
                            writer.WriteSafeString($"case {orderedNonTerminalElement.UmlClass.QueryFullyQualifiedTypeName(targetInterface: orderedNonTerminalElement.UmlClass.IsAbstract)} poco{orderedNonTerminalElement.UmlClass.Name}:{Environment.NewLine}");
                            ProcessNonTerminalElement(writer, orderedNonTerminalElement.UmlClass, orderedNonTerminalElement.RuleElement, $"poco{orderedNonTerminalElement.UmlClass.Name}", ruleGenerationContext);
                        }

                        writer.WriteSafeString($"{Environment.NewLine}break;{Environment.NewLine}");
                    }

                    writer.WriteSafeString($"}}{Environment.NewLine}");

                    break;
                }
                case AssignmentElement:
                    var assignmentElements = alternatives.SelectMany(x => x.Elements).OfType<AssignmentElement>().ToList();
                    var propertiesTarget = assignmentElements.Select(x => x.Property).Distinct().ToList();

                    if (propertiesTarget.Count == 1)
                    {
                        var targetProperty = umlClass.QueryAllProperties().Single(x => string.Equals(x.Name, propertiesTarget[0]));

                        var orderElementsByInheritance = OrderElementsByInheritance(assignmentElements.Select(x => x.Value).OfType<NonTerminalElement>().ToList(), umlClass.Cache, ruleGenerationContext);
                        
                        if (assignmentElements.All(x => x.Operator == "+=") && assignmentElements.All(x => x.Value is NonTerminalElement))
                        {
                            if (orderElementsByInheritance.Select(x => x.UmlClass).Distinct().Count() != orderElementsByInheritance.Count)
                            {
                                writer.WriteSafeString($"Build{alternatives.ElementAt(0).TextualNotationRule.RuleName}Alternatives(poco, stringBuilder);");
                            }
                            else
                            {
                                writer.WriteSafeString($"foreach(var elementIn{targetProperty.Name.CapitalizeFirstLetter()} in poco.{targetProperty.QueryPropertyNameBasedOnUmlProperties()}){Environment.NewLine}");
                                writer.WriteSafeString($"{{{Environment.NewLine}");
                                writer.WriteSafeString($"switch(elementIn{targetProperty.Name.CapitalizeFirstLetter()}){Environment.NewLine}");
                                writer.WriteSafeString($"{{{Environment.NewLine}");

                                foreach (var orderedElement in orderElementsByInheritance)
                                {
                                    writer.WriteSafeString($"case {orderedElement.UmlClass.QueryFullyQualifiedTypeName(targetInterface:orderedElement.UmlClass.IsAbstract)} {orderedElement.UmlClass.Name.LowerCaseFirstLetter()}:{Environment.NewLine}");
                                    ProcessNonTerminalElement(writer, orderedElement.UmlClass, orderedElement.RuleElement, orderedElement.UmlClass.Name.LowerCaseFirstLetter(),  ruleGenerationContext);
                                    writer.WriteSafeString($"{Environment.NewLine}break;{Environment.NewLine}");
                                }
                            
                                writer.WriteSafeString($"}}{Environment.NewLine}");
                                writer.WriteSafeString($"}}{Environment.NewLine}");
                            }
                        }
                        else
                        {
                            var typeFiltering = string.Join(", ", orderElementsByInheritance.Select(x => $"typeof({x.UmlClass.QueryFullyQualifiedTypeName(targetInterface: x.UmlClass.IsAbstract)})"));
                            writer.WriteSafeString($"using var iterator = SysML2.NET.Extensions.EnumerableExtensions.GetElementsOfType(poco.{targetProperty.QueryPropertyNameBasedOnUmlProperties()}, {typeFiltering}).GetEnumerator();{Environment.NewLine}");
                            writer.WriteSafeString($"iterator.MoveNext();{Environment.NewLine}{Environment.NewLine}");
                            writer.WriteSafeString("if(iterator.Current != null)");
                            writer.WriteSafeString($"{{{Environment.NewLine}");
                            
                            writer.WriteSafeString($"switch(iterator.Current){Environment.NewLine}");
                            writer.WriteSafeString($"{{{Environment.NewLine}");

                            foreach (var orderedElement in orderElementsByInheritance)
                            {
                                if (orderedElement.UmlClass.Name == "Element")
                                {
                                    writer.WriteSafeString($"case {{ }} {orderedElement.UmlClass.Name.LowerCaseFirstLetter()}:{Environment.NewLine}");                                    
                                }
                                else
                                {
                                    writer.WriteSafeString($"case {orderedElement.UmlClass.QueryFullyQualifiedTypeName(targetInterface:orderedElement.UmlClass.IsAbstract)} {orderedElement.UmlClass.Name.LowerCaseFirstLetter()}:{Environment.NewLine}");
                                }

                                ProcessNonTerminalElement(writer, orderedElement.UmlClass, orderedElement.RuleElement, orderedElement.UmlClass.Name.LowerCaseFirstLetter(),  ruleGenerationContext);
                                writer.WriteSafeString($"{Environment.NewLine}break;{Environment.NewLine}");
                            }
                            
                            writer.WriteSafeString($"}}{Environment.NewLine}");
                            writer.WriteSafeString($"}}{Environment.NewLine}");
                        }
                    }
                    else
                    {
                        foreach (var alternative in alternatives)
                        {
                            DeclareAllRequiredIterators(writer, umlClass, alternative, ruleGenerationContext);
                        }

                        var properties = umlClass.QueryAllProperties();
                        
                        for (var alternativeIndex = 0; alternativeIndex < alternatives.Count; alternativeIndex++)
                        {
                            if (alternativeIndex != 0)
                            {
                                writer.WriteSafeString("else ");
                            }
                            
                            var assignment =assignmentElements[alternativeIndex];
                            var targetProperty = properties.Single(x => string.Equals(x.Name, assignment.Property));
                            
                            var iterator = ruleGenerationContext.DefinedIterators.SingleOrDefault(x => x.ApplicableRuleElements.Contains(assignment));
                            writer.WriteSafeString($"if({targetProperty.QueryIfStatementContentForNonEmpty(iterator?.IteratorVariableName ?? "poco")}){Environment.NewLine}"); 
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
        /// Declares all required iterator for all <see cref="RuleElement"/> present inside an <see cref="Alternatives"/>
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass"/></param>
        /// <param name="alternatives">The <see cref="Alternatives"/> to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext"/></param>
        private static void DeclareAllRequiredIterators(EncodedTextWriter writer, IClass umlClass, Alternatives alternatives, RuleGenerationContext ruleGenerationContext)
        {
            foreach (var ruleElement in alternatives.Elements)
            {
                switch (ruleElement)
                {
                    case AssignmentElement assignmentElement:
                        DeclareIteratorIfRequired(writer, umlClass, assignmentElement, ruleGenerationContext);
                        break;
                    case GroupElement groupElement:
                        foreach (var groupElementAlternative in groupElement.Alternatives)
                        {
                            DeclareAllRequiredIterators(writer, umlClass, groupElementAlternative, ruleGenerationContext);
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
        /// <exception cref="ArgumentException">If the type of the <see cref="RuleElement"/> is not supported</exception>
        private static void ProcessRuleElement(EncodedTextWriter writer, IClass umlClass, RuleElement textualRuleElement, RuleGenerationContext ruleGenerationContext)
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
                        ProcessNonTerminalElement(writer, umlClass, nonTerminalElement, assignedProperty == null ? "poco" : $"poco.{assignedProperty.QueryPropertyNameBasedOnUmlProperties()}", ruleGenerationContext);
                    }
                    else
                    {
                        ProcessNonTerminalElement(writer, umlClass, nonTerminalElement, "poco", ruleGenerationContext);
                    }

                    break;
                case GroupElement groupElement:
                    ruleGenerationContext.CallerRule = groupElement ;
                    
                    if (groupElement.IsCollection)
                    {
                        var assignmentRule = groupElement.Alternatives.SelectMany(x => x.Elements).FirstOrDefault(x => x is AssignmentElement { Value: NonTerminalElement } || x is AssignmentElement {Value: ValueLiteralElement});

                        if (assignmentRule is AssignmentElement assignmentElement)
                        {
                            var iteratorToUse = ruleGenerationContext.DefinedIterators.Single(x => x.ApplicableRuleElements.Contains(assignmentElement));
                            writer.WriteSafeString($"{Environment.NewLine}while({iteratorToUse.IteratorVariableName}.MoveNext()){Environment.NewLine}");
                        }

                        writer.WriteSafeString($"{{{Environment.NewLine}");
                        ProcessAlternatives(writer, umlClass, groupElement.Alternatives, ruleGenerationContext);
                        writer.WriteSafeString($"{Environment.NewLine}}}");
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
                    ProcessAssignmentElement(writer, umlClass, ruleGenerationContext, assignmentElement);
                    break;
                case NonParsingAssignmentElement nonParsingAssignmentElement:
                    writer.WriteSafeString($"// NonParsing Assignment Element : {nonParsingAssignmentElement.PropertyName} {nonParsingAssignmentElement.Operator} {nonParsingAssignmentElement.Value} => Does not have to be process");
                    break;
                case ValueLiteralElement valueLiteralElement:
                    if (valueLiteralElement.QueryIsQualifiedName())
                    {
                        writer.WriteSafeString($"stringBuilder.Append(poco.qualifiedName);{Environment.NewLine}");
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
        /// <param name="isPartOfMultipleAlternative"></param>
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
                        var iteratorToUse = ruleGenerationContext.DefinedIterators.Single(x => x.ApplicableRuleElements.Contains(assignmentElement));
                                
                        if (!isPartOfMultipleAlternative && assignmentElement.Container is not GroupElement { IsCollection: true } && assignmentElement.Container is not GroupElement { IsOptional: true })
                        {
                            writer.WriteSafeString($"{iteratorToUse.IteratorVariableName}.MoveNext();{Environment.NewLine}");
                        }

                        ProcessNonTerminalElement(writer, umlClass, nonTerminalElement, $"{iteratorToUse.IteratorVariableName}.Current", ruleGenerationContext);
                    }
                    else if(assignmentElement.Value is GroupElement groupElement)
                    {
                        var previousCaller = ruleGenerationContext.CallerRule;
                        ruleGenerationContext.CallerRule = assignmentElement;
                        ProcessAlternatives(writer, umlClass, groupElement.Alternatives, ruleGenerationContext);
                        ruleGenerationContext.CallerRule = previousCaller;
                    }
                    else if(assignmentElement.Value is ValueLiteralElement valueLiteralElement && valueLiteralElement.QueryIsQualifiedName())
                    {
                        var iteratorToUse = ruleGenerationContext.DefinedIterators.Single(x => x.ApplicableRuleElements.Contains(assignmentElement));

                        if (assignmentElement.Container is not GroupElement { IsCollection: true } && assignmentElement.Container is not GroupElement { IsOptional: true })
                        {
                            writer.WriteSafeString($"{iteratorToUse.IteratorVariableName}.MoveNext();{Environment.NewLine}");
                        }
                                
                        writer.WriteSafeString($"{Environment.NewLine}if({iteratorToUse.IteratorVariableName}.Current != null){Environment.NewLine}");
                        writer.WriteSafeString($"{{{Environment.NewLine}");
                        writer.WriteSafeString($"stringBuilder.Append({iteratorToUse.IteratorVariableName}.Current.qualifiedName);{Environment.NewLine}");
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
                        else if(targetProperty.QueryIsReferenceProperty())
                        {
                            switch (assignmentElement.Value)
                            {
                                case NonTerminalElement nonTerminalElement:
                                {
                                    var previousCaller = ruleGenerationContext.CallerRule;
                                    ruleGenerationContext.CallerRule = nonTerminalElement;
                                    ProcessNonTerminalElement(writer, targetProperty.Type as IClass, nonTerminalElement, $"poco.{targetPropertyName}", ruleGenerationContext);
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
                writer.WriteSafeString($"Build{assignmentElement.Property.CapitalizeFirstLetter()}(poco, stringBuilder);");
            }
        }

        /// <summary>
        /// Process a <see cref="NonTerminalElement"/>
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass"/></param>
        /// <param name="nonTerminalElement">The <see cref="NonTerminalElement"/> to process</param>
        /// <param name="variableName">The name of the variable that should be used to call the non-terminal method</param>
        /// <param name="ruleGenerationContext"></param>
        private static void ProcessNonTerminalElement(EncodedTextWriter writer, IClass umlClass, NonTerminalElement nonTerminalElement, string variableName, RuleGenerationContext ruleGenerationContext)
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

            var isForProperty = variableName.Contains('.');
            
            if (isForProperty)
            {
                writer.WriteSafeString($"{Environment.NewLine}if ({variableName} != null){Environment.NewLine}");
                writer.WriteSafeString($"{{{Environment.NewLine}");
            }
                            
            if (typeTarget != ruleGenerationContext.NamedElementToGenerate.Name)
            {
                var targetType = umlClass.Cache.Values.OfType<INamedElement>().SingleOrDefault(x => x.Name == typeTarget);
                                
                if (targetType != null)
                {
                    if (targetType is IClass targetClass && (umlClass.QueryAllGeneralClassifiers().Contains(targetClass) || !variableName.Contains("poco")))
                    {
                        writer.WriteSafeString($"{targetType.Name}TextualNotationBuilder.Build{nonTerminalElement.Name}({variableName}, stringBuilder);");
                    }
                    else
                    {
                        var previousCaller = ruleGenerationContext.CallerRule;
                        ruleGenerationContext.CallerRule = nonTerminalElement;
                        ProcessAlternatives(writer, umlClass, referencedRule?.Alternatives, ruleGenerationContext);
                        ruleGenerationContext.CallerRule = previousCaller;
                    }
                }
                else
                {
                    var previousCaller = ruleGenerationContext.CallerRule;
                    ruleGenerationContext.CallerRule = nonTerminalElement;
                    ProcessAlternatives(writer, umlClass, referencedRule?.Alternatives, ruleGenerationContext);
                    ruleGenerationContext.CallerRule = previousCaller;
                }
            }
            else
            {
                writer.WriteSafeString($"Build{ nonTerminalElement.Name}({variableName}, stringBuilder);");
            }

            if (isForProperty)
            {
                writer.WriteSafeString($"{Environment.NewLine}}}");
            }
        }

        /// <summary>
        /// Declares an iterator to perform iteration over a collection, if required to declare it
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass"/></param>
        /// <param name="assignmentElement">The <see cref="AssignmentElement"/> to process</param>
        /// <param name="ruleGenerationContext"></param>
        private static void DeclareIteratorIfRequired(EncodedTextWriter writer, IClass umlClass, AssignmentElement assignmentElement, RuleGenerationContext ruleGenerationContext)
        {
            var allProperties = umlClass.QueryAllProperties();
            var targetProperty =  allProperties.SingleOrDefault(x => string.Equals(x.Name, assignmentElement.Property, StringComparison.OrdinalIgnoreCase));
            
            if (targetProperty == null || !targetProperty.QueryIsEnumerable())
            {
                return;
            }

            switch (assignmentElement.Value)
            {
                case NonTerminalElement nonTerminalElement:
                {
                    var referencedRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == nonTerminalElement.Name);

                    if (ruleGenerationContext.DefinedIterators.SingleOrDefault(x => x.IsIteratorValidForProperty(targetProperty, referencedRule) || x.ApplicableRuleElements.Contains(assignmentElement)) is { } alreadyDefinedIterator)
                    {
                        alreadyDefinedIterator.ApplicableRuleElements.Add(assignmentElement);
                        return;
                    }

                    var iteratorToUse = new IteratorDefinition
                    {
                        DefinedForProperty = targetProperty
                    };

                    iteratorToUse.ApplicableRuleElements.Add(assignmentElement);

                    string typeTarget;

                    if (referencedRule == null)
                    {
                        typeTarget = umlClass.Name;
                    }
                    else
                    {
                        typeTarget = referencedRule.TargetElementName ?? referencedRule.RuleName;
                    }

                    if (typeTarget != targetProperty.Type.Name)
                    {
                        var targetType = umlClass.Cache.Values.OfType<INamedElement>().SingleOrDefault(x => x.Name == typeTarget);
                        iteratorToUse.ConstrainedType = targetType;

                        writer.WriteSafeString(targetType != null
                            ? $"using var {iteratorToUse.IteratorVariableName} = poco.{targetProperty.QueryPropertyNameBasedOnUmlProperties()}.OfType<{targetType.QueryFullyQualifiedTypeName(targetInterface: false)}>().GetEnumerator();"
                            : $"using var {iteratorToUse.IteratorVariableName} = poco.{targetProperty.QueryPropertyNameBasedOnUmlProperties()}.GetEnumerator();");
                    }
                    else
                    {
                        writer.WriteSafeString($"using var {iteratorToUse.IteratorVariableName} = poco.{targetProperty.QueryPropertyNameBasedOnUmlProperties()}.GetEnumerator();");
                    }

                    writer.WriteSafeString(Environment.NewLine);
                    ruleGenerationContext.DefinedIterators.Add(iteratorToUse);
                    break;
                }
                case ValueLiteralElement:
                    if (ruleGenerationContext.DefinedIterators.SingleOrDefault(x => x.IsIteratorValidForProperty(targetProperty, null) || x.ApplicableRuleElements.Contains(assignmentElement)) is { } existingValueLiteralIterator)
                    {
                        existingValueLiteralIterator.ApplicableRuleElements.Add(assignmentElement);
                        return;
                    }

                    var valueLiteralIterator = new IteratorDefinition
                    {
                        DefinedForProperty = targetProperty
                    };

                    valueLiteralIterator.ApplicableRuleElements.Add(assignmentElement);
                    writer.WriteSafeString($"using var {valueLiteralIterator.IteratorVariableName} = poco.{targetProperty.QueryPropertyNameBasedOnUmlProperties()}.GetEnumerator();");
                    writer.WriteSafeString(Environment.NewLine);
                    ruleGenerationContext.DefinedIterators.Add(valueLiteralIterator);
                    
                    break;
                case AssignmentElement containedAssignment:
                    DeclareIteratorIfRequired(writer, umlClass, containedAssignment, ruleGenerationContext);
                    break;
                case GroupElement groupElement:
                    var nonTerminalRules = groupElement.Alternatives.SelectMany(x => x.Elements).OfType<NonTerminalElement>().ToList();
                    
                    if (ruleGenerationContext.DefinedIterators.Any(x => x.ApplicableRuleElements.Contains(assignmentElement)))
                    {
                        return;
                    }

                    var referencedTypes = new HashSet<INamedElement>();

                    foreach (var nonTerminalElement in nonTerminalRules)
                    {
                        var referencedRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == nonTerminalElement.Name);

                        if (ruleGenerationContext.DefinedIterators.SingleOrDefault(x => x.IsIteratorValidForProperty(targetProperty, referencedRule)) is { } alreadyDefined)
                        {
                            alreadyDefined.ApplicableRuleElements.Add(assignmentElement);
                            return;
                        }

                        string typeTarget;

                        if (referencedRule == null)
                        {
                            typeTarget = umlClass.Name;
                        }
                        else
                        {
                            typeTarget = referencedRule.TargetElementName ?? referencedRule.RuleName;
                        }
                        
                        referencedTypes.Add(umlClass.Cache.Values.OfType<INamedElement>().Single(x => x.Name == typeTarget));
                    }
                    
                    var iteratorToUseForGroup = new IteratorDefinition
                    {
                        DefinedForProperty = targetProperty
                    };

                    var typesFilter = string.Join(", ", referencedTypes.Select(x => $"typeof({x.QueryFullyQualifiedTypeName(targetInterface: false)})"));
                    writer.WriteSafeString($"using var {iteratorToUseForGroup.IteratorVariableName} = SysML2.NET.Extensions.EnumerableExtensions.GetElementsOfType(poco.{targetProperty.QueryPropertyNameBasedOnUmlProperties()}, {typesFilter}).GetEnumerator();{Environment.NewLine}");
                    
                    iteratorToUseForGroup.ApplicableRuleElements.Add(assignmentElement);
                    ruleGenerationContext.DefinedIterators.Add(iteratorToUseForGroup);
                    break;
            }
        }

        /// <summary>
        /// Keeps tracks of defined iterator for enumerable <see cref="IProperty"/>
        /// </summary>
        private class IteratorDefinition
        {
            /// <summary>
            /// Gets or sets the <see cref="IProperty"/> that have to have an iterator defined
            /// </summary>
            public IProperty DefinedForProperty { get; init; }

            /// <summary>
            /// Gets or sets the <see cref="INamedElement"/> that should be 
            /// </summary>
            public INamedElement ConstrainedType { get; set; }

            /// <summary>
            /// Gets the name of the variable defined for the iterator
            /// </summary>
            public string IteratorVariableName => this.ComputeIteratorName();

            /// <summary>
            /// Provides a collection of <see cref="AssignmentElement"/> that will use the defined iterator
            /// </summary>
            public HashSet<AssignmentElement> ApplicableRuleElements { get; } = [];

            /// <summary>
            /// Compute the name of the iterator variable
            /// </summary>
            /// <returns>The computed name</returns>
            private string ComputeIteratorName()
            {
                var name = this.DefinedForProperty.Name.LowerCaseFirstLetter();

                if (this.ConstrainedType != null)
                {
                    name += $"Of{this.ConstrainedType.Name.CapitalizeFirstLetter()}";
                }
                
                name += "Iterator";
                return name;
            }

            /// <summary>
            /// Asserts that the current <see cref="IteratorDefinition"/> is valid for an <see cref="IProperty"/> for a specific <see cref="TextualNotationRule"/>
            /// </summary>
            /// <param name="property">The specific <see cref="IProperty"/></param>
            /// <param name="targetRule">The specific <see cref="TextualNotationRule"/> that should constraint a collection type</param>
            /// <returns>True if the <see cref="IteratorDefinition"/> is valid for the provided parameters</returns>
            public bool IsIteratorValidForProperty(IProperty property, TextualNotationRule targetRule)
            {
                if (property != this.DefinedForProperty)
                {
                    return false;
                }

                if (targetRule == null)
                {
                    return this.ConstrainedType == null;
                }

                var typeTarget = targetRule.TargetElementName ?? targetRule.RuleName;
                
                return string.Equals(this.ConstrainedType?.Name, typeTarget, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        /// <summary>
        /// Provides context over the rule generation history
        /// </summary>
        private class RuleGenerationContext
        {
            /// <summary>
            /// Gets or sets the collection of <see cref="IteratorDefinition" />
            /// </summary>
            public List<IteratorDefinition> DefinedIterators { get; set; }

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
        }
    }
}
