// ------------------------------------------------------------------------------------------------
// <copyright file="ReferenceSubsettingExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    public class ReferenceSubsettingExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeReferencingFeature()
        {
            // Null subject → ArgumentNullException.
            Assert.That(() => ((IReferenceSubsetting)null).ComputeReferencingFeature(), Throws.TypeOf<ArgumentNullException>());

            var emptySubsetting = new ReferenceSubsetting();

            Assert.That(emptySubsetting.ComputeReferencingFeature(), Is.Null);

            // OwningRelatedElement is an IFeature → returns the same instance.
            var feature = new Feature();
            var refSubsettingWithFeature = new ReferenceSubsetting();

            ((IContainedRelationship)refSubsettingWithFeature).OwningRelatedElement = feature;

            Assert.That(refSubsettingWithFeature.ComputeReferencingFeature(), Is.SameAs(feature));

            var namespaceObj = new Namespace();
            var refSubsettingWithNamespace = new ReferenceSubsetting();

            ((IContainedRelationship)refSubsettingWithNamespace).OwningRelatedElement = namespaceObj;

            Assert.That(refSubsettingWithNamespace.ComputeReferencingFeature(), Is.Null);
        }
    }
}
