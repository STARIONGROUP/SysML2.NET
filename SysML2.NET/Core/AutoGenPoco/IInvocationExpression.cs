﻿// -------------------------------------------------------------------------------------------------
// <copyright file="IInvocationExpression.cs" company="RHEA System S.A.">
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
    /// An InvocationExpression is an Expression each of whose input parameters are bound to the result of
    /// an owned argument Expression. Each input parameter may be bound to the result of at most one
    /// argument.TBDnot ownedTyping->exists(oclIsKindOf(Behavior)) andnot
    /// ownedSubsetting.subsettedFeature.type->exists(oclIsKindOf(Behavior)) implies   
    /// ownedFeature.selectByKind(BindingConnector)->exists(        relatedFeature->includes(self) and      
    /// relatedFeature->includes(result))
    /// </summary>
    public partial interface IInvocationExpression : IExpression
    {
        /// <summary>
        /// Queries the derived property Argument
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Expression> QueryArgument();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
