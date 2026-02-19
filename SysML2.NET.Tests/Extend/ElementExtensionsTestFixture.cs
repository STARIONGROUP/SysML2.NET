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
            Assert.That(() => ((IElement)null).ComputeDocumentation(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeIsLibraryElement_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IElement)null).ComputeIsLibraryElement(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeName_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IElement)null).ComputeName(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedAnnotation_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IElement)null).ComputeOwnedAnnotation(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedElement_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IElement)null).ComputeOwnedElement(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwner_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IElement)null).ComputeOwner(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwningMembership_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IElement)null).ComputeOwningMembership(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwningNamespace_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IElement)null).ComputeOwningNamespace(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeQualifiedName_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IElement)null).ComputeQualifiedName(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeShortName_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IElement)null).ComputeShortName(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeTextualRepresentation_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IElement)null).ComputeTextualRepresentation(), Throws.TypeOf<NotSupportedException>());
        }
    }
}
