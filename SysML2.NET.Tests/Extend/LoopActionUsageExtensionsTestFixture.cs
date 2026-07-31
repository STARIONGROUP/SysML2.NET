// -------------------------------------------------------------------------------------------------
// <copyright file="LoopActionUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class LoopActionUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeBodyAction()
        {
            // LoopActionUsage is abstract, so the concrete ILoopActionUsage subtype WhileLoopActionUsage is used as subject.
            Assert.That(() => ((ILoopActionUsage)null).ComputeBodyAction(), Throws.TypeOf<ArgumentNullException>());

            // No input parameters -> inputParameter(2) is out of range -> null.
            Assert.That(new WhileLoopActionUsage().ComputeBodyAction(), Is.Null);

            // Position-2 input parameter that IS an ActionUsage -> returned via the as-cast.
            var subjectWithBodyAction = new WhileLoopActionUsage();
            var bodyActionParameter = new ActionUsage { Direction = FeatureDirectionKind.In };
            subjectWithBodyAction.AssignOwnership(new FeatureMembership(), new ReferenceUsage { Direction = FeatureDirectionKind.In });
            subjectWithBodyAction.AssignOwnership(new FeatureMembership(), bodyActionParameter);

            // Position-2 input parameter that is NOT an ActionUsage -> the as-cast yields null.
            var subjectWithNonActionSecondParameter = new WhileLoopActionUsage();
            subjectWithNonActionSecondParameter.AssignOwnership(new FeatureMembership(), new ReferenceUsage { Direction = FeatureDirectionKind.In });
            subjectWithNonActionSecondParameter.AssignOwnership(new FeatureMembership(), new ReferenceUsage { Direction = FeatureDirectionKind.In });

            using (Assert.EnterMultipleScope())
            {
                // bodyAction = inputParameter(2) as ActionUsage
                Assert.That(subjectWithBodyAction.ComputeBodyAction(), Is.SameAs(bodyActionParameter));
                Assert.That(subjectWithNonActionSecondParameter.ComputeBodyAction(), Is.Null);
            }
        }
    }
}
