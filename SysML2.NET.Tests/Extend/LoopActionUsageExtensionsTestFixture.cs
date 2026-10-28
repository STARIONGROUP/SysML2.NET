// ------------------------------------------------------------------------------------------------
// <copyright file="LoopActionUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
