// ------------------------------------------------------------------------------------------------
// <copyright file="DerivedPropertyAttribute.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
