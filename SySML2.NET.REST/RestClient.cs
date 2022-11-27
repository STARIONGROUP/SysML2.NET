// -------------------------------------------------------------------------------------------------
// <copyright file="RestClient.cs" company="RHEA System S.A.">
// 
//   Copyright 2022 RHEA System S.A.
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

namespace SySML2.NET.REST
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Threading;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.API;
    using SysML2.NET.API.DTO;
    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;
    using SysML2.NET.Serializer.Json;
    using SysML2.NET.Serializer;

    /// <summary>
    /// The purpose of the <see cref="RestClient"/> is to provide an abstraction over the SysML REST/HTTP PSM protocol
    /// </summary>
    public class RestClient : IRestClient
    {
        /// <summary>
        /// Dependency injected <see cref="HttpClient"/> used to perform HTTP requests on the REST API
        /// </summary>
        private readonly HttpClient httpClient;

        /// <summary>
        /// Dependency injected <see cref="IDeSerializer"/> used to deserialize from JSON
        /// </summary>
        private readonly IDeSerializer deserializer;

        /// <summary>
        /// Dependency injected <see cref="ISerializer"/> used to serialize to JSON
        /// </summary>
        private readonly ISerializer serializer;

        /// <summary>
        /// The base uri or hostname of the model server REST API
        /// </summary>
        private string baseUri;

        /// <summary>
        /// The <see cref="ILogger"/> used to log
        /// </summary>
        private readonly ILogger<RestClient> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Session"/> class
        /// </summary>
        /// <param name="httpClient">
        /// Dependency injected <see cref="HttpClient"/> used to perform HTTP requests
        /// </param>
        /// <param name="deserializer">
        /// Dependency injected <see cref="IDeSerializer"/> used to deserialize from JSON
        /// </param>
        /// <param name="serializer">
        /// Dependency injected <see cref="ISerializer"/> used to serialize to JSON
        /// </param>
        /// /// <param name="loggerFactory">
        /// The (injected) <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        public RestClient(HttpClient httpClient, IDeSerializer deserializer, ISerializer serializer, ILoggerFactory loggerFactory = null)
        {
            this.httpClient = httpClient;
            this.deserializer = deserializer;
            this.serializer = serializer;

            this.logger = loggerFactory == null ? NullLogger<RestClient>.Instance : loggerFactory.CreateLogger<RestClient>();
        }

        /// <summary>
        /// Opens a connection to the SysML2 model server REST API
        /// </summary>
        /// <param name="username">
        /// The username used to login a SysML2 model server
        /// </param>
        /// <param name="password">
        /// The password (secret) used to login a SysML2 model server
        /// </param>
        /// <param name="uri">
        /// The uri of the model-server
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        /// <returns>
        /// The list of <see cref="Project"/>s that the user has access to
        /// </returns>
        public async Task<IEnumerable<Project>> Open(string username, string password, string uri, CancellationToken cancellationToken)
        {
            try
            {
                this.baseUri = uri;
                
                var projects = await this.RequestProjects(null, null, cancellationToken);
                return projects;
            }
            catch (Exception)
            {
                this.baseUri = null;
                throw;
            }
        }

        /// <summary>
        /// Closes a connection to the SysML2 model server REST API
        /// </summary>
        public void Close()
        {
            this.baseUri = null;
        }

        /// <summary>
        /// Requests the <see cref="Project"/>s from the SysML2 model server REST API
        /// </summary>
        /// <param name="project">
        /// The unique identifier of the <see cref="Project"/> to read. In case this is null all <see cref="Project"/>s are read.
        /// </param>
        /// <param name="queryParameters">
        /// The <see cref="QueryParameters"/> used to filter the response on the SysML2 model server REST API
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        /// <returns>
        /// The list of <see cref="Project"/>s that are available on the SysML2 model server REST API
        /// </returns>
        public async Task<IEnumerable<Project>> RequestProjects(Guid? project, QueryParameters queryParameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(this.baseUri))
            {
                throw new InvalidOperationException("prior to executing a REST request the client must be opened. Please use the Open method to do so");
            }

            var requestUri = project == null ? new Uri($"{this.baseUri}/projects{queryParameters}") : new Uri($"{this.baseUri}/projects/{project}{queryParameters}");

            var data = await this.RequestData(requestUri, cancellationToken);
            
            if (data != null && data.Any())
            {
                return data.OfType<Project>();
            }

            return Enumerable.Empty<Project>();
        }

        /// <summary>
        /// Requests the <see cref="Branch"/>es in a <see cref="Project"/> from the SysML2 model server REST API
        /// </summary>
        /// <param name="project">
        /// The unique identifier of the <see cref="Project"/> that contains the requested <see cref="Branch"/>
        /// </param>
        /// <param name="branch">
        /// The unique identifier of the <see cref="Branch"/> to read. In case this is null all <see cref="Branch"/>s are read.
        /// </param>
        /// <param name="queryParameters">
        /// The <see cref="QueryParameters"/> used to filter the response on the SysML2 model server REST API
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        /// <returns>
        /// A list of <see cref="Branch"/>es that are contained by the specified <see cref="Project"/>, or a single <see cref="Branch"/> if so requested
        /// </returns>
        public async Task<IEnumerable<Branch>> RequestBranches(Guid project, Guid? branch, QueryParameters queryParameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(this.baseUri))
            {
                throw new InvalidOperationException("prior to executing a REST request the client must be opened. Please use the Open method to do so");
            }

            if (project == Guid.Empty)
            {
                throw new ArgumentException("The project unique identifier may not be the empty Guid", nameof(project));
            }

            var requestUri = branch == null ? new Uri($"{this.baseUri}/projects/{project}/branches{queryParameters}") : new Uri($"{this.baseUri}/projects/{project}/branches/{branch}{queryParameters}");

            this.logger.LogDebug(requestUri.ToString());

            var data = await this.RequestData(requestUri, cancellationToken);

            if (data != null && data.Any())
            {
                return data.OfType<Branch>();
            }

            return Enumerable.Empty<Branch>();
        }

        /// <summary>
        /// Requests the <see cref="Commit"/>s in a <see cref="Project"/> from the SysML2 model server REST API
        /// </summary>
        /// <param name="project">
        /// The unique identifier of the <see cref="Project"/> that contains the requested <see cref="Commit"/>
        /// </param>
        /// <param name="commit"></param>
        /// <param name="queryParameters">
        /// The <see cref="QueryParameters"/> used to filter the response on the SysML2 model server REST API
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        /// <returns>
        /// A list of <see cref="Commit"/>s that are contained by the specified <see cref="Project"/>, or a single <see cref="Commit"/> if so requested
        /// </returns>
        public async Task<IEnumerable<Commit>> RequestCommits(Guid project, Guid? commit, QueryParameters queryParameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(this.baseUri))
            {
                throw new InvalidOperationException("prior to executing a REST request the client must be opened. Please use the Open method to do so");
            }

            if (project == Guid.Empty)
            {
                throw new ArgumentException("The project unique identifier may not be the empty Guid", nameof(project));
            }

            var requestUri = commit == null ? new Uri($"{this.baseUri}/projects/{project}/commits{queryParameters}") : new Uri($"{this.baseUri}/projects/{project}/commits/{commit}{queryParameters}");
            var data = await this.RequestData(requestUri, cancellationToken);

            if (data != null && data.Any())
            {
                return data.OfType<Commit>();
            }

            return Enumerable.Empty<Commit>();
        }

        /// <summary>
        /// Requests the <see cref="Tag"/>s in a <see cref="Project"/> from the SysML2 model server REST API
        /// </summary>
        /// <param name="project">
        /// The unique identifier of the <see cref="Project"/> that contains the requested <see cref="Tag"/>
        /// </param>
        /// <param name="tag"></param>
        /// <param name="queryParameters">
        /// The <see cref="QueryParameters"/> used to filter the response on the SysML2 model server REST API
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        /// <returns>
        /// A list of <see cref="Tag"/>s that are contained by the specified <see cref="Project"/>, or a single <see cref="Tag"/> if so requested
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<Tag>> RequestTags(Guid project, Guid? tag, QueryParameters queryParameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(this.baseUri))
            {
                throw new InvalidOperationException("prior to executing a REST request the client must be opened. Please use the Open method to do so");
            }

            if (project == Guid.Empty)
            {
                throw new ArgumentException("The project unique identifier may not be the empty Guid", nameof(project));
            }

            var requestUri = tag == null ? new Uri($"{this.baseUri}/projects/{project}/tags{queryParameters}") : new Uri($"{this.baseUri}/projects/{project}/tags/{tag}{queryParameters}");
            var data = await this.RequestData(requestUri, cancellationToken);

            if (data != null && data.Any())
            {
                return data.OfType<Tag>();
            }

            return Enumerable.Empty<Tag>();
        }

        /// <summary>
        /// Requests the <see cref="Element"/>s in a <see cref="Branch"/> of a <see cref="Project"/> from the SysML2 model server REST API
        /// </summary>
        /// <param name="project">
        /// The unique identifier of the <see cref="Project"/> that contains the requested <see cref="Element"/>s
        /// </param>
        /// <param name="commit">
        /// The unique identifier of the <see cref="Commit"/> that contains the requested <see cref="Element"/>s
        /// </param>
        /// <param name="element">
        /// The unique identifier of the <see cref="Element"/> to read. In case this is null all <see cref="Element"/>s are read.
        /// </param>
        /// <param name="queryParameters">
        /// The <see cref="QueryParameters"/> used to filter the response on the SysML2 model server REST API
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        /// <returns>
        /// A list of <see cref="Element"/>s that are contained by the specified <see cref="Project"/> and <see cref="Commit"/>, or a single <see cref="Element"/> if so requested
        /// </returns>
        public async Task<IEnumerable<IElement>> RequestElements(Guid project, Guid commit, Guid? element, QueryParameters queryParameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(this.baseUri))
            {
                throw new InvalidOperationException("prior to executing a REST request the client must be opened. Please use the Open method to do so");
            }

            if (project == Guid.Empty)
            {
                throw new ArgumentException("The project unique identifier may not be the empty Guid", nameof(project));
            }

            if (commit == Guid.Empty)
            {
                throw new ArgumentException("The commit unique identifier may not be the empty Guid", nameof(commit));
            }

            var requestUri = element == null ? new Uri($"{this.baseUri}/projects/{project}/commits/{commit}/elements{queryParameters}") : new Uri($"{this.baseUri}/projects/{project}/commits/{commit}/elements/{element}{queryParameters}");
            var data = await this.RequestData(requestUri, cancellationToken);

            if (data != null && data.Any())
            {
                return data.OfType<IElement>();
            }

            return Enumerable.Empty<IElement>();
            
        }

        /// <summary>
        /// Requests the root <see cref="Element"/>s in a <see cref="Commit"/> of a <see cref="Project"/> from the SysML2 model server REST API
        /// </summary>
        /// <param name="project">
        /// The unique identifier of the <see cref="Project"/> that contains the requested <see cref="Element"/>s
        /// </param>
        /// <param name="commit">
        /// The unique identifier of the <see cref="Commit"/> that contains the requested <see cref="Element"/>s
        /// </param>
        /// <param name="queryParameters">
        /// The <see cref="QueryParameters"/> used to filter the response on the SysML2 model server REST API
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        /// <returns>
        /// A list of <see cref="Element"/>s that are contained by the specified <see cref="Project"/> and <see cref="Commit"/>, or a single <see cref="Element"/> if so requested
        /// </returns>
        public async Task<IEnumerable<IElement>> RequestRootElements(Guid project, Guid commit, QueryParameters queryParameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(this.baseUri))
            {
                throw new InvalidOperationException("prior to executing a REST request the client must be opened. Please use the Open method to do so");
            }

            if (project == Guid.Empty)
            {
                throw new ArgumentException("The project unique identifier may not be the empty Guid", nameof(project));
            }

            if (commit == Guid.Empty)
            {
                throw new ArgumentException("The commit unique identifier may not be the empty Guid", nameof(commit));
            }

            var requestUri = new Uri($"{this.baseUri}/projects/{project}/commits/{commit}/roots{queryParameters}");
            var data = await this.RequestData(requestUri, cancellationToken);
            if (data != null && data.Any())
            {
                return data.OfType<IElement>();
            }

            return Enumerable.Empty<IElement>();
        }

        /// <summary>
        /// Requests an <see cref="IEnumerable{IData}"/> from the SysML2 model server REST API
        /// </summary>
        /// <param name="requestUri">
        /// The request <see cref="Uri"/> including the query parameters
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        /// <returns></returns>
        private async Task<IEnumerable<IData>> RequestData(Uri requestUri, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("request data from: {0}", requestUri);

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = requestUri,
            };
            
            using var response = await this.httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();

            var result = await this.deserializer.DeSerializeAsync(stream, SerializationModeKind.JSON, SerializationTargetKind.RESTAPI, cancellationToken);

            return result;
        }
    }
}
