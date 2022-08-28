// -------------------------------------------------------------------------------------------------
// <copyright file="IRestClient.cs" company="RHEA System S.A.">
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
    using System.Threading.Tasks;
    using System.Threading;
    
    using SysML2.NET.API;
    using SysML2.NET.API.DTO;
    using SysML2.NET.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="IRestClient"/> is to provide an abstraction over the SysML REST/HTTP PSM protocol
    /// </summary>
    public interface IRestClient
    {
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
        Task<IEnumerable<Project>> Open(string username, string password, string uri, CancellationToken cancellationToken);

        /// <summary>
        /// Closes a connection to the SysML2 model server REST API
        /// </summary>
        void Close();

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
        Task<IEnumerable<Project>> RequestProjects(Guid? project, QueryParameters queryParameters, CancellationToken cancellationToken);

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
        Task<IEnumerable<Branch>> RequestBranches(Guid project, Guid? branch, QueryParameters queryParameters, CancellationToken cancellationToken);


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
        Task<IEnumerable<Commit>> RequestCommits(Guid project, Guid? commit, QueryParameters queryParameters, CancellationToken cancellationToken);

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
        Task<IEnumerable<Tag>> RequestTags(Guid project, Guid? tag, QueryParameters queryParameters, CancellationToken cancellationToken);

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
        Task<IEnumerable<Element>> RequestElements(Guid project, Guid commit, Guid? element, QueryParameters queryParameters, CancellationToken cancellationToken);

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
        Task<IEnumerable<Element>> RequestRootElements(Guid project, Guid commit, QueryParameters queryParameters, CancellationToken cancellationToken);
    }
}
