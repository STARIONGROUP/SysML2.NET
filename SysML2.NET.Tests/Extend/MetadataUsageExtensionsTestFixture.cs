// ------------------------------------------------------------------------------------------------
// <copyright file="MetadataUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.Metadata;
    using SysML2.NET.Core.POCO.Systems.Metadata;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class MetadataUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeMetadataDefinition()
        {
            Assert.That(() => ((IMetadataUsage)null).ComputeMetadataDefinition(), Throws.TypeOf<ArgumentNullException>());

            // [0..1] lower bound: no Metaclass typing → null.
            var subjectNoTyping = new MetadataUsage();

            Assert.That(subjectNoTyping.ComputeMetadataDefinition(), Is.Null);

            // Populated: one FeatureTyping whose Type is a Metaclass (a non-Metaclass typing is filtered out) → returned.
            var subjectOneTyping = new MetadataUsage();
            var metaclass = new Metaclass();
            subjectOneTyping.AssignOwnership(new FeatureTyping { Type = metaclass });
            subjectOneTyping.AssignOwnership(new FeatureTyping { Type = new Type() });

            Assert.That(subjectOneTyping.ComputeMetadataDefinition(), Is.SameAs(metaclass));

            // The redefined metaclass OCL elects metaClassTypes->first(), so two Metaclass typings are legal.
            var subjectTwoTypings = new MetadataUsage();
            var firstMetaclass = new Metaclass();
            subjectTwoTypings.AssignOwnership(new FeatureTyping { Type = firstMetaclass });
            subjectTwoTypings.AssignOwnership(new FeatureTyping { Type = new Metaclass() });

            Assert.That(subjectTwoTypings.ComputeMetadataDefinition(), Is.SameAs(firstMetaclass));
        }
    }
}
