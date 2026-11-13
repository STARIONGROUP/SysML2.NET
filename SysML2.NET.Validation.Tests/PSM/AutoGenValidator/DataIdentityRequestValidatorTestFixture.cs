// ------------------------------------------------------------------------------------------------
// <copyright file="DataIdentityRequestValidatorTestFixture.cs" company="Starion Group S.A.">
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
    public class DataIdentityRequestValidatorTestFixture
    {
        [Test]
        public void VerifyValidate()
        {
            var validator = new DataIdentityRequestValidator();
            var dataIdentityRequest = new DataIdentityRequest { Id = Guid.NewGuid() };

            Assert.That(validator.Validate(dataIdentityRequest).IsValid, Is.True);

            dataIdentityRequest.Id = Guid.Empty;

            Assert.That(validator.Validate(dataIdentityRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Id"]));
        }
    }
}
