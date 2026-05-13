// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureMembershipExtensions.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

namespace SysML2.NET.Core.POCO.Core.Types
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Exceptions;

    /// <summary>
    /// The <see cref="FeatureMembershipExtensions"/> class provides extensions methods for
    /// the <see cref="IFeatureMembership"/> interface
    /// </summary>
    internal static class FeatureMembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="featureMembershipSubject">
        /// The subject <see cref="IFeatureMembership"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IFeature ComputeOwnedMemberFeature(this IFeatureMembership featureMembershipSubject)
        {
            if (featureMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(featureMembershipSubject));
            }

            return featureMembershipSubject.OwnedRelatedElement.Count != 1
                ? throw new IncompleteModelException($"{nameof(featureMembershipSubject)} must have exactly one related element")
                : featureMembershipSubject.OwnedRelatedElement[0] as IFeature;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="featureMembershipSubject">
        /// The subject <see cref="IFeatureMembership"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IType ComputeOwningType(this IFeatureMembership featureMembershipSubject)
        {
            return featureMembershipSubject == null
                ? throw new ArgumentNullException(nameof(featureMembershipSubject))
                : featureMembershipSubject.OwningRelatedElement as IType;
        }

    }
}
