// -------------------------------------------------------------------------------------------------
// <copyright file="IDependency.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Root.Dependencies
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.DTO.Root.Elements;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Dependency is a Relationship that indicates that one or more client Elements require one more
    /// supplier Elements for their complete specification. In general, this means that a change to one of
    /// the supplier Elements may necessitate a change to, or re-specification of, the client Elements.     
    ///                   Note that a Dependency is entirely a model-level Relationship, without
    /// instance-level semantics.
    /// </summary>
    [Class(xmiId: "_19_0_2_12e503d9_1594006219028_628649_74", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IDependency : IRelationship
    {
        /// <summary>
        /// The Element or Elements dependent on the supplier Elements.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594006406653_175551_182", aggregation: AggregationKind.None, lowerValue: 1, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_696758_43228")]
        List<Guid> Client { get; set; }

        /// <summary>
        /// The Element or Elements on which the client Elements depend in some respect.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594006525044_548771_207", aggregation: AggregationKind.None, lowerValue: 1, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_138197_43179")]
        List<Guid> Supplier { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
