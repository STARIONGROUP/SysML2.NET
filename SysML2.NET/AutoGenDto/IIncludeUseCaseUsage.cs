// -------------------------------------------------------------------------------------------------
// <copyright file="IIncludeUseCaseUsage.cs" company="RHEA System S.A.">
//
// Copyright 2022 RHEA System S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An IncludeUseCaseUsage is a UseCaseUsage that represents the inclusion of a UseCaseUsage by a
    /// UseCaseDefinition or UseCaseUsage. The UseCaseUsage to be included (which may be the
    /// IncludeUseCaseUsage itself) is related to the includedUseCase by a Subsetting Relationship. An
    /// IncludeUseCaseUsage is also a PerformActionUsage, with its includedUseCase as the performedAction.If
    /// the IncludeUseCaseUsage is owned by a UseCaseDefinition or UseCaseUsage, then it also subsets the
    /// UseCaseUsage UseCase::includedUseCases from the Systems model library.
    /// </summary>
    public partial interface IIncludeUseCaseUsage : IUseCaseUsage, IPerformActionUsage
    {
    }
}
