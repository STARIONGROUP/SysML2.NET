// ------------------------------------------------------------------------------------------------
// <copyright file="RequirementConstraintMembershipValidatorTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.DTO.Systems.Requirements;
    using SysML2.NET.Core.Systems.Requirements;
    using SysML2.NET.Validation.Core.DTO;

    [TestFixture]
    public class RequirementConstraintMembershipValidatorTestFixture
    {
        [Test]
        public void VerifyValidate()
        {
            var validator = new RequirementConstraintMembershipValidator();
            var membership = new RequirementConstraintMembership { Id = Guid.NewGuid(), ElementId = "the element id", Kind = RequirementConstraintKind.Requirement };

            Assert.That(validator.Validate(membership).IsValid, Is.True);

            membership.Kind = (RequirementConstraintKind)int.MaxValue;

            Assert.That(validator.Validate(membership).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Kind"]));
        }
    }
}
