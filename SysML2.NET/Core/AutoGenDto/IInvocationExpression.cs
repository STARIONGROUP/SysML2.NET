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

namespace SysML2.NET.Core.DTO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An InvocationExpression is an Expression each of whose input parameters are bound to the result of
    /// an argument Expression.not ownedTyping->exists(oclIsKindOf(Behavior)) andnot
    /// ownedSubsetting.subsettedFeature.type->exists(oclIsKindOf(Behavior)) implies   
    /// ownedFeature.selectByKind(BindingConnector)->exists(        relatedFeature->includes(self) and      
    ///  relatedFeature->includes(result))            TBDownedFeature->    select(direction =
    /// _'in').valuation->    select(v | v <> null).value
    /// </summary>
    public partial interface IInvocationExpression : IExpression
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
