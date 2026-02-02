// -------------------------------------------------------------------------------------------------
// <copyright file="UmlMessagePackGenerator.cs" company="Starion Group S.A.">
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

    using NamedElementHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.NamedElementHelper;
    using PropertyHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.PropertyHelper;

    /// <summary>
    /// A UML Handlebars based DTO MessagePack code generator
    /// </summary>
    public class UmlMessagePackGenerator : UmlHandleBarsGenerator
    {
        /// <summary>
        /// Gets the name of the template used to generate the MessagePack Payload
        /// </summary>
        private const string MessagePackPayloadTemplateName = "core-messagepack-payload-uml-template";

        /// <summary>
        /// Gets the name of the template used to generate the MessagePack PayloadFactory
        /// </summary>
        private const string MessagePackPayloadFactoryTemplateName = "core-messagepack-payloadfactory-uml-template";

        /// <summary>
        /// Gets the name of the template used to generate the MessagePack PayloadMessagePackFormatter
        /// </summary>
        private const string PayloadMessagePackFormatterTemplateName = "core-messagepack-payloadmessagepackformatter-uml-template";

        /// <summary>
        /// Gets the name of the template used to generate the MessagePack DTO specific PayloadMessagePackFormatter
        /// </summary>
        private const string DtoMessagePackFormatterTemplateName = "core-dto-messagepackformatter-uml-template";

        /// <summary>
        /// Gets the name of the template used to generate the MessagePack DataResolverGetFormatterHelper
        /// </summary>
        private const string MessagePackDataResolverGetFormatterHelper = "core-messagepack-DataResolverGetFormatterHelper";

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
        public override Task GenerateAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Generates the MessagePack PayloadMessagePackFormatter
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult"/> holding the UML model
        /// </param>
        /// <param name="outputDirectory">
        /// The <see cref="DirectoryInfo"/> to which the generated class is written
        /// </param>
        /// <returns>
        /// The generated code as string
        /// </returns>
        public async Task<string> GenerateMessagePackPayloadFactory(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates[MessagePackPayloadFactoryTemplateName];

            var classes = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .OrderBy(x => x.Name)
                .ToList();

            var generatedPayloadMessagePackFormatter = template(classes);

            generatedPayloadMessagePackFormatter  = this.CodeCleanup(generatedPayloadMessagePackFormatter );

            const string fileName = "PayloadMessagePackFormatter.cs";

            await Write(generatedPayloadMessagePackFormatter , outputDirectory, fileName);

            return generatedPayloadMessagePackFormatter ;
        }

        /// <summary>
        /// Generates the MessagePack Payload
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult"/> holding the UML model
        /// </param>
        /// <param name="outputDirectory">
        /// The <see cref="DirectoryInfo"/> to which the generated class is written
        /// </param>
        /// <returns>
        /// The generated code as string
        /// </returns>
        public async Task<string> GenerateMessagePackPayload(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates[MessagePackPayloadTemplateName];

            var classes = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .OrderBy(x => x.Name)
                .ToList();

            var generateMessagePackPayload = template(classes);

            generateMessagePackPayload = this.CodeCleanup(generateMessagePackPayload);

            const string fileName = "Payload.cs";

            await Write(generateMessagePackPayload, outputDirectory, fileName);

            return generateMessagePackPayload;
        }

        /// <summary>
        /// Generates the MessagePack DataResolverGetFormatterHelper
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult"/> holding the UML model
        /// </param>
        /// <param name="outputDirectory">
        /// The <see cref="DirectoryInfo"/> to which the generated class is written
        /// </param>
        /// <returns>
        /// The generated code as string
        /// </returns>
        public async Task<string> GenerateDataResolverGetFormatterHelper(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates[MessagePackDataResolverGetFormatterHelper];

            var classes = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .OrderBy(x => x.Name)
                .ToList();

            var generateMessagePackDataResolverGetFormatterHelper = template(classes);

            generateMessagePackDataResolverGetFormatterHelper = this.CodeCleanup(generateMessagePackDataResolverGetFormatterHelper);

            const string fileName = "DataResolverGetFormatterHelper.cs";

            await Write(generateMessagePackDataResolverGetFormatterHelper, outputDirectory, fileName);

            return generateMessagePackDataResolverGetFormatterHelper;
        }

        /// <summary>
        /// Generates the MessagePack PayloadMessagePackFormatter
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult"/> holding the UML model
        /// </param>
        /// <param name="outputDirectory">
        /// The <see cref="DirectoryInfo"/> to which the generated class is written
        /// </param>
        /// <returns>
        /// The generated code as string
        /// </returns>
        public async Task<string> GenerateMessagePackPayloadMessagePackFormatter(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates[PayloadMessagePackFormatterTemplateName];

            var classes = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .OrderBy(x => x.Name)
                .ToList();

            var generateMessagePackPayloadFormatter = template(classes);

            generateMessagePackPayloadFormatter = this.CodeCleanup(generateMessagePackPayloadFormatter);

            const string fileName = "PayloadMessagePackFormatter.cs";

            await Write(generateMessagePackPayloadFormatter, outputDirectory, fileName);

            return generateMessagePackPayloadFormatter;
        }

        /// <summary>
        /// Generates the MessagePack MessagePackFormatter
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult"/> holding the UML model
        /// </param>
        /// <param name="outputDirectory">
        /// The <see cref="DirectoryInfo"/> to which the generated class is written
        /// </param>
        /// <returns>
        /// The generated code as string
        /// </returns>
        public async Task GenerateMessagePackFormatters(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates[DtoMessagePackFormatterTemplateName];

            var classes = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .OrderBy(x => x.Name)
                .ToList();

            foreach (var @class in classes)
            {
                var generatedMessagePackFormatter = template(@class);

                generatedMessagePackFormatter = this.CodeCleanup(generatedMessagePackFormatter);

                var fileName = $"{@class.Name.CapitalizeFirstLetter()}MessagePackFormatter.cs";

                await WriteAsync(generatedMessagePackFormatter, outputDirectory, fileName);
            }
        }

        /// <summary>
        /// Generates the named MessagePackFormatter is to be generated
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult"/> holding the UML model
        /// </param>
        /// <param name="outputDirectory">
        /// The <see cref="DirectoryInfo"/> to which the generated class is written
        /// </param>
        /// <param name="className">
        /// the name of the <see cref="IClass"/> for which the 
        /// </param>
        /// <returns>
        /// The generated code as string
        /// </returns>
        public async Task<string> GenerateMessagePackFormatter(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string className)
        {
            var template = this.Templates[DtoMessagePackFormatterTemplateName];

            var @class = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .Single(x => x.Name == className);

            var generatedMessagePackFormatter = template(@class);

            generatedMessagePackFormatter = this.CodeCleanup(generatedMessagePackFormatter);

            var fileName = $"{@class.Name.CapitalizeFirstLetter()}MessagePackFormatter.cs";

            await WriteAsync(generatedMessagePackFormatter, outputDirectory, fileName);

            return generatedMessagePackFormatter;
        }

        /// <summary>
        /// Register the custom helpers
        /// </summary>
        protected override void RegisterHelpers()
        {
            this.Handlebars.RegisterStringHelper();
            this.Handlebars.RegisterClassHelper();
            this.Handlebars.RegisterPropertyHelper();

            SysML2.NET.CodeGenerator.HandleBarHelpers.ClassHelper.RegisterClassHelper(this.Handlebars);
            SysML2.NET.CodeGenerator.HandleBarHelpers.NamedElementHelper.RegisterNamedElementHelper(this.Handlebars);
            SysML2.NET.CodeGenerator.HandleBarHelpers.PropertyHelper.RegisterPropertyHelper(this.Handlebars);
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate(MessagePackPayloadTemplateName);
            this.RegisterTemplate(MessagePackPayloadFactoryTemplateName);
            this.RegisterTemplate(MessagePackDataResolverGetFormatterHelper);
            this.RegisterTemplate(PayloadMessagePackFormatterTemplateName);
            this.RegisterTemplate(DtoMessagePackFormatterTemplateName);
        }
    }
}
