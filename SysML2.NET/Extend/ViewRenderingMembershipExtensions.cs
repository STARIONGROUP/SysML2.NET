// -------------------------------------------------------------------------------------------------
// <copyright file="ViewRenderingMembershipExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Systems.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Extensions;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="ViewRenderingMembershipExtensions"/> class provides extensions methods for
    /// the <see cref="IViewRenderingMembership"/> interface
    /// </summary>
    internal static class ViewRenderingMembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="viewRenderingMembershipSubject">
        /// The subject <see cref="IViewRenderingMembership"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IRenderingUsage ComputeOwnedRendering(this IViewRenderingMembership viewRenderingMembershipSubject)
        {
            if (viewRenderingMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(viewRenderingMembershipSubject));
            }

            return viewRenderingMembershipSubject.OwnedRelatedElement.RequireSingleOfType<IRenderingUsage>(nameof(viewRenderingMembershipSubject));
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// referencedRendering =
        ///     let referencedFeature : Feature =
        ///     ownedRendering.referencedFeatureTarget() in
        ///     if referencedFeature = null then ownedRendering
        ///     else if referencedFeature.oclIsKindOf(RenderingUsage) then
        ///     refrencedFeature.oclAsType(RenderingUsage)
        ///     else null
        ///     endif endif
        /// </code>
        /// </remarks>
        /// <param name="viewRenderingMembershipSubject">
        /// The subject <see cref="IViewRenderingMembership"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IRenderingUsage ComputeReferencedRendering(this IViewRenderingMembership viewRenderingMembershipSubject)
        {
            if (viewRenderingMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(viewRenderingMembershipSubject));
            }

            var ownedRendering = viewRenderingMembershipSubject.ownedRendering;

            var referencedFeature = ownedRendering?.ReferencedFeatureTarget();

            if (referencedFeature == null)
            {
                return ownedRendering;
            }

            return referencedFeature as IRenderingUsage;
        }

    }
}
