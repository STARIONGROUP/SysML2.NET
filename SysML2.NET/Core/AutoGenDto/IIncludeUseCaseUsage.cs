// -------------------------------------------------------------------------------------------------
// <copyright file="IIncludeUseCaseUsage.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Core.DTO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An IncludeUseCaseUsage is a UseCaseUsage that represents the inclusion of a UseCaseUsage by a
    /// UseCaseDefinition or UseCaseUsage. Unless it is the IncludeUseCaseUsage itself, the UseCaseUsage to
    /// be included is related to the includedUseCase by a ReferenceSubsetting Relationship. An
    /// IncludeUseCaseUsage is also a PerformActionUsage, with its useCaseIncluded as the
    /// performedAction.owningType <> null and(owningType.oclIsKindOf(UseCaseDefinition) or
    /// owningType.oclIsKindOf(UseCaseUsage) implies   
    /// specializesFromLibrary('UseCases::UseCase::includedUseCases')ownedReferenceSubsetting <> null
    /// implies    ownedReferenceSubsetting.referencedFeature.oclIsKindOf(UseCaseUsage)
    /// </summary>
    public partial interface IIncludeUseCaseUsage : IUseCaseUsage, IPerformActionUsage
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
