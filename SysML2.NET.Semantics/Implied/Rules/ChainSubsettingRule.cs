// -------------------------------------------------------------------------------------------------
// <copyright file="ChainSubsettingRule.cs" company="Starion Group S.A.">
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

    /// <summary>
    /// Base for a rule that satisfies a <c>subsetsChain(first, second)</c> obligation.
    /// </summary>
    /// <remarks>
    /// KerML 1.0 §8.3.3.3.4 Feature defines <c>subsetsChain(first, second)</c> as holding when the Feature
    /// "directly or indirectly specializes a Feature whose last two chainingFeatures are the given Features
    /// first and second". The general of that Subsetting need not exist in the model, so it is SYNTHESIZED
    /// by <see cref="IImpliedRelationshipFactory.CreateImpliedFeatureChain" />.
    /// <para>This is the one place the layer emits a Relationship whose other end is a new Element rather
    /// than one the caller already holds. The synthesized chain is detached and unnamed; everything it means
    /// is in its <c>chainingFeature</c> list. A consumer that resolves names through implied Specializations
    /// must therefore tolerate a general it cannot find in the model — see the note on
    /// <see cref="IImpliedRelationshipFactory.CreateImpliedFeatureChain" />.</para>
    /// <para>A rule yields nothing when either end of the chain is absent: an incomplete model states no
    /// chain, and fabricating half of one would assert something the model does not.</para>
    /// </remarks>
    public abstract class ChainSubsettingRule : IImpliedRelationshipRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChainSubsettingRule" /> class.
        /// </summary>
        /// <param name="factory">The factory creating the chain and the Subsetting.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory" /> is null.</exception>
        protected ChainSubsettingRule(IImpliedRelationshipFactory factory)
        {
            this.Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public abstract string ConstraintName { get; }

        /// <summary>
        /// Gets the factory creating the chain and the Subsetting.
        /// </summary>
        protected IImpliedRelationshipFactory Factory { get; }

        /// <summary>
        /// Computes the chain Subsettings the Element requires.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>One Subsetting per required chain; empty when the constraint does not apply.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public IReadOnlyList<IRelationship> Apply(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return
            [
                ..this.QueryChains(element)
                    .Where(chain => chain.Subsetting != null && chain.First != null && chain.Second != null)
                    .Select(chain => this.Factory.CreateImpliedSubsetting(
                        chain.Subsetting,
                        this.Factory.CreateImpliedFeatureChain(chain.First, chain.Second)))
            ];
        }

        /// <summary>
        /// Returns each chain obligation the constraint places on the Element.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The Feature that must subset the chain, and the two Features forming it.</returns>
        protected abstract IEnumerable<(IFeature Subsetting, IFeature First, IFeature Second)> QueryChains(IElement element);
    }
}
