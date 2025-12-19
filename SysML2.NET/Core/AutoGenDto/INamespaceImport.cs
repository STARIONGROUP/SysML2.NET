// -------------------------------------------------------------------------------------------------
// <copyright file="INamespaceImport.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Decorators;

    /// <summary>
    /// A NamespaceImport is an Import that imports Memberships from its importedNamespace into the
    /// importOwningNamespace. If  isRecursive = false, then only the visible Memberships of the
    /// importedNamespace are imported. If  isRecursive = true, then, in addition, Memberships are
    /// recursively imported from any ownedMembers of the importedNamespace that are Namespaces.
    /// </summary>
    [Class(xmiId: "_19_0_4_12e503d9_1668208114894_902739_132", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface INamespaceImport : IImport
    {
        /// <summary>
        /// The Namespace whose visible Memberships are imported by this NamespaceImport.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674966_977620_43202", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_138197_43179")]
        Guid ImportedNamespace { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
