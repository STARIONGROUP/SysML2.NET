// -------------------------------------------------------------------------------------------------
// <copyright file="RenderingDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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
