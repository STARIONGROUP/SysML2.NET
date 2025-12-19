// -------------------------------------------------------------------------------------------------
// <copyright file="RedefinedByPropertyAttribute.cs" company="Starion Group S.A.">
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
    /// Attribute used to decorate properties that have been redefined
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false)]
    public sealed class RedefinedByPropertyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedefinedByPropertyAttribute"/> class.
        /// </summary>
        /// <param name="propertyName">
        /// the name of the property that is redefining this property
        /// </param>
        /// <remarks>
        /// the property that is decorated with this attribute should be implemented
        /// as an explicit interface
        /// </remarks>
        public RedefinedByPropertyAttribute(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        /// <summary>
        /// Gets or sets the name of the property that is redefining this property
        /// </summary>
        public string PropertyName { get; set; }
    }
}
