// ------------------------------------------------------------------------------------------------
// <copyright file="FeatureChainingExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    [TestFixture]
    public class FeatureChainingExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeFeatureChained()
        {
            Assert.That(() => ((IFeatureChaining)null).ComputeFeatureChained(), Throws.TypeOf<ArgumentNullException>());

            var featureChaining = new FeatureChaining();

            Assert.That(featureChaining.ComputeFeatureChained(), Is.Null);

            var feature = new Feature();

            ((IContainedRelationship)featureChaining).OwningRelatedElement = feature;

            Assert.That(featureChaining.ComputeFeatureChained(), Is.SameAs(feature));

            var nonFeatureOwner = new Namespace();
            var nonFeatureChaining = new FeatureChaining();

            ((IContainedRelationship)nonFeatureChaining).OwningRelatedElement = nonFeatureOwner;

            Assert.That(nonFeatureChaining.ComputeFeatureChained(), Is.Null);
        }
    }
}
