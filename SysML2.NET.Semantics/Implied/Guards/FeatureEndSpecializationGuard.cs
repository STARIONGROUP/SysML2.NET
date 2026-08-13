// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureEndSpecializationGuard.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Associations;
    using SysML2.NET.Core.POCO.Kernel.Connectors;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Guards checkFeatureEndSpecialization: an end Feature of an Association or Connector specializes
    /// Links::Link::participant.
    /// </summary>
    /// <remarks>
    /// OCL: <c>isEnd and owningType &lt;&gt; null and (owningType.oclIsKindOf(Association) or
    /// owningType.oclIsKindOf(Connector)) implies
    /// specializesFromLibrary('Links::Link::participant')</c>. An end Feature owned by anything else — a
    /// plain Type, for instance — is out of scope.
    /// </remarks>
    public class FeatureEndSpecializationGuard : IImpliedRuleGuard
    {
        /// <summary>
        /// Gets the name of the semantic constraint this guard decides.
        /// </summary>
        public string ConstraintName => "checkFeatureEndSpecialization";

        /// <summary>
        /// Asserts whether the Feature is an end of an Association or Connector.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>True when the Element is an end Feature owned by an Association or Connector.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public bool Applies(IElement element)
        {
            return element == null
                ? throw new ArgumentNullException(nameof(element))
                : element is IFeature { IsEnd: true, owningType: IAssociation or IConnector };
        }
    }
}
