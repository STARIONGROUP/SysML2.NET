// -------------------------------------------------------------------------------------------------
// <copyright file="ReferenceSubsettingExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Exceptions;

    [TestFixture]
    public class ReferenceSubsettingExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeReferencingFeature()
        {
            // Null subject → ArgumentNullException.
            Assert.That(() => ((IReferenceSubsetting)null).ComputeReferencingFeature(), Throws.TypeOf<ArgumentNullException>());

            // Empty ReferenceSubsetting (OwningRelatedElement is null) → [1..1] violation: IncompleteModelException.
            var emptySubsetting = new ReferenceSubsetting();

            Assert.That(() => emptySubsetting.ComputeReferencingFeature(), Throws.TypeOf<IncompleteModelException>());

            // OwningRelatedElement is an IFeature → returns the same instance.
            var feature = new Feature();
            var refSubsettingWithFeature = new ReferenceSubsetting();

            ((IContainedRelationship)refSubsettingWithFeature).OwningRelatedElement = feature;

            Assert.That(refSubsettingWithFeature.ComputeReferencingFeature(), Is.SameAs(feature));

            // OwningRelatedElement is a non-IFeature (Namespace) → [1..1] type violation: IncompleteModelException.
            var namespaceObj = new Namespace();
            var refSubsettingWithNamespace = new ReferenceSubsetting();

            ((IContainedRelationship)refSubsettingWithNamespace).OwningRelatedElement = namespaceObj;

            Assert.That(() => refSubsettingWithNamespace.ComputeReferencingFeature(), Throws.TypeOf<IncompleteModelException>());
        }
    }
}
