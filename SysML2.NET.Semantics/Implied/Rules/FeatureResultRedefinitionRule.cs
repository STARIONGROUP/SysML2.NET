// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureResultRedefinitionRule.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Implements checkFeatureResultRedefinition: the result parameter of a Function or Expression redefines
    /// the result of each Function or Expression it specializes.
    /// </summary>
    /// <remarks>
    /// OCL: <c>owningType &lt;&gt; null and (owningType.oclIsKindOf(Function) and self =
    /// owningType.oclAsType(Function).result or owningType.oclIsKindOf(Expression) and self =
    /// owningType.oclAsType(Expression).result) implies owningType.ownedSpecialization.general-&gt;
    /// select(oclIsKindOf(Function) or oclIsKindOf(Expression))-&gt;forAll(supertype |
    /// redefines(… supertype's result …))</c>.
    /// <para>The Feature must BE its owner's result, not merely be owned by a Function — which is why the
    /// identity comparison against <c>result</c> is what gates the rule.</para>
    /// </remarks>
    public class FeatureResultRedefinitionRule : IImpliedRelationshipRule
    {
        /// <summary>
        /// The factory creating the detached Redefinitions.
        /// </summary>
        private readonly IImpliedRelationshipFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureResultRedefinitionRule" /> class.
        /// </summary>
        /// <param name="factory">The factory creating the detached Redefinitions.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory" /> is null.</exception>
        public FeatureResultRedefinitionRule(IImpliedRelationshipFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public string ConstraintName => "checkFeatureResultRedefinition";

        /// <summary>
        /// Computes the Redefinitions a result parameter requires towards the results of its owning Type's
        /// Function or Expression supertypes.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>One Redefinition per Function or Expression supertype that has a result; empty otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public IReadOnlyList<IRelationship> Apply(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (element is not IFeature { owningType: not null } feature || !ReferenceEquals(QueryResult(feature.owningType), feature))
            {
                return [];
            }

            return
            [
                ..feature.owningType.ownedSpecialization
                    .Select(specialization => QueryResult(specialization.General))
                    .Where(supertypeResult => supertypeResult != null)
                    .Select(supertypeResult => this.factory.CreateImpliedRedefinition(feature, supertypeResult))
            ];
        }

        /// <summary>
        /// Returns the result parameter of a Type when it is a Function or an Expression.
        /// </summary>
        /// <param name="type">The Type to inspect, which may be null.</param>
        /// <returns>The result parameter, or <c>null</c> when the Type has none.</returns>
        private static IFeature QueryResult(IType type)
        {
            return type switch
            {
                IFunction function => function.result,
                IExpression expression => expression.result,
                _ => null
            };
        }
    }
}
