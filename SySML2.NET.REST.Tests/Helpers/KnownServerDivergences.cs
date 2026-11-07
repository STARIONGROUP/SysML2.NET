// ------------------------------------------------------------------------------------------------
// <copyright file="KnownServerDivergences.cs" company="Starion Group S.A.">
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
    using System.Net;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.REST.Exceptions;

    /// <summary>
    /// The known ways in which the Flexo server under the scenario diverges from the Systems Modeling API and Services
    /// specification; they are reported as warnings because they are no failure of the client under test
    /// </summary>
    /// <remarks>
    /// The expectations of the scenario stay those of the specification; only the outcome is reported differently.
    /// </remarks>
    public static class KnownServerDivergences
    {
        /// <summary>
        /// The source revision of Flexo that the divergences were found in
        /// </summary>
        private const string FlexoRevision = "Open-MBEE/flexo-mms-sysmlv2@836d904d";

        /// <summary>
        /// The properties of the API records that the server leaves out
        /// </summary>
        private static readonly HashSet<string> OmittedRecordProperties = ["alias", "name", "description", "deleted"];

        /// <summary>
        /// Matches a schema error that lists missing required properties, capturing their quoted names
        /// </summary>
        private static readonly Regex MissingRequired = new(@"^\S* required: Required properties \[(?<names>[^\]]*)\] are not present$", RegexOptions.Compiled);

        /// <summary>
        /// Matches a schema error on a list property sent as <c>null</c>
        /// </summary>
        private static readonly Regex NullList = new(@"^\S* type: Value is ""null"" but should be ""array""$", RegexOptions.Compiled);

        /// <summary>
        /// Gets the divergence that the roots are the elements without an <c>owningRelatedElement</c>, owned through
        /// an <c>owningRelationship</c> or not, where Clause 7.2.2 makes them the elements without an <c>owner</c>
        /// </summary>
        public static ServerDivergence RootsIgnoreOwningRelationship { get; } = new("GET /projects/{projectId}/commits/{commitId}/roots answers every element without an owningRelatedElement, ignoring its owningRelationship", $"{FlexoRevision} src/main/kotlin/org/openmbee/flexo/sysmlv2/apis/ElementApi.kt");

        /// <summary>
        /// Gets the divergence that a request for an unknown resource is answered with a client error other than
        /// 404 Not Found, which the OpenAPI document declares for it
        /// </summary>
        public static ServerDivergence UnknownResourceNotFound { get; } = new("a request for an unknown resource is answered with another client error than 404 Not Found", $"{FlexoRevision}, layer1 access control on a missing repository");

        /// <summary>
        /// Gets the divergence that a request naming an unknown resource succeeds, where the OpenAPI document declares
        /// 404 Not Found for it
        /// </summary>
        public static ServerDivergence UnknownResourceAccepted { get; } = new("a request naming an unknown resource succeeds instead of answering 404 Not Found", $"{FlexoRevision} src/main/kotlin/org/openmbee/flexo/sysmlv2/apis/RelationshipApi.kt");

        /// <summary>
        /// Gets the divergence that an error answer carries an empty or plain-text body, where the OpenAPI document
        /// declares an <c>application/json</c> <c>Error</c> body for it
        /// </summary>
        public static ServerDivergence ErrorWithoutErrorBody { get; } = new(
            "an error answer carries an empty or text/plain body instead of an application/json Error",
            $"{FlexoRevision}, layer1 Http404Exception and the meta datatypes route",
            (call, violation) => (int)call.StatusCode >= 400 && violation.Kind is ContractViolationKind.UnexpectedBody or ContractViolationKind.UndeclaredMediaType);

        /// <summary>
        /// Gets the divergence that a create answers 200 OK, where the OpenAPI document declares 201 Created and no 200
        /// </summary>
        public static ServerDivergence CreatedAnsweredOk { get; } = new(
            "a create answers 200 OK instead of 201 Created",
            $"{FlexoRevision}, observed on POST /projects and POST /projects/{{projectId}}/commits",
            (call, violation) => call is { StatusCode: HttpStatusCode.OK, Operation: { } operation } && operation.Method == HttpMethod.Post && operation.Responses.ContainsKey("201") && !operation.Responses.ContainsKey("200"));

        /// <summary>
        /// Gets the divergence that the API records leave out required properties, or send <c>null</c> for an empty
        /// list, where the OpenAPI document requires them
        /// </summary>
        public static ServerDivergence RecordsWithoutRequiredProperties { get; } = new(
            "projects, branches, commits and data versions leave out the required alias, name, description and deleted, and send null for an empty previousCommit",
            $"{FlexoRevision}, observed on every project, branch, commit and change read",
            (_, violation) => violation.Kind == ContractViolationKind.SchemaMismatch && (NullList.IsMatch(violation.Message) || MissingNames(violation) is { Count: > 0 } names && names.All(OmittedRecordProperties.Contains)));

        /// <summary>
        /// Gets the divergence that elements are returned without their derived properties, which the response schemas
        /// of the OpenAPI document require
        /// </summary>
        public static ServerDivergence ElementsWithoutDerivedProperties { get; } = new(
            "elements are returned without their derived properties, e.g. owner and qualifiedName",
            $"{FlexoRevision}, the stored payload is returned as sent",
            (_, violation) => violation.Kind == ContractViolationKind.SchemaMismatch && MissingNames(violation) is { Count: > 0 } names && names.All(DtoMetadata.DerivedPropertyNames.Contains));

        /// <summary>
        /// Gets the divergence that a datatype is returned with an <c>$id</c> that is not a URI
        /// </summary>
        public static ServerDivergence DatatypeIdNotUri { get; } = new(
            "a datatype is returned with an $id that is not a URI",
            $"{FlexoRevision}, the meta datatypes route",
            (call, violation) => call.Operation?.PathTemplate == "/meta/datatypes/{datatypeId}" && violation.Message.StartsWith("/$id format:", StringComparison.Ordinal));

        /// <summary>
        /// Gets every known divergence
        /// </summary>
        public static IReadOnlyList<ServerDivergence> All { get; } = [RootsIgnoreOwningRelationship, UnknownResourceNotFound, UnknownResourceAccepted, ErrorWithoutErrorBody, CreatedAnsweredOk, RecordsWithoutRequiredProperties, ElementsWithoutDerivedProperties, DatatypeIdNotUri];

        /// <summary>
        /// Asserts that a call for an unknown resource fails with 404 Not Found, or reports the one tolerated divergence
        /// when the server answers the way it describes
        /// </summary>
        /// <remarks>
        /// Whether the body of the answer conforms is checked with every call by <see cref="OpenApiConformance"/>;
        /// here, a 404 whose body conforms must reach the caller as a parsed <c>Error</c>.
        /// </remarks>
        /// <param name="call">
        /// The call for the unknown resource
        /// </param>
        /// <param name="lastCall">
        /// Gets the call that the <see cref="EndpointRecorder"/> recorded last, or <c>null</c>
        /// </param>
        /// <param name="tolerated">
        /// The divergence the call may show, either <see cref="UnknownResourceNotFound"/> or
        /// <see cref="UnknownResourceAccepted"/>, or <c>null</c> to tolerate none
        /// </param>
        /// <param name="description">
        /// The description of the call in assertion messages
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        public static async Task ExpectNotFoundAsync(Func<Task> call, Func<EndpointCall> lastCall, ServerDivergence tolerated = null, [CallerArgumentExpression(nameof(call))] string description = null)
        {
            PsmApiException failure = null;

            try
            {
                await call();
            }
            catch (PsmApiException exception)
            {
                failure = exception;
            }

            var tolerates = tolerated == UnknownResourceAccepted ? failure == null : failure is { StatusCode: not HttpStatusCode.NotFound } && (int)failure.StatusCode is >= 400 and < 500;

            if (tolerated != null && tolerates)
            {
                Report(tolerated, $"{description} answered {(failure == null ? "a success" : $"{(int)failure.StatusCode} {failure.StatusCode}")}");
                return;
            }

            Assert.That(failure?.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), description);

            if (failure is { StatusCode: HttpStatusCode.NotFound } && lastCall?.Invoke() is { ResponseViolations.Count: 0 })
            {
                Assert.That(failure.Error, Is.Not.Null, $"{description}: the 404 body conforms to the OpenAPI document, but the client read no Error from it");
            }
        }

        /// <summary>
        /// Reports a known divergence as a warning
        /// </summary>
        /// <param name="divergence">
        /// The divergence that was observed
        /// </param>
        /// <param name="detail">
        /// What was observed
        /// </param>
        public static void Report(ServerDivergence divergence, string detail)
        {
            Assert.Warn($"Known server divergence: {divergence.Summary} ({divergence.Reference}). Observed: {detail}");
        }

        /// <summary>
        /// Reads the names of the missing required properties from a schema violation
        /// </summary>
        /// <param name="violation">
        /// The violation
        /// </param>
        /// <returns>
        /// The names, or <c>null</c> when the violation is about something else
        /// </returns>
        private static IReadOnlyList<string> MissingNames(ContractViolation violation)
        {
            var match = MissingRequired.Match(violation.Message);

            return match.Success ? match.Groups["names"].Value.Split(',').Select(name => name.Trim('"')).ToList() : null;
        }
    }
}
