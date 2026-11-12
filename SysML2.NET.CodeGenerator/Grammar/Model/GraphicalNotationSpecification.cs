// ------------------------------------------------------------------------------------------------
// <copyright file="GraphicalNotationSpecification.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Grammar.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides access to all <see cref="GraphicalNotationRule" /> defined in the graphical notation specification
    /// </summary>
    public class GraphicalNotationSpecification
    {
        /// <summary>
        /// Gets the <see cref="GraphicalNotationRule" />s in declaration order, one per production definition
        /// </summary>
        public List<GraphicalNotationRule> Rules { get; } = [];

        /// <summary>
        /// Queries the references that name no rule: textual names against the textual notation rules, graphical names against
        /// <see cref="Rules" />
        /// </summary>
        /// <param name="textualRuleNames">The names of the textual notation rules</param>
        /// <returns>The distinct unresolved references, with the production that holds each</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="textualRuleNames" /> is null</exception>
        public IReadOnlyList<(string RuleName, string ReferenceName)> QueryUnresolvedReferences(IReadOnlySet<string> textualRuleNames)
        {
            ArgumentNullException.ThrowIfNull(textualRuleNames);

            var productionNames = this.Rules.Select(rule => rule.RuleName).ToHashSet();

            return
            [
                ..this.Rules
                    .SelectMany(rule => rule.QueryAllElements().Select(element => (rule.RuleName, ReferenceName: element switch
                    {
                        GraphicalNonTerminalElement { IsTextualReference: true } textualReference
                            when !textualRuleNames.Contains(textualReference.Name) => textualReference.Name,
                        GraphicalNonTerminalElement { IsTextualReference: false } graphicalReference
                            when !productionNames.Contains(graphicalReference.Name) => graphicalReference.Name,
                        GraphicalEdgeEndElement edgeEnd when !productionNames.Contains(edgeEnd.Name) => edgeEnd.Name,
                        _ => null
                    })))
                    .Where(reference => reference.ReferenceName != null)
                    .Distinct()
            ];
        }
    }
}
