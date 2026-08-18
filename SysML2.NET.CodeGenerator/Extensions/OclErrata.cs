// -------------------------------------------------------------------------------------------------
// <copyright file="OclErrata.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Corrects known defects in the OCL bodies carried by the UML XMI, at generation time.
    /// </summary>
    /// <remarks>
    /// The XMI files under <c>Resources/</c> are OMG source and are never edited, and the generated output
    /// is never hand-edited either — so a defect in a normative OCL body can only be corrected here, on the
    /// way from the one to the other.
    /// <para>Every entry below is a library qualified name that no Type in the Kernel Semantic Library or
    /// the Systems Library declares, so the constraint that carries it cannot be satisfied by any model.
    /// Each is evidenced by the same XMI spelling the name correctly elsewhere, by the library declaring
    /// the corrected name, or both. Nothing here reinterprets what a constraint MEANS: an erratum only
    /// repairs a name that is demonstrably a typo.</para>
    /// <para>These corrections are expected to become unnecessary as OMG publishes fixes. On a new XMI
    /// release, run the generator and prune whatever <see cref="QueryUnappliedErrata" /> reports — an entry
    /// that no longer matches has been fixed upstream. `ImpliedRelationshipTargetsTestFixture` fails if a
    /// target stops resolving, so a regression cannot pass unnoticed.</para>
    /// </remarks>
    public static class OclErrata
    {
        /// <summary>
        /// The corrections applied to OCL bodies, keyed by the exact quoted literal they replace.
        /// </summary>
        /// <remarks>
        /// Matching includes the surrounding single quotes, so a correction cannot partially match a
        /// longer name — <c>'Items::Item::subitem'</c> does not match <c>'Items::Item::subitems'</c> — and
        /// re-applying a correction to already-corrected text is a no-op.
        /// </remarks>
        private static readonly OclErratum[] Entries =
        [
            new("'Action::Action::controls'", "'Actions::Action::controls'",
                "The package is 'Actions'; the same XMI uses 'Actions::Action::…' in every other Action constraint."),
            new("'Actions::Action::join'", "'Actions::Action::joins'",
                "The Systems Library declares 'joins'; no Feature named 'join' exists."),
            new("'Items::Item::subitem'", "'Items::Item::subitems'",
                "The Systems Library declares 'subitems'; no Feature named 'subitem' exists."),
            new("'Objects::Object::ownedPerformance'", "'Objects::Object::ownedPerformances'",
                "The Kernel Semantic Library declares 'ownedPerformances'; no Feature named 'ownedPerformance' exists."),
            new("'Occurrence::Occurrence::portions'", "'Occurrences::Occurrence::portions'",
                "The package is 'Occurrences'; the same XMI uses 'Occurrences::Occurrence::…' for snapshots, timeSlices and timeEnclosedOccurrences."),
            new("'Occurrence::Occurrence::suboccurrences'", "'Occurrences::Occurrence::suboccurrences'",
                "The package is 'Occurrences'; the same XMI spells this exact name correctly in other constraints."),
            new("'Performances::Performance::enclosedPerformance'", "'Performances::Performance::enclosedPerformances'",
                "The Kernel Semantic Library declares 'enclosedPerformances'; no Feature named 'enclosedPerformance' exists."),
            new("'Performances::Performance::subperformance'", "'Performances::Performance::subperformances'",
                "The Kernel Semantic Library declares 'subperformances'; no Feature named 'subperformance' exists.")
        ];

        /// <summary>
        /// The corrections that have matched at least one OCL body during this generator run.
        /// </summary>
        private static readonly HashSet<string> AppliedOriginals = [];

        /// <summary>
        /// Applies every known correction to an OCL body.
        /// </summary>
        /// <param name="ocl">The OCL body read from the XMI, which may be null.</param>
        /// <returns>The corrected OCL body, or <paramref name="ocl" /> unchanged when nothing applies.</returns>
        public static string Apply(string ocl)
        {
            if (string.IsNullOrWhiteSpace(ocl))
            {
                return ocl;
            }

            return Entries
                .Where(erratum => ocl.Contains(erratum.Original, StringComparison.Ordinal))
                .Aggregate(ocl, (corrected, erratum) =>
                {
                    AppliedOriginals.Add(erratum.Original);

                    return corrected.Replace(erratum.Original, erratum.Replacement);
                });
        }

        /// <summary>
        /// Returns the corrections that matched no OCL body during this generator run.
        /// </summary>
        /// <returns>The stale entries, which should be pruned from <see cref="Entries" />.</returns>
        /// <remarks>
        /// Only meaningful once every constraint has been read. A stale entry means the XMI no longer
        /// carries the defect — either OMG fixed it, or the constraint was removed.
        /// </remarks>
        public static IReadOnlyList<OclErratum> QueryUnappliedErrata()
        {
            return [..Entries.Where(erratum => !AppliedOriginals.Contains(erratum.Original))];
        }
    }
}
