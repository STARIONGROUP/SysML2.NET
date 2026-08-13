// -------------------------------------------------------------------------------------------------
// <copyright file="ImpliedSpecializationReducer.cs" company="Starion Group S.A.">
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
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Types;

    /// <summary>
    /// Drops the implied Specializations that KerML 8.4.2 considers redundant for a Type.
    /// </summary>
    /// <remarks>
    /// Rule 1 drops a candidate when the Type already owns a Specialization with the same general Type, or
    /// when any owned or surviving implied Specialization has a general Type that is a STRICT subtype of the
    /// candidate's; the more specific Specialization already satisfies the looser constraint. Rule 2 keeps
    /// only the first of several candidates sharing a general Type. Neither rule is applied to
    /// Redefinitions, whose semantics go beyond basic Specialization.
    /// </remarks>
    public class ImpliedSpecializationReducer : IImpliedSpecializationReducer
    {
        /// <summary>
        /// Reduces the candidate implied Specializations of a Type to the non-redundant set.
        /// </summary>
        /// <param name="type">The Type the candidates were computed for.</param>
        /// <param name="candidates">The implied Specializations to reduce.</param>
        /// <returns>The retained Specializations, in the order the candidates were supplied.</returns>
        /// <exception cref="ArgumentNullException">Thrown when either argument is null.</exception>
        public IReadOnlyList<ISpecialization> Reduce(IType type, IReadOnlyList<ISpecialization> candidates)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (candidates == null)
            {
                throw new ArgumentNullException(nameof(candidates));
            }

            var declaredGenerals = type.ownedSpecialization
                .Select(specialization => specialization.General)
                .Where(general => general != null)
                .ToList();

            var retained = new List<ISpecialization>();
            var retainedGenerals = new List<IType>();

            foreach (var candidate in candidates.Where(candidate => candidate?.General != null))
            {
                // Rule 2: a general Type already retained makes this candidate a duplicate.
                if (retainedGenerals.Contains(candidate.General))
                {
                    continue;
                }

                if (IsSupersededBy(candidate.General, declaredGenerals))
                {
                    continue;
                }

                retained.Add(candidate);
                retainedGenerals.Add(candidate.General);
            }

            // Rule 1 across the implied set itself: a retained candidate whose general Type is a strict
            // SUPERtype of another retained candidate's is redundant. Applied after the pass above because
            // it needs the full surviving set, not a prefix of it. The candidate's own general Type is
            // excluded from the comparison, since AllSupertypes includes the Type itself and would
            // otherwise make every candidate supersede itself.
            return retained
                .Where(candidate => !IsStrictlySupersededBy(candidate.General, retainedGenerals))
                .ToList();
        }

        /// <summary>
        /// Asserts whether a candidate general Type is already covered by one of the supplied general Types,
        /// treating an identical general Type as covering it.
        /// </summary>
        /// <param name="candidateGeneral">The general Type of the candidate Specialization.</param>
        /// <param name="generals">The general Types to test against.</param>
        /// <returns>True when one of <paramref name="generals" /> is the same Type or a subtype of it.</returns>
        private static bool IsSupersededBy(IType candidateGeneral, IReadOnlyList<IType> generals)
        {
            return generals.Any(general => general == candidateGeneral
                                           || (general != null && general.AllSupertypes().Contains(candidateGeneral)));
        }

        /// <summary>
        /// Asserts whether a candidate general Type is covered by a DIFFERENT general Type in the supplied
        /// set, i.e. one that is a strict subtype of it.
        /// </summary>
        /// <param name="candidateGeneral">The general Type of the candidate Specialization.</param>
        /// <param name="generals">The general Types to test against, which may include the candidate's own.</param>
        /// <returns>True when a different Type in <paramref name="generals" /> is a strict subtype of it.</returns>
        private static bool IsStrictlySupersededBy(IType candidateGeneral, IReadOnlyList<IType> generals)
        {
            return generals
                .Where(general => general != null && general != candidateGeneral)
                .Any(general => general.AllSupertypes().Contains(candidateGeneral));
        }
    }
}
