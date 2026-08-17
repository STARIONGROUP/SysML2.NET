// -------------------------------------------------------------------------------------------------
// <copyright file="LibraryRedefinitionRule.cs" company="Starion Group S.A.">
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
    /// The shared behaviour of the constraints expressed as <c>redefinesFromLibrary('…')</c>.
    /// </summary>
    /// <remarks>
    /// These constraints all resolve a library Feature by qualified name and require a Redefinition to it.
    /// What differs between them is only WHICH Feature must redefine it — sometimes the Element itself,
    /// sometimes one reached by navigation — so a subclass supplies just that, via
    /// <see cref="QueryRedefinition" />.
    /// </remarks>
    public abstract class LibraryRedefinitionRule : IImpliedRelationshipRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryRedefinitionRule" /> class.
        /// </summary>
        /// <param name="libraryTypeIndex">The index resolving the library Feature by qualified name.</param>
        /// <param name="factory">The factory creating the detached Redefinition.</param>
        /// <exception cref="ArgumentNullException">Thrown when either argument is null.</exception>
        protected LibraryRedefinitionRule(ILibraryTypeIndex libraryTypeIndex, IImpliedRelationshipFactory factory)
        {
            this.LibraryTypeIndex = libraryTypeIndex ?? throw new ArgumentNullException(nameof(libraryTypeIndex));
            this.Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets the name of the semantic constraint the concrete rule implements.
        /// </summary>
        public abstract string ConstraintName { get; }

        /// <summary>
        /// Gets the index resolving the library Feature by qualified name.
        /// </summary>
        protected ILibraryTypeIndex LibraryTypeIndex { get; }

        /// <summary>
        /// Gets the factory creating the detached Redefinition.
        /// </summary>
        protected IImpliedRelationshipFactory Factory { get; }

        /// <summary>
        /// Computes the implied Redefinition the constraint requires of the supplied Element.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>A single Redefinition, or empty when the constraint does not apply.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        /// <exception cref="UnresolvedLibraryTypeException">Thrown when the targeted library Feature is not indexed.</exception>
        public IReadOnlyList<IRelationship> Apply(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            var redefinition = this.QueryRedefinition(element);

            if (redefinition == null)
            {
                return [];
            }

            var (redefiningFeature, libraryQualifiedName) = redefinition.Value;

            if (!this.LibraryTypeIndex.TryGetType(libraryQualifiedName, out var libraryType))
            {
                throw new UnresolvedLibraryTypeException(libraryQualifiedName, this.ConstraintName);
            }

            return libraryType is IFeature libraryFeature
                ? [this.Factory.CreateImpliedRedefinition(redefiningFeature, libraryFeature)]
                : [];
        }

        /// <summary>
        /// Returns the Feature that must redefine a library Feature, together with that Feature's qualified
        /// name.
        /// </summary>
        /// <param name="element">The Element under evaluation, never null.</param>
        /// <returns>The redefining Feature and the library qualified name, or <c>null</c> when the constraint does not apply.</returns>
        protected abstract (IFeature RedefiningFeature, string LibraryQualifiedName)? QueryRedefinition(IElement element);
    }
}
