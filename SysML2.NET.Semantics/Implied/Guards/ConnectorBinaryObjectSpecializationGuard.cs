// -------------------------------------------------------------------------------------------------
// <copyright file="ConnectorBinaryObjectSpecializationGuard.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Semantics.Implied.Guards
{
    using System;
    using System.Linq;

    using SysML2.NET.Core.POCO.Kernel.Associations;
    using SysML2.NET.Core.POCO.Kernel.Connectors;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Guards checkConnectorBinaryObjectSpecialization: a binary Connector typed by an AssociationStructure
    /// specializes Objects::binaryLinkObjects.
    /// </summary>
    /// <remarks>
    /// OCL: <c>connectorEnds-&gt;size() = 2 and
    /// association-&gt;exists(oclIsKindOf(AssociationStructure)) implies
    /// specializesFromLibrary('Objects::binaryLinkObjects')</c>. Both conjuncts are required: a binary
    /// Connector typed by a plain Association carries a different library Specialization.
    /// </remarks>
    public class ConnectorBinaryObjectSpecializationGuard : IImpliedRuleGuard
    {
        /// <summary>
        /// Gets the name of the semantic constraint this guard decides.
        /// </summary>
        public string ConstraintName => "checkConnectorBinaryObjectSpecialization";

        /// <summary>
        /// Asserts whether the Connector has exactly two ends and is typed by an AssociationStructure.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>True when the Element is a binary Connector typed by an AssociationStructure.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public bool Applies(IElement element)
        {
            return element == null
                ? throw new ArgumentNullException(nameof(element))
                : element is IConnector { connectorEnd.Count: 2 } connector
                  && connector.association.Any(association => association is IAssociationStructure);
        }
    }
}
