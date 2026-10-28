// ------------------------------------------------------------------------------------------------
// <copyright file="PortDefinitionExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Ports
{
    using System;
    using System.Linq;

    using SysML2.NET.Decorators;

    /// <summary>
    /// The <see cref="PortDefinitionExtensions" /> class provides extensions methods for
    /// the <see cref="IPortDefinition" /> interface
    /// </summary>
    internal static class PortDefinitionExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// conjugatedPortDefinition =
        ///                             let conjugatedPortDefinitions : OrderedSet(ConjugatedPortDefinition) =
        ///                             ownedMember-&gt;selectByKind(ConjugatedPortDefinition) in
        ///                             if conjugatedPortDefinitions-&gt;isEmpty() then null
        ///                             else conjugatedPortDefinitions-&gt;first()
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="portDefinitionSubject">
        /// The subject <see cref="IPortDefinition" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IPortDefinition.conjugatedPortDefinition))]
        internal static IConjugatedPortDefinition ComputeConjugatedPortDefinition(this IPortDefinition portDefinitionSubject)
        {
            return portDefinitionSubject == null
                ? throw new ArgumentNullException(nameof(portDefinitionSubject))
                : portDefinitionSubject.ownedMember.OfType<IConjugatedPortDefinition>().FirstOrDefault();
        }
    }
}
