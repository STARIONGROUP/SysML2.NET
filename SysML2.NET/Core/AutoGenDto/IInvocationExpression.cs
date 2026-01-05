// -------------------------------------------------------------------------------------------------
// <copyright file="IInvocationExpression.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2025 Starion Group S.A.
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.DTO.Kernel.Expressions
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An InvocationExpression is an InstantiationExpression whose instantiatedType must be a Behavior or a
    /// Feature typed by a single Behavior (such as a Step). Each of the input parameters of the
    /// instantiatedType are bound to the result of an argument Expression. If the instantiatedType is a
    /// Function or a Feature typed by a Function, then the result of the InvocationExpression is the result
    /// of the invoked Function. Otherwise, the result is an instance of the instantiatedType (essentially
    /// like a behavioral ConstructorExpression).
    /// </summary>
    [Class(xmiId: "_18_5_3_12e503d9_1557528671608_638869_111563", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IInvocationExpression : IInstantiationExpression
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
