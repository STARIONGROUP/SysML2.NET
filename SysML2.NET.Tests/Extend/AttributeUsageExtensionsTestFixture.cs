// -------------------------------------------------------------------------------------------------
// <copyright file="AttributeUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Attributes;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class AttributeUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeAttributeDefinition()
        {
            Assert.That(() => ((IAttributeUsage)null).ComputeAttributeDefinition(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new AttributeUsage();

            Assert.That(emptySubject.ComputeAttributeDefinition(), Has.Count.EqualTo(0));

            var subject = new AttributeUsage();
            var attributeDefinition = new AttributeDefinition();
            var bareClassifier = new Classifier();

            subject.AssignOwnership(new FeatureTyping { Type = attributeDefinition });
            subject.AssignOwnership(new FeatureTyping { Type = bareClassifier });

            // Only the IDataType (AttributeDefinition) is returned; bare Classifier is excluded.
            Assert.That(subject.ComputeAttributeDefinition(), Is.EquivalentTo(new[] { attributeDefinition }));
        }

        [Test]
        public void VerifyComputeIsReference()
        {
            Assert.That(() => ((IAttributeUsage)null).ComputeIsReference(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new AttributeUsage();

            Assert.That(emptySubject.ComputeIsReference(), Is.True);

            var populatedSubject = new AttributeUsage();
            var attributeDefinition = new AttributeDefinition();
            populatedSubject.AssignOwnership(new FeatureTyping { Type = attributeDefinition });

            // Always true regardless of subject state — AttributeUsages are always referential.
            Assert.That(populatedSubject.ComputeIsReference(), Is.True);
        }
    }
}
