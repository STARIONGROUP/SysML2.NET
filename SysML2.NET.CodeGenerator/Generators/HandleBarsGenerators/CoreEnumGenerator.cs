// -------------------------------------------------------------------------------------------------
// <copyright file="CoreEnumGenerator.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
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
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using ECoreNetto;
    
    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.HandleBarHelpers;

    /// <summary>
    /// A Handlebars based enum code generator
    /// </summary>
    public class CoreEnumGenerator : EcoreHandleBarsGenerator
    {
        /// <summary>
        /// Generates the <see cref="EEnum"/> instances
        /// that are in the provided <see cref="EPackage"/>
        /// </summary>
        /// <param name="package">
        /// the <see cref="EPackage"/> that contains the <see cref="EEnum"/> to generate
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        public override async Task Generate(EPackage package, DirectoryInfo outputDirectory)
        {
            var template = this.Templates["core-enum-template"];

            foreach (var eEnum in package.EClassifiers.OfType<EEnum>())
            {
                var generatedEnum = template(eEnum);

                generatedEnum = CodeCleanup(generatedEnum);

                var fileName = $"{eEnum.Name.CapitalizeFirstLetter()}.cs";

                await Write(generatedEnum, outputDirectory, fileName);
            }
        }

        /// <summary>
        /// Register the custom helpers
        /// </summary>
        protected override void RegisterHelpers()
        {
            this.Handlebars.RegisteredDocumentationHelper();
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate("core-enum-template");
        }
    }
}
