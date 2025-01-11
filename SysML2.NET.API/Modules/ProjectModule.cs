// -------------------------------------------------------------------------------------------------
// <copyright file="PartDefinitionModule.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.API.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;

    using Carter;
    using Carter.Response;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.AspNetCore.Http;

    using SysML2.NET.Core.DTO;
    using SysML2.NET.Serializer.Json;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using PIM;
    using PIM.DTO;
    using SysML2.NET.Serializer;
    using SysML2.NET.API.PSM.AutoGenServices;

    /// <summary>
    /// Implements the routes of the ProjectService as defined in the Systems Modeling Application 1Programming Interface (API) and Services specification
    /// </summary>
    public class ProjectModule : BaseModule, ICarterModule
    {
        /// <summary>
        /// The (injected) <see cref="ILogger{ProjectModule}"/>
        /// </summary>
        private readonly ILogger<ProjectModule> logger;

        /// <summary>
        /// The (injected) <see cref="IProjectService"/>
        /// </summary>
        private readonly IProjectService projectService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectModule"/> class
        /// </summary>
        /// <param name="logger">
        /// The (injected) <see cref="ILogger{ProjectModule}"/>
        /// </param>
        public ProjectModule(ILogger<ProjectModule> logger, IProjectService projectService, ISerializer serializer, IDeSerializer deserializer)
            : base(logger, serializer, deserializer)
        {
            this.logger = logger;
            this.projectService = projectService;
        }

        /// <summary>
        /// Adds the specific <see cref="IPartDefinition"/> routes to the
        /// </summary>
        /// <param name="app"></param>
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/projects", async (CancellationToken cancellationToken, HttpRequest req, HttpResponse res) =>
            {
                try
                {
                    var dataItems = await projectService.Read(Guid.Empty, 0, 0, cancellationToken);

                    var jsonWriterOptions = new JsonWriterOptions
                    {
                        Indented = true,
                        SkipValidation = false
                    };

                    res.StatusCode = 200;
                    await WriteResultsToResponse(dataItems, SerializationModeKind.JSON, res, jsonWriterOptions,
                        cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    this.logger.LogInformation("request cancelled");
                    res.StatusCode = 202;
                }
                catch (Exception exception)
                {
                    res.StatusCode = 400;
                    await res.AsJson(exception.Message);
                }
            });

            app.MapGet("/projects/{id:guid}", async (CancellationToken cancellationToken, Guid id, HttpRequest req, HttpResponse res) =>
            {
                try
                {
                    var dataItems = await projectService.Read(id, 0, 0, cancellationToken);

                    var jsonWriterOptions = new JsonWriterOptions
                    {
                        Indented = true,
                        SkipValidation = false
                    };

                    res.StatusCode = 200;
                    await WriteResultsToResponse(dataItems, SerializationModeKind.JSON, res, jsonWriterOptions, cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    this.logger.LogInformation("request cancelled");
                    res.StatusCode = 202;
                }
                catch (Exception exception)
                {
                    res.StatusCode = 400;
                    await res.AsJson(exception.Message);
                }
            });

            app.MapPost("/projects", async (CancellationToken cancellationToken, HttpRequest req, HttpResponse res) =>
            {
                try
                {
                    var stream = new MemoryStream();
                    await req.Body.CopyToAsync(stream, cancellationToken);

                    var dataItems = await this.deserializer.DeSerializeAsync(stream, SerializationModeKind.JSON, SerializationTargetKind.PSM, CancellationToken.None);

                    var project = (Project)dataItems.Single();
                    
                    await projectService.Create(project, cancellationToken);

                    var jsonWriterOptions = new JsonWriterOptions
                    {
                        Indented = true,
                        SkipValidation = false
                    };

                    res.StatusCode = 200;
                    await WriteResultToResponse(project, SerializationModeKind.JSON, res, jsonWriterOptions, cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    this.logger.LogInformation("request cancelled");
                    res.StatusCode = 202;
                }
                catch (Exception exception)
                {
                    res.StatusCode = 400;
                    await res.AsJson(exception.Message);
                }
            });
        }
    }
}
