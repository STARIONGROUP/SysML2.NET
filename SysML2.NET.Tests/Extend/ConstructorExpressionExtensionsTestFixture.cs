// ------------------------------------------------------------------------------------------------
// <copyright file="ConstructorExpressionExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ConstructorExpressionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeRedefinedModelLevelEvaluableOperation()
        {
            // Null guard.
            Assert.That(
                () => ((IConstructorExpression)null).ComputeRedefinedModelLevelEvaluableOperation([]),
                Throws.TypeOf<ArgumentNullException>());

            // Empty arguments: instantiatedType is unresolved -> ComputeArgument returns [] ->
            // All(...) over an empty source -> true.
            var emptySubject = new ConstructorExpression();

            // False branch: a single argument whose ModelLevelEvaluable is deterministically false
            // (a base Expression carrying a non-implied Specialization) -> All(...) -> false.
            var falseArgument = new Expression();
            falseArgument.AssignOwnership(new Specialization { IsImplied = false, Specific = falseArgument });
            var falseSubject = BuildConstructor(falseArgument);

            // True branch: a single LiteralInteger argument (a LiteralExpression is always
            // model-level evaluable) -> All(...) -> true.
            var trueArgument = new LiteralInteger();
            var trueSubject = BuildConstructor(trueArgument);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(emptySubject.ComputeRedefinedModelLevelEvaluableOperation([]), Is.True);
                Assert.That(falseSubject.ComputeRedefinedModelLevelEvaluableOperation([]), Is.False);
                Assert.That(trueSubject.ComputeRedefinedModelLevelEvaluableOperation([]), Is.True);
            }

            // Builds a ConstructorExpression whose single ComputeArgument-derived argument is the supplied
            // expression: a constructed Behavior owns a feature, redefined by a result-owned feature whose
            // FeatureValue holds the argument (mirrors the constructor-family scaffolding in
            // InstantiationExpressionExtensionsTestFixture.VerifyComputeArgument).
            static ConstructorExpression BuildConstructor(IExpression argument)
            {
                var constructor = new ConstructorExpression();

                var constructedBehavior = new Behavior();
                constructor.AssignOwnership(new OwningMembership(), constructedBehavior);

                var constructedFeature = new Feature();
                constructedBehavior.AssignOwnership(new FeatureMembership(), constructedFeature);

                var resultParameter = new Feature();
                constructor.AssignOwnership(new ReturnParameterMembership(), resultParameter);

                var resultRedefiningFeature = new Feature();
                resultRedefiningFeature.AssignOwnership(new Redefinition { RedefinedFeature = constructedFeature });
                resultRedefiningFeature.AssignOwnership(new FeatureValue(), argument);
                resultParameter.AssignOwnership(new FeatureMembership(), resultRedefiningFeature);

                return constructor;
            }
        }
    }
}
