// -------------------------------------------------------------------------------------------------
// <copyright file="ConstructorExpressionSpecializationRule.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Implements checkConstructorExpressionSpecialization: a ConstructorExpression subsets the library
    /// constructor evaluations.
    /// </summary>
    /// <remarks>
    /// OCL: <c>specializes('Performances::constructorEvaluations')</c>.
    /// <para>Reached the not-covered manifest only because the OCL calls <c>specializes</c> rather than
    /// <c>specializesFromLibrary</c>, which is what the generated table's classifier matches; the target is
    /// a library Feature all the same, so the rule is the ordinary library-subsetting shape.</para>
    /// <para>KerML 1.0 §8.4.4.9.4 Constructor Expressions (p. 261) records what this buys: the library
    /// Expression subsets <c>Performances::evaluations</c> and redefines its result parameter to
    /// multiplicity <c>1..1</c>, so a ConstructorExpression always produces a single value.</para>
    /// </remarks>
    public class ConstructorExpressionSpecializationRule : LibrarySpecializationRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorExpressionSpecializationRule" /> class.
        /// </summary>
        /// <param name="libraryTypeIndex">The index resolving the library Feature by qualified name.</param>
        /// <param name="factory">The factory creating the detached Subsetting.</param>
        public ConstructorExpressionSpecializationRule(ILibraryTypeIndex libraryTypeIndex, IImpliedRelationshipFactory factory)
            : base(libraryTypeIndex, factory)
        {
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public override string ConstraintName => "checkConstructorExpressionSpecialization";

        /// <summary>
        /// Returns the ConstructorExpression together with the library Feature it subsets.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The Feature and the library qualified name, or <c>null</c> when the constraint does not apply.</returns>
        protected override (IFeature SpecificFeature, string LibraryQualifiedName)? QuerySpecialization(IElement element)
        {
            return element is not IConstructorExpression constructorExpression
                ? null
                : (constructorExpression, "Performances::constructorEvaluations");
        }
    }
}
