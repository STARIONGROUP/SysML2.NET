// -------------------------------------------------------------------------------------------------
// <copyright file="BooleanExpressionExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Kernel.Functions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// The <see cref="BooleanExpressionExtensions" /> class provides extensions methods for
    /// the <see cref="IBooleanExpression" /> interface
    /// </summary>
    internal static class BooleanExpressionExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="booleanExpressionSubject">
        /// The subject <see cref="IBooleanExpression" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [ExcludeFromCodeCoverage]
        internal static IPredicate ComputePredicate(this IBooleanExpression booleanExpressionSubject)
        {
            return booleanExpressionSubject == null
                ? throw new ArgumentNullException(nameof(booleanExpressionSubject))
                : booleanExpressionSubject.type.OfType<IPredicate>().FirstOrDefault();
        }
    }
}
