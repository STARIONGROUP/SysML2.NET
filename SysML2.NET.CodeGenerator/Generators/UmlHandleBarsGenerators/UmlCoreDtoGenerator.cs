// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreDtoGenerator.cs" company="Starion Group S.A.">
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

    using SysML2.NET.CodeGenerator.Contexts;
    using SysML2.NET.CodeGenerator.Enumeration;
    using SysML2.NET.CodeGenerator.UmlHandleBarHelpers;
    using SysML2.NET.CodeGenerator.Extensions;

    using uml4net.Extensions;
    using uml4net.HandleBars;
    using uml4net.StructuredClassifiers;
    using uml4net.xmi.Readers;

    using ClassHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.ClassHelper;

    /// <summary>
    /// A UML Handlebars based DTO code generator
    /// </summary>
    public class UmlCoreDtoGenerator : UmlHandleBarsGenerator
    {
        /// <summary>
        /// Gets the name of the template used to generate DTO interface
        /// </summary>
        private const string DtoInterfaceTemplateName = "core-dto-interface-uml-template";
        
        /// <summary>
        /// Gets the name of the template used to generate DTO class
        /// </summary>
        private const string DtoClassTemplateName = "core-dto-class-uml-template";

        /// <summary>
        /// Generates the SysML2 classes as DTO interfaces and classes
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        public override async Task GenerateAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, ModelKind modelKind)
        {
            await this.GenerateDataTransferObjectInterfacesAsync(xmiReaderResult, outputDirectory, modelKind);
            await this.GenerateDataTransferObjectClassesAsync(xmiReaderResult, outputDirectory, modelKind);
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
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>
        /// an awaitable task
        /// </returns>
        public Task GenerateDataTransferObjectInterfacesAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, ModelKind modelKind)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GenerateDataTransferObjectInterfacesInternalAsync(xmiReaderResult, outputDirectory, modelKind);
        }

        /// <summary>
        /// Generates DTO Interface
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult" /> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <param name="name">
        /// The name of the DTO to generate
        /// </param>
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>
        /// an awaitable task
        /// </returns>
        public Task<string> GenerateDataTransferObjectInterfaceAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string name, ModelKind modelKind)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            return this.GenerateDataTransferObjectInterfaceInternalAsync(xmiReaderResult, outputDirectory, name, modelKind);
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
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>
        /// an awaitable task
        /// </returns>
        public Task GenerateDataTransferObjectClassesAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, ModelKind modelKind)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);

            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GenerateDataTransferObjectClassesInternalAsync(xmiReaderResult, outputDirectory, modelKind);
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
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>
        /// an awaitable task
        /// </returns>
        public Task<string> GenerateDataTransferObjectClassAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string name, ModelKind modelKind)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            return this.GenerateDataTransferObjectClassInternalAsync(xmiReaderResult, outputDirectory, name, modelKind);
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
            this.RegisterTemplate(DtoInterfaceTemplateName);
            this.RegisterTemplate(DtoClassTemplateName);
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
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>
        /// an awaitable task
        /// </returns>
        private async Task GenerateDataTransferObjectInterfacesInternalAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, ModelKind modelKind)
        {
            var template = this.Templates[DtoInterfaceTemplateName];

            var classes = xmiReaderResult.QueryRoot(null, name: modelKind.QueryRootNamePerModelKind()).QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .ToList();

            foreach (var @class in classes)
            {
                var generatedDto = template(new {ClassContext =@class, Model = modelKind.ToString()});

                generatedDto = this.CodeCleanup(generatedDto);

                var fileName = $"I{@class.Name.CapitalizeFirstLetter()}.cs";

                await WriteAsync(generatedDto, outputDirectory, fileName);
            }
        }

        /// <summary>
        /// Generates DTO Interface
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult" /> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <param name="name">
        /// The name of the DTO interface to generate
        /// </param>
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>
        /// an awaitable task
        /// </returns>
        private async Task<string> GenerateDataTransferObjectInterfaceInternalAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string name, ModelKind modelKind)
        {
            var template = this.Templates[DtoInterfaceTemplateName];

            var classes = xmiReaderResult.QueryRoot(null, modelKind.QueryRootNamePerModelKind()).QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .ToList();

            var @class = classes.Single(x => x.Name == name);
            
            var context = new ClassModelKindContext()
            {
                ClassContext = @class,
                Model = modelKind
            };
            
            var generatedDataTransferObjectInterface = template(@context);

            generatedDataTransferObjectInterface = this.CodeCleanup(generatedDataTransferObjectInterface);

            var fileName = $"I{context.ClassContext.Name.CapitalizeFirstLetter()}.cs";

            await WriteAsync(generatedDataTransferObjectInterface, outputDirectory, fileName);

            return generatedDataTransferObjectInterface;
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
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>
        /// an awaitable task
        /// </returns>
        private async Task GenerateDataTransferObjectClassesInternalAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, ModelKind modelKind)
        {
            var template = this.Templates[DtoClassTemplateName];

            var classes = xmiReaderResult.QueryRoot(null, name: modelKind.QueryRootNamePerModelKind()).QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .ToList();

            foreach (var @class in classes)
            { 
                var context = new ClassModelKindContext()
                {
                    ClassContext = @class,
                    Model = modelKind
                };
                
                var generatedDto = template(context);

                generatedDto = this.CodeCleanup(generatedDto);

                var fileName = $"{@class.Name.CapitalizeFirstLetter()}.cs";

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
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>
        /// an awaitable task
        /// </returns>
        private async Task<string> GenerateDataTransferObjectClassInternalAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string name, ModelKind modelKind)
        {
            var template = this.Templates[DtoClassTemplateName];

            var classes = xmiReaderResult.QueryRoot(null, name: modelKind.QueryRootNamePerModelKind()).QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .ToList();

            var @class = classes.Single(x => x.Name == name);

            var context = new ClassModelKindContext()
            {
                ClassContext =  @class,
                Model =modelKind
            };
            
            var generatedDataTransferObjectClass = template(context);

            generatedDataTransferObjectClass = this.CodeCleanup(generatedDataTransferObjectClass);

            var fileName = $"{@class.Name.CapitalizeFirstLetter()}.cs";

            await WriteAsync(generatedDataTransferObjectClass, outputDirectory, fileName);

            return generatedDataTransferObjectClass;
        }
    }
}
