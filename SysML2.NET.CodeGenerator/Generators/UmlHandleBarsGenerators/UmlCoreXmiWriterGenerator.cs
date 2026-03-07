// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreXmiWriterGenerator.cs" company="Starion Group S.A.">
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
    using SysML2.NET.CodeGenerator.HandleBarHelpers;

    using uml4net.Extensions;
    using uml4net.StructuredClassifiers;
    using uml4net.xmi.Readers;

    /// <summary>
    /// A UML Handlebars based XMI Writer code generator
    /// </summary>
    public class UmlCoreXmiWriterGenerator : UmlHandleBarsGenerator
    {
        /// <summary>
        /// Gets the name of the Xmi Writer template
        /// </summary>
        private const string XmiWriterTemplateName = "core-xmi-writer-template";

        /// <summary>
        /// Gets the name of the Xmi Writer Facade template
        /// </summary>
        private const string XmiWriterFacadeTemplateName = "core-xmi-writer-facade-template";

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
            await this.GenerateXmiWriters(xmiReaderResult, outputDirectory);
            await this.GenerateXmiWriterFacade(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates XMI Writer classes for all concrete <see cref="IClass" />
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
        private Task GenerateXmiWriters(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GenerateXmiWritersInternal(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates XMI Writer classes for all concrete <see cref="IClass" />
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        private async Task GenerateXmiWritersInternal(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates[XmiWriterTemplateName];

            var classes = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .ToList();

            foreach (var umlClass in classes)
            {
                var generatedXmiWriter = template(umlClass);

                generatedXmiWriter = this.CodeCleanup(generatedXmiWriter);

                var fileName = $"{umlClass.Name.CapitalizeFirstLetter()}Writer.cs";
                await WriteAsync(generatedXmiWriter, outputDirectory, fileName);
            }
        }

        /// <summary>
        /// Generates XMI Writer facade class for all concrete <see cref="IClass" />
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
        private Task GenerateXmiWriterFacade(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GenerateXmiWriterFacadeInternal(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates XMI Writer facade class for all concrete <see cref="IClass" />
        /// </summary>
        /// <param name="xmiReaderResult">the <see cref="XmiReaderResult" /> that contains the UML model to generate from</param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        private async Task GenerateXmiWriterFacadeInternal(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates[XmiWriterFacadeTemplateName];

            var classes = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .OrderBy(x => x.Name)
                .ToList();

            var generatedFacade = template(classes);
            generatedFacade = this.CodeCleanup(generatedFacade);
            await WriteAsync(generatedFacade, outputDirectory, "XmiDataWriterFacade.cs");
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
            this.RegisterTemplate(XmiWriterTemplateName);
            this.RegisterTemplate(XmiWriterFacadeTemplateName);
            this.RegisterPartialTemplate("core-xmi-writer-partial-for-attribute-template");
            this.RegisterPartialTemplate("core-xmi-writer-partial-for-element-template");
        }
    }
}
