// -------------------------------------------------------------------------------------------------
// <copyright file="SQLSchemaGenerator.cs" company="Starion Group S.A.">
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
    using System.Threading.Tasks;

    using SysML2.NET.CodeGenerator.HandleBarHelpers;

    using uml4net.HandleBars;
    using uml4net.xmi.Readers;

    /// <summary>
    /// A Handlebars based generator that produces the PostgreSQL persistence schema for SysML v2
    /// models from the UML metamodel. Only non-derived, non-redefining properties are stored;
    /// derived state is materialized separately at commit time. The reference design is
    /// SysML2.NET.CodeGenerator/Sql/schema.golden.sql.
    /// </summary>
    public class SQLSchemaGenerator : UmlHandleBarsGenerator
    {
        /// <summary>
        /// The name of the generated schema file
        /// </summary>
        public const string SchemaFileName = "schema2.sql";

        /// <summary>
        /// Generates the PostgreSQL schema
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
        public override async Task GenerateAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            await this.GenerateSqlSchemaAsync(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates the PostgreSQL schema and returns the generated DDL
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult" /> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task" /> carrying the generated DDL
        /// </returns>
        public Task<string> GenerateSqlSchemaAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            return this.GenerateSqlSchemaInternalAsync(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Register the custom helpers
        /// </summary>
        protected override void RegisterHelpers()
        {
            this.Handlebars.RegisterStringHelper();
            this.Handlebars.RegisterEnumerableHelper();

            this.Handlebars.RegisterUmlTemplateSqlSchemaHelpers();
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate("core-sql-schema-2");
        }

        /// <summary>
        /// Generates the PostgreSQL schema and returns the generated DDL
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult" /> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo" />
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task" /> carrying the generated DDL
        /// </returns>
        private async Task<string> GenerateSqlSchemaInternalAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            var template = this.Templates["core-sql-schema-2"];

            var payload = CreateHandlebarsPayload(xmiReaderResult);

            var generatedSchema = template(payload);

            await WriteAsync(generatedSchema, outputDirectory, SchemaFileName);

            return generatedSchema;
        }
    }
}
