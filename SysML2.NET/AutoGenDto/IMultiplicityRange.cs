// -------------------------------------------------------------------------------------------------
// <copyright file="IMultiplicityRange.cs" company="RHEA System S.A.">
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
    /// A MultiplicityRange is a Multiplicity whose value is defined to be the (inclusive) range of natural
    /// numbers given by the result of a lowerBound Expression and the result of an upperBound Expression.
    /// The result of the lowerBound Expression shall be of type Natural, while the result of the upperBound
    /// Expression shall be of type UnlimitedNatural. If the result of the upperBound Expression is the
    /// unbounded value *, then the specified range includes all natural numbers greater than or equal to
    /// the lowerBound value.bound->forAll(b | b.featuringType = self.featuringType)
    /// </summary>
    public partial interface IMultiplicityRange : IMultiplicity
    {
    }
}
