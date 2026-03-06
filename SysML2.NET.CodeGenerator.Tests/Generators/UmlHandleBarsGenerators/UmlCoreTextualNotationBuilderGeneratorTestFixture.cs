// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreTextualNotationBuilderGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    using System.Linq;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators;
    using SysML2.NET.CodeGenerator.Grammar;
    using SysML2.NET.CodeGenerator.Grammar.Model;

    [TestFixture]
    public class UmlCoreTextualNotationBuilderGeneratorTestFixture
    {
        private DirectoryInfo umlPocoDirectoryInfo;
        private UmlCoreTextualNotationBuilderGenerator umlCoreTextualNotationBuilderGenerator;
        private TextualNotationSpecification textualNotationSpecification;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", "_SysML2.NET.Core.UmlCoreTextualNotationBuilderGenerator");

            this.umlPocoDirectoryInfo = directoryInfo.CreateSubdirectory(path);
            this.umlCoreTextualNotationBuilderGenerator = new UmlCoreTextualNotationBuilderGenerator();
            
            var textualRulesFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "datamodel");
            var kermlRules = GrammarLoader.LoadTextualNotationSpecification(Path.Combine(textualRulesFolder, "KerML-textual-bnf.kebnf"));
            var sysmlRules = GrammarLoader.LoadTextualNotationSpecification(Path.Combine(textualRulesFolder, "SysML-textual-bnf.kebnf"));

            var combinesRules = new TextualNotationSpecification();
            combinesRules.Rules.AddRange(sysmlRules.Rules);

            foreach (var rule in kermlRules.Rules.Where(rule => combinesRules.Rules.All(r => r.RuleName != rule.RuleName)))
            {
                combinesRules.Rules.Add(rule);
            }
            
            this.textualNotationSpecification = combinesRules;
        }

        [Test]
        public async Task VerifyCanGenerateTextualNotation()
        {
            await Assert.ThatAsync(() => this.umlCoreTextualNotationBuilderGenerator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.textualNotationSpecification, this.umlPocoDirectoryInfo), Throws.Nothing);
        }
    }
}
