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

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Associations;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Connectors;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IFlowEnd> ComputeFlowEnd(this IFlow flowSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IInteraction> ComputeInteraction(this IFlow flowSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IPayloadFeature ComputePayloadFeature(this IFlow flowSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IClassifier> ComputePayloadType(this IFlow flowSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IFeature ComputeSourceOutputFeature(this IFlow flowSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
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
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IFeature ComputeTargetInputFeature(this IFlow flowSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

    }
}
