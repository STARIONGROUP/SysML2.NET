// -------------------------------------------------------------------------------------------------
// <copyright file="MetadataAccessExpressionExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Kernel.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Metadata;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="MetadataAccessExpressionExtensions" /> class provides extensions methods for
    /// the <see cref="IMetadataAccessExpression" /> interface
    /// </summary>
    internal static class MetadataAccessExpressionExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="metadataAccessExpressionSubject">
        /// The subject <see cref="IMetadataAccessExpression" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IElement ComputeReferencedElement(this IMetadataAccessExpression metadataAccessExpressionSubject)
        {
            if (metadataAccessExpressionSubject == null)
            {
                throw new ArgumentNullException(nameof(metadataAccessExpressionSubject));
            }

            var ownedRelationships = metadataAccessExpressionSubject.OwnedRelationship;

            foreach (var ownedRelationship in ownedRelationships)
            {
                if (ownedRelationship is IOwningMembership owningMembership and not IFeatureMembership)
                {
                    return owningMembership.OwnedRelatedElement.RequireSingleOfType<IElement>(nameof(owningMembership));
                }
            }

            throw new IncompleteModelException(
                $"{nameof(IMetadataAccessExpression)}.referencedElement is [1..1] but no non-FeatureMembership OwningMembership was found on '{nameof(metadataAccessExpressionSubject)}'.");
        }

        /// <summary>
        /// A MetadataAccessExpression is always model-level evaluable.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// true
        /// </code>
        /// </remarks>
        /// <param name="metadataAccessExpressionSubject">
        /// The subject <see cref="IMetadataAccessExpression" />
        /// </param>
        /// <param name="visited">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeRedefinedModelLevelEvaluableOperation(this IMetadataAccessExpression metadataAccessExpressionSubject, List<IFeature> visited)
        {
            if (metadataAccessExpressionSubject == null)
            {
                throw new ArgumentNullException(nameof(metadataAccessExpressionSubject));
            }

            return true;
        }

        /// <summary>
        /// Return the ownedElements of the referencedElement that are MetadataFeatures and have the
        /// referencedElement as an annotatedElement, plus a MetadataFeature whose annotatedElement is the
        /// referencedElement, whose metaclass is the reflective Metaclass corresponding to the MOF class of the
        /// referencedElement and whose ownedFeatures are bound to the values of the MOF properties of the
        /// referencedElement.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// referencedElement.ownedElement-&gt;
        ///                                 select(oclIsKindOf(MetadataFeature)
        ///                                 and annotatedElement-&gt;includes(referencedElement))-&gt;
        ///                                 including(metaclassFeature())
        /// </code>
        /// </remarks>
        /// <param name="metadataAccessExpressionSubject">
        /// The subject <see cref="IMetadataAccessExpression" />
        /// </param>
        /// <param name="target">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IElement" />
        /// </returns>
        internal static List<IElement> ComputeRedefinedEvaluateOperation(this IMetadataAccessExpression metadataAccessExpressionSubject, IElement target)
        {
            if (metadataAccessExpressionSubject == null)
            {
                throw new ArgumentNullException(nameof(metadataAccessExpressionSubject));
            }

            var referencedElement = metadataAccessExpressionSubject.referencedElement;

            var result = new List<IElement>();

            foreach (var ownedElement in referencedElement.ownedElement)
            {
                if (ownedElement is IMetadataFeature metadataFeature
                    && metadataFeature.annotatedElement.Contains(referencedElement))
                {
                    result.Add(metadataFeature);
                }
            }

            result.Add(metadataAccessExpressionSubject.MetaclassFeature());

            return result;
        }

        /// <summary>
        /// Return a MetadataFeature whose annotatedElement is the referencedElement, whose metaclass is the
        /// reflective Metaclass corresponding to the MOF class of the referencedElement and whose ownedFeatures
        /// are bound to the MOF properties of the referencedElement.
        /// </summary>
        /// <param name="metadataAccessExpressionSubject">
        /// The subject <see cref="IMetadataAccessExpression" />
        /// </param>
        /// <returns>
        /// The expected <see cref="IMetadataFeature" />
        /// </returns>
        [ExcludeFromCodeCoverage]
        internal static IMetadataFeature ComputeMetaclassFeatureOperation(this IMetadataAccessExpression metadataAccessExpressionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }
    }
}
