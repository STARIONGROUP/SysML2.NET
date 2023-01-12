// -------------------------------------------------------------------------------------------------
// <copyright file="IOccurrenceUsage.cs" company="RHEA System S.A.">
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
    /// An OccurrenceUsage is a Usage whose type is a Class. Nominally, if the type is an
    /// OccurrenceDefinition, an OccurrenceUsage is a Usage of that OccurrenceDefinition within a system.
    /// However, other types of Kernel Classes are also allowed, to permit use of Classes from the Kernel
    /// Library.An OccurrenceUsage must subset, directly or indirectly, the base Feature occurrences from
    /// the Kernel model library.if portionKind = null then portioningFeature = nullelse    
    /// portioningFeature <> null and    portionKind = portioningFeature.portionKind and   
    /// occurrenceDefinition.asSet() = portioningFeature.type.asSet()endiflet individualDefinitions :
    /// Sequence(OccurrenceDefinition) =     occurrenceDefinition->       
    /// selectByKind(OccurrenceDefinition)->        select(isIndividual) inif
    /// individualDefinitions->isEmpty() then nullelse individualDefinitions->at(1) endifisIndividual
    /// implies individualDefinition <> null
    /// </summary>
    public partial interface IOccurrenceUsage : IUsage
    {
        /// <summary>
        /// Queries the derived property IndividualDefinition
        /// </summary>
        OccurrenceDefinition QueryIndividualDefinition();

        /// <summary>
        /// Whether this OccurrenceUsage represents the usage of the specific individual (or portion of it)
        /// represented by its individualDefinition.
        /// </summary>
        bool IsIndividual { get; set; }

        /// <summary>
        /// Queries the derived property OccurrenceDefinition
        /// </summary>
        List<Class> QueryOccurrenceDefinition();

        /// <summary>
        /// Queries the derived property PortioningFeature
        /// </summary>
        PortioningFeature QueryPortioningFeature();

        /// <summary>
        /// The kind of portion of the instances of the occurrenceDefinition represented by this
        /// OccurrenceUsage, if it is so restricted.
        /// </summary>
        PortionKind? PortionKind { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
