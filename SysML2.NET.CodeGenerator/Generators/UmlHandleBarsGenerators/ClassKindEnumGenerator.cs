// -------------------------------------------------------------------------------------------------
// <copyright file="ClassKindEnumGenerator.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
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
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using SysML2.NET.CodeGenerator.HandleBarHelpers;

    using uml4net.xmi.Readers;

    /// <summary>
    /// A Handlebars based generator that emits the ClassKind enum — the frozen smallint interning
    /// of the SysML v2 metaclass names, the C# mirror of the sysml2.class_kind catalog table. The
    /// source of truth is the append-only <see cref="ClassKindRegistry" />, NOT the UML model: the
    /// model is only used to fail fast on registry drift, exactly as the SQL schema generator does.
    /// </summary>
    public class ClassKindEnumGenerator : UmlHandleBarsGenerator
    {
        /// <summary>
        /// Generates the ClassKind enum
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult" /> that contains the UML model to validate the
        /// <see cref="ClassKindRegistry" /> against
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        public override async Task GenerateAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            await this.GenerateClassKindEnumAsync(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates the ClassKind enum and returns the generated code
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult" /> that contains the UML model to validate the
        /// <see cref="ClassKindRegistry" /> against
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <returns>
        /// an awaitable task that returns the generated code
        /// </returns>
        public Task<string> GenerateClassKindEnumAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GenerateClassKindEnumInternalAsync(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Register the custom helpers — none: the template renders a fully precomputed
        /// <see cref="ClassKindEnumPayload" />
        /// </summary>
        protected override void RegisterHelpers()
        {
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate("core-classkind-enum-template");
        }

        /// <summary>
        /// Generates the ClassKind enum
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult" /> that contains the UML model to validate the
        /// <see cref="ClassKindRegistry" /> against
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <returns>
        /// an awaitable task that returns the generated code
        /// </returns>
        private async Task<string> GenerateClassKindEnumInternalAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var payload = CreateHandlebarsPayload(xmiReaderResult);

            SqlSchemaHelpers.AssertRegistryInSyncWithModel(payload);

            var versionNamesById = ClassKindRegistry.ModelVersions.ToDictionary(version => version.Id, version => version.Name);

            var members = ClassKindRegistry.ClassKinds
                .Select(registration => new ClassKindEnumMember(
                    registration.Name,
                    registration.Id,
                    registration.IsAbstract,
                    versionNamesById[registration.IntroducedIn],
                    registration.RemovedIn.HasValue ? versionNamesById[registration.RemovedIn.Value] : null))
                .ToList();

            var template = this.Templates["core-classkind-enum-template"];

            var generatedEnum = template(new ClassKindEnumPayload(members));

            generatedEnum = this.CodeCleanup(generatedEnum);

            await WriteAsync(generatedEnum, outputDirectory, "ClassKind.cs");

            return generatedEnum;
        }
    }
}
