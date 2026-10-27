// ------------------------------------------------------------------------------------------------
// <copyright file="PayloadFeatureRedefinitionRule.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied.Rules
{
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Interactions;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Implements checkPayloadFeatureRedefinition: a PayloadFeature redefines Transfers::Transfer::payload.
    /// </summary>
    /// <remarks>
    /// OCL: <c>redefinesFromLibrary('Transfers::Transfer::payload')</c> — unconditional, so every
    /// PayloadFeature carries it.
    /// </remarks>
    public class PayloadFeatureRedefinitionRule : LibraryRedefinitionRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PayloadFeatureRedefinitionRule" /> class.
        /// </summary>
        /// <param name="libraryTypeIndex">The index resolving the library Feature by qualified name.</param>
        /// <param name="factory">The factory creating the detached Redefinition.</param>
        public PayloadFeatureRedefinitionRule(ILibraryTypeIndex libraryTypeIndex, IImpliedRelationshipFactory factory)
            : base(libraryTypeIndex, factory)
        {
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public override string ConstraintName => "checkPayloadFeatureRedefinition";

        /// <summary>
        /// Returns the PayloadFeature itself as the redefining Feature.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The Element and the library qualified name, or <c>null</c> when it is not a PayloadFeature.</returns>
        protected override (IFeature RedefiningFeature, string LibraryQualifiedName)? QueryRedefinition(IElement element)
        {
            return element is IPayloadFeature payloadFeature
                ? (payloadFeature, "Transfers::Transfer::payload")
                : null;
        }
    }
}
