// -------------------------------------------------------------------------------------------------
// <copyright file="SubsettedPropertyAttribute.cs" company="Starion Group S.A.">
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
    /// Attribute used to decorate properties when these are subsetted properties
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class SubsettedPropertyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubsettedPropertyAttribute"/> class.
        /// </summary>
        /// <param name="propertyName"></param>
        public SubsettedPropertyAttribute(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        /// <summary>
        /// Gets or sets the name of the subsetted property in the following form:
        /// ClassName.PropertyName
        /// </summary>
        public string PropertyName { get; set; }
    }
}
