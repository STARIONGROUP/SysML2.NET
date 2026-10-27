// ------------------------------------------------------------------------------------------------
// <copyright file="AttributeUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
