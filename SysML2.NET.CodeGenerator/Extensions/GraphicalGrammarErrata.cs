// ------------------------------------------------------------------------------------------------
// <copyright file="GraphicalGrammarErrata.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Corrects known defects in the graphical BNF (<c>Resources/SysML-graphical-bnf.kgbnf</c>), at generation time.
    /// </summary>
    /// <remarks>
    /// The grammar is OMG source and is never edited. An entry belongs here only when a reference names no production and exactly
    /// one production is evidently meant; a reference with no evident target stays unresolved and is reported by
    /// <c>GraphicalNotationSpecification.QueryUnresolvedReferences</c>.
    /// </remarks>
    public static class GraphicalGrammarErrata
    {
        /// <summary>
        /// The production-text corrections applied to the grammar before it is parsed.
        /// </summary>
        private static readonly GrammarProductionErratum[] ProductionEntries =
        [
            new("usage-cp",
                "usage-cp = usageDeclaration ValuePart?",
                "usage-cp = UsageDeclaration ValuePart?",
                "SysML 8.2.3.6 writes 'usageDeclaration', which names no rule; every other reference in 8.2.3 writes the textual rule " +
                "'UsageDeclaration' (SysML 8.2.2.6.2)."),
            new("allocation-node",
                "allocation-node =\n      general-node\n    | element-in-textual-compartment",
                "allocation-node =\n      general-node\n    | element-inside-textual-compartment",
                "SysML 8.2.3.15 references 'element-in-textual-compartment', which is never defined; the production is " +
                "'element-inside-textual-compartment' (SysML 8.2.3.2)."),
            new("exhibit-states-compartment-contents",
                "exhibit-states-compartment-contents = exhibit-state-scompartment-element* '…'?\n\nexhibit-states-compartment-element-compartment = UsageDeclaration",
                "exhibit-states-compartment-contents = exhibit-states-compartment-element* '…'?\n\nexhibit-states-compartment-element = UsageDeclaration",
                "SysML 8.2.3.18 references 'exhibit-state-scompartment-element', which is never defined, and defines " +
                "'exhibit-states-compartment-element-compartment', which is never referenced; every sibling compartment pairs " +
                "'<x>-compartment-contents' with '<x>-compartment-element'."),
            new("action-flow-view",
                "(perform-action-swimlanes)?",
                "(perform-actions-swimlanes)?",
                "SysML 8.2.3.17 references 'perform-action-swimlanes', which is never defined; the production is 'perform-actions-swimlanes'.")
        ];

        /// <summary>
        /// The production rule names whose corrections have matched during this generator run.
        /// </summary>
        private static readonly HashSet<string> AppliedRuleNames = [];

        /// <summary>
        /// Applies every known production correction to the raw text of the graphical BNF.
        /// </summary>
        /// <param name="kgbnfSource">The grammar text as read from disk.</param>
        /// <returns>The corrected grammar text with <c>\n</c> line endings, or <paramref name="kgbnfSource" /> when it is blank.</returns>
        /// <remarks>
        /// Line endings are normalised first because every <c>Original</c> is written with <c>\n</c>.
        /// </remarks>
        public static string ApplyProductions(string kgbnfSource)
        {
            if (string.IsNullOrWhiteSpace(kgbnfSource))
            {
                return kgbnfSource;
            }

            var normalisedSource = kgbnfSource.Replace("\r\n", "\n").Replace('\r', '\n');

            return ProductionEntries
                .Where(erratum => normalisedSource.Contains(erratum.Original, StringComparison.Ordinal))
                .Aggregate(normalisedSource, (corrected, erratum) =>
                {
                    AppliedRuleNames.Add(erratum.RuleName);
                    return corrected.Replace(erratum.Original, erratum.Replacement, StringComparison.Ordinal);
                });
        }

        /// <summary>
        /// Returns the corrections that matched nothing during this generator run.
        /// </summary>
        /// <returns>The stale entries, which should be pruned.</returns>
        /// <remarks>
        /// Only meaningful once the graphical BNF has been loaded.
        /// </remarks>
        public static IReadOnlyList<(string RuleName, string Justification)> QueryUnappliedErrata()
            =>
            [
                ..ProductionEntries
                    .Where(erratum => !AppliedRuleNames.Contains(erratum.RuleName))
                    .Select(erratum => (erratum.RuleName, erratum.Justification))
            ];
    }
}
