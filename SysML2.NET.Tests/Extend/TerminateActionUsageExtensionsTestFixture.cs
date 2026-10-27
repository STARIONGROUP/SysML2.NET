// ------------------------------------------------------------------------------------------------
// <copyright file="TerminateActionUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class TerminateActionUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeTerminatedOccurrenceArgument()
        {
            Assert.That(() => ((ITerminateActionUsage)null).ComputeTerminatedOccurrenceArgument(), Throws.TypeOf<ArgumentNullException>());

            // No input parameters -> argument(1) is null -> null.
            Assert.That(new TerminateActionUsage().ComputeTerminatedOccurrenceArgument(), Is.Null);

            // A single owned input parameter carrying a FeatureValue -> argument(1) resolves to its value.
            var terminateActionUsage = new TerminateActionUsage();
            var inputParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            terminateActionUsage.AssignOwnership(new FeatureMembership(), inputParameter);

            var terminatedOccurrenceExpression = new LiteralInteger();
            inputParameter.AssignOwnership(new FeatureValue(), terminatedOccurrenceExpression);

            // terminatedOccurrenceArgument = argument(1)
            Assert.That(terminateActionUsage.ComputeTerminatedOccurrenceArgument(), Is.SameAs(terminatedOccurrenceExpression));
        }
    }
}
