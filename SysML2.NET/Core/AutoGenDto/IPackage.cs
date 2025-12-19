// -------------------------------------------------------------------------------------------------
// <copyright file="IPackage.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Kernel.Packages
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Package is a Namespace used to group Elements, without any instance-level semantics. It may have
    /// one or more model-level evaluable filterCondition Expressions used to filter its
    /// importedMemberships. Any imported member must meet all of the filterConditions.
    /// </summary>
    [Class(xmiId: "_19_0_4_12e503d9_1606943754976_445656_5532", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IPackage : INamespace
    {
        /// <summary>
        /// The model-level evaluable Boolean-valued Expression used to filter the members of this Package,
        /// which are owned by the Package are via ElementFilterMemberships.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1607033896050_867332_6206", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_259543_43268")]
        List<Guid> FilterCondition { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
