// -------------------------------------------------------------------------------------------------
// <copyright file="StartUp.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.API
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Autofac;

    using Carter;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using SysML2.NET.API.PSM.AutoGenServices;
    using SysML2.NET.API.Services;
    using SysML2.NET.OGM.Repository;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The <see cref="StartUp"/> used to configure the application
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class StartUp
    {
        /// <summary>
        /// The Cookie Scheme used by the Marvin API
        /// </summary>
        public const string CookieScheme = "SysML2.NET";

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">
        /// The <see cref="IConfiguration"/> of the application.
        /// </param>
        /// <param name="environment">
        /// The <see cref="IWebHostEnvironment"/> of the application
        /// </param>
        public StartUp(IConfiguration configuration, IWebHostEnvironment environment)
        {
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });
            
            services.AddCarter();
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you. If you
        // need a reference to the container, you need to use the
        // "Without ConfigureContainer" mechanism shown later.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<SysML2.NET.Serializer.Json.Serializer>().As<ISerializer>();
            builder.RegisterType<SysML2.NET.Serializer.Json.DeSerializer>().As<IDeSerializer>();

            builder.RegisterType<ProjectService>().As<IProjectService>();
            builder.RegisterType<ProjectRepository>().As<IProjectRepository>();

            var carterModulessInAssembly = typeof(StartUp).Assembly.GetExportedTypes()
                .Where(type => typeof(ICarterModule).IsAssignableFrom(type)).ToArray();
            builder.RegisterTypes(carterModulessInAssembly).PropertiesAutowired();
        }

        // Configure is where you add middleware. This is called after
        // ConfigureContainer. You can use IApplicationBuilder.ApplicationServices
        // here if you need to resolve things from the container.
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseCors();
            app.UseEndpoints(builder => builder.MapCarter());
        }
    }
}
