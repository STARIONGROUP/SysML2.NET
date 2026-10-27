// ------------------------------------------------------------------------------------------------
// <copyright file="EnumerationDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Tests.Extend
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Systems.Attributes;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Enumerations;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class EnumerationDefinitionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeEnumeratedValue()
        {
            Assert.That(() => ((IEnumerationDefinition)null).ComputeEnumeratedValue(), Throws.TypeOf<ArgumentNullException>());

            // Empty: no variant memberships → empty list.
            var emptySubject = new EnumerationDefinition();

            Assert.That(emptySubject.ComputeEnumeratedValue(), Is.Empty);

            // Populated: two VariantMemberships, one owning an EnumerationUsage variant, the other owning a
            // non-EnumerationUsage variant (AttributeUsage). Only the EnumerationUsage survives the kind filter,
            // and the variantMembership order is preserved.
            var subject = new EnumerationDefinition();
            var enumeratedValue = new EnumerationUsage();
            var nonEnumeratedVariant = new AttributeUsage();
            subject.AssignOwnership(new VariantMembership(), enumeratedValue);
            subject.AssignOwnership(new VariantMembership(), nonEnumeratedVariant);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeEnumeratedValue(), Is.EqualTo([enumeratedValue]));
                Assert.That(subject.ComputeEnumeratedValue(), Does.Not.Contain(nonEnumeratedVariant));
            }
        }
    }
}
