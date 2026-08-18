// -------------------------------------------------------------------------------------------------
// <copyright file="OccurrenceDefinitionMultiplicitySpecializationRule.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Systems.Occurrences;

    /// <summary>
    /// Implements checkOccurrenceDefinitionMultiplicitySpecialization: an individual OccurrenceDefinition
    /// has at most one instance.
    /// </summary>
    /// <remarks>
    /// OCL: <c>isIndividual implies multiplicity &lt;&gt; null and
    /// multiplicity.specializesFromLibrary('Base::zeroOrOne')</c>.
    /// <para>The Subsetting is carried by the definition's MULTIPLICITY, not by the definition itself — a
    /// Multiplicity IS a Feature, so it can subset a library Feature in its own right. Declaring an
    /// OccurrenceDefinition individual asserts it denotes a single thing, which is what bounding its
    /// multiplicity to <c>Base::zeroOrOne</c> expresses.</para>
    /// </remarks>
    public class OccurrenceDefinitionMultiplicitySpecializationRule : LibrarySpecializationRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OccurrenceDefinitionMultiplicitySpecializationRule" /> class.
        /// </summary>
        /// <param name="libraryTypeIndex">The index resolving the library Feature by qualified name.</param>
        /// <param name="factory">The factory creating the detached Subsetting.</param>
        public OccurrenceDefinitionMultiplicitySpecializationRule(ILibraryTypeIndex libraryTypeIndex, IImpliedRelationshipFactory factory)
            : base(libraryTypeIndex, factory)
        {
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public override string ConstraintName => "checkOccurrenceDefinitionMultiplicitySpecialization";

        /// <summary>
        /// Returns an individual OccurrenceDefinition's multiplicity together with the library bound.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The Feature and the library qualified name, or <c>null</c> when the constraint does not apply.</returns>
        protected override (IFeature SpecificFeature, string LibraryQualifiedName)? QuerySpecialization(IElement element)
        {
            return element is not IOccurrenceDefinition { IsIndividual: true, multiplicity: not null } occurrenceDefinition
                ? null
                : (occurrenceDefinition.multiplicity, "Base::zeroOrOne");
        }
    }
}
