// ------------------------------------------------------------------------------------------------
// <copyright file="NotationInvariantsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Tests.Extensions
{
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Extensions;

    [TestFixture]
    public class NotationInvariantsTestFixture
    {
        [Test]
        public void VerifyQueryMetamodelName()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(NotationInvariants.QueryMetamodelName(NotationInvariants.ResultMemberMetaclass), Is.EqualTo("ReturnParameterMembership"));
                Assert.That(NotationInvariants.QueryMetamodelName(NotationInvariants.ImpliedDirectionProperty), Is.EqualTo("direction"));
                Assert.That(NotationInvariants.QueryMetamodelName("NoSuchInvariant"), Is.Null);
                Assert.That(NotationInvariants.QueryMetamodelName(null), Is.Null);
            }
        }

        [Test]
        public void VerifyQueryMetaclass()
        {
            // Without a cache source there is nothing to resolve against, and an unknown key has no name to
            // resolve — neither may throw, because both simply disable the rule the invariant backs.
            using (Assert.EnterMultipleScope())
            {
                Assert.That(NotationInvariants.QueryMetaclass(NotationInvariants.ResultMemberMetaclass, null), Is.Null);
                Assert.That(NotationInvariants.QueryMetaclass("NoSuchInvariant", null), Is.Null);
                Assert.That(NotationInvariants.QueryMetaclass(null, null), Is.Null);
            }
        }

        [Test]
        public void VerifyQueryUnresolvedInvariants()
        {
            NotationInvariants.MarkResolved(NotationInvariants.ImpliedDirectionProperty);

            // Marking must be tolerant: a blank key records nothing rather than corrupting the set.
            NotationInvariants.MarkResolved(null);
            NotationInvariants.MarkResolved("   ");

            var unresolved = NotationInvariants.QueryUnresolvedInvariants();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(unresolved, Is.Not.Null);
                Assert.That(unresolved.Any(invariant => invariant.Name == NotationInvariants.ImpliedDirectionProperty), Is.False);

                // An unresolved entry has to be actionable: it names the anchor that went missing and why it
                // mattered, because the rule it backs is silently off until it is re-anchored.
                Assert.That(unresolved.All(invariant => !string.IsNullOrWhiteSpace(invariant.MetamodelName)), Is.True);
                Assert.That(unresolved.All(invariant => !string.IsNullOrWhiteSpace(invariant.Justification)), Is.True);
            }
        }
    }
}
