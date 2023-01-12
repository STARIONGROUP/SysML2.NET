// -------------------------------------------------------------------------------------------------
// <copyright file="IImport.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;

    /// <summary>
    /// An Import is an Relationship between its importOwningNamespace and either a Membership (for a
    /// MembershipImport) or another Namespace (for a NamespaceImport), which determines a set of
    /// Memberships that become importedMemberships of the importOwningNamespace. If isImportAll = false
    /// (the default), then only public Memberships are considered &quot;visible&quot;. If isImportAll =
    /// true, then all Memberships are considered &quot;visible&quot;, regardless of their declared
    /// visibility. If isRecursive = true, then visible Memberships are also recursively imported from owned
    /// sub-Namespaces.
    /// </summary>
    public partial interface IImport : IRelationship
    {
        /// <summary>
        /// Queries the derived property ImportedElement
        /// </summary>
        Element QueryImportedElement();

        /// <summary>
        /// Queries the derived property ImportOwningNamespace
        /// </summary>
        Namespace QueryImportOwningNamespace();

        /// <summary>
        /// Whether to import memberships without regard to declared visibility.
        /// </summary>
        bool IsImportAll { get; set; }

        /// <summary>
        /// Whether to recursively import Memberships from visible, owned sub-Namespaces.
        /// </summary>
        bool IsRecursive { get; set; }

        /// <summary>
        /// The visibility level of the imported members from this Import relative to the importOwningNamespace.
        /// </summary>
        VisibilityKind Visibility { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
