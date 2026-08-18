// -------------------------------------------------------------------------------------------------
// <copyright file="InheritanceScopeTestFixture.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

namespace SysML2.NET.Tests.Extensions
{
    using NUnit.Framework;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class InheritanceScopeTestFixture
    {
        [Test]
        public void VerifyBegin()
        {
            Assert.That(InheritanceScope.Current, Is.Null, "no scope is open before the first Begin");

            using (var outerScope = InheritanceScope.Begin())
            {
                Assert.That(InheritanceScope.Current, Is.SameAs(outerScope));

                // Scopes nest: the inner one takes over, and the outer is restored when it closes.
                using (var innerScope = InheritanceScope.Begin())
                {
                    Assert.That(InheritanceScope.Current, Is.SameAs(innerScope));
                    Assert.That(innerScope, Is.Not.SameAs(outerScope));
                }

                Assert.That(InheritanceScope.Current, Is.SameAs(outerScope));
            }

            Assert.That(InheritanceScope.Current, Is.Null);
        }

        [Test]
        public void VerifyDispose()
        {
            var scope = InheritanceScope.Begin();

            var subject = new Type();
            subject.AssignOwnership(new Specialization { Specific = subject, General = BuildSupertypeWithPublicMembership(out _) });

            Assert.That(subject.ComputeInheritedMembership(), Has.Count.EqualTo(1));
            Assert.That(scope.DefaultSignatureResults, Is.Not.Empty, "the resolved supertype is retained while the scope is open");

            scope.Dispose();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(InheritanceScope.Current, Is.Null);
                Assert.That(scope.DefaultSignatureResults, Is.Empty, "disposal releases the retained POCOs");
            }

            // Disposing twice is a no-op rather than clobbering whichever scope is current by then.
            using (var replacementScope = InheritanceScope.Begin())
            {
                scope.Dispose();

                Assert.That(InheritanceScope.Current, Is.SameAs(replacementScope));
            }

            Assert.That(InheritanceScope.Current, Is.Null);
        }

        [Test]
        public void VerifyDisposeOutOfOrder()
        {
            var outerScope = InheritanceScope.Begin();
            var innerScope = InheritanceScope.Begin();

            // Closing the ENCLOSING scope first must not strand the one still open: the inner scope stays
            // current, so queries made through it keep sharing.
            outerScope.Dispose();

            Assert.That(InheritanceScope.Current, Is.SameAs(innerScope));

            // And closing the inner one then returns to no-scope rather than to the closed outer scope.
            innerScope.Dispose();

            Assert.That(InheritanceScope.Current, Is.Null);
        }

        [Test]
        public void VerifyScopedResolutionMatchesUnscopedResolution()
        {
            // Two Types over a SHARED supertype: the case the scope exists to collapse, since both
            // resolve the same supertype subtree.
            var sharedSupertype = BuildSupertypeWithPublicMembership(out var sharedMembership);

            var firstSubtype = new Type();
            firstSubtype.AssignOwnership(new Specialization { Specific = firstSubtype, General = sharedSupertype });

            var secondSubtype = new Type();
            secondSubtype.AssignOwnership(new Specialization { Specific = secondSubtype, General = sharedSupertype });

            var unscopedFirst = firstSubtype.ComputeInheritedMembership();
            var unscopedSecond = secondSubtype.ComputeInheritedMembership();

            using (InheritanceScope.Begin())
            {
                using (Assert.EnterMultipleScope())
                {
                    // The second query reads the first's cached entry, and must still agree.
                    Assert.That(firstSubtype.ComputeInheritedMembership(), Is.EqualTo(unscopedFirst));
                    Assert.That(secondSubtype.ComputeInheritedMembership(), Is.EqualTo(unscopedSecond));
                    Assert.That(unscopedFirst, Is.EqualTo([sharedMembership]));
                }
            }

            // A non-default signature never shares, so an excluded supertype still drops out even though
            // the default-signature answer for the same Type is already cached.
            using (InheritanceScope.Begin())
            {
                Assert.That(firstSubtype.ComputeInheritedMembership(), Is.EqualTo([sharedMembership]));
                Assert.That(firstSubtype.ComputeInheritedMembershipsOperation(null, [sharedSupertype], false), Is.Empty);
            }
        }

        [Test]
        public void VerifyScopedResolutionOfCircularSpecialization()
        {
            // KerML §8.2.3.5.1 makes circular Specializations LEGAL, which makes inheritance resolution
            // PATH-DEPENDENT: a Type reached along a cycle must not be answered from a cache keyed on the
            // Type alone. Widening the memo's lifetime to a scope must not weaken that guard.
            var first = new Type();
            var second = new Type();

            var firstMembership = new FeatureMembership { Visibility = VisibilityKind.Public };
            first.AssignOwnership(firstMembership, new Feature { DeclaredName = "first" });

            var secondMembership = new FeatureMembership { Visibility = VisibilityKind.Public };
            second.AssignOwnership(secondMembership, new Feature { DeclaredName = "second" });

            first.AssignOwnership(new Specialization { Specific = first, General = second });
            second.AssignOwnership(new Specialization { Specific = second, General = first });

            using (InheritanceScope.Begin())
            {
                using (Assert.EnterMultipleScope())
                {
                    // Identical to the unscoped answers: each Type inherits the other's membership and
                    // never its own, so neither is served a cached entry from the other's walk.
                    Assert.That(first.ComputeInheritedMembership(), Is.EqualTo([secondMembership]));
                    Assert.That(second.ComputeInheritedMembership(), Is.EqualTo([firstMembership]));

                    // Repeating them inside the same scope stays stable.
                    Assert.That(first.ComputeInheritedMembership(), Is.EqualTo([secondMembership]));
                    Assert.That(second.ComputeInheritedMembership(), Is.EqualTo([firstMembership]));
                }
            }

            // A third Type specializing BOTH members of the cycle reaches them by a path neither reaches
            // itself by, so it is the shape most likely to be served a wrongly-cached entry. Its answer
            // must be the same whether or not the cycle members were queried first inside the same scope.
            var descendant = new Type();
            descendant.AssignOwnership(new Specialization { Specific = descendant, General = first });
            descendant.AssignOwnership(new Specialization { Specific = descendant, General = second });

            var unscopedDescendant = descendant.ComputeInheritedMembership();

            using (InheritanceScope.Begin())
            {
                first.ComputeInheritedMembership();
                second.ComputeInheritedMembership();

                Assert.That(descendant.ComputeInheritedMembership(), Is.EqualTo(unscopedDescendant));

                // Both cycle members contribute, each reached down both branches of the diamond.
                Assert.That(unscopedDescendant, Does.Contain(firstMembership));
                Assert.That(unscopedDescendant, Does.Contain(secondMembership));
            }
        }

        /// <summary>
        /// Builds a Type carrying a single public Membership, which a subtype therefore inherits.
        /// </summary>
        /// <param name="publicMembership">The public Membership the returned Type owns.</param>
        /// <returns>The supertype.</returns>
        private static Type BuildSupertypeWithPublicMembership(out IOwningMembership publicMembership)
        {
            var supertype = new Type();
            publicMembership = new OwningMembership { Visibility = VisibilityKind.Public };
            supertype.AssignOwnership(publicMembership, new Type());

            return supertype;
        }
    }
}
