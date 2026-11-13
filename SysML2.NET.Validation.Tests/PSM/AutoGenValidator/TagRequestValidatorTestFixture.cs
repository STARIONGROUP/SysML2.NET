// ------------------------------------------------------------------------------------------------
// <copyright file="TagRequestValidatorTestFixture.cs" company="Starion Group S.A.">
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
    public class TagRequestValidatorTestFixture
    {
        private static readonly string[] NameFailure = ["Name"];

        [Test]
        public void VerifyValidate()
        {
            var validator = new TagRequestValidator();
            var tagRequest = new TagRequest { Name = "the tag", TaggedCommit = Guid.NewGuid() };

            Assert.That(validator.Validate(tagRequest).IsValid, Is.True);

            tagRequest.TaggedCommit = Guid.Empty;

            Assert.That(validator.Validate(tagRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["TaggedCommit"]));

            tagRequest.TaggedCommit = Guid.NewGuid();
            tagRequest.Name = null;

            Assert.That(validator.Validate(tagRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(NameFailure));

            tagRequest.Name = string.Empty;

            Assert.That(validator.Validate(tagRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(NameFailure));

            tagRequest.Name = " ";

            Assert.That(validator.Validate(tagRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(NameFailure));
        }
    }
}
