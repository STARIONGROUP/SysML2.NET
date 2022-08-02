// -------------------------------------------------------------------------------------------------
// <copyright file="EFeatureAttribute.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Decorators
{
    using System;

    /// <summary>
    /// Attribute used to decorate properties with with the properties sourced from
    /// the SysML2 ecore meta-model.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EFeatureAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EFeatureAttribute"/>
        /// </summary>
        /// <param name="changeable">
        /// a value indicating whether this feature is Changeable.
        /// </param>
        /// <param name="volatile">
        /// a value indicating whether this feature is Volatile.
        /// </param>
        /// <param name="transient">
        /// a value indicating whether this feature is Transient.
        /// </param>
        /// <param name="unsettable">
        /// a value indicating whether this feature is un-settable.
        /// </param>
        /// <param name="derived">
        /// a value indicating whether this feature is derived.
        /// </param>
        /// <param name="ordered">
        /// a value indicating whether this feature is ordered.
        /// </param>
        /// <param name="unique">
        /// a value indicating whether this feature is unique.
        /// </param>
        /// <param name="lowerBound">
        /// the lower bound of the feature.
        /// </param>
        /// <param name="upperBound">
        /// the upper bound of the feature.
        /// </param>
        /// <param name="many">
        /// a value indicating whether this feature is many.
        /// </param>
        /// <param name="required">
        /// a value indicating whether this feature is required.
        /// </param>
        /// <param name="isContainment">
        /// a value indicating whether this feature is a member of a containment relationship.
        /// </param>
        public EFeatureAttribute(bool changeable, bool @volatile, bool transient, bool unsettable, bool derived, bool ordered, bool unique, int lowerBound, int upperBound, bool many, bool required, bool isContainment)
        {
            this.Changeable = changeable;
            this.Volatile = @volatile;
            this.Transient = transient;
            this.Unsettable = unsettable;
            this.Derived = derived;
            this.Ordered = ordered;
            this.Unique = unique;
            this.LowerBound = lowerBound;
            this.UpperBound = upperBound;
            this.Many = many;
            this.Required = required;
            this.IsContainment = isContainment;
        }
        
        /// <summary>
        /// Gets a value indicating whether this feature is changeable.
        /// </summary>
        /// <remarks>
        /// A feature that is not changeable will not include a
        /// generated set method, and the reflective eSet()
        /// method will throw an exception if you try to set it.
        /// Declaring one end of a bi-directional relationship to
        /// be not changeable is a good way to force clients to
        /// always set the reference from the other end, but still
        /// provide convenient navigation methods from either end.
        /// Declaring one-way references or attributes to be not
        /// changeable usually implies that the feature will be set or
        /// changed by some other(user-written) code
        /// </remarks>
        public bool Changeable { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this feature is Volatile.
        /// </summary>
        /// <remarks>
        /// A feature that is declared volatile is generated without
        /// storage fields and with empty implementation method
        /// bodies, which you are required to complete.Volatile
        /// is commonly used for a feature whose value is derived
        /// from some other feature, or for a feature that is to be
        /// implemented by hand using a different storage and
        /// implementation pattern.
        /// </remarks>
        public bool Volatile { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this feature is Transient.
        /// </summary>
        /// <remarks>
        /// Transient features are used to declare (modeled) data
        /// whose lifetime never spans application invocations and
        /// therefore doesn't need to be persisted. The (default XMI)
        /// serializer will not save features that are declared to be
        /// transient.
        /// </remarks>
        public bool Transient { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this feature can be unset.
        /// </summary>
        /// <remarks>
        /// When true, the value space for this feature includes the state of being unset.
        /// </remarks>
        public bool Unsettable { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this feature is Derived.
        /// </summary>
        /// <remarks>
        /// The value of a derived feature is computed from other
        /// features, so it doesn't represent any additional object
        /// state.Framework classes, such as EcoreUtil.Copier,
        /// that copy model objects will not attempt to copy such
        /// features.The generated code is unaffected by the value
        /// of the derived flag.Derived features are typically also
        /// marked volatile and transient
        /// </remarks>
        public bool Derived { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this is ordered
        /// </summary>
        public bool Ordered { get; private set; }
        
        /// <summary>
        /// Gets a value indicating whether this is unique
        /// </summary>
        public bool Unique { get; private set; }

        /// <summary>
        /// Gets the lower bound
        /// </summary>
        public int LowerBound { get; private set; }

        /// <summary>
        /// Gets the upper bound
        /// </summary>
        public int UpperBound { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this is many
        /// </summary>
        public bool Many { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this is required
        /// </summary>
        public bool Required { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this feature is a member of a containment relationship
        /// </summary>
        public bool IsContainment { get; private set; }
    }
}
