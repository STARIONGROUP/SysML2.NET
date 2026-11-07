// ------------------------------------------------------------------------------------------------
// <copyright file="OpenApiParameter.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Nodes;

    using Json.Schema;

    /// <summary>
    /// One parameter that an OpenAPI operation declares
    /// </summary>
    public sealed class OpenApiParameter
    {
        /// <summary>
        /// The options that values are evaluated with: <c>format</c> is asserted, as for <c>uuid</c> identifiers
        /// </summary>
        private static readonly EvaluationOptions Evaluation = new() { RequireFormatValidation = true };

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiParameter"/> class
        /// </summary>
        /// <param name="name">
        /// The name of the parameter, e.g. <c>page[size]</c>
        /// </param>
        /// <param name="location">
        /// Where the parameter goes: <c>path</c>, <c>query</c>, <c>header</c> or <c>cookie</c>
        /// </param>
        /// <param name="isRequired">
        /// Whether the parameter is required
        /// </param>
        /// <param name="type">
        /// The <c>type</c> of the schema of the parameter, or <c>null</c> when it has none
        /// </param>
        /// <param name="schema">
        /// The schema of the parameter
        /// </param>
        public OpenApiParameter(string name, string location, bool isRequired, string type, JsonSchema schema)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Location = location ?? throw new ArgumentNullException(nameof(location));
            this.IsRequired = isRequired;
            this.Type = type;
            this.Schema = schema ?? throw new ArgumentNullException(nameof(schema));
        }

        /// <summary>
        /// Gets the name of the parameter
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets where the parameter goes: <c>path</c>, <c>query</c>, <c>header</c> or <c>cookie</c>
        /// </summary>
        public string Location { get; }

        /// <summary>
        /// Gets a value indicating whether the parameter is required
        /// </summary>
        public bool IsRequired { get; }

        /// <summary>
        /// Gets the <c>type</c> of the schema of the parameter, or <c>null</c> when it has none
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Gets the schema of the parameter
        /// </summary>
        public JsonSchema Schema { get; }

        /// <summary>
        /// Checks the text of a value against the schema, reading an array as the comma-separated <c>form</c> style
        /// that the document uses for its array parameters
        /// </summary>
        /// <param name="value">
        /// The unescaped text of the value
        /// </param>
        /// <returns>
        /// The violations found, empty when the value conforms
        /// </returns>
        public IReadOnlyList<ContractViolation> Check(string value)
        {
            var instance = JsonSerializer.SerializeToElement(this.ToNode(value));

            return this.Schema.Evaluate(instance, Evaluation).IsValid
                ? []
                : [new ContractViolation(ContractViolationKind.InvalidParameter, $"{this.Name}={value} does not fit a {this.Type ?? "value"} of the declared schema")];
        }

        /// <summary>
        /// Converts the text of a value to the JSON value that its schema describes; text that does not parse stays a
        /// string, which the schema then rejects
        /// </summary>
        /// <param name="value">
        /// The text of the value
        /// </param>
        /// <returns>
        /// The JSON value
        /// </returns>
        private JsonNode ToNode(string value)
        {
            return this.Type switch
            {
                "integer" when long.TryParse(value, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out var integer) => JsonValue.Create(integer),
                "number" when double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var number) => JsonValue.Create(number),
                "boolean" when bool.TryParse(value, out var boolean) && value == value.ToLowerInvariant() => JsonValue.Create(boolean),
                "array" => new JsonArray([.. value.Split(',').Select(item => (JsonNode)JsonValue.Create(item))]),
                _ => JsonValue.Create(value)
            };
        }
    }
}
