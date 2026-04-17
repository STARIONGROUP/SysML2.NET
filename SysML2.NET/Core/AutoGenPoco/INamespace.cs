// -------------------------------------------------------------------------------------------------
// <copyright file="INamespace.cs" company="Starion Group S.A.">
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Root.Namespaces
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Namespace is an Element that contains other Elements, known as its members, via Membership
    /// Relationships with those Elements. The members of a Namespace may be owned by the Namespace, aliased
    /// in the Namespace, or imported into the Namespace via Import Relationships.                        A
    /// Namespace can provide names for its members via the memberNames and memberShortNames specified by
    /// the Memberships in the Namespace. If a Membership specifies a memberName and/or memberShortName,
    /// then those are names of the corresponding memberElement relative to the Namespace. For an
    /// OwningMembership, the ownedMemberName and ownedMemberShortName are given by the Element name and
    /// shortName. Note that the same Element may be the memberElement of multiple Memberships in a
    /// Namespace (though it may be owned at most once), each of which may define a separate alias for the
    /// Element relative to the Namespace.
    /// </summary>
    [Class(xmiId: "_18_5_3_12e503d9_1533160651694_110063_42176", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface INamespace : IElement
    {
        /// <summary>
        /// The Memberships in this Namespace that result from the ownedImports of this Namespace.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674979_207869_43270", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674962_198288_43183")]
        List<IMembership> importedMembership { get; }

        /// <summary>
        /// The set of all member Elements of this Namespace, which are the memberElements of all memberships of
        /// the Namespace.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674979_644335_43267", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<IElement> member { get; }

        /// <summary>
        /// All Memberships in this Namespace, including (at least) the union of ownedMemberships and
        /// importedMemberships.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674962_198288_43183", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: true, isUnique: true, defaultValue: null)]
        List<IMembership> membership { get; }

        /// <summary>
        /// The ownedRelationships of this Namespace that are Imports, for which the Namespace is the
        /// importOwningNamespace.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674974_746786_43247", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        List<IImport> ownedImport { get; }

        /// <summary>
        /// The owned members of this Namespace, which are the <cpde>ownedMemberElements of the ownedMemberships
        /// of the Namespace.</cpde>
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674979_259543_43268", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_644335_43267")]
        List<IElement> ownedMember { get; }

        /// <summary>
        /// The ownedRelationships of this Namespace that are Memberships, for which the Namespace is the
        /// membershipOwningNamespace.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674979_190614_43269", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674962_198288_43183")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        List<IMembership> ownedMembership { get; }

        /// <summary>
        /// Return the names of the given element as it is known in this Namespace.
        /// </summary>
        /// <param name="element">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="string" />
        /// </returns>
        List<string> NamesOf(IElement element);

        /// <summary>
        /// Returns this visibility of mem relative to this Namespace. If mem is an importedMembership, this is
        /// the visibility of its Import. Otherwise it is the visibility of the Membership itself.
        /// </summary>
        /// <param name="mem">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="VisibilityKind" />
        /// </returns>
        VisibilityKind VisibilityOf(IMembership mem);

        /// <summary>
        /// If includeAll = true, then return all the Memberships of this Namespace. Otherwise, return only the
        /// publicly visible Memberships of this Namespace, including ownedMemberships that have a visibility of
        /// public and Memberships imported with a visibility of public. If isRecursive = true, also recursively
        /// include all visible Memberships of any public owned Namespaces, or, if IncludeAll = true, all
        /// Memberships of all owned Namespaces. When computing imported Memberships, ignore this Namespace and
        /// any Namespaces in the given excluded set.
        /// </summary>
        /// <param name="excluded">
        /// No documentation provided
        /// </param>
        /// <param name="isRecursive">
        /// No documentation provided
        /// </param>
        /// <param name="includeAll">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        List<IMembership> VisibleMemberships(List<INamespace> excluded, bool isRecursive, bool includeAll);

        /// <summary>
        /// Derive the imported Memberships of this Namespace as the importedMembership of all ownedImports,
        /// excluding those Imports whose importOwningNamespace is in the excluded set, and excluding
        /// Memberships that have distinguisibility collisions with each other or with any ownedMembership.
        /// </summary>
        /// <param name="excluded">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        List<IMembership> ImportedMemberships(List<INamespace> excluded);

        /// <summary>
        /// If visibility is not null, return the Memberships of this Namespace with the given visibility,
        /// including ownedMemberships with the given visibility and Memberships imported with the given
        /// visibility. If visibility is null, return all ownedMemberships and imported Memberships regardless
        /// of visibility. When computing imported Memberships, ignore this Namespace and any Namespaces in the
        /// given excluded set.
        /// </summary>
        /// <param name="visibility">
        /// No documentation provided
        /// </param>
        /// <param name="excluded">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        List<IMembership> MembershipsOfVisibility(VisibilityKind? visibility, List<INamespace> excluded);

        /// <summary>
        /// Resolve the given qualified name to the named Membership (if any), starting with this Namespace as
        /// the local scope. The qualified name string must conform to the concrete syntax of the KerML textual
        /// notation. According to the KerML name resolution rules every qualified name will resolve to either a
        /// single Membership, or to none.
        /// </summary>
        /// <param name="qualifiedName">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="IMembership" />
        /// </returns>
        IMembership Resolve(string qualifiedName);

        /// <summary>
        /// Resolve the given qualified name to the named Membership (if any) in the effective global Namespace
        /// that is the outermost naming scope. The qualified name string must conform to the concrete syntax of
        /// the KerML textual notation.
        /// </summary>
        /// <param name="qualifiedName">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="IMembership" />
        /// </returns>
        IMembership ResolveGlobal(string qualifiedName);

        /// <summary>
        /// Resolve a simple name starting with this Namespace as the local scope, and continuing with
        /// containing outer scopes as necessary. However, if this Namespace is a root Namespace, then the
        /// resolution is done directly in global scope.
        /// </summary>
        /// <param name="name">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="IMembership" />
        /// </returns>
        IMembership ResolveLocal(string name);

        /// <summary>
        /// Resolve a simple name from the visible Memberships of this Namespace.
        /// </summary>
        /// <param name="name">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="IMembership" />
        /// </returns>
        IMembership ResolveVisible(string name);

        /// <summary>
        /// Return a string with valid KerML syntax representing the qualification part of a given
        /// qualifiedName, that is, a qualified name with all the segment names of the given name except the
        /// last. If the given qualifiedName has only one segment, then return null.
        /// </summary>
        /// <param name="qualifiedName">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        string QualificationOf(string qualifiedName);

        /// <summary>
        /// Return the simple name that is the last segment name of the given qualifiedName. If this segment
        /// name has the form of a KerML unrestricted name, then "unescape" it by removing the surrounding
        /// single quotes and replacing all escape sequences with the specified character.
        /// </summary>
        /// <param name="qualifiedName">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        string UnqualifiedNameOf(string qualifiedName);
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
