// -------------------------------------------------------------------------------------------------
// <copyright file="CollectionCursorTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.TextualNotation.Tests.Writers
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Serializer.TextualNotation.Writers;

    /// <summary>
    /// Test fixture for <see cref="CollectionCursor{T}"/>'s forward-progress surface —
    /// <see cref="CollectionCursor{T}.Position"/> and
    /// <see cref="CollectionCursor{T}.AssertAdvancedSince"/> — which every generated <c>*</c> loop relies
    /// on to turn a non-terminating iteration into an immediate failure.
    /// </summary>
    [TestFixture]
    public class CollectionCursorTestFixture
    {
        /// <summary>
        /// <see cref="CollectionCursor{T}.Position"/> reports the offset and tracks
        /// <see cref="CollectionCursor{T}.Move"/>, saturating at the end of the collection rather than
        /// running past it.
        /// </summary>
        [Test]
        public void VerifyPosition()
        {
            var cursor = new CollectionCursor<string>(["alpha", "beta"]);

            Assert.That(cursor.Position, Is.EqualTo(0));

            cursor.Move();

            Assert.That(cursor.Position, Is.EqualTo(1));

            cursor.Move();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(cursor.Position, Is.EqualTo(2));
                Assert.That(cursor.Current, Is.Null);
            }

            // Move past the end saturates, so the position stays a valid comparison anchor.
            cursor.Move();

            Assert.That(cursor.Position, Is.EqualTo(2));
        }

        /// <summary>
        /// <see cref="CollectionCursor{T}.AssertAdvancedSince"/> passes when the cursor consumed something
        /// and throws when it did not — the case that would otherwise spin the enclosing <c>*</c> loop
        /// forever.
        /// </summary>
        [Test]
        public void VerifyAssertAdvancedSince()
        {
            var cursor = new CollectionCursor<string>(["alpha", "beta"]);

            // An iteration that consumed one element is forward progress.
            var positionBeforeConsumingIteration = cursor.Position;
            cursor.Move();

            Assert.That(() => cursor.AssertAdvancedSince(positionBeforeConsumingIteration, "DefinitionBodyItem"), Throws.Nothing);

            // An iteration that consumed nothing cannot terminate the loop, so it must fail loudly.
            var positionBeforeStalledIteration = cursor.Position;

            var stalledIteration = Assert.Throws<InvalidOperationException>(
                () => cursor.AssertAdvancedSince(positionBeforeStalledIteration, "DefinitionBodyItem"));

            using (Assert.EnterMultipleScope())
            {
                // The rule name is the only handle a caller has on WHICH loop stalled, so it must be quoted.
                Assert.That(stalledIteration.Message, Does.Contain("DefinitionBodyItem"));

                // The element under the cursor is what the loop admitted and the builder declined — naming
                // it is what makes the mismatch diagnosable without a debugger.
                Assert.That(stalledIteration.Message, Does.Contain(nameof(String)));
            }

            // An exhausted cursor stalls too: the loop condition, not this assertion, is what ends iteration.
            cursor.Move();
            var positionAtEnd = cursor.Position;

            Assert.That(() => cursor.AssertAdvancedSince(positionAtEnd, "CaseBodyItem"), Throws.TypeOf<InvalidOperationException>());
        }

        /// <summary>
        /// <see cref="CollectionCursor{T}.TryTake{TDerived}"/> locates an element by role: a match at the
        /// cursor advances it like <see cref="CollectionCursor{T}.Move"/>, a match ahead is consumed out of
        /// order and skipped by all later traversal, and the cursor otherwise stays put.
        /// </summary>
        [Test]
        public void VerifyTryTake()
        {
            var emptyCursor = new CollectionCursor<string>([]);

            Assert.That(() => emptyCursor.TryTake<string>(null, out _), Throws.TypeOf<ArgumentNullException>());
            Assert.That(emptyCursor.TryTake<string>(element => true, out _), Is.False);

            var cursor = new CollectionCursor<string>(["alpha", "beta", "gamma", "delta"]);

            // No match leaves the cursor untouched and yields no value.
            var missed = cursor.TryTake<string>(element => element == "omega", out var missedValue);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(missed, Is.False);
                Assert.That(missedValue, Is.Null);
                Assert.That(cursor.Position, Is.EqualTo(0));
            }

            // A match AT the cursor consumes it exactly like Move(1) — real forward progress.
            var positionBeforeTakeAtCursor = cursor.Position;
            var takenAtCursor = cursor.TryTake<string>(element => element == "alpha", out var valueAtCursor);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(takenAtCursor, Is.True);
                Assert.That(valueAtCursor, Is.EqualTo("alpha"));
                Assert.That(cursor.Position, Is.EqualTo(1));
                Assert.That(cursor.Current, Is.EqualTo("beta"));
            }

            Assert.That(() => cursor.AssertAdvancedSince(positionBeforeTakeAtCursor, "TransitionUsage"), Throws.Nothing);

            // A match AHEAD of the cursor is taken without advancing it; the consumed slot disappears
            // from Current / GetNext / Move traversal.
            var takenAhead = cursor.TryTake<string>(element => element == "gamma", out var valueAhead);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(takenAhead, Is.True);
                Assert.That(valueAhead, Is.EqualTo("gamma"));
                Assert.That(cursor.Position, Is.EqualTo(1));
                Assert.That(cursor.Current, Is.EqualTo("beta"));
                Assert.That(cursor.GetNext(1), Is.EqualTo("delta"));
            }

            cursor.Move();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(cursor.Current, Is.EqualTo("delta"));
                Assert.That(cursor.Position, Is.EqualTo(3));
            }

            // A taken element can never be taken again.
            Assert.That(cursor.TryTake<string>(element => element == "gamma", out _), Is.False);

            // The type test participates in the match: a TDerived mismatch is not taken.
            var mixedCursor = new CollectionCursor<object>(["epsilon", new object()]);
            mixedCursor.Move();

            Assert.That(mixedCursor.TryTake<string>(element => true, out _), Is.False);
        }

        /// <summary>
        /// <see cref="CollectionCursor{T}.Contains{TDerived}"/> answers whether an unconsumed element
        /// matching the predicate exists at or ahead of the cursor, without consuming anything — the
        /// guard-condition companion of <see cref="CollectionCursor{T}.TryTake{TDerived}"/>.
        /// </summary>
        [Test]
        public void VerifyContains()
        {
            var emptyCursor = new CollectionCursor<string>([]);

            Assert.That(() => emptyCursor.Contains<string>(null), Throws.TypeOf<ArgumentNullException>());
            Assert.That(emptyCursor.Contains<string>(element => true), Is.False);

            var cursor = new CollectionCursor<string>(["alpha", "beta", "gamma"]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(cursor.Contains<string>(element => element == "alpha"), Is.True);
                Assert.That(cursor.Contains<string>(element => element == "gamma"), Is.True);
                Assert.That(cursor.Contains<string>(element => element == "omega"), Is.False);

                // Non-consuming: the probe left the cursor exactly where it was.
                Assert.That(cursor.Position, Is.EqualTo(0));
                Assert.That(cursor.Current, Is.EqualTo("alpha"));
            }

            // Elements behind the cursor are out of reach.
            cursor.Move();

            Assert.That(cursor.Contains<string>(element => element == "alpha"), Is.False);

            // Elements consumed out of order by TryTake are out of reach too.
            cursor.TryTake<string>(element => element == "gamma", out _);

            Assert.That(cursor.Contains<string>(element => element == "gamma"), Is.False);

            // The type test participates in the match: a TDerived mismatch does not count.
            var mixedCursor = new CollectionCursor<object>([new object()]);

            Assert.That(mixedCursor.Contains<string>(element => true), Is.False);
        }
    }
}
