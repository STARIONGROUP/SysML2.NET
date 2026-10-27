// ------------------------------------------------------------------------------------------------
// <copyright file="ConjugatedPortDefinitionExtensions.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="ConjugatedPortDefinitionExtensions" /> class provides extensions methods for
    /// the <see cref="IConjugatedPortDefinition" /> interface
    /// </summary>
    internal static class ConjugatedPortDefinitionExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="conjugatedPortDefinitionSubject">
        /// The subject <see cref="IConjugatedPortDefinition" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IConjugatedPortDefinition.originalPortDefinition))]
        internal static IPortDefinition ComputeOriginalPortDefinition(this IConjugatedPortDefinition conjugatedPortDefinitionSubject)
        {
            if (conjugatedPortDefinitionSubject == null)
            {
                throw new ArgumentNullException(nameof(conjugatedPortDefinitionSubject));
            }

            return conjugatedPortDefinitionSubject.owningMembership?.membershipOwningNamespace as IPortDefinition
                   ?? throw new IncompleteModelException(
                       $"{nameof(conjugatedPortDefinitionSubject)} must have an owning namespace of type {nameof(IPortDefinition)}");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="conjugatedPortDefinitionSubject">
        /// The subject <see cref="IConjugatedPortDefinition" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IConjugatedPortDefinition.ownedPortConjugator))]
        internal static IPortConjugation ComputeOwnedPortConjugator(this IConjugatedPortDefinition conjugatedPortDefinitionSubject)
        {
            if (conjugatedPortDefinitionSubject == null)
            {
                throw new ArgumentNullException(nameof(conjugatedPortDefinitionSubject));
            }

            return conjugatedPortDefinitionSubject.OwnedRelationship
                .SingleStrict<IPortConjugation>(nameof(conjugatedPortDefinitionSubject));
        }

        /// <summary>
        /// If the name of the originalPortDefinition is non-empty, then return that with the character ~
        /// prepended.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// let originalName : String = originalPortDefinition.name in
        ///                                 if originalName = null then null
        ///                                 else '~' + originalName
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="conjugatedPortDefinitionSubject">
        /// The subject <see cref="IConjugatedPortDefinition" />
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        [Operation(name: nameof(IConjugatedPortDefinition.EffectiveName))]
        internal static string ComputeRedefinedEffectiveNameOperation(this IConjugatedPortDefinition conjugatedPortDefinitionSubject)
        {
            if (conjugatedPortDefinitionSubject == null)
            {
                throw new ArgumentNullException(nameof(conjugatedPortDefinitionSubject));
            }

            var originalName = conjugatedPortDefinitionSubject.originalPortDefinition.name;

            return originalName is null
                ? null
                : $"~{originalName}";
        }
    }
}
