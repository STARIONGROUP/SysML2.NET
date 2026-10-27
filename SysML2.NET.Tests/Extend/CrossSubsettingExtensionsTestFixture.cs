// ------------------------------------------------------------------------------------------------
// <copyright file="CrossSubsettingExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Exceptions;

    [TestFixture]
    public class CrossSubsettingExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeCrossingFeature()
        {
            Assert.That(() => ((ICrossSubsetting)null).ComputeCrossingFeature(), Throws.TypeOf<ArgumentNullException>());

            var crossSubsetting = new CrossSubsetting();

            Assert.That(crossSubsetting.ComputeCrossingFeature, Throws.TypeOf<IncompleteModelException>());

            var feature = new Feature();

            ((IContainedRelationship)crossSubsetting).OwningRelatedElement = feature;

            Assert.That(crossSubsetting.ComputeCrossingFeature(), Is.SameAs(feature));

            var nonFeatureOwner = new Namespace();
            var nonFeatureCrossSubsetting = new CrossSubsetting();

            ((IContainedRelationship)nonFeatureCrossSubsetting).OwningRelatedElement = nonFeatureOwner;

            Assert.That(nonFeatureCrossSubsetting.ComputeCrossingFeature, Throws.TypeOf<IncompleteModelException>());
        }
    }
}
