// -------------------------------------------------------------------------------------------------
// <copyright file="ReferenceUsageExtensions.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
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
