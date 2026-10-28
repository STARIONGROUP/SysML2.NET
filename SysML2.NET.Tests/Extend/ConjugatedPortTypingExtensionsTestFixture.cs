// ------------------------------------------------------------------------------------------------
// <copyright file="ConjugatedPortTypingExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ConjugatedPortTypingExtensionsTestFixture
    {
        [Test]
        public void VerifyComputePortDefinition()
        {
            Assert.That(() => ((IConjugatedPortTyping)null).ComputePortDefinition(), Throws.TypeOf<ArgumentNullException>());

            // No ConjugatedPortDefinition -> [1..1] violation.
            var emptyConjugatedPortTyping = new ConjugatedPortTyping();

            Assert.That(emptyConjugatedPortTyping.ComputePortDefinition, Throws.TypeOf<IncompleteModelException>());

            // A ConjugatedPortDefinition with no ownership chain resolves no originalPortDefinition.
            var unownedConjugatedPortTyping = new ConjugatedPortTyping { ConjugatedPortDefinition = new ConjugatedPortDefinition() };

            Assert.That(unownedConjugatedPortTyping.ComputePortDefinition, Throws.TypeOf<IncompleteModelException>());

            // Positive case: the ConjugatedPortDefinition's originalPortDefinition derives from its
            // owningMembership -> membershipOwningNamespace, which must be a PortDefinition. Wire that
            // ownership chain so originalPortDefinition (and thus portDefinition) resolves to the original.
            var originalPortDefinition = new PortDefinition();
            var conjugatedPortDefinition = new ConjugatedPortDefinition();
            originalPortDefinition.AssignOwnership(new OwningMembership(), conjugatedPortDefinition);

            var conjugatedPortTyping = new ConjugatedPortTyping { ConjugatedPortDefinition = conjugatedPortDefinition };

            Assert.That(conjugatedPortTyping.ComputePortDefinition(), Is.SameAs(originalPortDefinition));
        }
    }
}
