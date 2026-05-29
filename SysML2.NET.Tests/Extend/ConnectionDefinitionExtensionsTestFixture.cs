// -------------------------------------------------------------------------------------------------
// <copyright file="ConnectionDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Systems.Connections;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ConnectionDefinitionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeConnectionEnd()
        {
            Assert.That(() => ((IConnectionDefinition)null).ComputeConnectionEnd(), Throws.TypeOf<ArgumentNullException>());

            var emptyConnectionDefinition = new ConnectionDefinition();

            Assert.That(emptyConnectionDefinition.ComputeConnectionEnd(), Is.Empty);

            // A feature with IsEnd = false must not be included.
            var nonEndConnectionDefinition = new ConnectionDefinition();
            var nonEndUsage = new Usage { IsEnd = false };
            nonEndConnectionDefinition.AssignOwnership(new FeatureMembership(), nonEndUsage);

            Assert.That(nonEndConnectionDefinition.ComputeConnectionEnd(), Is.Empty);

            // A Feature (not IUsage) with IsEnd = true must be filtered out by OfType<IUsage>().
            var nonUsageConnectionDefinition = new ConnectionDefinition();
            var nonUsageFeature = new Feature { IsEnd = true };
            nonUsageConnectionDefinition.AssignOwnership(new FeatureMembership(), nonUsageFeature);

            Assert.That(nonUsageConnectionDefinition.ComputeConnectionEnd(), Is.Empty);

            // Two Usage features with IsEnd = true must both be returned.
            var subject = new ConnectionDefinition();
            var firstEnd = new Usage { IsEnd = true };
            var secondEnd = new Usage { IsEnd = true };
            var excludedNonEnd = new Usage { IsEnd = false };
            subject.AssignOwnership(new FeatureMembership(), firstEnd);
            subject.AssignOwnership(new FeatureMembership(), secondEnd);
            subject.AssignOwnership(new FeatureMembership(), excludedNonEnd);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeConnectionEnd(), Has.Count.EqualTo(2));
                Assert.That(subject.ComputeConnectionEnd(), Does.Contain(firstEnd));
                Assert.That(subject.ComputeConnectionEnd(), Does.Contain(secondEnd));
                Assert.That(subject.ComputeConnectionEnd(), Does.Not.Contain(excludedNonEnd));
            }
        }
    }
}
