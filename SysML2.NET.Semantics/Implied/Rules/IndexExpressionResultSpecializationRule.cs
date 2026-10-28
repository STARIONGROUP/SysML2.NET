// ------------------------------------------------------------------------------------------------
// <copyright file="IndexExpressionResultSpecializationRule.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied.Rules
{
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Implements checkIndexExpressionResultSpecialization: the result of an IndexExpression subsets the
    /// result of the collection it indexes, unless that collection is an Array.
    /// </summary>
    /// <remarks>
    /// OCL: <c>arguments-&gt;notEmpty() and not
    /// arguments-&gt;first().result.specializesFromLibrary('Collections::Array') implies
    /// result.specializes(arguments-&gt;first().result)</c>.
    /// <para>Indexing an Array is excluded because an Array's element type is not a subset of the Array —
    /// the Subsetting that holds for an ordinary collection would be wrong there.</para>
    /// </remarks>
    public class IndexExpressionResultSpecializationRule : ArgumentResultSpecializationRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexExpressionResultSpecializationRule" /> class.
        /// </summary>
        /// <param name="factory">The factory creating the detached Subsetting.</param>
        public IndexExpressionResultSpecializationRule(IImpliedRelationshipFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public override string ConstraintName => "checkIndexExpressionResultSpecialization";

        /// <summary>
        /// Asserts whether the Element is an IndexExpression.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>True when the Element is an IndexExpression.</returns>
        protected override bool IsInScope(IElement element) => element is IIndexExpression;

        /// <summary>
        /// Excludes an indexed Array.
        /// </summary>
        /// <param name="firstArgumentResult">The result parameter of the first argument Expression.</param>
        /// <returns>True unless the indexed collection specializes the library Array.</returns>
        protected override bool AppliesTo(IFeature firstArgumentResult)
        {
            return !firstArgumentResult.SpecializesFromLibrary("Collections::Array");
        }
    }
}
