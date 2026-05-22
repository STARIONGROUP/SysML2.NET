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
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="MetadataFeatureExtensions"/> class provides extensions methods for
    /// the <see cref="IMetadataFeature"/> interface
    /// </summary>
    internal static class MetadataFeatureExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// metaclass =
        ///                             let metaclassTypes : Sequence(Type) = type-&gt;selectByKind(Metaclass) in
        ///                             if metaclassTypes-&gt;isEmpty() then null
        ///                             else metaClassTypes-&gt;first()
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="metadataFeatureSubject">
        /// The subject <see cref="IMetadataFeature"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IMetaclass ComputeMetaclass(this IMetadataFeature metadataFeatureSubject)
        {
            if (metadataFeatureSubject == null)
            {
                throw new ArgumentNullException(nameof(metadataFeatureSubject));
            }

            var metaclassTypes = metadataFeatureSubject.type.OfType<IMetaclass>().ToList();

            return metaclassTypes.Count == 0 ? null : metaclassTypes[0];
        }

        /// <summary>
        /// If the given baseFeature is a feature of this MetadataFeature, or is directly or indirectly
        /// redefined by a feature, then return the result of evaluating the appropriate (model-level evaluable)
        /// value Expression for it (if any), with the MetadataFeature as the target.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// let selectedFeatures : Sequence(Feature) = feature-&gt;
        ///                                 select(closure(ownedRedefinition.redefinedFeature)-&gt;
        ///                                 includes(baseFeature)) in
        ///                                 if selectedFeatures-&gt;isEmpty() then null
        ///                                 else
        ///                                 let selectedFeature : Feature = selectedFeatures-&gt;first() in
        ///                                 let featureValues : FeatureValue = selectedFeature-&gt;
        ///                                 closure(ownedRedefinition.redefinedFeature).ownedMember-&gt;
        ///                                 selectAsKind(FeatureValue) in
        ///                                 if featureValues-&gt;isEmpty() then null
        ///                                 else featureValues-&gt;first().value.evaluate(self)
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="metadataFeatureSubject">
        /// The subject <see cref="IMetadataFeature"/>
        /// </param>
        /// <param name="baseFeature">
        /// The base <see cref="IFeature"/> to look up in the redefinition closure of each feature
        /// owned by the subject.
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IElement" />
        /// </returns>
        internal static List<IElement> ComputeEvaluateFeatureOperation(this IMetadataFeature metadataFeatureSubject, IFeature baseFeature)
        {
            if (metadataFeatureSubject == null)
            {
                throw new ArgumentNullException(nameof(metadataFeatureSubject));
            }

            if (baseFeature == null)
            {
                throw new ArgumentNullException(nameof(baseFeature));
            }

            var selectedFeatures = metadataFeatureSubject.feature
                .Where(feature => ComputeRedefinitionClosure(feature).Contains(baseFeature))
                .ToList();

            if (selectedFeatures.Count == 0)
            {
                return [];
            }

            var selectedFeature = selectedFeatures[0];

            var featureValues = ComputeRedefinitionClosure(selectedFeature)
                .SelectMany(feature => feature.ownedMember)
                .OfType<IFeatureValue>()
                .ToList();

            if (featureValues.Count == 0)
            {
                return [];
            }

            var valueExpression = featureValues[0].value;

            return valueExpression == null ? [] : valueExpression.Evaluate(metadataFeatureSubject);
        }

        /// <summary>
        /// Check if this MetadataFeature has a metaclass which is a kind of SemanticMetadata.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// specializesFromLibrary('Metaobjects::SemanticMetadata')
        /// </code>
        /// </remarks>
        /// <param name="metadataFeatureSubject">
        /// The subject <see cref="IMetadataFeature"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeIsSemanticOperation(this IMetadataFeature metadataFeatureSubject)
        {
            return metadataFeatureSubject == null
                ? throw new ArgumentNullException(nameof(metadataFeatureSubject))
                : metadataFeatureSubject.SpecializesFromLibrary("Metaobjects::SemanticMetadata");
        }

        /// <summary>
        /// Check if this MetadataFeature has a metaclass that is a kind of KerML::Element (that is, it is from
        /// the reflective abstract syntax model).
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// specializesFromLibrary('KerML::Element')
        /// </code>
        /// </remarks>
        /// <param name="metadataFeatureSubject">
        /// The subject <see cref="IMetadataFeature"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeIsSyntacticOperation(this IMetadataFeature metadataFeatureSubject)
        {
            return metadataFeatureSubject == null
                ? throw new ArgumentNullException(nameof(metadataFeatureSubject))
                : metadataFeatureSubject.SpecializesFromLibrary("KerML::Element");
        }

        /// <summary>
        /// If this MetadataFeature reflectively represents a model element, then return the corresponding
        /// Element instance from the MOF abstract syntax representation of the model.
        /// </summary>
        /// <remarks>
        /// English:
        /// <code>
        /// No OCL
        /// </code>
        /// OCL2.0:
        /// <code>
        /// isSyntactic()
        /// </code>
        /// </remarks>
        /// <param name="metadataFeatureSubject">
        /// The subject <see cref="IMetadataFeature"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="IElement" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IElement ComputeSyntaxElementOperation(this IMetadataFeature metadataFeatureSubject)
        {
            // Implementation deferred: requires a MOF reflective metaclass registry
            // (runtime MetadataFeature -> reflected IElement) that is not present in this SDK.
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the reflexive-transitive closure of <paramref name="start"/> over
        /// <c>ownedRedefinition.RedefinedFeature</c>, using a HashSet visited-set for cycle protection.
        /// </summary>
        /// <param name="start">
        /// The seed <see cref="IFeature"/> to start the closure from. The seed itself is included in
        /// the result when non-null.
        /// </param>
        /// <returns>
        /// A fresh <see cref="List{T}"/> containing the seed and all transitively redefined features,
        /// in BFS order. Returns an empty list when <paramref name="start"/> is null.
        /// </returns>
        private static List<IFeature> ComputeRedefinitionClosure(IFeature start)
        {
            var visited = new HashSet<IFeature>();
            var result = new List<IFeature>();
            var queue = new Queue<IFeature>();

            if (start != null && visited.Add(start))
            {
                queue.Enqueue(start);
                result.Add(start);
            }

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                foreach (var redefinedFeature in current.ownedRedefinition.Select(x => x.RedefinedFeature).Where(x => x != null && visited.Add(x)))
                {
                    queue.Enqueue(redefinedFeature);
                    result.Add(redefinedFeature);
                }
            }

            return result;
        }
    }
}
