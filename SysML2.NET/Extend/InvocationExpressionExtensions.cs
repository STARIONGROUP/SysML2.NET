// ------------------------------------------------------------------------------------------------
// <copyright file="InvocationExpressionExtensions.cs" company="Starion Group S.A.">
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
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Decorators;

    /// <summary>
    /// The <see cref="InvocationExpressionExtensions" /> class provides extensions methods for
    /// the <see cref="IInvocationExpression" /> interface
    /// </summary>
    internal static class InvocationExpressionExtensions
    {
        /// <summary>
        /// An InvocationExpression is model-level evaluable if all its argument Expressions are model-level
        /// evaluable and its function is model-level evaluable.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// argument-&gt;forAll(modelLevelEvaluable(visited)) and
        ///                                 function.isModelLevelEvaluable
        /// </code>
        /// </remarks>
        /// <param name="invocationExpressionSubject">
        /// The subject <see cref="IInvocationExpression" />
        /// </param>
        /// <param name="visited">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        [Operation(nameof(IInvocationExpression.ModelLevelEvaluable))]
        internal static bool ComputeRedefinedModelLevelEvaluableOperation(this IInvocationExpression invocationExpressionSubject, List<IFeature> visited)
        {
            if (invocationExpressionSubject == null)
            {
                throw new ArgumentNullException(nameof(invocationExpressionSubject));
            }

            return invocationExpressionSubject.argument.All(argument => argument.ModelLevelEvaluable(visited))
                   && invocationExpressionSubject.function?.isModelLevelEvaluable == true;
        }

        /// <summary>
        /// Apply the Function that is the type of this InvocationExpression to the argument values resulting
        /// from evaluating each of the argument Expressions on the given target. If the application is not
        /// possible, then return an empty sequence.
        /// </summary>
        /// <param name="invocationExpressionSubject">
        /// The subject <see cref="IInvocationExpression" />
        /// </param>
        /// <param name="target">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IElement" />
        /// </returns>
        [ExcludeFromCodeCoverage]
        [Operation(nameof(IInvocationExpression.Evaluate))]
        internal static List<IElement> ComputeRedefinedEvaluateOperation(this IInvocationExpression invocationExpressionSubject, IElement target)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }
    }
}
