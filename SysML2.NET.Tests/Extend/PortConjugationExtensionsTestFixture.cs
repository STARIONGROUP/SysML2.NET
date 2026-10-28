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
    using SysML2.NET.Exceptions;

    [TestFixture]
    public class PortConjugationExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeConjugatedPortDefinition()
        {
            // Null subject → ArgumentNullException.
            Assert.That(() => ((IPortConjugation)null).ComputeConjugatedPortDefinition(), Throws.TypeOf<ArgumentNullException>());

            // Empty PortConjugation (OwningRelatedElement is null) → [1..1] violation: IncompleteModelException.
            var emptyPortConj = new PortConjugation();

            Assert.That(() => emptyPortConj.ComputeConjugatedPortDefinition(), Throws.TypeOf<IncompleteModelException>());

            // OwningRelatedElement is an IConjugatedPortDefinition → returns the same instance.
            var conjugated = new ConjugatedPortDefinition();
            var portConjWithConjugated = new PortConjugation();

            ((IContainedRelationship)portConjWithConjugated).OwningRelatedElement = conjugated;

            Assert.That(portConjWithConjugated.ComputeConjugatedPortDefinition(), Is.SameAs(conjugated));

            // OwningRelatedElement is a PortDefinition (IPortDefinition but NOT IConjugatedPortDefinition) → [1..1] type violation: IncompleteModelException.
            var portDefinition = new PortDefinition();
            var portConjWithPortDefinition = new PortConjugation();

            ((IContainedRelationship)portConjWithPortDefinition).OwningRelatedElement = portDefinition;

            Assert.That(() => portConjWithPortDefinition.ComputeConjugatedPortDefinition(), Throws.TypeOf<IncompleteModelException>());
        }
    }
}
