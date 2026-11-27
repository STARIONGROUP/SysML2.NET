// ------------------------------------------------------------------------------------------------
// <copyright file="DependencyValidatorTestFixture.cs" company="Starion Group S.A.">
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

    using FluentValidation;

    using NUnit.Framework;

    using SysML2.NET.Core.DTO.Root.Dependencies;
    using SysML2.NET.Validation.Core.DTO;

    [TestFixture]
    public class DependencyValidatorTestFixture
    {
        [Test]
        public void VerifyValidate()
        {
            var validator = new DependencyValidator();
            var dependency = new Dependency { Id = Guid.NewGuid(), ElementId = "the element id", Client = [Guid.NewGuid()], Supplier = [Guid.NewGuid()] };

            Assert.That(validator.Validate(dependency).IsValid, Is.True);

            dependency.Client = [];

            Assert.That(validator.Validate(dependency).Errors.Select(failure => (failure.PropertyName, failure.Severity)), Is.EquivalentTo([("Client", Severity.Error)]));

            dependency.Supplier = null;

            Assert.That(validator.Validate(dependency).Errors.Select(failure => (failure.PropertyName, failure.Severity)), Is.EquivalentTo([("Client", Severity.Error), ("Supplier", Severity.Error)]));

            dependency.Client = [Guid.NewGuid(), Guid.NewGuid()];
            dependency.Supplier = [Guid.NewGuid()];

            Assert.That(validator.Validate(dependency).IsValid, Is.True);
        }
    }
}
