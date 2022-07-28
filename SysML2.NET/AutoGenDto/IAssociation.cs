// -------------------------------------------------------------------------------------------------
// <copyright file="IAssociation.cs" company="RHEA System S.A.">
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
    /// An Association is a Relationship and a Classifier to enable classification of links between things
    /// (in the universe). The co-domains (types) of the associationEnd Features are the relatedTypes, as
    /// co-domain and participants (linked things) of an Association identify each other.relatedTypes =
    /// associationEnd.typelet numend : Natural = associationEnd->size() in    allSupertypes()->includes(   
    ///     if numend = 2 then Kernel Library::BinaryLink        else Kernel
    /// Library::Link)oclIsKindOf(Structure) =
    /// oclIsKindOf(AssociationStructure)allSupertypes()->includes(Kernel Library::Link)
    /// </summary>
    public interface IAssociation : IClassifier, IRelationship
    {
    }
}