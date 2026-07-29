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

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.Systems.States;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class StateDefinitionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeDoAction()
        {
            Assert.That(() => ((IStateDefinition)null).ComputeDoAction(), Throws.TypeOf<ArgumentNullException>());

            // Empty: no StateSubactionMembership at all → null.
            var emptyStateDefinition = new StateDefinition();

            Assert.That(emptyStateDefinition.ComputeDoAction(), Is.Null);

            // Wrong kind: one StateSubactionMembership of kind Entry only → null (action is never accessed).
            var stateDefinitionWithEntry = new StateDefinition();
            var entryAction = new ActionUsage();
            stateDefinitionWithEntry.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Entry }, entryAction);

            Assert.That(stateDefinitionWithEntry.ComputeDoAction(), Is.Null);

            // Matching kind: one StateSubactionMembership of kind Do → returns the wired ActionUsage.
            var stateDefinitionWithDo = new StateDefinition();
            var doAction = new ActionUsage();
            stateDefinitionWithDo.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Do }, doAction);

            Assert.That(stateDefinitionWithDo.ComputeDoAction(), Is.SameAs(doAction));

            // All three kinds present → the Kind filter picks the Do membership's action; Entry and Exit excluded.
            var stateDefinitionAllKinds = new StateDefinition();
            var allKindsEntry = new ActionUsage();
            var allKindsDo = new ActionUsage();
            var allKindsExit = new ActionUsage();
            stateDefinitionAllKinds.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Entry }, allKindsEntry);
            stateDefinitionAllKinds.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Do }, allKindsDo);
            stateDefinitionAllKinds.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Exit }, allKindsExit);

            Assert.That(stateDefinitionAllKinds.ComputeDoAction(), Is.SameAs(allKindsDo));
        }

        [Test]
        public void VerifyComputeEntryAction()
        {
            Assert.That(() => ((IStateDefinition)null).ComputeEntryAction(), Throws.TypeOf<ArgumentNullException>());

            // Empty: no StateSubactionMembership at all → null.
            var emptyStateDefinition = new StateDefinition();

            Assert.That(emptyStateDefinition.ComputeEntryAction(), Is.Null);

            // Wrong kind: one StateSubactionMembership of kind Do only → null (action is never accessed).
            var stateDefinitionWithDo = new StateDefinition();
            var doAction = new ActionUsage();
            stateDefinitionWithDo.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Do }, doAction);

            Assert.That(stateDefinitionWithDo.ComputeEntryAction(), Is.Null);

            // Matching kind: one StateSubactionMembership of kind Entry → returns the wired ActionUsage.
            var stateDefinitionWithEntry = new StateDefinition();
            var entryAction = new ActionUsage();
            stateDefinitionWithEntry.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Entry }, entryAction);

            Assert.That(stateDefinitionWithEntry.ComputeEntryAction(), Is.SameAs(entryAction));

            // All three kinds present → the Kind filter picks the Entry membership's action; Do and Exit excluded.
            var stateDefinitionAllKinds = new StateDefinition();
            var allKindsEntry = new ActionUsage();
            var allKindsDo = new ActionUsage();
            var allKindsExit = new ActionUsage();
            stateDefinitionAllKinds.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Entry }, allKindsEntry);
            stateDefinitionAllKinds.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Do }, allKindsDo);
            stateDefinitionAllKinds.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Exit }, allKindsExit);

            Assert.That(stateDefinitionAllKinds.ComputeEntryAction(), Is.SameAs(allKindsEntry));
        }

        [Test]
        public void VerifyComputeExitAction()
        {
            Assert.That(() => ((IStateDefinition)null).ComputeExitAction(), Throws.TypeOf<ArgumentNullException>());

            // Empty: no StateSubactionMembership at all → null.
            var emptyStateDefinition = new StateDefinition();

            Assert.That(emptyStateDefinition.ComputeExitAction(), Is.Null);

            // Wrong kind: one StateSubactionMembership of kind Do only → null (action is never accessed).
            var stateDefinitionWithDo = new StateDefinition();
            var doAction = new ActionUsage();
            stateDefinitionWithDo.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Do }, doAction);

            Assert.That(stateDefinitionWithDo.ComputeExitAction(), Is.Null);

            // Matching kind: one StateSubactionMembership of kind Exit → returns the wired ActionUsage.
            var stateDefinitionWithExit = new StateDefinition();
            var exitAction = new ActionUsage();
            stateDefinitionWithExit.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Exit }, exitAction);

            Assert.That(stateDefinitionWithExit.ComputeExitAction(), Is.SameAs(exitAction));

            // All three kinds present → the Kind filter picks the Exit membership's action; Entry and Do excluded.
            var stateDefinitionAllKinds = new StateDefinition();
            var allKindsEntry = new ActionUsage();
            var allKindsDo = new ActionUsage();
            var allKindsExit = new ActionUsage();
            stateDefinitionAllKinds.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Entry }, allKindsEntry);
            stateDefinitionAllKinds.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Do }, allKindsDo);
            stateDefinitionAllKinds.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Exit }, allKindsExit);

            Assert.That(stateDefinitionAllKinds.ComputeExitAction(), Is.SameAs(allKindsExit));
        }

        [Test]
        public void VerifyComputeState()
        {
            Assert.That(() => ((IStateDefinition)null).ComputeState(), Throws.TypeOf<ArgumentNullException>());

            // Empty: no owned actions at all → empty list.
            var emptySubject = new StateDefinition();

            Assert.That(emptySubject.ComputeState(), Is.Empty);

            // A plain ActionUsage (not a StateUsage) is filtered out by the selectByKind(StateUsage).
            var subjectWithPlainActionOnly = new StateDefinition();
            var plainAction = new ActionUsage();
            subjectWithPlainActionOnly.AssignOwnership(new FeatureMembership(), plainAction);

            Assert.That(subjectWithPlainActionOnly.ComputeState(), Is.Empty);

            // Populated: mix of a plain ActionUsage (excluded) and two StateUsages (included, in action order).
            var subject = new StateDefinition();
            var otherPlainAction = new ActionUsage();
            var stateUsage1 = new StateUsage();
            var stateUsage2 = new StateUsage();

            subject.AssignOwnership(new FeatureMembership(), otherPlainAction);
            subject.AssignOwnership(new FeatureMembership(), stateUsage1);
            subject.AssignOwnership(new FeatureMembership(), stateUsage2);

            Assert.That(subject.ComputeState(), Is.EqualTo([stateUsage1, stateUsage2]));
        }
    }
}
