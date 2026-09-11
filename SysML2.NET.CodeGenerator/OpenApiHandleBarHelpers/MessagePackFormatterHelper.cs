// -------------------------------------------------------------------------------------------------
// <copyright file="MessagePackFormatterHelper.cs" company="Starion Group S.A.">
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
    using System.Globalization;

    using HandlebarsDotNet;

    using Microsoft.OpenApi;

    using SysML2.NET.CodeGenerator.Extensions;

    /// <summary>
    /// A handlebars block helper that supports the generation of the MessagePack formatters
    /// </summary>
    public static class MessagePackFormatterHelper
    {
        /// <summary>
        /// Registers the <see cref="MessagePackFormatterHelper"/>
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars"/> context with which the helper needs to be registered
        /// </param>
        /// <param name="queryTypeOverride">
        /// A function that returns the C# type of a property whose schema has no C# equivalent
        /// </param>
        /// <param name="queryUnionType">
        /// A function that returns the C# type that a union schema resolves to
        /// </param>
        /// <param name="queryElementReaderOverride">
        /// A function that returns the reader that reads the items of a property whose schema has no C# equivalent
        /// </param>
        /// <param name="queryElementTypeOverride">
        /// A function that returns the C# type of the items of a property whose schema has no C# equivalent
        /// </param>
        public static void RegisterMessagePackFormatterHelper(this IHandlebars handlebars, Func<string, string, string> queryTypeOverride, Func<string, string> queryUnionType, Func<string, string, string> queryElementReaderOverride, Func<string, string, string> queryElementTypeOverride)
        {
            ArgumentNullException.ThrowIfNull(handlebars);
            ArgumentNullException.ThrowIfNull(queryTypeOverride);
            ArgumentNullException.ThrowIfNull(queryUnionType);
            ArgumentNullException.ThrowIfNull(queryElementReaderOverride);
            ArgumentNullException.ThrowIfNull(queryElementTypeOverride);

            handlebars.RegisterHelper("Property.WriteUnionType", (writer, _, arguments) =>
                writer.WriteSafeString(queryUnionType(QueryPropertySchema(arguments).QueryTerminalReferenceName())));

            handlebars.RegisterHelper("Property.WriteElementReader", (writer, _, arguments) =>
                writer.WriteSafeString(queryElementReaderOverride(QuerySchemaName(arguments, 1), QuerySchemaName(arguments, 0))));

            handlebars.RegisterHelper("Schema.WritePropertyCount", (writer, _, arguments) =>
            {
                if (arguments.Length != 2)
                {
                    throw new HandlebarsException("{{#Schema.WritePropertyCount}} helper must have exactly two arguments");
                }

                var schema = arguments[0] as IOpenApiSchema ?? throw new ArgumentException("supposed to be IOpenApiSchema");
                var className = arguments[1] as string ?? throw new ArgumentException("supposed to be a schema name");

                writer.WriteSafeString(schema.QueryMappableProperties(className, queryTypeOverride).Count.ToString(CultureInfo.InvariantCulture));
            });

            handlebars.RegisterHelper("Property.WriteLocalName", (writer, _, arguments) =>
                writer.WriteSafeString(QueryLocalName(QuerySchemaName(arguments, 0))));

            handlebars.RegisterHelper("Property.WriteElementType", (writer, _, arguments) =>
                writer.WriteSafeString(QueryCoreTypeName(arguments) ?? queryElementTypeOverride(QuerySchemaName(arguments, 1), QuerySchemaName(arguments, 0))));

            handlebars.RegisterHelper("Property.QueryIsGuid", (_, arguments) =>
                QueryCoreTypeName(arguments) == "Guid" && !QueryPropertySchema(arguments).QueryIsIdentifiedReference());

            handlebars.RegisterHelper("Property.QueryIsDateTime", (_, arguments) => QueryCoreTypeName(arguments) == "DateTime");
        }

        /// <summary>
        /// Queries the name of the local variable that holds a value read for a property
        /// </summary>
        /// <param name="propertyName">The name of the property as it appears in the schema.</param>
        /// <returns>The name of the local variable.</returns>
        private static string QueryLocalName(string propertyName)
        {
            var name = OpenApiSchemaExtensions.QueryPropertyName(propertyName);

            return char.ToLowerInvariant(name[0]) + name[1..];
        }

        /// <summary>
        /// Queries a name from the helper arguments
        /// </summary>
        /// <param name="arguments">The helper arguments, being the property name, the class name and the class schema.</param>
        /// <param name="index">The index of the argument.</param>
        /// <returns>The name.</returns>
        private static string QuerySchemaName(Arguments arguments, int index)
        {
            if (arguments.Length < index + 1)
            {
                throw new HandlebarsException("the helper is missing arguments");
            }

            return arguments[index] as string ?? throw new ArgumentException("supposed to be a name");
        }

        /// <summary>
        /// Queries the schema of the property that the helper arguments identify
        /// </summary>
        /// <param name="arguments">The helper arguments, being the property name, the class name and the class schema.</param>
        /// <returns>The <see cref="IOpenApiSchema"/> of the property.</returns>
        private static IOpenApiSchema QueryPropertySchema(Arguments arguments)
        {
            return QueryClassSchema(arguments).Properties[QuerySchemaName(arguments, 0)];
        }

        /// <summary>
        /// Queries the schema of the class that the helper arguments identify
        /// </summary>
        /// <param name="arguments">The helper arguments, being the property name, the class name and the class schema.</param>
        /// <returns>The <see cref="IOpenApiSchema"/> of the class.</returns>
        private static IOpenApiSchema QueryClassSchema(Arguments arguments)
        {
            if (arguments.Length != 3)
            {
                throw new HandlebarsException("the helper must have exactly three arguments");
            }

            return arguments[2] as IOpenApiSchema ?? throw new ArgumentException("supposed to be IOpenApiSchema");
        }

        /// <summary>
        /// Queries the C# type of the property that the helper arguments identify
        /// </summary>
        /// <param name="arguments">The helper arguments, being the property name, the class name and the class schema.</param>
        /// <returns>The C# type without its list wrapper or nullable annotation.</returns>
        private static string QueryCoreTypeName(Arguments arguments)
        {
            return QueryPropertySchema(arguments).QueryCoreTypeName(QuerySchemaName(arguments, 1), QuerySchemaName(arguments, 0));
        }
    }
}
