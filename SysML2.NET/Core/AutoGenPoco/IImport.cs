// -------------------------------------------------------------------------------------------------
// <copyright file="IImport.cs" company="Starion Group S.A.">
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Root.Namespaces
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An Import is an Relationship between its importOwningNamespace and either a Membership (for a
    /// MembershipImport) or another Namespace (for a NamespaceImport), which determines a set of
    /// Memberships that become importedMemberships of the importOwningNamespace. If isImportAll = false
    /// (the default), then only public Memberships are considered &quot;visible&quot;. If isImportAll =
    /// true, then all Memberships are considered &quot;visible&quot;, regardless of their declared
    /// visibility. If isRecursive = true, then visible Memberships are also recursively imported from owned
    /// sub-Namespaces.
    /// </summary>
    [Class(xmiId: "Root-Namespaces-Import", isAbstract: true, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IImport : IRelationship
    {
        /// <summary>
        /// The effectively imported Element for this Import. For a MembershipImport, this is the memberElement
        /// of the importedMembership. For a NamespaceImport, it is the importedNamespace.
        /// </summary>
        [Property(xmiId: "Root-Namespaces-Import-importedElement", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        IElement importedElement { get; }

        /// <summary>
        /// The Namespace into which Memberships are imported by this Import, which must be the
        /// owningRelatedElement of the Import.
        /// </summary>
        [Property(xmiId: "Root-Namespaces-Import-importOwningNamespace", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Relationship-owningRelatedElement")]
        [RedefinedProperty(propertyName: "Root-Elements-Relationship-source")]
        INamespace importOwningNamespace { get; }

        /// <summary>
        /// Whether to import memberships without regard to declared visibility.
        /// </summary>
        [Property(xmiId: "Root-Namespaces-Import-isImportAll", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsImportAll { get; set; }

        /// <summary>
        /// Whether to recursively import Memberships from visible, owned sub-Namespaces.
        /// </summary>
        [Property(xmiId: "Root-Namespaces-Import-isRecursive", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsRecursive { get; set; }

        /// <summary>
        /// The visibility level of the imported members from this Import relative to the importOwningNamespace.
        /// The default is private.
        /// </summary>
        [Property(xmiId: "Root-Namespaces-Import-visibility", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "private")]
        VisibilityKind Visibility { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
