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
    using System.Linq;

    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="BooleanExpressionExtensions" /> class provides extensions methods for
    /// the <see cref="IBooleanExpression" /> interface
    /// </summary>
    internal static class BooleanExpressionExtensions
    {
        /// <summary>
        /// Computes the derived <c>predicate</c> property: the <see cref="IPredicate"/> that is the
        /// single type of this <see cref="IBooleanExpression"/>.
        /// </summary>
        /// <param name="booleanExpressionSubject">
        /// The subject <see cref="IBooleanExpression" />
        /// </param>
        /// <returns>
        /// The matching <see cref="IPredicate"/>, or <c>null</c> when no such type exists.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="booleanExpressionSubject"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="MultiplicityViolationException">
        /// Thrown when more than one <see cref="IType"/> on the subject is an
        /// <see cref="IPredicate"/> (upper-bound violation against the derived <c>[0..1]</c> property).
        /// </exception>
        internal static IPredicate ComputePredicate(this IBooleanExpression booleanExpressionSubject)
        {
            return booleanExpressionSubject == null
                ? throw new ArgumentNullException(nameof(booleanExpressionSubject))
                : booleanExpressionSubject.type.SingleOrDefaultStrict<IPredicate>(nameof(booleanExpressionSubject));
        }
    }
}
