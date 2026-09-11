// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Core.Features
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="FeatureExtensions"/> class provides extensions methods for
    /// the <see cref="IFeature"/> interface
    /// </summary>
    internal static class FeatureExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// chainingFeature = ownedFeatureChaining.chainingFeature
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IFeature.chainingFeature))]
        internal static List<IFeature> ComputeChainingFeature(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// crossFeature =
        ///     if ownedCrossSubsetting = null then null
        ///     else
        ///         let chainingFeatures: Sequence(Feature) =
        ///             ownedCrossSubsetting.crossedFeature.chainingFeature in
        ///         if chainingFeatures-&gt;size() &lt; 2 then null
        ///         else chainingFeatures-&gt;at(2)
        ///     endif
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IFeature.crossFeature))]
        internal static IFeature ComputeCrossFeature(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IFeature.endOwningType))]
        internal static IType ComputeEndOwningType(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// featureTarget = if chainingFeature-&gt;isEmpty() then self else chainingFeature-&gt;last() endif
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IFeature.featureTarget))]
        internal static IFeature ComputeFeatureTarget(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// featuringType =
        ///     let featuringTypes : OrderedSet(Type) =
        ///         typeFeaturing.type-&gt;asOrderedSet() in
        ///     if chainingFeature-&gt;isEmpty() then featuringTypes
        ///     else
        ///         featuringTypes-&gt;
        ///             union(chainingFeature-&gt;first().featuringType)-&gt;
        ///             asOrderedSet()
        ///     endif
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IFeature.featuringType))]
        internal static List<IType> ComputeFeaturingType(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedCrossSubsetting =
        ///     let crossSubsettings: Sequence(CrossSubsetting) =
        ///         ownedSubsetting-&gt;selectByKind(CrossSubsetting) in
        ///     if crossSubsettings-&gt;isEmpty() then null
        ///     else crossSubsettings-&gt;first()
        ///     endif
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IFeature.ownedCrossSubsetting))]
        internal static ICrossSubsetting ComputeOwnedCrossSubsetting(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedFeatureChaining = ownedRelationship-&gt;selectByKind(FeatureChaining)
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IFeature.ownedFeatureChaining))]
        internal static List<IFeatureChaining> ComputeOwnedFeatureChaining(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedFeatureInverting = ownedRelationship-&gt;selectByKind(FeatureInverting)-&gt;
        ///     select(fi | fi.featureInverted = self)
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IFeature.ownedFeatureInverting))]
        internal static List<IFeatureInverting> ComputeOwnedFeatureInverting(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedRedefinition = ownedSubsetting-&gt;selectByKind(Redefinition)
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IFeature.ownedRedefinition))]
        internal static List<IRedefinition> ComputeOwnedRedefinition(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedReferenceSubsetting =
        ///     let referenceSubsettings : OrderedSet(ReferenceSubsetting) =
        ///         ownedSubsetting-&gt;selectByKind(ReferenceSubsetting) in
        ///     if referenceSubsettings-&gt;isEmpty() then null
        ///     else referenceSubsettings-&gt;first() endif
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IFeature.ownedReferenceSubsetting))]
        internal static IReferenceSubsetting ComputeOwnedReferenceSubsetting(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedSubsetting = ownedSpecialization-&gt;selectByKind(Subsetting)
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IFeature.ownedSubsetting))]
        internal static List<ISubsetting> ComputeOwnedSubsetting(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedTypeFeaturing = ownedRelationship-&gt;selectByKind(TypeFeaturing)-&gt;
        ///     select(tf | tf.featureOfType = self)
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IFeature.ownedTypeFeaturing))]
        internal static List<ITypeFeaturing> ComputeOwnedTypeFeaturing(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedTyping = ownedGeneralization-&gt;selectByKind(FeatureTyping)
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IFeature.ownedTyping))]
        internal static List<IFeatureTyping> ComputeOwnedTyping(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IFeature.owningFeatureMembership))]
        internal static IFeatureMembership ComputeOwningFeatureMembership(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IFeature.owningType))]
        internal static IType ComputeOwningType(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// type =
        ///     let types : OrderedSet(Types) = OrderedSet{self}-&gt;
        ///         -- Note: The closure operation automatically handles circular relationships.
        ///         closure(typingFeatures()).typing.type-&gt;asOrderedSet() in
        ///     types-&gt;reject(t1 | types-&gt;exist(t2 | t2 &lt;&gt; t1 and t2.specializes(t1)))
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(IFeature.type))]
        internal static List<IType> ComputeType(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Return the directionOf this Feature relative to the given type.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// type.directionOf(self)
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <param name="type">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="FeatureDirectionKind" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IFeature.DirectionFor))]
        internal static FeatureDirectionKind? ComputeDirectionForOperation(this IFeature featureSubject, IType type)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// If a Feature has no declaredShortName or declaredName, then its effective shortName is given by the
        /// effective shortName of the Feature returned by the namingFeature() operation, if any.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if declaredShortName &lt;&gt; null or declaredName &lt;&gt; null then
        ///     declaredShortName
        /// else
        ///     let namingFeature : Feature = namingFeature() in
        ///     if namingFeature = null then
        ///         null
        ///     else
        ///         namingFeature.effectiveShortName()
        ///     endif
        /// endif
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IFeature.EffectiveShortName))]
        internal static string ComputeRedefinedEffectiveShortNameOperation(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// If a Feature has no declaredName or declaredShortName, then its effective name is given by the
        /// effective name of the Feature returned by the namingFeature() operation, if any.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if declaredShortName &lt;&gt; null or declaredName &lt;&gt; null then
        ///     declaredName
        /// else
        ///     let namingFeature : Feature = namingFeature() in
        ///     if namingFeature = null then
        ///         null
        ///     else
        ///         namingFeature.effectiveName()
        ///     endif
        /// endif
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IFeature.EffectiveName))]
        internal static string ComputeRedefinedEffectiveNameOperation(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// By default, the naming Feature of a Feature is given by its first redefinedFeature of its first
        /// ownedRedefinition, if any.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if ownedRedefinition-&gt;isEmpty() then
        ///     null
        /// else
        ///     ownedRedefinition-&gt;at(1).redefinedFeature
        /// endif
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="IFeature" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IFeature.NamingFeature))]
        internal static IFeature ComputeNamingFeatureOperation(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// let supertypes : OrderedSet(Type) =
        ///     self.oclAsType(Type).supertypes(excludeImplied) in
        /// if featureTarget = self then supertypes
        /// else supertypes-&gt;append(featureTarget)
        /// endif
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <param name="excludeImplied">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IType" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IFeature.Supertypes))]
        internal static List<IType> ComputeRedefinedSupertypesOperation(this IFeature featureSubject, bool excludeImplied)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Check whether this Feature directly redefines the given redefinedFeature.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedRedefinition.redefinedFeature-&gt;includes(redefinedFeature)
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <param name="redefinedFeature">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IFeature.Redefines))]
        internal static bool ComputeRedefinesOperation(this IFeature featureSubject, IFeature redefinedFeature)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Check whether this Feature directly redefines the named library Feature. libraryFeatureName must
        /// conform to the syntax of a KerML qualified name and must resolve to a Feature in global scope.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// let mem: Membership = resolveGlobal(libraryFeatureName) in
        /// mem &lt;&gt; null and mem.memberElement.oclIsKindOf(Feature) and
        /// redefines(mem.memberElement.oclAsType(Feature))
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <param name="libraryFeatureName">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IFeature.RedefinesFromLibrary))]
        internal static bool ComputeRedefinesFromLibraryOperation(this IFeature featureSubject, string libraryFeatureName)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Check whether this Feature directly or indirectly specializes a Feature whose last two
        /// chainingFeatures are the given Features first and second.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// allSuperTypes()-&gt;selectAsKind(Feature)-&gt;
        ///     exists(f | let n: Integer = f.chainingFeature-&gt;size() in
        ///         n &gt;= 2 and
        ///         f.chainingFeature-&gt;at(n-1) = first and
        ///         f.chainingFeature-&gt;at(n) = second)
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <param name="first">
        /// No documentation provided
        /// </param>
        /// <param name="second">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IFeature.SubsetsChain))]
        internal static bool ComputeSubsetsChainOperation(this IFeature featureSubject, IFeature first, IFeature second)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// A Feature is compatible with an otherType if it either directly or indirectly specializes the
        /// otherType or if the otherType is also a Feature and all of the following are true.<ol>	<li>Neither
        /// this Feature or the otherType have any ownedFeatures.</li>	<li>This Feature directly or indirectly
        /// redefines a Feature that is also directly or indirectly redefined by the otherType.</li>	<li>This
        /// Feature can access the otherType.</li></ol>
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// specializes(otherType) or
        ///     supertype.oclIsKindOf(Feature) and
        ///     ownedFeature-&gt;isEmpty() and
        ///     otherType.ownedFeature-&gt;isEmpty() and
        ///     ownedRedefinitions.allRedefinedFeatures()-&gt;exists(f |
        ///         otherType.oclAsType(Feature).allRedefinedFeatures()-&gt;includes(f)) and
        ///     canAccess(otherType.oclAsType(Feature))
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <param name="otherType">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IFeature.IsCompatibleWith))]
        internal static bool ComputeRedefinedIsCompatibleWithOperation(this IFeature featureSubject, IType otherType)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Return the Features used to determine the types of this Feature (other than this Feature itself). If
        /// this Feature is not conjugated, then the typingFeatures consist of all subsetted Features, except
        /// from CrossSubsetting, and the last chainingFeature (if any). If this Feature is conjugated, then the
        /// typingFeatures are only its originalType (if the originalType is a Feature).<strong>Note.</strong>
        /// CrossSubsetting is excluded from the determination of the type of a Feature in order to avoid
        /// circularity in the construction of implied CrossSubsetting relationships. The
        /// validateFeatureCrossFeatureType requires that the crossFeature of a Feature have the same type as
        /// the Feature.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if not isConjugated then
        ///     let subsettedFeatures : OrderedSet(Feature) =
        ///         subsetting-&gt;reject(s | s.oclIsKindOf(CrossSubsetting)).subsettedFeatures in
        ///     if chainingFeature-&gt;isEmpty() or
        ///        subsettedFeature-&gt;includes(chainingFeature-&gt;last())
        ///     then subsettedFeatures
        ///     else subsettedFeatures-&gt;append(chainingFeature-&gt;last())
        ///     endif
        /// else if conjugator.originalType.oclIsKindOf(Feature) then
        ///     OrderedSet{conjugator.originalType.oclAsType(Feature)}
        /// else OrderedSet{}
        /// endif endif
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IFeature" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IFeature.TypingFeatures))]
        internal static List<IFeature> ComputeTypingFeaturesOperation(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// If isCartesianProduct is true, then return the list of Types whose Cartesian product can be
        /// represented by this Feature. (If isCartesianProduct is not true, the operation will still return a
        /// valid value, it will just not represent anything useful.)
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// featuringType-&gt;select(t | t.owner &lt;&gt; self)-&gt;
        ///     union(featuringType-&gt;select(t | t.owner = self)-&gt;
        ///         selectByKind(Feature).asCartesianProduct())-&gt;
        ///     union(type)
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IType" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IFeature.AsCartesianProduct))]
        internal static List<IType> ComputeAsCartesianProductOperation(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Check whether this Feature can be used to represent a Cartesian product of Types.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// type-&gt;size() = 1 and
        /// featuringType.size() = 1 and
        /// (featuringType.first().owner = self implies
        ///     featuringType.first().oclIsKindOf(Feature) and
        ///     featuringType.first().oclAsType(Feature).isCartesianProduct())
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IFeature.IsCartesianProduct))]
        internal static bool ComputeIsCartesianProductOperation(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Return whether this Feature is an owned cross Feature of an end Feature.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// owningNamespace &lt;&gt; null and
        /// owningNamespace.oclIsKindOf(Feature) and
        /// owningNamespace.oclAsType(Feature).ownedCrossFeature() = self
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IFeature.IsOwnedCrossFeature))]
        internal static bool ComputeIsOwnedCrossFeatureOperation(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// If this Feature is an end Feature of its owningType, then return the first ownedMember of the
        /// Feature that is a Feature, but not a Multiplicity or a MetadataFeature, and whose owningMembership
        /// is not a FeatureMembership. If this exists, it is the crossFeature of the end Feature.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if not isEnd or owningType = null then null
        /// else
        ///     let ownedMemberFeatures: Sequence(Feature) =
        ///         ownedMember-&gt;selectByKind(Feature)-&gt;
        ///             reject(oclIsKindOf(Multiplicity) or
        ///                    oclIsKindOf(MetadataFeature) or
        ///                    oclIsKindOf(FeatureValue))-&gt;
        ///             reject(owningMembership.oclIsKindOf(FeatureMembership)) in
        ///     if ownedMemberFeatures.isEmpty() then null
        ///     else ownedMemberFeatures-&gt;first()
        ///     endif
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="IFeature" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IFeature.OwnedCrossFeature))]
        internal static IFeature ComputeOwnedCrossFeatureOperation(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Return this Feature and all the Features that are directly or indirectly Redefined by this Feature.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedRedefinition.redefinedFeature-&gt;
        ///     closure(ownedRedefinition.redefinedFeature)-&gt;
        ///     asOrderedSet()-&gt;prepend(self)
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IFeature" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IFeature.AllRedefinedFeatures))]
        internal static List<IFeature> ComputeAllRedefinedFeaturesOperation(this IFeature featureSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Return if the featuringTypes of this Feature are compatible with the given type. If type is null,
        /// then check if this Feature is explicitly or implicitly featured by Base::Anything. If this Feature
        /// has isVariable = true, then also consider it to be featured within its owningType. If this Feature
        /// is a feature chain whose first chainingFeature has isVariable = true, then also consider it to be
        /// featured within the owningType of its first chainingFeature.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if type = null then
        ///     featuringType-&gt;forAll(f | f = resolveGlobal('Base::Anything').memberElement)
        /// else
        ///     featuringType-&gt;forAll(f | type.isCompatibleWith(f)) or
        ///     isVariable and type.specializes(owningType) or
        ///     chainingFeature-&gt;notEmpty() and chainingFeature-&gt;first().isVariable and
        ///         type.specializes(chainingFeature-&gt;first().owningType)
        /// endif
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <param name="type">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IFeature.IsFeaturedWithin))]
        internal static bool ComputeIsFeaturedWithinOperation(this IFeature featureSubject, IType type)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// A Feature can access another feature if the other feature is featured within one of the direct or
        /// indirect featuringTypes of this Feature.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// let anythingType: Element =
        ///     subsettingFeature.resolveGlobal('Base::Anything').memberElement in
        /// let allFeaturingTypes : Sequence(Type) =
        ///     featuringTypes-&gt;closure(t |
        ///         if not t.oclIsKindOf(Feature) then Sequence{}
        ///         else
        ///             let featuringTypes : OrderedSet(Type) = t.oclAsType(Feature).featuringType in
        ///             if featuringTypes-&gt;isEmpty() then Sequence{anythingType}
        ///             else featuringTypes
        ///             endif
        ///         endif) in
        /// allFeaturingTypes-&gt;exists(t | feature.isFeaturedWithin(t))
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <param name="feature">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IFeature.CanAccess))]
        internal static bool ComputeCanAccessOperation(this IFeature featureSubject, IFeature feature)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Return whether the given type must be a featuringType of this Feature. If this Feature has
        /// isVariable = false, then return true if the type is the owningType of the Feature. If isVariable =
        /// true, then return true if the type is a Feature representing the snapshots of the owningType of this
        /// Feature.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// owningType &lt;&gt; null and
        /// if not isVariable then type = owningType
        /// else if owningType = resolveGlobal('Occurrences::Occurrence').memberElement then
        ///     type = resolveGlobal('Occurrences::Occurrence::snapshots').memberElement
        /// else
        ///     type.oclIsKindOf(Feature) and
        ///     let feature : Feature = type.oclAsType(Feature) in
        ///     feature.featuringType-&gt;includes(owningType) and
        ///     feature.redefinesFromLibrary('Occurrences::Occurrence::snapshots')
        /// endif
        /// </code>
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature"/>
        /// </param>
        /// <param name="type">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Operation(name: nameof(IFeature.IsFeaturingType))]
        internal static bool ComputeIsFeaturingTypeOperation(this IFeature featureSubject, IType type)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }
    }
}
