// ------------------------------------------------------------------------------------------------
// <copyright file="DifferencingExtensions.cs" company="Starion Group S.A.">
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

    /// <summary>
    /// The <see cref="DifferencingExtensions" /> class provides extensions methods for
    /// the <see cref="IDifferencing" /> interface
    /// </summary>
    internal static class DifferencingExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="differencingSubject">
        /// The subject <see cref="IDifferencing" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(nameof(IDifferencing.typeDifferenced))]
        internal static IType ComputeTypeDifferenced(this IDifferencing differencingSubject)
        {
            if (differencingSubject == null)
            {
                throw new ArgumentNullException(nameof(differencingSubject));
            }

            return differencingSubject.OwningRelatedElement as IType;
        }
    }
}
