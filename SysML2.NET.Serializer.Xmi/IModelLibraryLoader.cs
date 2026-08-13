// -------------------------------------------------------------------------------------------------
// <copyright file="IModelLibraryLoader.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Xmi
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// Loads a complete set of model libraries from disk, independently of any user model.
    /// </summary>
    /// <remarks>
    /// Deserializing a user model yields only the libraries that model transitively references. The KerML
    /// 8.4.2 semantic constraints need the whole library set regardless — every Class must specialize
    /// <c>Occurrences::Occurrence</c> and every Feature <c>Base::things</c>, whether or not the model
    /// mentions them — so a model-independent load is required to resolve them.
    /// </remarks>
    public interface IModelLibraryLoader
    {
        /// <summary>
        /// Loads every model library found beneath a directory.
        /// </summary>
        /// <param name="libraryDirectory">The root directory to search recursively.</param>
        /// <returns>The distinct root Namespaces of the loaded libraries.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="libraryDirectory" /> is null.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">Thrown when the directory does not exist.</exception>
        IReadOnlyCollection<INamespace> Load(string libraryDirectory);

        /// <summary>
        /// Asynchronously loads every model library found beneath a directory.
        /// </summary>
        /// <param name="libraryDirectory">The root directory to search recursively.</param>
        /// <param name="cancellationToken">The token used to cancel the load.</param>
        /// <returns>The distinct root Namespaces of the loaded libraries.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="libraryDirectory" /> is null.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">Thrown when the directory does not exist.</exception>
        Task<IReadOnlyCollection<INamespace>> LoadAsync(string libraryDirectory, CancellationToken cancellationToken = default);
    }
}
