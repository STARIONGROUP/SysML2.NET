// ------------------------------------------------------------------------------------------------
// <copyright file="ViewRenderingMembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ViewRenderingMembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwnedRendering()
        {
            Assert.That(() => ((IViewRenderingMembership)null).ComputeOwnedRendering(), Throws.TypeOf<ArgumentNullException>());

            // Empty OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var emptyMembership = new ViewRenderingMembership();

            Assert.That(() => emptyMembership.ComputeOwnedRendering(), Throws.TypeOf<IncompleteModelException>());

            // Single non-IRenderingUsage in OwnedRelatedElement (zero matches of target type) →
            // [1..1] violation: throws IncompleteModelException.
            var nonRenderingMembership = new ViewRenderingMembership();
            var nonRenderingElement = new Namespace();
            ((IContainedRelationship)nonRenderingMembership).OwnedRelatedElement.Add(nonRenderingElement);

            Assert.That(() => nonRenderingMembership.ComputeOwnedRendering(), Throws.TypeOf<IncompleteModelException>());

            // Single IRenderingUsage wired via parent → returned.
            var owningDefinition = new ViewDefinition();
            var renderingMembership = new ViewRenderingMembership();
            var renderingUsage = new RenderingUsage();
            owningDefinition.AssignOwnership(renderingMembership, renderingUsage);

            Assert.That(renderingMembership.ComputeOwnedRendering(), Is.SameAs(renderingUsage));

            // Two IRenderingUsage in OwnedRelatedElement → upper-bound violation: throws MultiplicityViolationException.
            var twoRenderingMembership = new ViewRenderingMembership();
            var firstRendering = new RenderingUsage();
            var secondRendering = new RenderingUsage();
            ((IContainedRelationship)twoRenderingMembership).OwnedRelatedElement.Add(firstRendering);
            ((IContainedRelationship)twoRenderingMembership).OwnedRelatedElement.Add(secondRendering);

            Assert.That(() => twoRenderingMembership.ComputeOwnedRendering(), Throws.TypeOf<MultiplicityViolationException>());

            // Mixed: annotation (Namespace) alongside a single IRenderingUsage — the type filter
            // picks out the RenderingUsage regardless of its position.
            var mixedMembership = new ViewRenderingMembership();
            var siblingNamespace = new Namespace();
            var mixedRendering = new RenderingUsage();
            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(siblingNamespace);
            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(mixedRendering);

            Assert.That(mixedMembership.ComputeOwnedRendering(), Is.SameAs(mixedRendering));
        }

        [Test]
        public void VerifyComputeReferencedRendering()
        {
            Assert.That(() => ((IViewRenderingMembership)null).ComputeReferencedRendering(), Throws.TypeOf<ArgumentNullException>());

            // No ownedRendering in OwnedRelatedElement (malformed) → IncompleteModelException
            // propagated from ComputeOwnedRendering.
            var emptyMembership = new ViewRenderingMembership();

            Assert.That(() => emptyMembership.ComputeReferencedRendering(), Throws.TypeOf<IncompleteModelException>());

            // Populated, no ownedReferenceSubsetting on the ownedRendering →
            // referencedFeature is null → referencedRendering == ownedRendering.
            var owningDefinition = new ViewDefinition();
            var renderingMembership = new ViewRenderingMembership();
            var renderingUsage = new RenderingUsage();
            owningDefinition.AssignOwnership(renderingMembership, renderingUsage);

            Assert.That(renderingMembership.ComputeReferencedRendering(), Is.SameAs(renderingUsage));

            // Populated, with ownedReferenceSubsetting whose ReferencedFeature is itself a
            // RenderingUsage → referencedRendering == that RenderingUsage.
            var owningDefinition2 = new ViewDefinition();
            var renderingMembership2 = new ViewRenderingMembership();
            var ownedRenderingUsage = new RenderingUsage();
            owningDefinition2.AssignOwnership(renderingMembership2, ownedRenderingUsage);

            var referencedRenderingUsage = new RenderingUsage();
            var referenceSubsetting = new ReferenceSubsetting { ReferencedFeature = referencedRenderingUsage };
            ownedRenderingUsage.AssignOwnership(referenceSubsetting);

            Assert.That(renderingMembership2.ComputeReferencedRendering(), Is.SameAs(referencedRenderingUsage));

            // Populated, with ownedReferenceSubsetting whose ReferencedFeature is a non-RenderingUsage
            // Feature → referencedRendering is null (else null branch of the OCL).
            var owningDefinition3 = new ViewDefinition();
            var renderingMembership3 = new ViewRenderingMembership();
            var ownedRenderingUsage3 = new RenderingUsage();
            owningDefinition3.AssignOwnership(renderingMembership3, ownedRenderingUsage3);

            var partUsageTarget = new PartUsage();
            var referenceSubsetting3 = new ReferenceSubsetting { ReferencedFeature = partUsageTarget };
            ownedRenderingUsage3.AssignOwnership(referenceSubsetting3);

            Assert.That(renderingMembership3.ComputeReferencedRendering(), Is.Null);
        }
    }
}
