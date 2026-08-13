// -------------------------------------------------------------------------------------------------
// <copyright file="VariationUsageSpecializationRule.cs" company="Starion Group S.A.">
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
    /// Implements checkUsageVariationUsageSpecialization: a variant Usage subsets the variation Usage that
    /// owns it.
    /// </summary>
    /// <remarks>
    /// SysML 2.0 8.4.2.3 gives the kernel equivalent of <c>variation part p { variant part p1; }</c> as
    /// <c>feature p subsets Parts::parts { member feature p1 subsets p; }</c>, so the implied Relationship is
    /// a Subsetting from the variant to the variation. The owning variation is reached through the
    /// VariantMembership's owning Namespace, which subsets Usage::owningVariationUsage in the abstract syntax.
    /// </remarks>
    public class VariationUsageSpecializationRule : IImpliedRelationshipRule
    {
        /// <summary>
        /// The factory creating the detached Subsetting.
        /// </summary>
        private readonly IImpliedRelationshipFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariationUsageSpecializationRule" /> class.
        /// </summary>
        /// <param name="factory">The factory creating the detached Subsetting.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory" /> is null.</exception>
        public VariationUsageSpecializationRule(IImpliedRelationshipFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public string ConstraintName => "checkUsageVariationUsageSpecialization";

        /// <summary>
        /// Computes the Subsetting a variant Usage requires towards its owning variation Usage.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>A single Subsetting, or empty when the Element is not a variant of a variation Usage.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public IReadOnlyList<IRelationship> Apply(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return element is IUsage { owningMembership: IVariantMembership, owningNamespace: IUsage variationUsage } variantUsage
                   && !ReferenceEquals(variationUsage, variantUsage)
                ? [this.factory.CreateImpliedSubsetting(variantUsage, variationUsage)]
                : [];
        }
    }
}
