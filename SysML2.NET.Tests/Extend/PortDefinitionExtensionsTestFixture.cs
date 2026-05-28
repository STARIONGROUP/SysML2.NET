// -------------------------------------------------------------------------------------------------
// <copyright file="PortDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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
