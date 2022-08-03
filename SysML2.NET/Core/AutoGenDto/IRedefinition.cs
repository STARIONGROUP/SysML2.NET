// -------------------------------------------------------------------------------------------------
// <copyright file="IRedefinition.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Core.DTO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;

    /// <summary>
    /// Redefinition specializes Subsetting to require the redefinedFeature and
    /// the redefiningFeature to have the same values (on each instance of the domain of the
    /// redefiningFeature). This means any restrictions on the redefiningFeature, such as type or
    /// multiplicity, also apply to the redefinedFeature (on each instance of the owningType of the
    /// redefining Feature), and vice versa. The redefinedFeature might have values for instances of
    /// the owningType of the redefiningFeature, but only as instances of the owningType of the
    /// redefinedFeature that happen to also be instances of the owningType of the redefiningFeature. This
    /// is supported by the constraints inherited from Subsetting on the domains of the
    /// redefiningFeature and redefinedFeature. However, these constraints are narrowed for Redefinition to
    /// require the owningTypes of the redefiningFeature and redefinedFeature to be different and the
    /// redefinedFeature to not be imported into the owningNamespace of the
    /// redefiningFeature. This enables the redefiningFeature to have the same name as the
    /// redefinedFeature if desired.
    /// </summary>
    public partial interface IRedefinition : ISubsetting
    {
        /// <summary>
        /// </summary>
        Guid RedefinedFeature { get; set; }

        /// <summary>
        /// </summary>
        Guid RedefiningFeature { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
