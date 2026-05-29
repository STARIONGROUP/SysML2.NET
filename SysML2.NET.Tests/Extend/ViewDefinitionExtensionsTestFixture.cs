// -------------------------------------------------------------------------------------------------
// <copyright file="ViewDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ViewDefinitionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeSatisfiedViewpoint()
        {
            Assert.That(() => ((IViewDefinition)null).ComputeSatisfiedViewpoint(), Throws.TypeOf<ArgumentNullException>());

            var viewDefinition = new ViewDefinition();

            // Empty: no ownedRequirement → empty result.
            Assert.That(viewDefinition.ComputeSatisfiedViewpoint(), Is.Empty);

            // Discrimination: plain RequirementUsage (not IViewpointUsage) → excluded.
            var plainRequirement = new RequirementUsage { IsComposite = true };
            viewDefinition.AssignOwnership(new FeatureMembership(), plainRequirement);

            Assert.That(viewDefinition.ComputeSatisfiedViewpoint(), Is.Empty);

            // Predicate discrimination: ViewpointUsage with IsComposite = false → excluded.
            var nonCompositeViewpoint = new ViewpointUsage { IsComposite = false };
            viewDefinition.AssignOwnership(new FeatureMembership(), nonCompositeViewpoint);

            Assert.That(viewDefinition.ComputeSatisfiedViewpoint(), Is.Empty);

            // Positive: composite ViewpointUsage → included.
            var compositeViewpoint = new ViewpointUsage { IsComposite = true };
            viewDefinition.AssignOwnership(new FeatureMembership(), compositeViewpoint);

            Assert.That(viewDefinition.ComputeSatisfiedViewpoint(), Is.EqualTo([compositeViewpoint]));

            // Multiple: second composite ViewpointUsage also returned in iteration order.
            var compositeViewpoint2 = new ViewpointUsage { IsComposite = true };
            viewDefinition.AssignOwnership(new FeatureMembership(), compositeViewpoint2);

            Assert.That(viewDefinition.ComputeSatisfiedViewpoint(), Is.EqualTo([compositeViewpoint, compositeViewpoint2]));
        }

        [Test]
        public void VerifyComputeView()
        {
            Assert.That(() => ((IViewDefinition)null).ComputeView(), Throws.TypeOf<ArgumentNullException>());

            var viewDefinition = new ViewDefinition();

            // Empty: no usages → empty result.
            Assert.That(viewDefinition.ComputeView(), Is.Empty);

            // Discrimination: PartUsage in usage (not IViewUsage) → excluded.
            var partUsage = new PartUsage();
            viewDefinition.AssignOwnership(new FeatureMembership(), partUsage);

            Assert.That(viewDefinition.ComputeView(), Is.Empty);

            // Positive: ViewUsage → included.
            var viewUsage = new ViewUsage();
            viewDefinition.AssignOwnership(new FeatureMembership(), viewUsage);

            Assert.That(viewDefinition.ComputeView(), Is.EqualTo([viewUsage]));

            // Multiple: second ViewUsage also returned.
            var viewUsage2 = new ViewUsage();
            viewDefinition.AssignOwnership(new FeatureMembership(), viewUsage2);

            Assert.That(viewDefinition.ComputeView(), Is.EqualTo([viewUsage, viewUsage2]));
        }

        [Test]
        public void VerifyComputeViewCondition()
        {
            Assert.That(() => ((IViewDefinition)null).ComputeViewCondition(), Throws.TypeOf<ArgumentNullException>());

            var viewDefinition = new ViewDefinition();

            // Empty: no ownedMembership → empty result.
            Assert.That(viewDefinition.ComputeViewCondition(), Is.Empty);

            // Discrimination: non-ElementFilterMembership in ownedMembership → excluded.
            var plainFeature = new Feature();
            var plainMembership = new OwningMembership();
            viewDefinition.AssignOwnership(plainMembership, plainFeature);

            Assert.That(viewDefinition.ComputeViewCondition(), Is.Empty);

            // Positive: ElementFilterMembership with a concrete IExpression → condition included.
            var filterCondition = new BooleanExpression();
            var filterMembership = new ElementFilterMembership();
            viewDefinition.AssignOwnership(filterMembership, filterCondition);

            Assert.That(viewDefinition.ComputeViewCondition(), Is.EqualTo([filterCondition]));
        }

        [Test]
        public void VerifyComputeViewRendering()
        {
            Assert.That(() => ((IViewDefinition)null).ComputeViewRendering(), Throws.TypeOf<ArgumentNullException>());

            var viewDefinition = new ViewDefinition();

            // Empty: no featureMembership → null.
            Assert.That(viewDefinition.ComputeViewRendering(), Is.Null);

            // Discrimination: non-ViewRenderingMembership featureMembership → null.
            var plainFeature = new Feature();
            var plainFeatureMembership = new FeatureMembership();
            viewDefinition.AssignOwnership(plainFeatureMembership, plainFeature);

            Assert.That(viewDefinition.ComputeViewRendering(), Is.Null);

            // Positive: ViewRenderingMembership whose ownedRendering is a RenderingUsage (no
            // ownedReferenceSubsetting) → referencedRendering == ownedRendering → returned.
            var renderingUsage = new RenderingUsage();
            var viewRenderingMembership = new ViewRenderingMembership();
            viewDefinition.AssignOwnership(viewRenderingMembership, renderingUsage);

            Assert.That(viewDefinition.ComputeViewRendering(), Is.SameAs(renderingUsage));

            // Multiple ViewRenderingMemberships: the first one wins (renderings->first() semantics).
            var renderingUsage2 = new RenderingUsage();
            var viewRenderingMembership2 = new ViewRenderingMembership();
            viewDefinition.AssignOwnership(viewRenderingMembership2, renderingUsage2);

            Assert.That(viewDefinition.ComputeViewRendering(), Is.SameAs(renderingUsage));
        }
    }
}
