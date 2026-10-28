// ------------------------------------------------------------------------------------------------
// <copyright file="TransitionFeatureMembershipExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.States
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.Systems.States;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="TransitionFeatureMembershipExtensions"/> class provides extensions methods for
    /// the <see cref="ITransitionFeatureMembership"/> interface
    /// </summary>
    internal static class TransitionFeatureMembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="transitionFeatureMembershipSubject">
        /// The subject <see cref="ITransitionFeatureMembership"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(ITransitionFeatureMembership.transitionFeature))]
        internal static IStep ComputeTransitionFeature(this ITransitionFeatureMembership transitionFeatureMembershipSubject)
        {
            if (transitionFeatureMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(transitionFeatureMembershipSubject));
            }

            return transitionFeatureMembershipSubject.OwnedRelatedElement.SingleStrict<IStep>(nameof(transitionFeatureMembershipSubject));
        }
    }
}
