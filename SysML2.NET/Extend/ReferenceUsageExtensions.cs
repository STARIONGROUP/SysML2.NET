// ------------------------------------------------------------------------------------------------
// <copyright file="ReferenceUsageExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.DefinitionAndUsage
{
    using System;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Systems.States;

    /// <summary>
    /// The <see cref="ReferenceUsageExtensions" /> class provides extensions methods for
    /// the <see cref="IReferenceUsage" /> interface
    /// </summary>
    internal static class ReferenceUsageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="referenceUsageSubject">
        /// The subject <see cref="IReferenceUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IReferenceUsage.isReference))]
        internal static bool ComputeIsReference(this IReferenceUsage referenceUsageSubject)
        {
            if (referenceUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(referenceUsageSubject));
            }

            return true;
        }

        /// <summary>
        /// If this ReferenceUsage is the payload parameter of a TransitionUsage, then its naming Feature is the
        /// payloadParameter of the triggerAction of that TransitionUsage (if any).
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if owningType &lt;&gt; null and owningType.oclIsKindOf(TransitionUsage) and
        ///                                 owningType.oclAsType(TransitionUsage).inputParameter(2) = self then
        ///                                 owningType.oclAsType(TransitionUsage).triggerPayloadParameter()
        ///                                 else self.oclAsType(Usage).namingFeature()
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="referenceUsageSubject">
        /// The subject <see cref="IReferenceUsage" />
        /// </param>
        /// <returns>
        /// The expected <see cref="IFeature" />
        /// </returns>
        [Operation(name: nameof(IReferenceUsage.NamingFeature))]
        internal static IFeature ComputeRedefinedNamingFeatureOperation(this IReferenceUsage referenceUsageSubject)
        {
            if (referenceUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(referenceUsageSubject));
            }

            if (referenceUsageSubject.owningType is ITransitionUsage transitionUsage
                && transitionUsage.InputParameter(2) == referenceUsageSubject)
            {
                return transitionUsage.TriggerPayloadParameter();
            }

            return UsageExtensions.ComputeRedefinedNamingFeatureOperation(referenceUsageSubject);
        }
    }
}
