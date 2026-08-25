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

    using uml4net.CommonStructure;
    using uml4net.Extensions;
    using uml4net.StructuredClassifiers;

    /// <summary>
    /// Provides textual notation rules related helper for <see cref="IHandlebars" />
    /// </summary>
    public static class RulesHelper
    {
        /// <summary>
        /// The name of the shared builder class that hosts all no-target rules that do not
        /// have a matching UML class (e.g. <c>FeaturePrefix</c>).
        /// </summary>
        public const string SharedBuilderClassName = "SharedTextualNotationBuilder";

        /// <summary>
        /// Register this helper
        /// </summary>
        /// <param name="handlebars">The <see cref="IHandlebars" /> context with which the helper needs to be registered</param>
        public static void RegisterRulesHelper(this IHandlebars handlebars)
        {
            var processor = new RuleProcessor();

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

                    var isOperatorExpressionRule = IsOperatorExpressionRule(umlClass);
                    var isOwnedExpressionRule = string.Equals(textualRule.RuleName, "OwnedExpression", StringComparison.Ordinal);
                    var isInlineBraceBodyRule = IsInlineBraceBodyRule(textualRule);

                    if (isOwnedExpressionRule)
                    {
                        writer.WriteSafeString("var operatorParensNeeded = writerContext.EmitOperatorParentheses && writerContext.OperatorContextStack.Count > 0 && SysML2.NET.Serializer.TextualNotation.Writers.OperatorPrecedence.NeedsParenthesesAsOperand(writerContext.OperatorContextStack.Peek(), poco);" + Environment.NewLine);
                        writer.WriteSafeString("if (operatorParensNeeded) { stringBuilder.Append('('); }" + Environment.NewLine);
                    }

                    if (isOperatorExpressionRule)
                    {
                        writer.WriteSafeString("writerContext.OperatorContextStack.Push(poco);" + Environment.NewLine);
                        writer.WriteSafeString("try" + Environment.NewLine + "{" + Environment.NewLine);
                    }

                    // Inline-brace-body rules — rules whose body alternative has the exact
                    // shape `'{' SingleNonTerminal '}'` with no quantifier and no `+=`
                    // accumulator — render their <c>{ … }</c> wrapper on a single line per
                    // the SST tutorial convention (e.g. constraint and expression bodies).
                    // The three rules that match in the KEBNF are
                    // <c>FunctionBody</c>, <c>ExpressionBody</c>, and <c>CalculationBody</c>;
                    // every other brace-bounded rule uses a <c>*</c>-quantified list and
                    // renders multi-line. The wrapping suppresses AppendLine newlines inside
                    // the rule body and re-terminates the logical line on exit so the next
                    // owning statement starts on its own line.
                    if (isInlineBraceBodyRule)
                    {
                        writer.WriteSafeString("stringBuilder.EnterInlineBlock();" + Environment.NewLine);
                        writer.WriteSafeString("try" + Environment.NewLine + "{" + Environment.NewLine);
                    }

                    if (RequiresHandCodedBody(textualRule.RuleName))
                    {
                        writer.WriteSafeString($"Build{textualRule.RuleName}HandCoded(poco, writerContext, stringBuilder);{Environment.NewLine}");
                    }
                    else
                    {
                        processor.ProcessAlternatives(writer, umlClass, textualRule.Alternatives, ruleGenerationContext);
                    }

                    if (isInlineBraceBodyRule)
                    {
                        writer.WriteSafeString("}" + Environment.NewLine + "finally" + Environment.NewLine + "{" + Environment.NewLine + "stringBuilder.ExitInlineBlock();" + Environment.NewLine + "stringBuilder.AppendLine();" + Environment.NewLine + "}" + Environment.NewLine);
                    }

                    if (isOperatorExpressionRule)
                    {
                        writer.WriteSafeString("}" + Environment.NewLine + "finally" + Environment.NewLine + "{" + Environment.NewLine + "writerContext.OperatorContextStack.Pop();" + Environment.NewLine + "}" + Environment.NewLine);
                    }

                    if (isOwnedExpressionRule)
                    {
                        // Emitted as the STRING ") " rather than the char ')': only the string
                        // overload of IndentedStringBuilder.Append runs the tight-left token
                        // normalisation that strips the space the operand left behind, and the
                        // trailing space restores the separator the enclosing binary-operator
                        // rule expects before it appends its own operator. The char overload
                        // bypasses both and renders `a and b )xor (c and d)`.
                        writer.WriteSafeString("if (operatorParensNeeded) { stringBuilder.Append(\") \"); }" + Environment.NewLine);
                    }
                }
            });
        }

        /// <summary>
        /// Determines whether the rule's whole body must be supplied by a hand-coded
        /// <c>Build{Rule}HandCoded</c> companion because its alternatives cannot be discriminated
        /// from the parsed grammar body.
        /// </summary>
        /// <remarks>
        /// Currently <c>FunctionOperationExpression</c>, whose trailing choice is
        /// <c>( ownedRelationship += BodyArgumentMember | ownedRelationship += FunctionReferenceArgumentMember
        /// | ArgumentList )</c>. The first two both target <c>ParameterMembership</c>, so no cursor-type test
        /// separates them — they differ only in the FeatureValue their argument carries (a
        /// <c>BodyExpression</c> vs a <c>FunctionReferenceExpression</c>) — and the third is a bare
        /// non-terminal with no assignment at all. The generator therefore emitted three branches sharing the
        /// guard <c>Current != null</c>, making branches 2 and 3 provably dead: the mandatory <c>()</c> of
        /// <c>ArgumentList</c> was never emitted, and the rule's trailing <c>EmptyResultMember</c> (a
        /// <c>ReturnParameterMembership</c>, hence an <c>IParameterMembership</c>) was captured by branch 1
        /// and then emitted a second time.
        /// </remarks>
        /// <param name="ruleName">The KEBNF rule name.</param>
        /// <returns><c>true</c> when the codegen should delegate the entire body.</returns>
        private static bool RequiresHandCodedBody(string ruleName)
        {
            return string.Equals(ruleName, "FunctionOperationExpression", StringComparison.Ordinal);
        }

        /// <summary>
        /// Determines whether <paramref name="rule"/> targets an <c>IOperatorExpression</c>
        /// (or any of its subclasses) as the rule's effective metaclass. Used by
        /// <c>WriteRule</c> to wrap the generated builder body with a precedence-stack
        /// push/pop so operand-rendering can decide on parens.
        /// </summary>
        /// <param name="umlClass">The rule's target <see cref="IClass"/>.</param>
        /// <returns><c>true</c> when the target is <c>OperatorExpression</c> or a subclass.</returns>
        private static bool IsOperatorExpressionRule(IClass umlClass)
        {
            if (umlClass == null)
            {
                return false;
            }

            return string.Equals(umlClass.Name, "OperatorExpression", StringComparison.Ordinal) || umlClass.QueryAllGeneralClassifiers().Any(general => string.Equals(general.Name, "OperatorExpression", StringComparison.Ordinal));
        }

        /// <summary>
        /// Determines whether <paramref name="rule"/> has any alternative of the exact shape
        /// <c>'{' SingleNonTerminal '}'</c> with no quantifier and no <c>+=</c> accumulator
        /// on the inner non-terminal. The KEBNF grammar uses this shape exclusively for
        /// expression-body wrappers — <c>FunctionBody</c>, <c>ExpressionBody</c> and
        /// <c>CalculationBody</c> — whose canonical SST rendering is a single inline line
        /// <c>{ expr }</c>. Every other brace-bounded rule uses a <c>*</c>-quantified list
        /// (e.g. <c>'{' PackageBodyElement* '}'</c>) and renders multi-line.
        /// </summary>
        /// <param name="rule">The textual notation rule being generated.</param>
        /// <returns>
        /// <c>true</c> when the rule contains the inline brace-body shape and therefore
        /// needs its braced alternative wrapped with
        /// <c>stringBuilder.EnterInlineBlock()</c> / <c>stringBuilder.ExitInlineBlock()</c>.
        /// </returns>
        private static bool IsInlineBraceBodyRule(TextualNotationRule rule)
        {
            if (rule == null)
            {
                return false;
            }

            foreach (var alternative in rule.Alternatives.Where(alternative => alternative.Elements.Count == 3))
            {
                if (alternative.Elements[0] is not TerminalElement { Value: "{" })
                {
                    continue;
                }

                if (alternative.Elements[2] is not TerminalElement { Value: "}" })
                {
                    continue;
                }

                if (alternative.Elements[1] is not NonTerminalElement nonTerminal)
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(nonTerminal.Suffix))
                {
                    continue;
                }

                if (nonTerminal.Container is AssignmentElement)
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Resolves the effective target class for a no-target rule by analyzing its assignments.
        /// </summary>
        /// <param name="rule">The <see cref="TextualNotationRule" /> to analyze</param>
        /// <param name="allRules">All available grammar rules</param>
        /// <param name="cacheSource">An <see cref="IClass" /> providing access to the UML model cache</param>
        /// <returns>The resolved <see cref="IClass" />, or null if not resolvable</returns>
        public static IClass ResolveNoTargetRuleEffectiveTarget(TextualNotationRule rule, IReadOnlyList<TextualNotationRule> allRules, IClass cacheSource)
        {
            return NoTargetRuleResolver.ResolveEffectiveTarget(rule, allRules, cacheSource);
        }

        /// <summary>
        /// Determines whether a no-target rule should be lifted into the shared builder class.
        /// </summary>
        /// <param name="rule">The rule to test</param>
        /// <param name="cacheSource">Any <see cref="IClass" /> from the loaded model used to access <c>Cache</c></param>
        /// <returns><c>true</c> when the rule should be generated into the shared builder</returns>
        public static bool IsSharedNoTargetRule(TextualNotationRule rule, IClass cacheSource)
        {
            return NoTargetRuleResolver.IsSharedRule(rule, cacheSource);
        }
    }
}
