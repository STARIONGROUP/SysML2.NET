// ------------------------------------------------------------------------------------------------
// <copyright file="MetamodelProperty.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers
{
    using System;
    using System.Reflection;

    /// <summary>
    /// A non-derived property of the metamodel, as declared on a generated DTO interface
    /// </summary>
    /// <param name="Interface">
    /// The DTO interface that declares the property, e.g. <c>IMembership</c>
    /// </param>
    /// <param name="Property">
    /// The declared property
    /// </param>
    public sealed record MetamodelProperty(Type Interface, PropertyInfo Property)
    {
        /// <summary>
        /// Gets the name of the property as <c>Metaclass.property</c>, e.g. <c>Membership.MemberElement</c>
        /// </summary>
        public string Name => $"{this.Interface.Name[1..]}.{this.Property.Name}";

        /// <summary>
        /// Reads the value of the property from a DTO, through the declaring interface
        /// </summary>
        /// <param name="dto">
        /// The DTO, which implements <see cref="Interface"/>
        /// </param>
        /// <returns>
        /// The value of the property
        /// </returns>
        public object ValueOf(object dto) => this.Property.GetValue(dto);
    }
}
