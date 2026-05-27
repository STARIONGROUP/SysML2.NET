// -------------------------------------------------------------------------------------------------
// <copyright file="StateSubactionMembershipExtensions.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Core.POCO.Systems.States
{
    using System;

    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="StateSubactionMembershipExtensions" /> class provides extensions methods for
    /// the <see cref="IStateSubactionMembership" /> interface
    /// </summary>
    internal static class StateSubactionMembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="stateSubactionMembershipSubject">
        /// The subject <see cref="IStateSubactionMembership" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IActionUsage ComputeAction(this IStateSubactionMembership stateSubactionMembershipSubject)
        {
            if (stateSubactionMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(stateSubactionMembershipSubject));
            }

            return stateSubactionMembershipSubject.OwnedRelatedElement.RequireSingleOfType<IActionUsage>(nameof(stateSubactionMembershipSubject));
        }
    }
}
