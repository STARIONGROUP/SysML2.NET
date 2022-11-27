// -------------------------------------------------------------------------------------------------
// <copyright file="IDataType.cs" company="RHEA System S.A.">
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
    /// A DataType is a Classifier of things (in the universe) that can only be distinguished by how they
    /// are related to other things (via Features). This means multiple things classified by the same
    /// DataType<ul>	<li>Cannot be distinguished when they are related to other things in exactly the same
    /// way, even when they are intended to be about different things.</li>	<li>Can be distinguished when
    /// they are related to other things in different ways, even when they are intended to be about the same
    /// thing.</li></ul>DataTypes serve to subdivide Classifiers into two kinds of objects: those that have
    /// some definition beyond their property values and those that are defined entirely by their values.
    /// DataTypes are the second kind. If two objects classified by DataType have identical property values,
    /// they are understood to be in fact the same object. DataTypes are intended to represent data or
    /// mathematical objects which is where the equivalence based on matched values is
    /// appropriate.allSupertypes()->includes(resolve("Base::DataValue"))ownedGeneralization.general->   
    /// forAll(not oclIsKindOf(Class))
    /// </summary>
    public partial interface IDataType : IClassifier
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
