// -------------------------------------------------------------------------------------------------
// <copyright file="OwningMembershipExtensions.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Root.Namespaces
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Exceptions;

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
        internal static IElement ComputeOwnedMemberElement(this IOwningMembership owningMembershipSubject)
        {
            if (owningMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(owningMembershipSubject));
            }

            return owningMembershipSubject.OwnedRelatedElement.Count != 1 ? throw new IncompleteModelException($"{nameof(owningMembershipSubject)} must have exactly one related element") : owningMembershipSubject.OwnedRelatedElement[0];
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
        internal static string ComputeOwnedMemberElementId(this IOwningMembership owningMembershipSubject)
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
        internal static string ComputeOwnedMemberName(this IOwningMembership owningMembershipSubject)
        {
            return owningMembershipSubject == null ? throw new ArgumentNullException(nameof(owningMembershipSubject)) : owningMembershipSubject.ownedMemberElement.name;
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
        internal static string ComputeOwnedMemberShortName(this IOwningMembership owningMembershipSubject)
        {
            return owningMembershipSubject == null ? throw new ArgumentNullException(nameof(owningMembershipSubject)) : owningMembershipSubject.ownedMemberElement.shortName;
        }

        /// <summary>
        /// If the ownedMemberElement of this OwningMembership has a non-null qualifiedName, then return the
        /// string constructed by appending to that qualifiedName the string "/owningMembership". Otherwise,
        /// return the path of the OwningMembership as specified for a Relationship in general.
        /// </summary>
        /// <param name="owningMembershipSubject">
        /// The subject <see cref="IOwningMembership"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static string ComputeRedefinedPathOperation(this IOwningMembership owningMembershipSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }
    }
}
