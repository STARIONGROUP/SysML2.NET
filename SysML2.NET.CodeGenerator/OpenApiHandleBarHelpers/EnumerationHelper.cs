// -------------------------------------------------------------------------------------------------
// <copyright file="EnumerationHelper.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.CodeGenerator.OpenApiHandleBarHelpers
{
    using System;

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
        }
    }
}
