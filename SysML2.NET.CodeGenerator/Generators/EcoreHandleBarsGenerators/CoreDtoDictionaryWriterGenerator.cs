// -------------------------------------------------------------------------------------------------
// <copyright file="DtoDictionaryWriterGenerator.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Generators.HandleBarsGenerators
{
    using System.Linq;
    using System.Threading.Tasks;

    using System.IO;

    using ECoreNetto;

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.HandleBarHelpers;

    /// <summary>
    /// A Handlebars based Dictionary writer code generator
    /// </summary>
	public class CoreDtoDictionaryWriterGenerator : EcoreHandleBarsGenerator
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
            await this.GenerateDtoDictionaryWriters(package, outputDirectory);
            await this.GenerateDtoDictionaryWriterProvider(package, outputDirectory);
        }

        /// <summary>
        /// Generates the DeSerializer classes for each <see cref="EClass"/> in the provided <see cref="EPackage"/>
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
        public async Task GenerateDtoDictionaryWriters(EPackage package, DirectoryInfo outputDirectory)
        {
            var template = this.Templates["core-dictionary-dto-serializer-writer-template"];

            foreach (var eClass in package.EClassifiers.OfType<EClass>().Where(x => !x.Abstract))
            {
                var generatedDeSerializer = template(eClass);

                generatedDeSerializer = CodeCleanup(generatedDeSerializer);

                var fileName = $"{eClass.Name.CapitalizeFirstLetter()}DictionaryWriter.cs";

                await Write(generatedDeSerializer, outputDirectory, fileName);
            }
        }

        /// <summary>
        /// Generates the DictionaryWriter for the specified class
        /// </summary>
        /// <param name="package">
        /// the <see cref="EPackage"/> that contains the classes that need to be generated
        /// </param>
        /// <param name="className">
        /// The name of the <see cref="EClass"/> that needs to be generated
        /// </param>
        /// <returns>
        /// generated code
        /// </returns>
        public async Task<string> GenerateDtoDictionaryWriter(EPackage package, DirectoryInfo outputDirectory, string className)
        {
            var template = this.Templates["core-dictionary-dto-serializer-writer-template"];

            var eClass = package.EClassifiers.OfType<EClass>().Single(x =>x.Name == className);

            var generatedDictionaryWriter = template(eClass);

            generatedDictionaryWriter = CodeCleanup(generatedDictionaryWriter);

            var fileName = $"{eClass.Name.CapitalizeFirstLetter()}DictionaryWriter.cs";

            await Write(generatedDictionaryWriter, outputDirectory, fileName);

            return generatedDictionaryWriter;
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
        public async Task GenerateDtoDictionaryWriterProvider(EPackage package, DirectoryInfo outputDirectory)
        {
            var template = this.Templates["core-dictionary-dto-serializer-writer-provider-template"];

            var eClasses = package.EClassifiers.OfType<EClass>().Where(x => !x.Abstract).OrderBy(x => x.Name).ToList();

            var generatedSerializationProvider = template(eClasses);

            generatedSerializationProvider = CodeCleanup(generatedSerializationProvider);

            var fileName = "DictionaryWriterProvider.cs";

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
            this.RegisterTemplate("core-dictionary-dto-serializer-writer-template");
            this.RegisterTemplate("core-dictionary-dto-serializer-writer-provider-template");
        }
    }
}
