// -------------------------------------------------------------------------------------------------
// <copyright file="PortUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
                Throws.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("portUsageSubject"));

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
