// -------------------------------------------------------------------------------------------------
// <copyright file="PrimitiveConstraint.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.PIM.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// a subtype of Constraint that represents simple conditions that be modeled using the
    /// property-operator -value tuple. e.g. mass <= 4 kg, or type instanceOf Generalization
    /// </summary>
    public class PrimitiveConstraint
    {
        /// <summary>
        /// Gets or sets the name of a property of Element or its subtypes that is being constrained
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Gets or sets the mathematical operators
        /// </summary>
        public Operator Operator { get; set; }

        /// <summary>
        /// Gets or sets list of primitive objects, such as String, Boolean, Integer, Double, and UUID
        /// </summary>
        public List<string> Value { get; set;} = [];

        /// <summary>
        /// Gets or sets a value indicating a logical NOT operator is applied to the <see cref="PrimitiveConstraint"/>
        /// </summary>
        public bool Inverse { get; set; }
    }
}
