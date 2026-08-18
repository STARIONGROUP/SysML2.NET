// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureValuationSpecializationRule.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Implements checkFeatureValuationSpecialization: an undeclared, undirected Feature with a value takes
    /// its typing from that value by subsetting the value Expression's result.
    /// </summary>
    /// <remarks>
    /// OCL: <c>direction = null and ownedSpecializations-&gt;forAll(isImplied) implies
    /// ownedMembership-&gt;selectByKind(FeatureValue)-&gt;forAll(fv | specializes(fv.value.result))</c>.
    /// <para>KerML 1.0 §8.4.4.11 Feature Values (p. 265): "if the featureWithValue has no explicit
    /// ownedSpecializations and is not directed, then it SUBSETS the result parameter of the value
    /// Expression. This reflects the semantics that the values of the featureWithValue is determined by the
    /// value Expression, giving the featureWithValue an implied typing that is useful for static type
    /// checking."</para>
    /// <para>The converse is the reason for the guard: a Feature that DOES declare a Specialization, or that
    /// is directed, already has its static typing from its declaration, and the spec says that typing
    /// "should then be validated against" the value's result rather than derived from it. Note that
    /// "no explicit ownedSpecializations" means every one of them is implied — not that there are none.</para>
    /// </remarks>
    public class FeatureValuationSpecializationRule : IImpliedRelationshipRule
    {
        /// <summary>
        /// The factory creating the detached Subsetting.
        /// </summary>
        private readonly IImpliedRelationshipFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureValuationSpecializationRule" /> class.
        /// </summary>
        /// <param name="factory">The factory creating the detached Subsetting.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory" /> is null.</exception>
        public FeatureValuationSpecializationRule(IImpliedRelationshipFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public string ConstraintName => "checkFeatureValuationSpecialization";

        /// <summary>
        /// Computes the Subsetting a valued Feature takes from its value Expression's result.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>One Subsetting per FeatureValue carrying a result; empty otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public IReadOnlyList<IRelationship> Apply(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (element is not IFeature { Direction: null } feature
                || feature.ownedSpecialization.Any(specialization => !specialization.IsImplied))
            {
                return [];
            }

            return
            [
                ..feature.ownedMembership
                    .OfType<IFeatureValue>()
                    .Select(featureValue => featureValue.value?.result)
                    .Where(valueResult => valueResult != null)
                    .Select(valueResult => this.factory.CreateImpliedSubsetting(feature, valueResult))
            ];
        }
    }
}
