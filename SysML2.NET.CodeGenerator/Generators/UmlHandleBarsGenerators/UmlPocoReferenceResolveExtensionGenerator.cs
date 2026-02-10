// -------------------------------------------------------------------------------------------------
// <copyright file="UmlPocoReferenceResolveExtensionGenerator.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2026 Starion Group S.A.
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

    using uml4net.Extensions;
    using uml4net.HandleBars;
    using uml4net.StructuredClassifiers;
    using uml4net.xmi.Readers;

    /// <summary>
    /// The <see cref="UmlPocoReferenceResolveExtensionGenerator"/> provides POCO extension method code generation
    /// for reference resolve
    /// </summary>
    public class UmlPocoReferenceResolveExtensionGenerator: UmlHandleBarsGenerator
    {
        /// <summary>
        /// Gets the name of the POCO extension template name
        /// </summary>
        private const string PocoExtensionTemplateName = "core-poco-reference-resolve-extension-template";
        
        /// <summary>
        /// Gets the name of the POCO extension facade template name
        /// </summary>
        private const string PocoExtensionFacadeTemplateName = "core-poco-reference-resolve-extension-facade-template";
        
        /// <summary>
        /// Register the custom helpers
        /// </summary>
        protected override void RegisterHelpers()
        {
            this.Handlebars.RegisterStringHelper();
            this.Handlebars.RegisterPropertyHelper();
            this.Handlebars.RegisterClassHelper();
            SysML2.NET.CodeGenerator.HandleBarHelpers.NamedElementHelper.RegisterNamedElementHelper(this.Handlebars);
            SysML2.NET.CodeGenerator.HandleBarHelpers.PropertyHelper.RegisterPropertyHelper(this.Handlebars);
            SysML2.NET.CodeGenerator.HandleBarHelpers.ClassHelper.RegisterClassHelper(this.Handlebars);
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate(PocoExtensionTemplateName);
            this.RegisterTemplate(PocoExtensionFacadeTemplateName);
        }

        /// <summary>
        /// Generates code specific to the concrete implementation
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
            await this.GeneratePocoExtensionClasses(xmiReaderResult, outputDirectory);
            await this.GeneratePocoExtensionFacadeClass(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generate poco extension facade class for each concrete classes of the UML model
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult"/> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// is null
        /// </param>
        /// <exception cref="ArgumentNullException">If the provided <paramref name="xmiReaderResult"/> or <paramref name="outputDirectory"/></exception>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        private Task GeneratePocoExtensionFacadeClass(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            
            return this.GeneratePocoExtensionFacadeClassInternal(outputDirectory, xmiReaderResult);
        }

        /// <summary>
        /// Generate poco extension facade class for each concrete classes of the UML model
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult"/> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// is null
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        private async Task GeneratePocoExtensionFacadeClassInternal(DirectoryInfo outputDirectory, XmiReaderResult xmiReaderResult)
        {
            var template = this.Templates[PocoExtensionFacadeTemplateName];

            var umlClasses = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .OrderBy(x => x.Name)
                .ToList();
            
            var generatedPocoExtensionFacade = template(umlClasses);

            generatedPocoExtensionFacade = this.CodeCleanup(generatedPocoExtensionFacade);

            await WriteAsync(generatedPocoExtensionFacade, outputDirectory, "PocoReferenceResolveExtensionsFacade.cs");
        }

        /// <summary>
        /// Generate poco extension classes for each concrete classes of the UML model
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult"/> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// is null
        /// </param>
        /// <exception cref="ArgumentNullException">If the provided <paramref name="xmiReaderResult"/> or <paramref name="outputDirectory"/></exception>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        private Task GeneratePocoExtensionClasses(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            
            return this.GeneratePocoExtensionClassesInternal(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generate poco extension classes for each concrete classes of the UML model
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
        private async Task GeneratePocoExtensionClassesInternal(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates[PocoExtensionTemplateName];

            var umlClasses = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract);

            foreach (var umlClass  in umlClasses)
            {
                var generatedPocoExtension = template(umlClass);

                generatedPocoExtension = this.CodeCleanup(generatedPocoExtension);

                var fileName = $"{umlClass.Name.CapitalizeFirstLetter()}Extensions.cs";

                await WriteAsync(generatedPocoExtension, outputDirectory, fileName);
            }
        }

        /// <summary>
        /// Generate poco extension class for a specific concrete class
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult"/> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// is null
        /// </param>
        /// <param name="className">The name of the class to generate</param>
        /// <exception cref="ArgumentNullException">If the provided <paramref name="xmiReaderResult"/> or <paramref name="outputDirectory"/></exception>
        /// <exception cref="ArgumentException">If the provided <paramref name="className"/> is null or whitespace</exception>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        public Task<string> GenerateExtensionClass(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string className)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            ArgumentException.ThrowIfNullOrWhiteSpace(className);
            
            return this.GenerateExtensionClassInternal(xmiReaderResult, outputDirectory, className);        
        }

        /// <summary>
        /// Generate poco extension class for a specific concrete class
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult"/> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// is null
        /// </param>
        /// <param name="className">The name of the class to generate</param>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        private async Task<string> GenerateExtensionClassInternal(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string className)
        {
            var template = this.Templates[PocoExtensionTemplateName];

            var umlClass = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Single(x => x.Name == className);

            var generatedPocoExtension = template(umlClass);

            generatedPocoExtension = this.CodeCleanup(generatedPocoExtension);

            var fileName = $"{umlClass.Name.CapitalizeFirstLetter()}Extensions.cs";

            await WriteAsync(generatedPocoExtension, outputDirectory, fileName);
            return generatedPocoExtension;
        }
    }
}
