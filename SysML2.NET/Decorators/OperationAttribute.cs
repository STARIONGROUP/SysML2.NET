// ------------------------------------------------------------------------------------------------
// <copyright file="OperationAttribute.cs" company="Starion Group S.A.">
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
    /// Attribute used to decorate a method that implements an operation of the metamodel.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OperationAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationAttribute"/> class.
        /// </summary>
        /// <param name="name">
        /// The name of the operation on the metaclass interface.
        /// </param>
        public OperationAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets the name of the operation on the metaclass interface.
        /// </summary>
        public string Name { get; set; }
    }
}
