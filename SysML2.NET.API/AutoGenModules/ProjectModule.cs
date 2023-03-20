// -------------------------------------------------------------------------------------------------
// <copyright file="PartDefinitionModule.cs" company="RHEA System S.A.">
// 
//   Copyright 2022-2023 RHEA System S.A.
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.API.AutoGenModules
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
    using SysML2.NET.API.Modules;
    using SysML2.NET.Serializer.Json;
    using SysML2.NET.API.Services;
    using System.Diagnostics;
    using System.Linq;
    using PIM;
    using SysML2.NET.Serializer;

    public partial class ProjectModule : BaseModule, ICarterModule
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
            app.MapGet("/Project", async (HttpRequest req, HttpResponse res) =>
            {
                try
                {
                    var dataItems = await projectService.Read(Guid.Empty,0 , 0);

                    var jsonWriterOptions = new JsonWriterOptions
                    {
                        Indented = true,
                        SkipValidation = false
                    };

                    await this.WriteResultsToResponse(dataItems, SerializationModeKind.JSON, res, jsonWriterOptions);
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
