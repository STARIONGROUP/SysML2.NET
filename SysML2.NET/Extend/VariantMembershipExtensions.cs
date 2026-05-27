// -------------------------------------------------------------------------------------------------
// <copyright file="VariantMembershipExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Systems.DefinitionAndUsage
{
    using System;

    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="VariantMembershipExtensions" /> class provides extensions methods for
    /// the <see cref="IVariantMembership" /> interface
    /// </summary>
    internal static class VariantMembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="variantMembershipSubject">
        /// The subject <see cref="IVariantMembership" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IUsage ComputeOwnedVariantUsage(this IVariantMembership variantMembershipSubject)
        {
            if (variantMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(variantMembershipSubject));
            }

            return variantMembershipSubject.OwnedRelatedElement.RequireSingleOfType<IUsage>(nameof(variantMembershipSubject));
        }
    }
}
