// -------------------------------------------------------------------------------------------------
// <copyright file="ConnectorExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    
    using SysML2.NET.Core.POCO.Kernel.Connectors;

    [TestFixture]
    public class ConnectorExtensionsTestFixture
    {
        [Test]
        public void ComputeAssociation_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IConnector)null).ComputeAssociation(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeConnectorEnd_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IConnector)null).ComputeConnectorEnd(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeDefaultFeaturingType_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IConnector)null).ComputeDefaultFeaturingType(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeRelatedFeature_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IConnector)null).ComputeRelatedFeature(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeSourceFeature_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IConnector)null).ComputeSourceFeature(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeTargetFeature_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IConnector)null).ComputeTargetFeature(), Throws.TypeOf<NotSupportedException>());
        }
    }
}
