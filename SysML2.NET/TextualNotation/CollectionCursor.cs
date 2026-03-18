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

namespace SysML2.NET.TextualNotation
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
        public T Current => this.GetCurrent(this.index);

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
            return amount < 0 ? throw new ArgumentException("Not able to get previous element in the collection") : this.GetCurrent(this.index + amount);
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

            this.index = Math.Min(this.index + amount, this.elements.Count);
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
