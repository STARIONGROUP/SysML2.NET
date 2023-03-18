// -------------------------------------------------------------------------------------------------
// <copyright file="IResultExpressionMembership.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A ResultExpressionMembership is a FeatureMembership that indicates that the ownedResultExpression
    /// provides the result values for the Function or Expression that owns it. The owning Function or
    /// Expression must contain a BindingConnector between the result parameter of the ownedResultExpression
    /// and the result parameter of the owning Function or Expression.owningType.oclIsKindOf(Function) or
    /// owningType.oclIsKindOf(Expression)
    /// </summary>
    public partial interface IResultExpressionMembership : IFeatureMembership
    {
        /// <summary>
        /// Queries the derived property OwnedResultExpression
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Expression QueryOwnedResultExpression();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
