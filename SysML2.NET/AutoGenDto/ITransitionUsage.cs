// -------------------------------------------------------------------------------------------------
// <copyright file="ITransitionUsage.cs" company="RHEA System S.A.">
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
    /// A TransitionUsage is an ActionUsage that is a behavioral Step representing a transition between
    /// ActionUsages or StateUsages.A TransitionUsage must subset, directly or indirectly, the base
    /// TransitionUsage transitionActions, if it is not a composite feature, or the TransitionUsage
    /// subtransitions inherited from its owner, if it is a composite feature.A TransitionUsage may by
    /// related to some of its ownedFeatures using TransitionFeatureMembership Relationships, corresponding
    /// to the triggers, guards and effects of the TransitionUsage.
    /// </summary>
    public interface ITransitionUsage : IActionUsage
    {
    }
}
