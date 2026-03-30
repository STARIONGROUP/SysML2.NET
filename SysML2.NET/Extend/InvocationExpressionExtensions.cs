// -------------------------------------------------------------------------------------------------
// <copyright file="InvocationExpressionExtensions.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

namespace SysML2.NET.Core.POCO.Kernel.Expressions
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="InvocationExpressionExtensions"/> class provides extensions methods for
    /// the <see cref="IInvocationExpression"/> interface
    /// </summary>
    internal static class InvocationExpressionExtensions
    {
        /// <summary>
        /// An InvocationExpression is model-level evaluable if all its argument Expressions are model-level
        /// evaluable and its function is model-level evaluable.
        /// </summary>
        /// <param name="invocationExpressionSubject">
        /// The subject <see cref="IInvocationExpression"/>
        /// </param>
        /// <param name="visited">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected bool
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static bool ComputeRedefinedModelLevelEvaluableOperation(this IInvocationExpression invocationExpressionSubject, IFeature visited)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Apply the Function that is the type of this InvocationExpression to the argument values resulting
        /// from evaluating each of the argument Expressions on the given target. If the application is not
        /// possible, then return an empty sequence.
        /// </summary>
        /// <param name="invocationExpressionSubject">
        /// The subject <see cref="IInvocationExpression"/>
        /// </param>
        /// <param name="target">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected IElement
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IElement ComputeRedefinedEvaluateOperation(this IInvocationExpression invocationExpressionSubject, IElement target)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }
    }
}
