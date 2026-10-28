// ------------------------------------------------------------------------------------------------
// <copyright file="PortDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class PortDefinitionExtensionsTestFixture
    {
        [Test]
        public void Verify_ComputeConjugatedPortDefinition()
        {
            Assert.That(
                () => ((IPortDefinition)null).ComputeConjugatedPortDefinition(),
                Throws.TypeOf<ArgumentNullException>());

            var emptyPortDefinition = new PortDefinition();
            Assert.That(emptyPortDefinition.ComputeConjugatedPortDefinition(), Is.Null);

            var portDefinitionWithConjugated = new PortDefinition();
            var conjugated = new ConjugatedPortDefinition();
            portDefinitionWithConjugated.AssignOwnership(new OwningMembership(), conjugated);
            Assert.That(portDefinitionWithConjugated.ComputeConjugatedPortDefinition(), Is.SameAs(conjugated));

            var portDefinitionWithOtherMember = new PortDefinition();
            var partUsage = new PartUsage();
            portDefinitionWithOtherMember.AssignOwnership(new OwningMembership(), partUsage);
            Assert.That(portDefinitionWithOtherMember.ComputeConjugatedPortDefinition(), Is.Null);
        }
    }
}
