// ------------------------------------------------------------------------------------------------
// <copyright file="ExternalRelationshipRequestValidatorTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Validation.Tests.PSM.AutoGenValidator
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.PSM.DTO;
    using SysML2.NET.Validation.PSM;

    [TestFixture]
    public class ExternalRelationshipRequestValidatorTestFixture
    {
        [Test]
        public void VerifyValidate()
        {
            var validator = new ExternalRelationshipRequestValidator();
            var externalRelationshipRequest = new ExternalRelationshipRequest();

            Assert.That(validator.Validate(externalRelationshipRequest).IsValid, Is.True);

            externalRelationshipRequest.Id = Guid.NewGuid();
            externalRelationshipRequest.ElementEnd = Guid.NewGuid();
            externalRelationshipRequest.ExternalDataEnd = Guid.NewGuid();

            Assert.That(validator.Validate(externalRelationshipRequest).IsValid, Is.True);

            externalRelationshipRequest.Id = Guid.Empty;
            externalRelationshipRequest.ElementEnd = Guid.Empty;
            externalRelationshipRequest.ExternalDataEnd = Guid.Empty;

            Assert.That(validator.Validate(externalRelationshipRequest).Errors.Select(failure => failure.PropertyName),
                Is.EquivalentTo(["Id", "ElementEnd", "ExternalDataEnd"]));
        }
    }
}
