// -------------------------------------------------------------------------------------------------
// <copyright file="IClass.cs" company="RHEA System S.A.">
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
    /// A Class is a Classifier of things (in the universe) that can be distinguished without regard to how
    /// they are related to other things (via Features). This means multiple things classified by the same
    /// Class can be distinguished, even when they are related other things in exactly the same
    /// way.Classes serve to subdivide Classifiers into two kinds of objects: those that have some
    /// definition beyond their property values and those that are defined entirely by their values. Classes
    /// are the first kind. Two objects that are classified by a given Class can have entirely identical
    /// descriptions and properties and still be treated as separate. Classes are intended for the
    /// construction of models representing real world things which can be separate entities even if all
    /// measurable properties are the same.allSupertypes()->includes(Kernel Library::Occurrence)
    /// </summary>
    public interface IClass : IClassifier
    {
    }
}