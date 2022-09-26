// -------------------------------------------------------------------------------------------------
// <copyright file="IConjugatedPortDefinition.cs" company="RHEA System S.A.">
//
//   Copyright 2022 RHEA System S.A.
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;

    /// <summary>
    /// A ConjugatedPortDefinition is a PortDefinition that is a PortConjugation of its original
    /// PortDefinition. That is, a ConjugatedPortDefinition inherits all the features of the original
    /// PortDefinition, but input flows of the original PortDefinition become outputs on the
    /// ConjugatedPortDefinition and output flows of the original PortDefinition become inputs on the
    /// ConjugatedPortDefinition. Every PortDefinition (that is not itself a ConjugatedPortDefinition) has
    /// exactly one corresponding ConjugatedPortDefinition, whose effective name is the name of the
    /// originalPortDefinition, with the character ~ prepended.originalPortDefinition =
    /// ownedPortConjugator.originalPortDefinitionconjugatedPortDefinition = null
    /// </summary>
    public partial interface IConjugatedPortDefinition : IPortDefinition
    {
        /// <summary>
        /// Queries the derived property OriginalPortDefinition
        /// </summary>
        PortDefinition QueryOriginalPortDefinition();

        /// <summary>
        /// Queries the derived property OwnedPortConjugator
        /// </summary>
        PortConjugation QueryOwnedPortConjugator();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
