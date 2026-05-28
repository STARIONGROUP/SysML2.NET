// -------------------------------------------------------------------------------------------------
// <copyright file="AnnotatingElementExtensions.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="AnnotatingElementExtensions" /> class provides extensions methods for
    /// the <see cref="IAnnotatingElement" /> interface
    /// </summary>
    internal static class AnnotatingElementExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// annotatedElement =
        ///                             if annotation-&gt;notEmpty() then annotation.annotatedElement
        ///                             else Sequence{owningNamespace} endif
        /// </code>
        /// </remarks>
        /// <param name="annotatingElementSubject">
        /// The subject <see cref="IAnnotatingElement" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IElement> ComputeAnnotatedElement(this IAnnotatingElement annotatingElementSubject)
        {
            if (annotatingElementSubject == null)
            {
                throw new ArgumentNullException(nameof(annotatingElementSubject));
            }

            var annotations = annotatingElementSubject.annotation;

            if (annotations.Count != 0)
            {
                var result = new List<IElement>(annotations.Count);
                result.AddRange(annotations.Select(annotation => annotation.AnnotatedElement));

                return result;
            }

            return [annotatingElementSubject.owningNamespace];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// annotation =
        ///                             if owningAnnotatingRelationship = null then ownedAnnotatingRelationship
        ///                             else owningAnnotatingRelationship-&gt;prepend(owningAnnotatingRelationship)
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="annotatingElementSubject">
        /// The subject <see cref="IAnnotatingElement" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IAnnotation> ComputeAnnotation(this IAnnotatingElement annotatingElementSubject)
        {
            if (annotatingElementSubject == null)
            {
                throw new ArgumentNullException(nameof(annotatingElementSubject));
            }

            var owning = annotatingElementSubject.owningAnnotatingRelationship;
            var owned = annotatingElementSubject.ownedAnnotatingRelationship;

            if (owning == null)
            {
                return [..owned];
            }

            return [owning, ..owned];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedAnnotatingRelationship = ownedRelationship-&gt;
        ///                             selectByKind(Annotation)-&gt;
        ///                             select(a | a.annotatedElement &lt;&gt; self)
        /// </code>
        /// </remarks>
        /// <param name="annotatingElementSubject">
        /// The subject <see cref="IAnnotatingElement" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IAnnotation> ComputeOwnedAnnotatingRelationship(this IAnnotatingElement annotatingElementSubject)
        {
            if (annotatingElementSubject == null)
            {
                throw new ArgumentNullException(nameof(annotatingElementSubject));
            }

            var result = new List<IAnnotation>();

            foreach (var relationship in annotatingElementSubject.OwnedRelationship)
            {
                if (relationship is IAnnotation annotation
                    && !ReferenceEquals(annotation.AnnotatedElement, annotatingElementSubject))
                {
                    result.Add(annotation);
                }
            }

            return result;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="annotatingElementSubject">
        /// The subject <see cref="IAnnotatingElement" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IAnnotation ComputeOwningAnnotatingRelationship(this IAnnotatingElement annotatingElementSubject)
        {
            return annotatingElementSubject == null
                ? throw new ArgumentNullException(nameof(annotatingElementSubject))
                : annotatingElementSubject.OwningRelationship as IAnnotation;
        }
    }
}
