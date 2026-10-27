// ------------------------------------------------------------------------------------------------
// <copyright file="DifferencingExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class DifferencingExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeTypeDifferenced()
        {
            Assert.That(() => ((IDifferencing)null).ComputeTypeDifferenced(), Throws.TypeOf<ArgumentNullException>());

            var emptyDifferencing = new Differencing();

            Assert.That(emptyDifferencing.ComputeTypeDifferenced, Throws.TypeOf<IncompleteModelException>());

            var owningType = new Type();
            var differencing = new Differencing();

            owningType.AssignOwnership(differencing);

            Assert.That(differencing.ComputeTypeDifferenced(), Is.SameAs(owningType));

            var nonTypeDifferencing = new Differencing();
            var nonTypeOwner = new Namespace();

            ((IContainedRelationship)nonTypeDifferencing).OwningRelatedElement = nonTypeOwner;

            Assert.That(nonTypeDifferencing.ComputeTypeDifferenced, Throws.TypeOf<IncompleteModelException>());
        }
    }
}
