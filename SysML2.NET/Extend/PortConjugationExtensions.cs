// ------------------------------------------------------------------------------------------------
// <copyright file="PortConjugationExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Ports
{
    using System;

    using SysML2.NET.Decorators;

    using SysML2.NET.Exceptions;

    /// <summary>
    /// The <see cref="PortConjugationExtensions" /> class provides extensions methods for
    /// the <see cref="IPortConjugation" /> interface
    /// </summary>
    internal static class PortConjugationExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="portConjugationSubject">
        /// The subject <see cref="IPortConjugation" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IPortConjugation.conjugatedPortDefinition))]
        internal static IConjugatedPortDefinition ComputeConjugatedPortDefinition(this IPortConjugation portConjugationSubject)
        {
            if (portConjugationSubject == null)
            {
                throw new ArgumentNullException(nameof(portConjugationSubject));
            }

            return portConjugationSubject.OwningRelatedElement as IConjugatedPortDefinition
                   ?? throw new IncompleteModelException(
                       $"{nameof(portConjugationSubject)} must have an owning related element of type {nameof(IConjugatedPortDefinition)}");
        }
    }
}
