// ------------------------------------------------------------------------------------------------
// <copyright file="TriggerInvocationExpressionExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Actions
{
    using System;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.Systems.Actions;

    /// <summary>
    /// The <see cref="TriggerInvocationExpressionExtensions" /> class provides extensions methods for
    /// the <see cref="ITriggerInvocationExpression" /> interface
    /// </summary>
    internal static class TriggerInvocationExpressionExtensions
    {
        /// <summary>
        /// Return one of the Functions TriggerWhen, TriggerAt or TriggerAfter, from the Kernel Semantic Library
        /// Triggers package, depending on whether the kind of this TriggerInvocationExpression is when, at or
        /// after, respectively.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// resolveGlobal(
        ///                                 if kind = TriggerKind::when then
        ///                                 'Triggers::TriggerWhen'
        ///                                 else if kind = TriggerKind::at then
        ///                                 'Triggers::TriggerAt'
        ///                                 else
        ///                                 'Triggers::TriggerAfter'
        ///                                 endif endif
        ///                                 ).memberElement.oclAsType(Type)
        /// </code>
        /// </remarks>
        /// <param name="triggerInvocationExpressionSubject">
        /// The subject <see cref="ITriggerInvocationExpression" />
        /// </param>
        /// <returns>
        /// The expected <see cref="IType" />
        /// </returns>
        [Operation(name: nameof(ITriggerInvocationExpression.InstantiatedType))]
        internal static IType ComputeRedefinedInstantiatedTypeOperation(this ITriggerInvocationExpression triggerInvocationExpressionSubject)
        {
            if (triggerInvocationExpressionSubject == null)
            {
                throw new ArgumentNullException(nameof(triggerInvocationExpressionSubject));
            }

            var qualifiedName = triggerInvocationExpressionSubject.Kind switch
            {
                TriggerKind.When => "Triggers::TriggerWhen",
                TriggerKind.At => "Triggers::TriggerAt",
                _ => "Triggers::TriggerAfter"
            };

            return triggerInvocationExpressionSubject.ResolveGlobal(qualifiedName)?.MemberElement as IType;
        }
    }
}
