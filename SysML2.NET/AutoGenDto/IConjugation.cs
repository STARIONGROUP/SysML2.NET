// -------------------------------------------------------------------------------------------------
// <copyright file="IConjugation.cs" company="RHEA System S.A.">
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
    /// Conjugation is a Relationship between two types in which the conjugatedType inherits all the
    /// Features of the originalType, but with all input and output Features reversed. That is, any Features
    /// with a FeatureMembership with direction in relative to the originalType are considered to have an
    /// effective direction of out relative to the conjugatedType and, similarly, Features with direction
    /// out in the originalType are considered to have an effective direction of in in the originalType.
    /// Features with direction inout, or with no direction, in the originalType, are inherited without
    /// change.A Type may participate as a conjugatedType in at most one Conjugation relationship, and such
    /// a Type may not also be the specific Type in any Generalization relationship.
    /// </summary>
    public interface IConjugation : IRelationship
    {
        /// <summary>
        /// </summary>
        Guid ConjugatedType { get; set; }

        /// <summary>
        /// </summary>
        Guid OriginalType { get; set; }

    }
}
