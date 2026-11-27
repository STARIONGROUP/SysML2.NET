// ------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreDtoValidatorGenerator.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.HandleBarHelpers;

    using uml4net.Extensions;
    using uml4net.StructuredClassifiers;
    using uml4net.xmi.Readers;

    /// <summary>
    /// UML based handlebars generator for the FluentValidation validators of the core data transfer objects
    /// </summary>
    public class UmlCoreDtoValidatorGenerator : UmlHandleBarsGenerator
    {
        /// <summary>
        /// The name of the template that generates a DTO validator
        /// </summary>
        private const string ValidatorTemplateName = "core-dto-validator-uml-template";

        /// <summary>
        /// Generates the validators of the concrete classes of the UML model
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult" /> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        public override Task GenerateAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GenerateInternalAsync(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates the validator of the specified class
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult" /> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <param name="name">
        /// The name of the class to generate the validator for
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        public Task<string> GenerateDtoValidatorAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory, string name)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            return this.WriteValidatorAsync(QueryValidatedClasses(xmiReaderResult).Single(x => x.Name == name), outputDirectory);
        }

        /// <summary>
        /// Register the custom helpers
        /// </summary>
        protected override void RegisterHelpers()
        {
            this.Handlebars.RegisterClassHelper();
            this.Handlebars.RegisterNamedElementHelper();
            this.Handlebars.RegisterDtoValidatorHelper();
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate(ValidatorTemplateName);
        }

        /// <summary>
        /// Queries the concrete classes that a validator is generated for
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult" /> that contains the UML model to generate from
        /// </param>
        /// <returns>
        /// The concrete classes of the UML model
        /// </returns>
        private static IEnumerable<IClass> QueryValidatedClasses(XmiReaderResult xmiReaderResult)
        {
            return xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract);
        }

        /// <summary>
        /// Generates the validators of the concrete classes of the UML model
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult" /> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        private async Task GenerateInternalAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            foreach (var @class in QueryValidatedClasses(xmiReaderResult))
            {
                await this.WriteValidatorAsync(@class, outputDirectory);
            }
        }

        /// <summary>
        /// Applies the validator template to the class and writes the result to disk
        /// </summary>
        /// <param name="class">
        /// The <see cref="IClass" /> to generate the validator for
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <returns>
        /// The generated code
        /// </returns>
        private async Task<string> WriteValidatorAsync(IClass @class, DirectoryInfo outputDirectory)
        {
            var generatedCode = this.CodeCleanup(this.Templates[ValidatorTemplateName](@class));

            await WriteAsync(generatedCode, outputDirectory, $"{@class.Name}Validator.cs");

            return generatedCode;
        }
    }
}
