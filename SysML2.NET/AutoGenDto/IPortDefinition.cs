// -------------------------------------------------------------------------------------------------
// <copyright file="IPortDefinition.cs" company="RHEA System S.A.">
//
// Copyright 2022 RHEA System S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A PortDefinition defines a point at which external entities can connect to and interact with a
    /// system or part of a system. Any ownedUsages of a PortDefinition, other than PortUsages, must not be
    /// composite.A PortDefinition must subclass, directly or indirectly, the base Class Port from the
    /// Systems model library.conjugatedPortDefinition =
    /// ownedMember->select(oclIsKindOf(ConjugatedPortDefinition))ownedUsage->    select(not
    /// oclIsKindOf(PortUsage))->    forAll(not isComposite)
    /// </summary>
    public partial interface IPortDefinition : IOccurrenceDefinition, IStructure
    {
    }
}
