// -------------------------------------------------------------------------------------------------
// <copyright file="TransitionUsageTransitionFeatureSpecializationRule.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
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
    using SysML2.NET.Core.POCO.Systems.States;

    /// <summary>
    /// Implements checkTransitionUsageTransitionFeatureSpecialization: each transition feature subsets the
    /// library part of a TransitionAction that plays its role.
    /// </summary>
    /// <remarks>
    /// OCL: <c>triggerAction-&gt;forAll(specializesFromLibrary('Actions::TransitionAction::accepter') and
    /// guardExpression-&gt;forAll(specializesFromLibrary('Actions::TransitionAction::guard') and
    /// effectAction-&gt;forAll(specializesFromLibrary('Actions::TransitionAction::effect'))</c>.
    /// <para>Three independent roles on ONE constraint, so this rule yields up to three Subsettings per
    /// transition feature collection rather than the single Relationship the library base class emits — which
    /// is why it does not use <see cref="LibrarySpecializationRule" />.</para>
    /// </remarks>
    public class TransitionUsageTransitionFeatureSpecializationRule : IImpliedRelationshipRule
    {
        /// <summary>
        /// The library Feature each transition-feature role subsets.
        /// </summary>
        private const string AccepterQualifiedName = "Actions::TransitionAction::accepter";

        /// <summary>
        /// The library Feature a guard Expression subsets.
        /// </summary>
        private const string GuardQualifiedName = "TransitionPerformances::TransitionPerformance::guard";

        /// <summary>
        /// The library Feature an effect ActionUsage subsets.
        /// </summary>
        private const string EffectQualifiedName = "Actions::TransitionAction::effect";

        /// <summary>
        /// The index resolving the library Features by qualified name.
        /// </summary>
        private readonly ILibraryTypeIndex libraryTypeIndex;

        /// <summary>
        /// The factory creating the detached Subsettings.
        /// </summary>
        private readonly IImpliedRelationshipFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransitionUsageTransitionFeatureSpecializationRule" /> class.
        /// </summary>
        /// <param name="libraryTypeIndex">The index resolving the library Features by qualified name.</param>
        /// <param name="factory">The factory creating the detached Subsettings.</param>
        /// <exception cref="ArgumentNullException">Thrown when either argument is null.</exception>
        public TransitionUsageTransitionFeatureSpecializationRule(ILibraryTypeIndex libraryTypeIndex, IImpliedRelationshipFactory factory)
        {
            this.libraryTypeIndex = libraryTypeIndex ?? throw new ArgumentNullException(nameof(libraryTypeIndex));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public string ConstraintName => "checkTransitionUsageTransitionFeatureSpecialization";

        /// <summary>
        /// Computes the Subsettings each of a TransitionUsage's transition features requires.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>One Subsetting per trigger, guard and effect; empty when the constraint does not apply.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        /// <exception cref="UnresolvedLibraryTypeException">Thrown when a targeted library Feature is not indexed.</exception>
        public IReadOnlyList<IRelationship> Apply(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (element is not ITransitionUsage transitionUsage)
            {
                return [];
            }

            return
            [
                ..this.Subset(transitionUsage.triggerAction, AccepterQualifiedName),
                ..this.Subset(transitionUsage.guardExpression, GuardQualifiedName),
                ..this.Subset(transitionUsage.effectAction, EffectQualifiedName)
            ];
        }

        /// <summary>
        /// Creates a Subsetting from each transition feature towards the library Feature for its role.
        /// </summary>
        /// <param name="transitionFeatures">The transition features playing one role.</param>
        /// <param name="libraryQualifiedName">The library Feature the role subsets.</param>
        /// <returns>The Subsettings, or empty when there are no such features.</returns>
        /// <exception cref="UnresolvedLibraryTypeException">Thrown when the library Feature is not indexed.</exception>
        private IEnumerable<IRelationship> Subset(IEnumerable<IFeature> transitionFeatures, string libraryQualifiedName)
        {
            var features = transitionFeatures.ToList();

            if (features.Count == 0)
            {
                return [];
            }

            if (!this.libraryTypeIndex.TryGetType(libraryQualifiedName, out var libraryType))
            {
                throw new UnresolvedLibraryTypeException(libraryQualifiedName, this.ConstraintName);
            }

            return libraryType is not IFeature libraryFeature
                ? []
                : features.Select(feature => this.factory.CreateImpliedSubsetting(feature, libraryFeature));
        }
    }
}
