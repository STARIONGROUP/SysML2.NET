// ------------------------------------------------------------------------------------------------
// <copyright file="NullExpressionExtensions.cs" company="Starion Group S.A.">
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
    /// The <see cref="NullExpressionExtensions" /> class provides extensions methods for
    /// the <see cref="INullExpression" /> interface
    /// </summary>
    internal static class NullExpressionExtensions
    {
        /// <summary>
        /// A NullExpression is always model-level evaluable.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// true
        /// </code>
        /// </remarks>
        /// <param name="nullExpressionSubject">
        /// The subject <see cref="INullExpression" />
        /// </param>
        /// <param name="visited">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        [Operation(name: nameof(INullExpression.ModelLevelEvaluable))]
        internal static bool ComputeRedefinedModelLevelEvaluableOperation(this INullExpression nullExpressionSubject, List<IFeature> visited)
        {
            if (nullExpressionSubject == null)
            {
                throw new ArgumentNullException(nameof(nullExpressionSubject));
            }

            return true;
        }

        /// <summary>
        /// The model-level value of a NullExpression is an empty sequence.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// Sequence{}
        /// </code>
        /// </remarks>
        /// <param name="nullExpressionSubject">
        /// The subject <see cref="INullExpression" />
        /// </param>
        /// <param name="target">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IElement" />
        /// </returns>
        [Operation(name: nameof(INullExpression.Evaluate))]
        internal static List<IElement> ComputeRedefinedEvaluateOperation(this INullExpression nullExpressionSubject, IElement target)
        {
            if (nullExpressionSubject == null)
            {
                throw new ArgumentNullException(nameof(nullExpressionSubject));
            }

            return [];
        }
    }
}
