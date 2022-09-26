// -------------------------------------------------------------------------------------------------
// <copyright file="IViewDefinition.cs" company="RHEA System S.A.">
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
    /// A ViewDefinition is a PartDefinition that specifies how a view artifact is constructed to satisfy a
    /// viewpoint. It specifies a viewConditions to define the model content to be presented and a rendering
    /// to define how the model content is presented.A ViewDefinition must subclass, directly or indirectly,
    /// the base ViewDefinition View from the Systems model library.
    /// </summary>
    public partial interface IViewDefinition : IPartDefinition
    {
        /// <summary>
        /// Queries the derived property SatisfiedViewpoint
        /// </summary>
        List<ViewpointUsage> QuerySatisfiedViewpoint();

        /// <summary>
        /// Queries the derived property View
        /// </summary>
        List<ViewUsage> QueryView();

        /// <summary>
        /// Queries the derived property ViewCondition
        /// </summary>
        List<Expression> QueryViewCondition();

        /// <summary>
        /// Queries the derived property ViewRendering
        /// </summary>
        RenderingUsage QueryViewRendering();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
