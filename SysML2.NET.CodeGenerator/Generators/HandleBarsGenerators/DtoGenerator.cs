// -------------------------------------------------------------------------------------------------
// <copyright file="DtoGenerator.cs" company="RHEA System S.A.">
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
    public class DtoGenerator : HandleBarsGenerator
    {
        /// <summary>
        /// Generates the <see cref="EClass"/> instances
        /// that are in the provided <see cref="EPackage"/>
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
            await this.GenerateInterfaces(package, outputDirectory);
            await this.GenerateClasses(package, outputDirectory);
        }

        public async Task GenerateInterfaces(EPackage package, DirectoryInfo outputDirectory)
        {
            var template = this.Templates["dto-interface-template"];

            foreach (var eClass in package.EClassifiers.OfType<EClass>())
            {
                var generatedInterface = template(eClass);

                generatedInterface = CodeCleanup(generatedInterface);

                var fileName = $"I{eClass.Name.CapitalizeFirstLetter()}.cs";

                await Write(generatedInterface, outputDirectory, fileName);
            }
        }

        public async Task GenerateClasses(EPackage package, DirectoryInfo outputDirectory)
        {
            var template = this.Templates["dto-class-template"];

            foreach (var eClass in package.EClassifiers.OfType<EClass>())
            {
                var generatedInterface = template(eClass);

                generatedInterface = CodeCleanup(generatedInterface);

                var fileName = $"{eClass.Name.CapitalizeFirstLetter()}.cs";

                await Write(generatedInterface, outputDirectory, fileName);
            }
        }

        /// <summary>
        /// Register the custom helpers
        /// </summary>
        protected override void RegisterHelpers()
        {
            this.Handlebars.RegisteredDocumentationHelper();
            this.Handlebars.RegisterTypeNameHelper();
            this.Handlebars.RegisterGeneralizationHelper();
            this.Handlebars.RegisterStructuralFeatureHelper();
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate("dto-interface-template");
            this.RegisterTemplate("dto-class-template");
        }
    }
}
