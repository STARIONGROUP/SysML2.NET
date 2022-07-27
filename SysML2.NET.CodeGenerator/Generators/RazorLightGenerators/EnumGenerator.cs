// -------------------------------------------------------------------------------------------------
// <copyright file="EnumGenerator.cs" company="RHEA System S.A.">
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
    
    using SysML2.NET.CodeGenerator.Extensions;

    /// <summary>
    /// The Enum Generator
    /// </summary>
    public class EnumGenerator : RazorLightGenerator
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
            foreach (var eEnum in package.EClassifiers.OfType<EEnum>())
            {
                var generatedEnum = await Engine.CompileRenderAsync("enum-template.cshtml", eEnum);

                generatedEnum = this.CodeCleanup(generatedEnum);

                var fileName = $"{eEnum.Name.CapitalizeFirstLetter()}.cs";

                await Write(generatedEnum, outputDirectory, fileName);
            }
        }
    }
}
