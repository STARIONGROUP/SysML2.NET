// -------------------------------------------------------------------------------------------------
// <copyright file="MultiplicityRangeExtensions.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

namespace SysML2.NET.Core.POCO.Kernel.Multiplicities
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.Functions;

    /// <summary>
    /// The <see cref="MultiplicityRangeExtensions"/> class provides extensions methods for
    /// the <see cref="IMultiplicityRange"/> interface
    /// </summary>
    internal static class MultiplicityRangeExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// bound =
        ///                             if upperBound = null then Sequence{}
        ///                             else if lowerBound = null then Sequence{upperBound}
        ///                             else Sequence{lowerBound, upperBound}
        ///                             endif endif
        /// </code>
        /// </remarks>
        /// <param name="multiplicityRangeSubject">
        /// The subject <see cref="IMultiplicityRange"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IExpression> ComputeBound(this IMultiplicityRange multiplicityRangeSubject)
        {
            if (multiplicityRangeSubject is null)
            {
                throw new ArgumentNullException(nameof(multiplicityRangeSubject));
            }

            var upper = multiplicityRangeSubject.upperBound;

            if (upper is null)
            {
                return [];
            }

            var lower = multiplicityRangeSubject.lowerBound;

            return lower is null ? [upper] : [lower, upper];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// lowerBound =
        ///                             let ownedExpressions : Sequence(Expression) =
        ///                             ownedMember-&gt;selectByKind(Expression) in
        ///                             if ownedExpressions-&gt;size() &lt; 2 then null
        ///                             else ownedExpressions-&gt;first()
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="multiplicityRangeSubject">
        /// The subject <see cref="IMultiplicityRange"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IExpression ComputeLowerBound(this IMultiplicityRange multiplicityRangeSubject)
        {
            if (multiplicityRangeSubject is null)
            {
                throw new ArgumentNullException(nameof(multiplicityRangeSubject));
            }

            var ownedExpressions = multiplicityRangeSubject.ownedMember
                .OfType<IExpression>()
                .ToList();

            return ownedExpressions.Count < 2 ? null : ownedExpressions[0];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// upperBound =
        ///                             let ownedExpressions : Sequence(Expression) =
        ///                             ownedMember-&gt;selectByKind(Expression) in
        ///                             if ownedExpressions-&gt;isEmpty() then null
        ///                             else if ownedExpressions-&gt;size() = 1 then ownedExpressions-&gt;at(1)
        ///                             else ownedExpressions-&gt;at(2)
        ///                             endif endif
        /// </code>
        /// </remarks>
        /// <param name="multiplicityRangeSubject">
        /// The subject <see cref="IMultiplicityRange"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IExpression ComputeUpperBound(this IMultiplicityRange multiplicityRangeSubject)
        {
            if (multiplicityRangeSubject is null)
            {
                throw new ArgumentNullException(nameof(multiplicityRangeSubject));
            }

            var ownedExpressions = multiplicityRangeSubject.ownedMember
                .OfType<IExpression>()
                .ToList();

            return ownedExpressions.Count switch
            {
                0 => null,
                1 => ownedExpressions[0],
                _ => ownedExpressions[1],
            };
        }

        /// <summary>
        /// Check whether this MultiplicityRange represents the range bounded by the given values lower and
        /// upper, presuming the lowerBound and upperBound Expressions are model-level evaluable.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// valueOf(upperBound) = upper and
        ///                                 let lowerValue: UnlimitedNatural = valueOf(lowerBound) in
        ///                                 (lowerValue = lower or
        ///                                 lowerValue = null and
        ///                                 (lower = upper or
        ///                                 lower = 0 and upper = *))
        /// </code>
        /// </remarks>
        /// <param name="multiplicityRangeSubject">
        /// The subject <see cref="IMultiplicityRange"/>
        /// </param>
        /// <param name="lower">
        /// The candidate lower bound as a non-negative integer.
        /// </param>
        /// <param name="upper">
        /// The candidate upper bound encoded as an UnlimitedNatural string (<c>"*"</c> for unbounded, or an invariant-culture decimal).
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeHasBoundsOperation(this IMultiplicityRange multiplicityRangeSubject, int lower, string upper)
        {
            if (multiplicityRangeSubject is null)
            {
                throw new ArgumentNullException(nameof(multiplicityRangeSubject));
            }

            var valueOfUpper = multiplicityRangeSubject.ValueOf(multiplicityRangeSubject.upperBound);

            if (!string.Equals(valueOfUpper, upper, StringComparison.Ordinal))
            {
                return false;
            }

            var lowerValue = multiplicityRangeSubject.ValueOf(multiplicityRangeSubject.lowerBound);
            var lowerAsString = lower.ToString(CultureInfo.InvariantCulture);

            if (string.Equals(lowerValue, lowerAsString, StringComparison.Ordinal))
            {
                return true;
            }

            if (lowerValue is not null)
            {
                return false;
            }

            return string.Equals(lowerAsString, upper, StringComparison.Ordinal)
                || (lower == 0 && string.Equals(upper, "*", StringComparison.Ordinal));
        }

        /// <summary>
        /// Evaluate the given bound Expression (at model level) and return the result represented as a MOF
        /// UnlimitedNatural value.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if bound = null or not bound.isModelLevelEvaluable then
        ///                                 null
        ///                                 else
        ///                                 let boundEval: Sequence(Element) = bound.evaluate(owningType) in
        ///                                 if boundEval-&gt;size() &lt;&gt; 1 then null else
        ///                                 let valueEval: Element = boundEval-&gt;at(1) in
        ///                                 if valueEval.oclIsKindOf(LiteralInfinity) then *
        ///                                 else if valueEval.oclIsKindOf(LiteralInteger) then
        ///                                 let value : Integer =
        ///                                 valueEval.oclAsKindOf(LiteralInteger).value in
        ///                                 if value &gt;= 0 then value else null endif
        ///                                 else null
        ///                                 endif endif
        ///                                 endif
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="multiplicityRangeSubject">
        /// The subject <see cref="IMultiplicityRange"/>
        /// </param>
        /// <param name="bound">
        /// The bound expression to evaluate; may be null, in which case the result is null.
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        internal static string ComputeValueOfOperation(this IMultiplicityRange multiplicityRangeSubject, IExpression bound)
        {
            if (multiplicityRangeSubject is null)
            {
                throw new ArgumentNullException(nameof(multiplicityRangeSubject));
            }

            if (bound is null || !bound.isModelLevelEvaluable)
            {
                return null;
            }

            var boundEval = bound.Evaluate(multiplicityRangeSubject.owningType);

            if (boundEval.Count != 1)
            {
                return null;
            }

            return boundEval[0] switch
            {
                ILiteralInfinity => "*",
                ILiteralInteger { Value: var value } when value >= 0 => value.ToString(CultureInfo.InvariantCulture),
                _ => null,
            };
        }
    }
}
