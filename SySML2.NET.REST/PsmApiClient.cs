// ------------------------------------------------------------------------------------------------
// <copyright file="PsmApiClient.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Text.Json;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.Common;
    using SysML2.NET.PSM;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.REST.Exceptions;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The client of the REST/HTTP platform specific model of the Systems Modeling API and Services
    /// </summary>
    public partial class PsmApiClient : IPsmApiClient
    {
        /// <summary>
        /// The media type of every payload exchanged with the Systems Modeling API and Services
        /// </summary>
        private const string JsonMediaType = "application/json";

        /// <summary>
        /// The pattern of one link-value of an RFC 8288 <c>Link</c> header, capturing its target and its relation type
        /// </summary>
        private const string LinkValueExpression = "<(?<target>[^>]*)>[^,]*?;\\s*rel=\"?(?<relation>[^\";,]+)\"?";

        /// <summary>
        /// The time, in milliseconds, after which matching a <c>Link</c> header value times out
        /// </summary>
        private const int LinkValueMatchTimeoutMilliseconds = 1000;

#if NET7_0_OR_GREATER
        /// <summary>
        /// Gets the <see cref="Regex"/> that matches one link-value of an RFC 8288 <c>Link</c> header
        /// </summary>
        /// <returns>
        /// The <see cref="Regex"/>
        /// </returns>
        [GeneratedRegex(LinkValueExpression, RegexOptions.None, LinkValueMatchTimeoutMilliseconds)]
        private static partial Regex LinkValuePattern();
#else
        /// <summary>
        /// The <see cref="Regex"/> that matches one link-value of an RFC 8288 <c>Link</c> header
        /// </summary>
        private static readonly Regex LinkValueRegex = new(LinkValueExpression, RegexOptions.Compiled, TimeSpan.FromMilliseconds(LinkValueMatchTimeoutMilliseconds));

        /// <summary>
        /// Gets the <see cref="Regex"/> that matches one link-value of an RFC 8288 <c>Link</c> header
        /// </summary>
        /// <returns>
        /// The <see cref="Regex"/>
        /// </returns>
        private static Regex LinkValuePattern() => LinkValueRegex;
#endif

        /// <summary>
        /// The <see cref="JsonWriterOptions"/> used to write every request payload
        /// </summary>
        private static readonly JsonWriterOptions WriterOptions = new();

        /// <summary>
        /// The <see cref="HttpClient"/> whose <see cref="HttpClient.BaseAddress"/> addresses the server
        /// </summary>
        private readonly HttpClient httpClient;

        /// <summary>
        /// The <see cref="IDeSerializer"/> that reads the response payloads
        /// </summary>
        private readonly IDeSerializer deSerializer;

        /// <summary>
        /// The <see cref="ISerializer"/> that writes the request payloads
        /// </summary>
        private readonly ISerializer serializer;

        /// <summary>
        /// The <see cref="ILogger"/> used to log
        /// </summary>
        private readonly ILogger<PsmApiClient> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PsmApiClient"/> class
        /// </summary>
        /// <param name="httpClient">
        /// The <see cref="HttpClient"/> whose <see cref="HttpClient.BaseAddress"/> addresses the server, ending with a slash
        /// </param>
        /// <param name="deSerializer">
        /// The <see cref="IDeSerializer"/> that reads the response payloads
        /// </param>
        /// <param name="serializer">
        /// The <see cref="ISerializer"/> that writes the request payloads
        /// </param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to set up logging, or <c>null</c> to disable logging
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="httpClient"/>, <paramref name="deSerializer"/> or <paramref name="serializer"/> is <c>null</c>
        /// </exception>
        public PsmApiClient(HttpClient httpClient, IDeSerializer deSerializer, ISerializer serializer, ILoggerFactory loggerFactory = null)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.deSerializer = deSerializer ?? throw new ArgumentNullException(nameof(deSerializer));
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            this.logger = loggerFactory == null ? NullLogger<PsmApiClient>.Instance : loggerFactory.CreateLogger<PsmApiClient>();
        }

        /// <summary>
        /// Escapes a path parameter so that it forms a single path segment
        /// </summary>
        /// <param name="value">
        /// The value of the path parameter
        /// </param>
        /// <returns>
        /// The escaped value
        /// </returns>
        private static string Escape(object value)
        {
            return Uri.EscapeDataString(Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty);
        }

        /// <summary>
        /// Converts the UTF-8 representation of an enumeration literal into the value of a query parameter
        /// </summary>
        /// <param name="utf8Value">
        /// The UTF-8 encoded representation of the literal in the Systems Modeling API and Services
        /// </param>
        /// <returns>
        /// The value of the query parameter
        /// </returns>
        private static string ToQueryValue(ReadOnlySpan<byte> utf8Value)
        {
            return Encoding.UTF8.GetString(utf8Value);
        }

        /// <summary>
        /// Throws when a required sequence parameter is <c>null</c> or empty
        /// </summary>
        /// <typeparam name="T">
        /// The type of the items of the sequence
        /// </typeparam>
        /// <param name="value">
        /// The value of the parameter
        /// </param>
        /// <param name="parameterName">
        /// The name of the parameter
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="value"/> is <c>null</c>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="value"/> is empty
        /// </exception>
        private static void ThrowIfNullOrEmpty<T>(IEnumerable<T> value, string parameterName)
        {
            if (value is null)
            {
                throw new ArgumentNullException(parameterName);
            }

            if (!value.Any())
            {
                throw new ArgumentException("At least one value is required", parameterName);
            }
        }

        /// <summary>
        /// Throws when a required text parameter is <c>null</c>, empty or whitespace
        /// </summary>
        /// <param name="value">
        /// The value of the parameter
        /// </param>
        /// <param name="parameterName">
        /// The name of the parameter
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="value"/> is <c>null</c>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="value"/> is empty or whitespace
        /// </exception>
        private static void ThrowIfNullOrWhiteSpace(string value, string parameterName)
        {
            if (value is null)
            {
                throw new ArgumentNullException(parameterName);
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("A non-whitespace value is required", parameterName);
            }
        }

        /// <summary>
        /// Throws when a required request body is <c>null</c>
        /// </summary>
        /// <param name="value">
        /// The request body
        /// </param>
        /// <param name="parameterName">
        /// The name of the parameter
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="value"/> is <c>null</c>
        /// </exception>
        private static void ThrowIfNull(object value, string parameterName)
        {
            if (value is null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// Queries the page that the link with the specified relation type of the <c>Link</c> header points to
        /// </summary>
        /// <param name="response">
        /// The <see cref="HttpResponseMessage"/> whose <c>Link</c> header is read
        /// </param>
        /// <param name="relationTypes">
        /// The relation types, any of which selects the link
        /// </param>
        /// <returns>
        /// The <see cref="Page"/> carrying the token exactly as the service returned it, or <c>null</c> when the header has no such link or the link carries no page token
        /// </returns>
        private static Page? QueryLinkedPage(HttpResponseMessage response, params string[] relationTypes)
        {
            if (!response.Headers.TryGetValues("Link", out var headerValues))
            {
                return null;
            }

            var target = headerValues
                .SelectMany(headerValue => LinkValuePattern().Matches(headerValue).Cast<Match>())
                .Where(match => relationTypes.Contains(match.Groups["relation"].Value, StringComparer.OrdinalIgnoreCase))
                .Select(match => match.Groups["target"].Value)
                .FirstOrDefault();

            if (target is null)
            {
                return null;
            }

            var queryStart = target.IndexOf('?');

            var token = queryStart < 0
                ? null
                : target.Substring(queryStart + 1)
                    .Split('&')
                    .Select(pair => pair.Split(['='], 2))
                    .Where(pair => pair.Length == 2)
                    .Where(pair => Uri.UnescapeDataString(pair[0]) is "page[after]" or "page[before]")
                    .Select(pair => Uri.UnescapeDataString(pair[1]))
                    .FirstOrDefault();

            return string.IsNullOrWhiteSpace(token) ? null : new Page(token);
        }

        /// <summary>
        /// Creates the JSON body of a request from a request payload
        /// </summary>
        /// <param name="requestBody">
        /// The <see cref="IRequest"/> to send, or <c>null</c> to send no body
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the serialization
        /// </param>
        /// <returns>
        /// The <see cref="HttpContent"/>, or <c>null</c> when <paramref name="requestBody"/> is <c>null</c>
        /// </returns>
        private async Task<HttpContent> CreateContentAsync(IRequest requestBody, CancellationToken cancellationToken)
        {
            if (requestBody is null)
            {
                return null;
            }

            var stream = new MemoryStream();

            await this.serializer.SerializeRequestAsync(requestBody, SerializationModeKind.JSON, false, stream, WriterOptions, cancellationToken);

            return CreateContent(stream);
        }

        /// <summary>
        /// Creates the JSON body of a request from a sequence of data items
        /// </summary>
        /// <param name="requestBody">
        /// The <see cref="IData"/> items to send, or <c>null</c> to send no body
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the serialization
        /// </param>
        /// <returns>
        /// The <see cref="HttpContent"/>, or <c>null</c> when <paramref name="requestBody"/> is <c>null</c>
        /// </returns>
        private async Task<HttpContent> CreateContentAsync(IEnumerable<IData> requestBody, CancellationToken cancellationToken)
        {
            if (requestBody is null)
            {
                return null;
            }

            var stream = new MemoryStream();

            await this.serializer.SerializeAsync(requestBody, SerializationModeKind.JSON, false, stream, WriterOptions, cancellationToken);

            return CreateContent(stream);
        }

        /// <summary>
        /// Wraps a serialized payload in an <see cref="HttpContent"/> of the JSON media type
        /// </summary>
        /// <param name="stream">
        /// The <see cref="MemoryStream"/> that holds the serialized payload
        /// </param>
        /// <returns>
        /// The <see cref="StreamContent"/>
        /// </returns>
        private static StreamContent CreateContent(MemoryStream stream)
        {
            stream.Position = 0;

            var content = new StreamContent(stream);
            content.Headers.ContentType = new MediaTypeHeaderValue(JsonMediaType);

            return content;
        }

        /// <summary>
        /// Reads the body of a response as a <see cref="Stream"/>
        /// </summary>
        /// <param name="content">
        /// The <see cref="HttpContent"/> to read
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the read
        /// </param>
        /// <returns>
        /// The <see cref="Stream"/>, which the caller disposes
        /// </returns>
        private static Task<Stream> ReadContentStreamAsync(HttpContent content, CancellationToken cancellationToken)
        {
#if NET5_0_OR_GREATER
            return content.ReadAsStreamAsync(cancellationToken);
#else
            cancellationToken.ThrowIfCancellationRequested();

            return content.ReadAsStreamAsync();
#endif
        }

        /// <summary>
        /// Throws a <see cref="PsmApiException"/> carrying the deserialized <see cref="Error"/> when the response is not successful
        /// </summary>
        /// <param name="response">
        /// The <see cref="HttpResponseMessage"/> to check
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the deserialization
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        /// <exception cref="PsmApiException">
        /// Thrown when the response has an error status code
        /// </exception>
        private async Task EnsureSuccessAsync(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            Error error = null;

            if (response.Content?.Headers.ContentType?.MediaType == JsonMediaType)
            {
                await using var stream = await ReadContentStreamAsync(response.Content, cancellationToken);

                error = await this.ReadErrorAsync(stream, response.StatusCode, cancellationToken);
            }

            throw new PsmApiException(response.StatusCode, error);
        }

        /// <summary>
        /// Reads the <see cref="Error"/> carried by the body of an error response
        /// </summary>
        /// <param name="stream">
        /// The <see cref="Stream"/> that holds the body of the response
        /// </param>
        /// <param name="statusCode">
        /// The <see cref="HttpStatusCode"/> of the response, used to log an unreadable body
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the deserialization
        /// </param>
        /// <returns>
        /// The <see cref="Error"/>, or <c>null</c> when the body is not a readable <see cref="Error"/>
        /// </returns>
        private async Task<Error> ReadErrorAsync(Stream stream, HttpStatusCode statusCode, CancellationToken cancellationToken)
        {
            try
            {
                return await this.deSerializer.DeSerializeResponseAsync<Error>(stream, SerializationModeKind.JSON, false, cancellationToken);
            }
            catch (Exception exception) when (exception is JsonException or SerializationException or InvalidOperationException)
            {
                this.logger.LogWarning(exception, "The {StatusCode} response does not carry a readable Error", statusCode);
                return null;
            }
        }

        /// <summary>
        /// Throws a <see cref="MergeConflictException"/> carrying the conflicting data when the response reports a conflict with a JSON body
        /// </summary>
        /// <param name="response">
        /// The <see cref="HttpResponseMessage"/> to check
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the deserialization
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        /// <exception cref="MergeConflictException">
        /// Thrown when the response has the <see cref="HttpStatusCode.Conflict"/> status code and a JSON body listing the conflicting data
        /// </exception>
        /// <exception cref="PsmApiException">
        /// Thrown when the response has the <see cref="HttpStatusCode.Conflict"/> status code and a JSON body that does not list the conflicting data
        /// </exception>
        private async Task ThrowOnConflictAsync(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            if (response.StatusCode != HttpStatusCode.Conflict || response.Content?.Headers.ContentType?.MediaType != JsonMediaType)
            {
                return;
            }

            await using var buffer = new MemoryStream();

            await using (var stream = await ReadContentStreamAsync(response.Content, cancellationToken))
            {
                await stream.CopyToAsync(buffer, cancellationToken);
            }

            buffer.Position = 0;

            IEnumerable<DataIdentity> conflicts;

            try
            {
                conflicts = await this.deSerializer.DeSerializeResponsesAsync<DataIdentity>(buffer, SerializationModeKind.JSON, false, cancellationToken);
            }
            catch (Exception exception) when (exception is JsonException or SerializationException or InvalidOperationException)
            {
                this.logger.LogWarning(exception, "The {StatusCode} response does not carry readable conflicting data", response.StatusCode);

                buffer.Position = 0;

                throw new PsmApiException(response.StatusCode, await this.ReadErrorAsync(buffer, response.StatusCode, cancellationToken));
            }

            throw new MergeConflictException(conflicts.ToList());
        }

        /// <summary>
        /// Reads a single response payload
        /// </summary>
        /// <typeparam name="T">
        /// The <see cref="IResponse"/> type of the payload
        /// </typeparam>
        /// <param name="response">
        /// The successful <see cref="HttpResponseMessage"/>
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the deserialization
        /// </param>
        /// <returns>
        /// The payload, or <c>null</c> when the response has the <see cref="HttpStatusCode.NoContent"/> status code
        /// </returns>
        private async Task<T> ReadResponseAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken) where T : IResponse
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return default;
            }

            await using var stream = await ReadContentStreamAsync(response.Content, cancellationToken);

            return await this.deSerializer.DeSerializeResponseAsync<T>(stream, SerializationModeKind.JSON, false, cancellationToken);
        }

        /// <summary>
        /// Reads one page of response payloads together with the pages that the <c>Link</c> header points to
        /// </summary>
        /// <typeparam name="T">
        /// The <see cref="IResponse"/> type of the payloads
        /// </typeparam>
        /// <param name="response">
        /// The successful <see cref="HttpResponseMessage"/>
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the deserialization
        /// </param>
        /// <returns>
        /// The <see cref="PagedResult{T}"/>
        /// </returns>
        private async Task<PagedResult<T>> ReadPagedResponseAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken) where T : IResponse
        {
            await using var stream = await ReadContentStreamAsync(response.Content, cancellationToken);

            var records = await this.deSerializer.DeSerializeResponsesAsync<T>(stream, SerializationModeKind.JSON, false, cancellationToken);

            return CreatePagedResult(response, records.ToList());
        }

        /// <summary>
        /// Reads a single data payload, such as an element
        /// </summary>
        /// <typeparam name="T">
        /// The <see cref="IData"/> type of the payload
        /// </typeparam>
        /// <param name="response">
        /// The successful <see cref="HttpResponseMessage"/>
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the deserialization
        /// </param>
        /// <returns>
        /// The payload
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the response does not carry exactly one data item
        /// </exception>
        private async Task<T> ReadDataAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken) where T : IData
        {
            await using var stream = await ReadContentStreamAsync(response.Content, cancellationToken);

            var data = await this.deSerializer.DeSerializeAsync<T>(stream, SerializationModeKind.JSON, SerializationTargetKind.PSM, false, cancellationToken);

            return data.Single();
        }

        /// <summary>
        /// Reads one page of data payloads, such as elements, together with the pages that the <c>Link</c> header points to
        /// </summary>
        /// <typeparam name="T">
        /// The <see cref="IData"/> type of the payloads
        /// </typeparam>
        /// <param name="response">
        /// The successful <see cref="HttpResponseMessage"/>
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the deserialization
        /// </param>
        /// <returns>
        /// The <see cref="PagedResult{T}"/>
        /// </returns>
        private async Task<PagedResult<T>> ReadPagedDataAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken) where T : IData
        {
            await using var stream = await ReadContentStreamAsync(response.Content, cancellationToken);

            var data = await this.deSerializer.DeSerializeAsync<T>(stream, SerializationModeKind.JSON, SerializationTargetKind.PSM, false, cancellationToken);

            return CreatePagedResult(response, data.ToList());
        }

        /// <summary>
        /// Reads a payload that has no data transfer object, such as a JSON schema
        /// </summary>
        /// <param name="response">
        /// The successful <see cref="HttpResponseMessage"/>
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the parsing
        /// </param>
        /// <returns>
        /// The parsed <see cref="JsonDocument"/>, which the caller disposes
        /// </returns>
        private static async Task<JsonDocument> ReadJsonAsync(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            await using var stream = await ReadContentStreamAsync(response.Content, cancellationToken);

            return await JsonDocument.ParseAsync(stream, default, cancellationToken);
        }

        /// <summary>
        /// Reads a payload that has no data transfer object, such as a JSON schema, together with the pages that the <c>Link</c> header points to
        /// </summary>
        /// <param name="response">
        /// The successful <see cref="HttpResponseMessage"/>
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the parsing
        /// </param>
        /// <returns>
        /// The <see cref="PagedResult{T}"/> whose single record is the parsed <see cref="JsonDocument"/>, which the caller disposes
        /// </returns>
        private static async Task<PagedResult<JsonDocument>> ReadPagedJsonAsync(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            var document = await ReadJsonAsync(response, cancellationToken);

            return CreatePagedResult(response, [document]);
        }

        /// <summary>
        /// Creates a <see cref="PagedResult{T}"/> from the records of a response and the pages that its <c>Link</c> header points to
        /// </summary>
        /// <typeparam name="T">
        /// The type of the records
        /// </typeparam>
        /// <param name="response">
        /// The successful <see cref="HttpResponseMessage"/>
        /// </param>
        /// <param name="records">
        /// The records of the page
        /// </param>
        /// <returns>
        /// The <see cref="PagedResult{T}"/>
        /// </returns>
        private static PagedResult<T> CreatePagedResult<T>(HttpResponseMessage response, IReadOnlyList<T> records)
        {
            return new PagedResult<T>
            {
                Records = records,
                Next = QueryLinkedPage(response, "next"),
                Previous = QueryLinkedPage(response, "prev", "previous")
            };
        }

        /// <summary>
        /// Sends a request to the server
        /// </summary>
        /// <param name="method">
        /// The <see cref="HttpMethod"/> of the request
        /// </param>
        /// <param name="requestUri">
        /// The request URI, relative to the <see cref="HttpClient.BaseAddress"/>
        /// </param>
        /// <param name="content">
        /// The body of the request, or <c>null</c> to send no body
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the request
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>, which the caller disposes
        /// </returns>
        private async Task<HttpResponseMessage> SendAsync(HttpMethod method, string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            using var requestMessage = new HttpRequestMessage(method, requestUri);
            requestMessage.Content = content;
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonMediaType));

            var response = await this.httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            if (this.logger.IsEnabled(LogLevel.Debug))
            {
                this.logger.LogDebug("{Method} {RequestUri} responded {StatusCode}", method, requestUri, response.StatusCode);
            }

            return response;
        }
    }
}
