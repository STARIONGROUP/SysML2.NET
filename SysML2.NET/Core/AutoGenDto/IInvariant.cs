// -------------------------------------------------------------------------------------------------
// <copyright file="IInvariant.cs" company="RHEA System S.A.">
//
//   Copyright 2022 RHEA System S.A.
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

    /// <summary>
    /// An Invariant is a BooleanExpression that is asserted to have a specific Boolean result value. If
    /// isNegated = false, then the Invariant must subset, directly or indirectly, the BooleanExpression
    /// trueEvaluations from the Kernel library, meaning that the result is asserted to be true. If
    /// isNegated = true, then the Invariant must subset, directly or indirectly, the BooleanExpression
    /// falseEvaluations from the Kernel library, meaning that the result is asserted to be false.
    /// </summary>
    public partial interface IInvariant : IBooleanExpression
    {
        /// <summary>
        /// Whether this Invariant is asserted to be false rather than true.
        /// </summary>
        bool IsNegated { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
