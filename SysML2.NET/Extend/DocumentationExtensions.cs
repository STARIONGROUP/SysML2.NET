// -------------------------------------------------------------------------------------------------
// <copyright file="DocumentationExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Root.Annotations
{
    using System;

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
