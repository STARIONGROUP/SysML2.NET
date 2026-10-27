// ------------------------------------------------------------------------------------------------
// <copyright file="FeatureTypingExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    public class FeatureTypingExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwningFeature()
        {
            Assert.That(() => ((IFeatureTyping)null).ComputeOwningFeature(), Throws.TypeOf<ArgumentNullException>());

            var featureTyping = new FeatureTyping();

            Assert.That(featureTyping.ComputeOwningFeature(), Is.Null);

            var feature = new Feature();

            ((IContainedRelationship)featureTyping).OwningRelatedElement = feature;

            // owningFeature subsets typedFeature, so the owner must play that role to surface.
            Assert.That(featureTyping.ComputeOwningFeature(), Is.Null);

            featureTyping.TypedFeature = feature;

            Assert.That(featureTyping.ComputeOwningFeature(), Is.SameAs(feature));

            featureTyping.TypedFeature = new Feature();

            Assert.That(featureTyping.ComputeOwningFeature(), Is.Null);

            featureTyping.TypedFeature = feature;

            var namespaceOwner = new Namespace();
            var nonFeatureFeatureTyping = new FeatureTyping();

            ((IContainedRelationship)nonFeatureFeatureTyping).OwningRelatedElement = namespaceOwner;

            Assert.That(nonFeatureFeatureTyping.ComputeOwningFeature(), Is.Null);
        }
    }
}
