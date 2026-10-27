// ------------------------------------------------------------------------------------------------
// <copyright file="LibraryPackageExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Kernel.Packages;

    [TestFixture]
    public class LibraryPackageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeRedefinedLibraryNamespaceOperation()
        {
            Assert.That(() => ((ILibraryPackage)null).ComputeRedefinedLibraryNamespaceOperation(), Throws.TypeOf<ArgumentNullException>());

            // The libraryNamespace of a LibraryPackage is itself (OCL: self) -> reference identity.
            var subject = new LibraryPackage();

            Assert.That(subject.ComputeRedefinedLibraryNamespaceOperation(), Is.SameAs(subject));
        }
    }
}
