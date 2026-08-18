// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureEndRedefinitionRule.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Implements checkFeatureEndRedefinition: the nth end Feature of a Type redefines the nth end Feature
    /// of each of its supertypes.
    /// </summary>
    /// <remarks>
    /// OCL: <c>isEnd and owningType &lt;&gt; null implies let i : Integer =
    /// owningType.ownedEndFeature-&gt;indexOf(self) in owningType.ownedSpecialization.general-&gt;forAll(
    /// supertype | supertype.endFeature-&gt;size() &gt;= i implies
    /// redefines(supertype.endFeature-&gt;at(i)))</c>.
    /// <para>The correspondence is POSITIONAL, not by name: end 1 redefines end 1. OCL collections are
    /// 1-based, so <c>indexOf</c> and <c>at(i)</c> are translated against a 0-based list accordingly, and a
    /// supertype with fewer ends than the position contributes nothing.</para>
    /// </remarks>
    public class FeatureEndRedefinitionRule : IImpliedRelationshipRule
    {
        /// <summary>
        /// The factory creating the detached Redefinitions.
        /// </summary>
        private readonly IImpliedRelationshipFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureEndRedefinitionRule" /> class.
        /// </summary>
        /// <param name="factory">The factory creating the detached Redefinitions.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory" /> is null.</exception>
        public FeatureEndRedefinitionRule(IImpliedRelationshipFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public string ConstraintName => "checkFeatureEndRedefinition";

        /// <summary>
        /// Computes the Redefinitions an end Feature requires towards the corresponding ends of its owning
        /// Type's supertypes.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>One Redefinition per supertype that has an end at the same position; empty otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public IReadOnlyList<IRelationship> Apply(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (element is not IFeature { IsEnd: true, owningType: not null } endFeature)
            {
                return [];
            }

            var position = endFeature.owningType.ownedEndFeature.IndexOf(endFeature);

            if (position < 0)
            {
                return [];
            }

            return
            [
                ..endFeature.owningType.ownedSpecialization
                    .Select(specialization => specialization.General)
                    .Where(supertype => supertype != null && supertype.endFeature.Count > position)
                    .Select(supertype => this.factory.CreateImpliedRedefinition(endFeature, supertype.endFeature[position]))
            ];
        }
    }
}
