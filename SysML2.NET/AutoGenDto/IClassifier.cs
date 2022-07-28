// -------------------------------------------------------------------------------------------------
// <copyright file="IClassifier.cs" company="RHEA System S.A.">
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
    /// A Classifier is a Type for model elements that classify:<ul>	<li>Things (in the universe)
    /// regardless of how Features relate them.  These are sequences of exactly one thing
    /// (sequence of length 1).</li>	<li>How the above things are related by Features. These are
    /// sequences of multiple things (length &gt; 1).</li></ul>Classifiers that classify relationships
    /// (sequence length &gt; 1) must also classify the things at the end of those sequences (sequence
    /// length =1).  Because of this, Classifiers specializing Features cannot classify anything (any
    /// sequences).ownedSuperclassing = ownedGeneralization->intersection(superclassing)multiplicity <> null
    /// implies multiplicity.featuringType->isEmpty()
    /// </summary>
    public interface IClassifier : IType
    {
    }
}
