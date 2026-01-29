// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreEnumProviderGenerator.cs" company="Starion Group S.A.">
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
    using SysML2.NET.CodeGenerator.UmlHandleBarHelpers;

    using uml4net.Extensions;
    using uml4net.HandleBars;
    using uml4net.SimpleClassifiers;
    using uml4net.xmi.Readers;

    using ClassHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.ClassHelper;

    /// <summary>
    /// A UML Handlebars based EnumProvider code generator
    /// </summary>
    public class UmlCoreEnumProviderGenerator : UmlHandleBarsGenerator
    {
        /// <summary>
        /// Generates the Core SysML2 model Enumeration Providers
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        public override async Task GenerateAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, ModelKind modelKind)
        {
            await this.GenerateEnumerationProvidersAsync(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates Enumerations
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult" /> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <returns>
        /// an awaitable task
        /// </returns>
        public Task GenerateEnumerationProvidersAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);

            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GenerateEnumerationProvidersInternalAsync(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates Enumeration Provider
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult" /> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <param name="name">
        /// The name of the Enumeration to generate
        /// </param>
        /// <returns>
        /// an awaitable task
        /// </returns>
        public Task<string> GenerateEnumerationProviderAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string name)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);

            ArgumentNullException.ThrowIfNull(outputDirectory);
            ArgumentException.ThrowIfNullOrEmpty(name);

            return this.GenerateEnumerationProviderInternalAsync(xmiReaderResult, outputDirectory, name);
        }

        /// <summary>
        /// Register the custom helpers
        /// </summary>
        protected override void RegisterHelpers()
        {
            StringHelper.RegisterStringHelper(this.Handlebars);
            IEnumerableHelper.RegisterEnumerableHelper(this.Handlebars);
            uml4net.HandleBars.ClassHelper.RegisterClassHelper(this.Handlebars);
            PropertyHelper.RegisterPropertyHelper(this.Handlebars);
            GeneralizationHelper.RegisterGeneralizationHelper(this.Handlebars);
            DocumentationHelper.RegisterDocumentationHelper(this.Handlebars);
            EnumHelper.RegisterEnumHelper(this.Handlebars);
            DecoratorHelper.RegisterDecoratorHelper(this.Handlebars);

            EnumerationLiteralHelper.RegisterTypeNameHelper(this.Handlebars);
            ClassHelper.RegisterClassHelper(this.Handlebars);
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate("core-enumprovider-uml-template");
        }

        /// <summary>
        /// Generates Enumeration
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult" /> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <returns>
        /// an awaitable task
        /// </returns>
        private async Task GenerateEnumerationProvidersInternalAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates["core-enumprovider-uml-template"];

            var enumerations = xmiReaderResult.QueryRoot(null, name: "SysML").QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IEnumeration>())
                .ToList();

            foreach (var enumeration in enumerations)
            {
                var generatedEnumerationProvider = template(enumeration);

                generatedEnumerationProvider = this.CodeCleanup(generatedEnumerationProvider);

                var fileName = $"{enumeration.Name.CapitalizeFirstLetter()}Provider.cs";

                await WriteAsync(generatedEnumerationProvider, outputDirectory, fileName);
            }
        }

        /// <summary>
        /// Generates EnumerationProvider classes
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult" /> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <param name="name">
        /// The name of the Enumeration to generate
        /// </param>
        /// <returns>
        /// an awaitable task
        /// </returns>
        private async Task<string> GenerateEnumerationProviderInternalAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string name)
        {
            var template = this.Templates["core-enumprovider-uml-template"];

            var enumerations = xmiReaderResult.QueryRoot(null, name: "SysML").QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IEnumeration>())
                .ToList();

            var enumeration = enumerations.Single(x => x.Name == name);

            var generatedProviderEnumeration = template(enumeration);

            generatedProviderEnumeration = this.CodeCleanup(generatedProviderEnumeration);

            var fileName = $"{enumeration.Name.CapitalizeFirstLetter()}Provider.cs";

            await WriteAsync(generatedProviderEnumeration, outputDirectory, fileName);

            return generatedProviderEnumeration;
        }
    }
}
