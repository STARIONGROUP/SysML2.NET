// ------------------------------------------------------------------------------------------------
// <copyright file="CommitRequestValidatorTestFixture.cs" company="Starion Group S.A.">
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
    public class CommitRequestValidatorTestFixture
    {
        [Test]
        public void VerifyValidate()
        {
            var validator = new CommitRequestValidator();
            var dataIdentityRequest = new DataIdentityRequest { Id = Guid.NewGuid() };
            var commitRequest = new CommitRequest { Change = [new DataVersionRequest { Identity = dataIdentityRequest }] };

            Assert.That(validator.Validate(commitRequest).IsValid, Is.True);

            commitRequest.Change = [];

            Assert.That(validator.Validate(commitRequest).IsValid, Is.True);

            commitRequest.Change = null;

            Assert.That(validator.Validate(commitRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Change"]));

            commitRequest.Change = [new DataVersionRequest(), null];

            Assert.That(validator.Validate(commitRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Change[1]"]));

            dataIdentityRequest.Id = Guid.Empty;
            commitRequest.Change = [new DataVersionRequest { Identity = dataIdentityRequest }];

            Assert.That(validator.Validate(commitRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Change[0].Identity.Id"]));
        }
    }
}
