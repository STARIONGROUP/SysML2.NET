// ------------------------------------------------------------------------------------------------
// <copyright file="TypeFeaturingExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    public class TypeFeaturingExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwningFeatureOfType()
        {
            Assert.That(() => ((ITypeFeaturing)null).ComputeOwningFeatureOfType(), Throws.TypeOf<ArgumentNullException>());

            var typeFeaturing = new TypeFeaturing();

            Assert.That(typeFeaturing.ComputeOwningFeatureOfType(), Is.Null);

            var feature = new Feature();

            ((IContainedRelationship)typeFeaturing).OwningRelatedElement = feature;

            Assert.That(typeFeaturing.ComputeOwningFeatureOfType(), Is.SameAs(feature));

            var nonFeatureTypeFeaturing = new TypeFeaturing();

            ((IContainedRelationship)nonFeatureTypeFeaturing).OwningRelatedElement = new Namespace();

            Assert.That(nonFeatureTypeFeaturing.ComputeOwningFeatureOfType(), Is.Null);
        }
    }
}
