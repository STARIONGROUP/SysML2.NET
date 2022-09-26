// -------------------------------------------------------------------------------------------------
// <copyright file="IOccurrenceDefinition.cs" company="RHEA System S.A.">
//
//   Copyright 2022 RHEA System S.A.
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
    /// An OccurrenceDefinition is a Definition of a Class of individuals that have an independent life over
    /// time and potentially an extent over space. This includes both structural things and behaviors that
    /// act on such structures.If isIndividual is true, then the OccurrenceDefinition is constrained to
    /// represent an individual thing. The instances of such an OccurrenceDefinition include all spatial and
    /// temporal portions of the individual being represented, but only one of these can be the complete
    /// Life of the individual. All other instances must be portions of the &quot;maximal portion&quot; that
    /// is single Life instance, capturing the conception that all of the instances represent one individual
    /// with a single &quot;identity&quot;.An OccurrenceDefinition must subclass, directly or indirectly,
    /// the base Class Occurrence from the Kernel model library.if not isIndividual then lifeClass =
    /// nullelse    lifeClass <> null and    lifeClass.allSupertypes()->includes(self)endif
    /// </summary>
    public partial interface IOccurrenceDefinition : IDefinition, IClass
    {
        /// <summary>
        /// Whether this OccurrenceDefinition is constrained to represent single individual.
        /// </summary>
        bool IsIndividual { get; set; }

        /// <summary>
        /// Queries the derived property LifeClass
        /// </summary>
        LifeClass QueryLifeClass();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
