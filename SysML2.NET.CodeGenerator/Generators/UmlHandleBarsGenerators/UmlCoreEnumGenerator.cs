// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreEnumGenerator.cs" company="Starion Group S.A.">
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

    using SysML2.NET.CodeGenerator.Contexts;
    using SysML2.NET.CodeGenerator.Enumeration;
    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.UmlHandleBarHelpers;

    using uml4net.Extensions;
    using uml4net.HandleBars;
    using uml4net.SimpleClassifiers;
    using uml4net.xmi.Readers;

    using NamedElementHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.NamedElementHelper;

    /// <summary>
    /// A UML Handlebars based enum code generator
    /// </summary>
    public class UmlCoreEnumGenerator : UmlHandleBarsGenerator
    {
        /// <summary>
        /// Gets the name of the enumeration template
        /// </summary>
        private const string EnumTemplateeName = "core-enumeration-uml-template";
        
        /// <summary>
        /// Generates the Core SysML2 model Enumerations
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        public override async Task GenerateAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, ModelKind modelKind)
        {
            await this.GenerateEnumerationsAsync(xmiReaderResult, outputDirectory, modelKind);
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
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>
        /// an awaitable task
        /// </returns>
        public Task GenerateEnumerationsAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, ModelKind modelKind)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GenerateEnumerationsInternalAsync(xmiReaderResult, outputDirectory, modelKind);
        }

        /// <summary>
        /// Generates POCO interfaces
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
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>
        /// an awaitable task
        /// </returns>
        public Task<string> GenerateEnumerationAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string name, ModelKind modelKind)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            ArgumentException.ThrowIfNullOrEmpty(name);

            return this.GenerateEnumerationInternalAsync(xmiReaderResult, outputDirectory, name, modelKind);
        }

        /// <summary>
        /// Register the custom helpers
        /// </summary>
        protected override void RegisterHelpers()
        {
            this.Handlebars.RegisterStringHelper();
            this.Handlebars.RegisterEnumerableHelper();
            this.Handlebars.RegisterClassHelper();
            this.Handlebars.RegisterPropertyHelper();
            this.Handlebars.RegisterGeneralizationHelper();
            this.Handlebars.RegisterDocumentationHelper();
            this.Handlebars.RegisterEnumHelper();
            this.Handlebars.RegisterDecoratorHelper();

            EnumerationLiteralHelper.RegisterTypeNameHelper(this.Handlebars);
            NamedElementHelper.RegisterNamedElementHelper(this.Handlebars);
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate(EnumTemplateeName);
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
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>
        /// an awaitable task
        /// </returns>
        private async Task GenerateEnumerationsInternalAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, ModelKind modelKind)
        {
            var template = this.Templates[EnumTemplateeName];

            var enumerations = xmiReaderResult.QueryRoot(null, name: modelKind.QueryRootNamePerModelKind()).QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IEnumeration>())
                .ToList();

            foreach (var enumeration in enumerations)
            {
                var enumContext = new EnumModelKindContext()
                {
                    Model = modelKind,
                    Enumeration = enumeration
                };
                
                var generatedEnumeration = template(enumContext);

                generatedEnumeration = this.CodeCleanup(generatedEnumeration);

                var fileName = $"{enumeration.Name.CapitalizeFirstLetter()}.cs";

                await WriteAsync(generatedEnumeration, outputDirectory, fileName);
            }
        }

        /// <summary>
        /// Generates POCO interfaces
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
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>
        /// an awaitable task
        /// </returns>
        private async Task<string> GenerateEnumerationInternalAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string name, ModelKind modelKind)
        {
            var template = this.Templates[EnumTemplateeName];

            var enumerations = xmiReaderResult.QueryRoot(null, name: modelKind.QueryRootNamePerModelKind()).QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IEnumeration>())
                .ToList();

            var enumeration = enumerations.Single(x => x.Name == name);

            var enumContext = new EnumModelKindContext()
            {
                Model = modelKind,
                Enumeration = enumeration
            };

            var generatedEnumeration = template(enumContext);

            generatedEnumeration = this.CodeCleanup(generatedEnumeration);

            var fileName = $"{enumeration.Name.CapitalizeFirstLetter()}.cs";

            await WriteAsync(generatedEnumeration, outputDirectory, fileName);

            return generatedEnumeration;
        }
    }
}
