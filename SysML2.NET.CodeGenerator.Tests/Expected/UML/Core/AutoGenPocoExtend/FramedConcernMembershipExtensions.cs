// ------------------------------------------------------------------------------------------------
// <copyright file="FramedConcernMembershipExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Requirements
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.Systems.Requirements;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Constraints;

    /// <summary>
    /// The <see cref="FramedConcernMembershipExtensions"/> class provides extensions methods for
    /// the <see cref="IFramedConcernMembership"/> interface
    /// </summary>
    internal static class FramedConcernMembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="framedConcernMembershipSubject">
        /// The subject <see cref="IFramedConcernMembership"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IFramedConcernMembership.ownedConcern))]
        internal static IConcernUsage ComputeOwnedConcern(this IFramedConcernMembership framedConcernMembershipSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="framedConcernMembershipSubject">
        /// The subject <see cref="IFramedConcernMembership"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IFramedConcernMembership.referencedConcern))]
        internal static IConcernUsage ComputeReferencedConcern(this IFramedConcernMembership framedConcernMembershipSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

    }
}
