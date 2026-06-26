// -------------------------------------------------------------------------------------------------
// <copyright file="ViewUsageExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Systems.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Kernel.Metadata;
    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="ViewUsageExtensions"/> class provides extensions methods for
    /// the <see cref="IViewUsage"/> interface
    /// </summary>
    internal static class ViewUsageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// exposedElement = ownedImport-&gt;selectByKind(Expose).
        ///                             importedMemberships(Set{}).memberElement-&gt;
        ///                             select(elm | includeAsExposed(elm))-&gt;
        ///                             asOrderedSet()
        /// </code>
        /// </remarks>
        /// <param name="viewUsageSubject">
        /// The subject <see cref="IViewUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IElement> ComputeExposedElement(this IViewUsage viewUsageSubject)
        {
            return viewUsageSubject == null
                ? throw new ArgumentNullException(nameof(viewUsageSubject))
                : [
                    ..viewUsageSubject.ownedImport
                        .OfType<IExpose>()
                        .SelectMany(expose => expose.ImportedMemberships([]))
                        .Select(membership => membership.MemberElement)
                        .Where(memberElement => memberElement != null)
                        .Where(viewUsageSubject.IncludeAsExposed)
                        .Distinct()
                ];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// satisfiedViewpoint = ownedRequirement-&gt;
        ///                             selectByKind(ViewpointUsage)-&gt;
        ///                             select(isComposite)
        /// </code>
        /// </remarks>
        /// <param name="viewUsageSubject">
        /// The subject <see cref="IViewUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IViewpointUsage> ComputeSatisfiedViewpoint(this IViewUsage viewUsageSubject)
        {
            return viewUsageSubject == null
                ? throw new ArgumentNullException(nameof(viewUsageSubject))
                : [..viewUsageSubject.nestedRequirement.OfType<IViewpointUsage>().Where(viewpointUsage => viewpointUsage.IsComposite)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// viewCondition = ownedMembership-&gt;
        ///                             selectByKind(ElementFilterMembership).
        ///                             condition
        /// </code>
        /// </remarks>
        /// <param name="viewUsageSubject">
        /// The subject <see cref="IViewUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IExpression> ComputeViewCondition(this IViewUsage viewUsageSubject)
        {
            return viewUsageSubject == null
                ? throw new ArgumentNullException(nameof(viewUsageSubject))
                : [
                    ..viewUsageSubject.ownedMembership
                        .OfType<IElementFilterMembership>()
                        .Select(elementFilterMembership => elementFilterMembership.condition)
                        .Where(condition => condition != null)
                ];
        }

        /// <summary>
        /// Computes the derived <c>viewDefinition</c> property: the <see cref="IViewDefinition"/>
        /// targeted by the single <see cref="IFeatureTyping"/> owned by
        /// <paramref name="viewUsageSubject"/>.
        /// </summary>
        /// <param name="viewUsageSubject">
        /// The subject <see cref="IViewUsage"/>
        /// </param>
        /// <returns>
        /// The matching <see cref="IViewDefinition"/>, or <c>null</c> when no such typing exists.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="viewUsageSubject"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="MultiplicityViolationException">
        /// Thrown when more than one <see cref="IFeatureTyping"/> targets an
        /// <see cref="IViewDefinition"/> (upper-bound violation against the derived
        /// <c>[0..1]</c> property).
        /// </exception>
        internal static IViewDefinition ComputeViewDefinition(this IViewUsage viewUsageSubject)
        {
            if (viewUsageSubject is null)
            {
                throw new ArgumentNullException(nameof(viewUsageSubject));
            }

            return viewUsageSubject.definition.SingleOrDefaultStrict<IViewDefinition>(nameof(viewUsageSubject));
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// viewRendering =
        ///                             let renderings: OrderedSet(ViewRenderingMembership) =
        ///                             featureMembership-&gt;selectByKind(ViewRenderingMembership) in
        ///                             if renderings-&gt;isEmpty() then null
        ///                             else renderings-&gt;first().referencedRendering
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="viewUsageSubject">
        /// The subject <see cref="IViewUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IRenderingUsage ComputeViewRendering(this IViewUsage viewUsageSubject)
        {
            if (viewUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(viewUsageSubject));
            }

            var renderings = viewUsageSubject.featureMembership.OfType<IViewRenderingMembership>().ToList();

            return renderings.Count == 0 ? null : renderings[0].referencedRendering;
        }

        /// <summary>
        /// Determine whether the given element meets all the owned and inherited viewConditions.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// let metadataFeatures: Sequence(AnnotatingElement) =
        ///                                 element.ownedAnnotation.annotatingElement-&gt;
        ///                                 select(oclIsKindOf(MetadataFeature)) in
        ///                                 self.membership-&gt;selectByKind(ElementFilterMembership).
        ///                                 condition-&gt;forAll(cond |
        ///                                 metadataFeatures-&gt;exists(elem |
        ///                                 cond.checkCondition(elem)))
        /// </code>
        /// </remarks>
        /// <param name="viewUsageSubject">
        /// The subject <see cref="IViewUsage"/>
        /// </param>
        /// <param name="element">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeIncludeAsExposedOperation(this IViewUsage viewUsageSubject, IElement element)
        {
            if (viewUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(viewUsageSubject));
            }

            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            var metadataFeatures = element.ownedAnnotation
                .Select(annotation => annotation.annotatingElement)
                .OfType<IMetadataFeature>()
                .ToList();

            var conditions = viewUsageSubject.membership
                .OfType<IElementFilterMembership>()
                .Select(elementFilterMembership => elementFilterMembership.condition)
                .Where(condition => condition != null)
                .ToList();

            return conditions.All(condition => metadataFeatures.Any(condition.CheckCondition));
        }
    }
}
