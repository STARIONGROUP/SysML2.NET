// -------------------------------------------------------------------------------------------------
// <copyright file="FunctionExtensions.cs" company="Starion Group S.A.">
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
    using System.Linq;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.KernelFunctions;

    /// <summary>
    /// The <see cref="FunctionExtensions"/> class provides extensions methods for
    /// the <see cref="IFunction"/> interface
    /// </summary>
    internal static class FunctionExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="functionSubject">
        /// The subject <see cref="IFunction"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IExpression> ComputeExpression(this IFunction functionSubject)
        {
            return functionSubject == null
                ? throw new ArgumentNullException(nameof(functionSubject))
                : [.. functionSubject.feature.OfType<IExpression>()];
        }

        /// <summary>
        /// Computes whether this <see cref="IFunction"/> is one of the Kernel Functions Library
        /// functions that may be invoked by a model-level evaluable <see cref="IInvocationExpression"/>.
        /// </summary>
        /// <remarks>
        /// There is no OCL derivation: KerML 1.0 §8.3.4.7.4 makes this a library-membership test, and
        /// the member set is enumerated by Table 5 (§8.2.5.8.1) and Table 7 (§8.2.5.8.2).
        /// </remarks>
        /// <param name="functionSubject">
        /// The subject <see cref="IFunction"/>
        /// </param>
        /// <returns>
        /// <c>true</c> when the subject is a model-level evaluable library function, <c>false</c> otherwise
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="functionSubject"/> is <c>null</c>.
        /// </exception>
        internal static bool ComputeIsModelLevelEvaluable(this IFunction functionSubject)
        {
            return functionSubject == null
                ? throw new ArgumentNullException(nameof(functionSubject))
                : ModelLevelEvaluableFunctions.Contains(functionSubject.owningNamespace?.name, functionSubject.name);
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// result =
        ///                             let resultParams : Sequence(Feature) =
        ///                             featureMemberships-&gt;
        ///                             selectByKind(ReturnParameterMembership).
        ///                             ownedMemberParameter in
        ///                             if resultParams-&gt;notEmpty() then resultParams-&gt;first()
        ///                             else null
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="functionSubject">
        /// The subject <see cref="IFunction"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IFeature ComputeResult(this IFunction functionSubject)
        {
            if (functionSubject == null)
            {
                throw new ArgumentNullException(nameof(functionSubject));
            }

            var resultParams = functionSubject.featureMembership
                .OfType<IReturnParameterMembership>()
                .Select(returnParameterMembership => returnParameterMembership.ownedMemberParameter)
                .ToList();

            return resultParams.Count == 0 ? null : resultParams[0];
        }

    }
}
