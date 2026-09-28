// -------------------------------------------------------------------------------------------------
// <copyright file="OpenApiSchemaExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.OpenApi;

    /// <summary>
    /// Extension methods for the <see cref="IOpenApiSchema"/> interface that resolve JSON Schema constructs to C# code
    /// </summary>
    public static class OpenApiSchemaExtensions
    {
        /// <summary>
        /// The name of the schema whose sole purpose is to carry the identifier of a referenced element
        /// </summary>
        public const string IdentifiedSchemaName = "Identified";

        /// <summary>
        /// The name of the property that carries the identifier of an element
        /// </summary>
        public const string IdentifierPropertyName = "@id";

        /// <summary>
        /// The name of the property that carries the type discriminator of an element
        /// </summary>
        public const string TypeDiscriminatorPropertyName = "@type";

        /// <summary>
        /// The C# identifiers used for the symbolic enumeration values, which have no identifier form of their own
        /// </summary>
        private static readonly Dictionary<string, string> SymbolicLiteralNames = new(StringComparer.Ordinal)
        {
            ["<"] = "lessthan",
            ["<="] = "lessthanorequalto",
            ["="] = "equalto",
            [">"] = "greaterthan",
            [">="] = "greaterthanorequalto"
        };

        /// <summary>
        /// Queries whether the schema is a union that enumerates its alternatives using <c>oneOf</c>
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <returns>True when the schema carries alternatives but no properties of its own.</returns>
        public static bool QueryIsUnion(this IOpenApiSchema schema)
        {
            ArgumentNullException.ThrowIfNull(schema);

            return schema.OneOf?.Count > 0 && (schema.Properties?.Count ?? 0) == 0;
        }

        /// <summary>
        /// Queries the names of the schemas that the union enumerates as its alternatives
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <returns>The referenced schema names, in declaration order.</returns>
        public static IReadOnlyList<string> QueryUnionAlternativeNames(this IOpenApiSchema schema)
        {
            ArgumentNullException.ThrowIfNull(schema);

            return schema.OneOf is null
                ? []
                : schema.OneOf.OfType<OpenApiSchemaReference>()
                    .Select(alternative => alternative.Reference?.Id)
                    .Where(alternativeName => !string.IsNullOrWhiteSpace(alternativeName))
                    .ToList();
        }

        /// <summary>
        /// Queries the value of the <c>@type</c> discriminator declared by the schema
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <returns>The discriminator value, or <c>null</c> when the schema declares none.</returns>
        public static string QueryTypeDiscriminator(this IOpenApiSchema schema)
        {
            ArgumentNullException.ThrowIfNull(schema);

            return schema.Properties is not null && schema.Properties.TryGetValue(TypeDiscriminatorPropertyName, out var discriminator)
                ? discriminator.Const
                : null;
        }

        /// <summary>
        /// Queries whether the schema resolves to a reference that stands for the identifier of another element
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <returns>True when the schema resolves to a reference to <see cref="IdentifiedSchemaName"/>.</returns>
        public static bool QueryIsIdentifiedReference(this IOpenApiSchema schema)
        {
            ArgumentNullException.ThrowIfNull(schema);

            return schema.QueryTerminalReferenceInternal()?.Reference?.Id == IdentifiedSchemaName;
        }

        /// <summary>
        /// Queries whether the schema resolves to a reference to another generated class
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <returns>True when the schema resolves to a reference that is neither an identifier nor a union.</returns>
        public static bool QueryIsCompositeReference(this IOpenApiSchema schema)
        {
            ArgumentNullException.ThrowIfNull(schema);

            var terminalReference = schema.QueryTerminalReferenceInternal();

            return terminalReference is not null && terminalReference.Reference?.Id != IdentifiedSchemaName && !terminalReference.QueryIsUnion();
        }

        /// <summary>
        /// Queries whether the schema resolves to a reference to a union, whose runtime type decides the serializer
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <returns>True when the schema resolves to a reference to a union.</returns>
        public static bool QueryIsPolymorphicReference(this IOpenApiSchema schema)
        {
            ArgumentNullException.ThrowIfNull(schema);

            var terminalReference = schema.QueryTerminalReferenceInternal();

            return terminalReference is not null && terminalReference.QueryIsUnion();
        }

        /// <summary>
        /// Queries the name of the schema that the reference resolves to
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <returns>The referenced schema name, or <c>null</c> when the schema does not resolve to a reference.</returns>
        public static string QueryTerminalReferenceName(this IOpenApiSchema schema)
        {
            ArgumentNullException.ThrowIfNull(schema);

            return schema.QueryTerminalReferenceInternal()?.Reference?.Id;
        }

        /// <summary>
        /// Queries whether the schema declares an identifier that it lists as required
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <returns>True when the identifier is declared and required.</returns>
        public static bool QueryHasRequiredIdentifier(this IOpenApiSchema schema)
        {
            ArgumentNullException.ThrowIfNull(schema);

            return schema.Properties?.ContainsKey(IdentifierPropertyName) == true
                   && schema.Required?.Contains(IdentifierPropertyName) == true;
        }

        /// <summary>
        /// Queries the properties of the schema that can be expressed as C# properties
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <param name="className">The name of the class generated for the schema.</param>
        /// <param name="queryTypeOverride">A function that supplies the declared type of a property whose schema has no C# equivalent.</param>
        /// <returns>The mappable properties, ordered by their name.</returns>
        public static IReadOnlyList<KeyValuePair<string, IOpenApiSchema>> QueryMappableProperties(this IOpenApiSchema schema, string className, Func<string, string, string> queryTypeOverride = null)
        {
            ArgumentNullException.ThrowIfNull(schema);
            ArgumentException.ThrowIfNullOrWhiteSpace(className);

            return schema.Properties is null
                ? []
                : schema.Properties
                    .Where(property => property.Key != TypeDiscriminatorPropertyName)
                    .Where(property => queryTypeOverride?.Invoke(className, property.Key) is not null || property.Value.QueryIsMappable(className, property.Key))
                    .OrderBy(property => property.Key, StringComparer.Ordinal)
                    .ToList();
        }

        /// <summary>
        /// Queries the properties of the schema that declare an inline set of allowed values
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <param name="className">The name of the class generated for the schema.</param>
        /// <returns>The generated enumeration name paired with the property schema that declares it.</returns>
        public static IReadOnlyList<KeyValuePair<string, IOpenApiSchema>> QueryEnumerations(this IOpenApiSchema schema, string className)
        {
            ArgumentNullException.ThrowIfNull(schema);
            ArgumentException.ThrowIfNullOrWhiteSpace(className);

            return schema.Properties is null
                ? []
                : schema.Properties
                    .Where(property => property.Value is not OpenApiSchemaReference && property.Value.Enum?.Count > 0)
                    .OrderBy(property => property.Key, StringComparer.Ordinal)
                    .Select(property => new KeyValuePair<string, IOpenApiSchema>(QueryEnumerationTypeName(className, property.Key), property.Value))
                    .ToList();
        }

        /// <summary>
        /// Queries the allowed values declared by the schema
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <returns>The allowed values, in declaration order.</returns>
        public static IReadOnlyList<string> QueryEnumerationValues(this IOpenApiSchema schema)
        {
            ArgumentNullException.ThrowIfNull(schema);

            return schema.Enum is null
                ? []
                : schema.Enum.Select(value => value.GetValue<string>()).ToList();
        }

        /// <summary>
        /// Queries the length in UTF-8 bytes of the longest allowed value declared by the schema
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <returns>The length of the longest allowed value, or zero when the schema declares none.</returns>
        public static int QueryLongestEnumerationValueByteLength(this IOpenApiSchema schema)
        {
            ArgumentNullException.ThrowIfNull(schema);

            var byteLengths = schema.QueryEnumerationValues().Select(QueryEnumerationValueByteLength).ToList();

            return byteLengths.Count == 0 ? 0 : byteLengths.Max();
        }

        /// <summary>
        /// Queries the length in UTF-8 bytes of an allowed value
        /// </summary>
        /// <param name="enumerationValue">The allowed value as it appears in the schema.</param>
        /// <returns>The length in UTF-8 bytes.</returns>
        public static int QueryEnumerationValueByteLength(string enumerationValue)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(enumerationValue);

            return Encoding.UTF8.GetByteCount(enumerationValue);
        }

        /// <summary>
        /// Queries whether the schema can be expressed as a C# type
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <param name="className">The name of the class that declares the property.</param>
        /// <param name="propertyName">The name of the property that the schema defines.</param>
        /// <returns>True when a C# type could be resolved.</returns>
        public static bool QueryIsMappable(this IOpenApiSchema schema, string className, string propertyName)
        {
            ArgumentNullException.ThrowIfNull(schema);

            return schema.QueryCoreTypeName(className, propertyName) is not null;
        }

        /// <summary>
        /// Queries whether the schema declares a list rather than a single value
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <returns>True when the schema resolves to an array.</returns>
        public static bool QueryIsCollection(this IOpenApiSchema schema)
        {
            ArgumentNullException.ThrowIfNull(schema);

            var unwrapped = schema.QueryUnwrapped();

            return unwrapped is not null and not OpenApiSchemaReference && unwrapped.Type?.HasFlag(JsonSchemaType.Array) == true;
        }

        /// <summary>
        /// Queries whether the value of the schema may be absent or explicitly null
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <param name="isRequired">A value indicating whether the declaring schema lists the property as required.</param>
        /// <returns>True when the property is optional or admits a null value.</returns>
        public static bool QueryIsNullable(this IOpenApiSchema schema, bool isRequired)
        {
            ArgumentNullException.ThrowIfNull(schema);

            if (!isRequired)
            {
                return true;
            }

            return schema is not OpenApiSchemaReference
                   && schema.OneOf?.Any(branch => branch is not OpenApiSchemaReference && branch.Type == JsonSchemaType.Null) == true;
        }

        /// <summary>
        /// Queries the C# type of the schema, without its list wrapper or nullable annotation
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <param name="className">The name of the class that declares the property.</param>
        /// <param name="propertyName">The name of the property that the schema defines.</param>
        /// <returns>The C# type name, or <c>null</c> when the schema has no C# equivalent.</returns>
        public static string QueryCoreTypeName(this IOpenApiSchema schema, string className, string propertyName)
        {
            ArgumentNullException.ThrowIfNull(schema);

            if (schema is OpenApiSchemaReference reference)
            {
                return reference.QueryReferencedTypeName();
            }

            var unwrapped = schema.QueryUnwrapped();

            if (unwrapped is null)
            {
                return null;
            }

            if (unwrapped is OpenApiSchemaReference unwrappedReference)
            {
                return unwrappedReference.QueryReferencedTypeName();
            }

            if (unwrapped.Type?.HasFlag(JsonSchemaType.Array) == true)
            {
                return unwrapped.Items?.QueryIsCollection() == true
                    ? null
                    : unwrapped.Items?.QueryCoreTypeName(className, propertyName);
            }

            return unwrapped.QueryScalarTypeName(className, propertyName);
        }

        /// <summary>
        /// Queries the declared C# type of the schema, including its list wrapper and nullable annotation
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <param name="className">The name of the class that declares the property.</param>
        /// <param name="propertyName">The name of the property that the schema defines.</param>
        /// <param name="isRequired">A value indicating whether the declaring schema lists the property as required.</param>
        /// <param name="queryTypeOverride">A function that supplies the declared type, taking precedence over the schema.</param>
        /// <returns>The declared C# type name, or <c>null</c> when the schema has no C# equivalent.</returns>
        public static string QueryCSharpTypeName(this IOpenApiSchema schema, string className, string propertyName, bool isRequired, Func<string, string, string> queryTypeOverride = null)
        {
            ArgumentNullException.ThrowIfNull(schema);

            var overriddenTypeName = queryTypeOverride?.Invoke(className, propertyName);

            if (overriddenTypeName is not null)
            {
                return overriddenTypeName;
            }

            var coreTypeName = schema.QueryCoreTypeName(className, propertyName);

            if (coreTypeName is null)
            {
                return null;
            }

            if (schema.QueryIsCollection())
            {
                return $"List<{coreTypeName}>";
            }

            return schema.QueryIsNullable(isRequired) && schema.QueryIsValueType()
                ? $"{coreTypeName}?"
                : coreTypeName;
        }

        /// <summary>
        /// Queries whether the schema resolves to a C# value type, and can therefore carry a nullable annotation
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <returns>True when the schema resolves to a value type.</returns>
        public static bool QueryIsValueType(this IOpenApiSchema schema)
        {
            ArgumentNullException.ThrowIfNull(schema);

            if (schema is OpenApiSchemaReference reference)
            {
                return reference.Reference?.Id == IdentifiedSchemaName;
            }

            var unwrapped = schema.QueryUnwrapped();

            if (unwrapped is OpenApiSchemaReference unwrappedReference)
            {
                return unwrappedReference.Reference?.Id == IdentifiedSchemaName;
            }

            if (unwrapped?.Type is null || unwrapped.Type.Value.HasFlag(JsonSchemaType.Array))
            {
                return false;
            }

            return (unwrapped.Type.Value & ~JsonSchemaType.Null) switch
            {
                JsonSchemaType.String => unwrapped.Format is "uuid" or "date-time" || unwrapped.Enum?.Count > 0,
                JsonSchemaType.Boolean or JsonSchemaType.Number or JsonSchemaType.Integer => true,
                _ => false
            };
        }

        /// <summary>
        /// Queries the name of the schema that a reference to <see cref="IdentifiedSchemaName"/> stands for
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <returns>The referenced schema name, or <c>null</c> when the schema carries no such annotation.</returns>
        public static string QueryReferencedSchemaName(this IOpenApiSchema schema)
        {
            ArgumentNullException.ThrowIfNull(schema);

            var annotated = schema.QueryTerminalReferenceInternal();

            if (string.IsNullOrWhiteSpace(annotated?.Comment))
            {
                return null;
            }

            var segments = annotated.Comment.Split('/', StringSplitOptions.RemoveEmptyEntries);

            return segments.Length == 0 ? null : segments[^1];
        }

        /// <summary>
        /// Queries the name of the enumeration generated for an inline set of allowed values
        /// </summary>
        /// <param name="className">The name of the class that declares the property.</param>
        /// <param name="propertyName">The name of the property that declares the allowed values.</param>
        /// <returns>The name of the generated enumeration.</returns>
        public static string QueryEnumerationTypeName(string className, string propertyName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(className);
            ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

            return $"{className}{QueryPropertyName(propertyName)}";
        }

        /// <summary>
        /// Queries the C# name of a property declared by a schema
        /// </summary>
        /// <param name="propertyName">The name of the property as it appears in the schema.</param>
        /// <returns>The C# property name.</returns>
        public static string QueryPropertyName(string propertyName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

            var trimmed = propertyName.TrimStart('@');

            return trimmed.Length == 0 ? trimmed : char.ToUpperInvariant(trimmed[0]) + trimmed[1..];
        }

        /// <summary>
        /// Queries the C# name of the interface generated for a union schema
        /// </summary>
        /// <param name="schemaName">The name of the union schema.</param>
        /// <returns>The name of the generated interface.</returns>
        public static string QueryInterfaceName(string schemaName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(schemaName);

            return $"I{schemaName}";
        }

        /// <summary>
        /// Queries the C# name of an enumeration literal
        /// </summary>
        /// <param name="enumerationValue">The allowed value as it appears in the schema.</param>
        /// <returns>The C# literal name.</returns>
        public static string QueryEnumerationLiteralName(string enumerationValue)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(enumerationValue);

            return SymbolicLiteralNames.TryGetValue(enumerationValue, out var symbolicName)
                ? symbolicName
                : ReservedCSharpNameMapper.Map(enumerationValue);
        }

        /// <summary>
        /// Queries the reference that the schema resolves to, looking through a nullable or list wrapper
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <returns>The terminal reference, or <c>null</c> when the schema does not resolve to one.</returns>
        public static OpenApiSchemaReference QueryTerminalReference(this IOpenApiSchema schema)
        {
            ArgumentNullException.ThrowIfNull(schema);

            return schema.QueryTerminalReferenceInternal();
        }

        /// <summary>
        /// Queries the reference that the schema resolves to, looking through a nullable or list wrapper
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <returns>The terminal reference, or <c>null</c> when the schema does not resolve to one.</returns>
        private static OpenApiSchemaReference QueryTerminalReferenceInternal(this IOpenApiSchema schema)
        {
            if (schema is OpenApiSchemaReference reference)
            {
                return reference;
            }

            var unwrapped = schema.QueryUnwrapped();

            if (unwrapped is OpenApiSchemaReference unwrappedReference)
            {
                return unwrappedReference;
            }

            return unwrapped?.Type?.HasFlag(JsonSchemaType.Array) == true
                ? unwrapped.Items?.QueryTerminalReferenceInternal()
                : null;
        }

        /// <summary>
        /// Queries the single meaningful alternative of the schema, discarding a <c>null</c> alternative
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <returns>The schema itself, its sole non-null alternative, or <c>null</c> when several alternatives remain.</returns>
        private static IOpenApiSchema QueryUnwrapped(this IOpenApiSchema schema)
        {
            if (schema is OpenApiSchemaReference || !(schema.OneOf?.Count > 0))
            {
                return schema;
            }

            var alternatives = schema.OneOf
                .Where(branch => branch is OpenApiSchemaReference || branch.Type != JsonSchemaType.Null)
                .ToList();

            return alternatives.Count == 1 ? alternatives[0].QueryUnwrapped() : null;
        }

        /// <summary>
        /// Queries the C# type that the referenced schema is generated as
        /// </summary>
        /// <param name="reference">The subject <see cref="OpenApiSchemaReference"/>.</param>
        /// <returns>The C# type name, or <c>null</c> when the reference carries no target.</returns>
        private static string QueryReferencedTypeName(this OpenApiSchemaReference reference)
        {
            var referencedName = reference.Reference?.Id;

            if (string.IsNullOrWhiteSpace(referencedName))
            {
                return null;
            }

            if (referencedName == IdentifiedSchemaName)
            {
                return "Guid";
            }

            return reference.QueryIsUnion() ? QueryInterfaceName(referencedName) : referencedName;
        }

        /// <summary>
        /// Queries the C# type of a schema that declares neither a reference nor an array
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <param name="className">The name of the class that declares the property.</param>
        /// <param name="propertyName">The name of the property that the schema defines.</param>
        /// <returns>The C# type name, or <c>null</c> when the schema has no C# equivalent.</returns>
        private static string QueryScalarTypeName(this IOpenApiSchema schema, string className, string propertyName)
        {
            if (schema.Type is null)
            {
                return null;
            }

            return (schema.Type.Value & ~JsonSchemaType.Null) switch
            {
                JsonSchemaType.String => schema.QueryStringTypeName(className, propertyName),
                JsonSchemaType.Boolean => "bool",
                JsonSchemaType.Number => "double",
                JsonSchemaType.Integer => "int",
                _ => null
            };
        }

        /// <summary>
        /// Queries the C# type of a string schema, honouring its format and its allowed values
        /// </summary>
        /// <param name="schema">The subject <see cref="IOpenApiSchema"/>.</param>
        /// <param name="className">The name of the class that declares the property.</param>
        /// <param name="propertyName">The name of the property that the schema defines.</param>
        /// <returns>The C# type name.</returns>
        private static string QueryStringTypeName(this IOpenApiSchema schema, string className, string propertyName)
        {
            switch (schema.Format)
            {
                case "uuid":
                    return "Guid";
                case "date-time":
                    return "DateTime";
                case "uri":
                    return "Uri";
                default:
                    return schema.Enum?.Count > 0 && !string.IsNullOrWhiteSpace(className) && !string.IsNullOrWhiteSpace(propertyName)
                        ? QueryEnumerationTypeName(className, propertyName)
                        : "string";
            }
        }
    }
}
