// ------------------------------------------------------------------------------------------------
// <copyright file="PrimitiveConstraintRequestValidatorTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Validation.Tests.PSM.AutoGenValidator
{
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.PSM.DTO;
    using SysML2.NET.PSM.Enumerations;
    using SysML2.NET.Validation.PSM;

    [TestFixture]
    public class PrimitiveConstraintRequestValidatorTestFixture
    {
        [Test]
        public void VerifyValidate()
        {
            var validator = new PrimitiveConstraintRequestValidator();

            var primitiveConstraintRequest = new PrimitiveConstraintRequest
            {
                Operator = PrimitiveConstraintRequestOperator.equalto,
                Property = "name",
                Value = [ConstraintValue.From("the value")]
            };

            Assert.That(validator.Validate(primitiveConstraintRequest).IsValid, Is.True);

            primitiveConstraintRequest.Value = null;

            Assert.That(validator.Validate(primitiveConstraintRequest).IsValid, Is.True);

            primitiveConstraintRequest.Value = [];

            Assert.That(validator.Validate(primitiveConstraintRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Value"]));

            primitiveConstraintRequest.Value = [ConstraintValue.Null];
            primitiveConstraintRequest.Property = null;
            primitiveConstraintRequest.Operator = (PrimitiveConstraintRequestOperator)99;

            Assert.That(validator.Validate(primitiveConstraintRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Property", "Operator"]));
        }
    }
}
