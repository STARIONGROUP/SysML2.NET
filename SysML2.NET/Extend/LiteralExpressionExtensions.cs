// ------------------------------------------------------------------------------------------------
// <copyright file="LiteralExpressionExtensions.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="LiteralExpressionExtensions" /> class provides extensions methods for
    /// the <see cref="ILiteralExpression" /> interface
    /// </summary>
    internal static class LiteralExpressionExtensions
    {
        /// <summary>
        /// A LiteralExpression is always model-level evaluable.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// true
        /// </code>
        /// </remarks>
        /// <param name="literalExpressionSubject">
        /// The subject <see cref="ILiteralExpression" />
        /// </param>
        /// <param name="visited">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        [Operation(name: nameof(ILiteralExpression.ModelLevelEvaluable))]
        internal static bool ComputeRedefinedModelLevelEvaluableOperation(this ILiteralExpression literalExpressionSubject, List<IFeature> visited)
        {
            if (literalExpressionSubject == null)
            {
                throw new ArgumentNullException(nameof(literalExpressionSubject));
            }

            return true;
        }

        /// <summary>
        /// The model-level value of a LiteralExpression is itself.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// Sequence{self}
        /// </code>
        /// </remarks>
        /// <param name="literalExpressionSubject">
        /// The subject <see cref="ILiteralExpression" />
        /// </param>
        /// <param name="target">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IElement" />
        /// </returns>
        [Operation(name: nameof(ILiteralExpression.Evaluate))]
        internal static List<IElement> ComputeRedefinedEvaluateOperation(this ILiteralExpression literalExpressionSubject, IElement target)
        {
            if (literalExpressionSubject == null)
            {
                throw new ArgumentNullException(nameof(literalExpressionSubject));
            }

            return [literalExpressionSubject];
        }
    }
}
