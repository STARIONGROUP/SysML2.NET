// -------------------------------------------------------------------------------------------------
// <copyright file="RazorLightGenerator.cs" company="RHEA System S.A.">
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
    using RazorLight;

    /// <summary>
    /// Abstract super class from which all <see cref="RazorLight"/> generators
    /// need to derive
    /// </summary>
    public abstract class RazorLightGenerator : Generator
    {
        /// <summary>
        /// The <see cref="RazorLightEngine"/> instance used for code-generation
        /// </summary>
        protected readonly RazorLightEngine Engine;

        /// <summary>
        /// Initializes a new instance of the <see cref="RazorLightGenerator"/>
        /// </summary>
        protected RazorLightGenerator()
        {
            this.Engine = new RazorLightEngineBuilder()
                .UseFileSystemProject(this.TemplateFolderPath)
                .UseMemoryCachingProvider()
                .Build();
        }

        /// <summary>
        /// perform code cleanup
        /// </summary>
        /// <param name="generatedCode">
        /// The generated code that needs to be cleaned
        /// </param>
        /// <returns>
        /// cleaned up code
        /// </returns>
        protected override string CodeCleanup(string generatedCode)
        {
            generatedCode = generatedCode.Replace("<gen>", "").Replace("</gen>", "").Replace("&nbsp;", " ");

            return base.CodeCleanup(generatedCode);
        }
    }
}
