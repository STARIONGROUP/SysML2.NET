// ------------------------------------------------------------------------------------------------
// <copyright file="LiteralStringValidatorTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Validation.Tests.Core.DTO.AutoGenValidator
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.Core.DTO.Kernel.Expressions;
    using SysML2.NET.Validation.Core.DTO;

    [TestFixture]
    public class LiteralStringValidatorTestFixture
    {
        [Test]
        public void VerifyValidate()
        {
            var validator = new LiteralStringValidator();
            var literalString = new LiteralString { Id = Guid.NewGuid(), ElementId = "the element id", Value = "the value" };

            Assert.That(validator.Validate(literalString).IsValid, Is.True);

            literalString.Value = string.Empty;

            Assert.That(validator.Validate(literalString).IsValid, Is.True);

            literalString.Value = null;

            Assert.That(validator.Validate(literalString).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Value"]));
        }
    }
}
