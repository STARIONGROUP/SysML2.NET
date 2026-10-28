// ------------------------------------------------------------------------------------------------
// <copyright file="SubclassificationExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    [TestFixture]
    public class SubclassificationExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwningClassifier()
        {
            Assert.That(() => ((ISubclassification)null).ComputeOwningClassifier(), Throws.TypeOf<ArgumentNullException>());

            // Empty Subclassification (OwningRelatedElement is null) → returns null.
            var subclassification = new Subclassification();

            Assert.That(subclassification.ComputeOwningClassifier(), Is.Null);

            // OwningRelatedElement is an IClassifier → returns the classifier.
            var classifier = new Classifier();

            ((IContainedRelationship)subclassification).OwningRelatedElement = classifier;

            Assert.That(subclassification.ComputeOwningClassifier(), Is.SameAs(classifier));

            // OwningRelatedElement is NOT an IClassifier (e.g. Namespace) → returns null.
            var nonClassifierSubclassification = new Subclassification();
            var namespaceObj = new Namespace();

            ((IContainedRelationship)nonClassifierSubclassification).OwningRelatedElement = namespaceObj;

            Assert.That(nonClassifierSubclassification.ComputeOwningClassifier(), Is.Null);
        }
    }
}
