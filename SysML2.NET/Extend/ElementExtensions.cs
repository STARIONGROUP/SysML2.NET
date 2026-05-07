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
    using System.Linq;

    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

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
        /// OCL (KerML XMI):
        /// <code>
        /// documentation = ownedElement-&gt;selectByKind(Documentation)
        /// </code>
        /// The documentation of an Element is its ownedElements that are Documentation.
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IDocumentation> ComputeDocumentation(this IElement elementSubject)
        {
            return elementSubject == null ?  throw new ArgumentNullException(nameof(elementSubject)) : [..elementSubject.ownedElement.OfType<IDocumentation>()];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// isLibraryElement = libraryNamespace() &lt;&gt; null
        /// </code>
        /// An Element isLibraryElement if libraryNamespace() is not null.
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static bool ComputeIsLibraryElement(this IElement elementSubject)
        {
            return elementSubject == null ?  throw new ArgumentNullException(nameof(elementSubject)) : elementSubject.LibraryNamespace() != null;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// name = effectiveName()
        /// </code>
        /// The name of an Element is given by the result of the effectiveName() operation. By default, it is the same as the declaredName, but this is overridden for certain kinds of Elements to compute a name even when the declaredName is null.
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static string ComputeName(this IElement elementSubject)
        {
            return elementSubject == null ? throw new ArgumentNullException(nameof(elementSubject)) : elementSubject.EffectiveName();
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// ownedAnnotation = ownedRelationship-&gt;
        ///     selectByKind(Annotation)-&gt;
        ///     select(a | a.annotatedElement = self)
        /// </code>
        /// The ownedAnnotations of an Element are its ownedRelationships that are Annotations, for which the Element is the annotatedElement.
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IAnnotation> ComputeOwnedAnnotation(this IElement elementSubject)
        {
            return elementSubject == null ? throw new ArgumentNullException(nameof(elementSubject)) : [..elementSubject.OwnedRelationship.OfType<IAnnotation>().Where(x => x.AnnotatedElement == elementSubject)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// ownedElement = ownedRelationship.ownedRelatedElement
        /// </code>
        /// The ownedElements of an Element are the ownedRelatedElements of its ownedRelationships.
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IElement> ComputeOwnedElement(this IElement elementSubject)
        {
            return elementSubject == null ? throw new ArgumentNullException(nameof(elementSubject)) : [..elementSubject.OwnedRelationship.SelectMany(x => x.OwnedRelatedElement)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// owner = owningRelationship.owningRelatedElement
        /// </code>
        /// The owner of an Element is the owningRelatedElement of its owningRelationship.
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IElement ComputeOwner(this IElement elementSubject)
        {
            return elementSubject == null ? throw new ArgumentNullException(nameof(elementSubject)) :elementSubject.OwningRelationship?.OwningRelatedElement;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// No explicit OCL derivation rule in XMI. Derived from UML association semantics:
        /// The owningRelationship of this Element, if that Relationship is a Membership. Since owningMembership subsets owningRelationship with type OwningMembership, its value is the owningRelationship when that relationship is an OwningMembership, otherwise null.
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IOwningMembership ComputeOwningMembership(this IElement elementSubject)
        {
            return elementSubject == null ? throw new ArgumentNullException(nameof(elementSubject)) : elementSubject.OwningRelationship as IOwningMembership;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// owningNamespace =
        ///     if owningMembership = null then null
        ///     else owningMembership.membershipOwningNamespace
        ///     endif
        /// </code>
        /// The owningNamespace of an Element is the membershipOwningNamespace of its owningMembership (if any).
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static INamespace ComputeOwningNamespace(this IElement elementSubject)
        {
            return elementSubject == null ? throw new ArgumentNullException(nameof(elementSubject)) : elementSubject.owningMembership?.membershipOwningNamespace;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// qualifiedName =
        ///     if owningNamespace = null then null
        ///     else if name &lt;&gt; null and
        ///     owningNamespace.ownedMember-&gt;
        ///     select(m | m.name = name).indexOf(self) &lt;&gt; 1 then null
        ///     else if owningNamespace.owner = null then escapedName()
        ///     else if owningNamespace.qualifiedName = null or
        ///     escapedName() = null then null
        ///     else owningNamespace.qualifiedName + '::' + escapedName()
        ///     endif endif endif endif
        /// </code>
        /// If this Element does not have an owningNamespace, then its qualifiedName is null. If the owningNamespace of this Element is a root Namespace, then the qualifiedName of the Element is the escaped name of the Element (if any). If the owningNamespace is non-null but not a root Namespace, then the qualifiedName of this Element is constructed from the qualifiedName of the owningNamespace and the escaped name of the Element, unless the qualifiedName of the owningNamespace is null or the escaped name is null, in which case the qualifiedName of this Element is also null. Further, if the owningNamespace has other ownedMembers with the same non-null name as this Element, and this Element is not the first, then the qualifiedName of this Element is null.
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static string ComputeQualifiedName(this IElement elementSubject)
        {
            if (elementSubject == null)
            {
                throw new ArgumentNullException(nameof(elementSubject));
            }

            if (elementSubject.owningNamespace == null)
            {
                return null;
            }

            if (elementSubject.name != null)
            {
                var membersWithTheName = elementSubject.owningNamespace.ownedMember.Where(x => x.name == elementSubject.name).ToList();

                if (membersWithTheName.IndexOf(elementSubject) != 0)
                {
                    return null;
                }
            }

            if (elementSubject.owningNamespace.owner == null)
            {
                return elementSubject.EscapedName();
            }
            
            var parentQualifiedName = elementSubject.owningNamespace.qualifiedName;
            var currentEscaped = elementSubject.EscapedName();

            if (string.IsNullOrWhiteSpace(parentQualifiedName) || string.IsNullOrWhiteSpace(currentEscaped))
            {
                return null;
            }

            return $"{parentQualifiedName}::{currentEscaped}";
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// shortName = effectiveShortName()
        /// </code>
        /// The shortName of an Element is given by the result of the effectiveShortName() operation. By default, it is the same as the declaredShortName, but this is overridden for certain kinds of Elements to compute a shortName even when the declaredName is null.
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static string ComputeShortName(this IElement elementSubject)
        {
            return elementSubject == null ? throw new ArgumentNullException(nameof(elementSubject)) : elementSubject.EffectiveShortName();
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// textualRepresentation = ownedElement-&gt;selectByKind(TextualRepresentation)
        /// </code>
        /// The textualRepresentations of an Element are its ownedElements that are TextualRepresentations.
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<ITextualRepresentation> ComputeTextualRepresentation(this IElement elementSubject)
        {
            return elementSubject == null ? throw new ArgumentNullException(nameof(elementSubject)) : [..elementSubject.ownedElement.OfType<ITextualRepresentation>()];
        }

        /// <summary>
        /// Validates the constraint that if an Element has any ownedRelationships for which isImplied = true,
        /// then the Element must also have isImpliedIncluded = true.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// ownedRelationship-&gt;exists(isImplied) implies isImpliedIncluded
        /// </code>
        /// If an Element has any ownedRelationships for which isImplied = true, then the Element must also have isImpliedIncluded = true. (Note that an Element can have isImplied = true even if no ownedRelationships have isImplied = true, indicating the Element simply has no implied Relationships.)
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// true if the constraint is satisfied; false otherwise
        /// </returns>
        internal static bool ValidateIsImpliedIncluded(this IElement elementSubject)
        {
            if (elementSubject == null)
            {
                throw new ArgumentNullException(nameof(elementSubject));
            }

            return elementSubject.OwnedRelationship.Count == 0
                || !elementSubject.OwnedRelationship.Any(r => r.IsImplied)
                || elementSubject.IsImpliedIncluded;
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
        internal static string ComputeEscapedNameOperation(this IElement elementSubject)
        {
            if (elementSubject == null)
            {
                throw new ArgumentNullException(nameof(elementSubject));
            }

            var targetName = elementSubject.name;

            if (string.IsNullOrWhiteSpace(targetName))
            {
                targetName = elementSubject.shortName;
            }

            if (string.IsNullOrWhiteSpace(targetName))
            {
                return null;
            }

            return targetName.QueryIsValidBasicName() ? targetName : targetName.ToUnrestrictedName();
        }

        /// <summary>
        /// Return an effective shortName for this Element. By default this is the same as its
        /// declaredShortName.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// declaredShortName
        /// </code>
        /// Return an effective shortName for this Element. By default this is the same as its declaredShortName. (Note: this operation is redefined on Feature to also consider the naming feature when declaredShortName and declaredName are both null.)
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        internal static string ComputeEffectiveShortNameOperation(this IElement elementSubject)
        {
            return elementSubject == null ? throw new ArgumentNullException(nameof(elementSubject)) : elementSubject.DeclaredShortName;
        }

        /// <summary>
        /// Return an effective name for this Element. By default this is the same as its declaredName.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// declaredName
        /// </code>
        /// Return an effective name for this Element. By default this is the same as its declaredName. (Note: this operation is redefined on Feature to also consider the naming feature when declaredShortName and declaredName are both null.)
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        internal static string ComputeEffectiveNameOperation(this IElement elementSubject)
        {
            return elementSubject == null ? throw new ArgumentNullException(nameof(elementSubject)) : elementSubject.DeclaredName;
        }

        /// <summary>
        /// By default, return the library Namespace of the owningRelationship of this Element, if it has one.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// if owningRelationship &lt;&gt; null then owningRelationship.libraryNamespace()
        /// else null endif
        /// </code>
        /// By default, return the library Namespace of the owningRelationship of this Element, if it has one. (Note: this operation is redefined on Relationship to also check owningRelatedElement, and on LibraryPackage to return itself.)
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="INamespace" />
        /// </returns>
        internal static INamespace ComputeLibraryNamespaceOperation(this IElement elementSubject)
        {
            return elementSubject == null ? throw new ArgumentNullException(nameof(elementSubject)) : elementSubject.OwningRelationship?.LibraryNamespace();
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
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// if qualifiedName &lt;&gt; null then qualifiedName
        /// else if owningRelationship &lt;&gt; null then
        /// owningRelationship.path() + '/' +
        /// owningRelationship.ownedRelatedElement-&gt;indexOf(self).toString()
        /// else ''
        /// endif endif
        /// </code>
        /// Return a unique description of the location of this Element in the containment structure rooted in a root Namespace. If the Element has a non-null qualifiedName, then return that. Otherwise, if it has an owningRelationship, then return the string constructed by appending to the path of its owningRelationship the character / followed by the string representation of its position in the list of ownedRelatedElements of the owningRelationship (indexed starting at 1). Otherwise, return the empty string. (Note that this operation is overridden for Relationships to use owningRelatedElement when appropriate.)
        /// </remarks>
        /// <param name="elementSubject">
        /// The subject <see cref="IElement"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        internal static string ComputePathOperation(this IElement elementSubject)
        {
            if (elementSubject == null)
            {
                throw new ArgumentNullException(nameof(elementSubject));
            }

            var qualifiedName = elementSubject.qualifiedName;

            if (!string.IsNullOrWhiteSpace(qualifiedName))
            {
                return qualifiedName;
            }

            if (elementSubject.OwningRelationship == null)
            {
                return string.Empty;
            }
            
            var ownedRelatedElementsIndex =  elementSubject.OwningRelationship.OwnedRelatedElement.ToList().IndexOf(elementSubject) +1;
            var parentPath = elementSubject.OwningRelationship.Path();
            
            return $"{parentPath}/{ownedRelatedElementsIndex}";
        }
    }
}
