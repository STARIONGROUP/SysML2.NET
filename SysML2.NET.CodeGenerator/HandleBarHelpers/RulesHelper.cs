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

    using SysML2.NET.CodeGenerator.Grammar.Model;

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
            handlebars.RegisterHelper("RulesHelper.WriteForPoco", (writer, _, arguments) =>
            {
                if (arguments.Length != 2)
                {
                    throw new ArgumentException("RulesHelper.WriteForPoco expects to have 2 arguments");
                }

                if (arguments[0] is not IClass umlClass)
                {
                    throw new ArgumentException("RulesHelper.WriteForPoco expects IClass as first argument");
                }

                if (arguments[1] is not List<TextualNotationRule> rules)
                {
                    throw new ArgumentException("RulesHelper.WriteForPoco expects a list of TextualNotationRule as second argument");
                }
                
                var canonicalRule = rules.SingleOrDefault(x => x.RuleName ==  umlClass.Name);

                if (canonicalRule == null)
                {
                    return;
                }

                writer.WriteSafeString($"// Rule definition : {canonicalRule.RawRule}");
                WriteForRule(writer, umlClass, canonicalRule, rules);
            });
        }

        private static void WriteForRule(EncodedTextWriter writer, IClass umlClass, TextualNotationRule textualRule, List<TextualNotationRule> rules)
        {
            foreach (var textualRuleElement in textualRule.Elements)
            {
                switch (textualRuleElement)
                {
                    case TerminalElement terminalElement:
                        writer.WriteSafeString($"stringBuilder.Append(\"{terminalElement.Value} \");");
                        break;
                    case NonTerminalElement nonTerminalElement:
                        var referencedRule = rules.SingleOrDefault(x => x.RuleName ==  nonTerminalElement.Name);
                        writer.WriteSafeString($"// non Terminal : {nonTerminalElement.Name}; Found rule {referencedRule?.RawRule}");
                        break;
                    case GroupElement groupElement:
                        writer.WriteSafeString("// Group Element ");
                        break;
                    case AssignmentElement assignmentElement:
                        writer.WriteSafeString($"// Assignment Element : {assignmentElement.Property} {assignmentElement.Operator} {assignmentElement.Value}");
                        break;
                    case NonParsingAssignmentElement nonParsingAssignmentElement:
                        writer.WriteSafeString($"// Assignment Element : {nonParsingAssignmentElement.PropertyName} {nonParsingAssignmentElement.Operator} {nonParsingAssignmentElement.Value}");
                        break;
                    default:
                        throw new ArgumentException("Unknown element type");
                }
                
                writer.WriteSafeString(Environment.NewLine);
            }
        }
    }
}
