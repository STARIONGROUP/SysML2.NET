// -------------------------------------------------------------------------------------------------
// <copyright file="InheritanceScope.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Extensions
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// Shares the intermediate results of inheritance resolution across every
    /// <see cref="IType.inheritedMembership" /> query made while the scope is open.
    /// </summary>
    /// <remarks>
    /// Resolving <see cref="IType.inheritedMembership" /> walks a Type's transitive supertypes. Without a
    /// scope each query starts from an empty cache, so every Type re-walks the library supertype chain that
    /// all Types in a model share. Opening a scope around a bulk traversal collapses that repeated work.
    /// <para>Only the default query signature — no excluded Namespaces, no excluded Types and implied
    /// Relationships included — is shared, because a different signature yields different results. The
    /// shared entries are precisely the results the resolver already treats as independent of the path
    /// taken to reach a Type, so sharing them across queries changes no outcome.</para>
    /// <para>The scope caches against the model as it stands when each entry is produced, so it must not
    /// remain open across a mutation of that model. Scope it to a single read-only traversal.</para>
    /// <para>The current scope is tracked per thread, and scopes may nest: disposing one restores the scope
    /// that was open before it. A scope must therefore be opened and disposed on the SAME thread — a
    /// traversal that hands off to another thread simply resolves without sharing on that thread, but
    /// disposing from one would leave the opening thread's scope open.</para>
    /// </remarks>
    /// <example>
    /// <code>
    /// using (InheritanceScope.Begin())
    /// {
    ///     foreach (var type in types)
    ///     {
    ///         Consume(type.inheritedMembership);
    ///     }
    /// }
    /// </code>
    /// </example>
    public sealed class InheritanceScope : IDisposable
    {
        /// <summary>
        /// The scope currently open on this thread, if any.
        /// </summary>
        [ThreadStatic]
        private static InheritanceScope current;

        /// <summary>
        /// The scope that was open when this one began, restored on disposal, and repointed when a scope
        /// it encloses is disposed before it.
        /// </summary>
        private InheritanceScope enclosingScope;

        /// <summary>
        /// A value indicating whether this scope has already been disposed.
        /// </summary>
        private bool isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="InheritanceScope" /> class and makes it the current
        /// scope on the calling thread.
        /// </summary>
        private InheritanceScope()
        {
            this.enclosingScope = current;
            current = this;
        }

        /// <summary>
        /// Gets the scope currently open on the calling thread, or <c>null</c> when there is none.
        /// </summary>
        internal static InheritanceScope Current => current;

        /// <summary>
        /// Gets the results shared by the default query signature, keyed by the Type they were resolved for.
        /// </summary>
        internal Dictionary<IType, List<IMembership>> DefaultSignatureResults { get; } = [];

        /// <summary>
        /// Opens a new inheritance scope on the calling thread.
        /// </summary>
        /// <returns>The scope, which restores the previously open scope when disposed.</returns>
        public static InheritanceScope Begin()
        {
            return new InheritanceScope();
        }

        /// <summary>
        /// Closes this scope, restores the scope that enclosed it and releases the shared results.
        /// </summary>
        /// <remarks>
        /// Scopes are expected to close in the order they opened, but a caller holding two overlapping
        /// scopes may close them in any order, so a scope that is not the current one is spliced out of
        /// the chain rather than allowed to overwrite whichever scope is current by then. Closing an
        /// already-closed scope does nothing.
        /// </remarks>
        public void Dispose()
        {
            if (this.isDisposed)
            {
                return;
            }

            this.isDisposed = true;

            if (ReferenceEquals(current, this))
            {
                current = this.enclosingScope;
            }
            else
            {
                for (var openScope = current; openScope != null; openScope = openScope.enclosingScope)
                {
                    if (ReferenceEquals(openScope.enclosingScope, this))
                    {
                        openScope.enclosingScope = this.enclosingScope;

                        break;
                    }
                }
            }

            this.enclosingScope = null;
            this.DefaultSignatureResults.Clear();
        }
    }
}
