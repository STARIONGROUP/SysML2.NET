// -------------------------------------------------------------------------------------------------
// <copyright file="PortDefinitionExtensions.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
// 
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// 
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Ports
{
    using System;
    using System.Linq;

    /// <summary>
    /// The <see cref="PortDefinitionExtensions" /> class provides extensions methods for
    /// the <see cref="IPortDefinition" /> interface
    /// </summary>
    internal static class PortDefinitionExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// conjugatedPortDefinition =
        ///                             let conjugatedPortDefinitions : OrderedSet(ConjugatedPortDefinition) =
        ///                             ownedMember-&gt;selectByKind(ConjugatedPortDefinition) in
        ///                             if conjugatedPortDefinitions-&gt;isEmpty() then null
        ///                             else conjugatedPortDefinitions-&gt;first()
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="portDefinitionSubject">
        /// The subject <see cref="IPortDefinition" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IConjugatedPortDefinition ComputeConjugatedPortDefinition(this IPortDefinition portDefinitionSubject)
        {
            return portDefinitionSubject == null
                ? throw new ArgumentNullException(nameof(portDefinitionSubject))
                : portDefinitionSubject.ownedMember.OfType<IConjugatedPortDefinition>().FirstOrDefault();
        }
    }
}
