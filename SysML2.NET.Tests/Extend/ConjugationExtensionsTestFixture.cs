// ------------------------------------------------------------------------------------------------
// <copyright file="ConjugationExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class ConjugationExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwningType()
        {
            Assert.That(() => ((IConjugation)null).ComputeOwningType(), Throws.TypeOf<ArgumentNullException>());

            var emptyConjugation = new Conjugation();

            Assert.That(emptyConjugation.ComputeOwningType(), Is.Null);

            var owningType = new Type();
            var conjugation = new Conjugation();

            owningType.AssignOwnership(conjugation);

            Assert.That(conjugation.ComputeOwningType(), Is.SameAs(owningType));

            var nonTypeConjugation = new Conjugation();
            var nonTypeOwner = new Namespace();

            ((IContainedRelationship)nonTypeConjugation).OwningRelatedElement = nonTypeOwner;

            Assert.That(nonTypeConjugation.ComputeOwningType(), Is.Null);
        }
    }
}
