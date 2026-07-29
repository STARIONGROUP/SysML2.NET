// -------------------------------------------------------------------------------------------------
// <copyright file="NullExpressionExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Kernel.Expressions
{
    using System;
    using System.Collections.Generic;

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
