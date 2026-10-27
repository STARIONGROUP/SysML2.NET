// ------------------------------------------------------------------------------------------------
// <copyright file="EventOccurrenceUsageExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Occurrences
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Classes;
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
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Core.POCO.Systems.Views;

    /// <summary>
    /// The <see cref="EventOccurrenceUsageExtensions"/> class provides extensions methods for
    /// the <see cref="IEventOccurrenceUsage"/> interface
    /// </summary>
    internal static class EventOccurrenceUsageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// eventOccurrence =
        ///                             if referencedFeatureTarget() = null then self
        ///                             else if referencedFeatureTarget().oclIsKindOf(OccurrenceUsage) then
        ///                             referencedFeatureTarget().oclAsType(OccurrenceUsage)
        ///                             else null
        ///                             endif endif
        /// </code>
        /// </remarks>
        /// <param name="eventOccurrenceUsageSubject">
        /// The subject <see cref="IEventOccurrenceUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IEventOccurrenceUsage.eventOccurrence))]
        internal static IOccurrenceUsage ComputeEventOccurrence(this IEventOccurrenceUsage eventOccurrenceUsageSubject)
        {
            if (eventOccurrenceUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(eventOccurrenceUsageSubject));
            }

            var referencedFeatureTarget = eventOccurrenceUsageSubject.ReferencedFeatureTarget();

            return referencedFeatureTarget == null
                ? eventOccurrenceUsageSubject
                : referencedFeatureTarget as IOccurrenceUsage;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="eventOccurrenceUsageSubject">
        /// The subject <see cref="IEventOccurrenceUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IEventOccurrenceUsage.isReference))]
        internal static bool ComputeIsReference(this IEventOccurrenceUsage eventOccurrenceUsageSubject)
        {
            // EventOccurrenceUsage::isReference is always true (SysML 2.0 spec, Clause 8.3.9.2).
            return eventOccurrenceUsageSubject == null
                ? throw new ArgumentNullException(nameof(eventOccurrenceUsageSubject))
                : true;
        }

    }
}
