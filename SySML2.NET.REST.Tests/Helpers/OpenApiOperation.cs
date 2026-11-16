// ------------------------------------------------------------------------------------------------
// <copyright file="OpenApiOperation.cs" company="Starion Group S.A.">
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
    using System.Net;
    using System.Net.Http;

    /// <summary>
    /// One operation of an OpenAPI document: an HTTP method on a path template, with the parameters, request body and
    /// responses it declares
    /// </summary>
    public sealed class OpenApiOperation
    {
        /// <summary>
        /// The key of the response that covers every status the operation does not declare explicitly
        /// </summary>
        private const string DefaultResponse = "default";

        /// <summary>
        /// The segments of <see cref="PathTemplate"/>
        /// </summary>
        private readonly string[] segments;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiOperation"/> class
        /// </summary>
        /// <param name="method">
        /// The HTTP method
        /// </param>
        /// <param name="pathTemplate">
        /// The path template, e.g. <c>/projects/{projectId}</c>
        /// </param>
        /// <param name="parameters">
        /// The parameters the operation declares
        /// </param>
        /// <param name="requestBody">
        /// The request body the operation declares, or <c>null</c> when it declares none
        /// </param>
        /// <param name="isRequestBodyRequired">
        /// Whether the request body is required
        /// </param>
        /// <param name="responses">
        /// The responses the operation declares, by status code, status range such as <c>4XX</c>, or <c>default</c>
        /// </param>
        public OpenApiOperation(HttpMethod method, string pathTemplate, IReadOnlyList<OpenApiParameter> parameters, OpenApiContent requestBody, bool isRequestBodyRequired, IReadOnlyDictionary<string, OpenApiContent> responses)
        {
            this.Method = method ?? throw new ArgumentNullException(nameof(method));
            this.PathTemplate = pathTemplate ?? throw new ArgumentNullException(nameof(pathTemplate));
            this.Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            this.RequestBody = requestBody;
            this.IsRequestBodyRequired = isRequestBodyRequired;
            this.Responses = responses ?? throw new ArgumentNullException(nameof(responses));
            this.segments = pathTemplate.Split('/', StringSplitOptions.RemoveEmptyEntries);
            this.LiteralSegmentCount = this.segments.Count(segment => !IsParameter(segment));
        }

        /// <summary>
        /// Gets the HTTP method
        /// </summary>
        public HttpMethod Method { get; }

        /// <summary>
        /// Gets the path template
        /// </summary>
        public string PathTemplate { get; }

        /// <summary>
        /// Gets the parameters the operation declares
        /// </summary>
        public IReadOnlyList<OpenApiParameter> Parameters { get; }

        /// <summary>
        /// Gets the request body the operation declares, or <c>null</c> when it declares none
        /// </summary>
        public OpenApiContent RequestBody { get; }

        /// <summary>
        /// Gets a value indicating whether the request body is required
        /// </summary>
        public bool IsRequestBodyRequired { get; }

        /// <summary>
        /// Gets the responses the operation declares, by status code, status range such as <c>4XX</c>, or <c>default</c>
        /// </summary>
        public IReadOnlyDictionary<string, OpenApiContent> Responses { get; }

        /// <summary>
        /// Gets a value indicating whether the document declares a client error (4xx) response for the operation
        /// </summary>
        public bool DeclaresClientError => this.Responses.Keys.Any(status => status.StartsWith('4'));

        /// <summary>
        /// Gets the number of segments of <see cref="PathTemplate"/> that are not parameters
        /// </summary>
        public int LiteralSegmentCount { get; }

        /// <summary>
        /// Checks a request against the query parameters and the request body the operation declares
        /// </summary>
        /// <param name="query">
        /// The query of the request, with or without its leading <c>?</c>
        /// </param>
        /// <param name="mediaType">
        /// The media type of the body without parameters, or <c>null</c> when none was sent
        /// </param>
        /// <param name="body">
        /// The body, empty when there was none
        /// </param>
        /// <returns>
        /// The violations found, empty when the request conforms
        /// </returns>
        public IReadOnlyList<ContractViolation> CheckRequest(string query, string mediaType, string body)
        {
            var declared = this.Parameters.Where(parameter => parameter.Location == "query").ToDictionary(parameter => parameter.Name, StringComparer.Ordinal);
            var sent = ParseQuery(query);
            var violations = new List<ContractViolation>();

            foreach (var (name, value) in sent)
            {
                violations.AddRange(declared.TryGetValue(name, out var parameter)
                    ? parameter.Check(value)
                    : [new ContractViolation(ContractViolationKind.UndeclaredParameter, $"query parameter {name} is not declared")]);
            }

            violations.AddRange(declared.Values
                .Where(parameter => parameter.IsRequired && !sent.Exists(pair => pair.Name == parameter.Name))
                .Select(parameter => new ContractViolation(ContractViolationKind.MissingParameter, $"required query parameter {parameter.Name} is missing")));

            if (this.RequestBody == null)
            {
                violations.AddRange(string.IsNullOrEmpty(body) ? [] : [new ContractViolation(ContractViolationKind.UnexpectedBody, "a request body where the document declares none")]);
            }
            else if (!string.IsNullOrEmpty(body) || this.IsRequestBodyRequired)
            {
                violations.AddRange(this.RequestBody.Check(mediaType, body));
            }

            return violations;
        }

        /// <summary>
        /// Checks a response against the response the operation declares for its status
        /// </summary>
        /// <param name="statusCode">
        /// The status of the response
        /// </param>
        /// <param name="mediaType">
        /// The media type of the body without parameters, or <c>null</c> when there was none
        /// </param>
        /// <param name="body">
        /// The body, empty when there was none
        /// </param>
        /// <returns>
        /// The violations found, empty when the response conforms
        /// </returns>
        public IReadOnlyList<ContractViolation> CheckResponse(HttpStatusCode statusCode, string mediaType, string body)
        {
            var status = ((int)statusCode).ToString(CultureInfo.InvariantCulture);

            var response = this.Responses.GetValueOrDefault(status)
                           ?? this.Responses.GetValueOrDefault($"{status[0]}XX")
                           ?? this.Responses.GetValueOrDefault(DefaultResponse);

            return response == null
                ? [new ContractViolation(ContractViolationKind.UndeclaredStatus, $"status {status} is not declared")]
                : response.Check(mediaType, body);
        }

        /// <summary>
        /// Asserts whether the segments of a request path fit <see cref="PathTemplate"/>
        /// </summary>
        /// <param name="pathSegments">
        /// The segments of the request path
        /// </param>
        /// <returns>
        /// <c>true</c> when every literal segment is equal and every parameter segment is filled
        /// </returns>
        public bool Matches(string[] pathSegments)
        {
            return pathSegments.Length == this.segments.Length
                   && this.segments.Zip(pathSegments).All(pair => IsParameter(pair.First) || string.Equals(pair.First, pair.Second, StringComparison.Ordinal));
        }

        /// <summary>
        /// Returns the operation as <c>METHOD /path/template</c>
        /// </summary>
        /// <returns>
        /// The text of the operation
        /// </returns>
        public override string ToString() => $"{this.Method} {this.PathTemplate}";

        /// <summary>
        /// Asserts whether a template segment is a parameter, e.g. <c>{projectId}</c>
        /// </summary>
        /// <param name="segment">
        /// The template segment
        /// </param>
        /// <returns>
        /// <c>true</c> for a parameter segment
        /// </returns>
        private static bool IsParameter(string segment) => segment.StartsWith('{') && segment.EndsWith('}');

        /// <summary>
        /// Splits a query into its unescaped name and value pairs
        /// </summary>
        /// <param name="query">
        /// The query, with or without its leading <c>?</c>
        /// </param>
        /// <returns>
        /// The pairs, in order
        /// </returns>
        private static List<(string Name, string Value)> ParseQuery(string query)
        {
            return (query ?? string.Empty).TrimStart('?')
                .Split('&', StringSplitOptions.RemoveEmptyEntries)
                .Select(pair => pair.Split('=', 2))
                .Select(parts => (Uri.UnescapeDataString(parts[0]), parts.Length == 2 ? Uri.UnescapeDataString(parts[1]) : string.Empty))
                .ToList();
        }
    }
}
