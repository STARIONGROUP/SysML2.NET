// ------------------------------------------------------------------------------------------------
// <copyright file="AssociationExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Kernel.Associations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;

    /// <summary>
    /// The <see cref="AssociationExtensions" /> class provides extensions methods for
    /// the <see cref="IAssociation" /> interface
    /// </summary>
    internal static class AssociationExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="associationSubject">
        /// The subject <see cref="IAssociation" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IAssociation.associationEnd))]
        internal static List<IFeature> ComputeAssociationEnd(this IAssociation associationSubject)
        {
            return associationSubject == null
                ? throw new ArgumentNullException(nameof(associationSubject))
                : [..associationSubject.feature.Where(memberFeature => memberFeature.IsEnd)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// relatedType = associationEnd.type
        /// </code>
        /// </remarks>
        /// <param name="associationSubject">
        /// The subject <see cref="IAssociation" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IAssociation.relatedType))]
        internal static List<IType> ComputeRelatedType(this IAssociation associationSubject)
        {
            return associationSubject == null
                ? throw new ArgumentNullException(nameof(associationSubject))
                : [..associationSubject.associationEnd.SelectMany(end => end.type)];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// sourceType =
        ///                             if relatedType-&gt;isEmpty() then null
        ///                             else relatedType-&gt;first() endif
        /// </code>
        /// </remarks>
        /// <param name="associationSubject">
        /// The subject <see cref="IAssociation" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IAssociation.sourceType))]
        internal static IType ComputeSourceType(this IAssociation associationSubject)
        {
            if (associationSubject == null)
            {
                throw new ArgumentNullException(nameof(associationSubject));
            }

            var relatedTypes = associationSubject.relatedType;

            return relatedTypes.Count == 0 ? null : relatedTypes[0];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// targetType =
        ///                             if relatedType-&gt;size() &lt; 2 then OrderedSet{}
        ///                             else
        ///                             relatedType-&gt;
        ///                             subSequence(2, relatedType-&gt;size())-&gt;
        ///                             asOrderedSet()
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="associationSubject">
        /// The subject <see cref="IAssociation" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IAssociation.targetType))]
        internal static List<IType> ComputeTargetType(this IAssociation associationSubject)
        {
            if (associationSubject == null)
            {
                throw new ArgumentNullException(nameof(associationSubject));
            }

            var relatedTypes = associationSubject.relatedType;

            return relatedTypes.Count < 2 ? [] : [..relatedTypes.Skip(1).Distinct()];
        }
    }
}
