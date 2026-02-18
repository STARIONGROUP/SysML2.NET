// -------------------------------------------------------------------------------------------------
// <copyright file="StateDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    
    using SysML2.NET.Core.POCO.Systems.States;

    [TestFixture]
    public class StateDefinitionExtensionsTestFixture
    {
        [Test]
        public void ComputeDoAction_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IStateDefinition)null).ComputeDoAction());
        }
        
        [Test]
        public void ComputeEntryAction_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IStateDefinition)null).ComputeEntryAction());
        }
        
        [Test]
        public void ComputeExitAction_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IStateDefinition)null).ComputeExitAction());
        }
        
        [Test]
        public void ComputeState_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => ((IStateDefinition)null).ComputeState());
        }
    }
}
