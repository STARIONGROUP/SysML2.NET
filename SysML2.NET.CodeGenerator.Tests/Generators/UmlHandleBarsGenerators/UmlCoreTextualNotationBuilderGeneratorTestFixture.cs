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
    using System;
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

        /// <summary>
        /// Regression for the selective nested-alternation flattening fix. A metaclass reachable only
        /// THROUGH a nested alternation rule used to be captured by an earlier arm typed on one of its
        /// supertypes, because the depth sort ranks a nested-rule arm by the RULE's declared target
        /// (<c>BehaviorUsageElement : Usage</c>, shallow) rather than by the deepest metaclass reachable
        /// through it (<c>PerformActionUsage</c>, deep) — and forces that arm last as <c>default:</c> when
        /// the target IS the generating class. So <c>BuildVariantUsageElement</c> matched every
        /// <c>IPerformActionUsage</c> against <c>case IEventOccurrenceUsage</c> (a supertype per
        /// <c>IPerformActionUsage : IActionUsage, IEventOccurrenceUsage</c>) and emitted
        /// <c>variant event doX;</c> instead of <c>variant perform doX;</c>. The fix hoists one
        /// <c>case</c> arm per genuinely-shadowed class to the top of the switch, delegating to the nested
        /// rule's builder. This test pins both known instances — <c>VariantUsageElement</c> and
        /// <c>OwnedRelatedElement</c> (where <c>IMultiplicity : IFeature</c> was swallowed by
        /// <c>case IFeature</c> before reaching <c>NonFeatureElement</c>).
        /// </summary>
        [Test]
        public async Task Verify_that_shadowed_nested_alternation_targets_are_hoisted()
        {
            await this.umlCoreTextualNotationBuilderGenerator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.textualNotationSpecification, this.umlPocoDirectoryInfo);

            var generatedUsageBuilderPath = Path.Combine(this.umlPocoDirectoryInfo.FullName, "UsageTextualNotationBuilder.cs");
            var generatedElementBuilderPath = Path.Combine(this.umlPocoDirectoryInfo.FullName, "ElementTextualNotationBuilder.cs");

            Assert.That(File.Exists(generatedUsageBuilderPath), Is.True, $"Expected generator to emit {generatedUsageBuilderPath}");
            Assert.That(File.Exists(generatedElementBuilderPath), Is.True, $"Expected generator to emit {generatedElementBuilderPath}");

            var buildVariantUsageElement = ExtractMethodBody(await File.ReadAllTextAsync(generatedUsageBuilderPath), "BuildVariantUsageElement");
            var buildOwnedRelatedElement = ExtractMethodBody(await File.ReadAllTextAsync(generatedElementBuilderPath), "BuildOwnedRelatedElement");

            var performActionArmIndex = buildVariantUsageElement.IndexOf("case SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage pocoPerformActionUsage:", StringComparison.Ordinal);
            var eventOccurrenceArmIndex = buildVariantUsageElement.IndexOf("case SysML2.NET.Core.POCO.Systems.Occurrences.IEventOccurrenceUsage pocoEventOccurrenceUsage:", StringComparison.Ordinal);
            var multiplicityArmIndex = buildOwnedRelatedElement.IndexOf("case SysML2.NET.Core.POCO.Core.Types.IMultiplicity pocoMultiplicity:", StringComparison.Ordinal);
            var featureArmIndex = buildOwnedRelatedElement.IndexOf("case SysML2.NET.Core.POCO.Core.Features.IFeature pocoFeature:", StringComparison.Ordinal);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(performActionArmIndex, Is.GreaterThanOrEqualTo(0),
                    "BuildVariantUsageElement must hoist an IPerformActionUsage arm — PerformActionUsage is only reachable through the nested BehaviorUsageElement rule.");
                Assert.That(performActionArmIndex, Is.LessThan(eventOccurrenceArmIndex),
                    "The hoisted IPerformActionUsage arm must precede case IEventOccurrenceUsage, otherwise the supertype arm swallows it and `variant perform` renders as `variant event`.");
                Assert.That(buildVariantUsageElement, Does.Contain("BuildBehaviorUsageElement(pocoPerformActionUsage, writerContext, stringBuilder);"),
                    "The hoisted arm must delegate to the nested rule's own builder so no builder is bypassed.");
                Assert.That(buildVariantUsageElement, Does.Contain("case SysML2.NET.Core.POCO.Systems.States.IExhibitStateUsage pocoExhibitStateUsage:"),
                    "ExhibitStateUsage (IExhibitStateUsage : IStateUsage, IPerformActionUsage) is shadowed by the same IEventOccurrenceUsage arm and must be hoisted too.");
                Assert.That(buildVariantUsageElement, Does.Contain("case SysML2.NET.Core.POCO.Systems.UseCases.IIncludeUseCaseUsage pocoIncludeUseCaseUsage:"),
                    "IncludeUseCaseUsage (IIncludeUseCaseUsage : IUseCaseUsage, IPerformActionUsage) is shadowed by the same IEventOccurrenceUsage arm and must be hoisted too.");
                Assert.That(multiplicityArmIndex, Is.GreaterThanOrEqualTo(0),
                    "BuildOwnedRelatedElement must hoist an IMultiplicity arm — Multiplicity is only reachable through the nested NonFeatureElement rule.");
                Assert.That(multiplicityArmIndex, Is.LessThan(featureArmIndex),
                    "The hoisted IMultiplicity arm must precede case IFeature — Multiplicity is a NonFeatureElement alternative but IMultiplicity : IFeature.");
                Assert.That(buildOwnedRelatedElement, Does.Contain("BuildNonFeatureElement(pocoMultiplicity, writerContext, stringBuilder);"),
                    "The hoisted IMultiplicity arm must delegate to BuildNonFeatureElement, the rule that lists Multiplicity as an alternative.");
            }
        }

        /// <summary>
        /// Extracts the source of a single generated builder method, so arm-ordering assertions anchor on
        /// the method under test rather than on the first file-wide match of a case label.
        /// </summary>
        /// <param name="generatedSource">The full generated builder source</param>
        /// <param name="methodName">The name of the <c>public static void Build…</c> method to extract</param>
        /// <returns>The method's source, up to the start of the next method</returns>
        private static string ExtractMethodBody(string generatedSource, string methodName)
        {
            var methodStartIndex = generatedSource.IndexOf($"public static void {methodName}(", StringComparison.Ordinal);

            Assert.That(methodStartIndex, Is.GreaterThanOrEqualTo(0), $"Expected the generated source to declare {methodName}");

            var nextMethodIndex = generatedSource.IndexOf("public static void ", methodStartIndex + 1, StringComparison.Ordinal);

            return nextMethodIndex < 0
                ? generatedSource[methodStartIndex..]
                : generatedSource[methodStartIndex..nextMethodIndex];
        }
    }
}
