﻿// -------------------------------------------------------------------------------------------------
// <copyright file="IDataType.cs" company="Starion Group S.A.">
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
    /// A DataType is a Classifier of things (in the universe) that can only be distinguished by how they
    /// are related to other things (via Features). This means multiple things classified by the same
    /// DataType<ul>	<li>Cannot be distinguished when they are related to other things in exactly the same
    /// way, even when they are intended to be about different things.</li>	<li>Can be distinguished when
    /// they are related to other things in different ways, even when they are intended to be about the same
    /// thing.</li></ul>ownedSpecialization.general->    forAll(not oclIsKindOf(Class) and            not
    /// oclIsKindOf(Association))specializesFromLibrary('Base::DataValue')
    /// </summary>
    public partial interface IDataType : IClassifier
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
