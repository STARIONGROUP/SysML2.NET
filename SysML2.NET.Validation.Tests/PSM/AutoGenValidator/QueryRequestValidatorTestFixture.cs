// ------------------------------------------------------------------------------------------------
// <copyright file="QueryRequestValidatorTestFixture.cs" company="Starion Group S.A.">
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
    public class QueryRequestValidatorTestFixture
    {
        [Test]
        public void VerifyValidate()
        {
            var validator = new QueryRequestValidator();
            var queryRequest = new QueryRequest { Name = "the query" };

            Assert.That(validator.Validate(queryRequest).IsValid, Is.True);

            queryRequest.Where = new PrimitiveConstraintRequest { Property = "name" };

            Assert.That(validator.Validate(queryRequest).IsValid, Is.True);

            queryRequest.Where = new PrimitiveConstraintRequest();

            Assert.That(validator.Validate(queryRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Where.Property"]));

            queryRequest.Where = new CompositeConstraintRequest { Constraint = [new PrimitiveConstraint()], Operator = CompositeConstraintRequestOperator.or };

            Assert.That(validator.Validate(queryRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Where.Constraint"]));

            queryRequest.Where = null;
            queryRequest.Name = null;

            Assert.That(validator.Validate(queryRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Name"]));
        }
    }
}
