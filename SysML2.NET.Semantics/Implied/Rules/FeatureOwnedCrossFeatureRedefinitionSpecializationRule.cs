// ------------------------------------------------------------------------------------------------
// <copyright file="FeatureOwnedCrossFeatureRedefinitionSpecializationRule.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Implements checkFeatureOwnedCrossFeatureRedefinitionSpecialization: an owned cross Feature subsets the
    /// cross Features of everything its owner redefines.
    /// </summary>
    /// <remarks>
    /// OCL: <c>isOwnedCrossFeature() implies ownedSubsetting.subsettedFeature-&gt;includesAll(
    /// owner.oclAsType(Feature).ownedRedefinition.redefinedFeature-&gt;select(crossFeature &lt;&gt; null).crossFeature)</c>.
    /// <para>When the owning Feature redefines another, the cross Feature must line up with that Feature's
    /// own cross Feature — so the redefinition of an end carries through to the opposite end. A redefined
    /// Feature without a cross Feature contributes nothing.</para>
    /// </remarks>
    public class FeatureOwnedCrossFeatureRedefinitionSpecializationRule : IImpliedRelationshipRule
    {
        /// <summary>
        /// The factory creating the detached Subsettings.
        /// </summary>
        private readonly IImpliedRelationshipFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureOwnedCrossFeatureRedefinitionSpecializationRule" /> class.
        /// </summary>
        /// <param name="factory">The factory creating the detached Subsettings.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory" /> is null.</exception>
        public FeatureOwnedCrossFeatureRedefinitionSpecializationRule(IImpliedRelationshipFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public string ConstraintName => "checkFeatureOwnedCrossFeatureRedefinitionSpecialization";

        /// <summary>
        /// Computes the Subsettings an owned cross Feature requires towards the cross Features of the
        /// Features its owner redefines.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>One Subsetting per redefined Feature carrying a cross Feature; empty otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public IReadOnlyList<IRelationship> Apply(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (element is not IFeature crossFeature || !crossFeature.IsOwnedCrossFeature() || crossFeature.owner is not IFeature owningFeature)
            {
                return [];
            }

            return
            [
                ..owningFeature.ownedRedefinition
                    .Select(redefinition => redefinition.RedefinedFeature?.crossFeature)
                    .Where(redefinedCrossFeature => redefinedCrossFeature != null)
                    .Select(redefinedCrossFeature => this.factory.CreateImpliedSubsetting(crossFeature, redefinedCrossFeature))
            ];
        }
    }
}
