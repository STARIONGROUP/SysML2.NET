// -------------------------------------------------------------------------------------------------
// <copyright file="ElementExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Root.Elements
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="ElementExtensions"/> class provides extensions methods for
    /// the <see cref="IElement"/> interface
    /// </summary>
    internal static class ElementExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IDocumentation> ComputeDocumentation(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static bool ComputeIsLibraryElement(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static string ComputeName(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IAnnotation> ComputeOwnedAnnotation(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IElement> ComputeOwnedElement(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IElement ComputeOwner(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IOwningMembership ComputeOwningMembership(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static INamespace ComputeOwningNamespace(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static string ComputeQualifiedName(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static string ComputeShortName(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<ITextualRepresentation> ComputeTextualRepresentation(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Return name, if that is not null, otherwise the shortName, if that is not null, otherwise null. If
        /// the returned value is non-null, it is returned as-is if it has the form of a basic name, or,
        /// otherwise, represented as a restricted name according to the lexical structure of the KerML textual
        /// notation (i.e., surrounded by single quote characters and with special characters escaped).
        /// </summary>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static string ComputeEscapedNameOperation(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Return an effective shortName for this Element. By default this is the same as its
        /// declaredShortName.
        /// </summary>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static string ComputeEffectiveShortNameOperation(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Return an effective name for this Element. By default this is the same as its declaredName.
        /// </summary>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static string ComputeEffectiveNameOperation(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// By default, return the library Namespace of the owningRelationship of this Element, if it has one.
        /// </summary>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="INamespace" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static INamespace ComputeLibraryNamespaceOperation(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Return a unique description of the location of this Element in the containment structure rooted in a
        /// root Namespace. If the Element has a non-null qualifiedName, then return that. Otherwise, if it has
        /// an owningRelationship, then return the string constructed by appending to the path of it's
        /// owningRelationship the character / followed by the string representation of its position in the list
        /// of ownedRelatedElements of the owningRelationship (indexed starting at 1). Otherwise, return the
        /// empty string.                            (Note that this operation is overridden for Relationships
        /// to use owningRelatedElement when appropriate.)
        /// </summary>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static string ComputePathOperation(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }
    }
}
