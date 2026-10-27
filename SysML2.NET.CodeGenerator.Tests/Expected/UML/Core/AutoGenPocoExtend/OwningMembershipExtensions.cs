// ------------------------------------------------------------------------------------------------
// <copyright file="OwningMembershipExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Root.Namespaces
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="OwningMembershipExtensions"/> class provides extensions methods for
    /// the <see cref="IOwningMembership"/> interface
    /// </summary>
    internal static class OwningMembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="owningMembershipSubject">
        /// The subject <see cref="IOwningMembership"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IOwningMembership.ownedMemberElement))]
        internal static IElement ComputeOwnedMemberElement(this IOwningMembership owningMembershipSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="owningMembershipSubject">
        /// The subject <see cref="IOwningMembership"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IOwningMembership.ownedMemberElementId))]
        internal static string ComputeOwnedMemberElementId(this IOwningMembership owningMembershipSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedMemberName = ownedMemberElement.name
        /// </code>
        /// </remarks>
        /// <param name="owningMembershipSubject">
        /// The subject <see cref="IOwningMembership"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IOwningMembership.ownedMemberName))]
        internal static string ComputeOwnedMemberName(this IOwningMembership owningMembershipSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedMemberShortName = ownedMemberElement.shortName
        /// </code>
        /// </remarks>
        /// <param name="owningMembershipSubject">
        /// The subject <see cref="IOwningMembership"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IOwningMembership.ownedMemberShortName))]
        internal static string ComputeOwnedMemberShortName(this IOwningMembership owningMembershipSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// If the ownedMemberElement of this OwningMembership has a non-null qualifiedName, then return the
        /// string constructed by appending to that qualifiedName the string "/owningMembership". Otherwise,
        /// return the path of the OwningMembership as specified for a Relationship in general.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if ownedElement.qualifiedName &lt;&gt; null then
        ///     ownedElement.qualifiedName + '/owningMembership'
        /// else self.oclAsType(Relationship).path()
        /// endif
        /// </code>
        /// </remarks>
        /// <param name="owningMembershipSubject">
        /// The subject <see cref="IOwningMembership"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IOwningMembership.Path))]
        internal static string ComputeRedefinedPathOperation(this IOwningMembership owningMembershipSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }
    }
}
