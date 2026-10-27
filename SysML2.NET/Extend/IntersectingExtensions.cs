// ------------------------------------------------------------------------------------------------
// <copyright file="IntersectingExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Core.Types
{
    using System;

    using SysML2.NET.Decorators;

    using SysML2.NET.Exceptions;

    /// <summary>
    /// The <see cref="IntersectingExtensions" /> class provides extensions methods for
    /// the <see cref="IIntersecting" /> interface
    /// </summary>
    internal static class IntersectingExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="intersectingSubject">
        /// The subject <see cref="IIntersecting" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IIntersecting.typeIntersected))]
        internal static IType ComputeTypeIntersected(this IIntersecting intersectingSubject)
        {
            if (intersectingSubject == null)
            {
                throw new ArgumentNullException(nameof(intersectingSubject));
            }

            return intersectingSubject.OwningRelatedElement as IType
                   ?? throw new IncompleteModelException(
                       $"{nameof(intersectingSubject)} must have an owning related element of type {nameof(IType)}");
        }
    }
}
