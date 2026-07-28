// -------------------------------------------------------------------------------------------------
// <copyright file="PartUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Items;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class PartUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputePartDefinition()
        {
            // Null subject: guard clause throws ArgumentNullException.
            Assert.That(() => ((IPartUsage)null).ComputePartDefinition(), Throws.TypeOf<ArgumentNullException>());

            var partUsage = new PartUsage();

            // Empty case: no FeatureTyping owned by the subject → empty list.
            Assert.That(partUsage.ComputePartDefinition(), Is.Empty);

            // Discrimination: FeatureTyping targets an ItemDefinition (a Structure, NOT an
            // IPartDefinition) — filtered out → still empty.
            var itemDefinition = new ItemDefinition();
            partUsage.AssignOwnership(new FeatureTyping { Type = itemDefinition });
            Assert.That(partUsage.ComputePartDefinition(), Is.Empty);

            // Populated case: FeatureTypings whose Types are IPartDefinition → all returned,
            // the non-matching ItemDefinition typing stays filtered out.
            var partDefinition = new PartDefinition();
            var secondPartDefinition = new PartDefinition();
            partUsage.AssignOwnership(new FeatureTyping { Type = partDefinition });
            partUsage.AssignOwnership(new FeatureTyping { Type = secondPartDefinition });
            Assert.That(partUsage.ComputePartDefinition(), Is.EqualTo([partDefinition, secondPartDefinition]));
        }
    }
}
