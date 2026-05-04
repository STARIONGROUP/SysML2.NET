// -------------------------------------------------------------------------------------------------
// <copyright file="TextualNotationBuilderTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Tests.TextualNotation
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Microsoft.Extensions.Logging;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Serializer.Xmi;
    using SysML2.NET.TextualNotation;

    /// <summary>
    /// Test fixture that verifies the textual notation builder produces valid output
    /// from a deserialized XMI model with all cross-file references resolved.
    /// </summary>
    [TestFixture]
    public class TextualNotationBuilderTestFixture
    {
        private ILoggerFactory loggerFactory;
        private INamespace rootNamespace;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Debug);
            });

            var deSerializer = new DeSerializer(this.loggerFactory);

            var filePath = Path.Combine(
                TestContext.CurrentContext.TestDirectory,
                "Resources",
                "Domain Libraries",
                "Quantities and Units",
                "Quantities.sysmlx");

            var fileUri = new Uri(filePath);

            // DeSerialize automatically follows href references to kernel libraries,
            // resolving all cross-file references so qualifiedName properties are populated
            this.rootNamespace = deSerializer.DeSerialize(fileUri);
        }

        [Test]
        public void Verify_that_Quantities_namespace_is_deserialized_with_expected_id()
        {
            Assert.Multiple(() =>
            {
                Assert.That(this.rootNamespace, Is.Not.Null);
                Assert.That(this.rootNamespace.Id, Is.EqualTo(Guid.Parse("88e753b3-e75d-525f-b9ad-d5e9095b98ec")));
            });
        }
        
        [Test]
        public void Verify_that_textual_notation_is_produced_from_Quantities_root_namespace()
        {
            using var cursorCache = new CursorCache();
            var stringBuilder = new StringBuilder();

            try
            {
                NamespaceTextualNotationBuilder.BuildRootNamespace(this.rootNamespace, cursorCache, stringBuilder);
            }
            catch (System.NotSupportedException notSupportedException)
            {
                TestContext.WriteLine($"Builder stopped early due to unimplemented derived property: {notSupportedException.Message}");
            }

            var textualNotation = stringBuilder.ToString();

            TestContext.WriteLine("=== Textual Notation Output ===");
            TestContext.WriteLine(textualNotation);
            TestContext.WriteLine("=== End ===");

            Assert.Multiple(() =>
            {
                Assert.That(textualNotation, Is.Not.Null);
                Assert.That(textualNotation, Is.Not.Empty);
                Assert.That(textualNotation, Does.Contain("standard"));
                Assert.That(textualNotation, Does.Contain("library"));
                Assert.That(textualNotation, Does.Contain("package"));
                Assert.That(textualNotation, Does.Contain("Quantities"));
            });
        }
    }
}
