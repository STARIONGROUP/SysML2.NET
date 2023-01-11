// -------------------------------------------------------------------------------------------------
// <copyright file="DtoDeSerializerGenerator.cs" company="RHEA System S.A.">
//
//   Copyright 2022 RHEA System S.A.
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
    using System.Linq;
    using System.Threading.Tasks;

    using System.IO;

    using ECoreNetto;

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.HandleBarHelpers;

    /// <summary>
    /// A Handlebars based DTO code generator
    /// </summary>
    public class DtoDeSerializerGenerator : HandleBarsGenerator
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
            await this.GenerateEnumDeSerializers(package, outputDirectory);
            await this.GenerateDtoDeSerializers(package, outputDirectory);
            await this.GenerateDeSerializationProvider(package, outputDirectory);
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
        public async Task GenerateEnumDeSerializers(EPackage package, DirectoryInfo outputDirectory)
        {
            var template = this.Templates["enum-deserializer-template"];

            foreach (var eClass in package.EClassifiers.OfType<EEnum>())
            {
                var generatedDeSerializer = template(eClass);

                generatedDeSerializer = CodeCleanup(generatedDeSerializer);

                var fileName = $"{eClass.Name.CapitalizeFirstLetter()}DeSerializer.cs";

                await Write(generatedDeSerializer, outputDirectory, fileName);
            }
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
        public async Task GenerateDtoDeSerializers(EPackage package, DirectoryInfo outputDirectory)
        {
            var template = this.Templates["dto-deserializer-template"];

            foreach (var eClass in package.EClassifiers.OfType<EClass>().Where(x => !x.Abstract))
            {
                var generatedDeSerializer = template(eClass);

                generatedDeSerializer = CodeCleanup(generatedDeSerializer);

                var fileName = $"{eClass.Name.CapitalizeFirstLetter()}DeSerializer.cs";

                await Write(generatedDeSerializer, outputDirectory, fileName);
            }
        }

        /// <summary>
        /// Generates the DeSerializationProvider class
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
        public async Task GenerateDeSerializationProvider(EPackage package, DirectoryInfo outputDirectory)
        {
            var template = this.Templates["dto-deserialization-provider-template"];

            var eClasses = package.EClassifiers.OfType<EClass>().Where(x => !x.Abstract).OrderBy(x => x.Name).ToList();
            
            var generatedDeSerializationProvider = template(eClasses);

            generatedDeSerializationProvider = CodeCleanup(generatedDeSerializationProvider);

            var fileName = "DeSerializationProvider.cs";

            await Write(generatedDeSerializationProvider, outputDirectory, fileName);
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
            this.RegisterTemplate("enum-deserializer-template");
            this.RegisterTemplate("dto-deserializer-template");
            this.RegisterTemplate("dto-deserialization-provider-template");
        }
    }
}
