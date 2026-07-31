// -------------------------------------------------------------------------------------------------
// <copyright file="AssignmentActionUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Kernel.Metadata;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class AssignmentActionUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeReferent()
        {
            Assert.That(() => ((IAssignmentActionUsage)null).ComputeReferent(), Throws.TypeOf<ArgumentNullException>());

            // No ownedMembership at all -> null.
            var empty = new AssignmentActionUsage();

            // A feature owned via a FeatureMembership is rejected by reject(FeatureMembership) -> null.
            var featureMembershipOnly = new AssignmentActionUsage();
            featureMembershipOnly.AssignOwnership(new FeatureMembership(), new Feature());

            // A non-FeatureMembership membership whose memberElement is a MetadataFeature is excluded
            // by the 'not MetadataFeature' filter -> null.
            var metadataOnly = new AssignmentActionUsage();
            var metadataMembership = new Membership();
            metadataOnly.AssignOwnership(metadataMembership);
            metadataMembership.MemberElement = new MetadataFeature();

            // A non-FeatureMembership membership whose memberElement is a plain Feature -> returned.
            var referentFeature = new Feature();
            var positive = new AssignmentActionUsage();
            var referentMembership = new Membership();
            positive.AssignOwnership(referentMembership);
            referentMembership.MemberElement = referentFeature;

            // Ordering: the first qualifying membership in ownedMembership order wins.
            var firstFeature = new Feature();
            var secondFeature = new Feature();
            var ordered = new AssignmentActionUsage();
            var firstMembership = new Membership();
            ordered.AssignOwnership(firstMembership);
            firstMembership.MemberElement = firstFeature;
            var secondMembership = new Membership();
            ordered.AssignOwnership(secondMembership);
            secondMembership.MemberElement = secondFeature;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(empty.ComputeReferent(), Is.Null);
                Assert.That(featureMembershipOnly.ComputeReferent(), Is.Null);
                Assert.That(metadataOnly.ComputeReferent(), Is.Null);
                Assert.That(positive.ComputeReferent(), Is.SameAs(referentFeature));
                Assert.That(ordered.ComputeReferent(), Is.SameAs(firstFeature));
            }
        }

        [Test]
        public void VerifyComputeTargetArgument()
        {
            Assert.That(() => ((IAssignmentActionUsage)null).ComputeTargetArgument(), Throws.TypeOf<ArgumentNullException>());

            // No input parameters -> argument(1) is null -> null.
            Assert.That(new AssignmentActionUsage().ComputeTargetArgument(), Is.Null);

            var targetExpression = new LiteralInteger();
            var valueExpression = new LiteralInteger();
            var assignmentActionUsage = CreateAssignmentActionUsageWithArguments(targetExpression, valueExpression);

            using (Assert.EnterMultipleScope())
            {
                // targetArgument = argument(1)
                Assert.That(assignmentActionUsage.ComputeTargetArgument(), Is.SameAs(targetExpression));
                Assert.That(assignmentActionUsage.ComputeTargetArgument(), Is.Not.SameAs(valueExpression));
            }
        }

        [Test]
        public void VerifyComputeValueExpression()
        {
            Assert.That(() => ((IAssignmentActionUsage)null).ComputeValueExpression(), Throws.TypeOf<ArgumentNullException>());

            // No input parameters -> argument(2) is null -> null.
            Assert.That(new AssignmentActionUsage().ComputeValueExpression(), Is.Null);

            var targetExpression = new LiteralInteger();
            var valueExpression = new LiteralInteger();
            var assignmentActionUsage = CreateAssignmentActionUsageWithArguments(targetExpression, valueExpression);

            // Only a single input parameter present -> argument(2) is out of range -> null.
            var singleArgumentAssignmentActionUsage = CreateAssignmentActionUsageWithArguments(new LiteralInteger());

            using (Assert.EnterMultipleScope())
            {
                // valueExpression = argument(2)
                Assert.That(assignmentActionUsage.ComputeValueExpression(), Is.SameAs(valueExpression));
                Assert.That(assignmentActionUsage.ComputeValueExpression(), Is.Not.SameAs(targetExpression));
                Assert.That(singleArgumentAssignmentActionUsage.ComputeValueExpression(), Is.Null);
            }
        }

        /// <summary>
        /// Builds an <see cref="AssignmentActionUsage" /> whose i-th owned input parameter carries a
        /// <see cref="FeatureValue" /> whose value is the i-th supplied argument expression, so that
        /// <c>Argument(i)</c> resolves to that expression.
        /// </summary>
        private static AssignmentActionUsage CreateAssignmentActionUsageWithArguments(params LiteralInteger[] argumentExpressions)
        {
            var assignmentActionUsage = new AssignmentActionUsage();

            foreach (var argumentExpression in argumentExpressions)
            {
                var inputParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
                assignmentActionUsage.AssignOwnership(new FeatureMembership(), inputParameter);
                inputParameter.AssignOwnership(new FeatureValue(), argumentExpression);
            }

            return assignmentActionUsage;
        }
    }
}
