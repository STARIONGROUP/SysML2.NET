// -------------------------------------------------------------------------------------------------
// <copyright file="ITriggerInvocationExpression.cs" company="Starion Group S.A.">
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.DTO.Systems.Actions
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Actions;
    using SysML2.NET.Core.DTO.Kernel.Expressions;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A TriggerInvocationExpression is an InvocationExpression that invokes one of the trigger Functions
    /// from the Kernel Semantic Library Triggers package, as indicated by its kind.
    /// </summary>
    [Class(xmiId: "Systems-Actions-TriggerInvocationExpression", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface ITriggerInvocationExpression : IInvocationExpression
    {
        /// <summary>
        /// Indicates which of the Functions from the Triggers model in the Kernel Semantic Library is to be
        /// invoked by this TriggerInvocationExpression.
        /// </summary>
        [Property(xmiId: "Systems-Actions-TriggerInvocationExpression-kind", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        TriggerKind Kind { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
