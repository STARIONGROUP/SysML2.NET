// -------------------------------------------------------------------------------------------------
// <copyright file="TypeFeaturingExtensionsTestFixture.cs" company="Starion Group S.A.">
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
