// ------------------------------------------------------------------------------------------------
// <copyright file="FeatureOwnedCrossFeatureSpecializationRule.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Implements checkFeatureOwnedCrossFeatureSpecialization: an owned cross Feature specializes every Type
    /// of the Feature that owns it.
    /// </summary>
    /// <remarks>
    /// OCL: <c>isOwnedCrossFeature() implies owner.oclAsType(Feature).type-&gt;forAll(t | self.specializes(t))</c>.
    /// <para>The cross Feature stands for the other end of its owner, so it carries the owner's typing. The
    /// Relationship kind follows the general rule for what "specialize" means of a Feature: a FeatureTyping
    /// onto a Classifier, a Subsetting onto a Feature.</para>
    /// </remarks>
    public class FeatureOwnedCrossFeatureSpecializationRule : IImpliedRelationshipRule
    {
        /// <summary>
        /// The factory creating the detached Relationships.
        /// </summary>
        private readonly IImpliedRelationshipFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureOwnedCrossFeatureSpecializationRule" /> class.
        /// </summary>
        /// <param name="factory">The factory creating the detached Relationships.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory" /> is null.</exception>
        public FeatureOwnedCrossFeatureSpecializationRule(IImpliedRelationshipFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public string ConstraintName => "checkFeatureOwnedCrossFeatureSpecialization";

        /// <summary>
        /// Computes the Relationships an owned cross Feature requires towards its owner's Types.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>One Relationship per Type of the owning Feature; empty otherwise.</returns>
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
                ..owningFeature.type
                    .Select(type => type switch
                    {
                        IFeature ownerTypeFeature => this.factory.CreateImpliedSubsetting(crossFeature, ownerTypeFeature),
                        IClassifier ownerTypeClassifier => (IRelationship)this.factory.CreateImpliedFeatureTyping(crossFeature, ownerTypeClassifier),
                        _ => null
                    })
                    .Where(relationship => relationship != null)
            ];
        }
    }
}
