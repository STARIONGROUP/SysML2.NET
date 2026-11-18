// ------------------------------------------------------------------------------------------------
// <copyright file="OwningMembershipValidatorTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Validation.Core.DTO;

    [TestFixture]
    public class OwningMembershipValidatorTestFixture
    {
        [Test]
        public void VerifyValidate()
        {
            var validator = new OwningMembershipValidator();
            var owningMembership = new OwningMembership { Id = Guid.NewGuid(), ElementId = "the element id", OwnedRelatedElement = [Guid.NewGuid()], OwningRelatedElement = Guid.NewGuid() };

            Assert.That(validator.Validate(owningMembership).IsValid, Is.True);

            owningMembership.OwningRelatedElement = null;

            Assert.That(validator.Validate(owningMembership).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["OwningRelatedElement"]));

            owningMembership.OwningRelatedElement = Guid.NewGuid();

            owningMembership.OwnedRelatedElement = [];

            Assert.That(validator.Validate(owningMembership).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["OwnedRelatedElement"]));

            owningMembership.OwnedRelatedElement = null;

            Assert.That(validator.Validate(owningMembership).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["OwnedRelatedElement"]));
        }
    }
}
