// -------------------------------------------------------------------------------------------------
// <copyright file="OpenApiSchemaInspector.cs" company="Starion Group S.A.">
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

    using Microsoft.OpenApi;

    /// <summary>
    /// Inspects a set of schemas and determines the smallest set that covers every variation the generator emits
    /// </summary>
    public static class OpenApiSchemaInspector
    {
        /// <summary>
        /// The C# types that the generator emits without introducing a type of its own
        /// </summary>
        private static readonly HashSet<string> IntrinsicTypeNames =
            new(StringComparer.Ordinal) { "Guid", "DateTime", "Uri", "string", "bool", "double", "int" };

        /// <summary>
        /// Inspects the provided schemas and returns those that should be used when writing code generation tests
        /// </summary>
        /// <param name="schemas">
        /// The schemas that are to be inspected
        /// </param>
        /// <returns>
        /// The schemas that together cover every property variation the generator emits
        /// </returns>
        public static IReadOnlyList<string> QueryInterestingSchemas(IReadOnlyList<KeyValuePair<string, IOpenApiSchema>> schemas)
        {
            ArgumentNullException.ThrowIfNull(schemas);

            var schemaVariations = MapSchemaPropertyVariation(schemas);

            return ReduceSchemaPropertyVariationToInterestingSchemas(schemaVariations);
        }

        /// <summary>
        /// Maps every schema to the set of variations that its properties exhibit
        /// </summary>
        /// <param name="schemas">
        /// The schemas that are to be inspected
        /// </param>
        /// <returns>
        /// The variations exhibited by each schema
        /// </returns>
        private static Dictionary<string, HashSet<string>> MapSchemaPropertyVariation(IReadOnlyList<KeyValuePair<string, IOpenApiSchema>> schemas)
        {
            var unionInterfaceNames = schemas
                .Where(schema => schema.Value.QueryIsUnion())
                .Select(schema => OpenApiSchemaExtensions.QueryInterfaceName(schema.Key))
                .ToHashSet(StringComparer.Ordinal);

            var schemaVariations = new Dictionary<string, HashSet<string>>(StringComparer.Ordinal);

            foreach (var schema in schemas)
            {
                var variations = new HashSet<string>(StringComparer.Ordinal);

                schemaVariations[schema.Key] = variations;

                if (schema.Value.QueryIsUnion())
                {
                    variations.Add("UNION");

                    continue;
                }

                if (!string.IsNullOrWhiteSpace(schema.Value.QueryTypeDiscriminator()))
                {
                    variations.Add("DISCRIMINATOR");
                }

                foreach (var property in schema.Value.Properties ?? new Dictionary<string, IOpenApiSchema>())
                {
                    if (property.Key == OpenApiSchemaExtensions.TypeDiscriminatorPropertyName)
                    {
                        continue;
                    }

                    var isRequired = schema.Value.Required?.Contains(property.Key) == true;

                    variations.Add(QueryPropertyVariation(property.Value, schema.Key, property.Key, isRequired, unionInterfaceNames));
                }
            }

            return schemaVariations;
        }

        /// <summary>
        /// Queries the variation token that characterizes how the generator emits the provided property
        /// </summary>
        /// <param name="propertySchema">
        /// The <see cref="IOpenApiSchema"/> that defines the property
        /// </param>
        /// <param name="className">
        /// The name of the class that declares the property
        /// </param>
        /// <param name="propertyName">
        /// The name of the property as it appears in the schema
        /// </param>
        /// <param name="isRequired">
        /// A value indicating whether the declaring schema lists the property as required
        /// </param>
        /// <param name="unionInterfaceNames">
        /// The names of the interfaces generated for the union schemas
        /// </param>
        /// <returns>
        /// The variation token
        /// </returns>
        private static string QueryPropertyVariation(IOpenApiSchema propertySchema, string className, string propertyName, bool isRequired, ICollection<string> unionInterfaceNames)
        {
            var coreTypeName = propertySchema.QueryCoreTypeName(className, propertyName);

            if (coreTypeName is null)
            {
                return "UNMAPPED";
            }

            var category = QueryVariationCategory(coreTypeName, className, propertyName, unionInterfaceNames);

            return propertySchema.QueryIsCollection()
                ? $"LIST:{category}"
                : $"SCALAR:{category}:{propertySchema.QueryIsNullable(isRequired) && propertySchema.QueryIsValueType()}";
        }

        /// <summary>
        /// Queries the category of the C# type that the generator emits for a property
        /// </summary>
        /// <param name="coreTypeName">
        /// The C# type without its list wrapper or nullable annotation
        /// </param>
        /// <param name="className">
        /// The name of the class that declares the property
        /// </param>
        /// <param name="propertyName">
        /// The name of the property as it appears in the schema
        /// </param>
        /// <param name="unionInterfaceNames">
        /// The names of the interfaces generated for the union schemas
        /// </param>
        /// <returns>
        /// The category of the C# type
        /// </returns>
        private static string QueryVariationCategory(string coreTypeName, string className, string propertyName, ICollection<string> unionInterfaceNames)
        {
            if (IntrinsicTypeNames.Contains(coreTypeName))
            {
                return coreTypeName;
            }

            if (coreTypeName == OpenApiSchemaExtensions.QueryEnumerationTypeName(className, propertyName))
            {
                return "ENUM";
            }

            return unionInterfaceNames.Contains(coreTypeName) ? "INTERFACE" : "CLASS";
        }

        /// <summary>
        /// Reduces the schemas and their variations in a greedy fashion such that the least amount of schemas is returned
        /// </summary>
        /// <param name="schemaVariations">
        /// The variations exhibited by each schema
        /// </param>
        /// <returns>
        /// A reduced set of schemas that together cover every variation
        /// </returns>
        private static IReadOnlyList<string> ReduceSchemaPropertyVariationToInterestingSchemas(Dictionary<string, HashSet<string>> schemaVariations)
        {
            var dictionaryClone = new Dictionary<string, HashSet<string>>(schemaVariations, StringComparer.Ordinal);

            var uniqueVariations = new HashSet<string>(dictionaryClone.Values.SelectMany(variations => variations), StringComparer.Ordinal);

            var result = new List<string>();
            var covered = new HashSet<string>(StringComparer.Ordinal);

            while (covered.Count < uniqueVariations.Count)
            {
                var bestSchema = dictionaryClone
                    .OrderByDescending(schema => schema.Value.Count(variation => !covered.Contains(variation)))
                    .First().Key;

                result.Add(bestSchema);

                foreach (var variation in dictionaryClone[bestSchema])
                {
                    covered.Add(variation);
                }

                dictionaryClone.Remove(bestSchema);
            }

            return result;
        }
    }
}
