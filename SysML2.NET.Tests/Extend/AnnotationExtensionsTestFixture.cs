// -------------------------------------------------------------------------------------------------
// <copyright file="AnnotationExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    
    using SysML2.NET.Core.POCO.Root.Annotations;

    [TestFixture]
    public class AnnotationExtensionsTestFixture
    {
        [Test]
        public void ComputeAnnotatingElement_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IAnnotation)null).ComputeAnnotatingElement(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedAnnotatingElement_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IAnnotation)null).ComputeOwnedAnnotatingElement(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwningAnnotatedElement_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IAnnotation)null).ComputeOwningAnnotatedElement(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwningAnnotatingElement_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IAnnotation)null).ComputeOwningAnnotatingElement(), Throws.TypeOf<NotSupportedException>());
        }
    }
}
