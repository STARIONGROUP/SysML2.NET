// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreJsonDtoDeSerializerGenerator.cs" company="Starion Group S.A.">
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

    using uml4net.Extensions;
    using uml4net.HandleBars;
    using uml4net.SimpleClassifiers;
    using uml4net.StructuredClassifiers;
    using uml4net.xmi.Readers;

    using NamedElementHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.NamedElementHelper;
    using PropertyHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.PropertyHelper;

    /// <summary>
    /// A UML Handlebars based DTO Json DeSerializer code generator
    /// </summary>
    public class UmlCoreJsonDtoDeSerializerGenerator : UmlHandleBarsGenerator
    {
        /// <summary>
        /// Gets the name of the template used to generate DTO Json DeSerializaer
        /// </summary>
        private const string DtoDeSerializerTemplateName = "core-json-dto-deserializer-uml-template";
        
        /// <summary>
        /// Gets the name of the template used to generate Enum Json DeSerializaer
        /// </summary>
        private const string EnumDeSerializerTemplateName = "core-json-enum-deserializer-uml-template";

        /// <summary>
        /// Gets the name of the template used to generate Json DeSerializer provider
        /// </summary>
        private const string DtoDeSerializerProviderTemplateName = "core-json-dto-deserialization-provider-uml-template";

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
            await this.GenerateDtoJsonDeSerializer(xmiReaderResult, outputDirectory);
            await this.GenerateDeSerializationProvider(xmiReaderResult, outputDirectory);
            await this.GenerateEnumJsonDeSerializer(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates Enum Json DeSerializer files
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
        private Task GenerateEnumJsonDeSerializer(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GenerateEnumJsonDeSerializerInternal(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates Enum Json DeSerializer files
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        private async Task GenerateEnumJsonDeSerializerInternal(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates[EnumDeSerializerTemplateName];

            var enumerations = xmiReaderResult.QueryRoot(null, name: "SysML").QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IEnumeration>())
                .ToList();

            foreach (var enumeration in enumerations)
            {
                var generatedInterface = template(enumeration);

                generatedInterface = this.CodeCleanup(generatedInterface);

                var fileName = $"{enumeration.Name.CapitalizeFirstLetter()}DeSerializer.cs"; 

                await WriteAsync(generatedInterface, outputDirectory, fileName);
            }
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
            this.RegisterTemplate(DtoDeSerializerTemplateName);
            this.RegisterTemplate(DtoDeSerializerProviderTemplateName);
            this.RegisterTemplate(EnumDeSerializerTemplateName);
        }

        /// <summary>
        /// Generates the DeSerialization Provider class
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
        private Task GenerateDeSerializationProvider(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GenerateDeSerializationProviderInternal(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates the DeSerialization Provider class
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        private async Task GenerateDeSerializationProviderInternal(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates[DtoDeSerializerProviderTemplateName];

            var classes = xmiReaderResult.QueryRoot(null, name: "SysML").QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .OrderBy(x => x.Name)
                .ToList();

            var generatedSerializationProvider = template(classes);

            generatedSerializationProvider = this.CodeCleanup(generatedSerializationProvider);

            const string fileName = "DeSerializationProvider.cs";

            await Write(generatedSerializationProvider, outputDirectory, fileName);
        }

        /// <summary>
        /// Generates DTO Json DeSerializer files
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
        private Task GenerateDtoJsonDeSerializer(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GenerateDtoJsonDeSerializerInternal(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates DTO Json Serializer files
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        private async Task GenerateDtoJsonDeSerializerInternal(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates[DtoDeSerializerTemplateName];

            var classes = xmiReaderResult.QueryRoot(null, name: "SysML").QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .ToList();

            foreach (var @class in classes)
            {
                var generatedDeSerializer = template(@class);

                generatedDeSerializer = this.CodeCleanup(generatedDeSerializer);

                var fileName = $"{@class.Name.CapitalizeFirstLetter()}DeSerializer.cs"; 

                await WriteAsync(generatedDeSerializer, outputDirectory, fileName);
            }
        }

        /// <summary>
        /// Generates DTO Json DeSerializer file based for a specific class name
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
        public Task<string> GenerateDtoDeSerializerClass(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string className)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            ArgumentException.ThrowIfNullOrWhiteSpace(className);

            return this.GenerateDtoDeSerializerClassInternal(xmiReaderResult, outputDirectory, className);
        }

        /// <summary>
        /// Generates DTO Json DeSerializer file based for a specific class name
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <param name="className">The name of the class to generate</param>
        /// <returns>
        /// an awaitable <see cref="Task" /> with the generated code
        /// </returns>
        private async Task<string> GenerateDtoDeSerializerClassInternal(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string className)
        {
            var template = this.Templates[DtoDeSerializerTemplateName];

            var classToGenerate = xmiReaderResult.QueryRoot(null, name: "SysML").QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Single(x => x.Name == className);
            
            var generatedJsonDeSerializer = template(classToGenerate);

            generatedJsonDeSerializer = this.CodeCleanup(generatedJsonDeSerializer);

            var fileName = $"{classToGenerate.Name.CapitalizeFirstLetter()}DeSerializer.cs";

            await WriteAsync(generatedJsonDeSerializer, outputDirectory, fileName);
            return generatedJsonDeSerializer;        
        }
    }
}
