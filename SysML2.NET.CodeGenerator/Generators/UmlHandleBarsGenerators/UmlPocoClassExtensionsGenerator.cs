// -------------------------------------------------------------------------------------------------
// <copyright file="UmlPocoClassExtensionsGenerator.cs" company="Starion Group S.A.">
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

    using SysML2.NET.CodeGenerator.UmlHandleBarHelpers;

    using uml4net.Extensions;
    using uml4net.HandleBars;
    using uml4net.StructuredClassifiers;
    using uml4net.xmi.Readers;

    using ClassHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.ClassHelper;
    using NamedElementHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.NamedElementHelper;
    using PropertyHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.PropertyHelper;

    public class UmlPocoClassExtensionsGenerator : UmlHandleBarsGenerator
    {
        /// <summary>
        /// Gets the name of the template used to generate classes
        /// </summary>
        private const string ExtendTemplateName = "core-poco-extend-uml-template";

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
            await this.GenerateExtendClassesAsync(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates POCO classe files
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
        public Task GenerateExtendClassesAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GenerateExtendClassesInternalAsync(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates POCO classe files
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        private async Task GenerateExtendClassesInternalAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates[ExtendTemplateName];

            var classes = xmiReaderResult.Root.QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => x.OwnedAttribute.Select(y => y.IsDerived || y.IsDerivedUnion).Any())
                .ToList();

            foreach (var @class in classes)
            {
                var generatedExtend = template(@class);

                generatedExtend = this.CodeCleanup(generatedExtend);

                var fileName = $"{@class.Name.CapitalizeFirstLetter()}Extensions.cs";

                await WriteAsync(generatedExtend, outputDirectory, fileName);
            }
        }

        /// <summary>
        /// Generate the POCO interface output code for a specific class
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <param name="className">The name of the class to generate</param>
        /// <returns>An awaitable <see cref="Task{TResult}" /> with the generated code</returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="xmiReaderResult" /> or the
        /// <paramref name="outputDirectory" /> is null
        /// </exception>
        /// <exception cref="ArgumentException">If the <paramref name="className" /> is null or whitespace</exception>
        public Task<string> GenerateExtendClassAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string className)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            ArgumentException.ThrowIfNullOrWhiteSpace(className);

            return this.GenerateExtendClassInternalAsync(xmiReaderResult, outputDirectory, className);
        }

        /// <summary>
        /// Generate the POCO interface output code for a specific class
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo" /></param>
        /// <param name="className">The name of the class to generate</param>
        /// <returns>An awaitable <see cref="Task{TResult}" /> with the generated code</returns>
        private async Task<string> GenerateExtendClassInternalAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string className)
        {
            var template = this.Templates[ExtendTemplateName];

            var umlClass = xmiReaderResult.Root.QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Single(x => x.Name == className);

            var generatedExtend = template(umlClass);

            generatedExtend = this.CodeCleanup(generatedExtend);

            var fileName = $"{umlClass.Name.CapitalizeFirstLetter()}Extensions.cs";

            await WriteAsync(generatedExtend, outputDirectory, fileName);
            return generatedExtend;
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
            this.RegisterTemplate(ExtendTemplateName);
        }
    }
}
