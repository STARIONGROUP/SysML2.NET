// ------------------------------------------------------------------------------------------------
// <copyright file="MembershipValidatorTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Validation.Tests.Core.DTO.AutoGenValidator
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Validation.Core.DTO;

    [TestFixture]
    public class MembershipValidatorTestFixture
    {
        private static readonly string[] OwnedRelationshipFailure = ["OwnedRelationship"];

        [Test]
        public void VerifyValidate()
        {
            var validator = new MembershipValidator();
            var relationshipId = Guid.NewGuid();
            var membership = new Membership { Id = Guid.NewGuid(), ElementId = "the element id", MemberElement = Guid.NewGuid() };

            Assert.That(validator.Validate(membership).IsValid, Is.True);

            membership.MemberElement = Guid.Empty;

            Assert.That(validator.Validate(membership).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["MemberElement"]));

            membership.MemberElement = Guid.NewGuid();
            membership.ElementId = null;

            Assert.That(validator.Validate(membership).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["ElementId"]));

            membership.ElementId = string.Empty;
            membership.OwningRelationship = Guid.NewGuid();

            Assert.That(validator.Validate(membership).IsValid, Is.True);

            membership.OwningRelationship = Guid.Empty;

            Assert.That(validator.Validate(membership).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["OwningRelationship"]));

            membership.OwningRelationship = null;
            membership.OwnedRelationship = [relationshipId, relationshipId];

            Assert.That(validator.Validate(membership).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(OwnedRelationshipFailure));

            membership.OwnedRelationship = [Guid.Empty];

            Assert.That(validator.Validate(membership).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["OwnedRelationship[0]"]));

            membership.OwnedRelationship = null;

            Assert.That(validator.Validate(membership).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(OwnedRelationshipFailure));

            membership.OwnedRelationship = [relationshipId];
            membership.AliasIds = [null];

            Assert.That(validator.Validate(membership).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["AliasIds[0]"]));

            membership.AliasIds = ["the alias", "the alias"];

            Assert.That(validator.Validate(membership).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["AliasIds"]));

            membership.AliasIds = ["the alias"];
            membership.Visibility = (VisibilityKind)int.MaxValue;

            Assert.That(validator.Validate(membership).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Visibility"]));
        }
    }
}
