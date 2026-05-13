// -------------------------------------------------------------------------------------------------
// <copyright file="ParameterMembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class ParameterMembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwnedMemberParameter()
        {
            Assert.That(() => ((IParameterMembership)null).ComputeOwnedMemberParameter(), Throws.TypeOf<ArgumentNullException>());

            var parameterMembership = new ParameterMembership();

            Assert.That(() => parameterMembership.ComputeOwnedMemberParameter(), Throws.TypeOf<IncompleteModelException>());

            var owningType = new Type();
            var feature = new Feature();

            owningType.AssignOwnership(parameterMembership, feature);

            Assert.That(parameterMembership.ComputeOwnedMemberParameter(), Is.SameAs(feature));

            // Wiring two features to verify the multiple-element guard:
            // First remove the existing wiring so we can create a fresh membership with two elements.
            var twoElementMembership = new ParameterMembership();
            var secondFeature = new Feature();

            ((IContainedRelationship)twoElementMembership).OwnedRelatedElement.Add(feature);
            ((IContainedRelationship)twoElementMembership).OwnedRelatedElement.Add(secondFeature);

            Assert.That(() => twoElementMembership.ComputeOwnedMemberParameter(), Throws.TypeOf<IncompleteModelException>());

            // NOTE: wiring a non-IFeature element as the sole OwnedRelatedElement is not possible via the
            // public AssignOwnership API (IParameterMembership requires an IFeature target).
            // To cover the as-cast-returns-null path we directly populate OwnedRelatedElement with a
            // plain Namespace (which is not an IFeature).
            var nonFeatureMembership = new ParameterMembership();
            var nonFeatureElement = new Namespace();

            ((IContainedRelationship)nonFeatureMembership).OwnedRelatedElement.Add(nonFeatureElement);

            Assert.That(nonFeatureMembership.ComputeOwnedMemberParameter(), Is.Null);
        }

        [Test]
        public void VerifyComputeParameterDirectionOperation()
        {
            Assert.That(() => ((IParameterMembership)null).ComputeParameterDirectionOperation(), Throws.TypeOf<ArgumentNullException>());

            var parameterMembership = new ParameterMembership();
            var owningType = new Type();
            var feature = new Feature();

            owningType.AssignOwnership(parameterMembership, feature);

            Assert.That(parameterMembership.ComputeParameterDirectionOperation(), Is.EqualTo(FeatureDirectionKind.In));
        }
    }
}
