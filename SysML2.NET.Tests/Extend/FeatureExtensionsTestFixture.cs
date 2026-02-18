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
            Assert.Throws<NotSupportedException>(() => ((IFeature)null).ComputeChainingFeature());
        }
        
        [Test]
        public void ComputeCrossFeature_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IFeature)null).ComputeCrossFeature());
        }
        
        [Test]
        public void ComputeEndOwningType_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IFeature)null).ComputeEndOwningType());
        }
        
        [Test]
        public void ComputeFeatureTarget_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IFeature)null).ComputeFeatureTarget());
        }
        
        [Test]
        public void ComputeFeaturingType_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IFeature)null).ComputeFeaturingType());
        }
        
        [Test]
        public void ComputeOwnedCrossSubsetting_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IFeature)null).ComputeOwnedCrossSubsetting());
        }
        
        [Test]
        public void ComputeOwnedFeatureChaining_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IFeature)null).ComputeOwnedFeatureChaining());
        }
        
        [Test]
        public void ComputeOwnedFeatureInverting_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IFeature)null).ComputeOwnedFeatureInverting());
        }
        
        [Test]
        public void ComputeOwnedRedefinition_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IFeature)null).ComputeOwnedRedefinition());
        }
        
        [Test]
        public void ComputeOwnedReferenceSubsetting_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IFeature)null).ComputeOwnedReferenceSubsetting());
        }
        
        [Test]
        public void ComputeOwnedSubsetting_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IFeature)null).ComputeOwnedSubsetting());
        }
        
        [Test]
        public void ComputeOwnedTypeFeaturing_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IFeature)null).ComputeOwnedTypeFeaturing());
        }
        
        [Test]
        public void ComputeOwnedTyping_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IFeature)null).ComputeOwnedTyping());
        }
        
        [Test]
        public void ComputeOwningFeatureMembership_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IFeature)null).ComputeOwningFeatureMembership());
        }
        
        [Test]
        public void ComputeOwningType_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IFeature)null).ComputeOwningType());
        }
        
        [Test]
        public void ComputeType_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IFeature)null).ComputeType());
        }
    }
}
