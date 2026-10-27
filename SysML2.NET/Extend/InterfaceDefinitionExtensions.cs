// ------------------------------------------------------------------------------------------------
// <copyright file="InterfaceDefinitionExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Systems.Ports;

    /// <summary>
    /// The <see cref="InterfaceDefinitionExtensions" /> class provides extensions methods for
    /// the <see cref="IInterfaceDefinition" /> interface
    /// </summary>
    internal static class InterfaceDefinitionExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="interfaceDefinitionSubject">
        /// The subject <see cref="IInterfaceDefinition" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IInterfaceDefinition.interfaceEnd))]
        internal static List<IPortUsage> ComputeInterfaceEnd(this IInterfaceDefinition interfaceDefinitionSubject)
        {
            return interfaceDefinitionSubject == null
                ? throw new ArgumentNullException(nameof(interfaceDefinitionSubject))
                : [..interfaceDefinitionSubject.feature.Where(feature => feature.IsEnd).OfType<IPortUsage>()];
        }
    }
}
