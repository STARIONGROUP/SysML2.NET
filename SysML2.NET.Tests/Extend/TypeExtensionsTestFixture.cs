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
            Assert.Throws<NotSupportedException>(() => ((IType)null).ComputeDifferencingType());
        }
        
        [Test]
        public void ComputeDirectedFeature_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IType)null).ComputeDirectedFeature());
        }
        
        [Test]
        public void ComputeEndFeature_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IType)null).ComputeEndFeature());
        }
        
        [Test]
        public void ComputeFeature_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IType)null).ComputeFeature());
        }
        
        [Test]
        public void ComputeFeatureMembership_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IType)null).ComputeFeatureMembership());
        }
        
        [Test]
        public void ComputeInheritedFeature_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IType)null).ComputeInheritedFeature());
        }
        
        [Test]
        public void ComputeInheritedMembership_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IType)null).ComputeInheritedMembership());
        }
        
        [Test]
        public void ComputeInput_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IType)null).ComputeInput());
        }
        
        [Test]
        public void ComputeIntersectingType_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IType)null).ComputeIntersectingType());
        }
        
        [Test]
        public void ComputeIsConjugated_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IType)null).ComputeIsConjugated());
        }
        
        [Test]
        public void ComputeMultiplicity_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IType)null).ComputeMultiplicity());
        }
        
        [Test]
        public void ComputeOutput_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IType)null).ComputeOutput());
        }
        
        [Test]
        public void ComputeOwnedConjugator_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IType)null).ComputeOwnedConjugator());
        }
        
        [Test]
        public void ComputeOwnedDifferencing_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IType)null).ComputeOwnedDifferencing());
        }
        
        [Test]
        public void ComputeOwnedDisjoining_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IType)null).ComputeOwnedDisjoining());
        }
        
        [Test]
        public void ComputeOwnedEndFeature_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IType)null).ComputeOwnedEndFeature());
        }
        
        [Test]
        public void ComputeOwnedFeature_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IType)null).ComputeOwnedFeature());
        }
        
        [Test]
        public void ComputeOwnedFeatureMembership_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IType)null).ComputeOwnedFeatureMembership());
        }
        
        [Test]
        public void ComputeOwnedIntersecting_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IType)null).ComputeOwnedIntersecting());
        }
        
        [Test]
        public void ComputeOwnedSpecialization_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IType)null).ComputeOwnedSpecialization());
        }
        
        [Test]
        public void ComputeOwnedUnioning_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IType)null).ComputeOwnedUnioning());
        }
        
        [Test]
        public void ComputeUnioningType_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IType)null).ComputeUnioningType());
        }
    }
}
