// ------------------------------------------------------------------------------------------------
// <copyright file="ReferenceSubsettingExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Core.Features
{
    using System;

    using SysML2.NET.Decorators;

    using SysML2.NET.Exceptions;

    /// <summary>
    /// The <see cref="ReferenceSubsettingExtensions" /> class provides extensions methods for
    /// the <see cref="IReferenceSubsetting" /> interface
    /// </summary>
    internal static class ReferenceSubsettingExtensions
    {
        /// <summary>
        /// Computes the derived property <c>referencingFeature</c> — the <see cref="IFeature" /> that owns
        /// this <see cref="IReferenceSubsetting" /> relationship, which is also its subsettingFeature.
        /// </summary>
        /// <param name="referenceSubsettingSubject">
        /// The subject <see cref="IReferenceSubsetting" />
        /// </param>
        /// <returns>
        /// The <see cref="IFeature" /> that is the owning related element of this relationship.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="referenceSubsettingSubject" /> is null.
        /// </exception>
        /// <exception cref="IncompleteModelException">
        /// Thrown when the owning related element is null or is not an <see cref="IFeature" />.
        /// </exception>
        [DerivedProperty(name: nameof(IReferenceSubsetting.referencingFeature))]
        internal static IFeature ComputeReferencingFeature(this IReferenceSubsetting referenceSubsettingSubject)
        {
            if (referenceSubsettingSubject == null)
            {
                throw new ArgumentNullException(nameof(referenceSubsettingSubject));
            }

            return referenceSubsettingSubject.OwningRelatedElement as IFeature
                   ?? throw new IncompleteModelException(
                       $"{nameof(referenceSubsettingSubject)} must have an owning related element of type {nameof(IFeature)}");
        }
    }
}
