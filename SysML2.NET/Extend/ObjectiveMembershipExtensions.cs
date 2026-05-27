// -------------------------------------------------------------------------------------------------
// <copyright file="ObjectiveMembershipExtensions.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Core.POCO.Systems.Cases
{
    using System;

    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="ObjectiveMembershipExtensions" /> class provides extensions methods for
    /// the <see cref="IObjectiveMembership" /> interface
    /// </summary>
    internal static class ObjectiveMembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="objectiveMembershipSubject">
        /// The subject <see cref="IObjectiveMembership" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IRequirementUsage ComputeOwnedObjectiveRequirement(this IObjectiveMembership objectiveMembershipSubject)
        {
            if (objectiveMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(objectiveMembershipSubject));
            }

            return objectiveMembershipSubject.OwnedRelatedElement.RequireSingleOfType<IRequirementUsage>(nameof(objectiveMembershipSubject));
        }
    }
}
