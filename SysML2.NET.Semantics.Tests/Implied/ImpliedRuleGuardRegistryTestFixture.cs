// -------------------------------------------------------------------------------------------------
// <copyright file="ImpliedRuleGuardRegistryTestFixture.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Tests.Implied
{
    using System;
    using System.Linq;

    using Microsoft.Extensions.DependencyInjection;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Semantics.Extensions;
    using SysML2.NET.Semantics.Implied;

    [TestFixture]
    public class ImpliedRuleGuardRegistryTestFixture
    {
        [Test]
        public void VerifyGetGuard()
        {
            var guard = new StubGuard("checkPortUsageSubportSpecialization", true);
            var registry = new ImpliedRuleGuardRegistry([guard]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(registry.GetGuard("checkPortUsageSubportSpecialization"), Is.SameAs(guard));
                Assert.That(() => registry.GetGuard("checkAbsentConstraint"), Throws.TypeOf<MissingImpliedRuleGuardException>());
                Assert.That(() => registry.GetGuard(null), Throws.TypeOf<ArgumentNullException>());
            }
        }

        [Test]
        public void VerifyHasGuard()
        {
            var registry = new ImpliedRuleGuardRegistry([new StubGuard("checkPortUsageSubportSpecialization", true)]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(registry.HasGuard("checkPortUsageSubportSpecialization"), Is.True);
                Assert.That(registry.HasGuard("checkAbsentConstraint"), Is.False);
                Assert.That(registry.HasGuard(null), Is.False);
            }
        }

        [Test]
        public void VerifyConstructor()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => new ImpliedRuleGuardRegistry(null), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => new ImpliedRuleGuardRegistry([]), Throws.Nothing);

                // Two guards deciding the same constraint is a wiring error, not a last-one-wins.
                Assert.That(
                    () => new ImpliedRuleGuardRegistry([new StubGuard("checkDuplicate", true), new StubGuard("checkDuplicate", false)]),
                    Throws.TypeOf<ArgumentException>());
            }
        }

        /// <summary>
        /// Phase-2 exit criterion: every conditional row in the generated table must have a guard, whether
        /// generated from its OCL or hand written. A row without one makes the provider throw rather than
        /// silently apply the constraint unconditionally.
        /// </summary>
        [Test]
        public void VerifyEveryConditionalConstraintHasAGuard()
        {
            using var serviceProvider = new ServiceCollection().AddSysML2Semantics().BuildServiceProvider();

            // Construction throws on two guards deciding one constraint, so this also proves discovery
            // does not re-register the generated guards.
            var registry = new ImpliedRuleGuardRegistry(serviceProvider.GetServices<IImpliedRuleGuard>());

            var unguarded = ImpliedRelationshipTable.AllConditionalConstraintNames
                .Where(constraintName => !registry.HasGuard(constraintName))
                .ToList();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(unguarded, Is.Empty, $"These conditional constraints have no guard: {string.Join(", ", unguarded)}");
                Assert.That(registry.HasGuard("checkAbsentConstraint"), Is.False);
            }
        }

        private sealed class StubGuard : IImpliedRuleGuard
        {
            private readonly bool applies;

            public StubGuard(string constraintName, bool applies)
            {
                this.ConstraintName = constraintName;
                this.applies = applies;
            }

            public string ConstraintName { get; }

            public bool Applies(IElement element) => this.applies;
        }
    }
}
