// ------------------------------------------------------------------------------------------------
// <copyright file="ContainerList.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="ContainerList{TParent,TChild}"/> provides containment support to automatically update the parent property when a <typeparamref name="TChild"/>
    /// is added
    /// </summary>
    /// <typeparam name="TParent">Any parent class</typeparam>
    /// <typeparam name="TChild">Any child class</typeparam>
    internal class ContainerList<TParent, TChild>: Collection<TChild> where TParent : IElement where TChild : IElement
    {
        /// <summary>
        /// The <typeparamref name="TParent"/> instance that will hold <typeparamref name="TChild"/>
        /// </summary>
        private readonly TParent owner;
        
        /// <summary>
        /// Provides access, from a <typeparamref name="TChild"/>, to his parent property setter
        /// </summary>
        private readonly Action<TChild, TParent> parentSetter;
        
        /// <summary>
        /// Provides access, from a <typeparamref name="TChild"/>, to his parent property getter
        /// </summary>
        private readonly Func<TChild, TParent> parentGetter;

        /// <summary>
        /// The items of this list, kept alongside it so that membership is tested in constant time
        /// </summary>
        private readonly HashSet<TChild> members = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerList{TParent,TChild}"/>
        /// </summary>
        /// <param name="owner">The <typeparamref name="TParent"/> instance that will hold <typeparamref name="TChild"/></param>
        /// <param name="parentSetter">Provides access, from a <typeparamref name="TChild"/>, to his parent property setter</param>
        /// <param name="parentGetter">Provides access, from a <typeparamref name="TChild"/>, to his parent property getter</param>
        public ContainerList(TParent owner, Action<TChild, TParent> parentSetter, Func<TChild, TParent> parentGetter)
        {
            this.owner = owner;
            this.parentSetter = parentSetter;
            this.parentGetter = parentGetter;
        }

        /// <summary>
        /// Removes every item and clears the owner of each of them.
        /// </summary>
        protected override void ClearItems()
        {
            foreach (var item in this.Items)
            {
                this.parentSetter(item, default);
            }

            base.ClearItems();
            this.members.Clear();
        }

        /// <summary>
        /// Determines whether the <paramref name="item"/> is in this list, in constant time.
        /// </summary>
        /// <param name="item">The item to look for.</param>
        /// <returns><c>true</c> if the <paramref name="item"/> is in this list; otherwise <c>false</c></returns>
        public new bool Contains(TChild item) => this.members.Contains(item);

        /// <summary>
        /// Returns the zero-based position of the <paramref name="item"/>, answering in constant time when it is not in this list.
        /// </summary>
        /// <param name="item">The item to look for.</param>
        /// <returns>The position of the <paramref name="item"/>, or -1 when it is not in this list</returns>
        public new int IndexOf(TChild item) => this.Contains(item) ? base.IndexOf(item) : -1;

        /// <summary>Inserts an element into the <see cref="T:System.Collections.ObjectModel.Collection`1" /> at the specified index.</summary>
        /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
        /// <param name="item">The object to insert. The value can be <see langword="null" /> for reference types.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///         <paramref name="index" /> is less than zero.
        /// -or-
        /// <paramref name="index" /> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="item"/> is null</exception>
        /// <exception cref="InvalidOperationException">If <paramref name="item"/> is already in this list or is owned by another parent</exception>
        protected override void InsertItem(int index, TChild item)
        {
            this.AssertCanBeAdded(item);

            base.InsertItem(index, item);
            this.members.Add(item);
            this.parentSetter(item, this.owner);
        }

        /// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///         <paramref name="index" /> is less than zero.
        /// -or-
        /// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
        protected override void RemoveItem(int index)
        {
            var currentItem = this[index];
            this.parentSetter(currentItem, default);
            base.RemoveItem(index);
            this.members.Remove(currentItem);
        }

        /// <summary>
        /// Replaces the item at <paramref name="index"/>, clearing the owner of the replaced item.
        /// </summary>
        /// <param name="index">The zero-based index of the item to replace.</param>
        /// <param name="item">The replacing item.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="item"/> is null</exception>
        /// <exception cref="InvalidOperationException">If <paramref name="item"/> is already in this list or is owned by another parent</exception>
        protected override void SetItem(int index, TChild item)
        {
            var replacedItem = this[index];

            if (object.Equals(replacedItem, item))
            {
                return;
            }

            this.AssertCanBeAdded(item);

            this.parentSetter(replacedItem, default);
            base.SetItem(index, item);
            this.members.Remove(replacedItem);
            this.members.Add(item);
            this.parentSetter(item, this.owner);
        }

        /// <summary>
        /// Moves the item at <paramref name="oldIndex"/> to <paramref name="newIndex"/> without changing its owner.
        /// </summary>
        /// <param name="oldIndex">The zero-based index of the item to move.</param>
        /// <param name="newIndex">The zero-based index the item occupies after the move.</param>
        /// <exception cref="ArgumentOutOfRangeException">If either index is outside the bounds of the list</exception>
        internal void Move(int oldIndex, int newIndex)
        {
            if (oldIndex < 0 || oldIndex >= this.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(oldIndex));
            }

            if (newIndex < 0 || newIndex >= this.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(newIndex));
            }

            var item = this.Items[oldIndex];
            this.Items.RemoveAt(oldIndex);
            this.Items.Insert(newIndex, item);
        }

        /// <summary>
        /// Asserts that <paramref name="item"/> can join this list: non-null, not already listed, and not owned by another parent.
        /// </summary>
        /// <param name="item">The candidate item.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="item"/> is null</exception>
        /// <exception cref="InvalidOperationException">If <paramref name="item"/> is already in this list or is owned by another parent</exception>
        private void AssertCanBeAdded(TChild item)
        {
            if (object.Equals(item, default(TChild)))
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (this.members.Contains(item))
            {
                throw new InvalidOperationException($"The element with Id {item.Id} is already contained in this list");
            }

            var currentParent = this.parentGetter(item);

            if (!object.Equals(currentParent, default(TParent)) && !ReferenceEquals(currentParent, this.owner))
            {
                throw new InvalidOperationException($"The current element with Id {item.Id} is already owned");
            }
        }
    }
}
