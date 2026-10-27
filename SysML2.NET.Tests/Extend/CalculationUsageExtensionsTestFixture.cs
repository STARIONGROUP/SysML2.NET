// ------------------------------------------------------------------------------------------------
// <copyright file="CalculationUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Tests.Extend
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.Calculations;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class CalculationUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeCalculationDefinition()
        {
            Assert.That(() => ((ICalculationUsage)null).ComputeCalculationDefinition(), Throws.TypeOf<ArgumentNullException>());

            var calculationUsage = new CalculationUsage();

            // No FeatureTyping entries → no IFunction type → null (property is [0..1]).
            Assert.That(calculationUsage.ComputeCalculationDefinition(), Is.Null);

            // Discrimination: FeatureTyping targets an ActionDefinition (a Behavior, NOT an
            // IFunction) — filtered out → still null.
            var actionDefinition = new ActionDefinition();
            calculationUsage.AssignOwnership(new FeatureTyping { Type = actionDefinition });
            Assert.That(calculationUsage.ComputeCalculationDefinition(), Is.Null);

            // Populated case: FeatureTyping whose Type is an IFunction (CalculationDefinition) → returned.
            var calculationDefinition = new CalculationDefinition();
            calculationUsage.AssignOwnership(new FeatureTyping { Type = calculationDefinition });
            Assert.That(calculationUsage.ComputeCalculationDefinition(), Is.SameAs(calculationDefinition));

            // [0..1] upper-bound violation: two matching typings → MultiplicityViolationException.
            var secondCalculationDefinition = new CalculationDefinition();
            calculationUsage.AssignOwnership(new FeatureTyping { Type = secondCalculationDefinition });
            Assert.That(() => calculationUsage.ComputeCalculationDefinition(), Throws.TypeOf<MultiplicityViolationException>());
        }

        [Test]
        public void VerifyComputeRedefinedModelLevelEvaluableOperation()
        {
            Assert.That(
                () => ((ICalculationUsage)null).ComputeRedefinedModelLevelEvaluableOperation(null),
                Throws.TypeOf<ArgumentNullException>());

            var subject = new CalculationUsage();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeRedefinedModelLevelEvaluableOperation(null), Is.False);
                Assert.That(subject.ComputeRedefinedModelLevelEvaluableOperation(new List<IFeature>()), Is.False);
            }
        }
    }
}
