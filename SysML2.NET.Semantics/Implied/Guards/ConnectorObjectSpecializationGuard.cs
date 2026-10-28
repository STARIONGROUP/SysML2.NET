// ------------------------------------------------------------------------------------------------
// <copyright file="ConnectorObjectSpecializationGuard.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied.Guards
{
    using System;
    using System.Linq;

    using SysML2.NET.Core.POCO.Kernel.Associations;
    using SysML2.NET.Core.POCO.Kernel.Connectors;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Guards checkConnectorObjectSpecialization: a Connector typed by an AssociationStructure specializes
    /// Objects::linkObjects.
    /// </summary>
    /// <remarks>
    /// OCL: <c>association-&gt;exists(oclIsKindOf(AssociationStructure)) implies
    /// specializesFromLibrary('Objects::linkObjects')</c>. The <c>exists</c> navigation over a collection is
    /// outside the generator's translatable shapes, so the guard is written by hand. It is the unconditioned
    /// counterpart of <see cref="ConnectorBinaryObjectSpecializationGuard" />, which adds the two-end
    /// condition.
    /// </remarks>
    public class ConnectorObjectSpecializationGuard : IImpliedRuleGuard
    {
        /// <summary>
        /// Gets the name of the semantic constraint this guard decides.
        /// </summary>
        public string ConstraintName => "checkConnectorObjectSpecialization";

        /// <summary>
        /// Asserts whether the Connector is typed by an AssociationStructure.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>True when the Element is a Connector with an AssociationStructure among its associations.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public bool Applies(IElement element)
        {
            return element == null
                ? throw new ArgumentNullException(nameof(element))
                : element is IConnector connector && connector.association.Any(association => association is IAssociationStructure);
        }
    }
}
