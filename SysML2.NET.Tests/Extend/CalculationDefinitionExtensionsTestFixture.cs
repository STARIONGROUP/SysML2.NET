// ------------------------------------------------------------------------------------------------
// <copyright file="CalculationDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Tests.Extend
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.Calculations;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class CalculationDefinitionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeCalculation()
        {
            Assert.That(() => ((ICalculationDefinition)null).ComputeCalculation(), Throws.TypeOf<ArgumentNullException>());

            var emptyCalculationDefinition = new CalculationDefinition();

            Assert.That(emptyCalculationDefinition.ComputeCalculation(), Has.Count.EqualTo(0));

            // Only CalculationUsage instances must be returned; ActionUsage and bare Usage must be filtered out.
            var subject = new CalculationDefinition();
            var calculationUsage = new CalculationUsage();
            var actionUsage = new ActionUsage();
            var bareUsage = new Usage();

            subject.AssignOwnership(new FeatureMembership(), calculationUsage);
            subject.AssignOwnership(new FeatureMembership(), actionUsage);
            subject.AssignOwnership(new FeatureMembership(), bareUsage);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeCalculation(), Does.Contain(calculationUsage));
                Assert.That(subject.ComputeCalculation(), Does.Not.Contain(actionUsage));
                Assert.That(subject.ComputeCalculation(), Does.Not.Contain(bareUsage));
            }
        }
    }
}
