// ------------------------------------------------------------------------------------------------
// <copyright file="ItemUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.Classes;
    using SysML2.NET.Core.POCO.Kernel.Structures;
    using SysML2.NET.Core.POCO.Systems.Items;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ItemUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeItemDefinition()
        {
            Assert.That(() => ((IItemUsage)null).ComputeItemDefinition(), Throws.TypeOf<ArgumentNullException>());

            // Empty: no typings → empty list. itemDefinition = occurrenceDefinition->selectByKind(Structure),
            // occurrenceDefinition = type->selectByKind(Class).
            var emptySubject = new ItemUsage();

            Assert.That(emptySubject.ComputeItemDefinition(), Is.Empty);

            // Populated: a Structure (which is a Class → surfaces in occurrenceDefinition, and is a Structure → kept in
            // itemDefinition) plus a plain Class (surfaces in occurrenceDefinition but is NOT a Structure → excluded).
            var subject = new ItemUsage();
            var structure = new Structure();
            subject.AssignOwnership(new FeatureTyping { Type = structure });
            subject.AssignOwnership(new FeatureTyping { Type = new Class() });

            var result = subject.ComputeItemDefinition();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result, Does.Contain(structure));
            }
        }
    }
}
