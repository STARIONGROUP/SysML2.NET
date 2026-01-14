// -------------------------------------------------------------------------------------------------
// <copyright file="IUseCaseDefinition.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.UseCases
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.DTO.Systems.Cases;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A UseCaseDefinition is a CaseDefinition that specifies a set of actions performed by its subject, in
    /// interaction with one or more actors external to the subject. The objective is to yield an observable
    /// result that is of value to one or more of the actors.
    /// </summary>
    [Class(xmiId: "_19_0_4_12e503d9_1621460866763_205297_823", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IUseCaseDefinition : ICaseDefinition
    {
        /// <summary>
        /// The UseCaseUsages that are included by this UseCaseDefinition, which are the useCaseIncludeds of the
        /// IncludeUseCaseUsages owned by this UseCaseDefinition.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1621461043764_27_910", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<Guid> includedUseCase { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
