// ------------------------------------------------------------------------------------------------
// <copyright file="ViewRenderingMembershipExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Decorators;

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
        [DerivedProperty(name: nameof(IViewRenderingMembership.ownedRendering))]
        internal static IRenderingUsage ComputeOwnedRendering(this IViewRenderingMembership viewRenderingMembershipSubject)
        {
            if (viewRenderingMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(viewRenderingMembershipSubject));
            }

            return viewRenderingMembershipSubject.OwnedRelatedElement.SingleStrict<IRenderingUsage>(nameof(viewRenderingMembershipSubject));
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
        [DerivedProperty(name: nameof(IViewRenderingMembership.referencedRendering))]
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
