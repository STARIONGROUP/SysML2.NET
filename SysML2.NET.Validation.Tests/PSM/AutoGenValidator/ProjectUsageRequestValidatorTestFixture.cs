// ------------------------------------------------------------------------------------------------
// <copyright file="ProjectUsageRequestValidatorTestFixture.cs" company="Starion Group S.A.">
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
    public class ProjectUsageRequestValidatorTestFixture
    {
        [Test]
        public void VerifyValidate()
        {
            var validator = new ProjectUsageRequestValidator();
            var projectUsageRequest = new ProjectUsageRequest();

            Assert.That(validator.Validate(projectUsageRequest).IsValid, Is.True);

            projectUsageRequest.Id = Guid.NewGuid();
            projectUsageRequest.UsedCommit = Guid.NewGuid();
            projectUsageRequest.UsedProject = Guid.NewGuid();

            Assert.That(validator.Validate(projectUsageRequest).IsValid, Is.True);

            projectUsageRequest.Id = Guid.Empty;
            projectUsageRequest.UsedCommit = Guid.Empty;
            projectUsageRequest.UsedProject = Guid.Empty;

            Assert.That(validator.Validate(projectUsageRequest).Errors.Select(failure => failure.PropertyName),
                Is.EquivalentTo(["Id", "UsedCommit", "UsedProject"]));
        }
    }
}
