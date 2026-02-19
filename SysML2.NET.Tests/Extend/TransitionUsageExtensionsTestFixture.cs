// -------------------------------------------------------------------------------------------------
// <copyright file="TransitionUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    public class TransitionUsageExtensionsTestFixture
    {
        [Test]
        public void ComputeEffectAction_ThrowsNotSupportedException()
        {
            Assert.That(() => ((ITransitionUsage)null).ComputeEffectAction(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeGuardExpression_ThrowsNotSupportedException()
        {
            Assert.That(() => ((ITransitionUsage)null).ComputeGuardExpression(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeSource_ThrowsNotSupportedException()
        {
            Assert.That(() => ((ITransitionUsage)null).ComputeSource(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeSuccession_ThrowsNotSupportedException()
        {
            Assert.That(() => ((ITransitionUsage)null).ComputeSuccession(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeTarget_ThrowsNotSupportedException()
        {
            Assert.That(() => ((ITransitionUsage)null).ComputeTarget(), Throws.TypeOf<NotSupportedException>());
        }
        
        [Test]
        public void ComputeTriggerAction_ThrowsNotSupportedException()
        {
            Assert.That(() => ((ITransitionUsage)null).ComputeTriggerAction(), Throws.TypeOf<NotSupportedException>());
        }
    }
}
