// ------------------------------------------------------------------------------------------------
// <copyright file="PartUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Systems.Items;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class PartUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputePartDefinition()
        {
            // Null subject: guard clause throws ArgumentNullException.
            Assert.That(() => ((IPartUsage)null).ComputePartDefinition(), Throws.TypeOf<ArgumentNullException>());

            var partUsage = new PartUsage();

            // Empty case: no FeatureTyping owned by the subject → empty list.
            Assert.That(partUsage.ComputePartDefinition(), Is.Empty);

            // Discrimination: FeatureTyping targets an ItemDefinition (a Structure, NOT an
            // IPartDefinition) — filtered out → still empty.
            var itemDefinition = new ItemDefinition();
            partUsage.AssignOwnership(new FeatureTyping { Type = itemDefinition });
            Assert.That(partUsage.ComputePartDefinition(), Is.Empty);

            // Populated case: FeatureTypings whose Types are IPartDefinition → all returned,
            // the non-matching ItemDefinition typing stays filtered out.
            var partDefinition = new PartDefinition();
            var secondPartDefinition = new PartDefinition();
            partUsage.AssignOwnership(new FeatureTyping { Type = partDefinition });
            partUsage.AssignOwnership(new FeatureTyping { Type = secondPartDefinition });
            Assert.That(partUsage.ComputePartDefinition(), Is.EqualTo([partDefinition, secondPartDefinition]));
        }
    }
}
