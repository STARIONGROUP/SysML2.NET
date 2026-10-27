// ------------------------------------------------------------------------------------------------
// <copyright file="EnumerationDefinitionExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Enumerations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;

    /// <summary>
    /// The <see cref="EnumerationDefinitionExtensions" /> class provides extensions methods for
    /// the <see cref="IEnumerationDefinition" /> interface
    /// </summary>
    internal static class EnumerationDefinitionExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="enumerationDefinitionSubject">
        /// The subject <see cref="IEnumerationDefinition" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IEnumerationDefinition.enumeratedValue))]
        internal static List<IEnumerationUsage> ComputeEnumeratedValue(this IEnumerationDefinition enumerationDefinitionSubject)
        {
            return enumerationDefinitionSubject == null
                ? throw new ArgumentNullException(nameof(enumerationDefinitionSubject))
                : [..enumerationDefinitionSubject.ComputeVariant().OfType<IEnumerationUsage>()];
        }
    }
}
