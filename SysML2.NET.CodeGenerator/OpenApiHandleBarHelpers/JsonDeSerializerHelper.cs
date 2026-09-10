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
                writer.WriteSafeString(QueryInstanceName(QuerySchemaName(arguments, 0))));

            handlebars.RegisterHelper("Property.WriteSeenFlag", (writer, _, arguments) =>
                writer.WriteSafeString($"{QueryLocalName(QuerySchemaName(arguments, 0))}Seen"));

            handlebars.RegisterHelper("Property.WriteLocalName", (writer, _, arguments) =>
                writer.WriteSafeString(QueryLocalName(QuerySchemaName(arguments, 0))));

            handlebars.RegisterHelper("Property.WriteReferencedDeSerializer", (writer, _, arguments) =>
                writer.WriteSafeString($"{QueryPropertySchema(arguments).QueryTerminalReferenceName()}DeSerializer"));

            handlebars.RegisterHelper("Property.WriteElementReader", (writer, _, arguments) =>
                writer.WriteSafeString(queryElementReaderOverride(QuerySchemaName(arguments, 1), QuerySchemaName(arguments, 0))));

            handlebars.RegisterHelper("Property.WriteUnionReader", (writer, _, arguments) =>
                writer.WriteSafeString(queryDispatchedUnionReader(QueryPropertySchema(arguments).QueryTerminalReferenceName()) ?? QueryFamilyReader(arguments)));

            handlebars.RegisterHelper("Property.WriteUnionCast", (writer, _, arguments) =>
                writer.WriteSafeString(queryDispatchedUnionReader(QueryPropertySchema(arguments).QueryTerminalReferenceName()) is null
                    ? $"({OpenApiSchemaExtensions.QueryInterfaceName(QueryPropertySchema(arguments).QueryTerminalReferenceName())})"
                    : string.Empty));

            handlebars.RegisterHelper("Property.WriteEnumerationProvider", (writer, _, arguments) =>
                writer.WriteSafeString($"{OpenApiSchemaExtensions.QueryEnumerationTypeName(QuerySchemaName(arguments, 1), QuerySchemaName(arguments, 0))}Provider"));

            handlebars.RegisterHelper("Property.WriteElementType", (writer, _, arguments) =>
                writer.WriteSafeString(QueryCoreTypeName(arguments) ?? queryElementTypeOverride(QuerySchemaName(arguments, 1), QuerySchemaName(arguments, 0))));

            handlebars.RegisterHelper("Property.QueryIsElementReaderOverridden", (_, arguments) =>
                queryElementReaderOverride(QuerySchemaName(arguments, 1), QuerySchemaName(arguments, 0)) is not null);

            handlebars.RegisterHelper("Property.QueryIsGuid", (_, arguments) =>
                QueryCoreTypeName(arguments) == "Guid" && !QueryPropertySchema(arguments).QueryIsIdentifiedReference());

            handlebars.RegisterHelper("Property.QueryIsDateTime", (_, arguments) => QueryCoreTypeName(arguments) == "DateTime");
        }

        /// <summary>
        /// Queries the name of the local variable that holds the instance being deserialized
        /// </summary>
        /// <param name="className">The name of the class that is deserialized.</param>
        /// <returns>The name of the local variable.</returns>
        private static string QueryInstanceName(string className)
        {
            return char.ToLowerInvariant(className[0]) + className[1..];
        }

        /// <summary>
        /// Queries the name of the local variable that holds the value of a property
        /// </summary>
        /// <param name="propertyName">The name of the property as it appears in the schema.</param>
        /// <returns>The name of the local variable.</returns>
        private static string QueryLocalName(string propertyName)
        {
            var name = OpenApiSchemaExtensions.QueryPropertyName(propertyName);

            return char.ToLowerInvariant(name[0]) + name[1..];
        }

        /// <summary>
        /// Queries the dispatcher method that reads a union whose alternatives are all declared by the document
        /// </summary>
        /// <param name="arguments">The helper arguments, being the property name, the class name and the class schema.</param>
        /// <returns>The dispatcher method.</returns>
        private static string QueryFamilyReader(Arguments arguments)
        {
            return QuerySchemaName(arguments, 1).EndsWith("Request", StringComparison.Ordinal)
                ? "PsmDeSerializationDispatcher.ReadRequest"
                : "PsmDeSerializationDispatcher.ReadResponse";
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
