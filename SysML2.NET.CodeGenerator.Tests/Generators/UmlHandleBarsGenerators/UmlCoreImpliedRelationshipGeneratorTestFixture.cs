// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreImpliedRelationshipGeneratorTestFixture.cs" company="Starion Group S.A.">
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

    [TestFixture]
    public class UmlCoreImpliedRelationshipGeneratorTestFixture
    {
        private DirectoryInfo outputDirectory;
        private UmlCoreImpliedRelationshipGenerator generator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", "_SysML2.NET.Semantics.AutoGenImplied");

            this.outputDirectory = directoryInfo.CreateSubdirectory(path);
            this.generator = new UmlCoreImpliedRelationshipGenerator();
        }

        [Test]
        public async Task VerifyImpliedRelationshipTableIsGenerated()
        {
            await Assert.ThatAsync(
                () => this.generator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.outputDirectory),
                Throws.Nothing);
        }

        [Test]
        public async Task VerifyGeneratedTableCarriesTheExtractedRules()
        {
            var generatedCode = await this.generator.GenerateImpliedRelationshipTable(
                GeneratorSetupFixture.XmiReaderResult,
                this.outputDirectory);

            using (Assert.EnterMultipleScope())
            {
                // An unconditional rule declared on the metaclass itself.
                Assert.That(generatedCode, Does.Contain(@"new(""checkPortUsageSpecialization"", ""Ports::ports"", ""PortUsage"", false)"));

                // A guarded rule keeps its target but is flagged so the caller consults the hand-written
                // predicate before applying it.
                Assert.That(generatedCode, Does.Contain(@"new(""checkPortUsageSubportSpecialization"", ""Ports::Port::subports"", ""PortUsage"", true)"));

                // Constraints are flattened DOWN the metaclass hierarchy: PartUsage declares none of these,
                // it inherits them, and the generated arm must still carry them.
                Assert.That(generatedCode, Does.Contain("IPartUsageRules").Or.Contain("PartUsageRules"));
                Assert.That(generatedCode, Does.Contain(@"""checkFeatureSpecialization"", ""Base::things"", ""Feature"""));

                // The manifest must account for every constraint that could not be generated, including the
                // two whose specification body the OMG left as TBD.
                Assert.That(generatedCode, Does.Contain("checkInvocationExpressionDefaultValueBindingConnector"));
                Assert.That(generatedCode, Does.Contain("specification body is TBD"));

                // The hand-maintained half of the table — the part the OCL cannot supply — must survive
                // into the generated file.
                Assert.That(generatedCode, Does.Contain("SubclassificationMetaclasses"));
                Assert.That(generatedCode, Does.Contain(@"""PartDefinition"""));
            }
        }
    }
}
