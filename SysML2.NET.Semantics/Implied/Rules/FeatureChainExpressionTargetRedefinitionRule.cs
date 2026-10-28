// ------------------------------------------------------------------------------------------------
// <copyright file="FeatureChainExpressionTargetRedefinitionRule.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied.Rules
{
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Kernel.Expressions;

    /// <summary>
    /// Implements checkFeatureChainExpressionTargetRedefinition: the source-target Feature of a FeatureChainExpression redefines ControlFunctions::'.'::source::target.
    /// </summary>
    /// <remarks>
    /// OCL: <c>let sourceTargetFeature : Feature = sourceTargetFeature() in sourceTargetFeature &lt;&gt; null and sourceTargetFeature.redefinesFromLibrary('ControlFunctions::\'.\'::source::target')</c>
    /// </remarks>
    public class FeatureChainExpressionTargetRedefinitionRule : LibraryRedefinitionRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureChainExpressionTargetRedefinitionRule" /> class.
        /// </summary>
        /// <param name="libraryTypeIndex">The index resolving the library Feature by qualified name.</param>
        /// <param name="factory">The factory creating the detached Redefinition.</param>
        public FeatureChainExpressionTargetRedefinitionRule(ILibraryTypeIndex libraryTypeIndex, IImpliedRelationshipFactory factory)
            : base(libraryTypeIndex, factory)
        {
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public override string ConstraintName => "checkFeatureChainExpressionTargetRedefinition";

        /// <summary>
        /// Returns the Feature that must redefine the library Feature.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The Feature and the library qualified name, or <c>null</c> when the constraint does not apply.</returns>
        protected override (IFeature RedefiningFeature, string LibraryQualifiedName)? QueryRedefinition(IElement element)
        {
            var redefiningFeature = element is IFeatureChainExpression featureChainExpression ? featureChainExpression.SourceTargetFeature() : null;

            return redefiningFeature == null
                ? null
                : (redefiningFeature, "ControlFunctions::'.'::source::target");
        }
    }
}
