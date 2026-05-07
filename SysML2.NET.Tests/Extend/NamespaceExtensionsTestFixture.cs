// -------------------------------------------------------------------------------------------------
// <copyright file="NamespaceExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Tests.Extend
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class NamespaceExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeImportedMembership()
        {
            Assert.That(() => ((INamespace)null).ComputeImportedMembership(), Throws.TypeOf<ArgumentNullException>());

            var namespaceElement = new Namespace();

            Assert.That(namespaceElement.ComputeImportedMembership(), Has.Count.EqualTo(0));

            var importedNamespace = new Namespace();
            var importedElement = new Definition { DeclaredName = "imported" };
            var importedMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            importedNamespace.AssignOwnership(importedMembership, importedElement);

            var namespaceImport = new NamespaceImport { ImportedNamespace = importedNamespace };
            namespaceElement.AssignOwnership(namespaceImport, new Namespace());

            Assert.That(namespaceElement.ComputeImportedMembership(), Is.EquivalentTo([importedMembership]));
        }

        [Test]
        public void VerifyComputeMember()
        {
            Assert.That(() => ((INamespace)null).ComputeMember(), Throws.TypeOf<ArgumentNullException>());

            var namespaceElement = new Namespace();

            Assert.That(namespaceElement.ComputeMember(), Has.Count.EqualTo(0));

            var element = new Definition();
            var membership = new OwningMembership();
            namespaceElement.AssignOwnership(membership, element);

            Assert.That(namespaceElement.ComputeMember(), Is.EquivalentTo([element]));
        }

        [Test]
        public void VerifyComputeMembership()
        {
            Assert.That(() => ((INamespace)null).ComputeMembership(), Throws.TypeOf<ArgumentNullException>());

            var namespaceElement = new Namespace();

            Assert.That(namespaceElement.ComputeMembership(), Has.Count.EqualTo(0));

            var element = new Definition();
            var membership = new OwningMembership();
            namespaceElement.AssignOwnership(membership, element);

            Assert.That(namespaceElement.ComputeMembership(), Is.EquivalentTo([membership]));
        }

        [Test]
        public void VerifyComputeOwnedImport()
        {
            Assert.That(() => ((INamespace)null).ComputeOwnedImport(), Throws.TypeOf<ArgumentNullException>());

            var namespaceElement = new Namespace();

            Assert.That(namespaceElement.ComputeOwnedImport(), Has.Count.EqualTo(0));

            var namespaceImport = new NamespaceImport();
            namespaceElement.AssignOwnership(namespaceImport, new Namespace());

            Assert.That(namespaceElement.ComputeOwnedImport(), Is.EquivalentTo([namespaceImport]));
        }

        [Test]
        public void VerifyComputeOwnedMember()
        {
            Assert.That(() => ((INamespace)null).ComputeOwnedMember(), Throws.TypeOf<ArgumentNullException>());

            var namespaceElement = new Namespace();

            Assert.That(namespaceElement.ComputeOwnedMember(), Has.Count.EqualTo(0));

            var element = new Definition();
            var membership = new OwningMembership();
            namespaceElement.AssignOwnership(membership, element);

            Assert.That(namespaceElement.ComputeOwnedMember(), Is.EquivalentTo([element]));
        }

        [Test]
        public void VerifyComputeOwnedMembership()
        {
            Assert.That(() => ((INamespace)null).ComputeOwnedMembership(), Throws.TypeOf<ArgumentNullException>());

            var namespaceElement = new Namespace();

            Assert.That(namespaceElement.ComputeOwnedMembership(), Has.Count.EqualTo(0));

            var element = new Definition();
            var membership = new OwningMembership();
            namespaceElement.AssignOwnership(membership, element);

            Assert.That(namespaceElement.ComputeOwnedMembership(), Is.EquivalentTo([membership]));
        }

        [Test]
        public void VerifyComputeNamesOfOperation()
        {
            Assert.That(() => ((INamespace)null).ComputeNamesOfOperation(new Definition()), Throws.TypeOf<ArgumentNullException>());

            var namespaceElement = new Namespace();

            Assert.That(() => namespaceElement.ComputeNamesOfOperation(null), Throws.TypeOf<ArgumentNullException>());

            var element = new Definition();

            Assert.That(namespaceElement.ComputeNamesOfOperation(element), Has.Count.EqualTo(0));

            var membership = new Membership { MemberName = "elementName", MemberShortName = "en", MemberElement = element };
            namespaceElement.AssignOwnership(membership, element);

            Assert.That(namespaceElement.ComputeNamesOfOperation(element), Is.EquivalentTo(["en", "elementName"]));

            membership.MemberShortName = null;

            Assert.That(namespaceElement.ComputeNamesOfOperation(element), Is.EquivalentTo(["elementName"]));
        }

        [Test]
        public void VerifyComputeVisibilityOfOperation()
        {
            Assert.That(() => ((INamespace)null).ComputeVisibilityOfOperation(new Membership()), Throws.TypeOf<ArgumentNullException>());

            var namespaceElement = new Namespace();

            Assert.That(() => namespaceElement.ComputeVisibilityOfOperation(null), Throws.TypeOf<ArgumentNullException>());

            var element = new Definition();
            var membership = new OwningMembership();
            namespaceElement.AssignOwnership(membership, element);

            Assert.That(namespaceElement.ComputeVisibilityOfOperation(membership), Is.EqualTo(VisibilityKind.Public));

            membership.Visibility = VisibilityKind.Private;

            Assert.That(namespaceElement.ComputeVisibilityOfOperation(membership), Is.EqualTo(VisibilityKind.Private));

            var unrelatedMembership = new Membership();

            Assert.That(namespaceElement.ComputeVisibilityOfOperation(unrelatedMembership), Is.EqualTo(VisibilityKind.Private));
        }

        [Test]
        public void VerifyComputeVisibleMembershipsOperation()
        {
            Assert.That(() => ((INamespace)null).ComputeVisibleMembershipsOperation([], false, false), Throws.TypeOf<ArgumentNullException>());

            var namespaceElement = new Namespace();
            var element = new Definition();
            var publicMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            var privateMembership = new OwningMembership { Visibility = VisibilityKind.Private };
            var privateElement = new Definition();

            namespaceElement.AssignOwnership(publicMembership, element);
            namespaceElement.AssignOwnership(privateMembership, privateElement);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(namespaceElement.ComputeVisibleMembershipsOperation([], false, true), Has.Count.EqualTo(2));
                Assert.That(namespaceElement.ComputeVisibleMembershipsOperation([], false, false), Is.EquivalentTo([publicMembership]));
            }

            var nestedNamespace = new Namespace();
            var nestedElement = new Definition();
            var nestedMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            nestedNamespace.AssignOwnership(nestedMembership, nestedElement);

            var nestedOwning = new OwningMembership { Visibility = VisibilityKind.Public };
            namespaceElement.AssignOwnership(nestedOwning, nestedNamespace);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(namespaceElement.ComputeVisibleMembershipsOperation([], false, false), Has.Count.EqualTo(2));
                Assert.That(() => namespaceElement.ComputeVisibleMembershipsOperation([], true, false), Throws.TypeOf<NotSupportedException>());
            }
        }

        [Test]
        public void VerifyComputeImportedMembershipsOperation()
        {
            var namespaceElement = new Namespace();

            Assert.That(namespaceElement.ComputeImportedMembershipsOperation([]), Has.Count.EqualTo(0));

            var importedNamespace = new Namespace();
            var importedElement = new Definition { DeclaredName = "imported" };
            var importedMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            importedNamespace.AssignOwnership(importedMembership, importedElement);

            var namespaceImport = new NamespaceImport { ImportedNamespace = importedNamespace };
            namespaceElement.AssignOwnership(namespaceImport, new Namespace());

            Assert.That(namespaceElement.ComputeImportedMembershipsOperation([]), Is.EquivalentTo([importedMembership]));

            var collidingElement = new Definition { DeclaredName = "imported" };
            var ownedMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            namespaceElement.AssignOwnership(ownedMembership, collidingElement);

            Assert.That(namespaceElement.ComputeImportedMembershipsOperation([]), Has.Count.EqualTo(0));
        }

        [Test]
        public void VerifyComputeMembershipsOfVisibilityOperation()
        {
            Assert.That(() => ((INamespace)null).ComputeMembershipsOfVisibilityOperation(null, []), Throws.TypeOf<ArgumentNullException>());

            var namespaceElement = new Namespace();
            var publicElement = new Definition();
            var privateElement = new Definition();
            var publicMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            var privateMembership = new OwningMembership { Visibility = VisibilityKind.Private };

            namespaceElement.AssignOwnership(publicMembership, publicElement);
            namespaceElement.AssignOwnership(privateMembership, privateElement);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(namespaceElement.ComputeMembershipsOfVisibilityOperation(null, []), Has.Count.EqualTo(2));
                Assert.That(namespaceElement.ComputeMembershipsOfVisibilityOperation(VisibilityKind.Public, []), Is.EquivalentTo([publicMembership]));
                Assert.That(namespaceElement.ComputeMembershipsOfVisibilityOperation(VisibilityKind.Private, []), Is.EquivalentTo([privateMembership]));
                Assert.That(namespaceElement.ComputeMembershipsOfVisibilityOperation(VisibilityKind.Protected, []), Has.Count.EqualTo(0));
            }
        }

        [Test]
        public void VerifyComputeResolveVisibleOperation()
        {
            Assert.That(() => ((INamespace)null).ComputeResolveVisibleOperation("name"), Throws.TypeOf<ArgumentNullException>());

            var namespaceElement = new Namespace();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(namespaceElement.ComputeResolveVisibleOperation(null), Is.Null);
                Assert.That(namespaceElement.ComputeResolveVisibleOperation("  "), Is.Null);
            }

            var element = new Definition { DeclaredName = "myElement" };
            var membership = new OwningMembership { Visibility = VisibilityKind.Public };
            namespaceElement.AssignOwnership(membership, element);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(namespaceElement.ComputeResolveVisibleOperation("myElement"), Is.EqualTo(membership));
                Assert.That(namespaceElement.ComputeResolveVisibleOperation("nonExistent"), Is.Null);
            }

            membership.Visibility = VisibilityKind.Private;

            Assert.That(namespaceElement.ComputeResolveVisibleOperation("myElement"), Is.Null);
        }

        [Test]
        public void VerifyComputeResolveLocalOperation()
        {
            Assert.That(() => ((INamespace)null).ComputeResolveLocalOperation("name"), Throws.TypeOf<ArgumentNullException>());

            var rootNamespace = new Namespace();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rootNamespace.ComputeResolveLocalOperation(null), Is.Null);
                Assert.That(rootNamespace.ComputeResolveLocalOperation("  "), Is.Null);
            }

            var element = new Definition { DeclaredName = "myElement" };
            var membership = new OwningMembership { Visibility = VisibilityKind.Public };
            rootNamespace.AssignOwnership(membership, element);

            Assert.That(rootNamespace.ComputeResolveLocalOperation("myElement"), Is.EqualTo(membership));

            var childNamespace = new Namespace();
            var childOwning = new OwningMembership { Visibility = VisibilityKind.Public };
            rootNamespace.AssignOwnership(childOwning, childNamespace);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(childNamespace.ComputeResolveLocalOperation("myElement"), Is.EqualTo(membership));
                Assert.That(childNamespace.ComputeResolveLocalOperation("nonExistent"), Is.Null);
            }
        }

        [Test]
        public void VerifyComputeResolveOperation()
        {
            Assert.That(() => ((INamespace)null).ComputeResolveOperation("name"), Throws.TypeOf<ArgumentNullException>());

            var rootNamespace = new Namespace();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rootNamespace.ComputeResolveOperation(null), Is.Null);
                Assert.That(rootNamespace.ComputeResolveOperation("  "), Is.Null);
            }

            var childNamespace = new Namespace { DeclaredName = "child" };
            var childMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            rootNamespace.AssignOwnership(childMembership, childNamespace);

            var element = new Definition { DeclaredName = "leaf" };
            var elementMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            childNamespace.AssignOwnership(elementMembership, element);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rootNamespace.ComputeResolveOperation("child"), Is.EqualTo(childMembership));
                Assert.That(rootNamespace.ComputeResolveOperation("child::leaf"), Is.EqualTo(elementMembership));
                Assert.That(rootNamespace.ComputeResolveOperation("child::nonExistent"), Is.Null);
                Assert.That(rootNamespace.ComputeResolveOperation("nonExistent::leaf"), Is.Null);
            }
        }

        [Test]
        public void VerifyComputeResolveGlobalOperation()
        {
            Assert.That(() => ((INamespace)null).ComputeResolveGlobalOperation("name"), Throws.TypeOf<ArgumentNullException>());

            var rootNamespace = new Namespace();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rootNamespace.ComputeResolveGlobalOperation(null), Is.Null);
                Assert.That(rootNamespace.ComputeResolveGlobalOperation("  "), Is.Null);
            }

            var childNamespace = new Namespace { DeclaredName = "child" };
            var childMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            rootNamespace.AssignOwnership(childMembership, childNamespace);

            var element = new Definition { DeclaredName = "leaf" };
            var elementMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            childNamespace.AssignOwnership(elementMembership, element);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(childNamespace.ComputeResolveGlobalOperation("child"), Is.EqualTo(childMembership));
                Assert.That(childNamespace.ComputeResolveGlobalOperation("child::leaf"), Is.EqualTo(elementMembership));
                Assert.That(childNamespace.ComputeResolveGlobalOperation("nonExistent"), Is.Null);
            }
        }

        [Test]
        public void VerifyComputeQualificationOfOperation()
        {
            Assert.That(() => ((INamespace)null).ComputeQualificationOfOperation("name"), Throws.TypeOf<ArgumentNullException>());

            var namespaceElement = new Namespace();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(namespaceElement.ComputeQualificationOfOperation(null), Is.Null);
                Assert.That(namespaceElement.ComputeQualificationOfOperation("  "), Is.Null);
                Assert.That(namespaceElement.ComputeQualificationOfOperation("simpleName"), Is.Null);
                Assert.That(namespaceElement.ComputeQualificationOfOperation("a::b"), Is.EqualTo("a"));
                Assert.That(namespaceElement.ComputeQualificationOfOperation("a::b::c"), Is.EqualTo("a::b"));
                Assert.That(namespaceElement.ComputeQualificationOfOperation("'a::b'::c"), Is.EqualTo("'a::b'"));
                Assert.That(namespaceElement.ComputeQualificationOfOperation("a::'b::c'"), Is.EqualTo("a"));
            }
        }

        [Test]
        public void VerifyComputeUnqualifiedNameOfOperation()
        {
            Assert.That(() => ((INamespace)null).ComputeUnqualifiedNameOfOperation("name"), Throws.TypeOf<ArgumentNullException>());

            var namespaceElement = new Namespace();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(namespaceElement.ComputeUnqualifiedNameOfOperation(null), Is.Null);
                Assert.That(namespaceElement.ComputeUnqualifiedNameOfOperation("  "), Is.Null);
                Assert.That(namespaceElement.ComputeUnqualifiedNameOfOperation("simpleName"), Is.EqualTo("simpleName"));
                Assert.That(namespaceElement.ComputeUnqualifiedNameOfOperation("a::b"), Is.EqualTo("b"));
                Assert.That(namespaceElement.ComputeUnqualifiedNameOfOperation("a::b::c"), Is.EqualTo("c"));
                Assert.That(namespaceElement.ComputeUnqualifiedNameOfOperation("a::'non basic'"), Is.EqualTo("non basic"));
                Assert.That(namespaceElement.ComputeUnqualifiedNameOfOperation("a::'it\\'s'"), Is.EqualTo("it's"));
                Assert.That(namespaceElement.ComputeUnqualifiedNameOfOperation("'a::b'::c"), Is.EqualTo("c"));
            }
        }
    }
}
