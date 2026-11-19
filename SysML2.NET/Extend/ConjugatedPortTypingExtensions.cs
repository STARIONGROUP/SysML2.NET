// ------------------------------------------------------------------------------------------------
// <copyright file="ConjugatedPortTypingExtensions.cs" company="Starion Group S.A.">
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

    /// <summary>
    /// The <see cref="ConjugatedPortTypingExtensions" /> class provides extensions methods for
    /// the <see cref="IConjugatedPortTyping" /> interface
    /// </summary>
    internal static class ConjugatedPortTypingExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// portDefinition = conjugatedPortDefinition.originalPortDefinition
        /// </code>
        /// </remarks>
        /// <param name="conjugatedPortTypingSubject">
        /// The subject <see cref="IConjugatedPortTyping" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(nameof(IConjugatedPortTyping.portDefinition))]
        internal static IPortDefinition ComputePortDefinition(this IConjugatedPortTyping conjugatedPortTypingSubject)
        {
            if (conjugatedPortTypingSubject == null)
            {
                throw new ArgumentNullException(nameof(conjugatedPortTypingSubject));
            }

            return conjugatedPortTypingSubject.ConjugatedPortDefinition?.originalPortDefinition;
        }
    }
}
