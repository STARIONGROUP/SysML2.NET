// -------------------------------------------------------------------------------------------------
// <copyright file="ActionUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.Systems.States;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ActionUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeActionDefinition()
        {
            Assert.That(() => ((IActionUsage)null).ComputeActionDefinition(), Throws.TypeOf<ArgumentNullException>());

            var emptyActionUsage = new ActionUsage();

            Assert.That(emptyActionUsage.ComputeActionDefinition(), Has.Count.EqualTo(0));

            var actionUsage = new ActionUsage();

            var firstActionDefinition = new ActionDefinition();
            actionUsage.AssignOwnership(new FeatureTyping { Type = firstActionDefinition });

            var secondActionDefinition = new ActionDefinition();
            actionUsage.AssignOwnership(new FeatureTyping { Type = secondActionDefinition });

            // A FeatureTyping whose Type is a plain Classifier (NOT an IBehavior) must be filtered out by OfType<IBehavior>().
            var classifier = new Classifier();
            actionUsage.AssignOwnership(new FeatureTyping { Type = classifier });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(actionUsage.ComputeActionDefinition(), Has.Count.EqualTo(2));
                Assert.That(actionUsage.ComputeActionDefinition(), Does.Contain(firstActionDefinition));
                Assert.That(actionUsage.ComputeActionDefinition(), Does.Contain(secondActionDefinition));
                Assert.That(actionUsage.ComputeActionDefinition(), Does.Not.Contain(classifier));
            }
        }

        [Test]
        public void VerifyComputeInputParametersOperation()
        {
            Assert.That(() => ((IActionUsage)null).ComputeInputParametersOperation(), Throws.TypeOf<ArgumentNullException>());

            var emptyActionUsage = new ActionUsage();

            Assert.That(emptyActionUsage.ComputeInputParametersOperation(), Has.Count.EqualTo(0));

            var actionUsage = new ActionUsage();

            var firstParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            var firstMembership = new FeatureMembership();
            actionUsage.AssignOwnership(firstMembership, firstParameter);

            var secondParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            var secondMembership = new FeatureMembership();
            actionUsage.AssignOwnership(secondMembership, secondParameter);

            // For later: inherited-vs-owned branch requires a Specialization wiring; covered indirectly.
            using (Assert.EnterMultipleScope())
            {
                Assert.That(actionUsage.ComputeInputParametersOperation(), Has.Count.EqualTo(2));
                Assert.That(actionUsage.ComputeInputParametersOperation(), Does.Contain(firstParameter));
                Assert.That(actionUsage.ComputeInputParametersOperation(), Does.Contain(secondParameter));
            }
        }

        [Test]
        public void VerifyComputeInputParameterOperation()
        {
            Assert.That(() => ((IActionUsage)null).ComputeInputParameterOperation(1), Throws.TypeOf<ArgumentNullException>());

            var emptyActionUsage = new ActionUsage();

            Assert.That(emptyActionUsage.ComputeInputParameterOperation(1), Is.Null);

            var actionUsage = new ActionUsage();

            var firstParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            var firstMembership = new FeatureMembership();
            actionUsage.AssignOwnership(firstMembership, firstParameter);

            var secondParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            var secondMembership = new FeatureMembership();
            actionUsage.AssignOwnership(secondMembership, secondParameter);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(actionUsage.ComputeInputParameterOperation(0), Is.Null);
                Assert.That(actionUsage.ComputeInputParameterOperation(-1), Is.Null);
                Assert.That(actionUsage.ComputeInputParameterOperation(1), Is.SameAs(firstParameter));
                Assert.That(actionUsage.ComputeInputParameterOperation(2), Is.SameAs(secondParameter));
                Assert.That(actionUsage.ComputeInputParameterOperation(3), Is.Null);
            }

            var singleParameterActionUsage = new ActionUsage();
            var lonelyParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            var lonelyMembership = new FeatureMembership();
            singleParameterActionUsage.AssignOwnership(lonelyMembership, lonelyParameter);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(singleParameterActionUsage.ComputeInputParameterOperation(1), Is.SameAs(lonelyParameter));
                Assert.That(singleParameterActionUsage.ComputeInputParameterOperation(2), Is.Null);
            }
        }

        [Test]
        public void VerifyComputeArgumentOperation()
        {
            Assert.That(() => ((IActionUsage)null).ComputeArgumentOperation(1), Throws.TypeOf<ArgumentNullException>());

            var emptyActionUsage = new ActionUsage();

            Assert.That(emptyActionUsage.ComputeArgumentOperation(1), Is.Null);

            // ActionUsage with one input parameter that has no FeatureValue membership.
            var bareParameterActionUsage = new ActionUsage();
            var bareParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            var bareMembership = new FeatureMembership();
            bareParameterActionUsage.AssignOwnership(bareMembership, bareParameter);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(bareParameterActionUsage.ComputeArgumentOperation(0), Is.Null);
                Assert.That(bareParameterActionUsage.ComputeArgumentOperation(-1), Is.Null);
                Assert.That(bareParameterActionUsage.ComputeArgumentOperation(1), Is.Null);
            }

            // ActionUsage with one input parameter owning a FeatureValue with a non-null IExpression value.
            var actionUsage = new ActionUsage();
            var inputParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            var inputMembership = new FeatureMembership();
            actionUsage.AssignOwnership(inputMembership, inputParameter);

            var expression = new LiteralInteger();
            var featureValue = new FeatureValue();
            inputParameter.AssignOwnership(featureValue, expression);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(actionUsage.ComputeArgumentOperation(1), Is.SameAs(expression));
                Assert.That(actionUsage.ComputeArgumentOperation(2), Is.Null);
            }
        }

        [Test]
        public void VerifyComputeIsSubactionUsageOperation()
        {
            Assert.That(() => ((IActionUsage)null).ComputeIsSubactionUsageOperation(), Throws.TypeOf<ArgumentNullException>());

            // Branch 1: IsComposite = false -> false.
            var nonCompositeActionUsage = new ActionUsage { IsComposite = false };

            Assert.That(nonCompositeActionUsage.ComputeIsSubactionUsageOperation(), Is.False);

            // Branch 2: composite, no owner -> false (owningType is null).
            var orphanCompositeActionUsage = new ActionUsage { IsComposite = true };

            Assert.That(orphanCompositeActionUsage.ComputeIsSubactionUsageOperation(), Is.False);

            // Branch 3: composite, owningType is a PartDefinition (not ActionDef/ActionUsage) -> false.
            var partDefinitionOwner = new PartDefinition();
            var partOwnedActionUsage = new ActionUsage { IsComposite = true };
            partDefinitionOwner.AssignOwnership(new FeatureMembership(), partOwnedActionUsage);

            Assert.That(partOwnedActionUsage.ComputeIsSubactionUsageOperation(), Is.False);

            // Branch 4: composite, owned by ActionDefinition via plain FeatureMembership -> true.
            var actionDefinitionOwner = new ActionDefinition();
            var actionDefOwnedActionUsage = new ActionUsage { IsComposite = true };
            actionDefinitionOwner.AssignOwnership(new FeatureMembership(), actionDefOwnedActionUsage);

            Assert.That(actionDefOwnedActionUsage.ComputeIsSubactionUsageOperation(), Is.True);

            // Branch 5: composite, owned by ActionUsage parent via StateSubactionMembership { Kind = Do } -> true.
            var actionUsageParent = new ActionUsage();
            var doSubaction = new ActionUsage { IsComposite = true };
            actionUsageParent.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Do }, doSubaction);

            Assert.That(doSubaction.ComputeIsSubactionUsageOperation(), Is.True);

            // Branch 6: composite, owned by ActionDefinition via StateSubactionMembership { Kind = Entry } -> false.
            var entryOwner = new ActionDefinition();
            var entrySubaction = new ActionUsage { IsComposite = true };
            entryOwner.AssignOwnership(new StateSubactionMembership { Kind = StateSubactionKind.Entry }, entrySubaction);

            Assert.That(entrySubaction.ComputeIsSubactionUsageOperation(), Is.False);
        }
    }
}
