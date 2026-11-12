// ------------------------------------------------------------------------------------------------
// <copyright file="ProjectRequestValidatorTestFixture.cs" company="Starion Group S.A.">
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
    public class ProjectRequestValidatorTestFixture
    {
        [Test]
        public void VerifyValidate()
        {
            var validator = new ProjectRequestValidator();
            var projectRequest = new ProjectRequest { Name = "the project" };

            Assert.That(validator.Validate(projectRequest).IsValid, Is.True);

            projectRequest.Name = string.Empty;
            projectRequest.DefaultBranch = Guid.NewGuid();

            Assert.That(validator.Validate(projectRequest).IsValid, Is.True);

            projectRequest.DefaultBranch = Guid.Empty;

            Assert.That(validator.Validate(projectRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["DefaultBranch"]));

            projectRequest.DefaultBranch = null;
            projectRequest.Name = null;

            Assert.That(validator.Validate(projectRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Name"]));
        }
    }
}
