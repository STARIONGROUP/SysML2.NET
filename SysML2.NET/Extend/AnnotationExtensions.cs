// -------------------------------------------------------------------------------------------------
// <copyright file="AnnotationExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Root.Annotations
{
    using System;
    using System.Linq;

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
        internal static IAnnotatingElement ComputeOwningAnnotatingElement(this IAnnotation annotationSubject)
        {
            return annotationSubject == null
                ? throw new ArgumentNullException(nameof(annotationSubject))
                : annotationSubject.OwningRelatedElement as IAnnotatingElement;
        }
    }
}
