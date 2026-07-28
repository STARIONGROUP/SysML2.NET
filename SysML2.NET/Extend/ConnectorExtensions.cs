// -------------------------------------------------------------------------------------------------
// <copyright file="ConnectorExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Kernel.Connectors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Associations;

    /// <summary>
    /// The <see cref="ConnectorExtensions"/> class provides extensions methods for
    /// the <see cref="IConnector"/> interface
    /// </summary>
    internal static class ConnectorExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="connectorSubject">
        /// The subject <see cref="IConnector"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IAssociation> ComputeAssociation(this IConnector connectorSubject)
        {
            return connectorSubject == null
                ? throw new ArgumentNullException(nameof(connectorSubject))
                : [.. FeatureExtensions.ComputeType(connectorSubject).OfType<IAssociation>()];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="connectorSubject">
        /// The subject <see cref="IConnector"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IFeature> ComputeConnectorEnd(this IConnector connectorSubject)
        {
            return connectorSubject == null
                ? throw new ArgumentNullException(nameof(connectorSubject))
                : [..connectorSubject.feature.Where(memberFeature => memberFeature.IsEnd)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// let commonFeaturingTypes : OrderedSet(Type) =
        ///                             relatedFeature-&gt;closure(featuringType)-&gt;select(t |
        ///                             relatedFeature-&gt;forAll(f | f.isFeaturedWithin(t))
        ///                             ) in
        ///                             let nearestCommonFeaturingTypes : OrderedSet(Type) =
        ///                             commonFeaturingTypes-&gt;reject(t1 |
        ///                             commonFeaturingTypes-&gt;exists(t2 |
        ///                             t2 &lt;&gt; t1 and t2-&gt;closure(featuringType)-&gt;contains(t1)
        ///                             )) in
        ///                             if nearestCommonFeaturingTypes-&gt;isEmpty() then null
        ///                             else nearestCommonFeaturingTypes-&gt;first()
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="connectorSubject">
        /// The subject <see cref="IConnector"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IType ComputeDefaultFeaturingType(this IConnector connectorSubject)
        {
            if (connectorSubject == null)
            {
                throw new ArgumentNullException(nameof(connectorSubject));
            }

            var relatedFeatures = connectorSubject.relatedFeature;

            if (relatedFeatures.Count == 0)
            {
                return null;
            }

            var fullClosure = ComputeFeaturingTypeClosure(relatedFeatures);

            var commonFeaturingTypes = fullClosure
                .Where(candidate => relatedFeatures.All(relatedFeature => relatedFeature.IsFeaturedWithin(candidate)))
                .ToList();

            if (commonFeaturingTypes.Count == 0)
            {
                return null;
            }

            var nearestCommonFeaturingTypes = commonFeaturingTypes
                .Where(candidateType => !commonFeaturingTypes.Any(successorType =>
                    successorType != candidateType
                    && ComputeFeaturingTypeClosure([successorType]).Contains(candidateType)))
                .ToList();

            return nearestCommonFeaturingTypes.Count == 0 ? null : nearestCommonFeaturingTypes[0];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// relatedFeature = connectorEnd.ownedReferenceSubsetting-&gt;
        ///                             select(s | s &lt;&gt; null).subsettedFeature
        /// </code>
        /// </remarks>
        /// <param name="connectorSubject">
        /// The subject <see cref="IConnector"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IFeature> ComputeRelatedFeature(this IConnector connectorSubject)
        {
            return connectorSubject == null
                ? throw new ArgumentNullException(nameof(connectorSubject))
                : [..connectorSubject.connectorEnd
                      .Select(connectorEndFeature => connectorEndFeature.ownedReferenceSubsetting)
                      .Where(referenceSubsetting => referenceSubsetting != null)
                      .Select(referenceSubsetting => referenceSubsetting.SubsettedFeature)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// sourceFeature =
        ///                             if relatedFeature-&gt;isEmpty() then null
        ///                             else relatedFeature-&gt;first()
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="connectorSubject">
        /// The subject <see cref="IConnector"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IFeature ComputeSourceFeature(this IConnector connectorSubject)
        {
            if (connectorSubject == null)
            {
                throw new ArgumentNullException(nameof(connectorSubject));
            }

            var relatedFeatures = connectorSubject.relatedFeature;

            return relatedFeatures.Count == 0 ? null : relatedFeatures[0];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// targetFeature =
        ///                             if relatedFeature-&gt;size() &lt; 2 then OrderedSet{}
        ///                             else
        ///                             relatedFeature-&gt;
        ///                             subSequence(2, relatedFeature-&gt;size())-&gt;
        ///                             asOrderedSet()
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="connectorSubject">
        /// The subject <see cref="IConnector"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IFeature> ComputeTargetFeature(this IConnector connectorSubject)
        {
            if (connectorSubject == null)
            {
                throw new ArgumentNullException(nameof(connectorSubject));
            }

            var relatedFeatures = connectorSubject.relatedFeature;

            return relatedFeatures.Count < 2 ? [] : relatedFeatures.GetRange(1, relatedFeatures.Count - 1);
        }

        /// <summary>
        /// Computes the OCL <c>closure(featuringType)</c> over a seed set of <see cref="IType"/> nodes.
        /// </summary>
        /// <remarks>
        /// The <c>featuringType</c> navigation is declared on <see cref="IFeature"/> only — pure
        /// <see cref="IType"/> nodes that are not also <see cref="IFeature"/> are sinks. The walk
        /// is breadth-first and uses a <see cref="HashSet{T}"/> visited-set to guarantee
        /// termination on cyclic featuringType graphs. The seed elements are included in the
        /// result, mirroring OCL <c>closure</c> semantics.
        /// </remarks>
        /// <param name="seed">
        /// The seed collection of <see cref="IType"/> elements from which to begin the closure.
        /// </param>
        /// <returns>
        /// The closure of the seed under the <c>featuringType</c> navigation, in BFS visit order.
        /// </returns>
        private static List<IType> ComputeFeaturingTypeClosure(IEnumerable<IType> seed)
        {
            var visited = new HashSet<IType>();
            var result = new List<IType>();
            var queue = new Queue<IType>(seed);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current == null || !visited.Add(current))
                {
                    continue;
                }

                result.Add(current);

                if (current is IFeature currentFeature)
                {
                    foreach (var featuringType in currentFeature.featuringType.Where(featuringType => featuringType != null && !visited.Contains(featuringType)))
                    {
                        queue.Enqueue(featuringType);
                    }
                }
            }

            return result;
        }
    }
}
