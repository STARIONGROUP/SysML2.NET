// -------------------------------------------------------------------------------------------------
// <copyright file="IReferenceUsage.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.DefinitionAndUsage
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A ReferenceUsage is a Usage that specifies a non-compositional (isComposite = false) reference to
    /// something. The definition of a ReferenceUsage can be any kind of Classifier, with the default being
    /// the top-level Classifier Base::Anything from the Kernel Semantic Library. This allows the
    /// specification of a generic reference without distinguishing if the thing referenced is an attribute
    /// value, item, action, etc.
    /// </summary>
    [Class(xmiId: "_19_0_2_12e503d9_1591477377905_618531_857", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IReferenceUsage : IUsage
    {
        /// <summary>
        /// Always true for a ReferenceUsage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1624035133434_200283_41434", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: "true")]
        [RedefinedProperty(propertyName: "_19_0_4_12e503d9_1624035114787_488767_41423")]
        new bool IsReference { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
