// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureReferenceExpressionExtensions.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Core.POCO.Kernel.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Kernel.Metadata;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="FeatureReferenceExpressionExtensions" /> class provides extensions methods for
    /// the <see cref="IFeatureReferenceExpression" /> interface
    /// </summary>
    internal static class FeatureReferenceExpressionExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// referent =
        ///                             let nonParameterMemberships : Sequence(Membership) = ownedMembership-&gt;
        ///                             reject(oclIsKindOf(ParameterMembership)) in
        ///                             if nonParameterMemberships-&gt;isEmpty() or
        ///                             not nonParameterMemberships-&gt;first().memberElement.oclIsKindOf(Feature)
        ///                             then null
        ///                             else nonParameterMemberships-&gt;first().memberElement.oclAsType(Feature)
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="featureReferenceExpressionSubject">
        /// The subject <see cref="IFeatureReferenceExpression" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IFeature ComputeReferent(this IFeatureReferenceExpression featureReferenceExpressionSubject)
        {
            if (featureReferenceExpressionSubject == null)
            {
                throw new ArgumentNullException(nameof(featureReferenceExpressionSubject));
            }

            var nonParameterMembership = featureReferenceExpressionSubject.ownedMembership
                .FirstOrDefault(membership => membership is not IParameterMembership);

            return nonParameterMembership?.MemberElement as IFeature;
        }

        /// <summary>
        /// A FeatureReferenceExpression is model-level evaluable if it&#39;s referent
        /// <ul>
        ///  <li>conforms to the self-reference feature Anything::self;</li>
        ///  <li>is an Expression that is model-level evaluable;</li>
        ///  <li>has an owningType that is a Metaclass or MetadataFeature; or</li>
        ///  <li>
        ///  has no featuringTypes and, if it has a FeatureValue, the value Expression is model-level
        ///  evaluable.
        ///  </li>
        /// </ul>
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// referent.conformsTo('Anything::self') or
        ///                                 visited-&gt;excludes(referent) and
        ///                                 (referent.oclIsKindOf(Expression) and
        ///                                 referent.oclAsType(Expression).modelLevelEvaluable(visited-&gt;including(referent)) or
        ///                                 referent.owningType &lt;&gt; null and
        ///                                 (referent.owningType.isOclKindOf(MetaClass) or
        ///                                 referent.owningType.isOclKindOf(MetadataFeature)) or
        ///                                 referent.featuringType-&gt;isEmpty() and
        ///                                 (referent.valuation = null or
        ///                                 referent.valuation.modelLevelEvaluable(visited-&gt;including(referent))))
        /// </code>
        /// </remarks>
        /// <param name="featureReferenceExpressionSubject">
        /// The subject <see cref="IFeatureReferenceExpression" />
        /// </param>
        /// <param name="visited">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeRedefinedModelLevelEvaluableOperation(this IFeatureReferenceExpression featureReferenceExpressionSubject, List<IFeature> visited)
        {
            if (featureReferenceExpressionSubject == null)
            {
                throw new ArgumentNullException(nameof(featureReferenceExpressionSubject));
            }

            var referent = featureReferenceExpressionSubject.referent;

            if (referent == null || referent.SpecializesFromLibrary("Anything::self"))
            {
                return true;
            }

            if (visited.Contains(referent))
            {
                return false;
            }

            var next = new List<IFeature>(visited) { referent };

            if (referent is IExpression referentExpression && referentExpression.ModelLevelEvaluable(next))
            {
                return true;
            }

            if (referent.owningType is IMetaclass or IMetadataFeature)
            {
                return true;
            }

            if (referent.featuringType.Count != 0)
            {
                return false;
            }

            var valueExpression = referent.ownedMembership.OfType<IFeatureValue>().FirstOrDefault()?.value;

            return valueExpression == null || valueExpression.ModelLevelEvaluable(next);
        }

        /// <summary>
        /// First, determine a value Expression for the referent:
        /// <ul>
        ///  <li>
        ///  If the target Element is a Type that has a feature that is the referent or (directly
        ///  or indirectly) redefines it, then the value Expression of the FeatureValue for that feature (if
        ///  any).
        ///  </li>
        ///  <li>
        ///  Else, if the referent has no featuringTypes, the value
        ///  Expression of the FeatureValue for the referent (if any).
        ///  </li>
        /// </ul>
        /// Then:
        /// <ul>
        ///  <li>
        ///  If such a
        ///  value Expression exists, return the result of evaluating that Expression on the target.
        ///  </li>
        ///  <li>Else, if the referent is not an Expression, return the referent.</li>
        ///  <li>Else return the empty sequence.</li>
        /// </ul>
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if not target.oclIsKindOf(Type) then Sequence{}
        ///                                 else
        ///                                 let feature: Sequence(Feature) =
        ///                                 target.oclAsType(Type).feature-&gt;select(f |
        ///                                 f.ownedRedefinition.redefinedFeature-&gt;
        ///                                 includes(referent)) in
        ///                                 if feature-&gt;notEmpty() then
        ///                                 feature.valuation.value.evaluate(target)
        ///                                 else if referent.featuringType-&gt;isEmpty()
        ///                                 then referent
        ///                                 else Sequence{}
        ///                                 endif endif
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="featureReferenceExpressionSubject">
        /// The subject <see cref="IFeatureReferenceExpression" />
        /// </param>
        /// <param name="target">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IElement" />
        /// </returns>
        internal static List<IElement> ComputeRedefinedEvaluateOperation(this IFeatureReferenceExpression featureReferenceExpressionSubject, IElement target)
        {
            if (featureReferenceExpressionSubject == null)
            {
                throw new ArgumentNullException(nameof(featureReferenceExpressionSubject));
            }

            if (target is not IType targetType)
            {
                return [];
            }

            var referent = featureReferenceExpressionSubject.referent;

            var redefiningFeatures = targetType.feature
                .Where(feature => feature.ownedRedefinition.Any(redefinition => redefinition.RedefinedFeature == referent))
                .ToList();

            if (redefiningFeatures.Count != 0)
            {
                return
                [
                    .. redefiningFeatures
                    .Select(feature => feature.ownedMembership.OfType<IFeatureValue>().FirstOrDefault()?.value)
                    .Where(valueExpression => valueExpression != null)
                    .SelectMany(valueExpression => valueExpression.Evaluate(target))
                ];
            }

            return referent != null && referent.featuringType.Count == 0 ? [referent] : [];
        }
    }
}
