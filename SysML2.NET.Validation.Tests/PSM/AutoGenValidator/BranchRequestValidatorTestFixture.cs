// ------------------------------------------------------------------------------------------------
// <copyright file="BranchRequestValidatorTestFixture.cs" company="Starion Group S.A.">
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
    public class BranchRequestValidatorTestFixture
    {
        [Test]
        public void VerifyValidate()
        {
            var validator = new BranchRequestValidator();
            var branchRequest = new BranchRequest { Name = "the branch", Head = Guid.NewGuid() };

            Assert.That(validator.Validate(branchRequest).IsValid, Is.True);

            branchRequest.Head = null;

            Assert.That(validator.Validate(branchRequest).IsValid, Is.True);

            branchRequest.Head = Guid.Empty;

            Assert.That(validator.Validate(branchRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Head"]));

            branchRequest.Head = Guid.NewGuid();
            branchRequest.Name = null;

            Assert.That(validator.Validate(branchRequest).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Name"]));
        }
    }
}
