// -------------------------------------------------------------------------------------------------
// <copyright file="ILibraryTypeIndex.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Semantics.Implied
{
    using SysML2.NET.Core.POCO.Core.Types;

    /// <summary>
    /// Resolves a model-library Type by its qualified name, for the semantic constraints that require a
    /// user Type to specialize a specific library Type.
    /// </summary>
    /// <remarks>
    /// Implementations must index the library ownership tree directly and must NOT resolve through
    /// Namespace.resolve: resolution consults inheritedMembership, which is what the implied layer exists to
    /// supply, so routing through it re-enters that bootstrap cycle. For the same reason the index is
    /// populated eagerly, before any resolution runs, rather than faulting libraries in on first miss.
    /// </remarks>
    public interface ILibraryTypeIndex
    {
        /// <summary>
        /// Attempts to resolve the library Type carrying the supplied qualified name.
        /// </summary>
        /// <param name="qualifiedName">The qualified name, for example Occurrences::Occurrence::suboccurrences.</param>
        /// <param name="type">When this method returns true, the resolved Type; otherwise null.</param>
        /// <returns>True when the qualified name resolves to an indexed Type.</returns>
        bool TryGetType(string qualifiedName, out IType type);
    }
}
