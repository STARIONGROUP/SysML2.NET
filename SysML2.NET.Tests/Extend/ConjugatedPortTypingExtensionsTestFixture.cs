// -------------------------------------------------------------------------------------------------
// <copyright file="ConjugatedPortTypingExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Tests.Extend
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ConjugatedPortTypingExtensionsTestFixture
    {
        [Test]
        public void VerifyComputePortDefinition()
        {
            Assert.That(() => ((IConjugatedPortTyping)null).ComputePortDefinition(), Throws.TypeOf<ArgumentNullException>());

            // No ConjugatedPortDefinition -> the null-safe navigation short-circuits to null.
            var emptyConjugatedPortTyping = new ConjugatedPortTyping();

            Assert.That(emptyConjugatedPortTyping.ComputePortDefinition(), Is.Null);

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
