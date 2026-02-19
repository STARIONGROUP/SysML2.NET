// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreTextualNotationBuilderGenerator.cs" company="Starion Group S.A.">
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
    using SysML2.NET.CodeGenerator.Grammar.Model;

    using uml4net.Extensions;
    using uml4net.HandleBars;
    using uml4net.StructuredClassifiers;
    using uml4net.xmi.Readers;

    using NamedElementHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.NamedElementHelper;

    /// <summary>
    /// An <see cref="UmlHandleBarsGenerator"/> to generate Textual Notation Builder
    /// </summary>
    public class UmlCoreTextualNotationBuilderGenerator: UmlHandleBarsGenerator
    {
        /// <summary>
        /// Gets the name of template for builder classes
        /// </summary>
        private const string BuilderTemplateName = "core-textual-notation-builder-template";
        
        /// <summary>
        /// Gets the name of template for builder facade class
        /// </summary>
        private const string BuilderFacadeTemplateName = "core-textual-notation-builder-facade-template";
        
        /// <summary>
        /// Register the custom helpers
        /// </summary>
        protected override void RegisterHelpers()
        {
            NamedElementHelper.RegisterNamedElementHelper(this.Handlebars);
            this.Handlebars.RegisterStringHelper();
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate(BuilderTemplateName);
            this.RegisterTemplate(BuilderFacadeTemplateName);
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
        /// <exception cref="NotSupportedException">This method cannot be used since it requires to have <see cref="TextualNotationSpecification"/>. Uses <see cref="GenerateAsync(uml4net.xmi.Readers.XmiReaderResult,TextualNotationSpecification, System.IO.DirectoryInfo)"/></exception>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        public override Task GenerateAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            throw new NotSupportedException("The generator needs TextualNotationSpecification access");
        }

        /// <summary>
        /// Generates code specific to the concrete implementation
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult"/> that contains the UML model to generate from
        /// </param>
        /// <param name="textualNotationSpecification">The <see cref="TextualNotationSpecification"/> that contains specific grammar rules to produce textual notation</param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        public async Task GenerateAsync(XmiReaderResult xmiReaderResult, TextualNotationSpecification textualNotationSpecification, DirectoryInfo outputDirectory)
        {
            await this.GenerateBuilderClasses(xmiReaderResult, textualNotationSpecification, outputDirectory);
            await this.GenerateBuilderFacade(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates Textual Notation builder classes for each concrete <see cref="IClass"/>
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult"/> that contains the UML model to generate from
        /// </param>
        /// <param name="textualNotationSpecification">The <see cref="TextualNotationSpecification"/> that contains specific grammar rules to produce textual notation</param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <exception cref="ArgumentNullException">If one of the given parameters is null</exception>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        private Task GenerateBuilderClasses(XmiReaderResult xmiReaderResult, TextualNotationSpecification textualNotationSpecification, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(textualNotationSpecification);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            
            return this.GenerateBuilderClassesInternal(xmiReaderResult, textualNotationSpecification, outputDirectory);
        }

        /// <summary>
        /// Generates Textual Notation builder classes for each concrete <see cref="IClass"/>
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult"/> that contains the UML model to generate from
        /// </param>
        /// <param name="textualNotationSpecification">The <see cref="TextualNotationSpecification"/> that contains specific grammar rules to produce textual notation</param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        private async Task GenerateBuilderClassesInternal(XmiReaderResult xmiReaderResult, TextualNotationSpecification textualNotationSpecification, DirectoryInfo outputDirectory)
        {
            var template = this.Templates[BuilderTemplateName];

            var classes = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .ToList();

            foreach (var umlClass in classes)
            {
                var generatedBuilder = template(new {ClassContext = umlClass, Rules = textualNotationSpecification.Rules});
                generatedBuilder = this.CodeCleanup(generatedBuilder);

                var fileName = $"{umlClass.Name.CapitalizeFirstLetter()}TextualNotationBuilder.cs";

                await WriteAsync(generatedBuilder, outputDirectory, fileName);
            }
        }

        /// <summary>
        /// Generates the Textual Notation builder facade
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult"/> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo"/></param>
        /// <exception cref="ArgumentNullException">If one of the given parameters is null</exception>
        /// <returns>an awaitable <see cref="Task"/></returns>
        private Task GenerateBuilderFacade(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            return this.GenerateBuilderFacadeInternal(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates the Textual Notation builder facade
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult"/> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo"/></param>
        /// <returns>an awaitable <see cref="Task"/></returns>
        private async Task GenerateBuilderFacadeInternal(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates[BuilderFacadeTemplateName];

            var classes = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .ToList();
            
            var generatedFacade = template(classes);
            generatedFacade = this.CodeCleanup(generatedFacade);
            
            await WriteAsync(generatedFacade, outputDirectory, "TextualNotationBuilderFacade.cs");
        }
    }
}
