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

    using SysML2.NET.Decorators;

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
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// documentation = ownedElement-&gt;selectByKind(Documentation)
        /// </code>
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IElement.documentation))]
        internal static List<IDocumentation> ComputeDocumentation(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// isLibraryElement = libraryNamespace() &lt;&gt; null
        /// </code>
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IElement.isLibraryElement))]
        internal static bool ComputeIsLibraryElement(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// name = effectiveName()
        /// </code>
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IElement.name))]
        internal static string ComputeName(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedAnnotation = ownedRelationship-&gt;
        ///     selectByKind(Annotation)-&gt;
        ///     select(a | a.annotatedElement = self)
        /// </code>
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IElement.ownedAnnotation))]
        internal static List<IAnnotation> ComputeOwnedAnnotation(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedElement = ownedRelationship.ownedRelatedElement
        /// </code>
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IElement.ownedElement))]
        internal static List<IElement> ComputeOwnedElement(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// owner = owningRelationship.owningRelatedElement
        /// </code>
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IElement.owner))]
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
        [DerivedProperty(name: nameof(IElement.owningMembership))]
        internal static IOwningMembership ComputeOwningMembership(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// owningNamespace =
        ///     if owningMembership = null then null
        ///     else owningMembership.membershipOwningNamespace
        ///     endif
        /// </code>
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IElement.owningNamespace))]
        internal static INamespace ComputeOwningNamespace(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// qualifiedName =
        ///     if owningNamespace = null then null
        ///     else if name &lt;&gt; null and
        ///         owningNamespace.ownedMember-&gt;
        ///         select(m | m.name = name).indexOf(self) &lt;&gt; 1 then null
        ///     else if owningNamespace.owner = null then escapedName()
        ///     else if owningNamespace.qualifiedName = null or
        ///             escapedName() = null then null
        ///     else owningNamespace.qualifiedName + '::' + escapedName()
        ///     endif endif endif endif
        /// </code>
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IElement.qualifiedName))]
        internal static string ComputeQualifiedName(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// shortName = effectiveShortName()
        /// </code>
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IElement.shortName))]
        internal static string ComputeShortName(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// textualRepresentation = ownedElement-&gt;selectByKind(TextualRepresentation)
        /// </code>
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IElement.textualRepresentation))]
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
        [Operation(name: nameof(IElement.EscapedName))]
        internal static string ComputeEscapedNameOperation(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Return an effective shortName for this Element. By default this is the same as its
        /// declaredShortName.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// declaredShortName
        /// </code>
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IElement.EffectiveShortName))]
        internal static string ComputeEffectiveShortNameOperation(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Return an effective name for this Element. By default this is the same as its declaredName.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// declaredName
        /// </code>
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IElement.EffectiveName))]
        internal static string ComputeEffectiveNameOperation(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// By default, return the library Namespace of the owningRelationship of this Element, if it has one.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if owningRelationship &lt;&gt; null then owningRelationship.libraryNamespace()
        /// else null endif
        /// </code>
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="INamespace" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IElement.LibraryNamespace))]
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
        /// empty string.(Note that this operation is overridden for Relationships to use owningRelatedElement
        /// when appropriate.)
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if qualifiedName &lt;&gt; null then qualifiedName
        /// else if owningRelationship &lt;&gt; null then
        ///     owningRelationship.path() + '/' +
        ///     owningRelationship.ownedRelatedElement-&gt;indexOf(self).toString()
        ///     -- A position index shall be converted to a decimal string representation
        ///     -- consisting of only decimal digits, with no sign, leading zeros or leading
        ///     -- or trailing whitespace.
        /// else ''
        /// endif endif
        /// </code>
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IElement.Path))]
        internal static string ComputePathOperation(this IElement elementSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }
    }
}
