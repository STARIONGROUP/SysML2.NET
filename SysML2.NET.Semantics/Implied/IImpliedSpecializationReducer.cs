// -------------------------------------------------------------------------------------------------
// <copyright file="IImpliedSpecializationReducer.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Core.Types;

    /// <summary>
    /// Drops the implied Specializations that KerML 8.4.2 considers redundant for a Type.
    /// </summary>
    /// <remarks>
    /// Two rules apply: an implied Specialization is dropped when the Type already has an ownedSpecialization
    /// with the same general Type, or when any owned or implied Specialization has a general Type that is a
    /// strict subtype of it; and only one of several implied Specializations sharing a general Type is kept.
    /// Neither rule applies to Redefinitions, whose semantics go beyond basic Specialization.
    /// </remarks>
    public interface IImpliedSpecializationReducer
    {
        /// <summary>
        /// Reduces the candidate implied Specializations of a Type to the non-redundant set.
        /// </summary>
        /// <param name="type">The Type the candidates were computed for.</param>
        /// <param name="candidates">The implied Specializations to reduce.</param>
        /// <returns>The retained Specializations, in the order the candidates were supplied.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when either argument is null.</exception>
        IReadOnlyList<ISpecialization> Reduce(IType type, IReadOnlyList<ISpecialization> candidates);
    }
}
