// -------------------------------------------------------------------------------------------------
// <copyright file="StateUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.Systems.States;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class StateUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeDoAction()
        {
            Assert.That(() => ((IStateUsage)null).ComputeDoAction(), Throws.TypeOf<ArgumentNullException>());

            // Empty: no StateSubactionMembership at all → null.
            var emptyStateUsage = new StateUsage();

            Assert.That(emptyStateUsage.ComputeDoAction(), Is.Null);

            // Wrong kind: one StateSubactionMembership of kind Entry only → null (action is never accessed).
            var stateUsageWithEntry = new StateUsage();
            var entryAction = new ActionUsage();
            stateUsageWithEntry.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Entry }, entryAction);

            Assert.That(stateUsageWithEntry.ComputeDoAction(), Is.Null);

            // Matching kind: one StateSubactionMembership of kind Do → returns the wired ActionUsage.
            var stateUsageWithDo = new StateUsage();
            var doAction = new ActionUsage();
            stateUsageWithDo.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Do }, doAction);

            Assert.That(stateUsageWithDo.ComputeDoAction(), Is.SameAs(doAction));

            // All three kinds present → the Kind filter picks the Do membership's action; Entry and Exit excluded.
            var stateUsageAllKinds = new StateUsage();
            var allKindsEntry = new ActionUsage();
            var allKindsDo = new ActionUsage();
            var allKindsExit = new ActionUsage();
            stateUsageAllKinds.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Entry }, allKindsEntry);
            stateUsageAllKinds.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Do }, allKindsDo);
            stateUsageAllKinds.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Exit }, allKindsExit);

            Assert.That(stateUsageAllKinds.ComputeDoAction(), Is.SameAs(allKindsDo));
        }

        [Test]
        public void VerifyComputeEntryAction()
        {
            Assert.That(() => ((IStateUsage)null).ComputeEntryAction(), Throws.TypeOf<ArgumentNullException>());

            // Empty: no StateSubactionMembership at all → null.
            var emptyStateUsage = new StateUsage();

            Assert.That(emptyStateUsage.ComputeEntryAction(), Is.Null);

            // Wrong kind: one StateSubactionMembership of kind Do only → null (action is never accessed).
            var stateUsageWithDo = new StateUsage();
            var doAction = new ActionUsage();
            stateUsageWithDo.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Do }, doAction);

            Assert.That(stateUsageWithDo.ComputeEntryAction(), Is.Null);

            // Matching kind: one StateSubactionMembership of kind Entry → returns the wired ActionUsage.
            var stateUsageWithEntry = new StateUsage();
            var entryAction = new ActionUsage();
            stateUsageWithEntry.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Entry }, entryAction);

            Assert.That(stateUsageWithEntry.ComputeEntryAction(), Is.SameAs(entryAction));

            // All three kinds present → the Kind filter picks the Entry membership's action; Do and Exit excluded.
            var stateUsageAllKinds = new StateUsage();
            var allKindsEntry = new ActionUsage();
            var allKindsDo = new ActionUsage();
            var allKindsExit = new ActionUsage();
            stateUsageAllKinds.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Entry }, allKindsEntry);
            stateUsageAllKinds.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Do }, allKindsDo);
            stateUsageAllKinds.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Exit }, allKindsExit);

            Assert.That(stateUsageAllKinds.ComputeEntryAction(), Is.SameAs(allKindsEntry));
        }

        [Test]
        public void VerifyComputeExitAction()
        {
            Assert.That(() => ((IStateUsage)null).ComputeExitAction(), Throws.TypeOf<ArgumentNullException>());

            // Empty: no StateSubactionMembership at all → null.
            var emptyStateUsage = new StateUsage();

            Assert.That(emptyStateUsage.ComputeExitAction(), Is.Null);

            // Wrong kind: one StateSubactionMembership of kind Do only → null (action is never accessed).
            var stateUsageWithDo = new StateUsage();
            var doAction = new ActionUsage();
            stateUsageWithDo.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Do }, doAction);

            Assert.That(stateUsageWithDo.ComputeExitAction(), Is.Null);

            // Matching kind: one StateSubactionMembership of kind Exit → returns the wired ActionUsage.
            var stateUsageWithExit = new StateUsage();
            var exitAction = new ActionUsage();
            stateUsageWithExit.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Exit }, exitAction);

            Assert.That(stateUsageWithExit.ComputeExitAction(), Is.SameAs(exitAction));

            // All three kinds present → the Kind filter picks the Exit membership's action; Entry and Do excluded.
            var stateUsageAllKinds = new StateUsage();
            var allKindsEntry = new ActionUsage();
            var allKindsDo = new ActionUsage();
            var allKindsExit = new ActionUsage();
            stateUsageAllKinds.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Entry }, allKindsEntry);
            stateUsageAllKinds.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Do }, allKindsDo);
            stateUsageAllKinds.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Exit }, allKindsExit);

            Assert.That(stateUsageAllKinds.ComputeExitAction(), Is.SameAs(allKindsExit));
        }

        [Test]
        public void VerifyComputeIsSubstateUsageOperation()
        {
            Assert.That(() => ((IStateUsage)null).ComputeIsSubstateUsageOperation(false), Throws.TypeOf<ArgumentNullException>());

            // IsComposite = false → false regardless of other conditions.
            var nonCompositeStateUsage = new StateUsage { IsComposite = false };

            Assert.That(nonCompositeStateUsage.ComputeIsSubstateUsageOperation(false), Is.False);

            // IsComposite = true, owningType = null (no owner) → false.
            var orphanStateUsage = new StateUsage { IsComposite = true };

            Assert.That(orphanStateUsage.ComputeIsSubstateUsageOperation(false), Is.False);

            // owningType is a StateDefinition with IsParallel = true, isParallel = true,
            // owned via plain FeatureMembership (not StateSubactionMembership) → true.
            var parentStateDefinition = new StateDefinition { IsParallel = true };
            var substateUnderDefinition = new StateUsage { IsComposite = true };
            parentStateDefinition.AssignOwnership(new FeatureMembership(), substateUnderDefinition);

            Assert.That(substateUnderDefinition.ComputeIsSubstateUsageOperation(true), Is.True);

            // owningType is a StateDefinition with IsParallel = false, isParallel = true → false (mismatch).
            var nonParallelDefinition = new StateDefinition { IsParallel = false };
            var substateParallelMismatch = new StateUsage { IsComposite = true };
            nonParallelDefinition.AssignOwnership(new FeatureMembership(), substateParallelMismatch);

            Assert.That(substateParallelMismatch.ComputeIsSubstateUsageOperation(true), Is.False);

            // owningType is a StateUsage with IsParallel = false, isParallel = false,
            // owned via plain FeatureMembership → true.
            var parentStateUsage = new StateUsage { IsParallel = false };
            var substateUnderUsage = new StateUsage { IsComposite = true };
            parentStateUsage.AssignOwnership(new FeatureMembership(), substateUnderUsage);

            Assert.That(substateUnderUsage.ComputeIsSubstateUsageOperation(false), Is.True);

            // All conditions met but owningFeatureMembership is a StateSubactionMembership → false.
            var parallelDefinition = new StateDefinition { IsParallel = true };
            var actionSubstate = new StateUsage { IsComposite = true };
            var doMembership = new StateSubactionMembership { Kind = StateSubactionKind.Do };
            parallelDefinition.AssignOwnership(doMembership, actionSubstate);

            Assert.That(actionSubstate.ComputeIsSubstateUsageOperation(true), Is.False);

            // owningType is some other type (not StateDefinition/StateUsage) → false.
            var otherParent = new ActionDefinition();
            var substateUnderOtherType = new StateUsage { IsComposite = true };
            otherParent.AssignOwnership(new FeatureMembership(), substateUnderOtherType);

            Assert.That(substateUnderOtherType.ComputeIsSubstateUsageOperation(false), Is.False);
        }

        [Test]
        public void VerifyComputeStateDefinition()
        {
            Assert.That(() => ((IStateUsage)null).ComputeStateDefinition(), Throws.TypeOf<ArgumentNullException>());

            // Empty: no FeatureTyping in OwnedRelationship → empty list.
            var emptyStateUsage = new StateUsage();

            Assert.That(emptyStateUsage.ComputeStateDefinition(), Has.Count.EqualTo(0));

            // One FeatureTyping whose Type is a StateDefinition (which implements IBehavior) → returned.
            var stateUsageWithStateDefinition = new StateUsage();
            var stateDefinition = new StateDefinition();
            stateUsageWithStateDefinition.AssignOwnership(new FeatureTyping { Type = stateDefinition });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(stateUsageWithStateDefinition.ComputeStateDefinition(), Has.Count.EqualTo(1));
                Assert.That(stateUsageWithStateDefinition.ComputeStateDefinition(), Does.Contain(stateDefinition));
            }

            // One FeatureTyping whose Type is a plain Behavior (not IStateDefinition) → also returned (spec allows it).
            var stateUsageWithBehavior = new StateUsage();
            var plainBehavior = new Behavior();
            stateUsageWithBehavior.AssignOwnership(new FeatureTyping { Type = plainBehavior });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(stateUsageWithBehavior.ComputeStateDefinition(), Has.Count.EqualTo(1));
                Assert.That(stateUsageWithBehavior.ComputeStateDefinition(), Does.Contain(plainBehavior));
            }

            // Mixed FeatureTypings: one IBehavior and one plain Classifier (not an IBehavior) → only the IBehavior returned.
            var mixedStateUsage = new StateUsage();
            var mixedBehavior = new StateDefinition();
            var nonBehaviorType = new Classifier();
            mixedStateUsage.AssignOwnership(new FeatureTyping { Type = mixedBehavior });
            mixedStateUsage.AssignOwnership(new FeatureTyping { Type = nonBehaviorType });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(mixedStateUsage.ComputeStateDefinition(), Has.Count.EqualTo(1));
                Assert.That(mixedStateUsage.ComputeStateDefinition(), Does.Contain(mixedBehavior));
                Assert.That(mixedStateUsage.ComputeStateDefinition(), Does.Not.Contain(nonBehaviorType));
            }
        }
    }
}
