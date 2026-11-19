// ------------------------------------------------------------------------------------------------
// <copyright file="PortConjugationExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Systems.Ports;

    [TestFixture]
    public class PortConjugationExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeConjugatedPortDefinition()
        {
            // Null subject → ArgumentNullException.
            Assert.That(() => ((IPortConjugation)null).ComputeConjugatedPortDefinition(), Throws.TypeOf<ArgumentNullException>());

            var emptyPortConj = new PortConjugation();

            Assert.That(emptyPortConj.ComputeConjugatedPortDefinition(), Is.Null);

            // OwningRelatedElement is an IConjugatedPortDefinition → returns the same instance.
            var conjugated = new ConjugatedPortDefinition();
            var portConjWithConjugated = new PortConjugation();

            ((IContainedRelationship)portConjWithConjugated).OwningRelatedElement = conjugated;

            Assert.That(portConjWithConjugated.ComputeConjugatedPortDefinition(), Is.SameAs(conjugated));

            var portDefinition = new PortDefinition();
            var portConjWithPortDefinition = new PortConjugation();

            ((IContainedRelationship)portConjWithPortDefinition).OwningRelatedElement = portDefinition;

            Assert.That(portConjWithPortDefinition.ComputeConjugatedPortDefinition(), Is.Null);
        }
    }
}
