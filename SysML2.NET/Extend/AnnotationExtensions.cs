// ------------------------------------------------------------------------------------------------
// <copyright file="AnnotationExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Root.Annotations
{
    using System;
    using System.Linq;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="AnnotationExtensions" /> class provides extensions methods for
    /// the <see cref="IAnnotation" /> interface
    /// </summary>
    internal static class AnnotationExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// annotatingElement =
        ///                             if ownedAnnotatingElement &lt;&gt; null then ownedAnnotatingElement
        ///                             else owningAnnotatingElement
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="annotationSubject">
        /// The subject <see cref="IAnnotation" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IAnnotation.annotatingElement))]
        internal static IAnnotatingElement ComputeAnnotatingElement(this IAnnotation annotationSubject)
        {
            return annotationSubject == null
                ? throw new ArgumentNullException(nameof(annotationSubject))
                : annotationSubject.ownedAnnotatingElement ?? annotationSubject.owningAnnotatingElement;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedAnnotatingElement =
        ///                             let ownedAnnotatingElements : Sequence(AnnotatingElement) =
        ///                             ownedRelatedElement-&gt;selectByKind(AnnotatingElement) in
        ///                             if ownedAnnotatingElements-&gt;isEmpty() then null
        ///                             else ownedAnnotatingElements-&gt;first()
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="annotationSubject">
        /// The subject <see cref="IAnnotation" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IAnnotation.ownedAnnotatingElement))]
        internal static IAnnotatingElement ComputeOwnedAnnotatingElement(this IAnnotation annotationSubject)
        {
            return annotationSubject == null
                ? throw new ArgumentNullException(nameof(annotationSubject))
                : annotationSubject.OwnedRelatedElement.OfType<IAnnotatingElement>().FirstOrDefault();
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="annotationSubject">
        /// The subject <see cref="IAnnotation" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IAnnotation.owningAnnotatedElement))]
        internal static IElement ComputeOwningAnnotatedElement(this IAnnotation annotationSubject)
        {
            if (annotationSubject == null)
            {
                throw new ArgumentNullException(nameof(annotationSubject));
            }

            var owningRelatedElement = annotationSubject.OwningRelatedElement;

            return owningRelatedElement != null
                   && ReferenceEquals(owningRelatedElement, annotationSubject.AnnotatedElement)
                ? owningRelatedElement
                : null;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="annotationSubject">
        /// The subject <see cref="IAnnotation" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IAnnotation.owningAnnotatingElement))]
        internal static IAnnotatingElement ComputeOwningAnnotatingElement(this IAnnotation annotationSubject)
        {
            return annotationSubject == null
                ? throw new ArgumentNullException(nameof(annotationSubject))
                : annotationSubject.OwningRelatedElement as IAnnotatingElement;
        }
    }
}
