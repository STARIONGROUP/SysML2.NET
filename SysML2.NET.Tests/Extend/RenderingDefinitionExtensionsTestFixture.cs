// ------------------------------------------------------------------------------------------------
// <copyright file="RenderingDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class RenderingDefinitionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeRendering()
        {
            Assert.That(() => ((IRenderingDefinition)null).ComputeRendering(), Throws.TypeOf<ArgumentNullException>());

            var renderingDefinition = new RenderingDefinition();

            // Empty: no usages wired → empty result.
            Assert.That(renderingDefinition.ComputeRendering(), Is.Empty);

            // Discrimination: PartUsage in usage (not IRenderingUsage) → excluded.
            var partUsage = new PartUsage();
            renderingDefinition.AssignOwnership(new FeatureMembership(), partUsage);

            Assert.That(renderingDefinition.ComputeRendering(), Is.Empty);

            // Positive: RenderingUsage wired via FeatureMembership → appears in usage → included.
            var renderingUsage = new RenderingUsage();
            renderingDefinition.AssignOwnership(new FeatureMembership(), renderingUsage);

            Assert.That(renderingDefinition.ComputeRendering(), Is.EqualTo([renderingUsage]));

            // Multiple: second RenderingUsage also returned.
            var renderingUsage2 = new RenderingUsage();
            renderingDefinition.AssignOwnership(new FeatureMembership(), renderingUsage2);

            Assert.That(renderingDefinition.ComputeRendering(), Is.EqualTo([renderingUsage, renderingUsage2]));
        }
    }
}
