// -------------------------------------------------------------------------------------------------
// <copyright file="ElementExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    
    using SysML2.NET.Core.POCO.Root.Elements;

    [TestFixture]
    public class ElementExtensionsTestFixture
    {
        [Test]
        public void ComputeDocumentation_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IElement)null).ComputeDocumentation());
        }
        
        [Test]
        public void ComputeIsLibraryElement_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IElement)null).ComputeIsLibraryElement());
        }
        
        [Test]
        public void ComputeName_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IElement)null).ComputeName());
        }
        
        [Test]
        public void ComputeOwnedAnnotation_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IElement)null).ComputeOwnedAnnotation());
        }
        
        [Test]
        public void ComputeOwnedElement_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IElement)null).ComputeOwnedElement());
        }
        
        [Test]
        public void ComputeOwner_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IElement)null).ComputeOwner());
        }
        
        [Test]
        public void ComputeOwningMembership_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IElement)null).ComputeOwningMembership());
        }
        
        [Test]
        public void ComputeOwningNamespace_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IElement)null).ComputeOwningNamespace());
        }
        
        [Test]
        public void ComputeQualifiedName_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IElement)null).ComputeQualifiedName());
        }
        
        [Test]
        public void ComputeShortName_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IElement)null).ComputeShortName());
        }
        
        [Test]
        public void ComputeTextualRepresentation_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IElement)null).ComputeTextualRepresentation());
        }
    }
}
