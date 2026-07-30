// -------------------------------------------------------------------------------------------------
// <copyright file="InstantiationExpressionExtensions.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Kernel.Functions;

    /// <summary>
    /// The <see cref="InstantiationExpressionExtensions" /> class provides extensions methods for
    /// the <see cref="IInstantiationExpression" /> interface
    /// </summary>
    internal static class InstantiationExpressionExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="instantiationExpressionSubject">
        /// The subject <see cref="IInstantiationExpression" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IExpression> ComputeArgument(this IInstantiationExpression instantiationExpressionSubject)
        {
            if (instantiationExpressionSubject == null)
            {
                throw new ArgumentNullException(nameof(instantiationExpressionSubject));
            }

            var instantiatedType = instantiationExpressionSubject.instantiatedType;

            if (instantiatedType == null)
            {
                return [];
            }

            return instantiationExpressionSubject is IConstructorExpression constructor
                ? [
                    .. instantiatedType.feature.SelectMany(f =>
                        constructor.result.ownedFeature.Where(of => of.Redefines(f)).Select(ValueOf).Where(value => value != null))
                ]
                : [
                    .. instantiatedType.input.SelectMany(inp =>
                        instantiationExpressionSubject.ownedFeature.Where(of => of.Redefines(inp)).Select(ValueOf).Where(value => value != null))
                ];

            static IExpression ValueOf(IFeature feature)
            {
                return feature.ownedMembership.OfType<IFeatureValue>().FirstOrDefault()?.value;
            }
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// instantiatedType = instantiatedType()
        /// </code>
        /// </remarks>
        /// <param name="instantiationExpressionSubject">
        /// The subject <see cref="IInstantiationExpression" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IType ComputeInstantiatedType(this IInstantiationExpression instantiationExpressionSubject)
        {
            if (instantiationExpressionSubject == null)
            {
                throw new ArgumentNullException(nameof(instantiationExpressionSubject));
            }

            return instantiationExpressionSubject.InstantiatedType();
        }

        /// <summary>
        /// Return the Type to act as the instantiatedType for this InstantiationExpression. By default, this is
        /// the memberElement of the first ownedMembership that is not a FeatureMembership, which must be a
        /// Type.                            <b>Note.</b> This operation is overridden in the subclass
        /// OperatorExpression.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// let members : Sequence(Element) = ownedMembership-&gt;
        ///                                 reject(oclIsKindOf(FeatureMembership)).memberElement in
        ///                                 if members-&gt;isEmpty() or not members-&gt;first().oclIsKindOf(Type) then null
        ///                                 else typeMembers-&gt;first().oclAsType(Type)
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="instantiationExpressionSubject">
        /// The subject <see cref="IInstantiationExpression" />
        /// </param>
        /// <returns>
        /// The expected <see cref="IType" />
        /// </returns>
        internal static IType ComputeInstantiatedTypeOperation(this IInstantiationExpression instantiationExpressionSubject)
        {
            if (instantiationExpressionSubject == null)
            {
                throw new ArgumentNullException(nameof(instantiationExpressionSubject));
            }

            return instantiationExpressionSubject.ownedMembership
                .Where(membership => membership is not IFeatureMembership)
                .Select(membership => membership.MemberElement)
                .FirstOrDefault() as IType;
        }
    }
}
