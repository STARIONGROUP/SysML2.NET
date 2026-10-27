// ------------------------------------------------------------------------------------------------
// <copyright file="PackageExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class PackageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeFilterCondition()
        {
            Assert.That(() => ((IPackage)null).ComputeFilterCondition(), Throws.TypeOf<ArgumentNullException>());

            var package = new Package();

            Assert.That(package.ComputeFilterCondition(), Is.Empty);

            var membership = new ElementFilterMembership();
            var expression = new BooleanExpression();

            var annotation = new Annotation();
            var comment = new Comment();

            package.AssignOwnership(membership, expression);
            package.AssignOwnership(annotation, comment);

            // ElementFilterMembership.condition (ComputeCondition) is implemented — the
            // populated case now returns the owned IExpression directly.
            Assert.That(package.ComputeFilterCondition(), Is.EqualTo([expression]));
        }

        [Test]
        public void VerifyComputeIncludeAsMemberOperation()
        {
            Assert.That(() => ((IPackage)null).ComputeIncludeAsMemberOperation(null), Throws.TypeOf<ArgumentNullException>());

            var package = new Package();
            Assert.That(package.ComputeIncludeAsMemberOperation(null), Is.False);

            var element = new Type();
            Assert.That(package.ComputeIncludeAsMemberOperation(element), Is.True);

            var membership = new ElementFilterMembership();
            var expression = new BooleanExpression();

            package.AssignOwnership(membership, expression);

            // ElementFilterMembership.condition is now implemented. A bare BooleanExpression
            // (no ResultExpressionMembership children) evaluates to [] → CheckCondition returns
            // false → element does not pass all filters → includeAsMember returns false.
            Assert.That(package.ComputeIncludeAsMemberOperation(element), Is.False);
        }

        [Test]
        public void VerifyComputeRedefinedImportedMembershipsOperation()
        {
            Assert.That(() => ((IPackage)null).ComputeRedefinedImportedMembershipsOperation([]), Throws.TypeOf<ArgumentNullException>());

            var package = new Package();

            Assert.That(package.ComputeRedefinedImportedMembershipsOperation([]), Is.Empty);

            // Valid MembershipImport (ImportedMembership wired to a real membership) with no
            // filterConditions: MembershipImport.importedMemberships is non-recursive, so it yields
            // [importedMembership]; Package.importedMemberships aggregates it and, absent any
            // ElementFilterMembership, ComputeRedefinedImportedMembershipsOperation returns it as-is.
            var importedMemberNamespace = new Namespace();
            var importedMembership = new Membership { MemberElement = importedMemberNamespace };
            var importMember = new MembershipImport { ImportedMembership = importedMembership, IsRecursive = false };

            package.AssignOwnership(importMember);
            Assert.That(package.ComputeRedefinedImportedMembershipsOperation([]), Does.Contain(importedMembership));

            // Discrimination: a bare BooleanExpression filterCondition evaluates to [] → CheckCondition
            // returns false → the imported membership fails the filter and is excluded → empty result.
            var membership = new ElementFilterMembership();
            var expression = new BooleanExpression();
            package.AssignOwnership(membership, expression);
            Assert.That(package.ComputeRedefinedImportedMembershipsOperation([]), Is.Empty);
        }
    }
}
