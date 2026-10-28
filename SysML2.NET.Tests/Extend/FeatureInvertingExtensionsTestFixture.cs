// ------------------------------------------------------------------------------------------------
// <copyright file="FeatureInvertingExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    public class FeatureInvertingExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwningFeature()
        {
            Assert.That(() => ((IFeatureInverting)null).ComputeOwningFeature(), Throws.TypeOf<ArgumentNullException>());

            var featureInverting = new FeatureInverting();

            Assert.That(featureInverting.ComputeOwningFeature(), Is.Null);

            var feature = new Feature();

            ((IContainedRelationship)featureInverting).OwningRelatedElement = feature;

            // owningFeature subsets featureInverted, so the owner must play that role to surface.
            Assert.That(featureInverting.ComputeOwningFeature(), Is.Null);

            featureInverting.FeatureInverted = feature;

            Assert.That(featureInverting.ComputeOwningFeature(), Is.SameAs(feature));

            featureInverting.FeatureInverted = new Feature();

            Assert.That(featureInverting.ComputeOwningFeature(), Is.Null);

            featureInverting.FeatureInverted = feature;

            var nonFeatureOwner = new Namespace();
            var nonFeatureInverting = new FeatureInverting();

            ((IContainedRelationship)nonFeatureInverting).OwningRelatedElement = nonFeatureOwner;

            Assert.That(nonFeatureInverting.ComputeOwningFeature(), Is.Null);
        }
    }
}
