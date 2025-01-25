﻿// -------------------------------------------------------------------------------------------------
// <copyright file="IIfActionUsage.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.Core.DTO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An IfActionUsage is an ActionUsage that specifies that the thenAction ActionUsage should be
    /// performed if the result of the ifArgument Expression is true. It may also optionally specify an
    /// elseAction ActionUsage that is performed if the result of the ifArgument is false.thenAction =    
    /// let parameter : Feature = inputParameter(2) in    if parameter <> null and
    /// parameter.oclIsKindOf(ActionUsage) then        parameter.oclAsType(ActionUsage)    else        null 
    ///   endifisSubactionUsage() implies    specializesFromLibrary('Actions::Action::ifSubactions')if
    /// elseAction = null then    specializesFromLibrary('Actions::ifThenActions')else   
    /// specializesFromLibrary('Actions::ifThenElseActions')endififArgument =     let parameter : Feature =
    /// inputParameter(1) in    if parameter <> null and parameter.oclIsKindOf(Expression) then       
    /// parameter.oclAsType(Expression)    else        null    endifelseAction =     let parameter : Feature
    /// = inputParameter(3) in    if parameter <> null and parameter.oclIsKindOf(ActionUsage) then       
    /// parameter.oclAsType(ActionUsage)    else        null    endifinputParameters()->size() >= 2
    /// </summary>
    public partial interface IIfActionUsage : IActionUsage
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
