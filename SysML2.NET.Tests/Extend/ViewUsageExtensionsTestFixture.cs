// -------------------------------------------------------------------------------------------------
// <copyright file="ViewUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ViewUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeExposedElement()
        {
            Assert.That(() => ((IViewUsage)null).ComputeExposedElement(), Throws.TypeOf<ArgumentNullException>());

            var viewUsage = new ViewUsage();

            // Empty: no ownedImport → empty list.
            Assert.That(viewUsage.ComputeExposedElement(), Is.Empty);

            // Discrimination: non-Expose import (MembershipImport) → excluded.
            var nonExposeMembership = new MembershipImport();
            viewUsage.AssignOwnership(nonExposeMembership);

            Assert.That(viewUsage.ComputeExposedElement(), Is.Empty);

            // STUB-BLOCKER: adding a MembershipExpose dispatches ComputeExposedElement to
            // MembershipExpose.ImportedMemberships(excluded), which calls
            // MembershipImportExtensions.ComputeRedefinedImportedMembershipsOperation — a
            // NotSupportedException stub. The populated positive case cannot be tested until that
            // upstream stub is implemented.
            var membershipExpose = new MembershipExpose();
            viewUsage.AssignOwnership(membershipExpose);

            Assert.That(() => viewUsage.ComputeExposedElement(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeIncludeAsExposedOperation()
        {
            // Null guard on subject.
            Assert.That(() => ((IViewUsage)null).ComputeIncludeAsExposedOperation(new Feature()), Throws.TypeOf<ArgumentNullException>());

            var viewUsage = new ViewUsage();

            // Null guard on element parameter.
            Assert.That(() => viewUsage.ComputeIncludeAsExposedOperation(null), Throws.TypeOf<ArgumentNullException>());

            var element = new Feature();

            // Case (a) — empty conditions: ViewUsage has no ElementFilterMembership in membership;
            // forAll over empty sequence is vacuously true → returns true.
            Assert.That(viewUsage.ComputeIncludeAsExposedOperation(element), Is.True);

            // Case (a) variant: non-filter memberships present, still no ElementFilterMembership →
            // conditions remain empty → vacuously true.
            var nonFilterFeature = new Feature();
            var nonFilterMembership = new FeatureMembership();
            viewUsage.AssignOwnership(nonFilterMembership, nonFilterFeature);

            Assert.That(viewUsage.ComputeIncludeAsExposedOperation(element), Is.True);

            // Case (b) — ElementFilterMembership.condition (ComputeCondition) is implemented.
            // element has no ownedAnnotations → metadataFeatures is empty. A bare BooleanExpression
            // (no ResultExpressionMembership children) evaluates to [] → CheckCondition returns
            // false → metadataFeatures.Any(cond.CheckCondition) is false → forAll fails →
            // element is excluded → returns false.
            // Note: AnnotationExtensions.ComputeAnnotatingElement remains a NotSupportedException
            // stub (C:\CODE\SysML2.NET\SysML2.NET\Extend\AnnotationExtensions.cs), but it is not
            // reached here because element.ownedAnnotation is empty.
            var filterCondition = new BooleanExpression();
            var filterMembership = new ElementFilterMembership();
            viewUsage.AssignOwnership(filterMembership, filterCondition);

            Assert.That(viewUsage.ComputeIncludeAsExposedOperation(element), Is.False);
        }

        [Test]
        public void VerifyComputeSatisfiedViewpoint()
        {
            Assert.That(() => ((IViewUsage)null).ComputeSatisfiedViewpoint(), Throws.TypeOf<ArgumentNullException>());

            var viewUsage = new ViewUsage();

            // Empty: no nestedRequirement → empty list.
            Assert.That(viewUsage.ComputeSatisfiedViewpoint(), Is.Empty);

            // Discrimination: plain RequirementUsage (not ViewpointUsage) → excluded.
            var plainRequirement = new RequirementUsage { IsComposite = true };
            var plainRequirementMembership = new FeatureMembership();
            viewUsage.AssignOwnership(plainRequirementMembership, plainRequirement);

            Assert.That(viewUsage.ComputeSatisfiedViewpoint(), Is.Empty);

            // Predicate discrimination: ViewpointUsage with IsComposite = false → excluded.
            var nonCompositeViewpoint = new ViewpointUsage { IsComposite = false };
            var nonCompositeViewpointMembership = new FeatureMembership();
            viewUsage.AssignOwnership(nonCompositeViewpointMembership, nonCompositeViewpoint);

            Assert.That(viewUsage.ComputeSatisfiedViewpoint(), Is.Empty);

            // Positive: ViewpointUsage with IsComposite = true → returned.
            var compositeViewpoint1 = new ViewpointUsage { IsComposite = true };
            var compositeViewpointMembership1 = new FeatureMembership();
            viewUsage.AssignOwnership(compositeViewpointMembership1, compositeViewpoint1);

            Assert.That(viewUsage.ComputeSatisfiedViewpoint(), Is.EqualTo([compositeViewpoint1]));

            // Populated: second composite ViewpointUsage also returned in iteration order.
            var compositeViewpoint2 = new ViewpointUsage { IsComposite = true };
            var compositeViewpointMembership2 = new FeatureMembership();
            viewUsage.AssignOwnership(compositeViewpointMembership2, compositeViewpoint2);

            Assert.That(viewUsage.ComputeSatisfiedViewpoint(), Is.EqualTo([compositeViewpoint1, compositeViewpoint2]));
        }

        [Test]
        public void VerifyComputeViewCondition()
        {
            Assert.That(() => ((IViewUsage)null).ComputeViewCondition(), Throws.TypeOf<ArgumentNullException>());

            var viewUsage = new ViewUsage();

            // Empty: no ownedMembership → empty list.
            Assert.That(viewUsage.ComputeViewCondition(), Is.Empty);

            // Discrimination: non-ElementFilterMembership in ownedMembership → excluded.
            var nonFilterFeature = new Feature();
            var nonFilterMembership = new FeatureMembership();
            viewUsage.AssignOwnership(nonFilterMembership, nonFilterFeature);

            Assert.That(viewUsage.ComputeViewCondition(), Is.Empty);

            // Positive: ElementFilterMembership.condition (ComputeCondition) is implemented —
            // the owned BooleanExpression is now returned directly.
            var filterCondition = new BooleanExpression();
            var filterMembership = new ElementFilterMembership();
            viewUsage.AssignOwnership(filterMembership, filterCondition);

            Assert.That(viewUsage.ComputeViewCondition(), Is.EqualTo([filterCondition]));
        }

        [Test]
        public void VerifyComputeViewDefinition()
        {
            Assert.That(() => ((IViewUsage)null).ComputeViewDefinition(), Throws.TypeOf<ArgumentNullException>());

            var viewUsage = new ViewUsage();

            // Empty: no FeatureTyping → null.
            Assert.That(viewUsage.ComputeViewDefinition(), Is.Null);

            // Negative: FeatureTyping whose Type is a non-ViewDefinition → null.
            var nonViewDefinitionType = new Feature();
            var typingToNonViewDefinition = new FeatureTyping { Type = nonViewDefinitionType };
            viewUsage.AssignOwnership(typingToNonViewDefinition);

            Assert.That(viewUsage.ComputeViewDefinition(), Is.Null);

            // Positive: FeatureTyping whose Type is a ViewDefinition → returned.
            var viewDefinition1 = new ViewDefinition();
            var typingToViewDefinition1 = new FeatureTyping { Type = viewDefinition1 };
            viewUsage.AssignOwnership(typingToViewDefinition1);

            Assert.That(viewUsage.ComputeViewDefinition(), Is.SameAs(viewDefinition1));

            // Multiple: two ViewDefinition typings → first returned (FirstOrDefault).
            var viewDefinition2 = new ViewDefinition();
            var typingToViewDefinition2 = new FeatureTyping { Type = viewDefinition2 };
            viewUsage.AssignOwnership(typingToViewDefinition2);

            Assert.That(viewUsage.ComputeViewDefinition(), Is.SameAs(viewDefinition1));
        }

        [Test]
        public void VerifyComputeViewRendering()
        {
            Assert.That(() => ((IViewUsage)null).ComputeViewRendering(), Throws.TypeOf<ArgumentNullException>());

            var viewUsage = new ViewUsage();

            // Empty: no featureMembership → null.
            Assert.That(viewUsage.ComputeViewRendering(), Is.Null);

            // Discrimination: non-ViewRenderingMembership featureMembership → null.
            var plainFeature = new Feature();
            var plainFeatureMembership = new FeatureMembership();
            viewUsage.AssignOwnership(plainFeatureMembership, plainFeature);

            Assert.That(viewUsage.ComputeViewRendering(), Is.Null);

            // STUB-BLOCKER: Wiring a ViewRenderingMembership and reading its referencedRendering
            // property dispatches to ViewRenderingMembershipExtensions.ComputeReferencedRendering,
            // which is a NotSupportedException stub. The populated positive case cannot be tested
            // cleanly until that upstream stub is implemented.
            var renderingUsage = new RenderingUsage();
            var viewRenderingMembership = new ViewRenderingMembership();
            viewUsage.AssignOwnership(viewRenderingMembership, renderingUsage);

            Assert.That(() => viewUsage.ComputeViewRendering(), Throws.TypeOf<NotSupportedException>());
        }
    }
}
