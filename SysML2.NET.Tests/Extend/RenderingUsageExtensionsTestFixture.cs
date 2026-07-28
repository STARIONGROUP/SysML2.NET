// -------------------------------------------------------------------------------------------------
// <copyright file="RenderingUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class RenderingUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeRenderingDefinition()
        {
            Assert.That(() => ((IRenderingUsage)null).ComputeRenderingDefinition(), Throws.TypeOf<ArgumentNullException>());

            var renderingUsage = new RenderingUsage();

            // Empty: no OwnedRelationship → null.
            Assert.That(renderingUsage.ComputeRenderingDefinition(), Is.Null);

            // Discrimination: FeatureTyping targets a PartDefinition, which is a superclass of
            // IRenderingDefinition and therefore NOT an IRenderingDefinition — filtered out → still null.
            var partDefinition = new PartDefinition();
            renderingUsage.AssignOwnership(new FeatureTyping { Type = partDefinition });
            Assert.That(renderingUsage.ComputeRenderingDefinition(), Is.Null);

            // Populated case: FeatureTyping whose Type is an IRenderingDefinition → returned.
            var renderingDefinition = new RenderingDefinition();
            renderingUsage.AssignOwnership(new FeatureTyping { Type = renderingDefinition });
            Assert.That(renderingUsage.ComputeRenderingDefinition(), Is.SameAs(renderingDefinition));

            // [0..1] upper-bound violation: two matching typings → MultiplicityViolationException.
            var secondRenderingDefinition = new RenderingDefinition();
            renderingUsage.AssignOwnership(new FeatureTyping { Type = secondRenderingDefinition });
            Assert.That(() => renderingUsage.ComputeRenderingDefinition(), Throws.TypeOf<MultiplicityViolationException>());
        }
    }
}
