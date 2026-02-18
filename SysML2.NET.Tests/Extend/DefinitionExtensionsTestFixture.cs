// -------------------------------------------------------------------------------------------------
// <copyright file="DefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;

    [TestFixture]
    public class DefinitionExtensionsTestFixture
    {
        [Test]
        public void ComputeDirectedUsage_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeDirectedUsage());
        }
        
        [Test]
        public void ComputeOwnedAction_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedAction());
        }
        
        [Test]
        public void ComputeOwnedAllocation_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedAllocation());
        }
        
        [Test]
        public void ComputeOwnedAnalysisCase_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedAnalysisCase());
        }
        
        [Test]
        public void ComputeOwnedAttribute_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedAttribute());
        }
        
        [Test]
        public void ComputeOwnedCalculation_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedCalculation());
        }
        
        [Test]
        public void ComputeOwnedCase_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedCase());
        }
        
        [Test]
        public void ComputeOwnedConcern_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedConcern());
        }
        
        [Test]
        public void ComputeOwnedConnection_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedConnection());
        }
        
        
        [Test]
        public void ComputeOwnedConstraint_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedConstraint());
        }
        
        [Test]
        public void ComputeOwnedEnumeration_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedEnumeration());
        }
        
        [Test]
        public void ComputeOwnedFlow_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedFlow());
        }
        
        [Test]
        public void ComputeOwnedInterface_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedInterface());
        }
        
        [Test]
        public void ComputeOwnedItem_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedItem());
        }
        
        [Test]
        public void ComputeOwnedMetadata_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedMetadata());
        }
        
        [Test]
        public void ComputeOwnedOccurrence_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedOccurrence());
        }
        
        [Test]
        public void ComputeOwnedPart_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedPart());
        }
        
        [Test]
        public void ComputeOwnedPort_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedPort());
        }
        
        [Test]
        public void ComputeOwnedReference_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedReference());
        }
        
        [Test]
        public void ComputeOwnedRendering_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedRendering());
        }
        
        [Test]
        public void ComputeOwnedRequirement_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedRequirement());
        }
        
        [Test]
        public void ComputeOwnedState_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedState());
        }
        
        [Test]
        public void ComputeOwnedTransition_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedTransition());
        }
        
        [Test]
        public void ComputeOwnedUsage_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedUsage());
        }
        
        [Test]
        public void ComputeOwnedUseCase_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedUseCase());
        }
        
        [Test]
        public void ComputeOwnedVerificationCase_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedVerificationCase());
        }
        
        [Test]
        public void ComputeOwnedView_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedView());
        }
        
        [Test]
        public void ComputeOwnedViewpoint_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeOwnedViewpoint());
        }
        
        [Test]
        public void ComputeUsage_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeUsage());
        }
        
        [Test]
        public void ComputeVariant_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeVariant());
        }
        
        [Test]
        public void ComputeVariantMembership_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IDefinition)null).ComputeVariantMembership());
        }
    }
}
