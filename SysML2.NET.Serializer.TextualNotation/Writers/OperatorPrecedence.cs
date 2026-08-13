// -------------------------------------------------------------------------------------------------
// <copyright file="OperatorPrecedence.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.TextualNotation.Writers
{
    using System.Linq;

    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.Functions;

    /// <summary>
    /// Precedence-aware parenthesization helpers for operator-expression operands.
    /// <para>
    /// The KEBNF grammar uses <c>SequenceExpression : Expression = '(' SequenceExpressionList ')'</c>
    /// as its parenthesization rule, but the SysML metamodel has no <c>SequenceExpression</c>
    /// class — parens are a grammar-only artifact that collapses to the inner
    /// <see cref="IOperatorExpression"/> at parse time. When writing back, the textual notation
    /// writers must therefore decide AT EMISSION TIME whether to wrap a nested operator
    /// expression in <c>(…)</c>. This class provides the precedence table that drives the
    /// decision: each operator-expression family / operator gets a precedence level, and
    /// <see cref="NeedsParenthesesAsOperand"/> returns <c>true</c> when the inner expression
    /// would be ambiguous without parens.
    /// </para>
    /// <para>
    /// Parentheses are emitted in two tiers. REQUIRED parens keep the text re-parsing to the same
    /// model, and are decided by precedence alone. CLARIFYING parens are emitted where precedence
    /// already settles the grouping but a reader benefits from seeing it — see
    /// <see cref="NeedsClarifyingParentheses"/>. Both tiers are gated by
    /// <c>TextualNotationWriterContext.EmitOperatorParentheses</c>.
    /// </para>
    /// </summary>
    internal static class OperatorPrecedence
    {
        /// <summary>
        /// Levels transcribe the normative operator precedence table — OMG KerML v1.0,
        /// Clause 8.2.5.8.1, Table 6 "Operator Precedence (highest to lowest)" — which
        /// Note 2 of the same clause makes the sole basis for implicit grouping of nested
        /// <c>OperatorExpression</c>s. Higher value = binds more tightly. The table is a
        /// total order, so the alternation order of the <c>OwnedExpression</c> rule in
        /// <c>Resources/KerML-textual-bnf.kebnf</c> carries no precedence information and
        /// must not be used to infer one.
        /// </summary>
        private const int LevelConditional = 1;
        private const int LevelNullCoalescing = 2;
        private const int LevelImplies = 3;
        private const int LevelInclusiveOr = 4;
        private const int LevelExclusiveOr = 5;
        private const int LevelAnd = 6;
        private const int LevelEquality = 7;
        private const int LevelClassification = 8;
        private const int LevelRelational = 9;
        private const int LevelRange = 10;
        private const int LevelAdditive = 11;
        private const int LevelMultiplicative = 12;
        private const int LevelPower = 13;
        private const int LevelUnary = 14;
        private const int LevelExtent = 15;
        private const int LevelPrimary = int.MaxValue;

        /// <summary>
        /// Returns the precedence level of <paramref name="expression"/>. Lower = binds less
        /// tightly. Primary forms (literals, feature references, invocations, etc.) return
        /// <see cref="int.MaxValue"/> — they are self-contained and never need wrapping.
        /// </summary>
        /// <param name="expression">The expression to classify, or <c>null</c>.</param>
        /// <returns>The precedence level.</returns>
        internal static int GetExpressionPrecedence(IExpression expression)
        {
            // Order matters in the metamodel inheritance chain:
            //   IExpression ← IInstantiationExpression ← IInvocationExpression ← IOperatorExpression ← {IFeatureChainExpression, IIndexExpression, ICollectExpression, ISelectExpression}.
            // Several "primary forms" (member-access `.`, index `[..]`, collect `.`,
            // select `.?`) are technically IOperatorExpression-typed in the metamodel
            // but render as self-contained tokens — they must be classified as primary
            // BEFORE the generic IOperatorExpression check.
            if (expression is IFeatureChainExpression
                || expression is IIndexExpression
                || expression is ICollectExpression
                || expression is ISelectExpression)
            {
                return LevelPrimary;
            }

            if (expression is IOperatorExpression op)
            {
                // BracketExpression (`[`) and SequenceOperatorExpression (`,`) are
                // OperatorExpression-typed metamodel-wise but render as primary forms
                // (`a[…]`, `(a, b)`). They are distinguished by their `operator`
                // discriminator; no separate interface exists.
                if (op.Operator is "[" or ",")
                {
                    return LevelPrimary;
                }

                return GetOperatorExpressionPrecedence(op);
            }

            // Non-operator-expression primary forms (literals, references, invocations,
            // etc.) are always self-contained tokens.
            if (expression is IInvocationExpression
                || expression is IFeatureReferenceExpression
                || expression is IMetadataAccessExpression
                || expression is INullExpression
                || expression is ILiteralExpression
                || expression is IConstructorExpression)
            {
                return LevelPrimary;
            }

            return LevelPrimary;
        }

        /// <summary>
        /// Returns the precedence level of an <see cref="IOperatorExpression"/> based on
        /// its <c>Operator</c> discriminator.
        /// </summary>
        /// <param name="op">The operator expression.</param>
        /// <returns>The precedence level.</returns>
        private static int GetOperatorExpressionPrecedence(IOperatorExpression op)
        {
            var @operator = op.Operator;

            if (@operator == "if")
            {
                return LevelConditional;
            }

            if (@operator == "??")
            {
                return LevelNullCoalescing;
            }

            if (@operator == "implies")
            {
                return LevelImplies;
            }

            // Table 6 keeps each bitwise operator on the level of its logical spelling:
            // `|` sits with `or`, `&` sits with `and`, and `xor` sits alone between them.
            if (@operator is "|" or "or")
            {
                return LevelInclusiveOr;
            }

            if (@operator == "xor")
            {
                return LevelExclusiveOr;
            }

            if (@operator is "&" or "and")
            {
                return LevelAnd;
            }

            if (@operator is "==" or "!=" or "===" or "!==")
            {
                return LevelEquality;
            }

            if (@operator is "as" or "istype" or "hastype" or "@" or "@@" or "meta")
            {
                return LevelClassification;
            }

            if (@operator is "<" or ">" or "<=" or ">=")
            {
                return LevelRelational;
            }

            if (@operator == "..")
            {
                return LevelRange;
            }

            if (@operator is "*" or "/" or "%")
            {
                return LevelMultiplicative;
            }

            if (@operator is "^" or "**")
            {
                return LevelPower;
            }

            if (@operator is "+" or "-")
            {
                // + and - are both binary (additive) and unary (sign). Distinguish by the
                // count of argument-member relationships on the operator expression: two
                // means additive (LHS + RHS); fewer means unary (sign).
                return HasTwoArguments(op) ? LevelAdditive : LevelUnary;
            }

            if (@operator is "~" or "not")
            {
                return LevelUnary;
            }

            if (@operator == "all")
            {
                return LevelExtent;
            }

            // Unknown operator: treat as unary-level (conservative — wraps when nested).
            return LevelUnary;
        }

        /// <summary>
        /// Determines whether <paramref name="operand"/> needs to be wrapped in
        /// <c>(…)</c> when it appears as an operand of <paramref name="outer"/>.
        /// <para>
        /// The rule follows KerML Clause 8.2.5.8.1 Note 2 directly: grouping is implied by
        /// operator precedence alone, so an operand needs no parentheses as soon as it binds
        /// more tightly than the operator it is an operand of. Parentheses are emitted only
        /// where re-parsing the unparenthesized text would regroup the expression — i.e. when
        /// the operand binds less tightly, or equally tightly (where associativity, not
        /// precedence, decides the grouping, and the operand's slot is not known here).
        /// </para>
        /// </summary>
        /// <param name="outer">The enclosing operator expression.</param>
        /// <param name="operand">The candidate operand expression.</param>
        /// <returns><c>true</c> when parens are required, or emitted for clarity.</returns>
        internal static bool NeedsParenthesesAsOperand(IExpression outer, IExpression operand)
        {
            if (operand == null || outer == null)
            {
                return false;
            }

            var innerPrecedence = GetExpressionPrecedence(operand);

            if (innerPrecedence == LevelPrimary)
            {
                return false;
            }

            // Unary prefix operators (`~`, `not`, unary `+`/`-`) and the extent operator (`all`)
            // bind tighter than every binary operator and have no left operand that the
            // enclosing operator could absorb — they are unambiguous as operands and never need
            // parenthesization. E.g. `not a and b` re-parses unambiguously as `(not a) and b`.
            if (innerPrecedence >= LevelUnary)
            {
                return false;
            }

            if (innerPrecedence <= GetExpressionPrecedence(outer))
            {
                return true;
            }

            return NeedsClarifyingParentheses(outer, operand);
        }

        /// <summary>
        /// Determines whether <paramref name="operand"/> should be wrapped in <c>(…)</c> purely for
        /// READABILITY, in the cases where <see cref="NeedsParenthesesAsOperand"/> has already
        /// established that precedence alone makes the grouping unambiguous.
        /// <para>
        /// Two mixes are clarified, following the grouping convention mainstream linters apply to the
        /// same problem (e.g. ESLint's <c>no-mixed-operators</c>): two DIFFERENT binary logical
        /// connectives — <c>a and b xor c and d</c> renders as <c>(a and b) xor (c and d)</c> — and two
        /// arithmetic operators from different precedence tiers — <c>a + b * c</c> renders as
        /// <c>a + (b * c)</c>.
        /// </para>
        /// <para>
        /// Comparisons nested in logical connectives (<c>a == b and c == d</c>) are deliberately left
        /// bare: that grouping is read the same way by everyone, so parenthesizing it is noise. The
        /// OMG pilot renders the same corpus the same way.
        /// </para>
        /// </summary>
        /// <param name="outer">The enclosing operator expression.</param>
        /// <param name="operand">The candidate operand expression, known to bind more tightly.</param>
        /// <returns><c>true</c> when parens clarify an otherwise unambiguous grouping.</returns>
        private static bool NeedsClarifyingParentheses(IExpression outer, IExpression operand)
        {
            if (outer is not IOperatorExpression outerOperator || operand is not IOperatorExpression operandOperator)
            {
                return false;
            }

            if (IsLogicalConnective(outerOperator.Operator) && IsLogicalConnective(operandOperator.Operator))
            {
                return outerOperator.Operator != operandOperator.Operator;
            }

            // The operand is already known to bind more tightly than the outer operator, so two
            // arithmetic operators here necessarily sit on different tiers of Table 6.
            return IsArithmeticOperator(outerOperator.Operator) && IsArithmeticOperator(operandOperator.Operator);
        }

        /// <summary>
        /// Determines whether <paramref name="operator"/> is one of the binary logical connectives —
        /// the short-circuiting <c>ConditionalBinaryOperator</c>s plus their non-short-circuiting
        /// spellings and <c>xor</c>.
        /// </summary>
        /// <param name="operator">The operator discriminator.</param>
        /// <returns><c>true</c> when the operator is a binary logical connective.</returns>
        private static bool IsLogicalConnective(string @operator)
        {
            return @operator is "&" or "and" or "|" or "or" or "xor" or "implies" or "??";
        }

        /// <summary>
        /// Determines whether <paramref name="operator"/> is one of the arithmetic operators.
        /// <para>The unary <c>+</c> / <c>-</c> forms never reach this test — they classify as
        /// <see cref="LevelUnary"/> and are returned early by
        /// <see cref="NeedsParenthesesAsOperand"/>.</para>
        /// </summary>
        /// <param name="operator">The operator discriminator.</param>
        /// <returns><c>true</c> when the operator is arithmetic.</returns>
        private static bool IsArithmeticOperator(string @operator)
        {
            return @operator is "+" or "-" or "*" or "/" or "%" or "^" or "**";
        }

        /// <summary>
        /// Counts the IOperatorExpression's argument-members to distinguish binary additive
        /// (<c>a + b</c>) from unary sign (<c>+ a</c>). Two argument-members indicate the
        /// binary form.
        /// <para>
        /// The result-member must be excluded from the count: every <c>OperatorExpression</c>
        /// — unary ones included — owns an <c>EmptyResultMember : ReturnParameterMembership</c>
        /// per the <c>UnaryOperatorExpression</c> / <c>BinaryOperatorExpression</c> rules in
        /// <c>Resources/KerML-textual-bnf.kebnf</c>, and <c>IReturnParameterMembership</c>
        /// derives from <c>IParameterMembership</c>. Counting it would make every unary
        /// <c>+</c>/<c>-</c> look binary and misclassify it as additive rather than unary.
        /// </para>
        /// </summary>
        /// <param name="op">The operator expression.</param>
        /// <returns><c>true</c> when at least two argument-members are present.</returns>
        private static bool HasTwoArguments(IOperatorExpression op)
        {
            return op.OwnedRelationship
                       .OfType<SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership>()
                       .Count(membership => membership is not IReturnParameterMembership) >= 2;
        }
    }
}
