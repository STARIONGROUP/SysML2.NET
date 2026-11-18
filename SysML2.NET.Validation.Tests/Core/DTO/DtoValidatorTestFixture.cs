// ------------------------------------------------------------------------------------------------
// <copyright file="DtoValidatorTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Validation.Tests.Core.DTO
{
    using System;
    using System.Linq;

    using FluentValidation;

    using NUnit.Framework;

    using SysML2.NET.Core.DTO.Root.Annotations;
    using SysML2.NET.Validation.Core.DTO;

    [TestFixture]
    public class DtoValidatorTestFixture
    {
        [Test]
        public void VerifyAddCustomValidation()
        {
            var comment = new Comment { Id = Guid.NewGuid(), Body = "the body" };

            Assert.That(new ValidatorWithoutCustomValidation().Validate(comment).IsValid, Is.True);

            var validator = new ValidatorWithCustomValidation();

            Assert.That(validator.Validate(comment).IsValid, Is.True);

            comment.Body = " ";

            Assert.That(validator.Validate(comment).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo([nameof(Comment.Body)]));
            Assert.That(new ValidatorWithoutCustomValidation().Validate(comment).IsValid, Is.True);

            comment.Id = Guid.Empty;

            Assert.That(new ValidatorWithoutCustomValidation().Validate(comment).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo([nameof(Comment.Id)]));
        }

        private sealed class ValidatorWithoutCustomValidation : DtoValidator<Comment>
        {
        }

        private sealed class ValidatorWithCustomValidation : DtoValidator<Comment>
        {
            protected override void AddCustomValidation()
            {
                base.AddCustomValidation();

                this.RuleFor(dto => dto.Body).Must(value => !string.IsNullOrWhiteSpace(value));
            }
        }
    }
}
