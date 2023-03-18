// -------------------------------------------------------------------------------------------------
// <copyright file="IClass.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
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
    /// A Class is a Classifier of things (in the universe) that can be distinguished without regard to how
    /// they are related to other things (via Features). This means multiple things classified by the same
    /// Class can be distinguished, even when they are related other things in exactly the same
    /// way.specializesFromLibrary('Occurrences::Occurrence')ownedSpecialization.general->    forAll(not
    /// oclIsKindOf(DataType)) andnot oclIsKindOf(AssociationStructure) implies   
    /// ownedSpecialization.general->        forAll(not oclIsKindOf(Association))
    /// </summary>
    public partial interface IClass : IClassifier
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
