// -------------------------------------------------------------------------------------------------
// <copyright file="InterfaceUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Systems.Connections;
    using SysML2.NET.Core.POCO.Systems.Interfaces;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class InterfaceUsageExtensionsTestFixture
    {
        [Test]
        public void Verify_ComputeInterfaceDefinition()
        {
            Assert.That(() => ((IInterfaceUsage)null).ComputeInterfaceDefinition(), Throws.TypeOf<ArgumentNullException>());

            // Empty InterfaceUsage: no FeatureTyping in OwnedRelationship -> empty list.
            var emptyInterfaceUsage = new InterfaceUsage();

            Assert.That(emptyInterfaceUsage.ComputeInterfaceDefinition(), Is.Empty);

            // Single FeatureTyping typed by an InterfaceDefinition -> single-element list.
            var singleInterfaceUsage = new InterfaceUsage();
            var soleInterfaceDefinition = new InterfaceDefinition();
            singleInterfaceUsage.AssignOwnership(new FeatureTyping { Type = soleInterfaceDefinition });

            Assert.That(singleInterfaceUsage.ComputeInterfaceDefinition(), Is.EqualTo([soleInterfaceDefinition]));

            // Filter discrimination: one FeatureTyping typed by InterfaceDefinition, one typed by a non-InterfaceDefinition ConnectionDefinition.
            var filteringInterfaceUsage = new InterfaceUsage();
            var keptInterfaceDefinition = new InterfaceDefinition();
            var droppedConnectionDefinition = new ConnectionDefinition();
            filteringInterfaceUsage.AssignOwnership(new FeatureTyping { Type = keptInterfaceDefinition });
            filteringInterfaceUsage.AssignOwnership(new FeatureTyping { Type = droppedConnectionDefinition });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(filteringInterfaceUsage.ComputeInterfaceDefinition(), Has.Count.EqualTo(1));
                Assert.That(filteringInterfaceUsage.ComputeInterfaceDefinition(), Does.Contain(keptInterfaceDefinition));
                Assert.That(filteringInterfaceUsage.ComputeInterfaceDefinition(), Does.Not.Contain(droppedConnectionDefinition));
            }

            // Multiple matches: two FeatureTypings, both typed by distinct InterfaceDefinitions -> both, in order.
            var multiInterfaceUsage = new InterfaceUsage();
            var firstInterfaceDefinition = new InterfaceDefinition();
            var secondInterfaceDefinition = new InterfaceDefinition();
            multiInterfaceUsage.AssignOwnership(new FeatureTyping { Type = firstInterfaceDefinition });
            multiInterfaceUsage.AssignOwnership(new FeatureTyping { Type = secondInterfaceDefinition });

            Assert.That(multiInterfaceUsage.ComputeInterfaceDefinition(), Is.EqualTo([firstInterfaceDefinition, secondInterfaceDefinition]));
        }
    }
}
