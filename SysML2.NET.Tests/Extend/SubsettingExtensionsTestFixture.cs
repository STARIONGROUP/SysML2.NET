// ------------------------------------------------------------------------------------------------
// <copyright file="SubsettingExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    public class SubsettingExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwningFeature()
        {
            Assert.That(() => ((ISubsetting)null).ComputeOwningFeature(), Throws.TypeOf<ArgumentNullException>());

            var subsetting = new Subsetting();

            Assert.That(subsetting.ComputeOwningFeature(), Is.Null);

            var feature = new Feature();

            ((IContainedRelationship)subsetting).OwningRelatedElement = feature;

            Assert.That(subsetting.ComputeOwningFeature(), Is.SameAs(feature));

            var namespaceOwner = new Namespace();
            var nonFeatureSubsetting = new Subsetting();

            ((IContainedRelationship)nonFeatureSubsetting).OwningRelatedElement = namespaceOwner;

            Assert.That(nonFeatureSubsetting.ComputeOwningFeature(), Is.Null);
        }
    }
}
