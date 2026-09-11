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
                writer.WriteSafeString(HandleBarArguments.QueryInstanceName(arguments.QuerySchemaName(0))));

            handlebars.RegisterHelper("Property.WriteName", (writer, _, arguments) =>
                writer.WriteSafeString(OpenApiSchemaExtensions.QueryPropertyName(arguments.QuerySchemaName(0))));

            handlebars.RegisterHelper("Property.WriteAccessor", (writer, _, arguments) =>
                writer.WriteSafeString($"{HandleBarArguments.QueryInstanceName(arguments.QuerySchemaName(1))}.{OpenApiSchemaExtensions.QueryPropertyName(arguments.QuerySchemaName(0))}"));

            handlebars.RegisterHelper("Property.WriteReferencedSerializer", (writer, _, arguments) =>
                writer.WriteSafeString($"{arguments.QueryPropertySchema().QueryTerminalReferenceName()}Serializer"));

            handlebars.RegisterHelper("Property.WriteEnumerationProvider", (writer, _, arguments) =>
                writer.WriteSafeString($"{OpenApiSchemaExtensions.QueryEnumerationTypeName(arguments.QuerySchemaName(1), arguments.QuerySchemaName(0))}Provider"));

            handlebars.RegisterHelper("Property.WriteElementWriter", (writer, _, arguments) =>
                writer.WriteSafeString(queryElementWriterOverride(arguments.QuerySchemaName(1), arguments.QuerySchemaName(0))));

            handlebars.RegisterHelper("Property.QueryIsOverridden", (_, arguments) =>
                queryElementWriterOverride(arguments.QuerySchemaName(1), arguments.QuerySchemaName(0)) is not null);

            handlebars.RegisterHelper("Property.QueryIsCollection", (_, arguments) =>
                arguments.QueryPropertySchema().QueryIsCollection());

            handlebars.RegisterHelper("Property.QueryIsIdentifiedReference", (_, arguments) =>
                arguments.QueryPropertySchema().QueryIsIdentifiedReference());

            handlebars.RegisterHelper("Property.QueryIsComposite", (_, arguments) =>
                arguments.QueryPropertySchema().QueryIsCompositeReference());

            handlebars.RegisterHelper("Property.QueryIsPolymorphic", (_, arguments) =>
                arguments.QueryPropertySchema().QueryIsPolymorphicReference());

            handlebars.RegisterHelper("Property.QueryIsEnumeration", (_, arguments) =>
                arguments.QueryCoreTypeName() == OpenApiSchemaExtensions.QueryEnumerationTypeName(arguments.QuerySchemaName(1), arguments.QuerySchemaName(0)));

            handlebars.RegisterHelper("Property.QueryIsBoolean", (_, arguments) => arguments.QueryCoreTypeName() == "bool");

            handlebars.RegisterHelper("Property.QueryIsNumeric", (_, arguments) => arguments.QueryCoreTypeName() is "double" or "int");

            handlebars.RegisterHelper("Property.QueryIsUri", (_, arguments) => arguments.QueryCoreTypeName() == "Uri");

            handlebars.RegisterHelper("Property.QueryIsNullableValue", (_, arguments) =>
            {
                var propertySchema = arguments.QueryPropertySchema();

                return propertySchema.QueryIsValueType() && propertySchema.QueryIsNullable(arguments.QueryIsRequired());
            });
        }
    }
}
