// -------------------------------------------------------------------------------------------------
// <copyright file="MetadataUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
// 
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
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
    using SysML2.NET.Exceptions;
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

            // [0..1] upper-bound violation (STRICT contract): two Metaclass typings → MultiplicityViolationException.
            var subjectTwoTypings = new MetadataUsage();
            subjectTwoTypings.AssignOwnership(new FeatureTyping { Type = new Metaclass() });
            subjectTwoTypings.AssignOwnership(new FeatureTyping { Type = new Metaclass() });

            Assert.That(subjectTwoTypings.ComputeMetadataDefinition, Throws.TypeOf<MultiplicityViolationException>());
        }
    }
}
