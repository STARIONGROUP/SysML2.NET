// -------------------------------------------------------------------------------------------------
// <copyright file="UmlMessagePackGenerator.cs" company="Starion Group S.A.">
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
        private const string DtoPayloadMessagePackFormatterTemplateName = "core-dto-payloadmessagepackformatter-uml-template";

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

        public async Task<string> GenerateMessagePackPayloadFactory(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates[MessagePackPayloadFactoryTemplateName];

            var classes = xmiReaderResult.Root.QueryPackages()
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .OrderBy(x => x.Name)
                .ToList();

            var generatedMessagePackPayloadFactory= template(classes);

            generatedMessagePackPayloadFactory = this.CodeCleanup(generatedMessagePackPayloadFactory);

            const string fileName = "PayloadFactory.cs";

            await Write(generatedMessagePackPayloadFactory, outputDirectory, fileName);

            return generatedMessagePackPayloadFactory;
        }

        public async Task<string> GenerateMessagePackPayload(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates[MessagePackPayloadTemplateName];

            var classes = xmiReaderResult.Root.QueryPackages()
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
            this.RegisterTemplate(MessagePackPayloadTemplateName);
            this.RegisterTemplate(MessagePackPayloadFactoryTemplateName);
            this.RegisterTemplate(PayloadMessagePackFormatterTemplateName);
            this.RegisterTemplate(DtoPayloadMessagePackFormatterTemplateName);
        }
    }
}
