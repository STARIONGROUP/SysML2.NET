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

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Associations;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IAssociation> ComputeAssociation(this IConnector connectorSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IFeature> ComputeConnectorEnd(this IConnector connectorSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IType ComputeDefaultFeaturingType(this IConnector connectorSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IFeature> ComputeRelatedFeature(this IConnector connectorSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IFeature ComputeSourceFeature(this IConnector connectorSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IFeature> ComputeTargetFeature(this IConnector connectorSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

    }
}
