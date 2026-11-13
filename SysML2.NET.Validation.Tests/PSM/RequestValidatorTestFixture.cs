// ------------------------------------------------------------------------------------------------
// <copyright file="RequestValidatorTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Validation.Tests.PSM
{
    using FluentValidation;

    using NUnit.Framework;

    using SysML2.NET.PSM.DTO;
    using SysML2.NET.Validation.PSM;

    [TestFixture]
    public class RequestValidatorTestFixture
    {
        [Test]
        public void VerifyAddCustomValidation()
        {
            var projectRequest = new ProjectRequest { Name = "the project" };

            Assert.That(new ValidatorWithoutCustomValidation().Validate(projectRequest).IsValid, Is.True);

            var validator = new ValidatorWithCustomValidation();

            Assert.That(validator.Validate(projectRequest).IsValid, Is.True);

            projectRequest.Name = " ";

            var result = validator.Validate(projectRequest);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Errors, Has.Count.EqualTo(1));
                Assert.That(result.Errors[0].PropertyName, Is.EqualTo(nameof(ProjectRequest.Name)));
            }

            Assert.That(new ValidatorWithoutCustomValidation().Validate(projectRequest).IsValid, Is.True);
        }

        private sealed class ValidatorWithoutCustomValidation : RequestValidator<ProjectRequest>
        {
        }

        private sealed class ValidatorWithCustomValidation : RequestValidator<ProjectRequest>
        {
            protected override void AddCustomValidation()
            {
                base.AddCustomValidation();

                this.RuleFor(dto => dto.Name).Must(value => !string.IsNullOrWhiteSpace(value));
            }
        }
    }
}
