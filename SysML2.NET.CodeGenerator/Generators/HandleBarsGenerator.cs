// -------------------------------------------------------------------------------------------------
// <copyright file="HandleBarsGenerator.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Generators
{
    using System.Collections.Generic;
    using System.IO;
    using HandlebarsDotNet;
    using HandlebarsDotNet.Helpers;

    /// <summary>
    /// Abstract super class from which all <see cref="HandlebarsDotNet"/> generators
    /// need to derive
    /// </summary>
    public abstract class HandleBarsGenerator : Generator
    {
        /// <summary>
        /// The <see cref="IHandlebars"/> instance used to generate code with
        /// </summary>
        protected IHandlebars Handlebars;

        /// <summary>
        /// Initializes a new instance of the <see cref="HandleBarsGenerator"/> class/
        /// </summary>
        protected HandleBarsGenerator()
        {
            Templates = new Dictionary<string, HandlebarsTemplate<object, object>>();

            Handlebars = HandlebarsDotNet.Handlebars.CreateSharedEnvironment();
            HandlebarsHelpers.Register(Handlebars);

            RegisterHelpers();
            RegisterTemplates();
        }

        /// <summary>
        /// Gets the registered <see cref="HandlebarsTemplate{TContext,TData}"/>
        /// </summary>
        public Dictionary<string, HandlebarsTemplate<object, object>> Templates { get; private set; }

        /// <summary>
        /// Register the custom helpers
        /// </summary>
        protected abstract void RegisterHelpers();

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected abstract void RegisterTemplates();

        /// <summary>
        /// Register handle-bars templates based on the template (file) name (without extension)
        /// </summary>
        /// <param name="name">
        /// (file) name (without extension)
        /// </param>
        protected void RegisterTemplate(string name)
        {
            var templatePath = Path.Combine(TemplateFolderPath, $"{name}.hbs");
 
            var template = File.ReadAllText(templatePath);

            var compiledTemplate = Handlebars.Compile(template);

            Templates.Add(name, compiledTemplate);
        }
    }
}
