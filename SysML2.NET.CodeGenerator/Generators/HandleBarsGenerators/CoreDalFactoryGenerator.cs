// -------------------------------------------------------------------------------------------------
// <copyright file="DalFactoryGenerator.cs" company="RHEA System S.A.">
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
    /// A Handlebars based DAL Factory code generator
    /// </summary>
    public class CoreDalFactoryGenerator : EcoreHandleBarsGenerator
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
            await this.GenerateElementDalFactory(package, outputDirectory);
            await this.GeneratePocoFactories(package, outputDirectory);
        }

        /// <summary>
        /// Generates the Dal ElementFactory class for the <see cref="EClass"/>es in the provided <see cref="EPackage"/>
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
        public async Task GenerateElementDalFactory(EPackage package, DirectoryInfo outputDirectory)
        {
            var template = this.Templates["core-element-dal-factory"];

            var eClasses = package.EClassifiers.OfType<EClass>().Where(x => !x.Abstract).OrderBy(x => x.Name).ToList();

            var generatedElementDalFactory = template(eClasses);

            generatedElementDalFactory = CodeCleanup(generatedElementDalFactory);

            var fileName = "ElementFactory.cs";

            await Write(generatedElementDalFactory, outputDirectory, fileName);
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
        public async Task GeneratePocoFactories(EPackage package, DirectoryInfo outputDirectory)
        {
            var eClasses = package.EClassifiers.OfType<EClass>().Where(x => !x.Abstract).OrderBy(x => x.Name).ToList();

            foreach (var eClass in eClasses)
            {
                await this.GeneratePocoFactory(package,outputDirectory, eClass.Name);
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
        public async Task<string> GeneratePocoFactory(EPackage package, DirectoryInfo outputDirectory, string className)
        {
            var template = this.Templates["core-poco-dal-factory"];

            var eClass = package.EClassifiers.OfType<EClass>().Single(x => x.Name == className);

            var generatedPocoFactory= template(eClass);

            generatedPocoFactory = CodeCleanup(generatedPocoFactory);

            var fileName = $"{eClass.Name.CapitalizeFirstLetter()}Factory.cs";

            await Write(generatedPocoFactory, outputDirectory, fileName);

            return generatedPocoFactory;
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
            this.RegisterTemplate("core-element-dal-factory");
            this.RegisterTemplate("core-poco-dal-factory");
        }
    }
}
