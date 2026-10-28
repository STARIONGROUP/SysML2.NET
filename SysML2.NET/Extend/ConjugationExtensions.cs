// ------------------------------------------------------------------------------------------------
// <copyright file="ConjugationExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Core.Types
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="ConjugationExtensions"/> class provides extensions methods for
    /// the <see cref="IConjugation"/> interface
    /// </summary>
    internal static class ConjugationExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="conjugationSubject">
        /// The subject <see cref="IConjugation"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IConjugation.owningType))]
        internal static IType ComputeOwningType(this IConjugation conjugationSubject)
        {
            return conjugationSubject == null
                ? throw new ArgumentNullException(nameof(conjugationSubject))
                : conjugationSubject.OwningRelatedElement as IType;
        }

    }
}
