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
        [TestCase("02-Parts Interconnection", "2a-Parts Interconnection.sysmlx")]
        [TestCase("02-Parts Interconnection", "2c-Parts Interconnection-Multiple Decompositions.sysmlx")]
        [TestCase("03-Function-based Behavior", "3a-Function-based Behavior-1.sysmlx")]
        [TestCase("03-Function-based Behavior", "3a-Function-based Behavior-2.sysmlx")]
        [TestCase("03-Function-based Behavior", "3a-Function-based Behavior-3.sysmlx")]
        [TestCase("03-Function-based Behavior", "3c-Function-based Behavior-structure mod-1.sysmlx")]
        [TestCase("03-Function-based Behavior", "3c-Function-based Behavior-structure mod-2.sysmlx")]
        [TestCase("03-Function-based Behavior", "3c-Function-based Behavior-structure mod-3.sysmlx")]
        [TestCase("03-Function-based Behavior", "3d-Function-based Behavior-item.sysmlx")]
        [TestCase("03-Function-based Behavior", "3e-Function-based Behavior-item.sysmlx")]
        [TestCase("04-Functional Allocation", "4a-Functional Allocation.sysmlx")]
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

            var readResult = await deSerializer.DeSerializeAsync(new Uri(filePath));
            var rootNamespace = readResult.RootNamespace;

            // The referenced namespaces are the roots of the model libraries pulled in while resolving the
            // file's external references. They form the global Namespace (KerML §8.2.3.5.2), so the writer
            // needs them to shorten a reference routed through a library the model does not itself import.
            using var writerContext = new TextualNotationWriterContext(rootNamespace, readResult.ReferencedNamespaces);
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
            
            var expectedFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Expected", folderName, fileName.Replace(".sysmlx", ".sysml"));

            var expectedContent = await File.ReadAllTextAsync(expectedFilePath);
            Assert.That(expectedContent, Is.EqualTo(textualNotation));
        }
    }
}
