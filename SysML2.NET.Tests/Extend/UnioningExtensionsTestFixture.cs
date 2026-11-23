// ------------------------------------------------------------------------------------------------
// <copyright file="UnioningExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;

    using SysMLType = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class UnioningExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeTypeUnioned()
        {
            // Null subject → ArgumentNullException.
            Assert.That(() => ((IUnioning)null).ComputeTypeUnioned(), Throws.TypeOf<ArgumentNullException>());

            var emptyUnioning = new Unioning();

            Assert.That(emptyUnioning.ComputeTypeUnioned(), Is.Null);

            // OwningRelatedElement is an IType → returns the same instance.
            var type = new SysMLType();
            var unioningWithType = new Unioning();

            ((IContainedRelationship)unioningWithType).OwningRelatedElement = type;

            Assert.That(unioningWithType.ComputeTypeUnioned(), Is.SameAs(type));

            var annotation = new Annotation();
            var unioningWithAnnotation = new Unioning();

            ((IContainedRelationship)unioningWithAnnotation).OwningRelatedElement = annotation;

            Assert.That(unioningWithAnnotation.ComputeTypeUnioned(), Is.Null);
        }
    }
}
