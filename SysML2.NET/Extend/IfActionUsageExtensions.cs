// -------------------------------------------------------------------------------------------------
// <copyright file="IfActionUsageExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Systems.Actions
{
    using System;

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
        internal static IActionUsage ComputeThenAction(this IIfActionUsage ifActionUsageSubject)
        {
            return ifActionUsageSubject == null
                ? throw new ArgumentNullException(nameof(ifActionUsageSubject))
                : ifActionUsageSubject.InputParameter(2) as IActionUsage;
        }
    }
}
