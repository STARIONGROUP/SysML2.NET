// ------------------------------------------------------------------------------------------------
// <copyright file="SubclassificationExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Core.Classifiers
{
    using System;

    using SysML2.NET.Decorators;

    /// <summary>
    /// The <see cref="SubclassificationExtensions" /> class provides extensions methods for
    /// the <see cref="ISubclassification" /> interface
    /// </summary>
    internal static class SubclassificationExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="subclassificationSubject">
        /// The subject <see cref="ISubclassification" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(ISubclassification.owningClassifier))]
        internal static IClassifier ComputeOwningClassifier(this ISubclassification subclassificationSubject)
        {
            return subclassificationSubject == null
                ? throw new ArgumentNullException(nameof(subclassificationSubject))
                : subclassificationSubject.OwningRelatedElement as IClassifier;
        }
    }
}
