// -------------------------------------------------------------------------------------------------
// <copyright file="TextuNotationValidationTestFixture.cs" company="Starion Group S.A.">
// 
//    Copyright (C) 2022-2026 Starion Group S.A.
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

namespace SysML2.NET.Serializer.TextualNotation.Tests.Writers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using NUnit.Framework;

    using SysML2.NET.Serializer.TextualNotation.Tests.Wrapper;
    using SysML2.NET.Serializer.TextualNotation.Writers;
    using SysML2.NET.Serializer.Xmi;

    [TestFixture]
    public class TextualNotationValidationTestFixture
    {
        [Test]
        [TestCase("01-Parts Tree", "1a-Parts Tree.sysmlx")]
        [TestCase("01-Parts Tree", "1c-Parts Tree Redefinition.sysmlx")]
        [TestCase("01-Parts Tree", "1d-Parts Tree with Reference.sysmlx")]
        public async Task VerifyValidationTextualNotationXmi(string folderName, string fileName)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Warning);
            });

            var libraryRoot = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources");
            
            var redirectingService = new LibraryRedirectingExternalReferenceService(
                libraryRoot,
                loggerFactory.CreateLogger<LibraryRedirectingExternalReferenceService>());

            var deSerializer = new DeSerializer(loggerFactory, redirectingService);

            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Validation", folderName, fileName);

            var rootNamespace = await deSerializer.DeSerializeAsync(new Uri(filePath));

            using var writerContext = new TextualNotationWriterContext(rootNamespace);
            writerContext.EmitOperatorParentheses = false;
            var stringBuilder = new IndentedStringBuilder();

            try
            {
                NamespaceTextualNotationBuilder.BuildRootNamespace(rootNamespace, writerContext, stringBuilder);
            }
            catch (System.Exception exception)
            {
                TestContext.WriteLine($"Builder stopped early due to: {exception.Message}, {exception.StackTrace}");
            }

            var textualNotation = stringBuilder.ToString();

            Assert.That(textualNotation, Is.Not.Empty);
            TestContext.WriteLine("=== Textual Notation Output ===");
            TestContext.WriteLine(textualNotation);
            TestContext.WriteLine("=== End ===");
        }
    }
}
