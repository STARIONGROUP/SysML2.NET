// -------------------------------------------------------------------------------------------------
// <copyright file="LibrarySpecializationRule.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Base for a rule whose OCL selects BETWEEN library Features by a condition, rather than naming one
    /// unconditionally.
    /// </summary>
    /// <remarks>
    /// The generated table carries one target per row, so it can express
    /// <c>specializesFromLibrary(X)</c> and <c>C implies specializesFromLibrary(X)</c> but not
    /// <c>if C then specializesFromLibrary(X) else specializesFromLibrary(Y) endif</c> — the target itself
    /// varies. Those constraints therefore fall to the not-covered manifest and are hand-coded on this base.
    /// <para>The implied Relationship is a <c>Subsetting</c>: every metaclass in this family is a Usage or
    /// an Expression, hence a Feature, and Subclassification applies only to the Classifier metaclasses the
    /// table tracks separately.</para>
    /// </remarks>
    public abstract class LibrarySpecializationRule : IImpliedRelationshipRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibrarySpecializationRule" /> class.
        /// </summary>
        /// <param name="libraryTypeIndex">The index resolving the library Feature by qualified name.</param>
        /// <param name="factory">The factory creating the detached Subsetting.</param>
        /// <exception cref="ArgumentNullException">Thrown when either argument is null.</exception>
        protected LibrarySpecializationRule(ILibraryTypeIndex libraryTypeIndex, IImpliedRelationshipFactory factory)
        {
            this.LibraryTypeIndex = libraryTypeIndex ?? throw new ArgumentNullException(nameof(libraryTypeIndex));
            this.Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public abstract string ConstraintName { get; }

        /// <summary>
        /// Gets the index resolving the library Feature by qualified name.
        /// </summary>
        protected ILibraryTypeIndex LibraryTypeIndex { get; }

        /// <summary>
        /// Gets the factory creating the detached Subsetting.
        /// </summary>
        protected IImpliedRelationshipFactory Factory { get; }

        /// <summary>
        /// Computes the Subsetting the Element requires towards the library Feature its condition selects.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The Subsetting, or empty when the constraint does not apply.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        /// <exception cref="UnresolvedLibraryTypeException">Thrown when the selected library Feature is not indexed.</exception>
        public IReadOnlyList<IRelationship> Apply(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            var specialization = this.QuerySpecialization(element);

            if (specialization == null)
            {
                return [];
            }

            var (specificFeature, libraryQualifiedName) = specialization.Value;

            if (!this.LibraryTypeIndex.TryGetType(libraryQualifiedName, out var libraryType))
            {
                throw new UnresolvedLibraryTypeException(libraryQualifiedName, this.ConstraintName);
            }

            return libraryType is IFeature libraryFeature
                ? [this.Factory.CreateImpliedSubsetting(specificFeature, libraryFeature)]
                : [];
        }

        /// <summary>
        /// Returns the Feature together with the library Feature the constraint's condition selects.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The Feature and the library qualified name, or <c>null</c> when the constraint does not apply.</returns>
        protected abstract (IFeature SpecificFeature, string LibraryQualifiedName)? QuerySpecialization(IElement element);
    }
}
