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
    /// Corrects known defects in the KEBNF grammar carried under <c>Resources/</c>, at generation time.
    /// </summary>
    /// <remarks>
    /// The KEBNF files are OMG source and are never edited, and the generated output is never hand-edited
    /// either — so a defect can only be corrected here, on the way from the one to the other. The files are
    /// mechanically extracted from the specification document, while the pilot implementation's parser is a
    /// separately hand-maintained Xtext grammar; the two drift, and a defect here is a SPECIFICATION defect.
    /// OMG has confirmed this class of finding and routes the fix through the Revision Task Forces
    /// (Systems-Modeling/SysML-v2-Release issue 124).
    /// <para>Two kinds of correction, because the defects differ in kind:</para>
    /// <para><see cref="Entries" /> — a rule whose name does not match the metaclass it builds and which
    /// omits the explicit target the grammar normally writes in that case
    /// (<c>RequirementKind : RequirementConstraintMembership</c>). Without it the rule name resolves to no
    /// metaclass, and the generator falls back to inferring one from the assigned property names, silently
    /// selecting an unrelated class that happens to declare the same property.</para>
    /// <para><see cref="ProductionEntries" /> — a production whose token sequence cannot derive notation the
    /// metamodel and the specification's own normative examples require. Applied to the grammar TEXT before
    /// it is parsed, so the corrected production flows through the normal pipeline and nothing downstream
    /// needs to special-case the rule.</para>
    /// <para>Scope is deliberately narrow, and the bar for both kinds is the same: the generator would
    /// otherwise produce output that is WRONG, not merely different. A production that admits more than one
    /// valid spelling is NOT an erratum — choosing between admissible spellings is the writer's business.
    /// The reference test is whether the pilot's Xtext grammar accepts what we emit: where it does, any
    /// difference is a style choice; where it cannot, the grammar is genuinely deficient.</para>
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
        /// The production-text corrections applied to the grammar before it is parsed.
        /// </summary>
        /// <remarks>
        /// An entry belongs here only when the grammar cannot derive the notation at all. A production that
        /// merely admits a spelling we do not emit is NOT an erratum — see the class remarks.
        /// </remarks>
        private static readonly GrammarProductionErratum[] ProductionEntries =
        [
            new("CaseBodyItem",
                "CaseBodyItem : Type =\n      ActionBodyItem",
                "CaseBodyItem : Type =\n      CalculationBodyItem",
                "SysML 8.2.2.22.1 gives CaseBodyItem the alternative 'ActionBodyItem', which reaches no " +
                "ReturnParameterMember, so 'return' cannot be written in a case body. Three independent " +
                "sources say it must be: (1) the pilot implementation's own grammar uses " +
                "'CalculationBodyItem' here (org.omg.sysml.xtext SysML.xtext, rule CaseBodyItem), and " +
                "CalculationBodyItem = ActionBodyItem | ReturnParameterMember; (2) the metamodel permits it " +
                "— constraint validateReturnParameterMembershipOwningType requires the owningType of a " +
                "ReturnParameterMembership to be a Function or Expression, and VerificationCaseUsage " +
                "specializes CaseUsage specializes CalculationUsage specializes Expression; (3) the " +
                "normative example in SysML 7.24.2 writes 'return verdict : VerdictKind = " +
                "evaluateData.verdict;' inside a 'verification def' body. There is no admissible " +
                "alternative spelling: rendering the ReturnParameterMembership through the generic " +
                "parameter path emits 'out verdict', which re-parses as a plain FeatureMembership with " +
                "direction out and so loses the metaclass. CalculationBodyItem is already declared in the " +
                "same file, so the replacement resolves without any further correction."),
            new("DefinitionElement",
                "    | InterfaceDefinition\n    | PortDefinition",
                "    | InterfaceDefinition\n    | AllocationDefinition\n    | PortDefinition",
                "SysML 8.2.2.5.2 declares 'AllocationDefinition = OccurrenceDefinitionPrefix 'allocation' " +
                "'def' Definition' but no production references it: DefinitionElement lists " +
                "ConnectionDefinition, FlowDefinition, InterfaceDefinition and PortDefinition, and omits " +
                "AllocationDefinition, so there is no path to it from RootNamespace. OMG has CONFIRMED this " +
                "as a specification error — Systems-Modeling/SysML-v2-Release issue 124 item 1, answered " +
                "2026-07-29: 'I can confirm that all your items are specification errors, except for item " +
                "5'; the correction is routed to a Revision Task Force, so it is not expected in the KEBNF " +
                "for some time. The issue's own suggested fix is the one applied here. The pilot " +
                "implementation already wires it exactly this way (org.omg.sysml.xtext SysML.xtext, rule " +
                "DefinitionElement, AllocationDefinition listed directly after InterfaceDefinition), which " +
                "is why the alternative is inserted at that position. There is no admissible alternative " +
                "spelling: AllocationDefinition specializes ConnectionDefinition, so without the " +
                "alternative the generator emits no dispatch arm and an AllocationDefinition renders " +
                "through the ConnectionDefinition arm as 'connection def'. That re-parses to a DIFFERENT " +
                "metaclass — AllocationUsage::allocationDefinition (which redefines " +
                "ConnectionUsage::connectionDefinition) derives to empty, and the library grounding shifts " +
                "from Allocations::Allocation to Connections::Connection. No OCL constraint flags the " +
                "downgrade, so nothing but the validation corpus catches it.")
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
        /// Applies every known production correction to the raw text of a KEBNF file.
        /// </summary>
        /// <param name="kebnfSource">The grammar text as read from disk.</param>
        /// <returns>
        /// The corrected grammar text, or <paramref name="kebnfSource" /> unchanged when nothing applies.
        /// </returns>
        /// <remarks>
        /// Correcting the text rather than the parsed rule keeps the correction in the grammar's own
        /// language: the entry reads as the production OMG should have written, and every consumer parses
        /// it exactly as it parses the rest of the file. Each <c>Original</c> is matched verbatim, so a
        /// correction cannot partially match, and re-applying it to already-corrected text is a no-op.
        /// Both KEBNF files are passed through this, so an entry only fires against the file that carries
        /// its production.
        /// <para>Line endings are normalised to <c>\n</c> FIRST, and every <c>Original</c> / <c>Replacement</c>
        /// is written with <c>\n</c>. A multi-line correction is otherwise silently inert on whichever
        /// platform disagrees with the checked-out line endings: the entries used to carry <c>\r\n</c>, which
        /// matched on Windows (<c>core.autocrlf=true</c> yields a CRLF working tree) and matched NOTHING on
        /// Linux CI, so the same commit generated different builders on the two platforms and the mismatch
        /// surfaced only as a downstream test failure. Normalising also makes the text handed to the parser
        /// byte-identical across platforms, so the whole generation pipeline is deterministic.</para>
        /// </remarks>
        public static string ApplyProductions(string kebnfSource)
        {
            if (string.IsNullOrWhiteSpace(kebnfSource))
            {
                return kebnfSource;
            }

            var normalisedSource = NormaliseLineEndings(kebnfSource);

            return ProductionEntries
                .Where(erratum => normalisedSource.Contains(erratum.Original, StringComparison.Ordinal))
                .Aggregate(normalisedSource, (corrected, erratum) =>
                {
                    AppliedRuleNames.Add(erratum.RuleName);

                    return corrected.Replace(erratum.Original, erratum.Replacement);
                });
        }

        /// <summary>
        /// Normalises CRLF and lone CR line endings to <c>\n</c> so a multi-line correction matches
        /// regardless of how the grammar file was checked out.
        /// </summary>
        /// <param name="source">The grammar text as read from disk.</param>
        /// <returns>The text with every line ending expressed as <c>\n</c>.</returns>
        private static string NormaliseLineEndings(string source)
        {
            return source.Replace("\r\n", "\n").Replace('\r', '\n');
        }

        /// <summary>
        /// Returns the corrections that matched nothing during this generator run.
        /// </summary>
        /// <returns>The stale entries, which should be pruned.</returns>
        /// <remarks>
        /// Only meaningful once every grammar file has been loaded and every rule read. A stale entry means
        /// the grammar no longer carries the defect — either OMG corrected it, or the rule was renamed or
        /// removed.
        /// </remarks>
        public static IReadOnlyList<(string RuleName, string Justification)> QueryUnappliedErrata()
        {
            return
            [
                ..Entries
                    .Where(erratum => !AppliedRuleNames.Contains(erratum.RuleName))
                    .Select(erratum => (erratum.RuleName, erratum.Justification)),
                ..ProductionEntries
                    .Where(erratum => !AppliedRuleNames.Contains(erratum.RuleName))
                    .Select(erratum => (erratum.RuleName, erratum.Justification))
            ];
        }
    }
}
