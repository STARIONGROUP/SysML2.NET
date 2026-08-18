// -------------------------------------------------------------------------------------------------
// <copyright file="ImpliedRelationshipExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Extensions;

    [TestFixture]
    public class ImpliedRelationshipExtensionsTestFixture
    {
        [Test]
        public void VerifyQueryImpliedRelationshipRules()
        {
            var rules = GeneratorSetupFixture.XmiReaderResult.QueryImpliedRelationshipRules();

            using (Assert.EnterMultipleScope())
            {
                // Only `check` rules are semantic constraints. `derive` rules are derivations and `validate`
                // rules are validation constraints (KerML §8.3.1); neither implies a Relationship, and both
                // carry the same category keywords in their names, so they must not leak in.
                Assert.That(rules.Select(rule => rule.ConstraintName), Is.All.StartWith("check"));

                // Category totals, cross-checked against an independent scan of the raw XMI.
                Assert.That(rules.Count(rule => rule.Category == ImpliedConstraintCategory.Specialization), Is.EqualTo(175));
                Assert.That(rules.Count(rule => rule.Category == ImpliedConstraintCategory.Redefinition), Is.EqualTo(15));
                Assert.That(rules.Count(rule => rule.Category == ImpliedConstraintCategory.TypeFeaturing), Is.EqualTo(7));
                Assert.That(rules.Count(rule => rule.Category == ImpliedConstraintCategory.BindingConnector), Is.EqualTo(11));

                // Form split — this is what decides how much is generated versus hand-written.
                Assert.That(rules.Count(rule => rule.Form == ImpliedRuleForm.UnconditionalLibrarySpecialization), Is.EqualTo(85));
                Assert.That(rules.Count(rule => rule.Form == ImpliedRuleForm.GuardedLibrarySpecialization), Is.EqualTo(63));
                Assert.That(rules.Count(rule => rule.Form == ImpliedRuleForm.SpecificationTbd), Is.EqualTo(2));
            }

            // A representative unconditional rule: the whole body is the library target.
            var portUsage = rules.Single(rule => rule.ConstraintName == "checkPortUsageSpecialization");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(portUsage.MetaclassName, Is.EqualTo("PortUsage"));
                Assert.That(portUsage.Form, Is.EqualTo(ImpliedRuleForm.UnconditionalLibrarySpecialization));
                Assert.That(portUsage.TargetLibraryName, Is.EqualTo("Ports::ports"));
                Assert.That(portUsage.GuardExpression, Is.Null);
            }

            // A representative guarded rule: the target is still extracted mechanically, and the guard is
            // captured verbatim for a hand-written predicate.
            var subport = rules.Single(rule => rule.ConstraintName == "checkPortUsageSubportSpecialization");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subport.Form, Is.EqualTo(ImpliedRuleForm.GuardedLibrarySpecialization));
                Assert.That(subport.TargetLibraryName, Is.EqualTo("Ports::Port::subports"));
                Assert.That(subport.GuardExpression, Is.Not.Empty);
                Assert.That(subport.GuardExpression, Does.Not.Contain("specializesFromLibrary"));
            }

            // Categories 2-4 relate user-model elements, so they never carry a library target.
            Assert.That(
                rules.Where(rule => rule.Category != ImpliedConstraintCategory.Specialization).Select(rule => rule.TargetLibraryName),
                Is.All.Null);

            // Every rule carries the OCL it was classified from, so a hand-coded arm can be checked
            // against the source of truth without re-reading the XMI.
            Assert.That(rules.Select(rule => rule.Ocl), Is.All.Not.Null);
        }
    }
}
