// ------------------------------------------------------------------------------------------------
// <copyright file="FeatureChainExpressionExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Kernel.Expressions
{
    using System;
    using System.Linq;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;

    /// <summary>
    /// The <see cref="FeatureChainExpressionExtensions" /> class provides extensions methods for
    /// the <see cref="IFeatureChainExpression" /> interface
    /// </summary>
    internal static class FeatureChainExpressionExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// targetFeature =
        ///                             let nonParameterMemberships : Sequence(Membership) = ownedMembership-&gt;
        ///                             reject(oclIsKindOf(ParameterMembership)) in
        ///                             if nonParameterMemberships-&gt;isEmpty() or
        ///                             not nonParameterMemberships-&gt;first().memberElement.oclIsKindOf(Feature)
        ///                             then null
        ///                             else nonParameterMemberships-&gt;first().memberElement.oclAsType(Feature)
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="featureChainExpressionSubject">
        /// The subject <see cref="IFeatureChainExpression" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IFeatureChainExpression.targetFeature))]
        internal static IFeature ComputeTargetFeature(this IFeatureChainExpression featureChainExpressionSubject)
        {
            if (featureChainExpressionSubject == null)
            {
                throw new ArgumentNullException(nameof(featureChainExpressionSubject));
            }

            var nonParam = featureChainExpressionSubject.ownedMembership
                .FirstOrDefault(m => m is not IParameterMembership);

            return nonParam?.MemberElement as IFeature;
        }

        /// <summary>
        /// Return the first ownedFeature of the first owned input parameter of this FeatureChainExpression (if
        /// any).
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// let inputParameters : Feature = ownedFeatures-&gt;
        ///                                 select(direction = _'in') in
        ///                                 if inputParameters-&gt;isEmpty() or
        ///                                 inputParameters-&gt;first().ownedFeature-&gt;isEmpty()
        ///                                 then null
        ///                                 else inputParameters-&gt;first().ownedFeature-&gt;first()
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="featureChainExpressionSubject">
        /// The subject <see cref="IFeatureChainExpression" />
        /// </param>
        /// <returns>
        /// The expected <see cref="IFeature" />
        /// </returns>
        [Operation(name: nameof(IFeatureChainExpression.SourceTargetFeature))]
        internal static IFeature ComputeSourceTargetFeatureOperation(this IFeatureChainExpression featureChainExpressionSubject)
        {
            if (featureChainExpressionSubject == null)
            {
                throw new ArgumentNullException(nameof(featureChainExpressionSubject));
            }

            var firstInputParam = featureChainExpressionSubject.ownedFeature
                .FirstOrDefault(f => f.Direction == FeatureDirectionKind.In);

            return firstInputParam?.ownedFeature.FirstOrDefault();
        }
    }
}
