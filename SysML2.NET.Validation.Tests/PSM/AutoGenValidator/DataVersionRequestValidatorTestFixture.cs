// ------------------------------------------------------------------------------------------------
// <copyright file="DataVersionRequestValidatorTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.Validation.PSM;

    [TestFixture]
    public class DataVersionRequestValidatorTestFixture
    {
        [Test]
        public void VerifyValidate()
        {
            var validator = new DataVersionRequestValidator();
            var dataVersionRequest = new DataVersionRequest();

            Assert.That(validator.Validate(dataVersionRequest).IsValid, Is.True);

            dataVersionRequest.Payload = new Namespace { Id = Guid.NewGuid() };

            Assert.That(validator.Validate(dataVersionRequest).IsValid, Is.True);

            dataVersionRequest.Payload = new ExternalDataRequest { Id = Guid.NewGuid() };

            Assert.That(validator.Validate(dataVersionRequest).IsValid, Is.True);

            dataVersionRequest.Payload = new ExternalDataRequest { Id = Guid.Empty };

            Assert.That(validator.Validate(dataVersionRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Payload.Id"]));

            dataVersionRequest.Payload = new ExternalRelationshipRequest { ElementEnd = Guid.Empty };

            Assert.That(validator.Validate(dataVersionRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Payload.ElementEnd"]));

            dataVersionRequest.Payload = new ProjectUsageRequest { UsedCommit = Guid.Empty };

            Assert.That(validator.Validate(dataVersionRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Payload.UsedCommit"]));

            dataVersionRequest.Payload = null;
            dataVersionRequest.Identity = new DataIdentityRequest { Id = Guid.Empty };

            Assert.That(validator.Validate(dataVersionRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Identity.Id"]));
        }
    }
}
