// ------------------------------------------------------------------------------------------------
// <copyright file="ConjugatedPortDefinitionValidatorTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.DTO.Systems.Ports;
    using SysML2.NET.Validation.Core.DTO;

    [TestFixture]
    public class ConjugatedPortDefinitionValidatorTestFixture
    {
        [Test]
        public void VerifyValidate()
        {
            var validator = new ConjugatedPortDefinitionValidator();
            var conjugatedPortDefinition = new ConjugatedPortDefinition { Id = Guid.NewGuid(), ElementId = "the element id", OwnedRelationship = [Guid.NewGuid()] };

            Assert.That(validator.Validate(conjugatedPortDefinition).IsValid, Is.True);

            conjugatedPortDefinition.OwnedRelationship = [];

            Assert.That(validator.Validate(conjugatedPortDefinition).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["OwnedRelationship"]));

            conjugatedPortDefinition.OwnedRelationship = null;

            Assert.That(validator.Validate(conjugatedPortDefinition).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["OwnedRelationship"]));
        }
    }
}
