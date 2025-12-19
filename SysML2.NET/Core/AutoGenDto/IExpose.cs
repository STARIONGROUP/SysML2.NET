// -------------------------------------------------------------------------------------------------
// <copyright file="IExpose.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.Views
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An Expose is an Import of Memberships into a ViewUsage that provide the Elements to be included in a
    /// view. Visibility is always ignored for an Expose (i.e., isImportAll = true).
    /// </summary>
    [Class(xmiId: "_19_0_2_12e503d9_1573075516960_794934_94", isAbstract: true, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IExpose : IImport
    {
        /// <summary>
        /// An Expose always imports all Elements, regardless of visibility (isImportAll = true).
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1622578615027_762161_80", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "true")]
        [RedefinedProperty(propertyName: "_19_0_4_12e503d9_1622577942205_869984_64")]
        new bool IsImportAll { get; set; }

        /// <summary>
        /// An Expose always has protected visibility.
        /// </summary>
        [Property(xmiId: "_2022x_2_12e503d9_1720469034555_222060_1140", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "protected")]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674976_798509_43257")]
        new VisibilityKind Visibility { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
