// ------------------------------------------------------------------------------------------------
// <copyright file="OpenApiContent.cs" company="Starion Group S.A.">
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
    using System.Linq;
    using System.Text.Json;

    using Json.Schema;

    /// <summary>
    /// The body that an OpenAPI request body or response declares: a schema per media type, or no body at all
    /// </summary>
    /// <remarks>
    /// Where the declared schema is a union of object schemas that each fix their <c>@type</c> with a <c>const</c>, as
    /// the element schemas are, an object is validated against the member of its <c>@type</c> alone: every other
    /// member rejects that <c>@type</c>, so the verdict is the same, its errors are those of that member, and the
    /// evaluation skips the other members.
    /// </remarks>
    public sealed class OpenApiContent
    {
        /// <summary>
        /// The number of schema errors reported for one body
        /// </summary>
        private const int ReportedErrors = 5;

        /// <summary>
        /// The options that bodies are evaluated with: <c>format</c> is asserted, as for <c>uuid</c> identifiers
        /// </summary>
        private static readonly EvaluationOptions Evaluation = new() { OutputFormat = OutputFormat.Hierarchical, RequireFormatValidation = true };

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiContent"/> class
        /// </summary>
        /// <param name="schemasByMediaType">
        /// The schema of the body per declared media type; empty when no body is declared
        /// </param>
        public OpenApiContent(IReadOnlyDictionary<string, JsonSchema> schemasByMediaType) : this(schemasByMediaType, new Dictionary<string, JsonSchema>(), false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiContent"/> class, with the member schemas of a union
        /// </summary>
        /// <param name="schemasByMediaType">
        /// The schema of the body per declared media type; empty when no body is declared
        /// </param>
        /// <param name="schemasByType">
        /// The member of the declared union for each <c>@type</c> it admits; empty when the schema is no such union
        /// </param>
        /// <param name="isArrayOfTypes">
        /// Whether the body is an array whose items are of the union, rather than an object of it
        /// </param>
        public OpenApiContent(IReadOnlyDictionary<string, JsonSchema> schemasByMediaType, IReadOnlyDictionary<string, JsonSchema> schemasByType, bool isArrayOfTypes)
        {
            this.SchemasByMediaType = schemasByMediaType ?? throw new ArgumentNullException(nameof(schemasByMediaType));
            this.SchemasByType = schemasByType ?? throw new ArgumentNullException(nameof(schemasByType));
            this.IsArrayOfTypes = isArrayOfTypes;
        }

        /// <summary>
        /// Gets the schema of the body per declared media type; empty when no body is declared
        /// </summary>
        public IReadOnlyDictionary<string, JsonSchema> SchemasByMediaType { get; }

        /// <summary>
        /// Gets the member of the declared union for each <c>@type</c> it admits; empty when the schema is no such union
        /// </summary>
        public IReadOnlyDictionary<string, JsonSchema> SchemasByType { get; }

        /// <summary>
        /// Gets a value indicating whether the body is an array whose items are of the union
        /// </summary>
        public bool IsArrayOfTypes { get; }

        /// <summary>
        /// Checks a body against the declared media types and their schemas
        /// </summary>
        /// <param name="mediaType">
        /// The media type of the body without parameters, or <c>null</c> when none was sent
        /// </param>
        /// <param name="body">
        /// The body, empty when there was none
        /// </param>
        /// <returns>
        /// The violations found, empty when the body conforms
        /// </returns>
        public IReadOnlyList<ContractViolation> Check(string mediaType, string body)
        {
            if (this.SchemasByMediaType.Count == 0)
            {
                return string.IsNullOrEmpty(body) ? [] : [new ContractViolation(ContractViolationKind.UnexpectedBody, "a body where the document declares none")];
            }

            var declared = this.SchemasByMediaType.Keys.FirstOrDefault(candidate => Accepts(candidate, mediaType));

            if (declared == null && string.IsNullOrEmpty(body))
            {
                return [new ContractViolation(ContractViolationKind.UnexpectedBody, $"no body where the document declares {string.Join(", ", this.SchemasByMediaType.Keys)}")];
            }

            if (declared == null)
            {
                return [new ContractViolation(ContractViolationKind.UndeclaredMediaType, $"media type {mediaType ?? "(none)"} where the document declares {string.Join(", ", this.SchemasByMediaType.Keys)}")];
            }

            if (!string.IsNullOrEmpty(body) && !IsJson(mediaType))
            {
                return [];
            }

            JsonDocument instance;

            try
            {
                instance = JsonDocument.Parse(string.IsNullOrEmpty(body) ? "null" : body);
            }
            catch (JsonException exception)
            {
                return [new ContractViolation(ContractViolationKind.InvalidJson, $"a {mediaType} body that is not JSON: {exception.Message}")];
            }

            using (instance)
            {
                if (this.CheckByType(instance.RootElement) is { } byType)
                {
                    return byType;
                }

                var results = this.SchemasByMediaType[declared].Evaluate(instance.RootElement, Evaluation);

                return results.IsValid ? [] : [.. Summarize(results, string.Empty).Select(error => new ContractViolation(ContractViolationKind.SchemaMismatch, error))];
            }
        }

        /// <summary>
        /// Asserts whether a declared media type, which may be a range such as <c>*/*</c>, accepts a media type
        /// </summary>
        /// <param name="declared">
        /// The declared media type or range
        /// </param>
        /// <param name="mediaType">
        /// The media type of the body, or <c>null</c>
        /// </param>
        /// <returns>
        /// <c>true</c> when <paramref name="declared"/> accepts <paramref name="mediaType"/>
        /// </returns>
        private static bool Accepts(string declared, string mediaType)
        {
            if (declared == "*/*")
            {
                return true;
            }

            if (mediaType == null)
            {
                return false;
            }

            return declared.EndsWith("/*", StringComparison.Ordinal)
                ? mediaType.StartsWith(declared[..^1], StringComparison.OrdinalIgnoreCase)
                : string.Equals(declared, mediaType, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Gets the innermost <c>anyOf</c> or <c>oneOf</c> branch of an evaluation path, e.g. <c>/$ref/anyOf/3/</c>
        /// </summary>
        /// <param name="evaluationPath">
        /// The evaluation path
        /// </param>
        /// <returns>
        /// The path of the branch with a trailing <c>/</c>, or <c>null</c> when the path is in no branch
        /// </returns>
        private static string BranchOf(string evaluationPath)
        {
            var segments = evaluationPath.Split('/');
            var branch = Array.FindLastIndex(segments, segment => segment is "anyOf" or "oneOf");

            return branch < 0 || branch + 1 >= segments.Length ? null : $"{string.Join('/', segments[..(branch + 2)])}/";
        }

        /// <summary>
        /// Collects the errors of a failed node and of its failed descendants; a node that passes, such as the
        /// <c>oneOf</c> of a nullable property, contributes none of the errors of its rejected alternatives
        /// </summary>
        /// <param name="node">
        /// The node, in the hierarchical output format
        /// </param>
        /// <returns>
        /// The errors, with the evaluation path and the instance location of their node
        /// </returns>
        private static IEnumerable<(string Path, string Location, string Keyword, string Message)> ErrorsOf(EvaluationResults node)
        {
            if (node.IsValid)
            {
                return [];
            }

            var own = (node.Errors ?? new Dictionary<string, string>()).Select(error => (node.EvaluationPath.ToString(), node.InstanceLocation.ToString(), error.Key, error.Value));

            return own.Concat((node.Details ?? []).SelectMany(ErrorsOf));
        }

        /// <summary>
        /// Asserts whether a media type is JSON, <c>application/json</c> or a <c>+json</c> suffix
        /// </summary>
        /// <param name="mediaType">
        /// The media type, or <c>null</c>
        /// </param>
        /// <returns>
        /// <c>true</c> for a JSON media type
        /// </returns>
        private static bool IsJson(string mediaType)
        {
            return mediaType != null && (string.Equals(mediaType, "application/json", StringComparison.OrdinalIgnoreCase) || mediaType.EndsWith("+json", StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Asserts whether an error is the rejection of the <c>null</c> alternative of a nullable value
        /// </summary>
        /// <param name="error">
        /// The error
        /// </param>
        /// <returns>
        /// <c>true</c> for a <c>type</c> error that asks for <c>null</c>
        /// </returns>
        private static bool IsNullAlternative((string Path, string Location, string Keyword, string Message) error)
        {
            return error.Keyword == "type" && error.Message.EndsWith("should be \"null\"", StringComparison.Ordinal);
        }

        /// <summary>
        /// Asserts whether an instance location is an instance or lies within it
        /// </summary>
        /// <param name="location">
        /// The instance location
        /// </param>
        /// <param name="instance">
        /// The location of the instance
        /// </param>
        /// <returns>
        /// <c>true</c> when <paramref name="location"/> is <paramref name="instance"/> or below it
        /// </returns>
        private static bool IsWithin(string location, string instance)
        {
            return location == instance || location.StartsWith($"{instance}/", StringComparison.Ordinal);
        }

        /// <summary>
        /// Summarizes the errors of a failed evaluation, one line per error: the errors inside an <c>anyOf</c> or
        /// <c>oneOf</c> branch that an instance rejected on its <c>@type</c> are left out, as is the <c>null</c>
        /// alternative of a nullable value that has errors of its own; when no branch accepts the <c>@type</c>, the
        /// <c>@type</c> mismatches are reported
        /// </summary>
        /// <param name="results">
        /// The results, in the hierarchical output format
        /// </param>
        /// <param name="location">
        /// The location of the evaluated instance in the body, empty for the body itself
        /// </param>
        /// <returns>
        /// At most <see cref="ReportedErrors"/> lines of the form <c>location keyword: message</c>
        /// </returns>
        private static IReadOnlyList<string> Summarize(EvaluationResults results, string location)
        {
            var errors = ErrorsOf(results)
                .Where(error => error.Keyword is not ("anyOf" or "oneOf" or "allOf" or "properties" or "items" or "$ref" or ""))
                .Distinct()
                .ToList();

            var typeMismatches = errors.Where(error => error.Keyword == "const" && error.Location.EndsWith("/@type", StringComparison.Ordinal)).ToList();

            var rejectedBranches = typeMismatches
                .Select(error => (Instance: error.Location[..^"/@type".Length], Branch: BranchOf(error.Path)))
                .Where(rejected => rejected.Branch != null)
                .ToHashSet();

            var locationsWithOtherErrors = errors.Where(error => !IsNullAlternative(error)).Select(error => error.Location).ToHashSet();

            var specific = errors
                .Where(error => !rejectedBranches.Any(rejected => IsWithin(error.Location, rejected.Instance) && $"{error.Path}/".StartsWith(rejected.Branch, StringComparison.Ordinal)))
                .Where(error => !(IsNullAlternative(error) && locationsWithOtherErrors.Contains(error.Location)))
                .ToList();

            var reported = specific.Count > 0 ? specific : typeMismatches.Count > 0 ? typeMismatches : errors;

            return errors.Count == 0
                ? ["the body does not validate"]
                : [.. reported.Take(ReportedErrors).Select(error => $"{(location.Length + error.Location.Length == 0 ? "/" : location + error.Location)} {error.Keyword}: {error.Message}")];
        }

        /// <summary>
        /// Checks the body against the member of the union for its <c>@type</c>, or for the <c>@type</c> of each of its
        /// items, where an item of a <c>@type</c> that no member admits is a violation of its own, reported ahead of
        /// the errors of the other items so that the limit on reported errors cannot hide it
        /// </summary>
        /// <param name="body">
        /// The body
        /// </param>
        /// <returns>
        /// The violations found, or <c>null</c> when the body cannot be checked by type and the declared schema decides
        /// </returns>
        private IReadOnlyList<ContractViolation> CheckByType(JsonElement body)
        {
            if (this.SchemasByType.Count == 0 || (this.IsArrayOfTypes && body.ValueKind != JsonValueKind.Array))
            {
                return null;
            }

            var instances = this.IsArrayOfTypes
                ? body.EnumerateArray().Select((item, index) => (Location: $"/{index}", Instance: item)).ToList()
                : [(Location: string.Empty, Instance: body)];

            var members = new List<(string Location, string UnknownType, JsonElement Instance, JsonSchema Schema)>();

            foreach (var (location, instance) in instances)
            {
                if (instance.ValueKind != JsonValueKind.Object
                    || !instance.TryGetProperty("@type", out var type)
                    || type.ValueKind != JsonValueKind.String)
                {
                    return null;
                }

                var typeName = type.GetString()!;

                if (this.SchemasByType.TryGetValue(typeName, out var member))
                {
                    members.Add((location, null, instance, member));
                }
                else if (this.IsArrayOfTypes)
                {
                    members.Add((location, typeName, instance, null));
                }
                else
                {
                    return null;
                }
            }

            return [.. members
                .OrderBy(entry => entry.UnknownType == null)
                .SelectMany(entry => entry.UnknownType != null
                    ? [$"{entry.Location}/@type const: \"{entry.UnknownType}\" is none of the types the schema admits"]
                    : entry.Schema.Evaluate(entry.Instance, Evaluation) is { IsValid: false } results ? Summarize(results, entry.Location) : [])
                .Take(ReportedErrors)
                .Select(error => new ContractViolation(ContractViolationKind.SchemaMismatch, error))];
        }
    }
}
