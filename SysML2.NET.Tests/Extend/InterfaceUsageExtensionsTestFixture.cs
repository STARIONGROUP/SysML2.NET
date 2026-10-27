// ------------------------------------------------------------------------------------------------
// <copyright file="InterfaceUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
