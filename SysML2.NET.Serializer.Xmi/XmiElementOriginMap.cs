// -------------------------------------------------------------------------------------------------
// <copyright file="XmiElementOriginMap.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Xmi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Tracks the association between elements and the XMI source files they were deserialized from.
    /// </summary>
    public class XmiElementOriginMap : IXmiElementOriginMap
    {
        /// <summary>
        /// Maps element identifiers to their source file URIs
        /// </summary>
        private readonly Dictionary<Guid, Uri> elementToFile = new Dictionary<Guid, Uri>();

        /// <summary>
        /// Maps source file URIs to their root namespace identifiers
        /// </summary>
        private readonly Dictionary<Uri, Guid> fileToRootNamespace = new Dictionary<Uri, Guid>();

        /// <summary>
        /// Registers the source file for a given element
        /// </summary>
        /// <param name="elementId">The <see cref="Guid"/> identifier of the element</param>
        /// <param name="sourceFile">The <see cref="Uri"/> of the source XMI file</param>
        public void Register(Guid elementId, Uri sourceFile)
        {
            this.elementToFile[elementId] = sourceFile;
        }

        /// <summary>
        /// Gets the source file for a given element
        /// </summary>
        /// <param name="elementId">The <see cref="Guid"/> identifier of the element</param>
        /// <returns>The <see cref="Uri"/> of the source file, or null if not tracked</returns>
        public Uri GetSourceFile(Guid elementId)
        {
            return this.elementToFile.TryGetValue(elementId, out var sourceFile) ? sourceFile : null;
        }

        /// <summary>
        /// Gets all element identifiers that were deserialized from the given source file
        /// </summary>
        /// <param name="sourceFile">The <see cref="Uri"/> of the source XMI file</param>
        /// <returns>An <see cref="IEnumerable{Guid}"/> of element identifiers</returns>
        public IEnumerable<Guid> GetElementsInFile(Uri sourceFile)
        {
            return this.elementToFile.Where(kvp => kvp.Value == sourceFile).Select(kvp => kvp.Key);
        }

        /// <summary>
        /// Gets all source files that have been registered
        /// </summary>
        /// <returns>An <see cref="IEnumerable{Uri}"/> of source file URIs</returns>
        public IEnumerable<Uri> GetAllSourceFiles()
        {
            return this.elementToFile.Values.Distinct();
        }

        /// <summary>
        /// Registers the root namespace identifier for a given source file
        /// </summary>
        /// <param name="sourceFile">The <see cref="Uri"/> of the source XMI file</param>
        /// <param name="rootNamespaceId">The <see cref="Guid"/> of the root namespace element</param>
        public void RegisterRootNamespace(Uri sourceFile, Guid rootNamespaceId)
        {
            this.fileToRootNamespace[sourceFile] = rootNamespaceId;
        }

        /// <summary>
        /// Gets the root namespace identifier for a given source file
        /// </summary>
        /// <param name="sourceFile">The <see cref="Uri"/> of the source XMI file</param>
        /// <returns>The <see cref="Guid"/> of the root namespace, or <see cref="Guid.Empty"/> if not registered</returns>
        public Guid GetRootNamespaceId(Uri sourceFile)
        {
            return this.fileToRootNamespace.TryGetValue(sourceFile, out var rootNamespaceId) ? rootNamespaceId : Guid.Empty;
        }
    }
}
