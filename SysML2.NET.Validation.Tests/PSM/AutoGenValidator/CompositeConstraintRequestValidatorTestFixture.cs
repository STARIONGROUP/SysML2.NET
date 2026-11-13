// ------------------------------------------------------------------------------------------------
// <copyright file="CompositeConstraintRequestValidatorTestFixture.cs" company="Starion Group S.A.">
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
    public class CompositeConstraintRequestValidatorTestFixture
    {
        [Test]
        public void VerifyValidate()
        {
            var validator = new CompositeConstraintRequestValidator();
            var constraint = new PrimitiveConstraint { Property = "name" };

            var compositeConstraintRequest = new CompositeConstraintRequest
            {
                Constraint = [constraint, constraint],
                Operator = CompositeConstraintRequestOperator.and
            };

            Assert.That(validator.Validate(compositeConstraintRequest).IsValid, Is.True);

            compositeConstraintRequest.Constraint = [constraint];

            Assert.That(validator.Validate(compositeConstraintRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Constraint"]));

            compositeConstraintRequest.Constraint = null;

            Assert.That(validator.Validate(compositeConstraintRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Constraint"]));

            compositeConstraintRequest.Constraint = [constraint, null];

            Assert.That(validator.Validate(compositeConstraintRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Constraint[1]"]));

            compositeConstraintRequest.Constraint = [constraint, constraint];
            compositeConstraintRequest.Operator = (CompositeConstraintRequestOperator)99;

            Assert.That(validator.Validate(compositeConstraintRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Operator"]));
        }
    }
}
