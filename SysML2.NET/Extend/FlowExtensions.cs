// -------------------------------------------------------------------------------------------------
// <copyright file="FlowExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Kernel.Interactions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Associations;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Connectors;

    /// <summary>
    /// The <see cref="FlowExtensions"/> class provides extensions methods for
    /// the <see cref="IFlow"/> interface
    /// </summary>
    internal static class FlowExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// flowEnd = connectorEnd-&gt;selectByKind(FlowEnd)
        /// </code>
        /// </remarks>
        /// <param name="flowSubject">
        /// The subject <see cref="IFlow"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IFlowEnd> ComputeFlowEnd(this IFlow flowSubject)
        {
            return flowSubject == null
                ? throw new ArgumentNullException(nameof(flowSubject))
                : [..flowSubject.connectorEnd.OfType<IFlowEnd>()];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="flowSubject">
        /// The subject <see cref="IFlow"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IInteraction> ComputeInteraction(this IFlow flowSubject)
        {
            return flowSubject == null
                ? throw new ArgumentNullException(nameof(flowSubject))
                : [..flowSubject.OwnedRelationship.OfType<IFeatureTyping>().Select(featureTyping => featureTyping.Type).OfType<IInteraction>()];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// payloadFeature =
        ///                             let payloadFeatures : Sequence(PayloadFeature) =
        ///                             ownedFeature-&gt;selectByKind(PayloadFeature) in
        ///                             if payloadFeatures-&gt;isEmpty() then null
        ///                             else payloadFeatures-&gt;first()
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="flowSubject">
        /// The subject <see cref="IFlow"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IPayloadFeature ComputePayloadFeature(this IFlow flowSubject)
        {
            if (flowSubject == null)
            {
                throw new ArgumentNullException(nameof(flowSubject));
            }

            var payloadFeatures = flowSubject.ownedFeature.OfType<IPayloadFeature>().ToList();

            return payloadFeatures.Count == 0 ? null : payloadFeatures[0];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// payloadType =
        ///                             if payloadFeature = null then Sequence{}
        ///                             else payloadFeature.type
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="flowSubject">
        /// The subject <see cref="IFlow"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IClassifier> ComputePayloadType(this IFlow flowSubject)
        {
            if (flowSubject == null)
            {
                throw new ArgumentNullException(nameof(flowSubject));
            }

            var payloadFeature = flowSubject.payloadFeature;

            return payloadFeature == null
                ? []
                : [..payloadFeature.type.OfType<IClassifier>()];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// sourceOutputFeature =
        ///                             if connectorEnd-&gt;isEmpty() or
        ///                             connectorEnd.ownedFeature-&gt;isEmpty()
        ///                             then null
        ///                             else connectorEnd.ownedFeature-&gt;first()
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="flowSubject">
        /// The subject <see cref="IFlow"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IFeature ComputeSourceOutputFeature(this IFlow flowSubject)
        {
            if (flowSubject == null)
            {
                throw new ArgumentNullException(nameof(flowSubject));
            }

            var connectorEnds = flowSubject.connectorEnd;

            if (connectorEnds.Count == 0)
            {
                return null;
            }

            var flatOwnedFeatures = connectorEnds.SelectMany(connectorEndFeature => connectorEndFeature.ownedFeature).ToList();

            return flatOwnedFeatures.Count == 0 ? null : flatOwnedFeatures[0];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// targetInputFeature =
        ///                             if connectorEnd-&gt;size() &lt; 2 or
        ///                             connectorEnd-&gt;at(2).ownedFeature-&gt;isEmpty()
        ///                             then null
        ///                             else connectorEnd-&gt;at(2).ownedFeature-&gt;first()
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="flowSubject">
        /// The subject <see cref="IFlow"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IFeature ComputeTargetInputFeature(this IFlow flowSubject)
        {
            if (flowSubject == null)
            {
                throw new ArgumentNullException(nameof(flowSubject));
            }

            var connectorEnds = flowSubject.connectorEnd;

            if (connectorEnds.Count < 2)
            {
                return null;
            }

            var secondConnectorEndOwnedFeatures = connectorEnds[1].ownedFeature;

            return secondConnectorEndOwnedFeatures.Count == 0 ? null : secondConnectorEndOwnedFeatures[0];
        }
    }
}
