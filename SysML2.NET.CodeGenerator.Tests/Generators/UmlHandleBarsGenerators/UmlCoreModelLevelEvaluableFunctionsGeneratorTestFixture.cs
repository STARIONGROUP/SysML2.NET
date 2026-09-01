// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreModelLevelEvaluableFunctionsGeneratorTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Tests.Generators.UmlHandleBarsGenerators
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators;
    using SysML2.NET.CodeGenerator.Grammar;
    using SysML2.NET.CodeGenerator.Grammar.Model;

    [TestFixture]
    public partial class UmlCoreModelLevelEvaluableFunctionsGeneratorTestFixture
    {
        private DirectoryInfo outputDirectoryInfo;
        private UmlCoreModelLevelEvaluableFunctionsGenerator generator;
        private TextualNotationSpecification textualNotationSpecification;
        private TextualNotationSpecification driftedTextualNotationSpecification;
        private string kernelFunctionLibraryPath;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", "_SysML2.NET.Core.UmlCoreModelLevelEvaluableFunctionsGenerator");

            this.outputDirectoryInfo = directoryInfo.CreateSubdirectory(path);
            this.generator = new UmlCoreModelLevelEvaluableFunctionsGenerator();

            var dataModelFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "datamodel");

            // The operator token rules (BinaryOperator, UnaryOperator, ClassificationTestOperator, …) are
            // declared by the KerML grammar only — the SysML grammar does not restate them.
            this.textualNotationSpecification = GrammarLoader.LoadTextualNotationSpecification(Path.Combine(dataModelFolder, "KerML-textual-bnf.kebnf"));

            this.kernelFunctionLibraryPath = Path.Combine(dataModelFolder, "Kernel Function Library");

            // The SysML grammar restates none of the operator token rules, so it stands in for a drifted
            // grammar: only the four operators defaulted by the UML model survive collection.
            this.driftedTextualNotationSpecification = GrammarLoader.LoadTextualNotationSpecification(Path.Combine(dataModelFolder, "SysML-textual-bnf.kebnf"));
        }

        [Test]
        public async Task VerifyGenerateAsync()
        {
            Assert.That(() => this.generator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.outputDirectoryInfo), Throws.TypeOf<NotSupportedException>());

            await Assert.ThatAsync(() => this.generator.GenerateAsync(null, this.textualNotationSpecification, this.kernelFunctionLibraryPath, this.outputDirectoryInfo), Throws.TypeOf<ArgumentNullException>());

            // A grammar that no longer carries the operator token rules must fail generation, not emit a
            // silently truncated set.
            await Assert.ThatAsync(() => this.generator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.driftedTextualNotationSpecification, this.kernelFunctionLibraryPath, this.outputDirectoryInfo), Throws.TypeOf<InvalidOperationException>());

            // A Kernel Function Library that cannot be read must fail generation too.
            await Assert.ThatAsync(() => this.generator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.textualNotationSpecification, Path.Combine(this.outputDirectoryInfo.FullName, "absent"), this.outputDirectoryInfo), Throws.TypeOf<DirectoryNotFoundException>());

            await Assert.ThatAsync(() => this.generator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.textualNotationSpecification, this.kernelFunctionLibraryPath, this.outputDirectoryInfo), Throws.Nothing);

            var generated = await File.ReadAllTextAsync(Path.Combine(this.outputDirectoryInfo.FullName, "ModelLevelEvaluableFunctions.cs"));

            // 39 operator symbols per KerML Table 5 and Table 7, less the 3 rows marked "No".
            Assert.That(FunctionRegex().Matches(generated), Has.Count.EqualTo(36));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(generated, Does.Contain("\"BaseFunctions::==\""));
                Assert.That(generated, Does.Contain("\"BaseFunctions::as\""));
                Assert.That(generated, Does.Contain("\"BaseFunctions::#\""));
                Assert.That(generated, Does.Contain("\"ControlFunctions::select\""));
                Assert.That(generated, Does.Contain("\"ControlFunctions::.\""));
                Assert.That(generated, Does.Contain("\"DataFunctions::^\""));
                Assert.That(generated, Does.Contain("\"DataFunctions::**\""));
            }

            // '==' and '===' are declared by BaseFunctions AND DataFunctions; the probe order elects BaseFunctions.
            using (Assert.EnterMultipleScope())
            {
                Assert.That(generated, Does.Not.Contain("\"DataFunctions::==\""));
                Assert.That(generated, Does.Not.Contain("\"DataFunctions::===\""));
            }

            using (Assert.EnterMultipleScope())
            {
                Assert.That(generated, Does.Not.Contain("\"BaseFunctions::all\""));
                Assert.That(generated, Does.Not.Contain("\"BaseFunctions::[\""));
                Assert.That(generated, Does.Not.Contain("\"DataFunctions::~\""));
                Assert.That(generated, Does.Not.Contain("\"DataFunctions::max\""));
                Assert.That(generated, Does.Not.Contain("\"ControlFunctions::reduce\""));
            }
        }

        [GeneratedRegex("^\\s*\"[^\"]*::[^\"]*\",?$", RegexOptions.Multiline)]
        private static partial Regex FunctionRegex();
    }
}
