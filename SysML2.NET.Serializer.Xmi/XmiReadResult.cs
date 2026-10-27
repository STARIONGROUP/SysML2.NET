// ------------------------------------------------------------------------------------------------
// <copyright file="XmiReadResult.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The outcome of reading one XMI resource: the <see cref="INamespace" /> that was asked for, together
    /// with the root <see cref="INamespace" />s of every other resource that had to be read to resolve it.
    /// <para>Reading a single file is never self-contained — an external reference (<c>href</c>) to a model
    /// library pulls in that library, which pulls in its own dependencies. Returning only the requested root
    /// hides those resources from the caller even though they are fully read and resolved.</para>
    /// <para>The distinction matters for name resolution. Per KerML 1.0 §8.2.3.5.2, a root
    /// <see cref="INamespace" /> has an implicit containing <i>global</i> <see cref="INamespace" /> that
    /// "includes all the visible Memberships of all other root Namespaces that are available to the first
    /// Namespace", and §8.2.3.5.4 makes resolution in that scope the final step of resolving a qualified
    /// name. <see cref="ReferencedNamespaces" /> is exactly that set, so a consumer resolving or emitting
    /// qualified names (such as the textual notation writer) can honour the global scope.</para>
    /// </summary>
    public class XmiReadResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmiReadResult" /> class
        /// </summary>
        /// <param name="rootNamespace">The root <see cref="INamespace" /> of the resource that was read</param>
        /// <param name="referencedNamespaces">
        /// The root <see cref="INamespace" />s of the resources read to resolve external references; may be
        /// <see langword="null" />, which is treated as an empty collection
        /// </param>
        /// <exception cref="ArgumentNullException">If <paramref name="rootNamespace" /> is null</exception>
        public XmiReadResult(INamespace rootNamespace, IReadOnlyCollection<INamespace> referencedNamespaces)
        {
            this.RootNamespace = rootNamespace ?? throw new ArgumentNullException(nameof(rootNamespace));
            this.ReferencedNamespaces = referencedNamespaces ?? [];
        }

        /// <summary>
        /// Gets the root <see cref="INamespace" /> of the resource that was read — the file the caller asked for
        /// </summary>
        public INamespace RootNamespace { get; }

        /// <summary>
        /// Gets the root <see cref="INamespace" />s of every OTHER resource read while resolving external
        /// references, excluding <see cref="RootNamespace" />. Together with <see cref="RootNamespace" /> these
        /// form the global <see cref="INamespace" /> described in KerML 1.0 §8.2.3.5.2.
        /// </summary>
        public IReadOnlyCollection<INamespace> ReferencedNamespaces { get; }
    }
}
