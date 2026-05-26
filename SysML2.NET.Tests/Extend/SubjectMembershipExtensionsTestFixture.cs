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

            var subjectMembership = new SubjectMembership();

            Assert.That(() => subjectMembership.ComputeOwnedSubjectParameter(), Throws.TypeOf<IncompleteModelException>());

            var owningType = new Type();
            var subjectUsage = new Usage();

            owningType.AssignOwnership(subjectMembership, subjectUsage);

            Assert.That(subjectMembership.ComputeOwnedSubjectParameter(), Is.SameAs(subjectUsage));

            // Wiring two usages to verify the multiple-element guard:
            // First create a fresh membership with two elements via the backdoor.
            var twoElementMembership = new SubjectMembership();
            var secondUsage = new Usage();

            ((IContainedRelationship)twoElementMembership).OwnedRelatedElement.Add(subjectUsage);
            ((IContainedRelationship)twoElementMembership).OwnedRelatedElement.Add(secondUsage);

            Assert.That(() => twoElementMembership.ComputeOwnedSubjectParameter(), Throws.TypeOf<IncompleteModelException>());

            // NOTE: wiring a non-IUsage element as the sole OwnedRelatedElement is not possible via the
            // public AssignOwnership API (ISubjectMembership requires an IUsage target).
            // To cover the as-cast-returns-null path we directly populate OwnedRelatedElement with a
            // plain Namespace (which is not an IUsage).
            var nonUsageMembership = new SubjectMembership();
            var nonUsageElement = new Namespace();

            ((IContainedRelationship)nonUsageMembership).OwnedRelatedElement.Add(nonUsageElement);

            Assert.That(nonUsageMembership.ComputeOwnedSubjectParameter(), Is.Null);
        }
    }
}
