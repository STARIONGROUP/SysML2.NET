// -------------------------------------------------------------------------------------------------
// <copyright file="ClassAttribute.cs" company="Starion Group S.A.">
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
    /// Attribute used to decorate classes using the properties sourced from
    /// the UML metamodel.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public sealed class ClassAttribute : Attribute
    {
        /// <summary>
        /// initializes a new instance of the <see cref="ClassAttribute"/> class.
        /// </summary>
        /// <param name="xmiId">
        /// The unique identifier
        /// </param>
        /// <param name="isAbstract">
        /// a value indicating that If true, the Class does not provide a complete declaration and cannot
        /// be instantiated. An abstract Class is typically used as a target of Associations or Generalizations
        /// </param>
        /// <param name="isFinalSpecialization">
        /// If true, the Classifier cannot be specialized
        /// </param>
        /// <param name="isActive">
        /// Determines whether an object specified by this Class is active or not. If true, then the owning Class is referred
        /// to as an active Class. If false, then such a Class is referred to as a passive Class
        /// </param>
        public ClassAttribute(string xmiId = "", bool isAbstract = false, bool isFinalSpecialization = false, bool isActive = false)
        {
            this.XmiId = xmiId;
            this.IsAbstract = isAbstract;
            this.IsFinalSpecialization = isFinalSpecialization;
            this.IsActive = isActive;
        }

        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        public string XmiId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating that if true, the Class does not provide a complete declaration and cannot
        /// be instantiated. An abstract Class is typically used as a target of Associations or Generalizations
        /// </summary>
        public bool IsAbstract { get; set; }

        /// <summary>
        /// Gets or set a value indicating that if true, the Classifier cannot be specialized
        /// </summary>
        public bool IsFinalSpecialization { get; set; }

        /// <summary>
        /// Determines whether an object specified by this Class is active or not. If true, then the owning Class is referred
        /// to as an active Class. If false, then such a Class is referred to as a passive Class
        /// </summary>
        public bool IsActive { get; set; }
    }
}
