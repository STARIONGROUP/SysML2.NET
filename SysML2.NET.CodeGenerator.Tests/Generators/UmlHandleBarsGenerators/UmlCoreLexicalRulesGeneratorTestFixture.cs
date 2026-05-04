// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreLexicalRulesGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    using System.IO;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators;
    using SysML2.NET.CodeGenerator.Grammar;
    using SysML2.NET.CodeGenerator.Grammar.Model;

    [TestFixture]
    public class UmlCoreLexicalRulesGeneratorTestFixture
    {
        private DirectoryInfo outputDirectoryInfo;
        private UmlCoreLexicalRulesGenerator generator;
        private TextualNotationSpecification textualNotationSpecification;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", "_SysML2.NET.Core.UmlCoreLexicalRulesGenerator");

            this.outputDirectoryInfo = directoryInfo.CreateSubdirectory(path);
            this.generator = new UmlCoreLexicalRulesGenerator();

            var textualRulesFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "datamodel");

            // The lexical support classes (Keywords, SymbolicKeywordKind, SymbolicKeywordKindExtensions)
            // are built from the SysML grammar only — the KerML grammar is intentionally excluded so
            // the emitted set of tokens matches the SysML v2 specification surface.
            this.textualNotationSpecification = GrammarLoader.LoadTextualNotationSpecification(Path.Combine(textualRulesFolder, "SysML-textual-bnf.kebnf"));
        }

        [Test]
        public async Task VerifyCanGenerateLexicalRules()
        {
            await Assert.ThatAsync(() => this.generator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.textualNotationSpecification, this.outputDirectoryInfo), Throws.Nothing);
        }
    }
}
