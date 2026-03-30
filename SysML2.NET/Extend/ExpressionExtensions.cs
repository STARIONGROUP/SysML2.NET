// -------------------------------------------------------------------------------------------------
// <copyright file="ExpressionExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Kernel.Functions
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="ExpressionExtensions"/> class provides extensions methods for
    /// the <see cref="IExpression"/> interface
    /// </summary>
    internal static class ExpressionExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="expressionSubject">
        /// The subject <see cref="IExpression"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IFunction ComputeFunction(this IExpression expressionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="expressionSubject">
        /// The subject <see cref="IExpression"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static bool ComputeIsModelLevelEvaluable(this IExpression expressionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="expressionSubject">
        /// The subject <see cref="IExpression"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IFeature ComputeResult(this IExpression expressionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Return whether this Expression is model-level evaluable. The visited parameter is used to track
        /// possible circular Feature references made from FeatureReferenceExpressions (see the redefinition of
        /// this operation for FeatureReferenceExpression). Such circular references are not allowed in
        /// model-level evaluable expressions.                            An Expression that is not otherwise
        /// specialized is model-level evaluable if it has no (non-implied) ownedSpecializations and all its
        /// ownedFeatures are either in parameters, the result parameter or a result Expression owned via a
        /// ResultExpressionMembership. The parameters  must not have any ownedFeatures or a FeatureValue, and
        /// the result Expression must be model-level evaluable.
        /// </summary>
        /// <param name="expressionSubject">
        /// The subject <see cref="IExpression"/>
        /// </param>
        /// <param name="visited">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected bool
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static bool ComputeModelLevelEvaluableOperation(this IExpression expressionSubject, IFeature visited)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// If this Expression isModelLevelEvaluable, then evaluate it using the target as the context Element
        /// for resolving Feature names and testing classification. The result is a collection of Elements,
        /// which, for a fully evaluable Expression, will be a LiteralExpression or a Feature that is not an
        /// Expression.
        /// </summary>
        /// <param name="expressionSubject">
        /// The subject <see cref="IExpression"/>
        /// </param>
        /// <param name="target">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected IElement
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IElement ComputeEvaluateOperation(this IExpression expressionSubject, IElement target)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Model-level evaluate this Expression with the given target. If the result is a LiteralBoolean,
        /// return its value. Otherwise return false.
        /// </summary>
        /// <param name="expressionSubject">
        /// The subject <see cref="IExpression"/>
        /// </param>
        /// <param name="target">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected bool
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static bool ComputeCheckConditionOperation(this IExpression expressionSubject, IElement target)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }
    }
}
