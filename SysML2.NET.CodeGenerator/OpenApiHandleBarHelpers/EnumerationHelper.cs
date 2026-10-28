// ------------------------------------------------------------------------------------------------
// <copyright file="EnumerationHelper.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.OpenApiHandleBarHelpers
{
    using System;
    using System.Globalization;

    using HandlebarsDotNet;

    using Microsoft.OpenApi;

    using SysML2.NET.CodeGenerator.Extensions;

    /// <summary>
    /// A handlebars block helper that writes the enumerations declared inline by an <see cref="IOpenApiSchema"/>
    /// </summary>
    public static class EnumerationHelper
    {
        /// <summary>
        /// Registers the <see cref="EnumerationHelper"/>
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars"/> context with which the helper needs to be registered
        /// </param>
        public static void RegisterEnumerationHelper(this IHandlebars handlebars)
        {
            ArgumentNullException.ThrowIfNull(handlebars);

            handlebars.RegisterHelper("Enumeration.QueryValues", (_, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#Enumeration.QueryValues}} helper must have exactly one argument");
                }

                var schema = arguments[0] as IOpenApiSchema ?? throw new ArgumentException("supposed to be IOpenApiSchema");

                return schema.QueryEnumerationValues();
            });

            handlebars.RegisterHelper("Enumeration.WriteLiteralName", (writer, _, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#Enumeration.WriteLiteralName}} helper must have exactly one argument");
                }

                var enumerationValue = arguments[0] as string ?? throw new ArgumentException("supposed to be an enumeration value");

                writer.WriteSafeString(OpenApiSchemaExtensions.QueryEnumerationLiteralName(enumerationValue));
            });

            handlebars.RegisterHelper("Enumeration.WriteValueLength", (writer, _, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#Enumeration.WriteValueLength}} helper must have exactly one argument");
                }

                var enumerationValue = arguments[0] as string ?? throw new ArgumentException("supposed to be an enumeration value");

                writer.WriteSafeString(enumerationValue.Length.ToString(CultureInfo.InvariantCulture));
            });

            handlebars.RegisterHelper("Enumeration.WriteValueByteLength", (writer, _, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#Enumeration.WriteValueByteLength}} helper must have exactly one argument");
                }

                var enumerationValue = arguments[0] as string ?? throw new ArgumentException("supposed to be an enumeration value");

                writer.WriteSafeString(OpenApiSchemaExtensions.QueryEnumerationValueByteLength(enumerationValue).ToString(CultureInfo.InvariantCulture));
            });

            handlebars.RegisterHelper("Enumeration.WriteLongestValueByteLength", (writer, _, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#Enumeration.WriteLongestValueByteLength}} helper must have exactly one argument");
                }

                var schema = arguments[0] as IOpenApiSchema ?? throw new ArgumentException("supposed to be IOpenApiSchema");

                writer.WriteSafeString(schema.QueryLongestEnumerationValueByteLength().ToString(CultureInfo.InvariantCulture));
            });
        }
    }
}
