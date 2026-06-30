// -------------------------------------------------------------------------------------------------
// <copyright file="ActionDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ActionDefinitionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeAction()
        {
            Assert.That(() => ((IActionDefinition)null).ComputeAction(), Throws.TypeOf<ArgumentNullException>());

            var emptyActionDefinition = new ActionDefinition();

            Assert.That(emptyActionDefinition.ComputeAction(), Has.Count.EqualTo(0));

            // Only ActionUsage instances must be returned; a bare Usage must be filtered out.
            var subject = new ActionDefinition();
            var actionUsage = new ActionUsage();
            var bareUsage = new Usage();

            subject.AssignOwnership(new FeatureMembership(), actionUsage);
            subject.AssignOwnership(new FeatureMembership(), bareUsage);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeAction(), Does.Contain(actionUsage));
                Assert.That(subject.ComputeAction(), Does.Not.Contain(bareUsage));
            }
        }
    }
}
