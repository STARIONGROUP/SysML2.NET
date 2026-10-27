// ------------------------------------------------------------------------------------------------
// <copyright file="ActionDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ActionDefinitionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeAction()
        {
            Assert.That(() => ((IActionDefinition)null).ComputeAction(), Throws.TypeOf<ArgumentNullException>());

            var emptyActionDefinition = new ActionDefinition();

            Assert.That(emptyActionDefinition.ComputeAction(), Has.Count.EqualTo(0));

            // Only ActionUsage instances must be returned; a bare Usage must be filtered out.
            var subject = new ActionDefinition();
            var actionUsage = new ActionUsage();
            var bareUsage = new Usage();

            subject.AssignOwnership(new FeatureMembership(), actionUsage);
            subject.AssignOwnership(new FeatureMembership(), bareUsage);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeAction(), Does.Contain(actionUsage));
                Assert.That(subject.ComputeAction(), Does.Not.Contain(bareUsage));
            }
        }
    }
}
