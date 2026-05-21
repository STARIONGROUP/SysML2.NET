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
    /// </summary>
    internal static class OperatorPrecedence
    {
        /// <summary>
        /// Levels are inferred from the <c>OwnedExpression</c> rule alternative ordering
        /// in <c>Resources/KerML-textual-bnf.kebnf</c> and tuned to reproduce the
        /// canonical parenthesization in <c>Resources/Quantities.sysml</c>. Lower = binds
        /// less tightly.
        /// </summary>
        private const int LevelConditional = 1;
        private const int LevelConditionalBinary = 2;
        private const int LevelBitwise = 3;
        private const int LevelEquality = 4;
        private const int LevelRelational = 5;
        private const int LevelClassification = 6;
        private const int LevelAdditive = 7;
        private const int LevelMultiplicative = 8;
        private const int LevelPower = 9;
        private const int LevelUnary = 10;
        private const int LevelExtent = 11;
        private const int LevelPrimary = int.MaxValue;

        /// <summary>
        /// Coarse-grained operator families used by the cross-family wrap rule.
        /// </summary>
        private enum PrecedenceBucket
        {
            Conditional,
            Binary,
            Classification,
            Unary,
            Extent,
            Primary,
        }

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

            if (@operator is "or" or "and" or "implies" or "??")
            {
                return LevelConditionalBinary;
            }

            if (@operator is "|" or "&" or "xor")
            {
                return LevelBitwise;
            }

            if (@operator is "==" or "!=" or "===" or "!==")
            {
                return LevelEquality;
            }

            if (@operator is "<" or ">" or "<=" or ">=" or "..")
            {
                return LevelRelational;
            }

            if (@operator is "as" or "istype" or "hastype" or "@" or "@@" or "meta")
            {
                return LevelClassification;
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
        /// <c>(…)</c> when it appears as an operand of <paramref name="outer"/>. The rule:
        /// cross-family nesting always wraps (binary inside conditional, etc.); same-family
        /// nesting wraps only when the inner has same-or-lower precedence than the outer.
        /// </summary>
        /// <param name="outer">The enclosing operator expression.</param>
        /// <param name="operand">The candidate operand expression.</param>
        /// <returns><c>true</c> when parens are required for unambiguous rendering.</returns>
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

            var outerPrecedence = GetExpressionPrecedence(outer);

            var innerBucket = GetBucket(innerPrecedence);
            var outerBucket = GetBucket(outerPrecedence);

            if (innerBucket != outerBucket)
            {
                return true;
            }

            return innerPrecedence <= outerPrecedence;
        }

        /// <summary>
        /// Counts the IOperatorExpression's argument-members to distinguish binary additive
        /// (<c>a + b</c>) from unary sign (<c>+ a</c>). Two argument-members indicate the
        /// binary form.
        /// </summary>
        /// <param name="op">The operator expression.</param>
        /// <returns><c>true</c> when at least two argument-members are present.</returns>
        private static bool HasTwoArguments(IOperatorExpression op)
        {
            var count = 0;

            foreach (var relationship in op.OwnedRelationship)
            {
                if (relationship is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership)
                {
                    count++;

                    if (count >= 2)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Maps a precedence level to its coarse-grained operator family bucket.
        /// </summary>
        /// <param name="precedence">The precedence level.</param>
        /// <returns>The bucket.</returns>
        private static PrecedenceBucket GetBucket(int precedence)
        {
            return precedence switch
            {
                LevelConditional or LevelConditionalBinary => PrecedenceBucket.Conditional,
                LevelBitwise or LevelEquality or LevelRelational or LevelAdditive or LevelMultiplicative or LevelPower => PrecedenceBucket.Binary,
                LevelClassification => PrecedenceBucket.Classification,
                LevelUnary => PrecedenceBucket.Unary,
                LevelExtent => PrecedenceBucket.Extent,
                _ => PrecedenceBucket.Primary,
            };
        }
    }
}
