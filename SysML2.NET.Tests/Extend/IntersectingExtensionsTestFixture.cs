// ------------------------------------------------------------------------------------------------
// <copyright file="IntersectingExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class IntersectingExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeTypeIntersected()
        {
            // Null subject → ArgumentNullException.
            Assert.That(() => ((IIntersecting)null).ComputeTypeIntersected(), Throws.TypeOf<ArgumentNullException>());

            var emptyIntersecting = new Intersecting();

            Assert.That(emptyIntersecting.ComputeTypeIntersected(), Is.Null);

            // OwningRelatedElement is an IType → returns the same instance.
            var type = new Type();
            var intersectingWithType = new Intersecting();

            ((IContainedRelationship)intersectingWithType).OwningRelatedElement = type;

            Assert.That(intersectingWithType.ComputeTypeIntersected(), Is.SameAs(type));

            var namespaceObj = new Namespace();
            var intersectingWithNamespace = new Intersecting();

            ((IContainedRelationship)intersectingWithNamespace).OwningRelatedElement = namespaceObj;

            Assert.That(intersectingWithNamespace.ComputeTypeIntersected(), Is.Null);
        }
    }
}
