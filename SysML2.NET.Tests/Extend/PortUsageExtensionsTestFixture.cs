// ------------------------------------------------------------------------------------------------
// <copyright file="PortUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class PortUsageExtensionsTestFixture
    {
        [Test]
        public void Verify_ComputePortDefinition()
        {
            Assert.That(
                () => ((IPortUsage)null).ComputePortDefinition(),
                Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new PortUsage();

            Assert.That(emptySubject.ComputePortDefinition(), Is.Empty);

            var singleSubject = new PortUsage();
            var portDefinition = new PortDefinition();
            singleSubject.AssignOwnership(new FeatureTyping { Type = portDefinition });

            Assert.That(singleSubject.ComputePortDefinition(), Is.EqualTo([portDefinition]));

            var filterSubject = new PortUsage();
            var filteredPortDefinition = new PortDefinition();
            var nonPortOccurrenceDefinition = new OccurrenceDefinition();
            filterSubject.AssignOwnership(new FeatureTyping { Type = filteredPortDefinition });
            filterSubject.AssignOwnership(new FeatureTyping { Type = nonPortOccurrenceDefinition });

            Assert.That(filterSubject.ComputePortDefinition(), Is.EqualTo([filteredPortDefinition]));

            var multiSubject = new PortUsage();
            var firstPortDefinition = new PortDefinition();
            var secondPortDefinition = new PortDefinition();
            multiSubject.AssignOwnership(new FeatureTyping { Type = firstPortDefinition });
            multiSubject.AssignOwnership(new FeatureTyping { Type = secondPortDefinition });

            Assert.That(multiSubject.ComputePortDefinition(), Is.EqualTo([firstPortDefinition, secondPortDefinition]));
        }
    }
}
