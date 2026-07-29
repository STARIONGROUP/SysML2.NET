    // -------------------------------------------------------------------------------------------------
// <copyright file="FeatureReferenceExpressionExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Kernel.Metadata;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class FeatureReferenceExpressionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeReferent()
        {
            // Branch 1: null subject → ArgumentNullException.
            Assert.That(
                () => ((IFeatureReferenceExpression)null).ComputeReferent(),
                Throws.TypeOf<ArgumentNullException>());

            // Branch 2: no ownedMembership at all → null.
            var subject = new FeatureReferenceExpression();

            Assert.That(subject.ComputeReferent(), Is.Null);

            // Branch 3: only a ParameterMembership → rejected by the reject(ParameterMembership) filter → null.
            var parameterFeature = new Feature();
            var parameterMembership = new ParameterMembership();
            subject.AssignOwnership(parameterMembership, parameterFeature);

            Assert.That(subject.ComputeReferent(), Is.Null);

            // Branch 4: first non-parameter membership whose MemberElement is NOT a Feature → null.
            var nonFeatureSubject = new FeatureReferenceExpression();
            var nonFeatureMembership = new Membership();
            nonFeatureSubject.AssignOwnership(nonFeatureMembership);
            nonFeatureMembership.MemberElement = new Namespace();

            Assert.That(nonFeatureSubject.ComputeReferent(), Is.Null);

            // Branch 5: first non-parameter membership whose MemberElement IS a Feature → returns that Feature.
            var referentFeature = new Feature();
            var featureMembership = new Membership();
            var positiveSubject = new FeatureReferenceExpression();
            positiveSubject.AssignOwnership(featureMembership);
            featureMembership.MemberElement = referentFeature;

            Assert.That(positiveSubject.ComputeReferent(), Is.SameAs(referentFeature));

            // Branch 6: ParameterMembership first, then a plain Membership with a Feature → the
            // ParameterMembership is rejected; the Membership is the first non-parameter one → its Feature.
            var mixedSubject = new FeatureReferenceExpression();
            var mixedParamFeature = new Feature();
            var mixedParamMembership = new ParameterMembership();
            mixedSubject.AssignOwnership(mixedParamMembership, mixedParamFeature);

            var mixedReferentFeature = new Feature();
            var mixedMembership = new Membership();
            mixedSubject.AssignOwnership(mixedMembership);
            mixedMembership.MemberElement = mixedReferentFeature;

            Assert.That(mixedSubject.ComputeReferent(), Is.SameAs(mixedReferentFeature));
        }

        [Test]
        public void VerifyComputeRedefinedModelLevelEvaluableOperation()
        {
            // Branch 1: null subject → ArgumentNullException.
            Assert.That(
                () => ((IFeatureReferenceExpression)null).ComputeRedefinedModelLevelEvaluableOperation([]),
                Throws.TypeOf<ArgumentNullException>());

            // Branch 2: referent == null (only a ParameterMembership, which ComputeReferent rejects) → true.
            var noReferentSubject = new FeatureReferenceExpression();
            var parameterFeature = new Feature();
            var parameterMembership = new ParameterMembership();
            noReferentSubject.AssignOwnership(parameterMembership, parameterFeature);

            // Branch 3: referent is a plain Feature with no featuringType and no FeatureValue → falls through
            //           to "value expression is null → true".
            var plainReferent = new Feature();
            var plainMembership = new Membership();
            var plainSubject = new FeatureReferenceExpression();
            plainSubject.AssignOwnership(plainMembership);
            plainMembership.MemberElement = plainReferent;

            // Branch 4: visited already contains the referent → cycle guard → false.
            var visitedReferent = new Feature();
            var visitedMembership = new Membership();
            var visitedSubject = new FeatureReferenceExpression();
            visitedSubject.AssignOwnership(visitedMembership);
            visitedMembership.MemberElement = visitedReferent;

            // Branch 5: referent.owningType is a Metaclass → true.
            var metaclassReferent = new Feature();
            var metaclass = new Metaclass();
            metaclass.AssignOwnership(new FeatureMembership(), metaclassReferent);
            var metaclassRefMembership = new Membership();
            var metaclassSubject = new FeatureReferenceExpression();
            metaclassSubject.AssignOwnership(metaclassRefMembership);
            metaclassRefMembership.MemberElement = metaclassReferent;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(noReferentSubject.ComputeRedefinedModelLevelEvaluableOperation([]), Is.True);
                Assert.That(plainSubject.ComputeRedefinedModelLevelEvaluableOperation([]), Is.True);
                Assert.That(visitedSubject.ComputeRedefinedModelLevelEvaluableOperation([visitedReferent]), Is.False);
                Assert.That(metaclassSubject.ComputeRedefinedModelLevelEvaluableOperation([]), Is.True);
            }
        }

        [Test]
        public void VerifyComputeRedefinedEvaluateOperation()
        {
            // Branch 1: null subject → ArgumentNullException.
            Assert.That(
                () => ((IFeatureReferenceExpression)null).ComputeRedefinedEvaluateOperation(null),
                Throws.TypeOf<ArgumentNullException>());

            // Branch 2: target is null (not a Type) → empty sequence.
            var subject = new FeatureReferenceExpression();

            Assert.That(subject.ComputeRedefinedEvaluateOperation(null), Is.Empty);

            // Branch 3: target is a non-Type element (a Comment) → empty sequence.
            Assert.That(subject.ComputeRedefinedEvaluateOperation(new Comment()), Is.Empty);

            // Branch 4: target IS a Type, but none of its features redefine the referent, and the
            //           referent has an empty featuringType → falls to else → returns [referent].
            var referent = new Feature();
            var referentMembership = new Membership();
            var referentSubject = new FeatureReferenceExpression();
            referentSubject.AssignOwnership(referentMembership);
            referentMembership.MemberElement = referent;

            var emptyType = new Type();
            var referentResult = referentSubject.ComputeRedefinedEvaluateOperation(emptyType);

            // Branch 5: target IS a Type, no feature redefines the referent, and the referent HAS a
            //           non-empty featuringType (via a TypeFeaturing) → falls to else → empty sequence.
            var featuredReferent = new Feature();
            var featuringType = new Type();
            featuredReferent.AssignOwnership(new TypeFeaturing { FeatureOfType = featuredReferent, FeaturingType = featuringType });

            var featuredMembership = new Membership();
            var featuredSubject = new FeatureReferenceExpression();
            featuredSubject.AssignOwnership(featuredMembership);
            featuredMembership.MemberElement = featuredReferent;

            // Branch 6 (recursion): target IS a Type whose feature redefines the referent and carries a
            //           FeatureValue whose value is a LiteralExpression. FeatureReferenceExpression.Evaluate
            //           recurses into the value Expression's Evaluate; LiteralExpression.Evaluate IS
            //           implemented (returns Sequence{self}), so this asserts the real evaluated result.
            var recursionReferent = new Feature();
            var recursionMembership = new Membership();
            var recursionSubject = new FeatureReferenceExpression();
            recursionSubject.AssignOwnership(recursionMembership);
            recursionMembership.MemberElement = recursionReferent;

            var redefiningFeature = new Feature();
            redefiningFeature.AssignOwnership(new Redefinition { RedefinedFeature = recursionReferent });

            var literalValue = new LiteralBoolean();
            redefiningFeature.AssignOwnership(new FeatureValue(), literalValue);

            var targetType = new Type();
            targetType.AssignOwnership(new FeatureMembership(), redefiningFeature);

            var recursionResult = recursionSubject.ComputeRedefinedEvaluateOperation(targetType);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(referentResult, Has.Count.EqualTo(1));
                Assert.That(referentResult[0], Is.SameAs(referent));
                Assert.That(featuredSubject.ComputeRedefinedEvaluateOperation(new Type()), Is.Empty);
                Assert.That(recursionResult, Has.Count.EqualTo(1));
                Assert.That(recursionResult[0], Is.SameAs(literalValue));
            }
        }
    }
}
