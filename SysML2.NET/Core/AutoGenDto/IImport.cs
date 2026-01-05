// -------------------------------------------------------------------------------------------------
// <copyright file="IImport.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.Core.DTO.Root.Namespaces
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.DTO.Root.Elements;
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
    [Class(xmiId: "_18_5_3_12e503d9_1533160651693_673132_42174", isAbstract: true, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IImport : IRelationship
    {
        /// <summary>
        /// The effectively imported Element for this Import. For a MembershipImport, this is the memberElement
        /// of the importedMembership. For a NamespaceImport, it is the importedNamespace.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1668801846848_909736_64", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid importedElement { get; }

        /// <summary>
        /// The Namespace into which Memberships are imported by this Import, which must be the
        /// owningRelatedElement of the Import.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674974_548878_43248", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_693018_16749")]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_696758_43228")]
        Guid importOwningNamespace { get; }

        /// <summary>
        /// Whether to import memberships without regard to declared visibility.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1622577942205_869984_64", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsImportAll { get; set; }

        /// <summary>
        /// Whether to recursively import Memberships from visible, owned sub-Namespaces.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1605759116711_596237_5033", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsRecursive { get; set; }

        /// <summary>
        /// The visibility level of the imported members from this Import relative to the importOwningNamespace.
        /// The default is private.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674976_798509_43257", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "private")]
        VisibilityKind Visibility { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
