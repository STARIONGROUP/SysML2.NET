// -------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace SysML2.NET.Viewer
{
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;
    using System.Threading.Tasks;
    using Blazored.SessionStorage;
    using BlazorStrap;

    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

    using Microsoft.Extensions.DependencyInjection;
    
    using Radzen;

    using Serilog;
    using Serilog.Events;
    
    using SySML2.NET.REST;
    using SysML2.NET.Serializer.Json;
    using SysML2.NET.Viewer.Services.Authentication;

    /// <summary>
    /// The purpose of the <see cref="Program"/> class is to provide the
    /// main entry point of te application
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main entry point of the application
        /// </summary>
        /// <param name="args">Command line arguments</param>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.BrowserConsole()
                .CreateLogger();

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true));

            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            
            AddServices(builder);
            AddViewModels(builder);
            
            await builder.Build().RunAsync();
        }

        /// <summary>
        /// Register all services into the <see cref="WebAssemblyHostBuilder.Services" />
        /// </summary>
        /// <param name="builder">The <see cref="WebAssemblyHostBuilder" /></param>
        private static void AddServices(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddBlazoredSessionStorage();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();

            builder.Services.AddBlazorStrap();

            builder.Services.AddScoped(sp => new HttpClient());
            
            builder.Services.AddScoped<AuthenticationStateProvider, AnonymousAuthenticationStateProvider>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IRestClient, RestClient>();
            builder.Services.AddScoped<IDeSerializer, DeSerializer>();
            builder.Services.AddScoped<ISerializer, Serializer>();
        }

        /// <summary>
        /// Register all ViewModels into the <see cref="WebAssemblyHostBuilder" />
        /// </summary>
        /// <param name="builder">The <see cref="WebAssemblyHostBuilder" /></param>
        private static void AddViewModels(WebAssemblyHostBuilder builder)
        {
            var viewModelInterfaces = Assembly.GetCallingAssembly().GetExportedTypes()
                .Where(x => x.IsInterface && x.Name.EndsWith("ViewModel")).ToList();

            foreach (var viewModelInterface in viewModelInterfaces)
            {
                var viewModel = Assembly.GetCallingAssembly().GetExportedTypes()
                    .FirstOrDefault(x => x.IsClass
                                         && x.Name == viewModelInterface.Name.Remove(0, 1)
                                         && x.GetInterface(viewModelInterface.Name) == viewModelInterface);

                if (viewModel != null)
                {
                    builder.Services.AddTransient(viewModelInterface, viewModel);
                }
            }
        }
    }
}
