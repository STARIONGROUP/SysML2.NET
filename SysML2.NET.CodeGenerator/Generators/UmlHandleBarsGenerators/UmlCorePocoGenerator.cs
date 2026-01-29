// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCorePocoGenerator.cs" company="Starion Group S.A.">
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
    using uml4net.StructuredClassifiers;
    using uml4net.xmi.Readers;

    using ClassHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.ClassHelper;
    using NamedElementHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.NamedElementHelper;
    using PropertyHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.PropertyHelper;

    /// <summary>
    /// A UML Handlebars based POCO code generator
    /// </summary>
    public class UmlCorePocoGenerator : UmlHandleBarsGenerator
    {
        /// <summary>
        /// Gets the name of the template used to generate interfaces
        /// </summary>
        private const string InterfaceTemplateName = "core-poco-interface-uml-template";

        /// <summary>
        /// Gets the name of the template used to generate classes
        /// </summary>
        private const string ClassTemplateName = "core-poco-class-uml-template";

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
            await this.GeneratePocoClassesAsync(xmiReaderResult, outputDirectory, modelKind);
            await this.GeneratePocoInterfacesAsync(xmiReaderResult, outputDirectory, modelKind);
        }

        /// <summary>
        /// Generates POCO interfaces files
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// In case of null value for <paramref name="xmiReaderResult" /> or
        /// <paramref name="outputDirectory" />
        /// </exception>
        public Task GeneratePocoInterfacesAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, ModelKind modelKind)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GeneratePocoInterfacesInternalAsync(xmiReaderResult, outputDirectory, modelKind);
        }

        /// <summary>
        /// Generates POCO class files
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// In case of null value for <paramref name="xmiReaderResult" /> or
        /// <paramref name="outputDirectory" />
        /// </exception>
        public Task GeneratePocoClassesAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, ModelKind modelKind)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GeneratePocoClassesInternalAsync(xmiReaderResult, outputDirectory, modelKind);
        }

        /// <summary>
        /// Generate the POCO interface output code for a specific class
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <param name="className">The name of the class to generate</param>
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>An awaitable <see cref="Task{TResult}" /> with the generated code</returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="xmiReaderResult" /> or the
        /// <paramref name="outputDirectory" /> is null
        /// </exception>
        /// <exception cref="ArgumentException">If the <paramref name="className" /> is null or whitespace</exception>
        public Task<string> GeneratePocoInterfaceAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string className, ModelKind modelKind)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            ArgumentException.ThrowIfNullOrWhiteSpace(className);

            return this.GeneratePocoInterfaceInternalAsync(xmiReaderResult, outputDirectory, className, modelKind);
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
            ClassHelper.RegisterClassHelper(this.Handlebars);
            NamedElementHelper.RegisterNamedElementHelper(this.Handlebars);
            PropertyHelper.RegisterPropertyHelper(this.Handlebars);
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate(InterfaceTemplateName);
            this.RegisterTemplate(ClassTemplateName);
        }

        /// <summary>
        /// Generates POCO class files
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        private async Task GeneratePocoClassesInternalAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, ModelKind modelKind)
        {
            var template = this.Templates[ClassTemplateName];

            var classes = xmiReaderResult.QueryRoot(null, name: modelKind.QueryRootNamePerModelKind()).QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .ToList();

            foreach (var @class in classes)
            {
                var classContext = new ClassModelKindContext()
                {
                    ClassContext = @class,
                    Model = modelKind
                };
                
                var generatedClass = template(classContext);
                
                generatedClass = this.CodeCleanup(generatedClass);

                var fileName = $"{@class.Name.CapitalizeFirstLetter()}.cs";

                await WriteAsync(generatedClass, outputDirectory, fileName);
            }
        }

        /// <summary>
        /// Generates POCO interfaces files
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        private async Task GeneratePocoInterfacesInternalAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, ModelKind modelKind)
        {
            var template = this.Templates[InterfaceTemplateName];

            var classes = xmiReaderResult.QueryRoot(null, name: modelKind.QueryRootNamePerModelKind()).QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .ToList();

            foreach (var @class in classes)
            {
                var classContext = new ClassModelKindContext()
                {
                    ClassContext = @class,
                    Model = modelKind
                };
                
                var generatedInterface = template(classContext);

                generatedInterface = this.CodeCleanup(generatedInterface);

                var fileName = $"I{@class.Name.CapitalizeFirstLetter()}.cs";

                await WriteAsync(generatedInterface, outputDirectory, fileName);
            }
        }

        /// <summary>
        /// Generate the POCO interface output code for a specific class
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <param name="className">The name of the class to generate</param>
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>An awaitable <see cref="Task{TResult}" /> with the generated code</returns>
        private async Task<string> GeneratePocoInterfaceInternalAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string className, ModelKind modelKind)
        {
            var template = this.Templates[InterfaceTemplateName];

            var umlClass = xmiReaderResult.QueryRoot(null, name: modelKind.QueryRootNamePerModelKind()).QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Single(x => x.Name == className);

            var classContext = new ClassModelKindContext()
            {
                ClassContext = umlClass,
                Model = modelKind
            };            
            
            var generatedInterface = template(classContext);

            generatedInterface = this.CodeCleanup(generatedInterface);

            var fileName = $"I{umlClass.Name.CapitalizeFirstLetter()}.cs";

            await WriteAsync(generatedInterface, outputDirectory, fileName);
            return generatedInterface;
        }

        /// <summary>
        /// Generate the POCO interface output code for a specific class
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <param name="className">The name of the class to generate</param>
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>An awaitable <see cref="Task{TResult}" /> with the generated code</returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="xmiReaderResult" /> or the
        /// <paramref name="outputDirectory" /> is null
        /// </exception>
        /// <exception cref="ArgumentException">If the <paramref name="className" /> is null or whitespace</exception>
        public Task<string> GeneratePocoClassAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string className, ModelKind modelKind)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            ArgumentException.ThrowIfNullOrWhiteSpace(className);

            return this.GeneratePocoClassInternalAsync(xmiReaderResult, outputDirectory, className, modelKind);
        }

        /// <summary>
        /// Generate the POCO interface output code for a specific class
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <param name="className">The name of the class to generate</param>
        /// <param name="modelKind">The specific <see cref="ModelKind"/> that the <paramref name="xmiReaderResult"/> represents</param>
        /// <returns>An awaitable <see cref="Task{TResult}" /> with the generated code</returns>
        private async Task<string> GeneratePocoClassInternalAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string className, ModelKind modelKind)
        {
            var template = this.Templates[ClassTemplateName];

            var umlClass = xmiReaderResult.QueryRoot(null, name: modelKind.QueryRootNamePerModelKind()).QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Single(x => x.Name == className);

            var classContext = new ClassModelKindContext()
            {
                ClassContext = umlClass,
                Model = modelKind
            };    
            
            var generatedInterface = template(classContext);

            generatedInterface = this.CodeCleanup(generatedInterface);

            var fileName = $"{umlClass.Name.CapitalizeFirstLetter()}.cs";

            await WriteAsync(generatedInterface, outputDirectory, fileName);
            return generatedInterface;
        }
    }
}
