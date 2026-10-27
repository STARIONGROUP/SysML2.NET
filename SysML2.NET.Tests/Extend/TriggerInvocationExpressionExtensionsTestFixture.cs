// ------------------------------------------------------------------------------------------------
// <copyright file="TriggerInvocationExpressionExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.Systems.Actions;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class TriggerInvocationExpressionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeRedefinedInstantiatedTypeOperation()
        {
            // Branch 1: null subject -> ArgumentNullException.
            Assert.That(
                () => ((ITriggerInvocationExpression)null).ComputeRedefinedInstantiatedTypeOperation(),
                Throws.TypeOf<ArgumentNullException>());

            // Branches 2-4: populated with each TriggerKind but NO loaded Kernel Semantic Library
            // "Triggers" package. The switch selects the qualified name (Triggers::TriggerWhen /
            // Triggers::TriggerAt / Triggers::TriggerAfter) for every Kind and calls resolveGlobal, which
            // returns null without a loaded library -> the method returns null for all three. This proves the
            // switch handles every enum value without throwing.
            var whenSubject = new TriggerInvocationExpression { Kind = TriggerKind.When };
            var atSubject = new TriggerInvocationExpression { Kind = TriggerKind.At };
            var afterSubject = new TriggerInvocationExpression { Kind = TriggerKind.After };

            using (Assert.EnterMultipleScope())
            {
                Assert.That(whenSubject.ComputeRedefinedInstantiatedTypeOperation(), Is.Null);
                Assert.That(atSubject.ComputeRedefinedInstantiatedTypeOperation(), Is.Null);
                Assert.That(afterSubject.ComputeRedefinedInstantiatedTypeOperation(), Is.Null);
            }

            // Branches 5-7 (positive resolveGlobal path): wire a resolvable global scope containing a
            // "Triggers" namespace with the three trigger Functions, so that for each Kind
            // ResolveGlobal("Triggers::Trigger<Kind>") returns a Membership whose MemberElement is an IType.
            // This exercises the "resolveGlobal -> ?.MemberElement as IType -> return the Type" body for
            // every switch arm (When / At / After).
            var root = new Namespace();

            var triggers = new Namespace { DeclaredName = "Triggers" };
            root.AssignOwnership(new OwningMembership { Visibility = VisibilityKind.Public }, triggers);

            var triggerWhenType = new Behavior { DeclaredName = "TriggerWhen" };
            var triggerAtType = new Behavior { DeclaredName = "TriggerAt" };
            var triggerAfterType = new Behavior { DeclaredName = "TriggerAfter" };

            triggers.AssignOwnership(new OwningMembership { Visibility = VisibilityKind.Public }, triggerWhenType);
            triggers.AssignOwnership(new OwningMembership { Visibility = VisibilityKind.Public }, triggerAtType);
            triggers.AssignOwnership(new OwningMembership { Visibility = VisibilityKind.Public }, triggerAfterType);

            // Each subject must sit under the same root so its owningNamespace chain reaches global scope.
            var resolvableWhen = new TriggerInvocationExpression { Kind = TriggerKind.When };
            var resolvableAt = new TriggerInvocationExpression { Kind = TriggerKind.At };
            var resolvableAfter = new TriggerInvocationExpression { Kind = TriggerKind.After };

            root.AssignOwnership(new OwningMembership { Visibility = VisibilityKind.Public }, resolvableWhen);
            root.AssignOwnership(new OwningMembership { Visibility = VisibilityKind.Public }, resolvableAt);
            root.AssignOwnership(new OwningMembership { Visibility = VisibilityKind.Public }, resolvableAfter);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(resolvableWhen.ComputeRedefinedInstantiatedTypeOperation(), Is.SameAs(triggerWhenType));
                Assert.That(resolvableAt.ComputeRedefinedInstantiatedTypeOperation(), Is.SameAs(triggerAtType));
                Assert.That(resolvableAfter.ComputeRedefinedInstantiatedTypeOperation(), Is.SameAs(triggerAfterType));
            }
        }
    }
}
