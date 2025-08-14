// -------------------------------------------------------------------------------------------------
// <copyright file="CoreJsonDtoSerializerGenerator.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2025 Starion Group S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Generators.HandleBarsGenerators
{
    using ECoreNetto;
    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.HandleBarHelpers;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// A Handlebars based DTO JSON serializer code generator
    /// </summary>
    public class CoreJsonDtoSerializerGenerator : EcoreHandleBarsGenerator
    {
        /// <summary>
        /// Generates the <see cref="EClass"/> static serializers
        /// for each <see cref="EPackage"/>
        /// </summary>
        /// <param name="package">
        /// the <see cref="EPackage"/> that contains the <see cref="EClass"/> to generate
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        public override async Task Generate(EPackage package, DirectoryInfo outputDirectory)
        {
            await this.GenerateSerializers(package, outputDirectory);
            await this.GenerateSerializationProvider(package, outputDirectory);
        }

        /// <summary>
        /// Generates the Serializer classes for each <see cref="EClass"/> in the provided <see cref="EPackage"/>
        /// </summary>
        /// <param name="package">
        /// the <see cref="EPackage"/> that contains the classes that need to be generated
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        public async Task GenerateSerializers(EPackage package, DirectoryInfo outputDirectory)
        {
            var template = this.Templates["core-json-dto-serializer-template"];

            foreach (var eClass in package.EClassifiers.OfType<EClass>().Where(x => !x.Abstract))
            {
                var generatedSerializer = template(eClass);

                generatedSerializer = CodeCleanup(generatedSerializer);

                var fileName = $"{eClass.Name.CapitalizeFirstLetter()}Serializer.cs";

                await Write(generatedSerializer, outputDirectory, fileName);
            }
        }

        /// <summary>
        /// Generates a named Json Serializer class
        /// </summary>
        /// <param name="package">
        /// the <see cref="EPackage"/> that contains the <see cref="EClass"/> to generate
        /// </param>
        /// <param name="className">
        /// The name of the class that is to be generated
        /// </param>
        /// <returns>
        /// a string representation of the generated class
        /// </returns>
        public string GenerateClass(EPackage package, string className)
        {
            var template = this.Templates["core-json-dto-serializer-template"];

            var eClass = package.EClassifiers.OfType<EClass>().Single(x => x.Name == className);

            if (eClass.Abstract)
            {
                throw new InvalidOperationException("Json Serializer should not be abstract");
            }

            var generatedDeSerializer = template(eClass);

            generatedDeSerializer = CodeCleanup(generatedDeSerializer);

            return generatedDeSerializer;
        }

        /// <summary>
        /// Generates the SerializationProvider class
        /// </summary>
        /// <param name="package">
        /// the <see cref="EPackage"/> that contains the classes that need to be generated
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        public async Task GenerateSerializationProvider(EPackage package, DirectoryInfo outputDirectory)
        {
            var template = this.Templates["core-json-dto-serialization-provider-template"];

            var eClasses = package.EClassifiers.OfType<EClass>().Where(x => !x.Abstract).OrderBy(x => x.Name).ToList();

            var generatedSerializationProvider = template(eClasses);

            generatedSerializationProvider = CodeCleanup(generatedSerializationProvider);

            var fileName = "SerializationProvider.cs";

            await Write(generatedSerializationProvider, outputDirectory, fileName);
        }

        /// <summary>
        /// Register the custom helpers
        /// </summary>
        protected override void RegisterHelpers()
        {
            ECoreNetto.HandleBars.StringHelper.RegisterStringHelper(this.Handlebars);
            ECoreNetto.HandleBars.StructuralFeatureHelper.RegisterStructuralFeatureHelper(this.Handlebars);
            ECoreNetto.HandleBars.GeneralizationHelper.RegisterGeneralizationHelper(this.Handlebars);

            this.Handlebars.RegisteredDocumentationHelper();
            this.Handlebars.RegisterTypeNameHelper();
            this.Handlebars.RegisterStructuralFeatureHelper();
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate("core-json-dto-serializer-template");
            this.RegisterTemplate("core-json-dto-serialization-provider-template");
        }
    }
}
