// ------------------------------------------------------------------------------------------------
// <copyright file="DisjoiningExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    public class DisjoiningExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwningType()
        {
            Assert.That(() => ((IDisjoining)null).ComputeOwningType(), Throws.TypeOf<ArgumentNullException>());

            var emptyDisjoining = new Disjoining();

            Assert.That(emptyDisjoining.ComputeOwningType(), Is.Null);

            var owningType = new Type();
            var disjoining = new Disjoining();

            owningType.AssignOwnership(disjoining);

            Assert.That(disjoining.ComputeOwningType(), Is.SameAs(owningType));

            var nonTypeDisjoining = new Disjoining();
            var nonTypeOwner = new Namespace();

            ((IContainedRelationship)nonTypeDisjoining).OwningRelatedElement = nonTypeOwner;

            Assert.That(nonTypeDisjoining.ComputeOwningType(), Is.Null);
        }
    }
}
