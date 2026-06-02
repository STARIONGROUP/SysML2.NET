// -------------------------------------------------------------------------------------------------
// <copyright file="ReferenceUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
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
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.Systems.States;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ReferenceUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeIsReference()
        {
            Assert.That(() => ((IReferenceUsage)null).ComputeIsReference(), Throws.TypeOf<ArgumentNullException>());

            // Any non-null IReferenceUsage returns true — this is a constant-true redefinition.
            var referenceUsage = new ReferenceUsage();

            Assert.That(referenceUsage.ComputeIsReference(), Is.True);

            // Even when IsComposite is set to true, the ReferenceUsage redefinition overrides
            // the base Usage::isReference (= not isComposite) logic and still returns true.
            var compositeReferenceUsage = new ReferenceUsage { IsComposite = true };

            Assert.That(compositeReferenceUsage.ComputeIsReference(), Is.True);
        }

        [Test]
        public void VerifyComputeRedefinedNamingFeatureOperation()
        {
            Assert.That(() => ((IReferenceUsage)null).ComputeRedefinedNamingFeatureOperation(), Throws.TypeOf<ArgumentNullException>());

            // Branch 1: owningType == null — no owning FeatureMembership → falls through to
            // UsageExtensions.ComputeRedefinedNamingFeatureOperation → ComputeNamingFeatureOperation
            // → null (no IRedefinition in OwnedRelationship on a bare ReferenceUsage).
            var bareSubject = new ReferenceUsage();

            Assert.That(bareSubject.ComputeRedefinedNamingFeatureOperation(), Is.Null);

            // Branch 2: owningType is a non-ITransitionUsage (PartUsage) → OCL condition
            // owningType.oclIsKindOf(TransitionUsage) is false → falls through → null.
            var partUsageOwner = new PartUsage();
            var subjectInPartUsage = new ReferenceUsage();
            partUsageOwner.AssignOwnership(new FeatureMembership(), subjectInPartUsage);

            Assert.That(subjectInPartUsage.ComputeRedefinedNamingFeatureOperation(), Is.Null);

            // Branch 3: owningType is an ITransitionUsage but self is NOT InputParameter(2).
            // Only one input parameter is wired, so InputParameter(2) returns null (≠ self) → falls through → null.
            var transitionUsageSingleParam = new TransitionUsage();
            var onlyParam = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            transitionUsageSingleParam.AssignOwnership(new FeatureMembership(), onlyParam);

            Assert.That(onlyParam.ComputeRedefinedNamingFeatureOperation(), Is.Null);

            // Branch 4: owningType is an ITransitionUsage, self IS InputParameter(2), but
            // triggerAction is empty → TriggerPayloadParameter() returns null → result is null.
            var transitionUsageNoTrigger = new TransitionUsage();
            var firstParamNoTrigger = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            var secondParamNoTrigger = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            transitionUsageNoTrigger.AssignOwnership(new FeatureMembership(), firstParamNoTrigger);
            transitionUsageNoTrigger.AssignOwnership(new FeatureMembership(), secondParamNoTrigger);

            Assert.That(secondParamNoTrigger.ComputeRedefinedNamingFeatureOperation(), Is.Null);

            // Branch 5: owningType is an ITransitionUsage, self IS InputParameter(2), and
            // triggerAction is non-empty → TriggerPayloadParameter() accesses triggerAction[0].payloadParameter
            // → AcceptActionUsage.parameter → StepExtensions.ComputeParameter, which is still a stub.
            // Assert the stub propagates rather than silently returning the wrong value.
            var transitionUsageWithTrigger = new TransitionUsage();
            var firstParamWithTrigger = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            var secondParamWithTrigger = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            transitionUsageWithTrigger.AssignOwnership(new FeatureMembership(), firstParamWithTrigger);
            transitionUsageWithTrigger.AssignOwnership(new FeatureMembership(), secondParamWithTrigger);

            var triggerFeatureMembership = new TransitionFeatureMembership { Kind = TransitionFeatureKind.Trigger };
            var triggerAcceptAction = new AcceptActionUsage();
            transitionUsageWithTrigger.AssignOwnership(triggerFeatureMembership, triggerAcceptAction);

            Assert.That(
                () => secondParamWithTrigger.ComputeRedefinedNamingFeatureOperation(),
                Throws.TypeOf<NotSupportedException>());
        }
    }
}
