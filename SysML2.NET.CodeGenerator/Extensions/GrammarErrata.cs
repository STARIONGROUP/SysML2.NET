// -------------------------------------------------------------------------------------------------
// <copyright file="GrammarErrata.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Supplies the target metaclass for KEBNF rules whose name does not match the metaclass they build,
    /// at generation time.
    /// </summary>
    /// <remarks>
    /// The KEBNF files under <c>Resources/</c> are OMG source and are never edited, and the generated
    /// output is never hand-edited either — so a rule that omits a target the generator cannot infer can
    /// only be corrected here, on the way from the one to the other. The files reproduce the
    /// textual-notation BNF of the KerML and SysML specifications verbatim, so a defect here is a
    /// SPECIFICATION defect; OMG has confirmed this class of finding and routes the fix through the
    /// Revision Task Forces (Systems-Modeling/SysML-v2-Release issue 124).
    /// <para>The grammar writes an explicit target whenever the rule name differs from the metaclass
    /// (<c>RequirementKind : RequirementConstraintMembership</c>, <c>SubjectMember : SubjectMembership</c>).
    /// Every entry below is a rule where that annotation is missing, so the rule name resolves to no
    /// metaclass at all and the generator falls back to inferring one from the assigned property names —
    /// which silently selects an unrelated class that happens to declare the same property.</para>
    /// <para>Scope is deliberately narrow: an entry corrects a rule the generator would otherwise bind to
    /// the WRONG metaclass. A production that merely admits more than one valid spelling is NOT an
    /// erratum — choosing between admissible spellings is the writer's business, not a correction to the
    /// grammar.</para>
    /// <para>These corrections are expected to become unnecessary as OMG publishes fixes. On a new KEBNF
    /// release, run the generator and prune whatever <see cref="QueryUnappliedErrata" /> reports — an entry
    /// that no longer matches has been fixed upstream.</para>
    /// </remarks>
    public static class GrammarErrata
    {
        /// <summary>
        /// The targets supplied to rules that omit them, keyed by the exact rule name.
        /// </summary>
        private static readonly GrammarErratum[] Entries =
        [
            new("LiteralReal", "LiteralRational",
                "KerML 8.2.2.24 writes 'LiteralReal = value = RealValue' with no target, but no metaclass named 'LiteralReal' exists — KerML 8.3.4.9 names it 'LiteralRational'. Its sibling literal rules (LiteralBoolean, LiteralString, LiteralInteger, LiteralInfinity) all match a metaclass by name, so only this one is left unresolved.")
        ];

        /// <summary>
        /// The corrections that have matched at least one rule during this generator run.
        /// </summary>
        private static readonly HashSet<string> AppliedRuleNames = [];

        /// <summary>
        /// Supplies the target metaclass for a rule when the grammar omits one and an erratum covers it.
        /// </summary>
        /// <param name="ruleName">The rule name read from the grammar.</param>
        /// <param name="targetElementName">The target the grammar declares, which may be null.</param>
        /// <returns>
        /// The corrected target, or <paramref name="targetElementName" /> unchanged when nothing applies.
        /// </returns>
        /// <remarks>
        /// A target the grammar states itself always wins: an erratum only fills a gap, so a rule that OMG
        /// later annotates upstream stops being corrected here and surfaces via
        /// <see cref="QueryUnappliedErrata" />.
        /// </remarks>
        public static string ApplyTarget(string ruleName, string targetElementName)
        {
            if (!string.IsNullOrWhiteSpace(targetElementName) || string.IsNullOrWhiteSpace(ruleName))
            {
                return targetElementName;
            }

            var erratum = Entries.SingleOrDefault(entry => string.Equals(entry.RuleName, ruleName, StringComparison.Ordinal));

            if (erratum == null)
            {
                return targetElementName;
            }

            AppliedRuleNames.Add(erratum.RuleName);

            return erratum.TargetElementName;
        }

        /// <summary>
        /// Returns the corrections that matched no rule during this generator run.
        /// </summary>
        /// <returns>The stale entries, which should be pruned from <see cref="Entries" />.</returns>
        /// <remarks>
        /// Only meaningful once every rule has been read. A stale entry means the grammar no longer carries
        /// the defect — either OMG annotated the rule, or the rule was renamed or removed.
        /// </remarks>
        public static IReadOnlyList<GrammarErratum> QueryUnappliedErrata()
        {
            return [..Entries.Where(erratum => !AppliedRuleNames.Contains(erratum.RuleName))];
        }
    }
}
