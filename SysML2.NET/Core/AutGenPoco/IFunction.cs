// -------------------------------------------------------------------------------------------------
// <copyright file="IFunction.cs" company="RHEA System S.A.">
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

    /// <summary>
    /// A Function is a Behavior that has a single out parameter that is identified as its result. Any other
    /// parameters of a Function than the result must have direction in. A Function represents the
    /// performance of a calculation that produces the values of its result parameter. This calculation may
    /// be decomposed into Expressions that are steps of the Function.
    /// </summary>
    public partial interface IFunction : IBehavior
    {
        /// <summary>
        /// Queries the derived property Expression
        /// </summary>
        List<Expression> QueryExpression();

        /// <summary>
        /// Queries the derived property IsModelLevelEvaluable
        /// </summary>
        bool QueryIsModelLevelEvaluable();

        /// <summary>
        /// Queries the derived property Result
        /// </summary>
        Feature QueryResult();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
