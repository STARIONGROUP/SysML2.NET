// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureChainExpressionExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Kernel.Expressions
{
    using System;
    using System.Linq;

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
