// ------------------------------------------------------------------------------------------------
// <copyright file="BooleanExpressionExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Kernel.Functions
{
    using System;
    using System.Linq;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Core.Types;
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
        [DerivedProperty(name: nameof(IBooleanExpression.predicate))]
        internal static IPredicate ComputePredicate(this IBooleanExpression booleanExpressionSubject)
        {
            return booleanExpressionSubject == null
                ? throw new ArgumentNullException(nameof(booleanExpressionSubject))
                : booleanExpressionSubject.type.SingleOrDefaultStrict<IPredicate>(nameof(booleanExpressionSubject));
        }
    }
}
