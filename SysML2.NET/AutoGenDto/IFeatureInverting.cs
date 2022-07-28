// -------------------------------------------------------------------------------------------------
// <copyright file="IFeatureInverting.cs" company="RHEA System S.A.">
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
    /// A FeatureInverting is a Relationship between Features asserting that their interpretations
    /// (sequences) are the reverse of each other, identified as featureInverted and invertingFeature. For
    /// example, a Feature identifying each person's parents is the inverse of a Feature identifying each
    /// person's children.  A person identified as a parent of another will identify that other as one of
    /// their children.
    /// </summary>
    public interface IFeatureInverting : IRelationship
    {
        /// <summary>
        /// </summary>
        Guid FeatureInverted { get; set; }

        /// <summary>
        /// </summary>
        Guid InvertingFeature { get; set; }

    }
}
