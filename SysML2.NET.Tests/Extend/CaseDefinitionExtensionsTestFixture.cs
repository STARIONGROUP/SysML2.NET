// -------------------------------------------------------------------------------------------------
// <copyright file="CaseDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Systems.Cases ;

    [TestFixture]
    public class CaseDefinitionExtensionsTestFixture
    {
        [Test]
        public void ComputeActorParameter_ThrowsNotSupportedException()
        {
            Assert.That(() => ((ICaseDefinition)null).ComputeActorParameter(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeObjectiveRequirement_ThrowsNotSupportedException()
        {
            Assert.That(() => ((ICaseDefinition)null).ComputeObjectiveRequirement(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeSubjectParameter_ThrowsNotSupportedException()
        {
            Assert.That(() => ((ICaseDefinition)null).ComputeSubjectParameter(), Throws.TypeOf<NotSupportedException>());
        }
    }
}
