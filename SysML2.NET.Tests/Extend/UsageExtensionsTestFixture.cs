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
            Assert.That(() => ((IUsage)null).ComputeDefinition(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeDirectedUsage_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeDirectedUsage(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeIsReference_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeIsReference(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeMayTimeVary_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeMayTimeVary(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedAction_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedAction(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedAllocation_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedAllocation(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedAnalysisCase_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedAnalysisCase(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedAttribute_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedAttribute(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedCalculation_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedCalculation(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedCase_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedCase(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedConcern_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedConcern(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedConnection_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedConnection(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedConstraint_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedConstraint(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedEnumeration_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedEnumeration(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedFlow_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedFlow(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedInterface_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedInterface(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedItem_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedItem(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedMetadata_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedMetadata(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedOccurrence_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedOccurrence(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedPart_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedPart(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedPort_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedPort(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedReference_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedReference(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedRendering_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedRendering(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedRequirement_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedRequirement(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedState_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedState(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedTransition_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedTransition(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedUsage_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedUsage(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedUseCase_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedUseCase(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedVerificationCase_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedVerificationCase(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedView_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedView(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeNestedViewpoint_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedViewpoint(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwningDefinition_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeOwningDefinition(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwningUsage_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeOwningUsage(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeUsage_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeUsage(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeVariant_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeVariant(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeVariantMembership_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IUsage)null).ComputeVariantMembership(), Throws.TypeOf<NotSupportedException>());
        }
    }
}

