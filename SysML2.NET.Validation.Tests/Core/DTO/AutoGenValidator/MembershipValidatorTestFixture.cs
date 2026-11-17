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
        [Test]
        public void VerifyValidate()
        {
            var validator = new MembershipValidator();
            var relationshipId = Guid.NewGuid();
            var membership = new Membership { Id = Guid.NewGuid(), ElementId = "the element id", MemberElement = Guid.NewGuid(), OwningRelatedElement = Guid.NewGuid() };

            Assert.That(validator.Validate(membership).IsValid, Is.True);

            membership.OwningRelatedElement = null;

            Assert.That(validator.Validate(membership).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["OwningRelatedElement"]));

            membership.OwningRelatedElement = Guid.NewGuid();

            membership.MemberElement = Guid.Empty;

            Assert.That(validator.Validate(membership).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["MemberElement"]));

            membership.MemberElement = Guid.NewGuid();
            membership.ElementId = null;

            Assert.That(validator.Validate(membership).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["ElementId"]));

            membership.ElementId = string.Empty;
            membership.OwningRelationship = Guid.Empty;
            membership.OwnedRelationship = [relationshipId, relationshipId, Guid.Empty];
            membership.AliasIds = [null, null];

            Assert.That(validator.Validate(membership).IsValid, Is.True);

            membership.OwnedRelationship = null;
            membership.AliasIds = null;

            Assert.That(validator.Validate(membership).IsValid, Is.True);

            membership.Visibility = (VisibilityKind)int.MaxValue;

            Assert.That(validator.Validate(membership).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Visibility"]));
        }
    }
}
