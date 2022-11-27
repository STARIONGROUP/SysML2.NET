// -------------------------------------------------------------------------------------------------
// <copyright file="IItemFlow.cs" company="RHEA System S.A.">
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
    /// An ItemFlow is a Step that represents the transfer of objects or values from one Feature to another.
    /// ItemFlows can take non-zero time to complete.An ItemFlow must be typed by the Interaction Transfer
    /// from the Kernel Semantic Library, or a specialization of it.
    /// </summary>
    public partial interface IItemFlow : IConnector, IStep
    {
        /// <summary>
        /// Queries the derived property Interaction
        /// </summary>
        List<Interaction> QueryInteraction();

        /// <summary>
        /// Queries the derived property ItemFeature
        /// </summary>
        ItemFeature QueryItemFeature();

        /// <summary>
        /// Queries the derived property ItemFlowEnd
        /// </summary>
        List<ItemFlowEnd> QueryItemFlowEnd();

        /// <summary>
        /// Queries the derived property ItemType
        /// </summary>
        List<Classifier> QueryItemType();

        /// <summary>
        /// Queries the derived property SourceOutputFeature
        /// </summary>
        Feature QuerySourceOutputFeature();

        /// <summary>
        /// Queries the derived property TargetInputFeature
        /// </summary>
        Feature QueryTargetInputFeature();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
