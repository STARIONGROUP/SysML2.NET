// -------------------------------------------------------------------------------------------------
// <copyright file="InterfaceDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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
                Throws.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("interfaceDefinitionSubject"));

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
