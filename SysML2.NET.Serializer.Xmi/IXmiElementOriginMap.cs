// ------------------------------------------------------------------------------------------------
// <copyright file="IXmiElementOriginMap.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Xmi
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Tracks the association between elements and the XMI source files they were deserialized from.
    /// This enables faithful round-trip serialization back to multiple XMI files with correct href references.
    /// </summary>
    public interface IXmiElementOriginMap
    {
        /// <summary>
        /// Registers the source file for a given element
        /// </summary>
        /// <param name="elementId">The <see cref="Guid"/> identifier of the element</param>
        /// <param name="sourceFile">The <see cref="Uri"/> of the source XMI file</param>
        void Register(Guid elementId, Uri sourceFile);

        /// <summary>
        /// Gets the source file for a given element
        /// </summary>
        /// <param name="elementId">The <see cref="Guid"/> identifier of the element</param>
        /// <returns>The <see cref="Uri"/> of the source file, or null if not tracked</returns>
        Uri GetSourceFile(Guid elementId);

        /// <summary>
        /// Gets all element identifiers that were deserialized from the given source file
        /// </summary>
        /// <param name="sourceFile">The <see cref="Uri"/> of the source XMI file</param>
        /// <returns>An <see cref="IEnumerable{Guid}"/> of element identifiers</returns>
        IEnumerable<Guid> GetElementsInFile(Uri sourceFile);

        /// <summary>
        /// Gets all source files that have been registered
        /// </summary>
        /// <returns>An <see cref="IEnumerable{Uri}"/> of source file URIs</returns>
        IEnumerable<Uri> GetAllSourceFiles();

        /// <summary>
        /// Registers the root namespace identifier for a given source file
        /// </summary>
        /// <param name="sourceFile">The <see cref="Uri"/> of the source XMI file</param>
        /// <param name="rootNamespaceId">The <see cref="Guid"/> of the root namespace element</param>
        void RegisterRootNamespace(Uri sourceFile, Guid rootNamespaceId);

        /// <summary>
        /// Gets the root namespace identifier for a given source file
        /// </summary>
        /// <param name="sourceFile">The <see cref="Uri"/> of the source XMI file</param>
        /// <returns>The <see cref="Guid"/> of the root namespace, or <see cref="Guid.Empty"/> if not registered</returns>
        Guid GetRootNamespaceId(Uri sourceFile);
    }
}
