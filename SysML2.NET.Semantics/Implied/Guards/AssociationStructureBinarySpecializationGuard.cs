// ------------------------------------------------------------------------------------------------
// <copyright file="AssociationStructureBinarySpecializationGuard.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied.Guards
{
    using System;

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Associations;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Guards checkAssociationStructureBinarySpecialization: an AssociationStructure with exactly two end Features specializes Objects::BinaryLinkObject.
    /// </summary>
    /// <remarks>
    /// OCL: <c>endFeature-&gt;size() = 2 implies specializesFromLibrary('Objects::BinaryLinkObject')</c>
    /// </remarks>
    public class AssociationStructureBinarySpecializationGuard : IImpliedRuleGuard
    {
        /// <summary>
        /// Gets the name of the semantic constraint this guard decides.
        /// </summary>
        public string ConstraintName => "checkAssociationStructureBinarySpecialization";

        /// <summary>
        /// Asserts whether the constraint applies to the supplied Element.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>True when the constraint applies.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public bool Applies(IElement element)
        {
            return element == null
                ? throw new ArgumentNullException(nameof(element))
                : element is IAssociationStructure associationStructure && ((IType)associationStructure).endFeature.Count == 2;
        }
    }
}
