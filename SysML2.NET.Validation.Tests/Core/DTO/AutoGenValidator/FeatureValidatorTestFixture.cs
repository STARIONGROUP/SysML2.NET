// ------------------------------------------------------------------------------------------------
// <copyright file="FeatureValidatorTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.DTO.Core.Features;
    using SysML2.NET.Validation.Core.DTO;

    [TestFixture]
    public class FeatureValidatorTestFixture
    {
        [Test]
        public void VerifyValidate()
        {
            var validator = new FeatureValidator();
            var feature = new Feature { Id = Guid.NewGuid(), ElementId = "the element id" };

            Assert.That(validator.Validate(feature).IsValid, Is.True);

            feature.Direction = FeatureDirectionKind.Inout;

            Assert.That(validator.Validate(feature).IsValid, Is.True);

            feature.Direction = (FeatureDirectionKind)int.MaxValue;

            Assert.That(validator.Validate(feature).Errors.Select(failure => failure.PropertyName), Is.EquivalentTo(["Direction"]));
        }
    }
}
