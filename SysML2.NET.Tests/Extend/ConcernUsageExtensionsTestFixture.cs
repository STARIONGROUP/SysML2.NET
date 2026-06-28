// -------------------------------------------------------------------------------------------------
// <copyright file="ConcernUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Extensions;
    using SysML2.NET.Exceptions;

    [TestFixture]
    public class ConcernUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeConcernDefinition()
        {
            // Null subject:
            Assert.That(() => ((IConcernUsage)null).ComputeConcernDefinition(), Throws.TypeOf<ArgumentNullException>());

            // Empty subject: no FeatureTyping relationships, so no ConcernDefinition:
            var concernUsage = new ConcernUsage();
            Assert.That(concernUsage.ComputeConcernDefinition(), Is.Null);

            // Typed by FeatureTyping, but not an IConcernDefinition:
            var requirementDefinition = new RequirementDefinition();
            var typingToRequirement = new FeatureTyping { Type = requirementDefinition };
            concernUsage.AssignOwnership(typingToRequirement);
            Assert.That(concernUsage.ComputeConcernDefinition(), Is.Null);

            // Correct typing: FeatureTyping.Type is a ConcernDefinition:
            var concernDefinition = new ConcernDefinition();
            var typingToConcern = new FeatureTyping { Type = concernDefinition };
            concernUsage.AssignOwnership(typingToConcern);
            Assert.That(concernUsage.ComputeConcernDefinition(), Is.SameAs(concernDefinition));

            // Multiple matching typings: FirstOrDefault is used so even breaking the multiplicity, it works;
            var secondConcernDefinition = new ConcernDefinition();
            var typingToSecondConcern = new FeatureTyping { Type = secondConcernDefinition };
            concernUsage.AssignOwnership(typingToSecondConcern);
            Assert.That(() => concernUsage.ComputeConcernDefinition(), Throws.TypeOf<MultiplicityViolationException>());
        }
    }
}
