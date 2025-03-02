﻿// -------------------------------------------------------------------------------------------------
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
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using uml4net.Extensions;
    using uml4net.Packages;
    using uml4net.SimpleClassifiers;
    using uml4net.StructuredClassifiers;
    using uml4net.xmi.Readers;

    /// <summary>
    /// A UML Handlebars based enum code generator
    /// </summary>
    public class UmlCoreEnumGenerator : UmlHandleBarsGenerator
    {
        /// <summary>
        /// Generates the Core SysML2 model Enumerations
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult"/> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        public override async Task GenerateAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            await this.GenerateEnumerationsAsync(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates Enumerations
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult"/> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// an awaitable task
        /// </returns>
        public Task GenerateEnumerationsAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);

            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GenerateEnumerationsInternalAsync(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates Enumeration
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult"/> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// an awaitable task
        /// </returns>
        private async Task GenerateEnumerationsInternalAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates["core-enumeration-uml-template"];

            var enumerations = xmiReaderResult.Root.QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IEnumeration>())
                .ToList();

            foreach (var enumeration in enumerations)
            {
                var generatedEnumeration = template(enumeration);

                generatedEnumeration = CodeCleanup(generatedEnumeration);

                var fileName = $"{enumeration.Name.CapitalizeFirstLetter()}.cs";

                await WriteAsync(generatedEnumeration, outputDirectory, fileName);
            }
        }

        /// <summary>
        /// Generates POCO interfaces
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult"/> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <param name="name">
        /// The name of the Enumeration to generate
        /// </param>
        /// <returns>
        /// an awaitable task
        /// </returns>
        public Task<string> GenerateEnumerationAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string name)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);

            ArgumentNullException.ThrowIfNull(outputDirectory);

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name));
            }

            return this.GenerateEnumerationInternalAsync(xmiReaderResult, outputDirectory, name);
        }

        /// <summary>
        /// Generates POCO interfaces
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult"/> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <param name="name">
        /// The name of the Enumeration to generate
        /// </param>
        /// <returns>
        /// an awaitable task
        /// </returns>
        private async Task<string> GenerateEnumerationInternalAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string name)
        {
            var template = this.Templates["core-enumeration-uml-template"];

            var enumerations = xmiReaderResult.Root.QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IEnumeration>())
                .ToList();

            var enumNames = enumerations.Select(x => x.Name);

            var enumeration = enumerations.Single(x => x.Name == name);

            var generatedEnumeration = template(enumeration);

            generatedEnumeration = CodeCleanup(generatedEnumeration);

            var fileName = $"{enumeration.Name.CapitalizeFirstLetter()}.cs";

            await WriteAsync(generatedEnumeration, outputDirectory, fileName);

            return generatedEnumeration;
        }

        /// <summary>
        /// Register the custom helpers
        /// </summary>
        protected override void RegisterHelpers()
        {
            uml4net.HandleBars.StringHelper.RegisterStringHelper(this.Handlebars);
            uml4net.HandleBars.IEnumerableHelper.RegisterEnumerableHelper(this.Handlebars);
            uml4net.HandleBars.ClassHelper.RegisterClassHelper(this.Handlebars);
            uml4net.HandleBars.PropertyHelper.RegisterPropertyHelper(this.Handlebars);
            uml4net.HandleBars.GeneralizationHelper.RegisterGeneralizationHelper(this.Handlebars);
            uml4net.HandleBars.DocumentationHelper.RegisteredDocumentationHelper(this.Handlebars);
            uml4net.HandleBars.EnumHelper.RegisterEnumHelper(this.Handlebars);
            uml4net.HandleBars.DecoratorHelper.RegisterDecoratorHelper(this.Handlebars);

            UmlHandleBarHelpers.EnumerationLiteralHelper.RegisterTypeNameHelper(this.Handlebars);
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate("core-enumeration-uml-template");
        }
    }
}
