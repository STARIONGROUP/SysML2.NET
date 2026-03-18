// -------------------------------------------------------------------------------------------------
// <copyright file="OwningMembership.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Root.Namespaces
{
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;

    /// <summary>
    /// An OwningMembership is a Membership that owns its memberElement as a ownedRelatedElement. The
    /// ownedMemberElement becomes an ownedMember of the membershipOwningNamespace.
    /// </summary>
    public partial class OwningMembership
    {
        /// <summary>
        /// Asserts that the current <see cref="OwningMembership"/> contains at least one <see cref="IFeature" /> inside the <see cref="OwnedRelatedElement"/> collection 
        /// </summary>
        /// <returns>True if one <see cref="IFeature"/> is contained into the <see cref="OwnedRelatedElement"/></returns>
        internal bool IsValidForNonFeatureMember()
        {
            return this.OwnedRelatedElement.OfType<IFeature>().Any();
        }

        /// <summary>
        /// Asserts that the current <see cref="OwningMembership"/> does not contains any <see cref="IFeature" /> inside the <see cref="OwnedRelatedElement"/> collection 
        /// </summary>
        /// <returns>True if none <see cref="IFeature"/> is contained into the <see cref="OwnedRelatedElement"/></returns>
        internal bool IsValidForFeatureMember()
        {
            return !this.IsValidForNonFeatureMember();
        }
    }
}
