// -------------------------------------------------------------------------------------------------
// <copyright file="SubjectMembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class SubjectMembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwnedSubjectParameter()
        {
            Assert.That(() => ((ISubjectMembership)null).ComputeOwnedSubjectParameter(), Throws.TypeOf<ArgumentNullException>());

            // Empty OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var subjectMembership = new SubjectMembership();

            Assert.That(() => subjectMembership.ComputeOwnedSubjectParameter(), Throws.TypeOf<IncompleteModelException>());

            // Single IUsage wired via the public API → returned.
            var owningType = new Type();
            var subjectUsage = new Usage();

            owningType.AssignOwnership(subjectMembership, subjectUsage);

            Assert.That(subjectMembership.ComputeOwnedSubjectParameter(), Is.SameAs(subjectUsage));

            // Two IUsages in OwnedRelatedElement → upper-bound violation: throws MultiplicityViolationException.
            var twoUsageMembership = new SubjectMembership();
            var firstUsage = new Usage();
            var secondUsage = new Usage();

            ((IContainedRelationship)twoUsageMembership).OwnedRelatedElement.Add(firstUsage);
            ((IContainedRelationship)twoUsageMembership).OwnedRelatedElement.Add(secondUsage);

            Assert.That(() => twoUsageMembership.ComputeOwnedSubjectParameter(), Throws.TypeOf<MultiplicityViolationException>());

            // Mixed-type owned related elements: exactly one IUsage alongside a non-IUsage (Namespace).
            // The OfType<IUsage>() projection MUST pick out the IUsage regardless of its position
            // (this is the core robustness guarantee — never positionally index the unfiltered collection).
            var mixedMembership = new SubjectMembership();
            var siblingNonUsage = new Namespace();
            var mixedUsage = new Usage();

            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(siblingNonUsage);
            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(mixedUsage);

            Assert.That(mixedMembership.ComputeOwnedSubjectParameter(), Is.SameAs(mixedUsage));

            // OwnedRelatedElement populated with non-IUsage element(s) only → no IUsage match:
            // [1..1] violation, throws IncompleteModelException.
            var nonUsageMembership = new SubjectMembership();
            var nonUsageElement = new Namespace();

            ((IContainedRelationship)nonUsageMembership).OwnedRelatedElement.Add(nonUsageElement);

            Assert.That(() => nonUsageMembership.ComputeOwnedSubjectParameter(), Throws.TypeOf<IncompleteModelException>());
        }
    }
}
