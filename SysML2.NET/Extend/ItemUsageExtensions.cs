// -------------------------------------------------------------------------------------------------
// <copyright file="ItemUsageExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Systems.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Structures;

    /// <summary>
    /// The <see cref="ItemUsageExtensions" /> class provides extensions methods for
    /// the <see cref="IItemUsage" /> interface
    /// </summary>
    internal static class ItemUsageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// itemDefinition = occurrenceDefinition-&gt;selectByKind(Structure)
        /// </code>
        /// </remarks>
        /// <param name="itemUsageSubject">
        /// The subject <see cref="IItemUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IStructure> ComputeItemDefinition(this IItemUsage itemUsageSubject)
        {
            return itemUsageSubject == null
                ? throw new ArgumentNullException(nameof(itemUsageSubject))
                : [.. itemUsageSubject.occurrenceDefinition.OfType<IStructure>()];
        }
    }
}
