// -------------------------------------------------------------------------------------------------
// <copyright file="ExhibitStateUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ExhibitStateUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeExhibitedState()
        {
            Assert.That(() => ((IExhibitStateUsage)null).ComputeExhibitedState(), Throws.TypeOf<ArgumentNullException>());

            // No ownedReferenceSubsetting → returns the subject itself (ExhibitStateUsage IS a StateUsage).
            var subjectNoSubsetting = new ExhibitStateUsage();

            Assert.That(subjectNoSubsetting.ComputeExhibitedState(), Is.SameAs(subjectNoSubsetting));

            // ReferenceSubsetting whose ReferencedFeature is a StateUsage → returns that usage.
            var subjectWithStateTarget = new ExhibitStateUsage();
            var targetState = new StateUsage();
            subjectWithStateTarget.AssignOwnership(new ReferenceSubsetting { ReferencedFeature = targetState });

            Assert.That(subjectWithStateTarget.ComputeExhibitedState(), Is.SameAs(targetState));

            // ReferenceSubsetting whose ReferencedFeature is NOT a StateUsage → null (invalid-model branch, per validateExhibitStateUsageReference).
            var subjectWithNonStateTarget = new ExhibitStateUsage();
            var nonStateTarget = new ActionUsage();
            subjectWithNonStateTarget.AssignOwnership(new ReferenceSubsetting { ReferencedFeature = nonStateTarget });

            Assert.That(subjectWithNonStateTarget.ComputeExhibitedState(), Is.Null);
        }
    }
}
