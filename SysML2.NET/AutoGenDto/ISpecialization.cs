// -------------------------------------------------------------------------------------------------
// <copyright file="ISpecialization.cs" company="RHEA System S.A.">
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
    /// Specialization is a Relationship between two Types that requires all instances of the specific type
    /// to also be instances of the general Type (i.e., the set of instances of the specific Type is a
    /// subset of those of the general Type, which might be the same set).not specific.isConjugated
    /// </summary>
    public partial interface ISpecialization : IRelationship
    {
        /// <summary>
        /// </summary>
        Guid General { get; set; }

        /// <summary>
        /// </summary>
        Guid Specific { get; set; }

    }
}
