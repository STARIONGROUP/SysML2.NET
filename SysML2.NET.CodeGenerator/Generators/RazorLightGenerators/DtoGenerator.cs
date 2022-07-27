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

namespace SysML2.NET.CodeGenerator.Generators.RazorLightGenerators
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using ECoreNetto;

    using RazorLight;

    using SysML2.NET.CodeGenerator.Extensions;

    /// <summary>
    /// The DTO Generator
    /// </summary>
    public class DtoGenerator : RazorLightGenerator
    {
        /// <summary>
        /// Generates the Data Transfer Objects (DTO) for the <see cref="EClass"/> instances
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
            await GenerateInterfaces(package, outputDirectory);
        }

        /// <summary>
        /// Generate the DTO interfaces
        /// </summary>
        /// <param name="engine">
        /// The <see cref="RazorLightEngine"/> used to perform the code generation
        /// </param>
        /// <param name="package">
        /// the <see cref="EPackage"/> that contais the <see cref="EClass"/> to generate
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// an awaitable <see cref="Task"/>
        private async Task GenerateInterfaces(EPackage package, DirectoryInfo outputDirectory)
        {
            foreach (var eClass in package.EClassifiers.OfType<EClass>())
            {
                var generatedInterface = await Engine.CompileRenderAsync("dto-interface-template.cshtml", eClass);

                generatedInterface = CodeCleanup(generatedInterface);

                var fileName = $"I{eClass.Name.CapitalizeFirstLetter()}.cs";

                await Write(generatedInterface, outputDirectory, fileName);
            }
        }
    }
}
