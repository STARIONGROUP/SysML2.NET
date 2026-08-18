// -------------------------------------------------------------------------------------------------
// <copyright file="ConnectorBinarySpecializationGuard.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Kernel.Connectors;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Guards checkConnectorBinarySpecialization: a Connector with exactly two ends specializes
    /// Links::binaryLinks.
    /// </summary>
    /// <remarks>
    /// OCL: <c>connectorEnd-&gt;size() = 2 implies specializesFromLibrary('Links::binaryLinks')</c>. This is
    /// the unconditioned binary case; <see cref="ConnectorBinaryObjectSpecializationGuard" /> adds the
    /// AssociationStructure condition for the stronger Objects::binaryLinkObjects Specialization, and the
    /// 8.4.2 redundancy rules decide which survives when both apply.
    /// </remarks>
    public class ConnectorBinarySpecializationGuard : IImpliedRuleGuard
    {
        /// <summary>
        /// Gets the name of the semantic constraint this guard decides.
        /// </summary>
        public string ConstraintName => "checkConnectorBinarySpecialization";

        /// <summary>
        /// Asserts whether the Connector has exactly two ends.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>True when the Element is a Connector with exactly two ends.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public bool Applies(IElement element)
        {
            return element == null
                ? throw new ArgumentNullException(nameof(element))
                : element is IConnector { connectorEnd.Count: 2 };
        }
    }
}
