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
                    ProcessAlternatives(writer, umlClass, textualRule.Alternatives, allRules);
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
        private static void ProcessAlternatives(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, IReadOnlyCollection<TextualNotationRule> rules)
        {
            if (alternatives.Count == 1)
            {
                foreach (var textualRuleElement in alternatives.ElementAt(0).Elements)
                {
                    ProcessRuleElement(writer, umlClass, rules, textualRuleElement);
                }
            }
            else
            {
                writer.WriteSafeString("throw new System.NotSupportedException(\"Multiple alternatives not implemented yet\");");
            }
        }

        /// <summary>
        /// Processes a <see cref="RuleElement"/>
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass"/></param>
        /// <param name="rules">A collection of all existing rules</param>
        /// <param name="textualRuleElement">The <see cref="RuleElement"/> to process</param>
        /// <exception cref="ArgumentException">If the type of the <see cref="RuleElement"/> is not supported</exception>
        private static void ProcessRuleElement(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<TextualNotationRule> rules, RuleElement textualRuleElement)
        {
            switch (textualRuleElement)
            {
                case TerminalElement terminalElement:
                    writer.WriteSafeString($"stringBuilder.Append(\"{terminalElement.Value} \");");
                    break;
                case NonTerminalElement nonTerminalElement:
                    var referencedRule = rules.Single(x => x.RuleName == nonTerminalElement.Name);
                    var typeTarget = referencedRule.TargetElementName ?? referencedRule.RuleName;
                    writer.WriteSafeString($"// non Terminal : {nonTerminalElement.Name}; Found rule {referencedRule.RawRule} {Environment.NewLine}");
                            
                    if (typeTarget != umlClass.Name)
                    {
                        var targetType = umlClass.Cache.Values.OfType<INamedElement>().SingleOrDefault(x => x.Name == typeTarget);
                                
                        if (targetType != null)
                        {
                            if (targetType is IClass targetClass && umlClass.QueryAllGeneralClassifiers().Contains(targetClass))
                            {
                                writer.WriteSafeString($"{targetType.Name}TextualNotationBuilder.Build{referencedRule.RuleName}(poco, stringBuilder);");
                            }
                            else
                            {
                                ProcessAlternatives(writer, umlClass, referencedRule.Alternatives, rules);
                            }
                        }
                        else
                        {
                            ProcessAlternatives(writer, umlClass, referencedRule.Alternatives, rules);
                        }
                    }
                    else
                    {
                        writer.WriteSafeString($"Build{referencedRule.RuleName}(poco, stringBuilder);");
                    }

                    break;
                case GroupElement groupElement:
                    writer.WriteSafeString($"// Group Element{Environment.NewLine}");
                    ProcessAlternatives(writer, umlClass, groupElement.Alternatives, rules);
                    break;
                case AssignmentElement assignmentElement:
                    writer.WriteSafeString($"// Assignment Element : {assignmentElement.Property} {assignmentElement.Operator} {assignmentElement.Value}{Environment.NewLine}");
                    var properties = umlClass.QueryAllProperties();
                    var targetProperty = properties.SingleOrDefault(x => string.Equals(x.Name, assignmentElement.Property, StringComparison.OrdinalIgnoreCase));

                    if (targetProperty != null)
                    {
                        writer.WriteSafeString($"// If property {targetProperty.Name} value is set, print {assignmentElement.Value}");
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
    }
}
