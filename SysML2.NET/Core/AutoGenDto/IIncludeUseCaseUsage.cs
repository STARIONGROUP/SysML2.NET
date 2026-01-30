// -------------------------------------------------------------------------------------------------
// <copyright file="IIncludeUseCaseUsage.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.UseCases
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.DTO.Systems.Actions;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An IncludeUseCaseUsage is a UseCaseUsage that represents the inclusion of a UseCaseUsage by a
    /// UseCaseDefinition or UseCaseUsage. Unless it is the IncludeUseCaseUsage itself, the UseCaseUsage to
    /// be included is related to the includedUseCase by a ReferenceSubsetting Relationship. An
    /// IncludeUseCaseUsage is also a PerformActionUsage, with its useCaseIncluded as the performedAction.
    /// </summary>
    [Class(xmiId: "Systems-UseCases-IncludeUseCaseUsage", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IIncludeUseCaseUsage : IUseCaseUsage, IPerformActionUsage
    {
        /// <summary>
        /// The UseCaseUsage to be included by this IncludeUseCaseUsage. It is the performedAction of the
        /// IncludeUseCaseUsage considered as a PerformActionUsage, which must be a UseCaseUsage.
        /// </summary>
        [Property(xmiId: "Systems-UseCases-IncludeUseCaseUsage-useCaseIncluded", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Systems-Actions-PerformActionUsage-performedAction")]
        Guid useCaseIncluded { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
