// ------------------------------------------------------------------------------------------------
// <copyright file="ActorMembershipExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Requirements
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="ActorMembershipExtensions"/> class provides extensions methods for
    /// the <see cref="IActorMembership"/> interface
    /// </summary>
    internal static class ActorMembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="actorMembershipSubject">
        /// The subject <see cref="IActorMembership"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IActorMembership.ownedActorParameter))]
        internal static IPartUsage ComputeOwnedActorParameter(this IActorMembership actorMembershipSubject)
        {
            if (actorMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(actorMembershipSubject));
            }

            return actorMembershipSubject.OwnedRelatedElement.SingleStrict<IPartUsage>(nameof(actorMembershipSubject));
        }

    }
}
