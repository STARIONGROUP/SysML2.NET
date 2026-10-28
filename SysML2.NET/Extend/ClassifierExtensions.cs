// ------------------------------------------------------------------------------------------------
// <copyright file="ClassifierExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Core.Classifiers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Decorators;

    /// <summary>
    /// The <see cref="ClassifierExtensions" /> class provides extensions methods for
    /// the <see cref="IClassifier" /> interface
    /// </summary>
    internal static class ClassifierExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedSubclassification =
        ///                             ownedSpecialization-&gt;selectByKind(Subclassification)
        /// </code>
        /// </remarks>
        /// <param name="classifierSubject">
        /// The subject <see cref="IClassifier" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IClassifier.ownedSubclassification))]
        internal static List<ISubclassification> ComputeOwnedSubclassification(this IClassifier classifierSubject)
        {
            return classifierSubject == null
                ? throw new ArgumentNullException(nameof(classifierSubject))
                : [..classifierSubject.ownedSpecialization.OfType<ISubclassification>()];
        }
    }
}
