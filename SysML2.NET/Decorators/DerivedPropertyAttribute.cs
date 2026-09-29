// -------------------------------------------------------------------------------------------------
// <copyright file="DerivedPropertyAttribute.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
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
    /// Attribute used to decorate a method that computes a derived property of the metamodel.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class DerivedPropertyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DerivedPropertyAttribute"/> class.
        /// </summary>
        /// <param name="name">
        /// The name of the derived property on the metaclass interface.
        /// </param>
        public DerivedPropertyAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets the name of the derived property on the metaclass interface.
        /// </summary>
        public string Name { get; set; }
    }
}
