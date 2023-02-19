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
// Unless isRequired by applicable law or agreed to in writing, software
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
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false)]
    public class EFeatureAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EFeatureAttribute"/>
        /// </summary>
        /// <param name="isChangeable">
        /// a value indicating whether this feature is IsChangeable.
        /// </param>
        /// <param name="isVolatile">
        /// a value indicating whether this feature is IsVolatile. A feature that is declared
        /// volatile is to be generated without storage fields and with empty implementation
        /// method bodies, which you are required to complete. Volatile is commonly used for
        /// a feature whose value is derived from some other feature, or for a feature that is
        /// to be implemented by hand using a different storage and implementation pattern.
        /// </param>
        /// <param name="isTransient">
        /// a value indicating whether this feature is IsTransient. Transient features are
        /// used to declare (modeled) data whose lifetime never spans application invocations
        /// and therefore doesn't need to be persisted.
        /// </param>
        /// <param name="isUnsettable">
        /// a value indicating whether this feature is un-settable.
        /// </param>
        /// <param name="isDerived">
        /// a value indicating whether this feature is isDerived. The value of a derived feature
        /// is computed from other features, so it doesn't represent any additional object state.
        /// Derived features are typically also marked volatile and transient.
        /// </param>
        /// <param name="isOrdered">
        /// a value indicating whether this feature is isOrdered.
        /// </param>
        /// <param name="isUnique">
        /// a value indicating whether this feature is isUnique.
        /// </param>
        /// <param name="lowerBound">
        /// the lower bound of the feature.
        /// </param>
        /// <param name="upperBound">
        /// the upper bound of the feature. the value -1 is equal to *
        /// </param>
        /// <param name="isMany">
        /// a value indicating whether this feature is isMany.
        /// </param>
        /// <param name="isRequired">
        /// a value indicating whether this feature is isRequired.
        /// </param>
        /// <param name="isContainment">
        /// a value indicating whether this feature is a member of a containment relationship.
        /// </param>
        public EFeatureAttribute(bool isChangeable = true, bool isVolatile = false, bool isTransient = false, bool isUnsettable = false, bool isDerived = false, bool isOrdered = true, bool isUnique = true, int lowerBound = 0, int upperBound = 1, bool isMany = false, bool isRequired = false, bool isContainment = false)
        {
            this.IsChangeable = isChangeable;
            this.IsVolatile = isVolatile;
            this.IsTransient = isTransient;
            this.IsUnsettable = isUnsettable;
            this.IsDerived = isDerived;
            this.IsOrdered = isOrdered;
            this.IsUnique = isUnique;
            this.LowerBound = lowerBound;
            this.UpperBound = upperBound;
            this.IsMany = isMany;
            this.IsRequired = isRequired;
            this.IsContainment = isContainment;
        }

        /// <summary>
        /// Gets or sets the name of the <see cref="EFeatureAttribute"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the <see cref="Type"/> of the <see cref="EFeatureAttribute"/>
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether this is a reference and therefore not an attribute
        /// </summary>
        public bool IsReferemce { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is an attribute and therefore not a reference
        /// </summary>
        public bool IsAttribute { get; set; }
        
        /// <summary>
        /// Gets a value indicating whether this feature is isChangeable.
        /// </summary>
        /// <remarks>
        /// A feature that is not isChangeable will not include a
        /// generated set method, and the reflective eSet()
        /// method will throw an exception if you try to set it.
        /// Declaring one end of a bi-directional relationship to
        /// be not isChangeable is a good way to force clients to
        /// always set the reference from the other end, but still
        /// provide convenient navigation methods from either end.
        /// Declaring one-way references or attributes to be not
        /// isChangeable usually implies that the feature will be set or
        /// changed by some other(user-written) code
        /// </remarks>
        public bool IsChangeable { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this feature is IsVolatile.
        /// </summary>
        /// <remarks>
        /// A feature that is declared isVolatile is generated without
        /// storage fields and with empty implementation method
        /// bodies, which you are isRequired to complete.IsVolatile
        /// is commonly used for a feature whose value is isDerived
        /// from some other feature, or for a feature that is to be
        /// implemented by hand using a different storage and
        /// implementation pattern.
        /// </remarks>
        public bool IsVolatile { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this feature is IsTransient.
        /// </summary>
        /// <remarks>
        /// IsTransient features are used to declare (modeled) data
        /// whose lifetime never spans application invocations and
        /// therefore doesn't need to be persisted. The (default XMI)
        /// serializer will not save features that are declared to be
        /// isTransient.
        /// </remarks>
        public bool IsTransient { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this feature can be unset.
        /// </summary>
        /// <remarks>
        /// When true, the value space for this feature includes the state of being unset.
        /// </remarks>
        public bool IsUnsettable { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this feature is IsDerived.
        /// </summary>
        /// <remarks>
        /// The value of a isDerived feature is computed from other
        /// features, so it doesn't represent any additional object
        /// state.Framework classes, such as EcoreUtil.Copier,
        /// that copy model objects will not attempt to copy such
        /// features.The generated code is unaffected by the value
        /// of the isDerived flag.IsDerived features are typically also
        /// marked isVolatile and isTransient
        /// </remarks>
        public bool IsDerived { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this is isOrdered
        /// </summary>
        public bool IsOrdered { get; private set; }
        
        /// <summary>
        /// Gets a value indicating whether this is isUnique
        /// </summary>
        public bool IsUnique { get; private set; }

        /// <summary>
        /// Gets the lower bound
        /// </summary>
        public int LowerBound { get; private set; }

        /// <summary>
        /// Gets the upper bound
        /// </summary>
        public int UpperBound { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this is isMany
        /// </summary>
        public bool IsMany { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this is isRequired
        /// </summary>
        public bool IsRequired { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this feature is a member of a containment relationship
        /// </summary>
        public bool IsContainment { get; private set; }
    }
}
