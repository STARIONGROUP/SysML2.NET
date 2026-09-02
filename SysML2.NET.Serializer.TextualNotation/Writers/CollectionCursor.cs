// -------------------------------------------------------------------------------------------------
// <copyright file="CollectionCursor.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.TextualNotation.Writers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a cursor over a read-only collection that allows sequential,
    /// forward-only traversal of elements. This class is primarily used to
    /// simulate grammar-driven consumption of elements.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    public class CollectionCursor<T> where T : class
    {
        /// <summary>
        /// Gets the collection that is currently traversed.
        /// </summary>
        private readonly IReadOnlyList<T> elements;

        /// <summary>
        /// Gets the indices of elements consumed ahead of the cursor by <see cref="TryTake{TDerived}" />;
        /// every traversal member skips them so a taken element is never visited twice.
        /// </summary>
        private readonly HashSet<int> consumedOutOfOrder = [];

        /// <summary>
        /// Gets the value of the current index.
        /// </summary>
        private int index;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionCursor{T}"/> class.
        /// </summary>
        /// <param name="elements">The collection to iterate over.</param>
        public CollectionCursor(IReadOnlyList<T> elements)
        {
            this.elements = elements ?? throw new ArgumentNullException(nameof(elements));
        }

        /// <summary>
        /// Gets the current <typeparamref name="T"/> element at the cursor position.
        /// <remarks>Returns <c>default</c> if the cursor is out of range.</remarks>
        /// </summary>
        public T Current => this.GetCurrent(this.SkipConsumed(this.index));

        /// <summary>
        /// Gets the cursor's current offset into the collection, skipping over elements already consumed
        /// out of order by <see cref="TryTake{TDerived}" />. Exposed so a caller iterating this cursor
        /// can prove its loop body made forward progress; see <see cref="AssertAdvancedSince" />.
        /// </summary>
        public int Position => this.SkipConsumed(this.index);

        /// <summary>
        /// Throws when the cursor has not moved past <paramref name="positionBeforeIteration"/>, i.e. the
        /// loop body just ran without consuming anything.
        /// </summary>
        /// <param name="positionBeforeIteration">The <see cref="Position"/> captured before the loop body ran.</param>
        /// <param name="ruleName">The KEBNF rule the loop body was building, named in the exception.</param>
        /// <exception cref="InvalidOperationException">When the cursor did not advance.</exception>
        /// <remarks>
        /// A grammar <c>*</c> loop tests the SAME cursor it consumes from, so an iteration that consumes
        /// nothing leaves every input to the next test unchanged — the loop cannot terminate. Detecting it
        /// here converts an unrecoverable hang into an immediate, attributable failure.
        /// <para>This is a real failure mode rather than a defensive flourish: the item dispatchers
        /// deliberately break out of their <c>default:</c> arm WITHOUT advancing, so that an element
        /// belonging to a parent rule survives for that rule to consume
        /// (<see cref="SharedTextualNotationBuilder"/>). That is correct only while the enclosing loop's
        /// condition excludes exactly those elements. When the two drift apart the writer hangs, and a hang
        /// is invisible to a test that compares output — which is precisely how one such drift reached a
        /// full green run.</para>
        /// </remarks>
        public void AssertAdvancedSince(int positionBeforeIteration, string ruleName)
        {
            if (this.Position != positionBeforeIteration)
            {
                return;
            }

            var currentDescription = this.Current?.GetType().Name ?? "<end of collection>";

            throw new InvalidOperationException(
                $"The textual notation writer made no progress building '{ruleName}': the loop body consumed nothing at position {positionBeforeIteration} (current element: {currentDescription}). "
                + $"The loop's condition admits an element that Build{ruleName} declines to consume — the two must agree, otherwise the loop cannot terminate.");
        }

        /// <summary>
        /// Gets the element at a specific index without modifying the cursor position.
        /// </summary>
        /// <param name="indexToUse">The index to read from.</param>
        /// <returns>
        /// The element at the given index, or <c>default</c> if the index is out of bounds.
        /// </returns>
        private T GetCurrent(int indexToUse)
        {
            return indexToUse >= this.elements.Count || indexToUse < 0 ? default : this.elements[indexToUse];
        }

        /// <summary>
        /// Peeks ahead in the collection without advancing the cursor.
        /// </summary>
        /// <param name="amount">
        /// The number of positions to look ahead. Must be greater than or equal to zero.
        /// </param>
        /// <returns>
        /// The element located at the current index plus the specified offset,
        /// or <c>default</c> if the resulting index is out of bounds.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="amount"/> is negative.
        /// </exception>
        public T GetNext(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Not able to get previous element in the collection");
            }

            var probeIndex = this.SkipConsumed(this.index);

            for (var stepIndex = 0; stepIndex < amount; stepIndex++)
            {
                probeIndex = this.SkipConsumed(probeIndex + 1);
            }

            return this.GetCurrent(probeIndex);
        }

        /// <summary>
        /// Determines whether an element the cursor has not yet consumed — at the cursor position or
        /// anywhere ahead of it — is a <typeparamref name="TDerived"/> satisfying
        /// <paramref name="predicate"/>, without consuming anything. This is the non-consuming companion
        /// of <see cref="TryTake{TDerived}" /> for use in guard conditions; both MUST be called with the
        /// identical predicate so the condition and the consumption agree on the element they select.
        /// </summary>
        /// <param name="predicate">The role discriminator the sought element must satisfy.</param>
        /// <typeparam name="TDerived">Any class inheriting from <typeparamref name="T"/>.</typeparam>
        /// <returns><c>true</c> when a matching unconsumed element exists; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="predicate"/> is <c>null</c>.</exception>
        public bool Contains<TDerived>(Func<TDerived, bool> predicate) where TDerived : class, T
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            for (var probeIndex = this.SkipConsumed(this.index); probeIndex < this.elements.Count; probeIndex = this.SkipConsumed(probeIndex + 1))
            {
                if (this.elements[probeIndex] is TDerived candidate && predicate(candidate))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Takes the first unconsumed element — at the cursor position or anywhere ahead of it — that is a
        /// <typeparamref name="TDerived"/> satisfying <paramref name="predicate"/>, locating the element by
        /// its ROLE rather than by its position. A match at the cursor advances the cursor exactly like
        /// <see cref="Move" />; a match ahead of it is recorded as consumed out of order and skipped by all
        /// later traversal, while the cursor itself stays put.
        /// </summary>
        /// <param name="predicate">The role discriminator the sought element must satisfy.</param>
        /// <param name="value">The taken element, or <c>default</c> when nothing matched.</param>
        /// <typeparam name="TDerived">Any class inheriting from <typeparamref name="T"/>.</typeparam>
        /// <returns><c>true</c> when a matching element was taken; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="predicate"/> is <c>null</c>.</exception>
        public bool TryTake<TDerived>(Func<TDerived, bool> predicate, out TDerived value) where TDerived : class, T
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            var startIndex = this.SkipConsumed(this.index);

            for (var probeIndex = startIndex; probeIndex < this.elements.Count; probeIndex = this.SkipConsumed(probeIndex + 1))
            {
                if (this.elements[probeIndex] is TDerived candidate && predicate(candidate))
                {
                    if (probeIndex == startIndex)
                    {
                        this.Move();
                    }
                    else
                    {
                        this.consumedOutOfOrder.Add(probeIndex);
                    }

                    value = candidate;
                    return true;
                }
            }

            value = default;
            return false;
        }

        /// <summary>
        /// Advances the cursor to the next element in the collection.
        /// </summary>
        /// <param name="amount">The amount to move-on the cursor position.</param>
        /// <returns>
        /// <c>true</c> if the cursor successfully moved to the next element;
        /// <c>false</c> if the end of the collection has been reached.
        /// </returns>
        public void Move(int amount = 1)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            for (var stepIndex = 0; stepIndex < amount; stepIndex++)
            {
                this.index = Math.Min(this.SkipConsumed(this.index) + 1, this.elements.Count);
            }
        }

        /// <summary>
        /// Returns the first index at or after <paramref name="from"/> that has not been consumed out of
        /// order by <see cref="TryTake{TDerived}" />. Identity while nothing was taken out of order, so
        /// every traversal member behaves exactly as it did before the primitive existed.
        /// </summary>
        /// <param name="from">The index to start probing at.</param>
        /// <returns>The first unconsumed index at or after <paramref name="from"/>.</returns>
        private int SkipConsumed(int from)
        {
            var unconsumedIndex = from;

            while (this.consumedOutOfOrder.Contains(unconsumedIndex))
            {
                unconsumedIndex++;
            }

            return unconsumedIndex;
        }
        
        /// <summary>
        /// Tries to get the current element as <typeparamref name="TDerived"/>.
        /// </summary>
        /// <param name="value">The retrieved <typeparamref name="TDerived"/> element, if applicable</param>
        /// <typeparam name="TDerived">Any class inheriting from <typeparamref name="T"/></typeparam>
        /// <returns>
        /// <c>true</c> if the <see cref="Current"/> could be assigned to <typeparamref name="T"/>
        /// <c>false</c> otherwise</returns>
        public bool TryGetCurrent<TDerived>(out TDerived value) where TDerived : class, T
        {
            value = this.Current as TDerived;
            return value != null;
        }
    }
}
