// -------------------------------------------------------------------------------------------------
// <copyright file="ExpressionExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Kernel.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="ExpressionExtensions"/> class provides extensions methods for
    /// the <see cref="IExpression"/> interface
    /// </summary>
    internal static class ExpressionExtensions
    {
        /// <summary>
        /// Computes the derived <c>function</c> property: the <see cref="IFunction"/> that is the
        /// single type of this <see cref="IExpression"/>.
        /// </summary>
        /// <param name="expressionSubject">
        /// The subject <see cref="IExpression"/>
        /// </param>
        /// <returns>
        /// The matching <see cref="IFunction"/>, or <c>null</c> when no such type exists.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="expressionSubject"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="MultiplicityViolationException">
        /// Thrown when more than one <see cref="IType"/> on the subject is an <see cref="IFunction"/>
        /// (upper-bound violation against the derived <c>[0..1]</c> property).
        /// </exception>
        internal static IFunction ComputeFunction(this IExpression expressionSubject)
        {
            return expressionSubject == null
                ? throw new ArgumentNullException(nameof(expressionSubject))
                : expressionSubject.type.SingleOrDefaultStrict<IFunction>(nameof(expressionSubject));
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// isModelLevelEvaluable = modelLevelEvaluable(Set(Element){})
        /// </code>
        /// </remarks>
        /// <param name="expressionSubject">
        /// The subject <see cref="IExpression"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static bool ComputeIsModelLevelEvaluable(this IExpression expressionSubject)
        {
            return expressionSubject?.ModelLevelEvaluable([]) ?? throw new ArgumentNullException(nameof(expressionSubject));
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// result =
        ///                             let resultParams : Sequence(Feature) =
        ///                             featureMemberships-&gt;
        ///                             selectByKind(ReturnParameterMembership).
        ///                             ownedMemberParameter in
        ///                             if resultParams-&gt;notEmpty() then resultParams-&gt;first()
        ///                             else null
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="expressionSubject">
        /// The subject <see cref="IExpression"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IFeature ComputeResult(this IExpression expressionSubject)
        {
            if (expressionSubject == null)
            {
                throw new ArgumentNullException(nameof(expressionSubject));
            }

            var resultParams = expressionSubject.featureMembership
                .OfType<IReturnParameterMembership>()
                .Select(returnParameterMembership => returnParameterMembership.ownedMemberParameter)
                .ToList();

            return resultParams.Count == 0 ? null : resultParams[0];
        }

        /// <summary>
        /// Return whether this Expression is model-level evaluable. The visited parameter is used to track
        /// possible circular Feature references made from FeatureReferenceExpressions (see the redefinition of
        /// this operation for FeatureReferenceExpression). Such circular references are not allowed in
        /// model-level evaluable expressions.                            An Expression that is not otherwise
        /// specialized is model-level evaluable if it has no (non-implied) ownedSpecializations and all its
        /// ownedFeatures are either in parameters, the result parameter or a result Expression owned via a
        /// ResultExpressionMembership. The parameters  must not have any ownedFeatures or a FeatureValue, and
        /// the result Expression must be model-level evaluable.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedSpecialization-&gt;forAll(isImplied) and
        ///                                 ownedFeature-&gt;forAll(f |
        ///                                 (directionOf(f) = FeatureDirectionKind::_'in' or f = result) and
        ///                                 f.ownedFeature-&gt;isEmpty() and f.valuation = null or
        ///                                 f.owningFeatureMembership.oclIsKindOf(ResultExpressionMembership) and
        ///                                 f.oclAsType(Expression).modelLevelEvaluable(visited)
        /// </code>
        /// </remarks>
        /// <param name="expressionSubject">
        /// The subject <see cref="IExpression"/>
        /// </param>
        /// <param name="visited">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeModelLevelEvaluableOperation(this IExpression expressionSubject, List<IFeature> visited)
        {
            if (expressionSubject == null)
            {
                throw new ArgumentNullException(nameof(expressionSubject));
            }

            visited ??= [];

            if (!expressionSubject.ownedSpecialization.All(specialization => specialization.IsImplied))
            {
                return false;
            }

            var resultFeature = expressionSubject.result;

            foreach (var ownedFeature in expressionSubject.ownedFeature)
            {
                //  f.valuation == null clause omitted — IFeature has no Valuation property in the current POCO (metamodel gap). Follow-up issue required.
                var branchA =
                    (expressionSubject.DirectionOf(ownedFeature) == FeatureDirectionKind.In
                     || ReferenceEquals(ownedFeature, resultFeature))
                    && ownedFeature.ownedFeature.Count == 0;

                var branchB =
                    ownedFeature.owningFeatureMembership is IResultExpressionMembership
                    && (ownedFeature as IExpression)?.ModelLevelEvaluable(visited) == true;

                if (!(branchA || branchB))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// If this Expression isModelLevelEvaluable, then evaluate it using the target as the context Element
        /// for resolving Feature names and testing classification. The result is a collection of Elements,
        /// which, for a fully evaluable Expression, will be a LiteralExpression or a Feature that is not an
        /// Expression.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// isModelLevelEvaluable
        /// </code>
        /// OCL2.0:
        /// <code>
        /// let resultExprs : Sequence(Expression) =
        ///                                 ownedFeatureMembership-&gt;
        ///                                 selectByKind(ResultExpressionMembership).
        ///                                 ownedResultExpression in
        ///                                 if resultExpr-&gt;isEmpty() then Sequence{}
        ///                                 else resultExprs-&gt;first().evaluate(target)
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="expressionSubject">
        /// The subject <see cref="IExpression"/>
        /// </param>
        /// <param name="target">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IElement" />
        /// </returns>
        internal static List<IElement> ComputeEvaluateOperation(this IExpression expressionSubject, IElement target)
        {
            if (expressionSubject == null)
            {
                throw new ArgumentNullException(nameof(expressionSubject));
            }

            var resultExprs = expressionSubject.ownedFeatureMembership
                .OfType<IResultExpressionMembership>()
                .Select(resultExpressionMembership => resultExpressionMembership.ownedResultExpression)
                .ToList();

            return resultExprs.Count == 0
                ? []
                : resultExprs[0].Evaluate(target);
        }

        /// <summary>
        /// Model-level evaluate this Expression with the given target. If the result is a LiteralBoolean,
        /// return its value. Otherwise return false.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// let results: Sequence(Element) = evaluate(target) in
        ///                                 result-&gt;size() = 1 and
        ///                                 results-&gt;first().oclIsKindOf(LiteralBoolean) and
        ///                                 results-&gt;first().oclAsType(LiteralBoolean).value
        /// </code>
        /// </remarks>
        /// <param name="expressionSubject">
        /// The subject <see cref="IExpression"/>
        /// </param>
        /// <param name="target">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeCheckConditionOperation(this IExpression expressionSubject, IElement target)
        {
            if (expressionSubject == null)
            {
                throw new ArgumentNullException(nameof(expressionSubject));
            }

            var results = expressionSubject.Evaluate(target);

            return results.Count == 1 && results[0] is ILiteralBoolean { Value: true };
        }
    }
}
