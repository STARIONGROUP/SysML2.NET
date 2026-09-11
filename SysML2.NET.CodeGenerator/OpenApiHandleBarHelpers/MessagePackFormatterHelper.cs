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
                writer.WriteSafeString(queryUnionType(arguments.QueryPropertySchema().QueryTerminalReferenceName())));

            handlebars.RegisterHelper("Property.WriteElementReader", (writer, _, arguments) =>
                writer.WriteSafeString(queryElementReaderOverride(arguments.QuerySchemaName(1), arguments.QuerySchemaName(0))));

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
                writer.WriteSafeString(HandleBarArguments.QueryLocalName(arguments.QuerySchemaName(0))));

            handlebars.RegisterHelper("Property.WriteElementType", (writer, _, arguments) =>
                writer.WriteSafeString(arguments.QueryCoreTypeName() ?? queryElementTypeOverride(arguments.QuerySchemaName(1), arguments.QuerySchemaName(0))));

            handlebars.RegisterHelper("Property.QueryIsGuid", (_, arguments) =>
                arguments.QueryCoreTypeName() == "Guid" && !arguments.QueryPropertySchema().QueryIsIdentifiedReference());

            handlebars.RegisterHelper("Property.QueryIsDateTime", (_, arguments) => arguments.QueryCoreTypeName() == "DateTime");
        }
    }
}
