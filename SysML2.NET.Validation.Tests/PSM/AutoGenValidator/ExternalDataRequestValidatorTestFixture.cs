// ------------------------------------------------------------------------------------------------
// <copyright file="ExternalDataRequestValidatorTestFixture.cs" company="Starion Group S.A.">
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
    public class ExternalDataRequestValidatorTestFixture
    {
        [Test]
        public void VerifyValidate()
        {
            var validator = new ExternalDataRequestValidator();
            var externalDataRequest = new ExternalDataRequest { ResourceIdentifier = new Uri("https://example.com/the-resource") };

            Assert.That(validator.Validate(externalDataRequest).IsValid, Is.True);

            externalDataRequest.ResourceIdentifier = null;
            externalDataRequest.Id = Guid.NewGuid();

            Assert.That(validator.Validate(externalDataRequest).IsValid, Is.True);

            externalDataRequest.ResourceIdentifier = new Uri("the-resource", UriKind.Relative);

            Assert.That(validator.Validate(externalDataRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["ResourceIdentifier"]));

            externalDataRequest.ResourceIdentifier = null;
            externalDataRequest.Id = Guid.Empty;

            Assert.That(validator.Validate(externalDataRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Id"]));
        }
    }
}
