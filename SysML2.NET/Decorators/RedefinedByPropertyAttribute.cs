// ------------------------------------------------------------------------------------------------
// <copyright file="RedefinedByPropertyAttribute.cs" company="Starion Group S.A.">
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
