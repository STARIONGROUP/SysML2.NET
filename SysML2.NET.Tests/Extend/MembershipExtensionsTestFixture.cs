// -------------------------------------------------------------------------------------------------
// <copyright file="MembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;

    [TestFixture]
    public class MembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeMemberElementId()
        {
            Assert.That(() => ((IMembership)null).ComputeMemberElementId(), Throws.TypeOf<ArgumentNullException>());

            var membership = new Membership();
            var element = new Definition { ElementId = "test-element-id-42" };
            membership.MemberElement = element;

            Assert.That(membership.ComputeMemberElementId(), Is.EqualTo("test-element-id-42"));
        }

        [Test]
        public void VerifyComputeIsDistinguishableFromOperation()
        {
            // Null guards
            Assert.That(() => ((IMembership)null).ComputeIsDistinguishableFromOperation(new Membership()), Throws.TypeOf<ArgumentNullException>());

            Assert.That(() => new Membership().ComputeIsDistinguishableFromOperation(null), Throws.TypeOf<ArgumentNullException>());

            // Clause C: incompatible metaclasses — types do not conform to each other → true
            var subjectIncompat = new Membership { MemberShortName = "A", MemberName = "A" };
            subjectIncompat.MemberElement = new Definition();

            var otherIncompat = new Membership { MemberShortName = "A", MemberName = "A" };
            otherIncompat.MemberElement = new Namespace();

            Assert.That(subjectIncompat.ComputeIsDistinguishableFromOperation(otherIncompat), Is.True);

            // Clause C edge case: null MemberElement on subject — no conformance possible → true
            var subjectNullElement = new Membership { MemberShortName = "A", MemberName = "A" };
            subjectNullElement.MemberElement = null;

            var otherWithElement = new Membership { MemberShortName = "A", MemberName = "A" };
            otherWithElement.MemberElement = new Definition();

            Assert.That(subjectNullElement.ComputeIsDistinguishableFromOperation(otherWithElement), Is.True);

            // Clause C edge case: null MemberElement on other — no conformance possible → true
            var subjectWithElement = new Membership { MemberShortName = "A", MemberName = "A" };
            subjectWithElement.MemberElement = new Definition();

            var otherNullElement = new Membership { MemberShortName = "A", MemberName = "A" };
            otherNullElement.MemberElement = null;

            Assert.That(subjectWithElement.ComputeIsDistinguishableFromOperation(otherNullElement), Is.True);

            // Clause B: same metaclass, all four cross-comparisons differ → true
            var subjectClauseB = new Membership { MemberShortName = "A", MemberName = "B" };
            subjectClauseB.MemberElement = new Definition();

            var otherClauseB = new Membership { MemberShortName = "X", MemberName = "Y" };
            otherClauseB.MemberElement = new Definition();

            Assert.That(subjectClauseB.ComputeIsDistinguishableFromOperation(otherClauseB), Is.True);

            // Clause A: both MemberShortName and MemberName null on subject → true (null name-part wins)
            var subjectClauseA = new Membership { MemberShortName = null, MemberName = null };
            subjectClauseA.MemberElement = new Definition();

            var otherClauseA = new Membership { MemberShortName = "X", MemberName = "Y" };
            otherClauseA.MemberElement = new Definition();

            Assert.That(subjectClauseA.ComputeIsDistinguishableFromOperation(otherClauseA), Is.True);

            // Indistinguishable: same metaclass + MemberShortName clash → false
            var subjectShortClash = new Membership { MemberShortName = "A", MemberName = "B" };
            subjectShortClash.MemberElement = new Definition();

            var otherShortClash = new Membership { MemberShortName = "A", MemberName = "Z" };
            otherShortClash.MemberElement = new Definition();

            Assert.That(subjectShortClash.ComputeIsDistinguishableFromOperation(otherShortClash), Is.False);

            // Indistinguishable: subject.MemberShortName matches other.MemberName → false
            var subjectCrossClash = new Membership { MemberShortName = "A", MemberName = "B" };
            subjectCrossClash.MemberElement = new Definition();

            var otherCrossClash = new Membership { MemberShortName = null, MemberName = "A" };
            otherCrossClash.MemberElement = new Definition();

            Assert.That(subjectCrossClash.ComputeIsDistinguishableFromOperation(otherCrossClash), Is.False);

            // Half-distinguishable: NamePart1 fires but NamePart2 fails (MemberName matches) → false
            var subjectHalf = new Membership { MemberShortName = "A", MemberName = "B" };
            subjectHalf.MemberElement = new Definition();

            var otherHalf = new Membership { MemberShortName = "X", MemberName = "B" };
            otherHalf.MemberElement = new Definition();

            Assert.That(subjectHalf.ComputeIsDistinguishableFromOperation(otherHalf), Is.False);
        }

        [Test]
        public void ComputeMembershipOwningNamespace_ThrowsNotSupportedException()
        {
            Assert.That(() => ((IMembership)null).ComputeMembershipOwningNamespace(), Throws.TypeOf<ArgumentNullException>());
        }
    }
}
