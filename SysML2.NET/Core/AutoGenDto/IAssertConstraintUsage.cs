// -------------------------------------------------------------------------------------------------
// <copyright file="IAssertConstraintUsage.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.Constraints
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.DTO.Kernel.Functions;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An AssertConstraintUsage is a ConstraintUsage that is also an Invariant and, so, is asserted to be
    /// true (by default). Unless it is the AssertConstraintUsage itself, the asserted ConstraintUsage is
    /// related to the AssertConstraintUsage by a ReferenceSubsetting Relationship.
    /// </summary>
    [Class(xmiId: "_19_0_2_12e503d9_1581045078368_47459_9326", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IAssertConstraintUsage : IConstraintUsage, IInvariant
    {
        /// <summary>
        /// The ConstraintUsage to be performed by the AssertConstraintUsage. It is the referenceFeature of the
        /// ownedReferenceSubsetting for the AssertConstraintUsage, if there is one, and, otherwise, the
        /// AssertConstraintUsage itself.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1581045158665_239617_9458", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid assertedConstraint { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
