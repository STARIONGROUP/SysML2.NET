// ------------------------------------------------------------------------------------------------
// <copyright file="UnioningExtensions.cs" company="Starion Group S.A.">
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
    /// The <see cref="UnioningExtensions" /> class provides extensions methods for
    /// the <see cref="IUnioning" /> interface
    /// </summary>
    internal static class UnioningExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="unioningSubject">
        /// The subject <see cref="IUnioning" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IUnioning.typeUnioned))]
        internal static IType ComputeTypeUnioned(this IUnioning unioningSubject)
        {
            if (unioningSubject == null)
            {
                throw new ArgumentNullException(nameof(unioningSubject));
            }

            return unioningSubject.OwningRelatedElement as IType
                   ?? throw new IncompleteModelException(
                       $"{nameof(unioningSubject)} must have an owning related element of type {nameof(IType)}");
        }
    }
}
