// -------------------------------------------------------------------------------------------------
// <copyright file="PortConjugationExtensionsTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
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
