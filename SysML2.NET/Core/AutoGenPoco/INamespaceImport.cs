// -------------------------------------------------------------------------------------------------
// <copyright file="INamespaceImport.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2024 RHEA System S.A.
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
    using SysML2.NET.Decorators;

    /// <summary>
    /// A NamespaceImport is an Import that imports Memberships from its importedNamespace into the
    /// importOwningNamespace. If  isRecursive = false, then only the visible Memberships of the
    /// importedNamespace are imported. If  isRecursive = true, then, in addition, Memberships are
    /// recursively imported from any ownedMembers of the importedNamespace that are
    /// Namespaces.importedElement = importedNamespace
    /// </summary>
    public partial interface INamespaceImport : IImport
    {
        /// <summary>
        /// The Namespace whose visible Memberships are imported by this NamespaceImport.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Namespace ImportedNamespace { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
