// -------------------------------------------------------------------------------------------------
// <copyright file="PrimitiveConstraint.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

namespace SysML2.NET.PSM.DTO
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.PSM.Enumerations;

    /// <summary>
    /// Data transfer object that represents the PrimitiveConstraint schema of the Systems Modeling API and Services
    /// </summary>
    [GeneratedCode("SysML2.NET", "latest")]
    public partial class PrimitiveConstraint : IConstraint, IResponse
    {
        /// <summary>
        /// The value of the type discriminator of the PrimitiveConstraint schema
        /// </summary>
        public const string Type = "PrimitiveConstraint";

        /// <summary>
        /// Gets or sets the inverse of the PrimitiveConstraint
        /// </summary>
        public bool Inverse { get; set; }

        /// <summary>
        /// Gets or sets the operator of the PrimitiveConstraint
        /// </summary>
        public PrimitiveConstraintOperator Operator { get; set; }

        /// <summary>
        /// Gets or sets the property of the PrimitiveConstraint
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Gets or sets the value of the PrimitiveConstraint
        /// </summary>
        public List<ConstraintValue> Value { get; set; }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
