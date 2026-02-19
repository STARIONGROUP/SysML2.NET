// -------------------------------------------------------------------------------------------------
// <copyright file="TypeExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    
    using SysML2.NET.Core.POCO.Core.Types;

    [TestFixture]
    public class TypeExtensionsTestFixture
    {
        [Test]
        public void ComputeDifferencingType_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IType)null).ComputeDifferencingType(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeDirectedFeature_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IType)null).ComputeDirectedFeature(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeEndFeature_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IType)null).ComputeEndFeature(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeFeature_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IType)null).ComputeFeature(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeFeatureMembership_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IType)null).ComputeFeatureMembership(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeInheritedFeature_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IType)null).ComputeInheritedFeature(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeInheritedMembership_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IType)null).ComputeInheritedMembership(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeInput_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IType)null).ComputeInput(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeIntersectingType_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IType)null).ComputeIntersectingType(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeIsConjugated_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IType)null).ComputeIsConjugated(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeMultiplicity_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IType)null).ComputeMultiplicity(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOutput_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IType)null).ComputeOutput(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedConjugator_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IType)null).ComputeOwnedConjugator(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedDifferencing_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IType)null).ComputeOwnedDifferencing(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedDisjoining_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IType)null).ComputeOwnedDisjoining(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedEndFeature_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IType)null).ComputeOwnedEndFeature(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedFeature_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IType)null).ComputeOwnedFeature(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedFeatureMembership_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IType)null).ComputeOwnedFeatureMembership(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedIntersecting_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IType)null).ComputeOwnedIntersecting(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedSpecialization_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IType)null).ComputeOwnedSpecialization(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedUnioning_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IType)null).ComputeOwnedUnioning(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeUnioningType_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IType)null).ComputeUnioningType(), Throws.TypeOf<NotSupportedException>());
        }
    }
}
