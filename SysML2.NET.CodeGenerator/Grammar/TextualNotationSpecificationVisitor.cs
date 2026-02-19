// -------------------------------------------------------------------------------------------------
// <copyright file="TextualNotationSpecificationVisitor.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Grammar
{
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.CodeGenerator.Grammar.Model;

    /// <summary>
    /// Custom <see cref="kebnfBaseVisitor{Result}"/> to read <see cref="TextualNotationSpecification" />
    /// </summary>
    public class TextualNotationSpecificationVisitor: kebnfBaseVisitor<object>
    {
        /// <summary>
        /// Visit a parse tree produced by <see cref="kebnfParser.specification"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result, as <see cref="TextualNotationSpecification"/>.</return>
        public override object VisitSpecification(kebnfParser.SpecificationContext context)
        {
            var specification = new TextualNotationSpecification();
            
            specification.Rules.AddRange(context.rule_definition().Select(rule => (TextualNotationRule)this.Visit(rule)));
            return specification;
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="kebnfParser.rule_definition"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result, as <see cref="TextualNotationRule" /></return>
        public override object VisitRule_definition(kebnfParser.Rule_definitionContext context)
        {
            var rule = new TextualNotationRule()
            {
                RuleName = context.name.Text,
                TargetElementName = context.name.Text
            };

            if (string.IsNullOrWhiteSpace(rule.RuleName))
            {
                rule.RuleName = rule.TargetElementName;
            }

            if (context.parameter_list() != null)
            {
                rule.Parameter = new RuleParameter()
                {
                    ParameterName = context.parameter_list().param_name.Text,
                    TargetElementName = context.parameter_list().param_type.Text
                };
            }
            
            rule.Elements.AddRange((List<RuleElement>)this.Visit(context.rule_body));
            return rule;
        }
        
        /// <summary>
        /// Visit a parse tree produced by <see cref="kebnfParser.AlternativesContext"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result, as a collection of <see cref="RuleElement" /></return>
        public override object VisitAlternatives(kebnfParser.AlternativesContext context)
        {
            return context.alternative().Select(a => (Alternatives)this.Visit(a)).SelectMany(x => x.Elements).ToList();
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="kebnfParser.alternative"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result, as an <see cref="Alternatives"/>.</return>
        public override object VisitAlternative(kebnfParser.AlternativeContext context)
        {
            var alternatives = new Alternatives();
            alternatives.Elements.AddRange(context.element().Select(e => (RuleElement)this.Visit(e)));
            return alternatives;
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="kebnfParser.assignment"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result, as <see cref="AssignmentElement"/>.</return>
        public override object VisitAssignment(kebnfParser.AssignmentContext context)
        {
            return new AssignmentElement()
            {
                Property = context.property.GetText(),
                Operator = context.op.Text,
                Suffix = context.suffix?.GetText(),
                Value = (RuleElement)this.Visit(context.content),
                Prefix = context.prefix?.Text
            };
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="kebnfParser.non_parsing_assignment"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result, as <see cref="NonParsingAssignmentElement"/></return>
        public override object VisitNon_parsing_assignment(kebnfParser.Non_parsing_assignmentContext context)
        {
            return new NonParsingAssignmentElement()
            {
                PropertyName = context.property.GetText(),
                Operator = context.op.Text,
                Value = context.val.GetText()
            };
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="kebnfParser.group"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result, as <see cref="GroupElement"/>.</return>
        public override object VisitGroup(kebnfParser.GroupContext context)
        {
            var group = new GroupElement()
            {
                Suffix = context.suffix?.GetText(),
            };
            
            group.Elements.AddRange((List<RuleElement>)this.Visit(context.alternatives()));
            return group;
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="kebnfParser.terminal"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result, as <see cref="TerminalElement"/>.</return>
        public override object VisitTerminal(kebnfParser.TerminalContext context)
        {
            return new TerminalElement()
            {
                Value = context.val.GetText().Trim('\''),
                Suffix = context.suffix?.GetText()
            };
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="kebnfParser.non_terminal"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result, as <see cref="NonTerminalElement"/>.</return>
        public override object VisitNon_terminal(kebnfParser.Non_terminalContext context)
        {
            return new NonTerminalElement()
            {
                Name = context.name.Text,
                Suffix = context.suffix?.GetText()
            };
        }

        /// <summary>
        /// Provides mapping data class for the alternative grammar part
        /// </summary>
        private class Alternatives
        {
            /// <summary>
            /// Gets a collection of <see cref="RuleElement" /> that is part of the <see cref="Alternatives" />
            /// </summary>
            public List<RuleElement> Elements { get; } = [];
        }
    }
}
