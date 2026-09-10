// -------------------------------------------------------------------------------------------------
// <copyright file="JsonSerializerHelper.cs" company="Starion Group S.A.">
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
    /// A handlebars block helper that supports the generation of the JSON serializers
    /// </summary>
    public static class JsonSerializerHelper
    {
        /// <summary>
        /// Registers the <see cref="JsonSerializerHelper"/>
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars"/> context with which the helper needs to be registered
        /// </param>
        /// <param name="queryElementWriterOverride">
        /// A function that returns the writer that emits the items of a property whose schema has no C# equivalent
        /// </param>
        public static void RegisterJsonSerializerHelper(this IHandlebars handlebars, Func<string, string, string> queryElementWriterOverride)
        {
            ArgumentNullException.ThrowIfNull(handlebars);
            ArgumentNullException.ThrowIfNull(queryElementWriterOverride);

            handlebars.RegisterHelper("Serializer.WriteInstanceName", (writer, _, arguments) =>
                writer.WriteSafeString(QueryInstanceName(QuerySchemaName(arguments, 0))));

            handlebars.RegisterHelper("Property.WriteName", (writer, _, arguments) =>
                writer.WriteSafeString(OpenApiSchemaExtensions.QueryPropertyName(QuerySchemaName(arguments, 0))));

            handlebars.RegisterHelper("Property.WriteAccessor", (writer, _, arguments) =>
                writer.WriteSafeString($"{QueryInstanceName(QuerySchemaName(arguments, 1))}.{OpenApiSchemaExtensions.QueryPropertyName(QuerySchemaName(arguments, 0))}"));

            handlebars.RegisterHelper("Property.WriteReferencedSerializer", (writer, _, arguments) =>
                writer.WriteSafeString($"{QueryPropertySchema(arguments).QueryTerminalReferenceName()}Serializer"));

            handlebars.RegisterHelper("Property.WriteEnumerationProvider", (writer, _, arguments) =>
                writer.WriteSafeString($"{OpenApiSchemaExtensions.QueryEnumerationTypeName(QuerySchemaName(arguments, 1), QuerySchemaName(arguments, 0))}Provider"));

            handlebars.RegisterHelper("Property.WriteElementWriter", (writer, _, arguments) =>
                writer.WriteSafeString(queryElementWriterOverride(QuerySchemaName(arguments, 1), QuerySchemaName(arguments, 0))));

            handlebars.RegisterHelper("Property.QueryIsOverridden", (_, arguments) =>
                queryElementWriterOverride(QuerySchemaName(arguments, 1), QuerySchemaName(arguments, 0)) is not null);

            handlebars.RegisterHelper("Property.QueryIsCollection", (_, arguments) =>
                QueryPropertySchema(arguments).QueryIsCollection());

            handlebars.RegisterHelper("Property.QueryIsIdentifiedReference", (_, arguments) =>
                QueryPropertySchema(arguments).QueryIsIdentifiedReference());

            handlebars.RegisterHelper("Property.QueryIsComposite", (_, arguments) =>
                QueryPropertySchema(arguments).QueryIsCompositeReference());

            handlebars.RegisterHelper("Property.QueryIsPolymorphic", (_, arguments) =>
                QueryPropertySchema(arguments).QueryIsPolymorphicReference());

            handlebars.RegisterHelper("Property.QueryIsEnumeration", (_, arguments) =>
                QueryCoreTypeName(arguments) == OpenApiSchemaExtensions.QueryEnumerationTypeName(QuerySchemaName(arguments, 1), QuerySchemaName(arguments, 0)));

            handlebars.RegisterHelper("Property.QueryIsBoolean", (_, arguments) => QueryCoreTypeName(arguments) == "bool");

            handlebars.RegisterHelper("Property.QueryIsNumeric", (_, arguments) => QueryCoreTypeName(arguments) is "double" or "int");

            handlebars.RegisterHelper("Property.QueryIsUri", (_, arguments) => QueryCoreTypeName(arguments) == "Uri");

            handlebars.RegisterHelper("Property.QueryIsNullableValue", (_, arguments) =>
            {
                var propertySchema = QueryPropertySchema(arguments);

                return propertySchema.QueryIsValueType() && propertySchema.QueryIsNullable(QueryIsRequired(arguments));
            });
        }

        /// <summary>
        /// Queries the name of the local variable that holds the instance being serialized
        /// </summary>
        /// <param name="className">The name of the class that is serialized.</param>
        /// <returns>The name of the local variable.</returns>
        private static string QueryInstanceName(string className)
        {
            return char.ToLowerInvariant(className[0]) + className[1..];
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
        /// Queries whether the class lists the property as required
        /// </summary>
        /// <param name="arguments">The helper arguments, being the property name, the class name and the class schema.</param>
        /// <returns>True when the property is required.</returns>
        private static bool QueryIsRequired(Arguments arguments)
        {
            return QueryClassSchema(arguments).Required?.Contains(QuerySchemaName(arguments, 0)) == true;
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
