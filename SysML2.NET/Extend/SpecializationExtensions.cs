// ------------------------------------------------------------------------------------------------
// <copyright file="SpecializationExtensions.cs" company="Starion Group S.A.">
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
    /// The <see cref="SpecializationExtensions" /> class provides extensions methods for
    /// the <see cref="ISpecialization" /> interface
    /// </summary>
    internal static class SpecializationExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="specializationSubject">
        /// The subject <see cref="ISpecialization" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(ISpecialization.owningType))]
        internal static IType ComputeOwningType(this ISpecialization specializationSubject)
        {
            return specializationSubject == null
                ? throw new ArgumentNullException(nameof(specializationSubject))
                : specializationSubject.OwningRelatedElement as IType;
        }
    }
}
