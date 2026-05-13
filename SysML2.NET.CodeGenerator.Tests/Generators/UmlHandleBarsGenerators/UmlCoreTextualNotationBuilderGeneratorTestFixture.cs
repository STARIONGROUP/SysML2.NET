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

        /// <summary>
        /// Regression for the subtype-overlap guard-synthesis fix. The <c>OwnedExpression</c>
        /// dispatch in <c>BuildOwnedExpression</c> groups seven rules that target
        /// <c>OperatorExpression</c> and pairs them with a sibling alternative
        /// (<c>PrimaryExpression</c>) that targets a SUPERTYPE (<c>Expression</c>). Before the
        /// fix, the would-be-default of the duplicate group (<c>ExtentExpression</c>) was emitted
        /// as a bare <c>case IOperatorExpression pocoOperatorExpression:</c> which greedily
        /// swallowed <c>IFeatureChainExpression</c> (an <c>IOperatorExpression</c> subtype)
        /// before it could reach <c>default → BuildPrimaryExpression</c>. The fix synthesises a
        /// <c>when</c>-clause from the rule's parsed assignments — <c>operator = 'all'</c> and
        /// <c>ownedRelationship += TypeReferenceMember</c> — so the case becomes
        /// <c>case IOperatorExpression … when … .Operator == "all" &amp;&amp; … .Current is IParameterMembership:</c>
        /// and <c>FeatureChainExpression</c> falls through to the correct dispatcher. This test
        /// pins both halves of the synthesised guard so future regressions are caught.
        /// </summary>
        [Test]
        public async Task Verify_that_ExtentExpression_case_carries_synthesised_guard()
        {
            await this.umlCoreTextualNotationBuilderGenerator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.textualNotationSpecification, this.umlPocoDirectoryInfo);

            var generatedExpressionBuilderPath = Path.Combine(this.umlPocoDirectoryInfo.FullName, "ExpressionTextualNotationBuilder.cs");
            Assert.That(File.Exists(generatedExpressionBuilderPath), Is.True, $"Expected generator to emit {generatedExpressionBuilderPath}");

            var generatedSource = await File.ReadAllTextAsync(generatedExpressionBuilderPath);

            Assert.Multiple(() =>
            {
                Assert.That(generatedSource, Does.Not.Contain("case SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression pocoOperatorExpression:" + System.Environment.NewLine + "                    OperatorExpressionTextualNotationBuilder.BuildExtentExpression"),
                    "ExtentExpression's case must not be emitted as the bare unguarded `case IOperatorExpression pocoOperatorExpression:` fall-through — it would swallow IFeatureChainExpression.");
                Assert.That(generatedSource, Does.Contain(".Operator == \"all\""),
                    "Synthesised guard for ExtentExpression must include the parsed scalar literal `operator = 'all'` as `.Operator == \"all\"`.");
                Assert.That(generatedSource, Does.Contain(".Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership"),
                    "Synthesised guard for ExtentExpression must include the parsed `ownedRelationship += TypeReferenceMember` cursor predicate.");
            });
        }
    }
}
