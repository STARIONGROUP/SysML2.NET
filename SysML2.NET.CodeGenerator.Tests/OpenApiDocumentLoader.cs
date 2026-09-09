// -------------------------------------------------------------------------------------------------
// <copyright file="OpenApiDocumentLoader.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
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
