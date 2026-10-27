// ------------------------------------------------------------------------------------------------
// <copyright file="RedefinedPropertyAttribute.cs" company="Starion Group S.A.">
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
    /// Attribute used to decorate properties when these are redefined properties
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class RedefinedPropertyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedefinedPropertyAttribute"/> class.
        /// </summary>
        public RedefinedPropertyAttribute(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        /// <summary>
        /// Gets or sets the name of the redefined property
        /// </summary>
        public string PropertyName { get; set; }
    }
}
