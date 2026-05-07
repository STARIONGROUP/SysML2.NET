// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureExtensions.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Core.POCO.Core.Features
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Kernel.Metadata;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="FeatureExtensions" /> class provides extensions methods for
    /// the <see cref="IFeature" /> interface
    /// </summary>
    internal static class FeatureExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// chainingFeature = ownedFeatureChaining.chainingFeature
        /// </code>
        /// The chainingFeatures of a Feature are the chainingFeatures of its ownedFeatureChainings.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IFeature> ComputeChainingFeature(this IFeature featureSubject)
        {
            return featureSubject == null
                ? throw new ArgumentNullException(nameof(featureSubject))
                : [..featureSubject.OwnedRelationship.OfType<IFeatureChaining>().Select(fc => fc.ChainingFeature)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// crossFeature =
        ///     if ownedCrossSubsetting = null then null
        ///     else
        ///     let chainingFeatures: Sequence(Feature) =
        ///     ownedCrossSubsetting.crossedFeature.chainingFeature in
        ///     if chainingFeatures-&gt;size() &lt; 2 then null
        ///     else chainingFeatures-&gt;at(2)
        ///     endif
        /// </code>
        /// The crossFeature of a Feature is the second chainingFeature of the crossedFeature of the ownedCrossSubsetting of the Feature, if any.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IFeature ComputeCrossFeature(this IFeature featureSubject)
        {
            if (featureSubject == null)
            {
                throw new ArgumentNullException(nameof(featureSubject));
            }

            var ownedCrossSubsetting = featureSubject.OwnedRelationship.OfType<ICrossSubsetting>().FirstOrDefault();

            var crossedFeature = ownedCrossSubsetting?.CrossedFeature;

            var chainingFeatures = crossedFeature?.chainingFeature;
            return chainingFeatures?.Count >= 2 ? chainingFeatures[1] : null;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// No explicit OCL derivation rule in XMI. Derived from UML association semantics:
        /// endOwningType is the owningType but only when the owningFeatureMembership is an EndFeatureMembership.
        /// Since endOwningType subsets owningType, it must equal owningType when the condition holds, and be null otherwise.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IType ComputeEndOwningType(this IFeature featureSubject)
        {
            return featureSubject == null
                ? throw new ArgumentNullException(nameof(featureSubject))
                : featureSubject.OwningRelationship is IEndFeatureMembership efm
                    ? efm.OwningRelatedElement as IType
                    : null;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// featureTarget = if chainingFeature-&gt;isEmpty() then self else chainingFeature-&gt;last() endif
        /// </code>
        /// If a Feature has no chainingFeatures, then its featureTarget is the Feature itself, otherwise the featureTarget is the last of the chainingFeatures.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IFeature ComputeFeatureTarget(this IFeature featureSubject)
        {
            if (featureSubject == null)
            {
                throw new ArgumentNullException(nameof(featureSubject));
            }

            var chainingFeatures = featureSubject.OwnedRelationship.OfType<IFeatureChaining>().ToList();

            return chainingFeatures.Count == 0
                ? featureSubject
                : chainingFeatures[^1].ChainingFeature;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// featuringType =
        ///     let featuringTypes : OrderedSet(Type) =
        ///     featuring.type-&gt;asOrderedSet() in
        ///     if chainingFeature-&gt;isEmpty() then featuringTypes
        ///     else
        ///     featuringTypes-&gt;
        ///     union(chainingFeature-&gt;first().featuringType)-&gt;
        ///     asOrderedSet()
        ///     endif
        /// </code>
        /// The featuringTypes of a Feature include the featuringTypes of all the typeFeaturings of the Feature.
        /// If the Feature has chainingFeatures, then its featuringTypes also include the featuringTypes of the first chainingFeature.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IType> ComputeFeaturingType(this IFeature featureSubject)
        {
            if (featureSubject == null)
            {
                throw new ArgumentNullException(nameof(featureSubject));
            }

            var featuringTypes = featureSubject.OwnedRelationship
                .OfType<ITypeFeaturing>()
                .Where(tf => tf.FeatureOfType == featureSubject)
                .Select(tf => tf.FeaturingType)
                .ToList();

            var chainingFeatures = featureSubject.OwnedRelationship
                .OfType<IFeatureChaining>()
                .Select(fc => fc.ChainingFeature)
                .ToList();

            if (chainingFeatures.Count > 0)
            {
                featuringTypes = [..featuringTypes.Union(chainingFeatures[0].featuringType)];
            }

            return featuringTypes;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// ownedCrossSubsetting =
        ///     let crossSubsettings: Sequence(CrossSubsetting) =
        ///     ownedSubsetting-&gt;selectByKind(CrossSubsetting) in
        ///     if crossSubsettings-&gt;isEmpty() then null
        ///     else crossSubsettings-&gt;first()
        ///     endif
        /// </code>
        /// The ownedCrossSubsetting of a Feature is the ownedSubsetting that is a CrossSubsetting, if any.
        /// A Feature must have at most one (validated by validateFeatureOwnedCrossSubsetting).
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static ICrossSubsetting ComputeOwnedCrossSubsetting(this IFeature featureSubject)
        {
            return featureSubject == null
                ? throw new ArgumentNullException(nameof(featureSubject))
                : featureSubject.OwnedRelationship.OfType<ICrossSubsetting>().FirstOrDefault();
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// ownedFeatureChaining = ownedRelationship-&gt;selectByKind(FeatureChaining)
        /// </code>
        /// The ownedFeatureChainings of a Feature are the ownedRelationships that are FeatureChainings.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IFeatureChaining> ComputeOwnedFeatureChaining(this IFeature featureSubject)
        {
            return featureSubject == null
                ? throw new ArgumentNullException(nameof(featureSubject))
                : [..featureSubject.OwnedRelationship.OfType<IFeatureChaining>()];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// ownedFeatureInverting = ownedRelationship-&gt;selectByKind(FeatureInverting)-&gt;
        ///     select(fi | fi.featureInverted = self)
        /// </code>
        /// The ownedFeatureInvertings of a Feature are its ownedRelationships that are FeatureInvertings
        /// and for which the Feature is the featureInverted.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IFeatureInverting> ComputeOwnedFeatureInverting(this IFeature featureSubject)
        {
            return featureSubject == null
                ? throw new ArgumentNullException(nameof(featureSubject))
                : [..featureSubject.OwnedRelationship.OfType<IFeatureInverting>().Where(fi => fi.FeatureInverted == featureSubject)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// ownedRedefinition = ownedSubsetting-&gt;selectByKind(Redefinition)
        /// </code>
        /// The ownedRedefinitions of a Feature are its ownedSubsettings that are Redefinitions.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IRedefinition> ComputeOwnedRedefinition(this IFeature featureSubject)
        {
            return featureSubject == null
                ? throw new ArgumentNullException(nameof(featureSubject))
                : [..featureSubject.OwnedRelationship.OfType<IRedefinition>()];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// ownedReferenceSubsetting =
        ///     let referenceSubsettings : OrderedSet(ReferenceSubsetting) =
        ///     ownedSubsetting-&gt;selectByKind(ReferenceSubsetting) in
        ///     if referenceSubsettings-&gt;isEmpty() then null
        ///     else referenceSubsettings-&gt;first() endif
        /// </code>
        /// The ownedReferenceSubsetting of a Feature is the first ownedSubsetting that is a ReferenceSubsetting (if any).
        /// A Feature must have at most one (validated by validateFeatureOwnedReferenceSubsetting).
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IReferenceSubsetting ComputeOwnedReferenceSubsetting(this IFeature featureSubject)
        {
            return featureSubject == null
                ? throw new ArgumentNullException(nameof(featureSubject))
                : featureSubject.OwnedRelationship.OfType<IReferenceSubsetting>().FirstOrDefault();
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// ownedSubsetting = ownedSpecialization-&gt;selectByKind(Subsetting)
        /// </code>
        /// The ownedSubsettings of a Feature are its ownedSpecializations that are Subsettings.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<ISubsetting> ComputeOwnedSubsetting(this IFeature featureSubject)
        {
            return featureSubject == null
                ? throw new ArgumentNullException(nameof(featureSubject))
                : [..featureSubject.OwnedRelationship.OfType<ISubsetting>()];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// ownedTypeFeaturing = ownedRelationship-&gt;selectByKind(TypeFeaturing)-&gt;
        ///     select(tf | tf.featureOfType = self)
        /// </code>
        /// The ownedTypeFeaturings of a Feature are its ownedRelationships that are TypeFeaturings
        /// and which have the Feature as their featureOfType.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<ITypeFeaturing> ComputeOwnedTypeFeaturing(this IFeature featureSubject)
        {
            return featureSubject == null
                ? throw new ArgumentNullException(nameof(featureSubject))
                : [..featureSubject.OwnedRelationship.OfType<ITypeFeaturing>().Where(tf => tf.FeatureOfType == featureSubject)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// ownedTyping = ownedGeneralization-&gt;selectByKind(FeatureTyping)
        /// </code>
        /// The ownedTypings of a Feature are its ownedSpecializations that are FeatureTypings.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IFeatureTyping> ComputeOwnedTyping(this IFeature featureSubject)
        {
            return featureSubject == null
                ? throw new ArgumentNullException(nameof(featureSubject))
                : [..featureSubject.OwnedRelationship.OfType<IFeatureTyping>()];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// No explicit OCL derivation rule in XMI. Derived from UML association semantics:
        /// owningFeatureMembership is the owningRelationship of this Element, if that Relationship is a FeatureMembership.
        /// It subsets owningMembership (which is the owningRelationship if it is a Membership).
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IFeatureMembership ComputeOwningFeatureMembership(this IFeature featureSubject)
        {
            return featureSubject == null
                ? throw new ArgumentNullException(nameof(featureSubject))
                : featureSubject.OwningRelationship as IFeatureMembership;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// No explicit OCL derivation rule in XMI. Derived from UML association semantics:
        /// owningType is the Type that is the owningType of the owningFeatureMembership of this Feature.
        /// Navigate through owningFeatureMembership to its owningType. Subsets typeWithFeature, owningNamespace, featuringType.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IType ComputeOwningType(this IFeature featureSubject)
        {
            return featureSubject == null
                ? throw new ArgumentNullException(nameof(featureSubject))
                : (featureSubject.OwningRelationship as IFeatureMembership)?.OwningRelatedElement as IType;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// type =
        ///     let types : OrderedSet(Types) = OrderedSet{self}-&gt;
        ///     -- Note: The closure operation automatically handles circular relationships.
        ///     closure(typingFeatures()).typing.type-&gt;asOrderedSet() in
        ///     types-&gt;reject(t1 | types-&gt;exist(t2 | t2 &lt;&gt; t1 and t2.specializes(t1)))
        /// </code>
        /// The types of a Feature are the union of the types of its typings and the types of the Features it subsets,
        /// with all redundant supertypes removed. If the Feature has chainingFeatures, then the union also includes the types of the last chainingFeature.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IType> ComputeType(this IFeature featureSubject)
        {
            return featureSubject == null
                ? throw new ArgumentNullException(nameof(featureSubject))
                : [..featureSubject.OwnedRelationship.OfType<IFeatureTyping>().Select(ft => ft.Type).Where(t => t != null)];
        }

        /// <summary>
        /// Return the directionOf this Feature relative to the given type.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// type.directionOf(self)
        /// </code>
        /// Return the directionOf this Feature relative to the given type.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <param name="type">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="FeatureDirectionKind" />
        /// </returns>
        internal static FeatureDirectionKind? ComputeDirectionForOperation(this IFeature featureSubject, IType type)
        {
            return featureSubject == null
                ? throw new ArgumentNullException(nameof(featureSubject))
                : type?.DirectionOf(featureSubject);
        }

        /// <summary>
        /// If a Feature has no declaredShortName or declaredName, then its effective shortName is given by the
        /// effective shortName of the Feature returned by the namingFeature() operation, if any.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
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
        /// If a Feature has no declaredShortName or declaredName, then its effective shortName is given by the
        /// effective shortName of the Feature returned by the namingFeature() operation, if any.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        internal static string ComputeRedefinedEffectiveShortNameOperation(this IFeature featureSubject)
        {
            if (featureSubject == null)
            {
                throw new ArgumentNullException(nameof(featureSubject));
            }

            if (!string.IsNullOrWhiteSpace(featureSubject.DeclaredShortName) || !string.IsNullOrWhiteSpace(featureSubject.DeclaredName))
            {
                return featureSubject.DeclaredShortName;
            }

            var namingFeature = featureSubject.OwnedRelationship.OfType<IRedefinition>().FirstOrDefault()?.RedefinedFeature;

            return namingFeature?.EffectiveShortName();
        }

        /// <summary>
        /// If a Feature has no declaredName or declaredShortName                            , then its
        /// effective name is given by the effective name of the Feature returned by the namingFeature()
        /// operation, if any.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
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
        /// If a Feature has no declaredName or declaredShortName, then its effective name is given by the
        /// effective name of the Feature returned by the namingFeature() operation, if any.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        internal static string ComputeRedefinedEffectiveNameOperation(this IFeature featureSubject)
        {
            if (featureSubject == null)
            {
                throw new ArgumentNullException(nameof(featureSubject));
            }

            if (!string.IsNullOrWhiteSpace(featureSubject.DeclaredShortName) || !string.IsNullOrWhiteSpace(featureSubject.DeclaredName))
            {
                return featureSubject.DeclaredName;
            }

            var namingFeature = featureSubject.OwnedRelationship.OfType<IRedefinition>().FirstOrDefault()?.RedefinedFeature;

            return namingFeature?.EffectiveName();
        }

        /// <summary>
        /// By default, the naming Feature of a Feature is given by its first redefinedFeature of its first
        /// ownedRedefinition, if any.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// if ownedRedefinition-&gt;isEmpty() then
        ///     null
        /// else
        ///     ownedRedefinition-&gt;at(1).redefinedFeature
        /// endif
        /// </code>
        /// By default, the naming Feature of a Feature is given by its first redefinedFeature of its first ownedRedefinition, if any.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// The expected <see cref="IFeature" />
        /// </returns>
        internal static IFeature ComputeNamingFeatureOperation(this IFeature featureSubject)
        {
            return featureSubject == null
                ? throw new ArgumentNullException(nameof(featureSubject))
                : featureSubject.OwnedRelationship.OfType<IRedefinition>().FirstOrDefault()?.RedefinedFeature;
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// let supertypes : OrderedSet(Type) =
        ///     self.oclAsType(Type).supertypes(excludeImplied) in
        /// if featureTarget = self then supertypes
        /// else supertypes-&gt;append(featureTarget)
        /// endif
        /// </code>
        /// If featureTarget is not self (i.e., this Feature has chainingFeatures), then the featureTarget is appended to the supertypes.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <param name="excludeImplied">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IType" />
        /// </returns>
        internal static List<IType> ComputeRedefinedSupertypesOperation(this IFeature featureSubject, bool excludeImplied)
        {
            if (featureSubject == null)
            {
                throw new ArgumentNullException(nameof(featureSubject));
            }

            // Inline Type-level supertypes to avoid recursion into the Feature override
            List<IType> supertypes;

            if (featureSubject.isConjugated)
            {
                var originalType = featureSubject.ownedConjugator?.OriginalType;
                supertypes = originalType != null ? [originalType] : [];
            }
            else
            {
                var specializations = featureSubject.OwnedRelationship.OfType<ISpecialization>();

                if (excludeImplied)
                {
                    specializations = specializations.Where(s => !s.IsImplied);
                }

                supertypes = [..specializations.Select(s => s.General).Where(g => g != null)];
            }

            var target = featureSubject.featureTarget;

            if (target != featureSubject)
            {
                supertypes.Add(target);
            }

            return supertypes;
        }

        /// <summary>
        /// Check whether this Feature directly redefines the given redefinedFeature.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// ownedRedefinition.redefinedFeature-&gt;includes(redefinedFeature)
        /// </code>
        /// Check whether this Feature directly redefines the given redefinedFeature.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <param name="redefinedFeature">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeRedefinesOperation(this IFeature featureSubject, IFeature redefinedFeature)
        {
            if (featureSubject == null)
            {
                throw new ArgumentNullException(nameof(featureSubject));
            }

            return featureSubject.OwnedRelationship
                .OfType<IRedefinition>()
                .Any(r => r.RedefinedFeature == redefinedFeature);
        }

        /// <summary>
        /// Check whether this Feature directly redefines the named library Feature. libraryFeatureName must
        /// conform to the syntax of a KerML qualified name and must resolve to a Feature in global scope.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// let mem: Membership = resolveGlobal(libraryFeatureName) in
        /// mem &lt;&gt; null and mem.memberElement.oclIsKindOf(Feature) and
        /// redefines(mem.memberElement.oclAsType(Feature))
        /// </code>
        /// Check whether this Feature directly redefines the named library Feature. libraryFeatureName must
        /// conform to the syntax of a KerML qualified name and must resolve to a Feature in global scope.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <param name="libraryFeatureName">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeRedefinesFromLibraryOperation(this IFeature featureSubject, string libraryFeatureName)
        {
            if (featureSubject == null)
            {
                throw new ArgumentNullException(nameof(featureSubject));
            }

            var membership = featureSubject.ResolveGlobal(libraryFeatureName);

            return membership?.MemberElement is IFeature libraryFeature
                && featureSubject.OwnedRelationship
                    .OfType<IRedefinition>()
                    .Any(r => r.RedefinedFeature == libraryFeature);
        }

        /// <summary>
        /// Check whether this Feature directly or indirectly specializes a Feature whose last two
        /// chainingFeatures are the given Features first and second.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// allSuperTypes()-&gt;selectAsKind(Feature)-&gt;
        ///     exists(f | let n: Integer = f.chainingFeature-&gt;size() in
        ///         n &gt;= 2 and
        ///         f.chainingFeature-&gt;at(n-1) = first and
        ///         f.chainingFeature-&gt;at(n) = second)
        /// </code>
        /// Check whether this Feature directly or indirectly specializes a Feature whose last two chainingFeatures
        /// are the given Features first and second.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
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
        internal static bool ComputeSubsetsChainOperation(this IFeature featureSubject, IFeature first, IFeature second)
        {
            if (featureSubject == null)
            {
                throw new ArgumentNullException(nameof(featureSubject));
            }

            // Inline AllSupertypes: BFS transitive closure of supertypes (bypassing the stub)
            var visited = new HashSet<IType>();
            var queue = new Queue<IType>();
            visited.Add(featureSubject);
            queue.Enqueue(featureSubject);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                // Inline supertypes: general Types from ownedSpecializations
                IEnumerable<IType> supertypes;

                if (current.isConjugated)
                {
                    var originalType = current.ownedConjugator?.OriginalType;
                    supertypes = originalType != null ? [originalType] : [];
                }
                else
                {
                    supertypes = current.OwnedRelationship
                        .OfType<ISpecialization>()
                        .Select(s => s.General)
                        .Where(g => g != null);
                }

                foreach (var supertype in supertypes)
                {
                    if (visited.Add(supertype))
                    {
                        queue.Enqueue(supertype);
                    }
                }
            }

            return visited
                .OfType<IFeature>()
                .Any(f =>
                {
                    var chain = f.chainingFeature;
                    var chainCount = chain.Count;
                    return chainCount >= 2
                        && chain[chainCount - 2] == first
                        && chain[chainCount - 1] == second;
                });
        }

        /// <summary>
        /// A Feature is compatible with an otherType if it either directly or indirectly specializes the
        /// otherType or if the otherType is also a Feature and all of the following are true.
        /// <ol>
        ///  <li>
        ///  Neither this Feature or the otherType have any
        ///  ownedFeatures.
        ///  </li>
        ///  <li>
        ///  This Feature directly or indirectly redefines a
        ///  Feature that is also directly or indirectly redefined by the otherType.
        ///  </li>
        ///  <li>This Feature can access the otherType.                            </li>
        /// </ol>
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// specializes(otherType) or
        /// supertype.oclIsKindOf(Feature) and
        /// ownedFeature-&gt;isEmpty() and
        /// otherType.ownedFeature-&gt;isEmpty() and
        /// ownedRedefinitions.allRedefinedFeatures()-&gt;exists(f |
        ///     otherType.oclAsType(Feature).allRedefinedFeatures()-&gt;includes(f)) and
        /// canAccess(otherType.oclAsType(Feature))
        /// </code>
        /// A Feature is compatible with an otherType if it either directly or indirectly specializes the
        /// otherType or if the otherType is also a Feature and all of the following are true: (1) Neither has
        /// ownedFeatures. (2) They share a common allRedefinedFeature. (3) This Feature can access the otherType.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <param name="otherType">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeRedefinedIsCompatibleWithOperation(this IFeature featureSubject, IType otherType)
        {
            if (featureSubject == null)
            {
                throw new ArgumentNullException(nameof(featureSubject));
            }

            if (featureSubject.Specializes(otherType))
            {
                return true;
            }

            if (otherType is not IFeature otherFeature)
            {
                return false;
            }

            if (featureSubject.ownedFeature.Count != 0 || otherType.ownedFeature.Count != 0)
            {
                return false;
            }

            var selfRedefined = new HashSet<IFeature>(
                featureSubject.OwnedRelationship
                    .OfType<IRedefinition>()
                    .Where(r => r.RedefinedFeature != null)
                    .SelectMany(r => r.RedefinedFeature.AllRedefinedFeatures()));

            var otherRedefined = otherFeature.AllRedefinedFeatures();

            var hasCommonRedefinition = otherRedefined.Any(selfRedefined.Contains);

            return hasCommonRedefinition && featureSubject.CanAccess(otherFeature);
        }

        /// <summary>
        /// Return the Features used to determine the types of this Feature (other than this Feature itself). If
        /// this Feature is not conjugated, then the typingFeatures consist of all subsetted Features, except
        /// from CrossSubsetting, and the last chainingFeature (if any). If this Feature is conjugated, then the
        /// typingFeatures are only its originalType (if the originalType is a Feature).
        /// <strong>Note.</strong> CrossSubsetting is excluded from the determination of the type of a
        /// Feature in order to avoid circularity in the construction of implied CrossSubsetting relationships.
        /// The validateFeatureCrossFeatureType requires that the crossFeature of a Feature have the same type
        /// as the Feature.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// if not isConjugated then
        ///     let subsettedFeatures : OrderedSet(Feature) =
        ///         subsetting-&gt;reject(s | s.oclIsKindOf(CrossSubsetting)).subsettedFeatures in
        ///     if chainingFeature-&gt;isEmpty() or
        ///         subsettedFeature-&gt;includes(chainingFeature-&gt;last())
        ///     then subsettedFeatures
        ///     else subsettedFeatures-&gt;append(chainingFeature-&gt;last())
        ///     endif
        /// else if conjugator.originalType.oclIsKindOf(Feature) then
        ///     OrderedSet{conjugator.originalType.oclAsType(Feature)}
        /// else OrderedSet{}
        /// endif endif
        /// </code>
        /// Return the Features used to determine the types of this Feature (other than this Feature itself).
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IFeature" />
        /// </returns>
        internal static List<IFeature> ComputeTypingFeaturesOperation(this IFeature featureSubject)
        {
            if (featureSubject == null)
            {
                throw new ArgumentNullException(nameof(featureSubject));
            }

            if (!featureSubject.isConjugated)
            {
                var subsettedFeatures = featureSubject.OwnedRelationship
                    .OfType<ISubsetting>()
                    .Where(s => s is not ICrossSubsetting)
                    .Select(s => s.SubsettedFeature)
                    .Where(f => f != null)
                    .Distinct()
                    .ToList();

                var chainingFeatures = featureSubject.chainingFeature;

                if (chainingFeatures.Count > 0 && !subsettedFeatures.Contains(chainingFeatures[^1]))
                {
                    subsettedFeatures.Add(chainingFeatures[^1]);
                }

                return subsettedFeatures;
            }

            var conjugator = featureSubject.ownedConjugator;

            if (conjugator?.OriginalType is IFeature originalFeature)
            {
                return [originalFeature];
            }

            return [];
        }

        /// <summary>
        /// If isCartesianProduct is true, then return the list of Types whose Cartesian product can be
        /// represented by this Feature. (If isCartesianProduct is not true, the operation will still return a
        /// valid value, it will just not represent anything useful.)
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// featuringType-&gt;select(t | t.owner &lt;&gt; self)-&gt;
        ///     union(featuringType-&gt;select(t | t.owner = self)-&gt;
        ///         selectByKind(Feature).asCartesianProduct())-&gt;
        ///     union(type)
        /// </code>
        /// If isCartesianProduct is true, then return the list of Types whose Cartesian product can be
        /// represented by this Feature. (If isCartesianProduct is not true, the operation will still return a
        /// valid value, it will just not represent anything useful.)
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IType" />
        /// </returns>
        internal static List<IType> ComputeAsCartesianProductOperation(this IFeature featureSubject)
        {
            if (featureSubject == null)
            {
                throw new ArgumentNullException(nameof(featureSubject));
            }

            var notOwnedBySelf = featureSubject.featuringType
                .Where(t => t.owner != featureSubject);

            var ownedBySelf = featureSubject.featuringType
                .Where(t => t.owner == featureSubject)
                .OfType<IFeature>()
                .SelectMany(f => f.AsCartesianProduct());

            return [..notOwnedBySelf.Concat(ownedBySelf).Concat(featureSubject.type)];
        }

        /// <summary>
        /// Check whether this Feature can be used to represent a Cartesian product of Types.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// type-&gt;size() = 1 and
        /// featuringType.size() = 1 and
        /// (featuringType.first().owner = self implies
        ///     featuringType.first().oclIsKindOf(Feature) and
        ///     featuringType.first().oclAsType(Feature).isCartesianProduct())
        /// </code>
        /// Check whether this Feature can be used to represent a Cartesian product of Types.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeIsCartesianProductOperation(this IFeature featureSubject)
        {
            if (featureSubject == null)
            {
                throw new ArgumentNullException(nameof(featureSubject));
            }

            if (featureSubject.type.Count != 1 || featureSubject.featuringType.Count != 1)
            {
                return false;
            }

            var firstFeaturingType = featureSubject.featuringType[0];

            if (firstFeaturingType.owner != featureSubject)
            {
                return true;
            }

            return firstFeaturingType is IFeature featuringFeature
                && featuringFeature.IsCartesianProduct();
        }

        /// <summary>
        /// Return whether this Feature is an owned cross Feature of an end Feature.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// owningNamespace &lt;&gt; null and
        /// owningNamespace.oclIsKindOf(Feature) and
        /// owningNamespace.oclAsType(Feature).ownedCrossFeature() = self
        /// </code>
        /// Return whether this Feature is an owned cross Feature of an end Feature.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeIsOwnedCrossFeatureOperation(this IFeature featureSubject)
        {
            if (featureSubject == null)
            {
                throw new ArgumentNullException(nameof(featureSubject));
            }

            return featureSubject.owningNamespace is IFeature owningFeature
                && owningFeature.OwnedCrossFeature() == featureSubject;
        }

        /// <summary>
        /// If this Feature is an end Feature of its owningType, then return the first ownedMember of the
        /// Feature that is a Feature, but not a Multiplicity or a MetadataFeature, and whose owningMembership
        /// is not a FeatureMembership. If this exists, it is the crossFeature of the end Feature.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// if not isEnd or owningType = null then null
        /// else
        ///     let ownedMemberFeatures: Sequence(Feature) =
        ///         ownedMember-&gt;selectByKind(Feature)-&gt;
        ///         reject(oclIsKindOf(Multiplicity) or
        ///                oclIsKindOf(MetadataFeature) or
        ///                oclIsKindOf(FeatureValue))-&gt;
        ///         reject(owningMembership.oclIsKindOf(FeatureMembership)) in
        ///     if ownedMemberFeatures.isEmpty() then null
        ///     else ownedMemberFeatures-&gt;first()
        ///     endif
        /// </code>
        /// If this Feature is an end Feature of its owningType, then return the first ownedMember of the
        /// Feature that is a Feature, but not a Multiplicity or a MetadataFeature, and whose owningMembership
        /// is not a FeatureMembership. If this exists, it is the crossFeature of the end Feature.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// The expected <see cref="IFeature" />
        /// </returns>
        internal static IFeature ComputeOwnedCrossFeatureOperation(this IFeature featureSubject)
        {
            if (featureSubject == null)
            {
                throw new ArgumentNullException(nameof(featureSubject));
            }

            if (!featureSubject.IsEnd || featureSubject.owningType == null)
            {
                return null;
            }

            return featureSubject.OwnedRelationship
                .OfType<IOwningMembership>()
                .Where(om => om is not IFeatureMembership)
                .SelectMany(om => om.OwnedRelatedElement)
                .OfType<IFeature>()
                .Where(f => f is not IMultiplicity
                         && f is not IMetadataFeature
                         && f is not IFeatureValue)
                .FirstOrDefault();
        }

        /// <summary>
        /// Return this Feature and all the Features that are directly or indirectly Redefined by this Feature.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// ownedRedefinition.redefinedFeature-&gt;
        ///     closure(ownedRedefinition.redefinedFeature)-&gt;
        ///     asOrderedSet()-&gt;prepend(self)
        /// </code>
        /// Return this Feature and all the Features that are directly or indirectly Redefined by this Feature.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IFeature" />
        /// </returns>
        internal static List<IFeature> ComputeAllRedefinedFeaturesOperation(this IFeature featureSubject)
        {
            if (featureSubject == null)
            {
                throw new ArgumentNullException(nameof(featureSubject));
            }

            var result = new List<IFeature> { featureSubject };
            var visited = new HashSet<IFeature> { featureSubject };
            var queue = new Queue<IFeature>();

            foreach (var redefinition in featureSubject.OwnedRelationship.OfType<IRedefinition>())
            {
                if (redefinition.RedefinedFeature != null && visited.Add(redefinition.RedefinedFeature))
                {
                    queue.Enqueue(redefinition.RedefinedFeature);
                }
            }

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                result.Add(current);

                foreach (var redefinition in current.OwnedRelationship.OfType<IRedefinition>())
                {
                    if (redefinition.RedefinedFeature != null && visited.Add(redefinition.RedefinedFeature))
                    {
                        queue.Enqueue(redefinition.RedefinedFeature);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Return if the featuringTypes of this Feature are compatible with the given type. If type is null,
        /// then check if this Feature is explicitly or implicitly featured by Base::Anything. If this Feature
        /// has isVariable = true, then also consider it to be featured within its owningType. If this Feature
        /// is a feature chain whose first chainingFeature has isVariable = true, then also consider it to be
        /// featured within the owningType of its first chainingFeature.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// if type = null then
        ///     featuringType-&gt;forAll(f | f = resolveGlobal('Base::Anything').memberElement)
        /// else
        ///     featuringType-&gt;forAll(f | type.isCompatibleWith(f)) or
        ///     isVariable and type.specializes(owningType) or
        ///     chainingFeature-&gt;notEmpty() and chainingFeature-&gt;first().isVariable and
        ///     type.specializes(chainingFeature-&gt;first().owningType)
        /// endif
        /// </code>
        /// Return if the featuringTypes of this Feature are compatible with the given type. If type is null,
        /// then check if this Feature is explicitly or implicitly featured by Base::Anything.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <param name="type">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeIsFeaturedWithinOperation(this IFeature featureSubject, IType type)
        {
            if (featureSubject == null)
            {
                throw new ArgumentNullException(nameof(featureSubject));
            }

            if (type == null)
            {
                var anythingMembership = featureSubject.ResolveGlobal("Base::Anything");
                var anythingElement = anythingMembership?.MemberElement;
                
                return featureSubject.featuringType.Count == 0 || featureSubject.featuringType.All(f => f == anythingElement);
            }

            if (featureSubject.featuringType.All(f => type.IsCompatibleWith(f)))
            {
                return true;
            }

            if (featureSubject.IsVariable && featureSubject.owningType != null && type.Specializes(featureSubject.owningType))
            {
                return true;
            }

            var chainingFeatures = featureSubject.chainingFeature;

            if (chainingFeatures.Count > 0)
            {
                var firstChaining = chainingFeatures[0];

                if (firstChaining.IsVariable && firstChaining.owningType != null && type.Specializes(firstChaining.owningType))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// A Feature can access another feature if the other feature is featured within one of the direct or
        /// indirect featuringTypes of this Feature.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
        /// <code>
        /// let anythingType: Element =
        ///     subsettingFeature.resolveGlobal('Base::Anything').memberElement in
        /// let allFeaturingTypes : Sequence(Type) =
        ///     featuringTypes-&gt;closure(t |
        ///         if not t.oclIsKindOf(Feature) then Sequence{}
        ///         else
        ///         let featuringTypes : OrderedSet(Type) = t.oclAsType(Feature).featuringType in
        ///         if featuringTypes-&gt;isEmpty() then Sequence{anythingType}
        ///         else featuringTypes
        ///         endif
        ///         endif) in
        /// allFeaturingTypes-&gt;exists(t | feature.isFeaturedWithin(t))
        /// </code>
        /// A Feature can access another feature if the other feature is featured within one of the direct or
        /// indirect featuringTypes of this Feature.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <param name="feature">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeCanAccessOperation(this IFeature featureSubject, IFeature feature)
        {
            if (featureSubject == null)
            {
                throw new ArgumentNullException(nameof(featureSubject));
            }

            var anythingMembership = featureSubject.ResolveGlobal("Base::Anything");
            var anythingType = anythingMembership?.MemberElement as IType;

            var visited = new HashSet<IType>();
            var queue = new Queue<IType>(featureSubject.featuringType);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (!visited.Add(current))
                {
                    continue;
                }

                if (current is IFeature currentFeature)
                {
                    var innerFeaturingTypes = currentFeature.featuringType;

                    if (innerFeaturingTypes.Count == 0 && anythingType != null)
                    {
                        queue.Enqueue(anythingType);
                    }
                    else
                    {
                        foreach (var innerType in innerFeaturingTypes)
                        {
                            queue.Enqueue(innerType);
                        }
                    }
                }
            }

            return visited.Any(feature.IsFeaturedWithin);
        }

        /// <summary>
        /// Return whether the given type must be a featuringType of this Feature. If this Feature has
        /// isVariable = false, then return true if the type is the owningType of the Feature. If isVariable =
        /// true, then return true if the type is a Feature representing the snapshots of the owningType of this
        /// Feature.
        /// </summary>
        /// <remarks>
        /// OCL (KerML XMI):
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
        /// Return whether the given type must be a featuringType of this Feature. If this Feature has
        /// isVariable = false, then return true if the type is the owningType of the Feature. If isVariable =
        /// true, then return true if the type is a Feature representing the snapshots of the owningType of this Feature.
        /// </remarks>
        /// <param name="featureSubject">
        /// The subject <see cref="IFeature" />
        /// </param>
        /// <param name="type">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeIsFeaturingTypeOperation(this IFeature featureSubject, IType type)
        {
            if (featureSubject == null)
            {
                throw new ArgumentNullException(nameof(featureSubject));
            }

            if (featureSubject.owningType == null)
            {
                return false;
            }

            if (!featureSubject.IsVariable)
            {
                return type == featureSubject.owningType;
            }

            var occurrenceMembership = featureSubject.ResolveGlobal("Occurrences::Occurrence");

            if (occurrenceMembership?.MemberElement == featureSubject.owningType)
            {
                var snapshotsMembership = featureSubject.ResolveGlobal("Occurrences::Occurrence::snapshots");
                return snapshotsMembership?.MemberElement == type;
            }

            return type is IFeature typeFeature
                && typeFeature.featuringType.Contains(featureSubject.owningType)
                && typeFeature.RedefinesFromLibrary("Occurrences::Occurrence::snapshots");
        }
    }
}
