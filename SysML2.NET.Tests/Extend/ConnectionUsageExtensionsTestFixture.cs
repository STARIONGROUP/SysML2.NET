// -------------------------------------------------------------------------------------------------
// <copyright file="ConnectionUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Associations;
    using SysML2.NET.Core.POCO.Systems.Connections;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ConnectionUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeConnectionDefinition()
        {
            Assert.That(() => ((IConnectionUsage)null).ComputeConnectionDefinition(), Throws.TypeOf<ArgumentNullException>());

            var emptyConnectionUsage = new ConnectionUsage();

            Assert.That(emptyConnectionUsage.ComputeConnectionDefinition(), Has.Count.EqualTo(0));

            // A FeatureTyping whose Type is a plain Classifier (not IAssociationStructure) must be filtered out.
            var connectionUsage = new ConnectionUsage();
            var plainClassifier = new Classifier();
            connectionUsage.AssignOwnership(new FeatureTyping { Type = plainClassifier });

            Assert.That(connectionUsage.ComputeConnectionDefinition(), Has.Count.EqualTo(0));

            // A FeatureTyping typed by a ConnectionDefinition (which implements IAssociationStructure) must be included.
            var firstConnectionDefinition = new ConnectionDefinition();
            connectionUsage.AssignOwnership(new FeatureTyping { Type = firstConnectionDefinition });

            Assert.That(connectionUsage.ComputeConnectionDefinition(), Has.Count.EqualTo(1));

            // A second ConnectionDefinition added — both must appear; the non-AssociationStructure must remain absent.
            var secondConnectionDefinition = new ConnectionDefinition();
            connectionUsage.AssignOwnership(new FeatureTyping { Type = secondConnectionDefinition });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(connectionUsage.ComputeConnectionDefinition(), Has.Count.EqualTo(2));
                Assert.That(connectionUsage.ComputeConnectionDefinition(), Does.Contain(firstConnectionDefinition));
                Assert.That(connectionUsage.ComputeConnectionDefinition(), Does.Contain(secondConnectionDefinition));
                Assert.That(connectionUsage.ComputeConnectionDefinition(), Does.Not.Contain(plainClassifier));
            }
        }
    }
}
