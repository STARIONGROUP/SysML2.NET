// -------------------------------------------------------------------------------------------------
// <copyright file="UmlHandleBarsGenerator.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators
{
    using System.IO;
    using System.Threading.Tasks;

    using uml4net.xmi.Readers;

    /// <summary>
    /// Abstract super class from which all uml based <see cref="HandlebarsDotNet"/> generators
    /// need to derive
    /// </summary>
    public abstract class UmlHandleBarsGenerator : HandleBarsGenerator
    {
        /// <summary>
        /// Generates code specific to the concrete implementation
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult"/> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        public abstract Task GenerateAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory);
    }
}