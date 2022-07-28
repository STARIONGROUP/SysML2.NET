// -------------------------------------------------------------------------------------------------
// <copyright file="ICalculationUsage.cs" company="RHEA System S.A.">
//
// Copyright 2022 RHEA System S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A CalculationUsage is an ActionUsage that is also an Expression, and, so, is typed by a Function.
    /// Nominally, if the type is a CalculationDefinition, a CalculationUsage is a Usage of that
    /// CalculationDefinition within a system. However, other kinds of kernel Functions are also allowed, to
    /// permit use of Functions from the Kernel Library.A CalculationUsage must subset, directly or
    /// indirectly, either the base CalculationUsage calculations from the Systems model library, if it is
    /// not a composite feature, or the CalculationUsage subcalculations inherited from its owner, if it is
    /// a composite feature.
    /// </summary>
    public interface ICalculationUsage : IActionUsage, IExpression
    {
    }
}
