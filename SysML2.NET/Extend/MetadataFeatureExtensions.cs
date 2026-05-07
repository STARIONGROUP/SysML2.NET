// -------------------------------------------------------------------------------------------------
// <copyright file="MetadataFeatureExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Kernel.Metadata
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="MetadataFeatureExtensions"/> class provides extensions methods for
    /// the <see cref="IMetadataFeature"/> interface
    /// </summary>
    internal static class MetadataFeatureExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="metadataFeatureSubject">
        /// The subject <see cref="IMetadataFeature"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IMetaclass ComputeMetaclass(this IMetadataFeature metadataFeatureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// If the given baseFeature is a feature of this MetadataFeature, or is directly or indirectly
        /// redefined by a feature, then return the result of evaluating the appropriate (model-level evaluable)
        /// value Expression for it (if any), with the MetadataFeature as the target.
        /// </summary>
        /// <param name="metadataFeatureSubject">
        /// The subject <see cref="IMetadataFeature"/>
        /// </param>
        /// <param name="baseFeature">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IElement" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IElement> ComputeEvaluateFeatureOperation(this IMetadataFeature metadataFeatureSubject, IFeature baseFeature)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Check if this MetadataFeature has a metaclass which is a kind of SemanticMetadata.
        /// </summary>
        /// <param name="metadataFeatureSubject">
        /// The subject <see cref="IMetadataFeature"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static bool ComputeIsSemanticOperation(this IMetadataFeature metadataFeatureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Check if this MetadataFeature has a metaclass that is a kind of KerML::Element (that is, it is from
        /// the reflective abstract syntax model).
        /// </summary>
        /// <param name="metadataFeatureSubject">
        /// The subject <see cref="IMetadataFeature"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static bool ComputeIsSyntacticOperation(this IMetadataFeature metadataFeatureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// If this MetadataFeature reflectively represents a model element, then return the corresponding
        /// Element instance from the MOF abstract syntax representation of the model.
        /// </summary>
        /// <param name="metadataFeatureSubject">
        /// The subject <see cref="IMetadataFeature"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="IElement" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IElement ComputeSyntaxElementOperation(this IMetadataFeature metadataFeatureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }
    }
}
