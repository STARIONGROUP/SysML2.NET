// -------------------------------------------------------------------------------------------------
// <copyright file="EnumHandlebarGenerator.cs" company="RHEA System S.A.">
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
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using ECoreNetto;

    using HandlebarsDotNet;
    using HandlebarsDotNet.Helpers;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.Formatting;

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.HandleBarHelpers;

    /// <summary>
    /// A handlebars based enum code generator
    /// </summary>
    public class EnumHandlebarGenerator
    {
        private string template;

        private IHandlebars handlebars;

        private HandlebarsTemplate<object, object> enumTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumHandlebarGenerator"/> class.
        /// </summary>
        public EnumHandlebarGenerator()
        {
            this.handlebars = Handlebars.CreateSharedEnvironment();
            this.handlebars.RegisteredDocumentationHelper();
            HandlebarsHelpers.Register(this.handlebars);
            
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var templatePath = Path.Combine(assemblyFolder, "Templates", "enum-template.hbs");

            this.template = File.ReadAllText(templatePath);

            this.enumTemplate = this.handlebars.Compile(this.template);
        }

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
        public async Task Generate(EPackage package, DirectoryInfo outputDirectory)
        {
            foreach (var eEnum in package.EClassifiers.OfType<EEnum>())
            {
                var generatedEnum = this.enumTemplate(eEnum);
                
                generatedEnum = this.CodeCleanup(generatedEnum);

                var fileName = $"{eEnum.Name.CapitalizeFirstLetter()}.cs";

                await this.Write(generatedEnum, outputDirectory, fileName);
            }
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
        public string CodeCleanup(string generatedCode)
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
        public async Task Write(string generatedCode, DirectoryInfo outputDirectory, string fileName)
        {
            var filePath = Path.Combine(outputDirectory.FullName, fileName);

            await File.WriteAllTextAsync(filePath, generatedCode);
        }
    }
}
