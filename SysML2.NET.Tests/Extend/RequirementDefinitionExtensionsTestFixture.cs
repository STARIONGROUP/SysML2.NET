// -------------------------------------------------------------------------------------------------
// <copyright file="RequirementDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    
    using SysML2.NET.Core.POCO.Systems.Requirements;

    [TestFixture]
    public class RequirementDefinitionExtensionsTestFixture
    {
        [Test]
        public void ComputeActorParameter_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IRequirementDefinition)null).ComputeActorParameter());
        }
        
        [Test]
        public void ComputeAssumedConstraint_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IRequirementDefinition)null).ComputeAssumedConstraint());
        }
        
        [Test]
        public void ComputeFramedConcern_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IRequirementDefinition)null).ComputeFramedConcern());
        }
        [Test]
        public void ComputeRequiredConstraint_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IRequirementDefinition)null).ComputeRequiredConstraint());
        }
        
        [Test]
        public void ComputeStakeholderParameter_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IRequirementDefinition)null).ComputeStakeholderParameter());
        }
        
        [Test]
        public void ComputeSubjectParameter_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IRequirementDefinition)null).ComputeSubjectParameter());
        }
        
        [Test]
        public void ComputeText_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IRequirementDefinition)null).ComputeText());
        }
    }
}
