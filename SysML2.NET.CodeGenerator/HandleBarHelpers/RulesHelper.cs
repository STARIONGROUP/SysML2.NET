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
    using SysML2.NET.CodeGenerator.Grammar;
    using SysML2.NET.CodeGenerator.Grammar.Model;

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
                    ProcessAlternatives(writer, umlClass, textualRule.Alternatives, allRules, definedIterators: null);
                }
            });
        }

        /// <summary>
        /// Processes a collection of a <see cref="Alternatives"/>
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass"/></param>
        /// <param name="alternatives">The collection of alternatives to process</param>
        /// <param name="rules">A collection of all existing rules</param>
        /// <param name="definedIterators">Collection of <see cref="IteratorDefinition"/> that keep tracks of defined iterator</param>
        /// <param name="callerElement">An optional <see cref="RuleElement"/> that is calling this function</param>
        private static void ProcessAlternatives(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, 
            IReadOnlyCollection<TextualNotationRule> rules, List<IteratorDefinition> definedIterators, RuleElement callerElement = null)
        {
            definedIterators ??= [];
            
            if (alternatives.Count == 1)
            {
                var alternative = alternatives.ElementAt(0);
                var elements = alternative.Elements;
                DeclareAllRequiredIterators(writer, umlClass, rules, alternative, definedIterators);
                
                if (callerElement is { IsOptional: true, IsCollection: false })
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
                                var iterator = definedIterators.FirstOrDefault(x => x.ApplicableRuleElements.Contains(assigment));
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
                            ProcessRuleElement(writer, umlClass, rules, textualRuleElement, definedIterators);
                        }
                    }
                    else
                    {
                        writer.WriteSafeString($"{Environment.NewLine}if(BuildGroupConditionFor{alternative.TextualNotationRule.RuleName}(poco))");
                        writer.WriteSafeString($"{Environment.NewLine}{{{Environment.NewLine}");

                        foreach (var textualRuleElement in elements)
                        {
                            ProcessRuleElement(writer, umlClass, rules, textualRuleElement, definedIterators);
                        }
                    }

                    writer.WriteSafeString($"}}{Environment.NewLine}");
                }
                else
                {
                    foreach (var textualRuleElement in elements)
                    {
                        ProcessRuleElement(writer, umlClass, rules, textualRuleElement, definedIterators);
                    }    
                }
            }
            else
            {
                writer.WriteSafeString("throw new System.NotSupportedException(\"Multiple alternatives not implemented yet\");");
            }
        }

        /// <summary>
        /// Declares all required iterator for all <see cref="RuleElement"/> present inside an <see cref="Alternatives"/>
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass"/></param>
        /// <param name="rules">A collection of all existing rules</param>
        /// <param name="alternatives">The <see cref="Alternatives"/> to process</param>
        /// <param name="definedIterators">Collection of <see cref="IteratorDefinition"/> that keep tracks of defined iterator</param>
        private static void DeclareAllRequiredIterators(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<TextualNotationRule> rules, Alternatives alternatives, List<IteratorDefinition> definedIterators)
        {
            foreach (var ruleElement in alternatives.Elements)
            {
                switch (ruleElement)
                {
                    case AssignmentElement { Value: NonTerminalElement } assignmentElement:
                        DeclareIteratorIfRequired(writer, umlClass, rules, assignmentElement, definedIterators);
                        break;
                    case AssignmentElement { Value: GroupElement } assignmentElement:
                        DeclareIteratorIfRequired(writer, umlClass, rules, assignmentElement, definedIterators);
                        break;
                    case GroupElement groupElement:
                        foreach (var groupElementAlternative in groupElement.Alternatives)
                        {
                            DeclareAllRequiredIterators(writer, umlClass, rules, groupElementAlternative, definedIterators);
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
        /// <param name="rules">A collection of all existing rules</param>
        /// <param name="textualRuleElement">The <see cref="RuleElement"/> to process</param>
        /// <param name="definedIterators">Collection of <see cref="IteratorDefinition"/> that keep tracks of defined iterator</param>
        /// <exception cref="ArgumentException">If the type of the <see cref="RuleElement"/> is not supported</exception>
        private static void ProcessRuleElement(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<TextualNotationRule> rules, RuleElement textualRuleElement, List<IteratorDefinition> definedIterators)
        {
            switch (textualRuleElement)
            {
                case TerminalElement terminalElement:
                    var valueToAdd = terminalElement.Value;

                    if (valueToAdd!="<" && valueToAdd!=">")
                    {
                        if (valueToAdd == "=")
                        {
                            valueToAdd = $" {valueToAdd} ";
                        }
                        else
                        {
                            valueToAdd += ' ';
                        }
                    }

                    writer.WriteSafeString($"stringBuilder.Append(\"{valueToAdd}\");");
                    break;
                case NonTerminalElement nonTerminalElement:
                    ProcessNonTerminalElement(writer, umlClass, rules, definedIterators, nonTerminalElement, "poco");

                    break;
                case GroupElement groupElement:
                    if (groupElement.IsCollection)
                    {
                        var assignmentRule = groupElement.Alternatives.SelectMany(x => x.Elements).FirstOrDefault(x => x is AssignmentElement { Value: NonTerminalElement });

                        if (assignmentRule is AssignmentElement assignmentElement)
                        {
                            var iteratorToUse = definedIterators.Single(x => x.ApplicableRuleElements.Contains(assignmentElement));
                            writer.WriteSafeString($"{Environment.NewLine}while({iteratorToUse.IteratorVariableName}.MoveNext()){Environment.NewLine}");
                        }

                        writer.WriteSafeString($"{{{Environment.NewLine}");
                        ProcessAlternatives(writer, umlClass, groupElement.Alternatives, rules, definedIterators, groupElement);
                        writer.WriteSafeString($"{Environment.NewLine}}}");
                    }
                    else
                    {
                        ProcessAlternatives(writer, umlClass, groupElement.Alternatives, rules,definedIterators, groupElement);
                    }

                    if (!groupElement.IsOptional)
                    {
                        writer.WriteSafeString($"{Environment.NewLine}stringBuilder.Append(' ');");
                    }

                    break;
                case AssignmentElement assignmentElement:
                    var properties = umlClass.QueryAllProperties();
                    var targetProperty = properties.SingleOrDefault(x => string.Equals(x.Name, assignmentElement.Property, StringComparison.OrdinalIgnoreCase));

                    if (targetProperty != null)
                    {
                        if (targetProperty.QueryIsEnumerable())
                        {
                            if (assignmentElement.Value is NonTerminalElement nonTerminalElement)
                            {
                                var iteratorToUse = definedIterators.Single(x => x.ApplicableRuleElements.Contains(assignmentElement));
                                
                                if (assignmentElement.Container is not GroupElement { IsCollection: true } && assignmentElement.Container is not GroupElement { IsOptional: true })
                                {
                                    writer.WriteSafeString($"{iteratorToUse.IteratorVariableName}.MoveNext();{Environment.NewLine}");
                                }

                                ProcessNonTerminalElement(writer, umlClass, rules, definedIterators, nonTerminalElement, $"{iteratorToUse.IteratorVariableName}.Current");
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
                                writer.WriteSafeString("}}");
                            }
                            else
                            {
                                if (targetProperty.QueryIsString())
                                {
                                    writer.WriteSafeString($"stringBuilder.Append(poco.{targetProperty.Name.CapitalizeFirstLetter()});");
                                }
                                else if(targetProperty.QueryIsBool())
                                {
                                    if (assignmentElement.Value is TerminalElement terminalElement)
                                    {
                                        writer.WriteSafeString($"stringBuilder.Append(\"{terminalElement.Value}\");");
                                    }
                                    else
                                    {
                                        writer.WriteSafeString("throw new System.NotSupportedException(\"Assigment of bool with rule element value different than TerminalElement not supported\");");
                                    }
                                }
                                else if (targetProperty.QueryIsEnum())
                                {
                                    writer.WriteSafeString($"stringBuilder.Append(poco.{targetProperty.Name.CapitalizeFirstLetter()}.ToString().ToLower());");
                                }
                                else
                                {
                                    writer.WriteSafeString("throw new System.NotSupportedException(\"Assigment of non-string value not yet supported\");");
                                }
                            }
                        }
                    }
                    else
                    {
                        writer.WriteSafeString($"Build{assignmentElement.Property.CapitalizeFirstLetter()}(poco, stringBuilder);");
                    }

                    break;
                case NonParsingAssignmentElement nonParsingAssignmentElement:
                    writer.WriteSafeString($"// Assignment Element : {nonParsingAssignmentElement.PropertyName} {nonParsingAssignmentElement.Operator} {nonParsingAssignmentElement.Value}");
                    break;
                case ValueLiteralElement valueLiteralElement:
                    writer.WriteSafeString($"// Value Literal Element : {valueLiteralElement.Value}");
                    break;
                default:
                    throw new ArgumentException("Unknown element type");
            }

            writer.WriteSafeString(Environment.NewLine);
        }

        /// <summary>
        /// Process a <see cref="NonTerminalElement"/>
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass"/></param>
        /// <param name="rules">A collection of all existing rules</param>
        /// <param name="nonTerminalElement">The <see cref="NonTerminalElement"/> to process</param>
        /// <param name="definedIterators">Collection of <see cref="IteratorDefinition"/> that keep tracks of defined iterator</param>
        /// <param name="variableName">The name of the variable that should be used to call the non-terminal method</param>
        private static void ProcessNonTerminalElement(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<TextualNotationRule> rules, List<IteratorDefinition> definedIterators, NonTerminalElement nonTerminalElement, string variableName)
        {
            var referencedRule = rules.SingleOrDefault(x => x.RuleName == nonTerminalElement.Name);

            string typeTarget;
            
            if (referencedRule == null)
            {
                typeTarget = umlClass.Name;                    
            }
            else
            {
                typeTarget = referencedRule.TargetElementName ?? referencedRule.RuleName;
            }
                            
            if (typeTarget != umlClass.Name)
            {
                var targetType = umlClass.Cache.Values.OfType<INamedElement>().SingleOrDefault(x => x.Name == typeTarget);
                                
                if (targetType != null)
                {
                    if (targetType is IClass targetClass && (umlClass.QueryAllGeneralClassifiers().Contains(targetClass) || variableName != "poco"))
                    {
                        writer.WriteSafeString($"{targetType.Name}TextualNotationBuilder.Build{nonTerminalElement.Name}({variableName}, stringBuilder);");
                    }
                    else
                    {
                        ProcessAlternatives(writer, umlClass, referencedRule!.Alternatives, rules, definedIterators);
                    }
                }
                else
                {
                    ProcessAlternatives(writer, umlClass, referencedRule!.Alternatives, rules, definedIterators);
                }
            }
            else
            {
                writer.WriteSafeString($"Build{ nonTerminalElement.Name}({variableName}, stringBuilder);");
            }
        }

        /// <summary>
        /// Declares an iterator to perform iteration over a collection, if required to declare it
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass"/></param>
        /// <param name="rules">A collection of all existing rules</param>
        /// <param name="assignmentElement">The <see cref="AssignmentElement"/> to process</param>
        /// <param name="definedIterators">Collection of <see cref="IteratorDefinition"/> that keep tracks of defined iterator</param>
        private static void DeclareIteratorIfRequired(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<TextualNotationRule> rules, AssignmentElement assignmentElement, List<IteratorDefinition> definedIterators)
        {
            var allProperties = umlClass.QueryAllProperties();
            var targetProperty =  allProperties.SingleOrDefault(x => string.Equals(x.Name, assignmentElement.Property, StringComparison.OrdinalIgnoreCase));
            
            if (targetProperty == null || !targetProperty.QueryIsEnumerable())
            {
                return;
            }

            if (assignmentElement.Value is GroupElement groupElement)
            {
                var groupedAssignment = groupElement.Alternatives.SelectMany(x => x.Elements).OfType<AssignmentElement>();

                foreach (var assignment in groupedAssignment)
                {
                    DeclareIteratorIfRequired(writer, umlClass, rules, assignment, definedIterators);
                }
            }

            if (assignmentElement.Value is not NonTerminalElement nonTerminalElement)
            {
                return;
            }

            var referencedRule = rules.SingleOrDefault(x => x.RuleName == nonTerminalElement.Name);
            
            if (definedIterators.SingleOrDefault(x => x.IsIteratorValidForProperty(targetProperty, referencedRule) || x.ApplicableRuleElements.Contains(assignmentElement)) is { } alreadyDefinedIterator)
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
            definedIterators.Add(iteratorToUse);
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
    }
}
