// -------------------------------------------------------------------------------------------------
// <copyright file="JsonDeSerializerHelper.cs" company="Starion Group S.A.">
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
    /// A handlebars block helper that supports the generation of the JSON deserializers
    /// </summary>
    public static class JsonDeSerializerHelper
    {
        /// <summary>
        /// Registers the <see cref="JsonDeSerializerHelper"/>
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars"/> context with which the helper needs to be registered
        /// </param>
        /// <param name="queryElementReaderOverride">
        /// A function that returns the reader that reads the items of a property whose schema has no C# equivalent
        /// </param>
        /// <param name="queryElementTypeOverride">
        /// A function that returns the C# type of the items of a property whose schema has no C# equivalent
        /// </param>
        /// <param name="queryDispatchedUnionReader">
        /// A function that returns the dispatcher method that reads a union that admits types outside the document
        /// </param>
        public static void RegisterJsonDeSerializerHelper(this IHandlebars handlebars, Func<string, string, string> queryElementReaderOverride, Func<string, string, string> queryElementTypeOverride, Func<string, string> queryDispatchedUnionReader)
        {
            ArgumentNullException.ThrowIfNull(handlebars);
            ArgumentNullException.ThrowIfNull(queryElementReaderOverride);
            ArgumentNullException.ThrowIfNull(queryElementTypeOverride);
            ArgumentNullException.ThrowIfNull(queryDispatchedUnionReader);

            handlebars.RegisterHelper("DeSerializer.WriteInstanceName", (writer, _, arguments) =>
                writer.WriteSafeString(HandleBarArguments.QueryInstanceName(arguments.QuerySchemaName(0))));

            handlebars.RegisterHelper("Property.WriteSeenFlag", (writer, _, arguments) =>
                writer.WriteSafeString($"{HandleBarArguments.QueryLocalName(arguments.QuerySchemaName(0))}Seen"));

            handlebars.RegisterHelper("Property.WriteLocalName", (writer, _, arguments) =>
                writer.WriteSafeString(HandleBarArguments.QueryLocalName(arguments.QuerySchemaName(0))));

            handlebars.RegisterHelper("Property.WriteReferencedDeSerializer", (writer, _, arguments) =>
                writer.WriteSafeString($"{arguments.QueryPropertySchema().QueryTerminalReferenceName()}DeSerializer"));

            handlebars.RegisterHelper("Property.WriteElementReader", (writer, _, arguments) =>
                writer.WriteSafeString(queryElementReaderOverride(arguments.QuerySchemaName(1), arguments.QuerySchemaName(0))));

            handlebars.RegisterHelper("Property.WriteUnionReader", (writer, _, arguments) =>
                writer.WriteSafeString(queryDispatchedUnionReader(arguments.QueryPropertySchema().QueryTerminalReferenceName()) ?? QueryFamilyReader(arguments)));

            handlebars.RegisterHelper("Property.WriteUnionCast", (writer, _, arguments) =>
                writer.WriteSafeString(queryDispatchedUnionReader(arguments.QueryPropertySchema().QueryTerminalReferenceName()) is null
                    ? $"({OpenApiSchemaExtensions.QueryInterfaceName(arguments.QueryPropertySchema().QueryTerminalReferenceName())})"
                    : string.Empty));

            handlebars.RegisterHelper("Property.WriteEnumerationProvider", (writer, _, arguments) =>
                writer.WriteSafeString($"{OpenApiSchemaExtensions.QueryEnumerationTypeName(arguments.QuerySchemaName(1), arguments.QuerySchemaName(0))}Provider"));

            handlebars.RegisterHelper("Property.WriteElementType", (writer, _, arguments) =>
                writer.WriteSafeString(arguments.QueryCoreTypeName() ?? queryElementTypeOverride(arguments.QuerySchemaName(1), arguments.QuerySchemaName(0))));

            handlebars.RegisterHelper("Property.QueryIsElementReaderOverridden", (_, arguments) =>
                queryElementReaderOverride(arguments.QuerySchemaName(1), arguments.QuerySchemaName(0)) is not null);

            handlebars.RegisterHelper("Property.QueryIsGuid", (_, arguments) =>
                arguments.QueryCoreTypeName() == "Guid" && !arguments.QueryPropertySchema().QueryIsIdentifiedReference());

            handlebars.RegisterHelper("Property.QueryIsDateTime", (_, arguments) => arguments.QueryCoreTypeName() == "DateTime");
        }

        /// <summary>
        /// Queries the dispatcher method that reads a union whose alternatives are all declared by the document
        /// </summary>
        /// <param name="arguments">The helper arguments, being the property name, the class name and the class schema.</param>
        /// <returns>The dispatcher method.</returns>
        private static string QueryFamilyReader(Arguments arguments)
        {
            return arguments.QuerySchemaName(1).EndsWith("Request", StringComparison.Ordinal)
                ? "PsmDeSerializationDispatcher.ReadRequest"
                : "PsmDeSerializationDispatcher.ReadResponse";
        }
    }
}
