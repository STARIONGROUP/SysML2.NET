// ------------------------------------------------------------------------------------------------
// <copyright file="AnnotatingElementExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Root.Annotations
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="AnnotatingElementExtensions"/> class provides extensions methods for
    /// the <see cref="IAnnotatingElement"/> interface
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
        ///  if annotation-&gt;notEmpty() then annotation.annotatedElement
        ///  else Sequence{owningNamespace} endif
        /// </code>
        /// </remarks>
        /// <param name="annotatingElementSubject">
        /// The subject <see cref="IAnnotatingElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IAnnotatingElement.annotatedElement))]
        internal static List<IElement> ComputeAnnotatedElement(this IAnnotatingElement annotatingElementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// annotation =
        ///     if owningAnnotatingRelationship = null then ownedAnnotatingRelationship
        ///     else owningAnnotatingRelationship-&gt;prepend(owningAnnotatingRelationship)
        ///     endif
        /// </code>
        /// </remarks>
        /// <param name="annotatingElementSubject">
        /// The subject <see cref="IAnnotatingElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IAnnotatingElement.annotation))]
        internal static List<IAnnotation> ComputeAnnotation(this IAnnotatingElement annotatingElementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedAnnotatingRelationship = ownedRelationship-&gt;
        ///     selectByKind(Annotation)-&gt;
        ///     select(a | a.annotatedElement &lt;&gt; self)
        /// </code>
        /// </remarks>
        /// <param name="annotatingElementSubject">
        /// The subject <see cref="IAnnotatingElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IAnnotatingElement.ownedAnnotatingRelationship))]
        internal static List<IAnnotation> ComputeOwnedAnnotatingRelationship(this IAnnotatingElement annotatingElementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="annotatingElementSubject">
        /// The subject <see cref="IAnnotatingElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IAnnotatingElement.owningAnnotatingRelationship))]
        internal static IAnnotation ComputeOwningAnnotatingRelationship(this IAnnotatingElement annotatingElementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

    }
}
