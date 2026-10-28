// ------------------------------------------------------------------------------------------------
// <copyright file="ClassifierExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ClassifierExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwnedSubclassification()
        {
            // Null subject:
            Assert.That(() => ((IClassifier)null).ComputeOwnedSubclassification(), Throws.TypeOf<ArgumentNullException>());

            // Empty classifier:
            var subject = new Classifier();
            Assert.That(subject.ComputeOwnedSubclassification(), Has.Count.EqualTo(0));

            var superClassifier = new Classifier();

            var subclassification = new Subclassification
            {
                Subclassifier = subject,
                Superclassifier = superClassifier
            };

            subject.AssignOwnership(subclassification);

            Assert.That(subject.ComputeOwnedSubclassification(), Is.EquivalentTo([subclassification]));

        }
    }
}
