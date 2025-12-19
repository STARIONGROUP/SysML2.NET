// -------------------------------------------------------------------------------------------------
// <copyright file="PropertyAttribute.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Decorators
{
    using System;

    /// <summary>
    /// AggregationKind is an Enumeration for specifying the kind of aggregation of a Property.
    /// </summary>
    public enum AggregationKind
    {
        /// <summary>
        /// Indicates that the Property has no aggregation.
        /// </summary>
        None,

        /// <summary>
        /// Indicates that the Property has shared aggregation.
        /// </summary>
        Shared,

        /// <summary>
        /// Indicates that the Property is aggregated compositely, i.e., the composite object has responsibility
        /// for the existence and storage of the composed objects (parts).
        /// </summary>
        Composite,

    }
    /// <summary>
    /// Attribute used to decorate properties with using the properties sourced from
    /// the UML metamodel.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false)]
    public sealed class PropertyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyAttribute"/> class.
        /// </summary>
        public PropertyAttribute(string xmiId = "", AggregationKind aggregation = AggregationKind.None, int lowerValue = 1, int upperValue = 1,
            bool isOrdered = false,
            bool isReadOnly = false,
            bool isDerived = false,
            bool isDerivedUnion = false,
            bool isUnique = true,
            string defaultValue = null)
        {
            this.XmiId = xmiId;
            this.Aggregation = aggregation;
            this.LowerValue = lowerValue;
            this.UpperValue = upperValue;
            this.IsOrdered = isOrdered;
            this.IsReadOnly = isReadOnly;
            this.IsDerived = isDerived;
            this.IsDerivedUnion = isDerivedUnion;
            this.IsUnique = isUnique;
            this.DefaultValue = defaultValue;
        }

        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        public string XmiId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="AggregationKind"/>.
        /// </summary>
        public AggregationKind Aggregation { get; set; }

        /// <summary>
        /// Gets or sets the lower value (lower bound) of the property
        /// </summary>
        public int LowerValue { get; set; }

        /// <summary>
        /// Gets or sets the upper value (upperbound) of the property
        /// </summary>
        public int UpperValue { get; set; }

        /// <summary>
        /// Gets or sets a value specifying whether this is an ordered property
        /// </summary>
        public bool IsOrdered { get; set; }

        /// <summary>
        /// Gets or sets a value specifying whether this is a readonly property
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Gets or sets a value specifying whether this is a derived property
        /// </summary>
        public bool IsDerived { get; set; }

        /// <summary>
        /// Gets or sets a value specifying whether this is a derived union property
        /// </summary>
        public bool IsDerivedUnion { get; set; }

        /// <summary>
        /// For a multivalued multiplicity, this attributes specifies whether the values in an instantiation of this MultiplicityElement are unique.
        /// </summary>
        public bool IsUnique { get; set; }

        /// <summary>
        /// Gets or sets the default value if any.
        /// </summary>
        public string DefaultValue { get; set; }
    }
}
