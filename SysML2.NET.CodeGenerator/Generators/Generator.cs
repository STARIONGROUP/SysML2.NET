// -------------------------------------------------------------------------------------------------
// <copyright file="Generator.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.CodeGenerator.Generators
{
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;

    using ECoreNetto;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.Formatting;
    
    /// <summary>
    /// Abstract class from which all generators derive
    /// </summary>
    public abstract class Generator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Generator"/> class.
        /// </summary>
        protected Generator()
        {
            var assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            this.TemplateFolderPath = Path.Combine(assemblyFolder, "Templates");
        }

        /// <summary>
        /// Gets the path where the template are stored
        /// </summary>
        public string TemplateFolderPath { get; private set; }

        /// <summary>
        /// Generates code specific to the concrete implementation
        /// </summary>
        /// <param name="package">
        /// the <see cref="EPackage"/> that contains the Ecore data to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        public abstract Task Generate(EPackage package, DirectoryInfo outputDirectory);

        /// <summary>
        /// perform code cleanup
        /// </summary>
        /// <param name="generatedCode">
        /// The generated code that needs to be cleaned
        /// </param>
        /// <returns>
        /// cleaned up code
        /// </returns>
        protected virtual string CodeCleanup(string generatedCode)
        {
            generatedCode = generatedCode.Replace("&nbsp;", " ");
            var workspace = new AdhocWorkspace();
            var syntaxTree = CSharpSyntaxTree.ParseText(generatedCode);
            var root = syntaxTree.GetRoot();
            var formattedSyntaxNode = Formatter.Format(root, workspace);
            generatedCode = formattedSyntaxNode.SyntaxTree.GetText().ToString();

            return generatedCode;
        }

        /// <summary>
        /// Writes the generated code to disk
        /// </summary>
        /// <param name="generatedCode">
        /// he generated code that needs to be written to disk
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <param name="fileName">
        /// The name of the file
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        protected static async Task Write(string generatedCode, DirectoryInfo outputDirectory, string fileName)
        {
            var filePath = Path.Combine(outputDirectory.FullName, fileName);

            await File.WriteAllTextAsync(filePath, generatedCode);
        }
    }
}
