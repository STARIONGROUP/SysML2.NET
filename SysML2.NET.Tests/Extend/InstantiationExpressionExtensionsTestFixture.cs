// -------------------------------------------------------------------------------------------------
// <copyright file="InstantiationExpressionExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class InstantiationExpressionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeArgument()
        {
            // Branch 1: null subject -> ArgumentNullException.
            Assert.That(
                () => ((IInstantiationExpression)null).ComputeArgument(),
                Throws.TypeOf<ArgumentNullException>());

            // Branch 2: instantiatedType is unresolved (no non-FeatureMembership member) -> short-circuits to [].
            var emptySubject = new InvocationExpression();

            Assert.That(emptySubject.ComputeArgument(), Is.Empty);

            // Branch 3 (invocation family): iterate instantiatedType.input, match against subject.ownedFeature
            // that redefines the input; collect its FeatureValue.value expressions.
            var invocation = new InvocationExpression();

            var invokedBehavior = new Behavior();
            invocation.AssignOwnership(new OwningMembership(), invokedBehavior);

            var inputParameter = new Feature { Direction = FeatureDirectionKind.In };
            invokedBehavior.AssignOwnership(new FeatureMembership(), inputParameter);

            var invocationArgument = new LiteralInteger();
            var argumentFeature = new Feature();
            argumentFeature.AssignOwnership(new Redefinition { RedefinedFeature = inputParameter });
            argumentFeature.AssignOwnership(new FeatureValue(), invocationArgument);
            invocation.AssignOwnership(new FeatureMembership(), argumentFeature);

            // Branch 4 (constructor family): iterate instantiatedType.feature, match against result.ownedFeature
            // that redefines the feature; collect its FeatureValue.value expressions.
            var constructor = new ConstructorExpression();

            var constructedBehavior = new Behavior();
            constructor.AssignOwnership(new OwningMembership(), constructedBehavior);

            var constructedFeature = new Feature();
            constructedBehavior.AssignOwnership(new FeatureMembership(), constructedFeature);

            var resultParameter = new Feature();
            constructor.AssignOwnership(new ReturnParameterMembership(), resultParameter);

            var constructorArgument = new LiteralInteger();
            var resultRedefiningFeature = new Feature();
            resultRedefiningFeature.AssignOwnership(new Redefinition { RedefinedFeature = constructedFeature });
            resultRedefiningFeature.AssignOwnership(new FeatureValue(), constructorArgument);
            resultParameter.AssignOwnership(new FeatureMembership(), resultRedefiningFeature);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(invocation.ComputeArgument(), Is.EqualTo(new[] { invocationArgument }));
                Assert.That(constructor.ComputeArgument(), Is.EqualTo(new[] { constructorArgument }));
            }
        }

        [Test]
        public void VerifyComputeInstantiatedType()
        {
            // Branch 1: null subject -> ArgumentNullException.
            Assert.That(
                () => ((IInstantiationExpression)null).ComputeInstantiatedType(),
                Throws.TypeOf<ArgumentNullException>());

            // Branch 2: empty model -> nothing to resolve -> null.
            var emptySubject = new InvocationExpression();

            Assert.That(emptySubject.ComputeInstantiatedType(), Is.Null);

            // Branch 3: delegates to InstantiatedType(); an InvocationExpression routes that operation to
            // ComputeInstantiatedTypeOperation, whose first non-FeatureMembership member is a Type.
            var subject = new InvocationExpression();
            var behavior = new Behavior();
            subject.AssignOwnership(new OwningMembership(), behavior);

            Assert.That(subject.ComputeInstantiatedType(), Is.SameAs(behavior));
        }

        [Test]
        public void VerifyComputeInstantiatedTypeOperation()
        {
            // Branch 1: null subject -> ArgumentNullException.
            Assert.That(
                () => ((IInstantiationExpression)null).ComputeInstantiatedTypeOperation(),
                Throws.TypeOf<ArgumentNullException>());

            // Branch 2: no ownedMembership at all -> null.
            var emptySubject = new InvocationExpression();

            Assert.That(emptySubject.ComputeInstantiatedTypeOperation(), Is.Null);

            // Branch 3: only a FeatureMembership -> rejected by reject(FeatureMembership) -> null.
            var featureOnlySubject = new InvocationExpression();
            featureOnlySubject.AssignOwnership(new FeatureMembership(), new Feature());

            Assert.That(featureOnlySubject.ComputeInstantiatedTypeOperation(), Is.Null);

            // Branch 4: first non-FeatureMembership member IS a Type -> returned.
            var typeSubject = new InvocationExpression();
            var behavior = new Behavior();
            typeSubject.AssignOwnership(new OwningMembership(), behavior);

            // Branch 5: first non-FeatureMembership member is NOT a Type -> null (the oclIsKindOf(Type) guard).
            var nonTypeSubject = new InvocationExpression();
            var nonTypeMembership = new Membership();
            nonTypeSubject.AssignOwnership(nonTypeMembership);
            nonTypeMembership.MemberElement = new Namespace();

            // Branch 6 (ordering + reject): a leading FeatureMembership is skipped so the later
            // non-FeatureMembership Type is chosen -> returns that Type.
            var orderedSubject = new InvocationExpression();
            orderedSubject.AssignOwnership(new FeatureMembership(), new Feature());
            var orderedBehavior = new Behavior();
            orderedSubject.AssignOwnership(new OwningMembership(), orderedBehavior);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(typeSubject.ComputeInstantiatedTypeOperation(), Is.SameAs(behavior));
                Assert.That(nonTypeSubject.ComputeInstantiatedTypeOperation(), Is.Null);
                Assert.That(orderedSubject.ComputeInstantiatedTypeOperation(), Is.SameAs(orderedBehavior));
            }
        }
    }
}
