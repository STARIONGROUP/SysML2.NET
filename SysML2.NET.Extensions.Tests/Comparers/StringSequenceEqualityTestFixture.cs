// -------------------------------------------------------------------------------------------------
// <copyright file="StringSequenceEqualityTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.Extensions.Tests.Comparers
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    using SysML2.NET.Extensions.Comparers;


    [TestFixture]
    public class StringSequenceEqualityTestFixture
    {
        [Test]
        public void Verify_that_ordered_equal_returns_true_for_the_same_reference()
        {
            var list = new List<string> { "a", "b" };

            Assert.That(StringSequenceEquality.OrderedEqual(list, list), Is.True);
        }

        [Test]
        public void Verify_that_unordered_equal_returns_true_for_the_same_reference()
        {
            var list = new List<string> { "a", "b" };

            Assert.That(StringSequenceEquality.UnorderedEqual(list, list), Is.True);
        }

        [Test]
        public void Verify_that_ordered_equal_returns_false_when_one_sequence_is_null()
        {
            var list = new List<string> { "a" };

            Assert.That(StringSequenceEquality.OrderedEqual(list, null), Is.False);
            Assert.That(StringSequenceEquality.OrderedEqual(null, list), Is.False);
        }

        [Test]
        public void Verify_that_unordered_equal_returns_false_when_one_sequence_is_null()
        {
            var list = new List<string> { "a" };

            Assert.That(StringSequenceEquality.UnorderedEqual(list, null), Is.False);
            Assert.That(StringSequenceEquality.UnorderedEqual(null, list), Is.False);
        }

        [Test]
        public void Verify_that_ordered_equal_returns_true_for_sequences_with_same_elements_in_same_order_using_default_ordinal_comparer()
        {
            var a = new List<string> { "Alpha", "Beta" };
            var b = new List<string> { "Alpha", "Beta" };

            Assert.That(StringSequenceEquality.OrderedEqual(a, b), Is.True);
        }

        [Test]
        public void Verify_that_ordered_equal_returns_false_for_sequences_with_same_elements_in_different_order()
        {
            var a = new List<string> { "Alpha", "Beta" };
            var b = new List<string> { "Beta", "Alpha" };

            Assert.That(StringSequenceEquality.OrderedEqual(a, b), Is.False);
        }

        [Test]
        public void Verify_that_unordered_equal_returns_true_for_sequences_with_same_elements_in_different_order()
        {
            var a = new List<string> { "Alpha", "Beta" };
            var b = new List<string> { "Beta", "Alpha" };

            Assert.That(StringSequenceEquality.UnorderedEqual(a, b), Is.True);
        }

        [Test]
        public void Verify_that_ordered_equal_returns_false_for_sequences_with_different_lengths()
        {
            var a = new List<string> { "Alpha" };
            var b = new List<string> { "Alpha", "Beta" };

            Assert.That(StringSequenceEquality.OrderedEqual(a, b), Is.False);
        }

        [Test]
        public void Verify_that_unordered_equal_returns_false_for_sequences_with_different_lengths()
        {
            var a = new List<string> { "Alpha" };
            var b = new List<string> { "Alpha", "Beta" };

            Assert.That(StringSequenceEquality.UnorderedEqual(a, b), Is.False);
        }

        [Test]
        public void Verify_that_unordered_equal_correctly_handles_duplicate_values()
        {
            var a = new List<string> { "Alpha", "Alpha", "Beta" };
            var b = new List<string> { "Beta", "Alpha", "Alpha" };

            Assert.That(StringSequenceEquality.UnorderedEqual(a, b), Is.True);
        }

        [Test]
        public void Verify_that_unordered_equal_returns_false_when_duplicate_multiplicity_differs()
        {
            var a = new List<string> { "Alpha", "Alpha" };
            var b = new List<string> { "Alpha", "Beta" };

            Assert.That(StringSequenceEquality.UnorderedEqual(a, b), Is.False);
        }

        [Test]
        public void Verify_that_ordered_equal_returns_false_when_any_element_differs()
        {
            var a = new List<string> { "Alpha", "Beta" };
            var b = new List<string> { "Alpha", "Gamma" };

            Assert.That(StringSequenceEquality.OrderedEqual(a, b), Is.False);
        }

        [Test]
        public void Verify_that_unordered_equal_returns_false_when_any_element_differs()
        {
            var a = new List<string> { "Alpha", "Beta" };
            var b = new List<string> { "Alpha", "Gamma" };

            Assert.That(StringSequenceEquality.UnorderedEqual(a, b), Is.False);
        }

        [Test]
        public void Verify_that_empty_sequences_are_considered_equal_for_ordered_comparison()
        {
            var a = new List<string>();
            var b = new List<string>();

            Assert.That(StringSequenceEquality.OrderedEqual(a, b), Is.True);
        }

        [Test]
        public void Verify_that_empty_sequences_are_considered_equal_for_unordered_comparison()
        {
            var a = new List<string>();
            var b = new List<string>();

            Assert.That(StringSequenceEquality.UnorderedEqual(a, b), Is.True);
        }

        [Test]
        public void Verify_that_default_ordinal_comparison_is_case_sensitive()
        {
            var a = new List<string> { "alpha" };
            var b = new List<string> { "ALPHA" };

            Assert.That(StringSequenceEquality.OrderedEqual(a, b), Is.False);
            Assert.That(StringSequenceEquality.UnorderedEqual(a, b), Is.False);
        }

        [Test]
        public void Verify_that_a_custom_comparer_can_enable_case_insensitive_ordered_equality()
        {
            var a = new List<string> { "alpha", "BETA" };
            var b = new List<string> { "ALPHA", "beta" };

            Assert.That(StringSequenceEquality.OrderedEqual(a, b, StringComparer.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void Verify_that_a_custom_comparer_can_enable_case_insensitive_unordered_equality()
        {
            var a = new List<string> { "alpha", "alpha", "BETA" };
            var b = new List<string> { "beta", "ALPHA", "ALPHA" };

            Assert.That(StringSequenceEquality.UnorderedEqual(a, b, StringComparer.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void Verify_that_unordered_equal_returns_false_when_case_insensitive_comparer_is_not_used_and_casing_differs()
        {
            var a = new List<string> { "alpha", "alpha", "BETA" };
            var b = new List<string> { "beta", "ALPHA", "ALPHA" };

            Assert.That(StringSequenceEquality.UnorderedEqual(a, b), Is.False);
        }
    }
}
