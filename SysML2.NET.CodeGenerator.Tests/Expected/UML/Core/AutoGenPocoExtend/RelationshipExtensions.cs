// ------------------------------------------------------------------------------------------------
// <copyright file="RelationshipExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Root.Elements
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="RelationshipExtensions"/> class provides extensions methods for
    /// the <see cref="IRelationship"/> interface
    /// </summary>
    internal static class RelationshipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// relatedElement = source-&gt;union(target)
        /// </code>
        /// </remarks>
        /// <param name="relationshipSubject">
        /// The subject <see cref="IRelationship"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IRelationship.relatedElement))]
        internal static List<IElement> ComputeRelatedElement(this IRelationship relationshipSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Return whether this Relationship has either an owningRelatedElement or owningRelationship that is a
        /// library element.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if owningRelatedElement &lt;&gt; null then owningRelatedElement.libraryNamespace()
        /// else if owningRelationship &lt;&gt; null then owningRelationship.libraryNamespace()
        /// else null endif endif
        /// </code>
        /// </remarks>
        /// <param name="relationshipSubject">
        /// The subject <see cref="IRelationship"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="INamespace" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IRelationship.LibraryNamespace))]
        internal static INamespace ComputeRedefinedLibraryNamespaceOperation(this IRelationship relationshipSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// If the owningRelationship of the Relationship is null but its owningRelatedElement is non-null,
        /// construct the path using the position of the Relationship in the list of ownedRelationships of its
        /// owningRelatedElement. Otherwise, return the path of the Relationship as specified for an Element in
        /// general.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if owningRelationship = null and owningRelatedElement &lt;&gt; null then
        ///     owningRelatedElement.path() + '/' +
        ///     owningRelatedElement.ownedRelationship-&gt;indexOf(self).toString()
        ///     -- A position index shall be converted to a decimal string representation
        ///     -- consisting of only decimal digits, with no sign, leading zeros or leading
        ///     -- or trailing whitespace.
        /// else self.oclAsType(Element).path()
        /// endif
        /// </code>
        /// </remarks>
        /// <param name="relationshipSubject">
        /// The subject <see cref="IRelationship"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IRelationship.Path))]
        internal static string ComputeRedefinedPathOperation(this IRelationship relationshipSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }
    }
}
