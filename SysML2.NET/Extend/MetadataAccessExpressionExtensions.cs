// -------------------------------------------------------------------------------------------------
// <copyright file="MetadataAccessExpressionExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Kernel.Expressions
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Kernel.Metadata;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="MetadataAccessExpressionExtensions"/> class provides extensions methods for
    /// the <see cref="IMetadataAccessExpression"/> interface
    /// </summary>
    internal static class MetadataAccessExpressionExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="metadataAccessExpressionSubject">
        /// The subject <see cref="IMetadataAccessExpression"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IElement ComputeReferencedElement(this IMetadataAccessExpression metadataAccessExpressionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// A MetadataAccessExpression is always model-level evaluable.
        /// </summary>
        /// <param name="metadataAccessExpressionSubject">
        /// The subject <see cref="IMetadataAccessExpression"/>
        /// </param>
        /// <param name="visited">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected bool
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static bool ComputeRedefinedModelLevelEvaluableOperation(this IMetadataAccessExpression metadataAccessExpressionSubject, IFeature visited)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Return the ownedElements of the referencedElement that are MetadataFeatures and have the
        /// referencedElement as an annotatedElement, plus a MetadataFeature whose annotatedElement is the
        /// referencedElement, whose metaclass is the reflective Metaclass corresponding to the MOF class of the
        /// referencedElement and whose ownedFeatures are bound to the values of the MOF properties of the
        /// referencedElement.
        /// </summary>
        /// <param name="metadataAccessExpressionSubject">
        /// The subject <see cref="IMetadataAccessExpression"/>
        /// </param>
        /// <param name="target">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected IElement
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IElement ComputeRedefinedEvaluateOperation(this IMetadataAccessExpression metadataAccessExpressionSubject, IElement target)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Return a MetadataFeature whose annotatedElement is the referencedElement, whose metaclass is the
        /// reflective Metaclass corresponding to the MOF class of the referencedElement and whose ownedFeatures
        /// are bound to the MOF properties of the referencedElement.
        /// </summary>
        /// <param name="metadataAccessExpressionSubject">
        /// The subject <see cref="IMetadataAccessExpression"/>
        /// </param>
        /// <returns>
        /// The expected IMetadataFeature
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IMetadataFeature ComputeMetaclassFeatureOperation(this IMetadataAccessExpression metadataAccessExpressionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }
    }
}
