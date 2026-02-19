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
            Assert.That(() => ((IDefinition)null).ComputeDirectedUsage(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedAction_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedAction(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedAllocation_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedAllocation(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedAnalysisCase_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedAnalysisCase(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedAttribute_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedAttribute(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedCalculation_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedCalculation(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedCase_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedCase(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedConcern_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedConcern(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedConnection_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedConnection(), Throws.TypeOf<NotSupportedException>());
        }
        
        
        [Test]
        public void ComputeOwnedConstraint_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedConstraint(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedEnumeration_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedEnumeration(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedFlow_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedFlow(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedInterface_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedInterface(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedItem_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedItem(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedMetadata_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedMetadata(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedOccurrence_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedOccurrence(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedPart_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedPart(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedPort_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedPort(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedReference_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedReference(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedRendering_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedRendering(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedRequirement_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedRequirement(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedState_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedState(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedTransition_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedTransition(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedUsage_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedUsage(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedUseCase_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedUseCase(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedVerificationCase_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedVerificationCase(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedView_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedView(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedViewpoint_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedViewpoint(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeUsage_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeUsage(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeVariant_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeVariant(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeVariantMembership_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IDefinition)null).ComputeVariantMembership(), Throws.TypeOf<NotSupportedException>());
        }
    }
}
