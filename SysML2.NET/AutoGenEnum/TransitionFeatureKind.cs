// -------------------------------------------------------------------------------------------------
// <copyright file="TransitionFeatureKind.cs" company="RHEA System S.A.">
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

namespace SysML2.NET
{
    /// <summary>
    /// A TransitionActionKind indicates whether the transitionFeature of a TransitionFeatureMembership is a
    /// trigger, guard or effect.
    /// </summary>
    public enum TransitionFeatureKind
    {
        /// <summary>
        /// Indicates that a member Transfer of a TransitionUsage represents a trigger.
        /// </summary>
        Trigger = 0,

        /// <summary>
        /// Indicates that a member Expression of a TransitionUsage represents a guard.
        /// </summary>
        Guard = 1,

        /// <summary>
        /// Indicates that a member Step of a TransitionUsage represents an effect.
        /// </summary>
        Effect = 2,

    }
}
