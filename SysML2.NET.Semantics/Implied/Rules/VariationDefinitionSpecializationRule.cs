// -------------------------------------------------------------------------------------------------
// <copyright file="VariationDefinitionSpecializationRule.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;

    /// <summary>
    /// Implements checkUsageVariationDefinitionSpecialization: a variant Usage is typed by the variation
    /// Definition that owns it.
    /// </summary>
    /// <remarks>
    /// SysML 2.0 8.4.2.3 gives the kernel equivalent of <c>variation part def P { variant part p1; }</c> as
    /// <c>class P specializes Parts::Part { member feature p1 : P subsets Parts::parts; }</c>. The variant is
    /// TYPED BY the Definition rather than subsetting it, because a Usage is a Feature and a Definition is a
    /// Classifier — which is why this rule produces a FeatureTyping where its Usage counterpart produces a
    /// Subsetting.
    /// </remarks>
    public class VariationDefinitionSpecializationRule : IImpliedRelationshipRule
    {
        /// <summary>
        /// The factory creating the detached FeatureTyping.
        /// </summary>
        private readonly IImpliedRelationshipFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariationDefinitionSpecializationRule" /> class.
        /// </summary>
        /// <param name="factory">The factory creating the detached FeatureTyping.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory" /> is null.</exception>
        public VariationDefinitionSpecializationRule(IImpliedRelationshipFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public string ConstraintName => "checkUsageVariationDefinitionSpecialization";

        /// <summary>
        /// Computes the FeatureTyping a variant Usage requires towards its owning variation Definition.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>A single FeatureTyping, or empty when the Element is not a variant of a variation Definition.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public IReadOnlyList<IRelationship> Apply(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return element is IUsage { owningMembership: IVariantMembership, owningNamespace: IDefinition variationDefinition } variantUsage
                ? [this.factory.CreateImpliedFeatureTyping(variantUsage, variationDefinition)]
                : [];
        }
    }
}
