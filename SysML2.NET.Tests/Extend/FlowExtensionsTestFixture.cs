// -------------------------------------------------------------------------------------------------
// <copyright file="FlowExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    
    using SysML2.NET.Core.POCO.Kernel.Interactions;

    [TestFixture]
    public class FlowExtensionsTestFixture
    {
        [Test]
        public void ComputeFlowEnd_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IFlow)null).ComputeFlowEnd(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeInteraction_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IFlow)null).ComputeInteraction(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputePayloadFeature_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IFlow)null).ComputePayloadFeature(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputePayloadType_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IFlow)null).ComputePayloadType(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeSourceOutputFeature_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IFlow)null).ComputeSourceOutputFeature(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeTargetInputFeature_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IFlow)null).ComputeTargetInputFeature(), Throws.TypeOf<NotSupportedException>());
        }
    }
}
