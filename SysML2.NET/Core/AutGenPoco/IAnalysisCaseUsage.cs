// -------------------------------------------------------------------------------------------------
// <copyright file="IAnalysisCaseUsage.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
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

    /// <summary>
    /// An AnalysisCaseUsage is a Usage of an AnalysisCaseDefinition.An AnalysisCaseUsage must subset,
    /// directly or indirectly, either the base AnalysisCaseUsage analysisCases from the Systems model
    /// library, if it is not owned by an AnalysisCaseDefinition or AnalysisCaseUsage, or the
    /// AnalysisCaseUsage subAnalysisCases inherited from its owner, otherwise.
    /// </summary>
    public partial interface IAnalysisCaseUsage : ICaseUsage
    {
        /// <summary>
        /// Queries the derived property AnalysisAction
        /// </summary>
        List<ActionUsage> QueryAnalysisAction();

        /// <summary>
        /// Queries the derived property AnalysisCaseDefinition
        /// </summary>
        AnalysisCaseDefinition QueryAnalysisCaseDefinition();

        /// <summary>
        /// Queries the derived property ResultExpression
        /// </summary>
        Expression QueryResultExpression();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
