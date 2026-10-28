// ------------------------------------------------------------------------------------------------
// <copyright file="Generator.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Generators
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

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
            this.AssignTemplateFolderPath();
        }

        /// <summary>
        /// Assigns the value of the <see cref="TemplateFolderPath"/>
        /// </summary>
        private void AssignTemplateFolderPath()
        {
            var assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            this.TemplateFolderPath = Path.Combine(assemblyFolder, "Templates");

            var subfolderLocation = this.GetOptionalSubfolderTemplateLocation();

            if (!string.IsNullOrWhiteSpace(subfolderLocation))
            {
                this.TemplateFolderPath = Path.Combine(this.TemplateFolderPath, subfolderLocation);
            }
        }

        /// <summary>
        /// Gets an optional subfolder location path to locate templates
        /// </summary>
        /// <returns>An optional subfolder name</returns>
        protected virtual string GetOptionalSubfolderTemplateLocation()
        {
            return null;
        }

        /// <summary>
        /// Gets the path where the template are stored
        /// </summary>
        public string TemplateFolderPath { get; protected set; }
        
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

            await File.WriteAllTextAsync(filePath, generatedCode, Encoding.UTF8);
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
        protected static async Task WriteAsync(string generatedCode, DirectoryInfo outputDirectory, string fileName)
        {
            if (string.IsNullOrEmpty(generatedCode))
            {
                throw new ArgumentException($"the {nameof(generatedCode)} may not be null", nameof(generatedCode));
            }

            ArgumentNullException.ThrowIfNull(outputDirectory);

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException($"the {nameof(fileName)} may not be null",nameof(fileName));
            }

            var filePath = Path.Combine(outputDirectory.FullName, fileName);

            await File.WriteAllTextAsync(filePath, generatedCode, Encoding.UTF8);
        }
    }
}
