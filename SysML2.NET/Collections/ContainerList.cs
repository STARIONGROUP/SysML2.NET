// -------------------------------------------------------------------------------------------------
// <copyright file="ContainerList.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Collections
{
    using System;
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

        /// <summary>Inserts an element into the <see cref="T:System.Collections.ObjectModel.Collection`1" /> at the specified index.</summary>
        /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
        /// <param name="item">The object to insert. The value can be <see langword="null" /> for reference types.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///         <paramref name="index" /> is less than zero.
        /// -or-
        /// <paramref name="index" /> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
        protected override void InsertItem(int index, TChild item)
        {
            if (object.Equals(item, default(TChild)))
            {
                throw new ArgumentNullException(nameof(item));
            }

            var currentParent = this.parentGetter(item);

            if (!object.Equals(currentParent, default(TParent)) && currentParent.Id != this.owner.Id)
            {
                throw new InvalidOperationException($"The current element with Id {item.Id} is already owned");
            }

            base.InsertItem(index, item);
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
        }
    }
}
