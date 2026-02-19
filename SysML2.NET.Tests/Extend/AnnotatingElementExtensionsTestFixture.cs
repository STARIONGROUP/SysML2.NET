// -------------------------------------------------------------------------------------------------
// <copyright file="AnnotatingElementExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    public class AnnotatingElementExtensionsTestFixture
    {
        [Test]
        public void ComputeAnnotatedElement_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IAnnotatingElement)null).ComputeAnnotatedElement(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeAnnotation_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IAnnotatingElement)null).ComputeAnnotation(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwnedAnnotatingRelationship_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IAnnotatingElement)null).ComputeOwnedAnnotatingRelationship(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeOwningAnnotatingRelationship_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IAnnotatingElement)null).ComputeOwningAnnotatingRelationship(), Throws.TypeOf<NotSupportedException>());
        }
    }
}
