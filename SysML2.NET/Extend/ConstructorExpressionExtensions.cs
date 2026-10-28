// ------------------------------------------------------------------------------------------------
// <copyright file="ConstructorExpressionExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Kernel.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Core.Features;

    /// <summary>
    /// The <see cref="ConstructorExpressionExtensions" /> class provides extensions methods for
    /// the <see cref="IConstructorExpression" /> interface
    /// </summary>
    internal static class ConstructorExpressionExtensions
    {
        /// <summary>
        /// A ConstructorExpression is model-level evaluable if all its argument Expressions are model-level
        /// evaluable.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// argument-&gt;forAll(modelLevelEvaluable(visited))
        /// </code>
        /// </remarks>
        /// <param name="constructorExpressionSubject">
        /// The subject <see cref="IConstructorExpression" />
        /// </param>
        /// <param name="visited">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        [Operation(name: nameof(IConstructorExpression.ModelLevelEvaluable))]
        internal static bool ComputeRedefinedModelLevelEvaluableOperation(this IConstructorExpression constructorExpressionSubject, List<IFeature> visited)
        {
            if (constructorExpressionSubject == null)
            {
                throw new ArgumentNullException(nameof(constructorExpressionSubject));
            }

            return constructorExpressionSubject.argument.All(argument => argument.ModelLevelEvaluable(visited));
        }
    }
}
