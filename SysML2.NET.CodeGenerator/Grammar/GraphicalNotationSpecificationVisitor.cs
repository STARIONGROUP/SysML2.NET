// ------------------------------------------------------------------------------------------------
// <copyright file="GraphicalNotationSpecificationVisitor.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Grammar
{
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.CodeGenerator.Grammar.Model;

    /// <summary>
    /// Custom <see cref="kgbnfBaseVisitor{Result}" /> to read a <see cref="GraphicalNotationSpecification" />
    /// </summary>
    public class GraphicalNotationSpecificationVisitor : kgbnfBaseVisitor<object>
    {
        /// <summary>
        /// Visits a parse tree produced by <see cref="kgbnfParser.alternative" />.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns>The visitor result, as <see cref="GraphicalAlternatives" />.</returns>
        public override object VisitAlternative(kgbnfParser.AlternativeContext context)
        {
            var alternatives = new GraphicalAlternatives();
            alternatives.Elements.AddRange(context.element().Select(element => (GraphicalRuleElement)this.Visit(element)));
            return alternatives;
        }

        /// <summary>
        /// Visits a parse tree produced by <see cref="kgbnfParser.alternatives" />.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns>The visitor result, as a list of <see cref="GraphicalAlternatives" />.</returns>
        public override object VisitAlternatives(kgbnfParser.AlternativesContext context)
            => context.alternative().Select(alternative => (GraphicalAlternatives)this.Visit(alternative)).ToList();

        /// <summary>
        /// Visits a parse tree produced by <see cref="kgbnfParser.edge_end" />.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns>The visitor result, as <see cref="GraphicalEdgeEndElement" />.</returns>
        public override object VisitEdge_end(kgbnfParser.Edge_endContext context)
            => new GraphicalEdgeEndElement
            {
                Name = context.name.Text,
                Suffix = context.suffix?.GetText()
            };

        /// <summary>
        /// Visits a parse tree produced by <see cref="kgbnfParser.group" />.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns>The visitor result, as <see cref="GraphicalGroupElement" />.</returns>
        public override object VisitGroup(kgbnfParser.GroupContext context)
        {
            var group = new GraphicalGroupElement
            {
                Suffix = context.suffix?.GetText()
            };

            group.Alternatives.AddRange((IEnumerable<GraphicalAlternatives>)this.Visit(context.alternatives()));
            return group;
        }

        /// <summary>
        /// Visits a parse tree produced by <see cref="kgbnfParser.image" />.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns>The visitor result, as <see cref="GraphicalImageElement" />.</returns>
        public override object VisitImage(kgbnfParser.ImageContext context) => new GraphicalImageElement();

        /// <summary>
        /// Visits a parse tree produced by <see cref="kgbnfParser.non_terminal" />.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns>The visitor result, as <see cref="GraphicalNonTerminalElement" />.</returns>
        public override object VisitNon_terminal(kgbnfParser.Non_terminalContext context)
            => new GraphicalNonTerminalElement
            {
                Name = context.name.Text,
                IsTextualReference = context.name.Type == kgbnfLexer.TEXTUAL_NAME,
                Suffix = context.suffix?.GetText()
            };

        /// <summary>
        /// Visits a parse tree produced by <see cref="kgbnfParser.rule_definition" />.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns>The visitor result, as <see cref="GraphicalNotationRule" />.</returns>
        public override object VisitRule_definition(kgbnfParser.Rule_definitionContext context)
        {
            var rule = new GraphicalNotationRule
            {
                RuleName = context.name.Text,
                IsPartial = context.op.Type == kgbnfLexer.PARTIAL_ASSIGN
            };

            rule.Alternatives.AddRange((IEnumerable<GraphicalAlternatives>)this.Visit(context.rule_body));
            return rule;
        }

        /// <summary>
        /// Visits a parse tree produced by <see cref="kgbnfParser.specification" />.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns>The visitor result, as <see cref="GraphicalNotationSpecification" />.</returns>
        public override object VisitSpecification(kgbnfParser.SpecificationContext context)
        {
            var specification = new GraphicalNotationSpecification();
            specification.Rules.AddRange(context.rule_definition().Select(rule => (GraphicalNotationRule)this.Visit(rule)));
            return specification;
        }

        /// <summary>
        /// Visits a parse tree produced by <see cref="kgbnfParser.terminal" />.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns>The visitor result, as <see cref="GraphicalTerminalElement" />.</returns>
        public override object VisitTerminal(kgbnfParser.TerminalContext context)
            => new GraphicalTerminalElement
            {
                Value = context.val.Text.Trim('\''),
                Suffix = context.suffix?.GetText()
            };
    }
}
