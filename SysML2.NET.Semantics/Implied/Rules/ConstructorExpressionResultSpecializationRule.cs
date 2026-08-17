// -------------------------------------------------------------------------------------------------
// <copyright file="ConstructorExpressionResultSpecializationRule.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Implements checkConstructorExpressionResultSpecialization: the result of a ConstructorExpression
    /// specializes the Type being instantiated.
    /// </summary>
    /// <remarks>
    /// OCL: <c>result.specializes(instantiatedType)</c>.
    /// <para>The OCL says THAT the result specializes, not by which Relationship. The specification supplies
    /// the missing half — KerML 1.0 §8.4.4.9.4 Constructor Expressions (p. 261): the result specializes the
    /// instantiatedType "via a FeatureTyping if the instantiatedType is a Classifier or a Subsetting if it
    /// is a Feature". Emitting one kind for both cases would be syntactically faithful to the OCL and
    /// semantically wrong.</para>
    /// </remarks>
    public class ConstructorExpressionResultSpecializationRule : IImpliedRelationshipRule
    {
        /// <summary>
        /// The factory creating the detached Relationship.
        /// </summary>
        private readonly IImpliedRelationshipFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorExpressionResultSpecializationRule" /> class.
        /// </summary>
        /// <param name="factory">The factory creating the detached Relationship.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory" /> is null.</exception>
        public ConstructorExpressionResultSpecializationRule(IImpliedRelationshipFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public string ConstraintName => "checkConstructorExpressionResultSpecialization";

        /// <summary>
        /// Computes the Relationship binding a ConstructorExpression's result to the Type it instantiates.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The FeatureTyping or Subsetting, or empty when the constraint does not apply.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public IReadOnlyList<IRelationship> Apply(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (element is not IConstructorExpression { result: not null, instantiatedType: not null } constructorExpression)
            {
                return [];
            }

            return constructorExpression.instantiatedType switch
            {
                IFeature instantiatedFeature => [this.factory.CreateImpliedSubsetting(constructorExpression.result, instantiatedFeature)],
                IClassifier instantiatedClassifier => [this.factory.CreateImpliedFeatureTyping(constructorExpression.result, instantiatedClassifier)],
                _ => []
            };
        }
    }
}
