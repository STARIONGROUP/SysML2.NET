// ------------------------------------------------------------------------------------------------
// <copyright file="SelectExpressionResultSpecializationRule.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied.Rules
{
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Implements checkSelectExpressionResultSpecialization: the result of a SelectExpression subsets the
    /// result of the collection it selects from.
    /// </summary>
    /// <remarks>
    /// OCL: <c>arguments-&gt;notEmpty() implies result.specializes(arguments-&gt;first().result)</c>.
    /// <para>Selecting from a collection yields a subset of it, so the result subsets the source's result.</para>
    /// </remarks>
    public class SelectExpressionResultSpecializationRule : ArgumentResultSpecializationRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectExpressionResultSpecializationRule" /> class.
        /// </summary>
        /// <param name="factory">The factory creating the detached Subsetting.</param>
        public SelectExpressionResultSpecializationRule(IImpliedRelationshipFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public override string ConstraintName => "checkSelectExpressionResultSpecialization";

        /// <summary>
        /// Asserts whether the Element is a SelectExpression.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>True when the Element is a SelectExpression.</returns>
        protected override bool IsInScope(IElement element) => element is ISelectExpression;
    }
}
