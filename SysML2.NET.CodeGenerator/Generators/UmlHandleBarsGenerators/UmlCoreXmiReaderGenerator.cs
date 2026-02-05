// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreXmiReaderGenerator.cs" company="Starion Group S.A.">
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
    using uml4net.StructuredClassifiers;
    using uml4net.xmi.Readers;

    /// <summary>
    /// A UML Handlebars based XMI Reader code generator
    /// </summary>
    public class UmlCoreXmiReaderGenerator : UmlHandleBarsGenerator
    {
        /// <summary>
        /// Gets the name of the Xmi Reader template
        /// </summary>
        private const string XmiReaderTemplateName = "core-xmi-reader-template";
        
        /// <summary>
        /// Gets the name of the Xmi Reader Facade template
        /// </summary>
        private const string XmiReaderFacadeTemplateName = "core-xmi-reader-facade-template";

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
            await this.GenerateXmiReaders(xmiReaderResult, outputDirectory);
            await this.GenerateXmiReaderFacade(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates XMI Reader facade class for all concrete <see cref="IClass" />
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// In case of null value for <paramref name="xmiReaderResult" /> or
        /// <paramref name="outputDirectory" />
        /// </exception>
        private Task GenerateXmiReaderFacade(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            
            return this.GenerateXmiReaderFacadeInternal(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates XMI Reader facade class for all concrete <see cref="IClass" />
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        private async Task GenerateXmiReaderFacadeInternal(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates[XmiReaderFacadeTemplateName];

            var classes = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .OrderBy(x => x.Name)
                .ToList();

            var generatedFacade = template(classes);
            generatedFacade = this.CodeCleanup(generatedFacade);
            await WriteAsync(generatedFacade, outputDirectory, "XmiDataReaderFacade.cs");
        }

        /// <summary>
        /// Generates XMI Reader classes for all concrete <see cref="IClass" />
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// In case of null value for <paramref name="xmiReaderResult" /> or
        /// <paramref name="outputDirectory" />
        /// </exception>
        private Task GenerateXmiReaders(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            
            return this.GenerateXmiReadersInternal(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates XMI Reader classes for all concrete <see cref="IClass" />
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        private async Task GenerateXmiReadersInternal(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates[XmiReaderTemplateName];

            var classes = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .ToList();

            foreach (var umlClass in classes)
            {
                var generatedXmiReader = template(umlClass);

                generatedXmiReader = this.CodeCleanup(generatedXmiReader);

                var fileName = $"{umlClass.Name.CapitalizeFirstLetter()}Reader.cs";
                await WriteAsync(generatedXmiReader, outputDirectory, fileName);
            }
        }

        /// <summary>
        /// Register the custom helpers
        /// </summary>
        protected override void RegisterHelpers()
        {
            this.Handlebars.RegisterNamedElementHelper();
            this.Handlebars.RegisterSafeContextHelper();
            this.Handlebars.RegisterClassHelper();
            SysML2.NET.CodeGenerator.HandleBarHelpers.PropertyHelper.RegisterPropertyHelper(this.Handlebars);
            uml4net.HandleBars.StringHelper.RegisterStringHelper(this.Handlebars);
            uml4net.HandleBars.ClassHelper.RegisterClassHelper(this.Handlebars);
            uml4net.HandleBars.PropertyHelper.RegisterPropertyHelper(this.Handlebars);
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate(XmiReaderTemplateName);
            this.RegisterTemplate(XmiReaderFacadeTemplateName);
            this.RegisterPartialTemplate("core-xmi-reader-partial-for-attribute-template");
            this.RegisterPartialTemplate("core-xmi-reader-partial-for-element-template");
        }
    }
}
