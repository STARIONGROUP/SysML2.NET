// -------------------------------------------------------------------------------------------------
// <copyright file="NullImpliedRelationshipProvider.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Semantics.Implied
{
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// An <see cref="IImpliedRelationshipProvider" /> that computes nothing.
    /// </summary>
    /// <remarks>
    /// This is the default collaborator on the optional-provider constructors, so a caller that has not opted
    /// into implied-relationship computation gets the SDK's pre-existing behaviour without null checks at
    /// every call site. Every constraint is reported as not covered, which is accurate for this
    /// implementation.
    /// </remarks>
    public class NullImpliedRelationshipProvider : IImpliedRelationshipProvider
    {
        /// <summary>
        /// Gets the shared instance.
        /// </summary>
        public static NullImpliedRelationshipProvider Instance { get; } = new NullImpliedRelationshipProvider();

        /// <summary>
        /// Gets the names of the semantic constraints this provider cannot compute, which is all of them.
        /// </summary>
        public IReadOnlyList<string> NotCoveredConstraints => ImpliedRelationshipTable.AllConstraintNames;

        /// <summary>
        /// Returns no implied Relationships.
        /// </summary>
        /// <param name="element">The Element, which is not inspected.</param>
        /// <returns>An empty collection.</returns>
        public IReadOnlyList<IRelationship> GetImpliedRelationships(IElement element) => [];

        /// <summary>
        /// Returns no implied Specializations.
        /// </summary>
        /// <param name="type">The Type, which is not inspected.</param>
        /// <returns>An empty collection.</returns>
        public IReadOnlyList<ISpecialization> GetImpliedSpecializations(IType type) => [];

        /// <summary>
        /// Returns no implied Redefinitions.
        /// </summary>
        /// <param name="feature">The Feature, which is not inspected.</param>
        /// <returns>An empty collection.</returns>
        public IReadOnlyList<IRedefinition> GetImpliedRedefinitions(IFeature feature) => [];

        /// <summary>
        /// Reports every constraint as not covered.
        /// </summary>
        /// <param name="constraintName">The constraint name, which is not inspected.</param>
        /// <returns>Always false.</returns>
        public bool IsConstraintCovered(string constraintName) => false;
    }
}
