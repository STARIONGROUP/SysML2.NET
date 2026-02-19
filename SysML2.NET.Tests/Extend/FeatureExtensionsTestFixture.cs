// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureExtensionsTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2026 Starion Group S.A.
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

    [TestFixture]
    public class FeatureExtensionsTestFixture
    {
        [Test]
        public void ComputeChainingFeature_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IFeature)null).ComputeChainingFeature(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeCrossFeature_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IFeature)null).ComputeCrossFeature(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeEndOwningType_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IFeature)null).ComputeEndOwningType(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeFeatureTarget_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IFeature)null).ComputeFeatureTarget(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeFeaturingType_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IFeature)null).ComputeFeaturingType(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedCrossSubsetting_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IFeature)null).ComputeOwnedCrossSubsetting(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedFeatureChaining_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IFeature)null).ComputeOwnedFeatureChaining(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedFeatureInverting_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IFeature)null).ComputeOwnedFeatureInverting(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedRedefinition_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IFeature)null).ComputeOwnedRedefinition(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedReferenceSubsetting_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IFeature)null).ComputeOwnedReferenceSubsetting(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedSubsetting_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IFeature)null).ComputeOwnedSubsetting(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedTypeFeaturing_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IFeature)null).ComputeOwnedTypeFeaturing(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedTyping_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IFeature)null).ComputeOwnedTyping(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwningFeatureMembership_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IFeature)null).ComputeOwningFeatureMembership(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwningType_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IFeature)null).ComputeOwningType(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeType_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IFeature)null).ComputeType(), Throws.TypeOf<NotSupportedException>());
        }
    }
}
