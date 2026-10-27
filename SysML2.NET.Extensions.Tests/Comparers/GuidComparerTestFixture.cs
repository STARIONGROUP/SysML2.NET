// ------------------------------------------------------------------------------------------------
// <copyright file="GuidComparerTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Extensions.Tests.Comparers
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Extensions.Comparers;

    [TestFixture]
    public class GuidComparerTestFixture
    {
        private GuidComparer comparer;

        [SetUp]
        public void SetUp()
        {
            this.comparer = new GuidComparer();
        }

        [Test]
        public void Verify_that_comparing_equal_guids_returns_zero()
        {
            var guid = Guid.NewGuid();

            var result = this.comparer.Compare(guid, guid);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Verify_that_comparing_two_identical_guid_values_returns_zero()
        {
            var guid = Guid.NewGuid();
            var sameGuid = new Guid(guid.ToByteArray());

            var result = this.comparer.Compare(guid, sameGuid);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Verify_that_comparing_a_smaller_guid_with_a_larger_guid_returns_a_negative_value()
        {
            var smaller = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var larger = Guid.Parse("00000000-0000-0000-0000-000000000002");

            var result = this.comparer.Compare(smaller, larger);

            Assert.That(result, Is.LessThan(0));
        }

        [Test]
        public void Verify_that_comparing_a_larger_guid_with_a_smaller_guid_returns_a_positive_value()
        {
            var smaller = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var larger = Guid.Parse("00000000-0000-0000-0000-000000000002");

            var result = this.comparer.Compare(larger, smaller);

            Assert.That(result, Is.GreaterThan(0));
        }

        [Test]
        public void Verify_that_guid_comparison_is_symmetric()
        {
            var guidA = Guid.NewGuid();
            var guidB = Guid.NewGuid();

            var ab = this.comparer.Compare(guidA, guidB);
            var ba = this.comparer.Compare(guidB, guidA);

            Assert.That(ab, Is.EqualTo(-ba));
        }

        [Test]
        public void Verify_that_guid_comparison_is_consistent_across_multiple_calls()
        {
            var guidA = Guid.NewGuid();
            var guidB = Guid.NewGuid();

            var first = this.comparer.Compare(guidA, guidB);
            var second = this.comparer.Compare(guidA, guidB);

            Assert.That(first, Is.EqualTo(second));
        }

        [Test]
        public void Verify_that_empty_guid_is_less_than_any_non_empty_guid()
        {
            var empty = Guid.Empty;
            var nonEmpty = Guid.NewGuid();

            var result = this.comparer.Compare(empty, nonEmpty);

            Assert.That(result, Is.LessThan(0));
        }

        [Test]
        public void Verify_that_non_empty_guid_is_greater_than_empty_guid()
        {
            var empty = Guid.Empty;
            var nonEmpty = Guid.NewGuid();

            var result = this.comparer.Compare(nonEmpty, empty);

            Assert.That(result, Is.GreaterThan(0));
        }
    }
}
