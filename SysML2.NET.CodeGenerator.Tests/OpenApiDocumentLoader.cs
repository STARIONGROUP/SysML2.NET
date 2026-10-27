// ------------------------------------------------------------------------------------------------
// <copyright file="OpenApiDocumentLoader.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Tests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.OpenApi;
    using Microsoft.OpenApi.Reader;

    using NUnit.Framework;

    public static class OpenApiDocumentLoader
    {
        private static readonly Lazy<Task<OpenApiDocument>> LazyDocument = new(LoadDocumentAsync);

        public static Task<OpenApiDocument> LoadAsync() => LazyDocument.Value;

        private static async Task<OpenApiDocument> LoadDocumentAsync()
        {
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "datamodel", "ptc-25-02-30.json");

            var settings = new OpenApiReaderSettings { RuleSet = ValidationRuleSet.GetEmptyRuleSet() };

            var result = await OpenApiDocument.LoadAsync(path, settings: settings);

            return result.Document;
        }
    }
}
