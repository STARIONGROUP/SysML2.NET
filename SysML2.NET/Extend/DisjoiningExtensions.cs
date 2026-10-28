// ------------------------------------------------------------------------------------------------
// <copyright file="DisjoiningExtensions.cs" company="Starion Group S.A.">
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
    /// The <see cref="DisjoiningExtensions"/> class provides extensions methods for
    /// the <see cref="IDisjoining"/> interface
    /// </summary>
    internal static class DisjoiningExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="disjoiningSubject">
        /// The subject <see cref="IDisjoining"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IDisjoining.owningType))]
        internal static IType ComputeOwningType(this IDisjoining disjoiningSubject)
        {
            return disjoiningSubject == null
                ? throw new ArgumentNullException(nameof(disjoiningSubject))
                : disjoiningSubject.OwningRelatedElement as IType;
        }

    }
}
