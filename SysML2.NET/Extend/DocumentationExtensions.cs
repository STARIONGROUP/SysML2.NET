// ------------------------------------------------------------------------------------------------
// <copyright file="DocumentationExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Root.Annotations
{
    using System;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Exceptions;

    /// <summary>
    /// The <see cref="DocumentationExtensions"/> class provides extensions methods for
    /// the <see cref="IDocumentation"/> interface
    /// </summary>
    internal static class DocumentationExtensions
    {
        /// <summary>
        /// Computes the derived property <c>documentedElement</c>.
        /// </summary>
        /// <remarks>
        /// Documentation is a Comment that specifically documents a documentedElement, which must be its owner.
        /// The <c>documentedElement</c> property redefines <c>annotatedElement</c> with cardinality 1..1 and
        /// subsets <c>owner</c>. The documented element is therefore the owner of the Documentation, accessed
        /// via <c>owner</c> (derived as <c>owningRelationship.owningRelatedElement</c>).
        /// </remarks>
        /// <param name="documentationSubject">
        /// The subject <see cref="IDocumentation"/>
        /// </param>
        /// <returns>
        /// The <see cref="IElement"/> that is documented by this <see cref="IDocumentation"/>
        /// </returns>
        [DerivedProperty(name: nameof(IDocumentation.documentedElement))]
        internal static IElement ComputeDocumentedElement(this IDocumentation documentationSubject)
        {
            if (documentationSubject == null)
            {
                throw new ArgumentNullException(nameof(documentationSubject));
            }

            return documentationSubject.owner
                   ?? throw new IncompleteModelException(
                       $"{nameof(documentationSubject)} must have an owner (its documentedElement)");
        }

    }
}
