// -------------------------------------------------------------------------------------------------
// <copyright file="IWhileLoopActionUsage.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2024 RHEA System S.A.
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
    /// A WhileLoopActionUsage is a LoopActionUsage that specifies that the bodyAction ActionUsage should be
    /// performed repeatedly while the result of the whileArgument Expression is true or until the result of
    /// the untilArgument Expression (if provided) is true. The whileArgument Expression is evaluated before
    /// each (possible) performance of the bodyAction, and the untilArgument Expression is evaluated after
    /// each performance of the bodyAction.isSubactionUsage() implies   
    /// specializesFromLibrary('Actions::Action::whileLoops')untilArgument =    let parameter : Feature =
    /// inputParameter(3) in    if parameter <> null and parameter.oclIsKindOf(Expression) then       
    /// parameter.oclAsType(Expression)    else        null   
    /// endifspecializesFromLibrary('Actions::whileLoopActions')whileArgument =    let parameter : Feature =
    /// inputParameter(1) in    if parameter <> null and parameter.oclIsKindOf(Expression) then       
    /// parameter.oclAsType(Expression)    else        null    endifinputParameters()->size() >= 2
    /// </summary>
    public partial interface IWhileLoopActionUsage : ILoopActionUsage
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
