// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreDalPocoExtensionsGenerator.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using SysML2.NET.CodeGenerator.Enumeration;

    using uml4net.Extensions;
    using uml4net.HandleBars;
    using uml4net.StructuredClassifiers;
    using uml4net.xmi.Readers;

    using NamedElementHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.NamedElementHelper;
    using PropertyHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.PropertyHelper;

    /// <summary>
    /// A UML Handlebars based POCO Extension code generator
    /// </summary>
    public class UmlCoreDalPocoExtensionsGenerator : UmlHandleBarsGenerator
    {
        /// <summary>
        /// Gets the name of the template used to generate POCO Element extension
        /// </summary>
        private const string ElementExtensionsTemplateName = "core-dal-element-uml-extensions";
        
        /// <summary>
        /// Gets the name of the template used to generate POCO Elements extension
        /// </summary>
        private const string PocoExtensionsTemplateName = "core-dal-poco-uml-extensions";

        /// <summary>
        /// Generates code specific to the concrete implementation
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        public override async Task GenerateAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, ModelKind modelKind)
        {
            await this.GenerateElementExtensions(xmiReaderResult, outputDirectory);
            await this.GeneratePocoExtensions(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates the static poco extensions for the DAL library
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// In case of null value for <paramref name="xmiReaderResult" /> or
        /// <paramref name="outputDirectory" />
        /// </exception>
        private Task GeneratePocoExtensions(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GeneratePocoExtensionsInternal(xmiReaderResult, outputDirectory);
        }
        
        /// <summary>
        /// Generates the static poco extensions for the DAL library
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        private async Task GeneratePocoExtensionsInternal(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates[PocoExtensionsTemplateName];

            var classes = xmiReaderResult.QueryRoot(null, name: "SysML").QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .ToList();

            foreach (var classToGenerate in classes)
            {
                var generatedPocoExtension = template(classToGenerate);

                generatedPocoExtension = this.CodeCleanup(generatedPocoExtension);

                var  fileName = $"{classToGenerate.Name}Extensions.cs";

                await Write(generatedPocoExtension, outputDirectory, fileName);
            }
        }

        /// <summary>
        /// Generates the static poco extensions for the DAL library for a specific class
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <param name="className">The name of the class to generate</param>
        /// <returns>
        /// an awaitable <see cref="Task" /> with the generated code
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// In case of null value for <paramref name="xmiReaderResult" /> or
        /// <paramref name="outputDirectory" />
        /// </exception>
        /// <exception cref="ArgumentException">In case of null or whitespace value for the <paramref name="className"/></exception>
        public Task<string> GenerateDalPocoExtension(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string className)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            ArgumentException.ThrowIfNullOrWhiteSpace(className);

            return this.GenerateDalPocoExtensionInternal(xmiReaderResult, outputDirectory, className);
        }

        /// <summary>
        /// Generates the static poco extensions for the DAL library for a specific class
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <param name="className">The name of the class to generate</param>
        /// <returns>
        /// an awaitable <see cref="Task" /> with the generated code
        /// </returns>
        private async Task<string> GenerateDalPocoExtensionInternal(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string className)
        {
            var template = this.Templates[PocoExtensionsTemplateName];

            var classToGenerate = xmiReaderResult.QueryRoot(null, name: "SysML").QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Single(x => x.Name == className);
            
            var generatedJsonSerializer = template(classToGenerate);

            generatedJsonSerializer = this.CodeCleanup(generatedJsonSerializer);

            var fileName = $"{classToGenerate.Name.CapitalizeFirstLetter()}Extensions.cs";

            await WriteAsync(generatedJsonSerializer, outputDirectory, fileName);
            return generatedJsonSerializer;
        }

        /// <summary>
        /// Register the custom helpers
        /// </summary>
        protected override void RegisterHelpers()
        {
            this.Handlebars.RegisterStringHelper();
            this.Handlebars.RegisterClassHelper();
            this.Handlebars.RegisterPropertyHelper();

            NamedElementHelper.RegisterNamedElementHelper(this.Handlebars);
            PropertyHelper.RegisterPropertyHelper(this.Handlebars);
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate(ElementExtensionsTemplateName);
            this.RegisterTemplate(PocoExtensionsTemplateName);
            this.RegisterPartialTemplate("core-dal-poco-uml-partial-extensions");
        }

        /// <summary>
        /// Generates the static poco extensions for the DAL library
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// In case of null value for <paramref name="xmiReaderResult" /> or
        /// <paramref name="outputDirectory" />
        /// </exception>
        private Task GenerateElementExtensions(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GenerateElementExtensionsInternal(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates the static poco extensions for the DAL library
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        private async Task GenerateElementExtensionsInternal(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates[ElementExtensionsTemplateName];

            var classes = xmiReaderResult.QueryRoot(null, name: "SysML").QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .OrderBy(x => x.Name)
                .ToList();

            var generatedElementExtensions = template(classes);

            generatedElementExtensions = this.CodeCleanup(generatedElementExtensions);

            const string fileName = "ElementExtensions.cs";

            await Write(generatedElementExtensions, outputDirectory, fileName);
        }
    }
}
