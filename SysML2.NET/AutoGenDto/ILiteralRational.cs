// -------------------------------------------------------------------------------------------------
// <copyright file="ILiteralRational.cs" company="RHEA System S.A.">
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
    /// A LiteralRational is a LiteralExpression that provides a Rational value as a result. It must have an
    /// owned result parameter whose type is Rational.An Expression that provides a Real value as a result.A
    /// LiteralReal must be typed by a specialization of Evaluation with no input parameters and a single
    /// Real value as its result.
    /// </summary>
    public interface ILiteralRational : ILiteralExpression
    {
        /// <summary>
        /// The value whose rational approximation is the result of evaluating this Expression.The Real value
        /// that is the result of evaluating this Expression.
        /// </summary>
        double Value { get; set; }

    }
}
