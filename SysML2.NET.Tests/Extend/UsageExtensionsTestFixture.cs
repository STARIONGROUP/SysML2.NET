// -------------------------------------------------------------------------------------------------
// <copyright file="UnioningExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    public class UsageExtensionsTestFixture
    {
        [Test]
        public void ComputeDefinition_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeDefinition());
        }
        
        [Test]
        public void ComputeDirectedUsage_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeDirectedUsage());
        }
        
        [Test]
        public void ComputeIsReference_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeIsReference());
        }
        
        [Test]
        public void ComputeMayTimeVary_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeMayTimeVary());
        }
        
        [Test]
        public void ComputeNestedAction_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedAction());
        }
        
        [Test]
        public void ComputeNestedAllocation_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedAllocation());
        }
        
        [Test]
        public void ComputeNestedAnalysisCase_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedAnalysisCase());
        }
        
        [Test]
        public void ComputeNestedAttribute_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedAttribute());
        }
        
        [Test]
        public void ComputeNestedCalculation_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedCalculation());
        }
        
        [Test]
        public void ComputeNestedCase_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedCase());
        }
        
        [Test]
        public void ComputeNestedConcern_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedConcern());
        }
        
        [Test]
        public void ComputeNestedConnection_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedConnection());
        }
        
        [Test]
        public void ComputeNestedConstraint_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedConstraint());
        }
        
        [Test]
        public void ComputeNestedEnumeration_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedEnumeration());
        }
        
        [Test]
        public void ComputeNestedFlow_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedFlow());
        }
        
        [Test]
        public void ComputeNestedInterface_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedInterface());
        }
        
        [Test]
        public void ComputeNestedItem_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedItem());
        }
        
        [Test]
        public void ComputeNestedMetadata_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedMetadata());
        }
        
        [Test]
        public void ComputeNestedOccurrence_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedOccurrence());
        }
        
        [Test]
        public void ComputeNestedPart_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedPart());
        }
        
        [Test]
        public void ComputeNestedPort_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedPort());
        }
        
        [Test]
        public void ComputeNestedReference_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedReference());
        }
        
        [Test]
        public void ComputeNestedRendering_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedRendering());
        }
        
        [Test]
        public void ComputeNestedRequirement_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedRequirement());
        }
        
        [Test]
        public void ComputeNestedState_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedState());
        }
        
        [Test]
        public void ComputeNestedTransition_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedTransition());
        }
        
        [Test]
        public void ComputeNestedUsage_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedUsage());
        }
        
        [Test]
        public void ComputeNestedUseCase_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedUseCase());
        }
        
        [Test]
        public void ComputeNestedVerificationCase_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedVerificationCase());
        }
        
        [Test]
        public void ComputeNestedView_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedView());
        }
        
        [Test]
        public void ComputeNestedViewpoint_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeNestedViewpoint());
        }
        
        [Test]
        public void ComputeOwningDefinition_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeOwningDefinition());
        }
        
        [Test]
        public void ComputeOwningUsage_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeOwningUsage());
        }
        
        [Test]
        public void ComputeUsage_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeUsage());
        }
        
        [Test]
        public void ComputeVariant_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeVariant());
        }
        
        [Test]
        public void ComputeVariantMembership_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IUsage)null).ComputeVariantMembership());
        }
    }
}

