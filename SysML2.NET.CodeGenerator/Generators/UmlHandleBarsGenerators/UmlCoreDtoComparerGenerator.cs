// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreDtoComparerGenerator.cs" company="Starion Group S.A.">
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
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.UmlHandleBarHelpers;

    using uml4net.Extensions;
    using uml4net.HandleBars;
    using uml4net.StructuredClassifiers;
    using uml4net.xmi.Readers;

    /// <summary>
    /// A UML Handlebars based DTO comparer code generator
    /// </summary>
    public class UmlCoreDtoComparerGenerator : UmlHandleBarsGenerator
    {
        /// <summary>
        /// Generates the SysML2 DTO comparers
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult" /> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        public override async Task GenerateAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            await this.GenerateDataTransferObjectComparersAsync(xmiReaderResult, outputDirectory);
            await this.GenerateDataTransferObjectComparerProviderAsync(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates DTO Interfaces
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
        public Task GenerateDataTransferObjectComparersAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GenerateDataTransferObjectComparersInternalAsync(xmiReaderResult, outputDirectory);
        }

        public Task GenerateDataTransferObjectComparerProviderAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GenerateDataTransferObjectComparerProviderInternalAsync(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates DTO class
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult" /> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <param name="name">
        /// The name of the DTO class to generate
        /// </param>
        /// <returns>
        /// an awaitable task
        /// </returns>
        public Task<string> GenerateDataTransferObjectComparerAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string name)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            return this.GenerateDataTransferObjectComparerInternalAsync(xmiReaderResult, outputDirectory, name);
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
            this.Handlebars.RegisterNamedElementHelper();

            EnumerationLiteralHelper.RegisterTypeNameHelper(this.Handlebars);
            HandleBarHelpers.ClassHelper.RegisterClassHelper(this.Handlebars);
            HandleBarHelpers.NamedElementHelper.RegisterNamedElementHelper(this.Handlebars);
            HandleBarHelpers.PropertyHelper.RegisterPropertyHelper(this.Handlebars);
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate("core-dto-comparer-class-uml-template");
            this.RegisterTemplate("core-dto-comparerprovider-uml-template");
        }

        /// <summary>
        /// Generates DTO classes
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
        private async Task GenerateDataTransferObjectComparersInternalAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates["core-dto-comparer-class-uml-template"];

            var classes = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .ToList();

            foreach (var @class in classes)
            {
                var generatedDto = template(@class);

                generatedDto = this.CodeCleanup(generatedDto);

                var fileName = $"{@class.Name.CapitalizeFirstLetter()}Comparer.cs";

                await WriteAsync(generatedDto, outputDirectory, fileName);
            }
        }

        /// <summary>
        /// Generates DTO classes
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult" /> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <param name="name">
        /// The name of the DTO class to generate
        /// </param>
        /// <returns>
        /// an awaitable task
        /// </returns>
        private async Task<string> GenerateDataTransferObjectComparerInternalAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string name)
        {
            var template = this.Templates["core-dto-comparer-class-uml-template"];

            var classes = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>());

            var @class = classes.Single(x => x.Name == name);

            var generatedDataTransferObjectClass = template(@class);

            generatedDataTransferObjectClass = this.CodeCleanup(generatedDataTransferObjectClass);

            var fileName = $"{@class.Name.CapitalizeFirstLetter()}Comparer.cs";

            await WriteAsync(generatedDataTransferObjectClass, outputDirectory, fileName);

            return generatedDataTransferObjectClass;
        }

        private async Task<string> GenerateDataTransferObjectComparerProviderInternalAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates["core-dto-comparerprovider-uml-template"];

            var classes = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .OrderBy(x => x.Name);

            var generatedDataTransferObjectComparerProvider = template(classes);

            generatedDataTransferObjectComparerProvider = template(classes);

            generatedDataTransferObjectComparerProvider = this.CodeCleanup(generatedDataTransferObjectComparerProvider);

            await WriteAsync(generatedDataTransferObjectComparerProvider, outputDirectory, "ComparerProvider.cs");

            return generatedDataTransferObjectComparerProvider;
        }
    }
}
