// ------------------------------------------------------------------------------------------------
// <copyright file="GuidSequenceEqualityTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Extensions.Tests.Comparers
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using SysML2.NET.Extensions.Comparers;

    [TestFixture]
    public class GuidSequenceEqualityTestFixture
    {
        [Test]
        public void Verify_that_ordered_equal_returns_true_for_the_same_reference()
        {
            var list = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            Assert.That(GuidSequenceEquality.OrderedEqual(list, list), Is.True);
        }

        [Test]
        public void Verify_that_unordered_equal_returns_true_for_the_same_reference()
        {
            var list = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            Assert.That(GuidSequenceEquality.UnorderedEqual(list, list), Is.True);
        }

        [Test]
        public void Verify_that_ordered_equal_returns_false_when_one_sequence_is_null()
        {
            var list = new List<Guid> { Guid.NewGuid() };

            Assert.That(GuidSequenceEquality.OrderedEqual(list, null), Is.False);
            Assert.That(GuidSequenceEquality.OrderedEqual(null, list), Is.False);
        }

        [Test]
        public void Verify_that_unordered_equal_returns_false_when_one_sequence_is_null()
        {
            var list = new List<Guid> { Guid.NewGuid() };

            Assert.That(GuidSequenceEquality.UnorderedEqual(list, null), Is.False);
            Assert.That(GuidSequenceEquality.UnorderedEqual(null, list), Is.False);
        }

        [Test]
        public void Verify_that_ordered_equal_returns_true_for_sequences_with_same_elements_in_same_order()
        {
            var g1 = Guid.NewGuid();
            var g2 = Guid.NewGuid();

            var a = new List<Guid> { g1, g2 };
            var b = new List<Guid> { g1, g2 };

            Assert.That(GuidSequenceEquality.OrderedEqual(a, b), Is.True);
        }

        [Test]
        public void Verify_that_ordered_equal_returns_false_for_sequences_with_same_elements_in_different_order()
        {
            var g1 = Guid.NewGuid();
            var g2 = Guid.NewGuid();

            var a = new List<Guid> { g1, g2 };
            var b = new List<Guid> { g2, g1 };

            Assert.That(GuidSequenceEquality.OrderedEqual(a, b), Is.False);
        }

        [Test]
        public void Verify_that_unordered_equal_returns_true_for_sequences_with_same_elements_in_different_order()
        {
            var g1 = Guid.NewGuid();
            var g2 = Guid.NewGuid();

            var a = new List<Guid> { g1, g2 };
            var b = new List<Guid> { g2, g1 };

            Assert.That(GuidSequenceEquality.UnorderedEqual(a, b), Is.True);
        }

        [Test]
        public void Verify_that_ordered_equal_returns_false_for_sequences_with_different_lengths()
        {
            var g1 = Guid.NewGuid();

            var a = new List<Guid> { g1 };
            var b = new List<Guid> { g1, Guid.NewGuid() };

            Assert.That(GuidSequenceEquality.OrderedEqual(a, b), Is.False);
        }

        [Test]
        public void Verify_that_unordered_equal_returns_false_for_sequences_with_different_lengths()
        {
            var g1 = Guid.NewGuid();

            var a = new List<Guid> { g1 };
            var b = new List<Guid> { g1, Guid.NewGuid() };

            Assert.That(GuidSequenceEquality.UnorderedEqual(a, b), Is.False);
        }

        [Test]
        public void Verify_that_unordered_equal_correctly_handles_duplicate_guids()
        {
            var g1 = Guid.NewGuid();
            var g2 = Guid.NewGuid();

            var a = new List<Guid> { g1, g1, g2 };
            var b = new List<Guid> { g2, g1, g1 };

            Assert.That(GuidSequenceEquality.UnorderedEqual(a, b), Is.True);
        }

        [Test]
        public void Verify_that_unordered_equal_returns_false_when_duplicate_multiplicity_differs()
        {
            var g1 = Guid.NewGuid();

            var a = new List<Guid> { g1, g1 };
            var b = new List<Guid> { g1 };

            Assert.That(GuidSequenceEquality.UnorderedEqual(a, b), Is.False);
        }

        [Test]
        public void Verify_that_ordered_equal_returns_false_when_any_element_differs()
        {
            var a = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            var b = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            Assert.That(GuidSequenceEquality.OrderedEqual(a, b), Is.False);
        }

        [Test]
        public void Verify_that_unordered_equal_returns_false_when_any_element_differs()
        {
            var a = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            var b = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            Assert.That(GuidSequenceEquality.UnorderedEqual(a, b), Is.False);
        }

        [Test]
        public void Verify_that_empty_sequences_are_considered_equal_for_ordered_comparison()
        {
            var a = new List<Guid>();
            var b = new List<Guid>();

            Assert.That(GuidSequenceEquality.OrderedEqual(a, b), Is.True);
        }

        [Test]
        public void Verify_that_empty_sequences_are_considered_equal_for_unordered_comparison()
        {
            var a = new List<Guid>();
            var b = new List<Guid>();

            Assert.That(GuidSequenceEquality.UnorderedEqual(a, b), Is.True);
        }
    }
}
