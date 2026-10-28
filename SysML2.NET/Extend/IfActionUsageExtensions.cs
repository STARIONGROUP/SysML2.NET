// ------------------------------------------------------------------------------------------------
// <copyright file="IfActionUsageExtensions.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Kernel.Functions;

    /// <summary>
    /// The <see cref="IfActionUsageExtensions" /> class provides extensions methods for
    /// the <see cref="IIfActionUsage" /> interface
    /// </summary>
    internal static class IfActionUsageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// elseAction =
        ///                             let parameter : Feature = inputParameter(3) in
        ///                             if parameter &lt;&gt; null and parameter.oclIsKindOf(ActionUsage) then
        ///                             parameter.oclAsType(ActionUsage)
        ///                             else
        ///                             null
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="ifActionUsageSubject">
        /// The subject <see cref="IIfActionUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IIfActionUsage.elseAction))]
        internal static IActionUsage ComputeElseAction(this IIfActionUsage ifActionUsageSubject)
        {
            return ifActionUsageSubject == null
                ? throw new ArgumentNullException(nameof(ifActionUsageSubject))
                : ifActionUsageSubject.InputParameter(3) as IActionUsage;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ifArgument =
        ///                             let parameter : Feature = inputParameter(1) in
        ///                             if parameter &lt;&gt; null and parameter.oclIsKindOf(Expression) then
        ///                             parameter.oclAsType(Expression)
        ///                             else
        ///                             null
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="ifActionUsageSubject">
        /// The subject <see cref="IIfActionUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IIfActionUsage.ifArgument))]
        internal static IExpression ComputeIfArgument(this IIfActionUsage ifActionUsageSubject)
        {
            return ifActionUsageSubject == null
                ? throw new ArgumentNullException(nameof(ifActionUsageSubject))
                : ifActionUsageSubject.InputParameter(1) as IExpression;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// thenAction =
        ///                             let parameter : Feature = inputParameter(2) in
        ///                             if parameter &lt;&gt; null and parameter.oclIsKindOf(ActionUsage) then
        ///                             parameter.oclAsType(ActionUsage)
        ///                             else
        ///                             null
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="ifActionUsageSubject">
        /// The subject <see cref="IIfActionUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IIfActionUsage.thenAction))]
        internal static IActionUsage ComputeThenAction(this IIfActionUsage ifActionUsageSubject)
        {
            return ifActionUsageSubject == null
                ? throw new ArgumentNullException(nameof(ifActionUsageSubject))
                : ifActionUsageSubject.InputParameter(2) as IActionUsage;
        }
    }
}
