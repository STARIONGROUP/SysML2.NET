// -------------------------------------------------------------------------------------------------
// <copyright file="ExpressionExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;

    using NUnit.Framework;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ExpressionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeFunction()
        {
            Assert.That(() => ((IExpression)null).ComputeFunction(), Throws.TypeOf<ArgumentNullException>());

            var expression = new Expression();

            // Empty: no typings → null.
            Assert.That(expression.ComputeFunction(), Is.Null);

            // Negative: FeatureTyping pointing at a non-Function Type → null.
            var nonFunctionType = new SysML2.NET.Core.POCO.Core.Types.Type();
            var typingToNonFunction = new FeatureTyping { Type = nonFunctionType };
            expression.AssignOwnership(typingToNonFunction);

            Assert.That(expression.ComputeFunction(), Is.Null);

            // Positive: FeatureTyping pointing at a Function → that Function returned.
            var function1 = new Function();
            var typingToFunction1 = new FeatureTyping { Type = function1 };
            expression.AssignOwnership(typingToFunction1);

            Assert.That(expression.ComputeFunction(), Is.SameAs(function1));

            // Multiple function typings (pathological but must not throw) → first returned.
            var function2 = new Function();
            var typingToFunction2 = new FeatureTyping { Type = function2 };
            expression.AssignOwnership(typingToFunction2);

            Assert.That(expression.ComputeFunction(), Is.SameAs(function1));
        }

        [Test]
        public void VerifyComputeIsModelLevelEvaluable()
        {
            Assert.That(() => ((IExpression)null).ComputeIsModelLevelEvaluable(), Throws.TypeOf<ArgumentNullException>());

            var expression = new Expression();

            // Empty/default: no ownedSpecialization, no ownedFeature → true (vacuous forAll).
            Assert.That(expression.ComputeIsModelLevelEvaluable(), Is.True);

            // One implied specialization (IsImplied = true) → still true.
            var impliedSpecialization = new Specialization { IsImplied = true, Specific = expression };
            expression.AssignOwnership(impliedSpecialization);

            Assert.That(expression.ComputeIsModelLevelEvaluable(), Is.True);

            // One non-implied specialization → false (early return from forAll).
            var expressionWithNonImplied = new Expression();
            var nonImpliedSpecialization = new Specialization { IsImplied = false, Specific = expressionWithNonImplied };
            expressionWithNonImplied.AssignOwnership(nonImpliedSpecialization);

            Assert.That(expressionWithNonImplied.ComputeIsModelLevelEvaluable(), Is.False);
        }

        [Test]
        public void VerifyComputeResult()
        {
            Assert.That(() => ((IExpression)null).ComputeResult(), Throws.TypeOf<ArgumentNullException>());

            var expression = new Expression();

            // Empty: no featureMembership → null.
            Assert.That(expression.ComputeResult(), Is.Null);

            // Negative: FeatureMembership with non-ReturnParameterMembership → null.
            var featureMembership = new FeatureMembership();
            var plainFeature = new Feature();
            expression.AssignOwnership(featureMembership, plainFeature);

            Assert.That(expression.ComputeResult(), Is.Null);

            // Positive: one ReturnParameterMembership with ownedMemberParameter set → that Feature returned.
            var resultFeature = new Feature();
            var returnParamMembership = new ReturnParameterMembership();
            expression.AssignOwnership(returnParamMembership, resultFeature);

            Assert.That(expression.ComputeResult(), Is.SameAs(resultFeature));

            // Multiple ReturnParameterMemberships (illegal per OCL validation but tolerated) → first returned.
            var secondResultFeature = new Feature();
            var secondReturnParamMembership = new ReturnParameterMembership();
            expression.AssignOwnership(secondReturnParamMembership, secondResultFeature);

            Assert.That(expression.ComputeResult(), Is.SameAs(resultFeature));
        }

        [Test]
        public void VerifyComputeModelLevelEvaluableOperation()
        {
            // Null guard on subject.
            Assert.That(() => ((IExpression)null).ComputeModelLevelEvaluableOperation(new List<IFeature>()), Throws.TypeOf<ArgumentNullException>());

            var expression = new Expression();

            // visited == null must NOT throw (production coalesces null to empty list).
            Assert.That(() => expression.ComputeModelLevelEvaluableOperation(null), Throws.Nothing);

            // Empty Expression (no specializations, no features) → true.
            Assert.That(expression.ComputeModelLevelEvaluableOperation([]), Is.True);

            // One implied specialization, no features → true.
            var impliedSpec = new Specialization { IsImplied = true, Specific = expression };
            expression.AssignOwnership(impliedSpec);

            Assert.That(expression.ComputeModelLevelEvaluableOperation([]), Is.True);

            // One non-implied specialization → false (early return).
            var expressionNonImplied = new Expression();
            var nonImpliedSpec = new Specialization { IsImplied = false, Specific = expressionNonImplied };
            expressionNonImplied.AssignOwnership(nonImpliedSpec);

            Assert.That(expressionNonImplied.ComputeModelLevelEvaluableOperation([]), Is.False);

            // ownedFeature with Direction = In (BRANCH_A satisfied via in-parameter) → true.
            var expressionInParam = new Expression();
            var inParamFeature = new Feature { Direction = FeatureDirectionKind.In };
            var inParamMembership = new FeatureMembership();
            expressionInParam.AssignOwnership(inParamMembership, inParamFeature);

            Assert.That(expressionInParam.ComputeModelLevelEvaluableOperation([]), Is.True);

            // ownedFeature equal to result (owned via ReturnParameterMembership) → BRANCH_A via f = result → true.
            var expressionResult = new Expression();
            var resultFeature = new Feature();
            var returnParamMembership = new ReturnParameterMembership();
            expressionResult.AssignOwnership(returnParamMembership, resultFeature);

            Assert.That(expressionResult.ComputeModelLevelEvaluableOperation([]), Is.True);

            // ownedFeature with Direction = Out and not result, no nested features → BRANCH_A fails, BRANCH_B fails → false.
            var expressionOutParam = new Expression();
            var outFeature = new Feature { Direction = FeatureDirectionKind.Out };
            var outMembership = new FeatureMembership();
            expressionOutParam.AssignOwnership(outMembership, outFeature);

            Assert.That(expressionOutParam.ComputeModelLevelEvaluableOperation([]), Is.False);

            // ownedFeature with a nested owned feature → BRANCH_A fails (hasNoNestedFeatures = false),
            // BRANCH_B also fails (not ResultExpressionMembership) → false.
            var expressionNestedFeature = new Expression();
            var featureWithNested = new Feature { Direction = FeatureDirectionKind.In };
            var nestedChild = new Feature();
            var nestedChildMembership = new FeatureMembership();
            featureWithNested.AssignOwnership(nestedChildMembership, nestedChild);

            var outerMembership = new FeatureMembership();
            expressionNestedFeature.AssignOwnership(outerMembership, featureWithNested);

            Assert.That(expressionNestedFeature.ComputeModelLevelEvaluableOperation([]), Is.False);

            // ownedFeature with a FeatureValue — NOTE: the production code's valuation check
            // (f.valuation == null) is a known metamodel gap (IFeature has no Valuation property).
            // FeatureValue is IOwningMembership but NOT IFeatureMembership, so the contained Expression
            // is not visible via ownedFeature of featureWithValuation. BRANCH_A therefore succeeds
            // (Direction = In, ownedFeature.Count == 0) and the operation returns true.
            var expressionWithValuation = new Expression();
            var featureWithValuation = new Feature { Direction = FeatureDirectionKind.In };
            var featureValue = new FeatureValue();
            var featureValueExpr = new Expression();
            featureWithValuation.AssignOwnership(featureValue, featureValueExpr);

            var valuationMembership = new FeatureMembership();
            expressionWithValuation.AssignOwnership(valuationMembership, featureWithValuation);

            Assert.That(expressionWithValuation.ComputeModelLevelEvaluableOperation([]), Is.True);

            // BRANCH_B: ownedFeature under ResultExpressionMembership — STUB-BLOCKER.
            // Although BRANCH_B logic only accesses owningFeatureMembership and calls ModelLevelEvaluable
            // on the inner Expression, the production code first computes expressionSubject.result (line 157
            // of ExpressionExtensions.cs), which traverses featureMembership → inheritedMembership →
            // ownedFeature → ResultExpressionMembership.ownedMemberFeature → ownedResultExpression →
            // ResultExpressionMembershipExtensions.ComputeOwnedResultExpression, which is a stub.
            // Therefore the BRANCH_B recursion path cannot be exercised until that stub is implemented.
            var expressionBranchB = new Expression();
            var innerExpression = new Expression();
            var resultExprMembershipB = new ResultExpressionMembership();
            expressionBranchB.AssignOwnership(resultExprMembershipB, innerExpression);

            Assert.That(
                () => expressionBranchB.ComputeModelLevelEvaluableOperation([]),
                Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeEvaluateOperation()
        {
            // Null guard on subject.
            Assert.That(() => ((IExpression)null).ComputeEvaluateOperation(null), Throws.TypeOf<ArgumentNullException>());

            var expression = new Expression();

            // target == null is permitted for the empty branch (base body doesn't dereference target).
            Assert.That(() => expression.ComputeEvaluateOperation(null), Throws.Nothing);

            // Empty: no IResultExpressionMembership in ownedFeatureMembership → returns empty list.
            Assert.That(expression.ComputeEvaluateOperation(null), Is.Empty);

            // Discrimination: FeatureMembership with non-ResultExpressionMembership type → still empty.
            var plainMembership = new FeatureMembership();
            var plainFeature = new Feature();
            expression.AssignOwnership(plainMembership, plainFeature);

            Assert.That(expression.ComputeEvaluateOperation(null), Is.Empty);

            // STUB-BLOCKER: wire a ResultExpressionMembership → the production code calls
            // resultExpressionMembership.ownedResultExpression, which dispatches to
            // ResultExpressionMembershipExtensions.ComputeOwnedResultExpression, which is a stub
            // that throws NotSupportedException. Assert the stub propagates.
            var expressionWithRem = new Expression();
            var resultExprMembership = new ResultExpressionMembership();
            var innerExpression = new Expression();
            expressionWithRem.AssignOwnership(resultExprMembership, innerExpression);

            // ResultExpressionMembershipExtensions.ComputeOwnedResultExpression is a stub — NSE expected.
            Assert.That(
                () => expressionWithRem.ComputeEvaluateOperation(null),
                Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeCheckConditionOperation()
        {
            // Null guard on subject.
            Assert.That(() => ((IExpression)null).ComputeCheckConditionOperation(null), Throws.TypeOf<ArgumentNullException>());

            var expression = new Expression();

            // target == null: forwarded to Evaluate; empty-resultExprs branch returns [] → false (Count != 1).
            Assert.That(expression.ComputeCheckConditionOperation(null), Is.False);

            // Empty: Evaluate returns [] (no ResultExpressionMembership) → false (Count != 1).
            Assert.That(expression.ComputeCheckConditionOperation(null), Is.False);

            // STUB-BLOCKER: wire a ResultExpressionMembership → Evaluate delegates to ComputeEvaluateOperation,
            // which accesses resultExpressionMembership.ownedResultExpression →
            // ResultExpressionMembershipExtensions.ComputeOwnedResultExpression is a stub → NSE propagates.
            var expressionWithRem = new Expression();
            var resultExprMembership = new ResultExpressionMembership();
            var innerExpression = new Expression();
            expressionWithRem.AssignOwnership(resultExprMembership, innerExpression);

            // ResultExpressionMembershipExtensions.ComputeOwnedResultExpression is a stub — NSE expected.
            Assert.That(
                () => expressionWithRem.ComputeCheckConditionOperation(null),
                Throws.TypeOf<NotSupportedException>());
        }
    }
}
