// -------------------------------------------------------------------------------------------------
// <copyright file="DalPocoExtensionsGenerator.cs" company="RHEA System S.A.">
// 
//   Copyright 2022-2023 RHEA System S.A.
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
    /// A Handlebars based DAL POCO Extensions code generator
    /// </summary>
    public class DalPocoExtensionsGenerator : HandleBarsGenerator
    {
        /// <summary>
        /// Generates the <see cref="EClass"/> static poco extensions for the DAL library
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
            await this.GenerateElementExtensions(package, outputDirectory);
            await this.GenerateDalPocoExtensions(package, outputDirectory);
        }

        /// <summary>
        /// Generates the DAL.ElementExtensions class/>
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
        public async Task GenerateElementExtensions(EPackage package, DirectoryInfo outputDirectory)
        {
            var template = this.Templates["dal-element-extensions"];

            var eClasses = package.EClassifiers.OfType<EClass>().Where(x => !x.Abstract).OrderBy(x => x.Name).ToList();

            var generatedElementExtensions = template(eClasses);

            generatedElementExtensions = CodeCleanup(generatedElementExtensions);

            var fileName = "ElementExtensions.cs";

            await Write(generatedElementExtensions, outputDirectory, fileName);
        }

        /// <summary>
        /// Generates the Dal PocoFactory classes for each <see cref="EClass"/> in the provided <see cref="EPackage"/>
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
        public async Task GenerateDalPocoExtensions(EPackage package, DirectoryInfo outputDirectory)
        {
            var eClasses = package.EClassifiers.OfType<EClass>().Where(x => !x.Abstract).OrderBy(x => x.Name).ToList();

            foreach (var eClass in eClasses)
            {
                await this.GenerateDalPocoExtension(package, outputDirectory, eClass.Name);
            }
        }

        /// <summary>
        /// Generates the Dal PocoFactory class for the <see cref="EClass"/> in the provided <see cref="EPackage"/>
        /// </summary>
        /// <param name="package">
        /// the <see cref="EPackage"/> that contains the classes that need to be generated
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <param name="className">
        /// name of tha <see cref="EClass"/> that is to be generated.
        /// </param>
        /// <returns>
        /// string containing the generated code
        /// </returns>
        public async Task<string> GenerateDalPocoExtension(EPackage package, DirectoryInfo outputDirectory, string className)
        {
            var template = this.Templates["dal-poco-extensions"];

            var eClass = package.EClassifiers.OfType<EClass>().Single(x => x.Name == className);

            var generatedPocoExtensions  = template(eClass);

            generatedPocoExtensions = CodeCleanup(generatedPocoExtensions);

            var fileName = $"{eClass.Name.CapitalizeFirstLetter()}Extensions.cs";

            await Write(generatedPocoExtensions, outputDirectory, fileName);

            return generatedPocoExtensions;
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
            this.RegisterTemplate("dal-element-extensions");
            this.RegisterTemplate("dal-poco-extensions");
        }
    }
}
