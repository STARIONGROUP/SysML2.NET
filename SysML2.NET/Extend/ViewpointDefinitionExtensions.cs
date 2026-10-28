// ------------------------------------------------------------------------------------------------
// <copyright file="ViewpointDefinitionExtensions.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.Allocations;
    using SysML2.NET.Core.POCO.Systems.AnalysisCases;
    using SysML2.NET.Core.POCO.Systems.Attributes;
    using SysML2.NET.Core.POCO.Systems.Calculations;
    using SysML2.NET.Core.POCO.Systems.Cases;
    using SysML2.NET.Core.POCO.Systems.Connections;
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Enumerations;
    using SysML2.NET.Core.POCO.Systems.Flows;
    using SysML2.NET.Core.POCO.Systems.Interfaces;
    using SysML2.NET.Core.POCO.Systems.Items;
    using SysML2.NET.Core.POCO.Systems.Metadata;
    using SysML2.NET.Core.POCO.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;

    /// <summary>
    /// The <see cref="ViewpointDefinitionExtensions"/> class provides extensions methods for
    /// the <see cref="IViewpointDefinition"/> interface
    /// </summary>
    internal static class ViewpointDefinitionExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// viewpointStakeholder = framedConcern.featureMemberhsip-&gt;
        ///                             selectByKind(StakeholderMembership).
        ///                             ownedStakeholderParameter
        /// </code>
        /// </remarks>
        /// <param name="viewpointDefinitionSubject">
        /// The subject <see cref="IViewpointDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IViewpointDefinition.viewpointStakeholder))]
        internal static List<IPartUsage> ComputeViewpointStakeholder(this IViewpointDefinition viewpointDefinitionSubject)
        {
            // The OCL uses "featureMemberhsip" which is a typo in the XMI source; the correct C# property is featureMembership.
            return viewpointDefinitionSubject == null
                ? throw new ArgumentNullException(nameof(viewpointDefinitionSubject))
                : [
                    ..viewpointDefinitionSubject.framedConcern
                        .SelectMany(concern => concern.featureMembership)
                        .OfType<IStakeholderMembership>()
                        .Select(stakeholderMembership => stakeholderMembership.ownedStakeholderParameter)
                ];
        }

    }
}
