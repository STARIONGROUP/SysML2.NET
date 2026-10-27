// ------------------------------------------------------------------------------------------------
// <copyright file="InterfaceDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Attributes;
    using SysML2.NET.Core.POCO.Systems.Interfaces;
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class InterfaceDefinitionExtensionsTestFixture
    {
        [Test]
        public void Verify_ComputeInterfaceEnd()
        {
            Assert.That(
                () => ((IInterfaceDefinition)null).ComputeInterfaceEnd(),
                Throws.TypeOf<ArgumentNullException>());

            var emptyInterfaceDefinition = new InterfaceDefinition();

            Assert.That(emptyInterfaceDefinition.ComputeInterfaceEnd(), Is.Empty);

            var singleEndInterfaceDefinition = new InterfaceDefinition();
            var singleEndPort = new PortUsage { IsEnd = true };
            singleEndInterfaceDefinition.AssignOwnership(new FeatureMembership(), singleEndPort);

            Assert.That(singleEndInterfaceDefinition.ComputeInterfaceEnd(), Is.EqualTo([singleEndPort]));

            var mixedIsEndInterfaceDefinition = new InterfaceDefinition();
            var nonEndPort = new PortUsage { IsEnd = false };
            var endPort = new PortUsage { IsEnd = true };
            mixedIsEndInterfaceDefinition.AssignOwnership(new FeatureMembership(), nonEndPort);
            mixedIsEndInterfaceDefinition.AssignOwnership(new FeatureMembership(), endPort);

            Assert.That(mixedIsEndInterfaceDefinition.ComputeInterfaceEnd(), Is.EqualTo([endPort]));

            var typeFilteringInterfaceDefinition = new InterfaceDefinition();
            var endPortUsage = new PortUsage { IsEnd = true };
            var endAttributeUsage = new AttributeUsage { IsEnd = true };
            typeFilteringInterfaceDefinition.AssignOwnership(new FeatureMembership(), endPortUsage);
            typeFilteringInterfaceDefinition.AssignOwnership(new FeatureMembership(), endAttributeUsage);

            Assert.That(typeFilteringInterfaceDefinition.ComputeInterfaceEnd(), Is.EqualTo([endPortUsage]));
        }
    }
}
