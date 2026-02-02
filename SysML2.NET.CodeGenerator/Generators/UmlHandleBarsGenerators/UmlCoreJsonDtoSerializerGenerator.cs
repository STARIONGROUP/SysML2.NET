// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreJsonDtoSerializerGenerator.cs" company="Starion Group S.A.">
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
    using SysML2.NET.CodeGenerator.HandleBarHelpers;

    using uml4net.Extensions;
    using uml4net.HandleBars;
    using uml4net.StructuredClassifiers;
    using uml4net.xmi.Readers;

    using ClassHelper = uml4net.HandleBars.ClassHelper;
    using NamedElementHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.NamedElementHelper;
    using PropertyHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.PropertyHelper;

    /// <summary>
    /// A UML Handlebars based DTO Json Serializer code generator
    /// </summary>
    public class UmlCoreJsonDtoSerializerGenerator : UmlHandleBarsGenerator
    {
        /// <summary>
        /// Gets the name of the template used to generate DTO Json Serializaer
        /// </summary>
        private const string DtoSerializerTemplateName = "core-json-dto-serializer-uml-template";

        /// <summary>
        /// Gets the name of the template used to generate Json Serializer provider
        /// </summary>
        private const string DtoSerializerProviderTemplateName = "core-json-dto-serialization-provider-uml-template";

        /// <summary>
        /// Generates code specific to the concrete implementation
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
            await this.GenerateDtoJsonSerializer(xmiReaderResult, outputDirectory);
            await this.GenerateSerializationProvider(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Register the custom helpers
        /// </summary>
        protected override void RegisterHelpers()
        {
            this.Handlebars.RegisterStringHelper();
            ClassHelper.RegisterClassHelper(this.Handlebars);
            uml4net.HandleBars.PropertyHelper.RegisterPropertyHelper(this.Handlebars);

            NamedElementHelper.RegisterNamedElementHelper(this.Handlebars);
            PropertyHelper.RegisterPropertyHelper(this.Handlebars);
            this.Handlebars.RegisterSafeContextHelper();
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate(DtoSerializerTemplateName);
            this.RegisterTemplate(DtoSerializerProviderTemplateName);
            this.RegisterPartialTemplate("core-json-dto-serializer-uml-partial-template");
        }

        /// <summary>
        /// Generates the Serialization Provider class
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
        private Task GenerateSerializationProvider(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GenerateSerializationProviderInternal(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates the Serialization Provider class
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        private async Task GenerateSerializationProviderInternal(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates[DtoSerializerProviderTemplateName];

            var classes = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .OrderBy(x => x.Name)
                .ToList();

            var generatedSerializationProvider = template(classes);

            generatedSerializationProvider = this.CodeCleanup(generatedSerializationProvider);

            const string fileName = "SerializationProvider.cs";

            await Write(generatedSerializationProvider, outputDirectory, fileName);
        }

        /// <summary>
        /// Generates DTO Json Serializer files
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
        private Task GenerateDtoJsonSerializer(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GenerateDtoJsonSerializerInternal(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates DTO Json Serializer files
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        private async Task GenerateDtoJsonSerializerInternal(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates[DtoSerializerTemplateName];

            var classes = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .ToList();

            foreach (var @class in classes)
            {
                var generatedJsonSerializer = template(@class);

                generatedJsonSerializer = this.CodeCleanup(generatedJsonSerializer);

                var fileName = $"{@class.Name.CapitalizeFirstLetter()}Serializer.cs";

                await WriteAsync(generatedJsonSerializer, outputDirectory, fileName);
            }
        }

        /// <summary>
        /// Generates DTO Json Serializer file based for a specific class
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
        public Task<string> GenerateDtoSerializerClass(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string className)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            ArgumentException.ThrowIfNullOrWhiteSpace(className);

            return this.GenerateDtoSerializerClassInternal(xmiReaderResult, outputDirectory, className);
        }

        /// <summary>
        /// Generates DTO Json Serializer file based for a specific class
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <param name="className">The name of the class to generate</param>
        /// <returns>
        /// an awaitable <see cref="Task" /> with the generated code
        /// </returns>
        private async Task<string> GenerateDtoSerializerClassInternal(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string className)
        {
            var template = this.Templates[DtoSerializerTemplateName];

            var classToGenerate = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Single(x => x.Name == className);
            
            var generatedJsonSerializer = template(classToGenerate);

            generatedJsonSerializer = this.CodeCleanup(generatedJsonSerializer);

            var fileName = $"{classToGenerate.Name.CapitalizeFirstLetter()}Serializer.cs";

            await WriteAsync(generatedJsonSerializer, outputDirectory, fileName);
            return generatedJsonSerializer;
        }
    }
}
