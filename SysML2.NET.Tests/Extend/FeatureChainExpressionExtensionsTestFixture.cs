// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureChainExpressionExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class FeatureChainExpressionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeSourceTargetFeatureOperation()
        {
            // Branch 1: null subject → ArgumentNullException.
            Assert.That(
                () => ((IFeatureChainExpression)null).ComputeSourceTargetFeatureOperation(),
                Throws.TypeOf<ArgumentNullException>());

            // Branch 2: no FeatureMembership → ownedFeature is empty → null.
            var featureChainExpression = new FeatureChainExpression();

            Assert.That(featureChainExpression.ComputeSourceTargetFeatureOperation(), Is.Null);

            // Branch 3: ownedFeature non-empty but all features have wrong direction (Out) → no In-direction match → null.
            var outFeature = new Feature { Direction = FeatureDirectionKind.Out };
            var outFeatureMembership = new FeatureMembership();
            featureChainExpression.AssignOwnership(outFeatureMembership, outFeature);

            Assert.That(featureChainExpression.ComputeSourceTargetFeatureOperation(), Is.Null);

            // Branch 4: first In-direction feature has empty ownedFeature → null.
            var inFeatureNoNested = new Feature { Direction = FeatureDirectionKind.In };
            var inMembershipNoNested = new FeatureMembership();
            featureChainExpression.AssignOwnership(inMembershipNoNested, inFeatureNoNested);

            Assert.That(featureChainExpression.ComputeSourceTargetFeatureOperation(), Is.Null);

            // Branch 5: first In-direction feature has one ownedFeature → returns that nested Feature.
            var inputParam = new Feature { Direction = FeatureDirectionKind.In };
            var inputParamMembership = new FeatureMembership();

            var nestedFeature = new Feature();
            var nestedFeatureMembership = new FeatureMembership();
            inputParam.AssignOwnership(nestedFeatureMembership, nestedFeature);

            var positiveSubject = new FeatureChainExpression();
            positiveSubject.AssignOwnership(inputParamMembership, inputParam);

            Assert.That(positiveSubject.ComputeSourceTargetFeatureOperation(), Is.SameAs(nestedFeature));

            // Branch 6: first In-direction feature has multiple ownedFeatures → returns the FIRST one (insertion order).
            var multiInputParam = new Feature { Direction = FeatureDirectionKind.In };
            var multiInputParamMembership = new FeatureMembership();

            var firstNestedFeature = new Feature();
            var firstNestedMembership = new FeatureMembership();
            multiInputParam.AssignOwnership(firstNestedMembership, firstNestedFeature);

            var secondNestedFeature = new Feature();
            var secondNestedMembership = new FeatureMembership();
            multiInputParam.AssignOwnership(secondNestedMembership, secondNestedFeature);

            var multiSubject = new FeatureChainExpression();
            multiSubject.AssignOwnership(multiInputParamMembership, multiInputParam);

            Assert.That(multiSubject.ComputeSourceTargetFeatureOperation(), Is.SameAs(firstNestedFeature));
        }

        [Test]
        public void VerifyComputeTargetFeature()
        {
            // Branch 1: null subject → ArgumentNullException.
            Assert.That(
                () => ((IFeatureChainExpression)null).ComputeTargetFeature(),
                Throws.TypeOf<ArgumentNullException>());

            // Branch 2: no ownedMembership at all → null.
            var featureChainExpression = new FeatureChainExpression();

            Assert.That(featureChainExpression.ComputeTargetFeature(), Is.Null);

            // Branch 3: only a ParameterMembership (IOwningMembership) → rejected by the filter → null.
            var parameterFeature = new Feature { Direction = FeatureDirectionKind.In };
            var parameterMembership = new ParameterMembership();
            featureChainExpression.AssignOwnership(parameterMembership, parameterFeature);

            Assert.That(featureChainExpression.ComputeTargetFeature(), Is.Null);

            // Branch 4: non-ParameterMembership (plain Membership) whose MemberElement is not an IFeature → null.
            var membershipWithNonFeature = new Membership();
            featureChainExpression.AssignOwnership(membershipWithNonFeature);
            membershipWithNonFeature.MemberElement = new Namespace();

            Assert.That(featureChainExpression.ComputeTargetFeature(), Is.Null);

            // Branch 5: non-ParameterMembership with an IFeature MemberElement → returns that Feature.
            var targetFeature = new Feature();
            var membershipWithFeature = new Membership();
            featureChainExpression.AssignOwnership(membershipWithFeature);
            membershipWithFeature.MemberElement = targetFeature;

            // The earlier non-ParameterMembership (membershipWithNonFeature) is first in insertion order,
            // so swap to a fresh subject to isolate this positive branch.
            var positiveSubject = new FeatureChainExpression();
            var positiveMembership = new Membership();
            positiveSubject.AssignOwnership(positiveMembership);
            positiveMembership.MemberElement = targetFeature;

            Assert.That(positiveSubject.ComputeTargetFeature(), Is.SameAs(targetFeature));

            // Branch 6: ParameterMembership first, then plain Membership with Feature → PM is rejected;
            // Membership is the first non-PM; returns the Feature.
            var mixedSubject = new FeatureChainExpression();
            var mixedParamFeature = new Feature { Direction = FeatureDirectionKind.In };
            var mixedParamMembership = new ParameterMembership();
            mixedSubject.AssignOwnership(mixedParamMembership, mixedParamFeature);

            var mixedTargetFeature = new Feature();
            var mixedNonParamMembership = new Membership();
            mixedSubject.AssignOwnership(mixedNonParamMembership);
            mixedNonParamMembership.MemberElement = mixedTargetFeature;

            Assert.That(mixedSubject.ComputeTargetFeature(), Is.SameAs(mixedTargetFeature));
        }
    }
}
