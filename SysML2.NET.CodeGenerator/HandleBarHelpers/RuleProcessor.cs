// -------------------------------------------------------------------------------------------------
// <copyright file="RuleProcessor.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.HandleBarHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using HandlebarsDotNet;

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.Grammar.Model;

    using uml4net.Extensions;
    using uml4net.StructuredClassifiers;

    /// <summary>
    /// Core rule processing engine that translates grammar alternatives into C# builder code.
    /// Instantiated once per Handlebars registration and captured in helper lambda closures.
    /// </summary>
    internal sealed partial class RuleProcessor
    {
        /// <summary>
        /// Core orchestration method that processes grammar alternatives and emits C# code.
        /// Dispatches to pattern handlers for recognized patterns, or falls back to element-by-element processing.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="alternatives">The grammar alternatives to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <param name="isPartOfMultipleAlternative">Whether this is part of a multi-alternative context</param>
        internal void ProcessAlternatives(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext, bool isPartOfMultipleAlternative = false)
        {
            ruleGenerationContext.DefinedCursors ??= [];

            if (alternatives.Count == 1)
            {
                this.ProcessSingleAlternative(writer, umlClass, alternatives.ElementAt(0), ruleGenerationContext, isPartOfMultipleAlternative);
            }
            else if (alternatives.All(x => x.Elements.Count == 1))
            {
                this.ProcessSingleElementAlternatives(writer, umlClass, alternatives, ruleGenerationContext);
            }
            else
            {
                this.ProcessMultiElementAlternatives(writer, umlClass, alternatives, ruleGenerationContext);
            }
        }

        /// <summary>
        /// Emits the body of a single alternative by iterating its elements and delegating to <see cref="ProcessRuleElement" />.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The current <see cref="IClass" /></param>
        /// <param name="alternative">The <see cref="Alternatives" /> whose elements are emitted</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        internal void EmitAlternativeBody(EncodedTextWriter writer, IClass umlClass, Alternatives alternative, RuleGenerationContext ruleGenerationContext)
        {
            var previousSiblings = ruleGenerationContext.CurrentSiblingElements;
            var previousIndex = ruleGenerationContext.CurrentElementIndex;
            ruleGenerationContext.CurrentSiblingElements = alternative.Elements;

            for (var elementIndex = 0; elementIndex < alternative.Elements.Count; elementIndex++)
            {
                ruleGenerationContext.CurrentElementIndex = elementIndex;
                this.ProcessRuleElement(writer, umlClass, alternative.Elements[elementIndex], ruleGenerationContext);
            }

            ruleGenerationContext.CurrentSiblingElements = previousSiblings;
            ruleGenerationContext.CurrentElementIndex = previousIndex;
        }

        /// <summary>
        /// Declares cursor variables for all enumerable properties referenced by assignment elements in the given alternative.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="alternatives">The <see cref="Alternatives" /> containing assignment elements</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        internal void DeclareAllRequiredCursors(EncodedTextWriter writer, IClass umlClass, Alternatives alternatives, RuleGenerationContext ruleGenerationContext)
        {
            foreach (var ruleElement in alternatives.Elements)
            {
                switch (ruleElement)
                {
                    case AssignmentElement assignmentElement:
                        this.DeclareCursorIfRequired(writer, umlClass, assignmentElement, ruleGenerationContext);
                        break;
                    case GroupElement groupElement:
                        foreach (var groupElementAlternative in groupElement.Alternatives)
                        {
                            this.DeclareAllRequiredCursors(writer, umlClass, groupElementAlternative, ruleGenerationContext);
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Emits a <c>Build{ruleName}HandCoded(variable, writerContext, stringBuilder);</c> fallback call.
        /// When <paramref name="deduplicate" /> is <c>true</c>, the call is only emitted if it has not
        /// already been emitted for the same rule name in the current generation scope.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="ruleName">The grammar rule name used to form the method name</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <param name="deduplicate">
        /// When <c>true</c>, suppress duplicate emissions via
        /// <see cref="RuleGenerationContext.EmittedHandCodedCalls" />
        /// </param>
        private void EmitHandCodedFallback(EncodedTextWriter writer, string ruleName, RuleGenerationContext ruleGenerationContext, bool deduplicate = false)
        {
            if (deduplicate && !ruleGenerationContext.EmittedHandCodedCalls.Add(ruleName))
            {
                return;
            }

            writer.WriteSafeString($"Build{ruleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, writerContext, stringBuilder);");
        }

        /// <summary>
        /// Processes a single alternative (no branching needed). Handles optional guard emission
        /// and iterates through the alternative's elements.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="alternative">The single <see cref="Alternatives" /> to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <param name="isPartOfMultipleAlternative">Whether this is part of a multi-alternative context</param>
        private void ProcessSingleAlternative(EncodedTextWriter writer, IClass umlClass, Alternatives alternative, RuleGenerationContext ruleGenerationContext, bool isPartOfMultipleAlternative)
        {
            var elements = alternative.Elements;
            this.DeclareAllRequiredCursors(writer, umlClass, alternative, ruleGenerationContext);

            if (ruleGenerationContext.CallerRule is { IsOptional: true, IsCollection: false })
            {
                var targetPropertiesName = elements.OfType<AssignmentElement>().Select(x => x.Property).Distinct().ToList();
                var allProperties = umlClass.QueryAllProperties();

                if (targetPropertiesName.Count > 0)
                {
                    writer.WriteSafeString(Environment.NewLine);
                    writer.WriteSafeString("if(");

                    var ifStatementContent = new List<string>();

                    foreach (var targetPropertyName in targetPropertiesName)
                    {
                        var property = allProperties.Single(x => string.Equals(x.Name, targetPropertyName, StringComparison.OrdinalIgnoreCase));

                        if (property.QueryIsEnumerable())
                        {
                            var assigment = elements.OfType<AssignmentElement>().First(x => x.Property == targetPropertyName);
                            var iterator = ruleGenerationContext.DefinedCursors.FirstOrDefault(x => x.ApplicableRuleElements.Contains(assigment));
                            ifStatementContent.Add(iterator == null ? $"BuildGroupConditionFor{assigment.TextualNotationRule.RuleName}(poco)" : property.QueryIfStatementContentForNonEmpty(iterator.CursorVariableName));
                        }
                        else
                        {
                            ifStatementContent.Add(property.QueryIfStatementContentForNonEmpty("poco"));
                        }
                    }

                    writer.WriteSafeString(string.Join(" && ", ifStatementContent));
                    writer.WriteSafeString($"){Environment.NewLine}");
                    writer.WriteSafeString($"{{{Environment.NewLine}");

                    var previousSiblings = ruleGenerationContext.CurrentSiblingElements;
                    var previousIndex = ruleGenerationContext.CurrentElementIndex;
                    ruleGenerationContext.CurrentSiblingElements = elements;

                    for (var elementIndex = 0; elementIndex < elements.Count; elementIndex++)
                    {
                        ruleGenerationContext.CurrentElementIndex = elementIndex;
                        var previousCaller = ruleGenerationContext.CallerRule;
                        this.ProcessRuleElement(writer, umlClass, elements[elementIndex], ruleGenerationContext);
                        ruleGenerationContext.CallerRule = previousCaller;
                    }

                    ruleGenerationContext.CurrentSiblingElements = previousSiblings;
                    ruleGenerationContext.CurrentElementIndex = previousIndex;
                }
                else
                {
                    var nonTerminalElements = elements.OfType<NonTerminalElement>().ToList();
                    var inlineConditionParts = new List<string>();

                    foreach (var nonTerminal in nonTerminalElements)
                    {
                        var referencedRule = ruleGenerationContext.FindRule(nonTerminal.Name);

                        if (referencedRule != null)
                        {
                            var condition = this.GenerateInlineOptionalCondition(referencedRule, umlClass, ruleGenerationContext.AllRules, "poco");

                            if (condition != null)
                            {
                                inlineConditionParts.Add(condition);
                            }
                        }
                    }

                    if (inlineConditionParts.Count > 0)
                    {
                        writer.WriteSafeString($"{Environment.NewLine}if ({string.Join(" || ", inlineConditionParts)}){Environment.NewLine}");
                    }
                    else
                    {
                        writer.WriteSafeString($"{Environment.NewLine}if (BuildGroupConditionFor{alternative.TextualNotationRule.RuleName}(poco)){Environment.NewLine}");
                    }

                    writer.WriteSafeString($"{{{Environment.NewLine}");

                    var previousSiblings = ruleGenerationContext.CurrentSiblingElements;
                    var previousIndex = ruleGenerationContext.CurrentElementIndex;
                    ruleGenerationContext.CurrentSiblingElements = elements;

                    for (var elementIndex = 0; elementIndex < elements.Count; elementIndex++)
                    {
                        ruleGenerationContext.CurrentElementIndex = elementIndex;
                        this.ProcessRuleElement(writer, umlClass, elements[elementIndex], ruleGenerationContext);
                    }

                    ruleGenerationContext.CurrentSiblingElements = previousSiblings;
                    ruleGenerationContext.CurrentElementIndex = previousIndex;
                }

                if (!ruleGenerationContext.IsNextElementNewLineTerminal() && !ruleGenerationContext.IsLastElement())
                {
                    writer.WriteSafeString($"stringBuilder.Append(' ');{Environment.NewLine}");
                }

                writer.WriteSafeString($"}}{Environment.NewLine}");
            }
            else
            {
                var previousSiblings = ruleGenerationContext.CurrentSiblingElements;
                var previousIndex = ruleGenerationContext.CurrentElementIndex;
                ruleGenerationContext.CurrentSiblingElements = elements;

                for (var elementIndex = 0; elementIndex < elements.Count; elementIndex++)
                {
                    ruleGenerationContext.CurrentElementIndex = elementIndex;
                    var previousCaller = ruleGenerationContext.CallerRule;
                    this.ProcessRuleElement(writer, umlClass, elements[elementIndex], ruleGenerationContext, isPartOfMultipleAlternative);
                    ruleGenerationContext.CallerRule = previousCaller;
                }

                ruleGenerationContext.CurrentSiblingElements = previousSiblings;
                ruleGenerationContext.CurrentElementIndex = previousIndex;
            }
        }

        /// <summary>
        /// Processes multiple alternatives where every alternative has exactly one element.
        /// Handles multi-collection assignments, unityped dispatch, and mixed-type element handling.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="alternatives">The grammar alternatives to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        private void ProcessSingleElementAlternatives(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
        {
            if (alternatives.ElementAt(0).Elements[0].TextualNotationRule.IsMultiCollectionAssignment)
            {
                this.ProcessMultiCollectionAssignment(writer, umlClass, alternatives, ruleGenerationContext);
                return;
            }

            var types = alternatives.SelectMany(x => x.Elements).Select(x => x.GetType()).Distinct().ToList();

            if (types.Count == 1)
            {
                this.ProcessUnitypedAlternativesWithOneElement(writer, umlClass, alternatives, ruleGenerationContext);
            }
            else
            {
                this.ProcessMixedTypeSingleElementAlternatives(writer, umlClass, alternatives, ruleGenerationContext, types);
            }
        }

        /// <summary>
        /// Processes multiple single-element alternatives with mixed element types
        /// (e.g., Assignment + NonTerminal, Terminal + Assignment).
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="alternatives">The grammar alternatives to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <param name="types">The distinct element types across all alternatives</param>
        private void ProcessMixedTypeSingleElementAlternatives(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext, List<Type> types)
        {
            if (types.SequenceEqual([typeof(AssignmentElement), typeof(NonTerminalElement)]))
            {
                foreach (var alternative in alternatives)
                {
                    this.DeclareAllRequiredCursors(writer, umlClass, alternative, ruleGenerationContext);
                }

                for (var alternativeIndex = 0; alternativeIndex < alternatives.Count; alternativeIndex++)
                {
                    var ruleElement = alternatives.ElementAt(alternativeIndex).Elements[0];

                    if (alternativeIndex != 0)
                    {
                        writer.WriteSafeString("else");
                    }

                    switch (ruleElement)
                    {
                        case AssignmentElement assignmentElement:
                            var targetProperty = umlClass.QueryAllProperties().Single(x => string.Equals(x.Name, assignmentElement.Property));

                            if (alternativeIndex != 0)
                            {
                                writer.WriteSafeString(" ");
                            }

                            if (targetProperty.QueryIsEnumerable())
                            {
                                this.DeclareAllRequiredCursors(writer, umlClass, alternatives.ElementAt(0), ruleGenerationContext);

                                var iterator = ruleGenerationContext.DefinedCursors.Single(x => x.ApplicableRuleElements.Contains(assignmentElement));

                                writer.WriteSafeString($"if({targetProperty.QueryIfStatementContentForNonEmpty(iterator.CursorVariableName)}){Environment.NewLine}");
                                writer.WriteSafeString($"{{{Environment.NewLine}");
                            }
                            else
                            {
                                writer.WriteSafeString($"{Environment.NewLine}if({targetProperty.QueryIfStatementContentForNonEmpty("poco")}){Environment.NewLine}");
                                writer.WriteSafeString($"{{{Environment.NewLine}");
                            }

                            this.ProcessAssignmentElement(writer, umlClass, ruleGenerationContext, assignmentElement, true);

                            writer.WriteSafeString($"{Environment.NewLine}}}");
                            break;

                        case NonTerminalElement nonTerminalElement:
                            writer.WriteSafeString($"{{{Environment.NewLine}");
                            this.ProcessNonTerminalElement(writer, umlClass, nonTerminalElement, ruleGenerationContext);
                            writer.WriteSafeString($"{Environment.NewLine}}}");
                            break;
                    }
                }
            }
            else if (types.SequenceEqual([typeof(NonTerminalElement), typeof(AssignmentElement)]))
            {
                this.EmitNonTerminalThenAssignmentDispatch(writer, umlClass, alternatives, ruleGenerationContext);
            }
            else if (alternatives.ElementAt(0).Elements[0] is TerminalElement terminalElement && alternatives.ElementAt(1).Elements[0] is AssignmentElement assignmentElement)
            {
                var targetProperty = umlClass.QueryAllProperties().Single(x => string.Equals(x.Name, assignmentElement.Property));

                writer.WriteSafeString($"if(!{targetProperty.QueryIfStatementContentForNonEmpty("poco")}){Environment.NewLine}");
                writer.WriteSafeString($"{{{Environment.NewLine}");
                this.ProcessRuleElement(writer, umlClass, terminalElement, ruleGenerationContext);
                writer.WriteSafeString($"{Environment.NewLine}}}");
                writer.WriteSafeString("else");
                writer.WriteSafeString($"{Environment.NewLine}{{{Environment.NewLine}");
                this.ProcessAssignmentElement(writer, umlClass, ruleGenerationContext, assignmentElement, true);
                writer.WriteSafeString($"{Environment.NewLine}}}");
            }
            else
            {
                var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;

                this.EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext, true);
            }
        }

        /// <summary>
        /// Emits the mixed NonTerminal + AssignmentElement dispatch pattern where the first alternative
        /// is a NonTerminal and the remaining are AssignmentElements targeting cursor elements via +=.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="alternatives">The grammar alternatives to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        private void EmitNonTerminalThenAssignmentDispatch(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
        {
            var nonTerminalElement = (NonTerminalElement)alternatives.ElementAt(0).Elements[0];
            var assignmentElements = alternatives.SelectMany(x => x.Elements).OfType<AssignmentElement>().ToList();

            var referencedAssignmentNonTerminals = assignmentElements.Select(x => x.Value).OfType<NonTerminalElement>().ToList();

            if (referencedAssignmentNonTerminals.Count != assignmentElements.Count)
            {
                var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                this.EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
                return;
            }

            var targetProperty = umlClass.QueryAllProperties().Single(x => string.Equals(x.Name, assignmentElements[0].Property));
            var cursorVarName = $"{targetProperty.Name.LowerCaseFirstLetter()}Cursor";
            var existingCursor = ruleGenerationContext.DefinedCursors.FirstOrDefault(x => x.IsCursorValidForProperty(targetProperty));

            if (existingCursor == null)
            {
                writer.WriteSafeString($"var {cursorVarName} = writerContext.CursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{targetProperty.QueryPropertyNameBasedOnUmlProperties()});{Environment.NewLine}");
                var cursorDef = new CursorDefinition { DefinedForProperty = targetProperty };

                foreach (var assignmentElement in assignmentElements)
                {
                    cursorDef.ApplicableRuleElements.Add(assignmentElement);
                }

                ruleGenerationContext.DefinedCursors.Add(cursorDef);
            }
            else
            {
                cursorVarName = existingCursor.CursorVariableName;
            }

            var assignmentMappedElements = RuleQueryUtilities.OrderElementsByInheritance(referencedAssignmentNonTerminals, umlClass.Cache, ruleGenerationContext);

            for (var assignmentIndex = 0; assignmentIndex < assignmentMappedElements.Count; assignmentIndex++)
            {
                var mappedElement = assignmentMappedElements[assignmentIndex];

                if (assignmentIndex > 0)
                {
                    writer.WriteSafeString("else ");
                }

                var assignmentVarName = mappedElement.UmlClass.Name.LowerCaseFirstLetter();
                writer.WriteSafeString($"if ({cursorVarName}.Current is {mappedElement.UmlClass.QueryFullyQualifiedTypeName()} {assignmentVarName}){Environment.NewLine}");
                writer.WriteSafeString($"{{{Environment.NewLine}");

                var previousVariableName = ruleGenerationContext.CurrentVariableName;
                ruleGenerationContext.CurrentVariableName = assignmentVarName;
                this.ProcessNonTerminalElement(writer, mappedElement.UmlClass, mappedElement.RuleElement, ruleGenerationContext);
                ruleGenerationContext.CurrentVariableName = previousVariableName;

                writer.WriteSafeString($"{Environment.NewLine}{cursorVarName}.Move();{Environment.NewLine}");
                writer.WriteSafeString($"}}{Environment.NewLine}");
            }

            writer.WriteSafeString($"else{Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");

            var nonTerminalReferencedRule = ruleGenerationContext.FindRule(nonTerminalElement.Name);

            var nonTerminalTypeTarget = nonTerminalReferencedRule != null
                ? nonTerminalReferencedRule.EffectiveTarget
                : umlClass.Name;

            var nonTerminalCall = this.ResolveBuilderCall(umlClass, nonTerminalElement, nonTerminalTypeTarget, ruleGenerationContext);

            if (nonTerminalCall != null)
            {
                writer.WriteSafeString(nonTerminalCall);
            }
            else
            {
                var previousCaller = ruleGenerationContext.CallerRule;
                var previousName = ruleGenerationContext.CurrentVariableName;
                ruleGenerationContext.CallerRule = nonTerminalElement;
                this.ProcessAlternatives(writer, umlClass, nonTerminalReferencedRule?.Alternatives, ruleGenerationContext);
                ruleGenerationContext.CallerRule = previousCaller;
                ruleGenerationContext.CurrentVariableName = previousName;
            }

            writer.WriteSafeString($"{Environment.NewLine}}}{Environment.NewLine}");
        }

        /// <summary>
        /// Processes multiple alternatives where at least one alternative has more than one element.
        /// Handles terminal-only, QualifiedName-or-chain, terminal-vs-body, and pattern handler dispatch.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="alternatives">The grammar alternatives to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        private void ProcessMultiElementAlternatives(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
        {
            // When all alternatives consist exclusively of terminal elements (and optionally non-parsing assignments), handle via code-gen
            if (alternatives.All(alt => alt.Elements.Count > 0 && alt.Elements.All(element => element is TerminalElement or NonParsingAssignmentElement)))
            {
                this.EmitTerminalOnlyAlternatives(writer, umlClass, alternatives, ruleGenerationContext);
                return;
            }

            // Detect pattern: property=[QualifiedName] | property=NonTerminal{containment+=property}
            if (alternatives.Count == 2 && this.TryEmitQualifiedNameOrChainAlternatives(writer, umlClass, alternatives, ruleGenerationContext))
            {
                return;
            }

            // Multi-element alternatives (e.g., ';' | '{' NamespaceBodyElement* '}')
            var firstAlt = alternatives.ElementAt(0);
            var hasTerminalOnlyFirstAlt = firstAlt.Elements.Count == 1 && firstAlt.Elements[0] is TerminalElement;

            if (hasTerminalOnlyFirstAlt && alternatives.Count == 2)
            {
                this.EmitTerminalVsBodyAlternatives(writer, umlClass, alternatives, ruleGenerationContext);
            }
            else
            {
                // Try each pattern handler in order; fall back to HandCoded if none match
                if (this.TryHandleOperatorLiteralAlternation(writer, umlClass, alternatives, ruleGenerationContext))
                {
                    return;
                }

                if (this.TryHandleEmptyVsNonEmptyMembership(writer, umlClass, alternatives, ruleGenerationContext))
                {
                    return;
                }

                if (this.TryHandlePocoTypeDispatchWithCompoundAlternatives(writer, umlClass, alternatives, ruleGenerationContext))
                {
                    return;
                }

                if (this.TryHandleReferenceOrInline(writer, umlClass, alternatives, ruleGenerationContext))
                {
                    return;
                }

                var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;

                this.EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext, true);
            }
        }

        /// <summary>
        /// Emits code for alternatives that consist exclusively of terminal elements and optional
        /// non-parsing assignments. Generates either a direct terminal emit or a switch on the
        /// non-parsing assignment property.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="alternatives">The grammar alternatives to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        private void EmitTerminalOnlyAlternatives(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
        {
            var nonParsingAssignments = alternatives
                .SelectMany(alt => alt.Elements.OfType<NonParsingAssignmentElement>())
                .ToList();

            if (nonParsingAssignments.Count == 0)
            {
                var firstAlternative = alternatives.ElementAt(0);

                foreach (var terminalOnly in firstAlternative.Elements.Cast<TerminalElement>())
                {
                    TerminalWriter.WriteTerminalAppend(writer, terminalOnly.Value);
                }
            }
            else
            {
                var assignmentPropertyName = nonParsingAssignments[0].PropertyName;
                var targetProperty = umlClass.QueryAllProperties().SingleOrDefault(x => string.Equals(x.Name, assignmentPropertyName, StringComparison.OrdinalIgnoreCase));

                if (targetProperty != null)
                {
                    var targetPropertyName = targetProperty.QueryPropertyNameBasedOnUmlProperties();

                    writer.WriteSafeString($"switch ({ruleGenerationContext.CurrentVariableName ?? "poco"}.{targetPropertyName}){Environment.NewLine}");
                    writer.WriteSafeString($"{{{Environment.NewLine}");

                    foreach (var alternative in alternatives)
                    {
                        var nonParsingAssignment = alternative.Elements.OfType<NonParsingAssignmentElement>().Single();
                        var terminals = alternative.Elements.OfType<TerminalElement>().ToList();
                        var enumValueName = nonParsingAssignment.Value.Trim('\'').CapitalizeFirstLetter();

                        writer.WriteSafeString($"case {targetProperty.Type.QueryFullyQualifiedTypeName()}.{enumValueName}:{Environment.NewLine}");

                        foreach (var terminal in terminals)
                        {
                            TerminalWriter.WriteTerminalAppend(writer, terminal.Value);
                        }

                        writer.WriteSafeString($"{Environment.NewLine}break;{Environment.NewLine}");
                    }

                    writer.WriteSafeString($"}}{Environment.NewLine}");
                }
                else
                {
                    var firstAlternative = alternatives.ElementAt(0);

                    foreach (var terminalOnly in firstAlternative.Elements.OfType<TerminalElement>())
                    {
                        TerminalWriter.WriteTerminalAppend(writer, terminalOnly.Value);
                    }
                }
            }
        }

        /// <summary>
        /// Attempts to emit code for the QualifiedName-or-chain pattern:
        /// <c>property=[QualifiedName] | property=NonTerminal{containment+=property}</c>.
        /// At runtime: if the referenced value is owned, call the chain builder; else output qualifiedName.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="alternatives">The grammar alternatives to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <returns><c>true</c> if the pattern matched and code was emitted; <c>false</c> otherwise</returns>
        private bool TryEmitQualifiedNameOrChainAlternatives(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
        {
            var qualifiedNameAlt = alternatives.FirstOrDefault(alt =>
                alt.Elements.Count == 1
                && alt.Elements[0] is AssignmentElement { Value: ValueLiteralElement qualifiedNameLiteral }
                && qualifiedNameLiteral.QueryIsQualifiedName());

            var chainAlt = alternatives.FirstOrDefault(alt =>
                alt.Elements.Count >= 2
                && alt.Elements[0] is AssignmentElement { Value: NonTerminalElement }
                && alt.Elements.OfType<NonParsingAssignmentElement>().Any());

            if (qualifiedNameAlt == null || chainAlt == null)
            {
                return false;
            }

            var qualifiedNameAssignment = (AssignmentElement)qualifiedNameAlt.Elements[0];
            var chainAssignment = (AssignmentElement)chainAlt.Elements[0];
            var chainNonTerminal = (NonTerminalElement)chainAssignment.Value;
            var containmentAssignment = chainAlt.Elements.OfType<NonParsingAssignmentElement>().First();

            var propertyName = qualifiedNameAssignment.Property;
            var allProperties = umlClass.QueryAllProperties();

            var targetProperty = allProperties.SingleOrDefault(x =>
                string.Equals(x.Name, propertyName, StringComparison.OrdinalIgnoreCase));

            var containmentProperty = allProperties.SingleOrDefault(x =>
                string.Equals(x.Name, containmentAssignment.PropertyName, StringComparison.OrdinalIgnoreCase));

            if (targetProperty == null || containmentProperty == null)
            {
                return false;
            }

            var variableName = ruleGenerationContext.CurrentVariableName ?? "poco";
            var resolvedPropertyName = targetProperty.QueryPropertyNameBasedOnUmlProperties();
            var resolvedContainmentName = containmentProperty.QueryPropertyNameBasedOnUmlProperties();

            var referencedRule = ruleGenerationContext.FindRule(chainNonTerminal.Name);

            var typeTarget = referencedRule != null
                ? referencedRule.EffectiveTarget
                : umlClass.Name;

            var chainTargetClass = RuleQueryUtilities.FindClass(umlClass.Cache, typeTarget);

            if (chainTargetClass == null)
            {
                return false;
            }

            var chainTypeName = chainTargetClass.QueryFullyQualifiedTypeName();
            var chainVarName = $"chained{resolvedPropertyName}As{chainTargetClass.Name}";

            string builderCallString;

            if (typeTarget == ruleGenerationContext.NamedElementToGenerate.Name)
            {
                builderCallString = $"Build{chainNonTerminal.Name}({chainVarName}, writerContext, stringBuilder);";
            }
            else
            {
                builderCallString = $"{typeTarget}TextualNotationBuilder.Build{chainNonTerminal.Name}({chainVarName}, writerContext, stringBuilder);";
            }

            writer.WriteSafeString($"if ({variableName}.{resolvedContainmentName}.Contains({variableName}.{resolvedPropertyName}) && {variableName}.{resolvedPropertyName} is {chainTypeName} {chainVarName}){Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");
            writer.WriteSafeString($"{builderCallString}{Environment.NewLine}");
            writer.WriteSafeString($"}}{Environment.NewLine}");
            writer.WriteSafeString($"else if ({variableName}.{resolvedPropertyName} != null){Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");
            writer.WriteSafeString($"SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder,{variableName}.{resolvedPropertyName}, writerContext);{Environment.NewLine}");
            writer.WriteSafeString($"stringBuilder.Append(' ');{Environment.NewLine}");
            writer.WriteSafeString($"}}{Environment.NewLine}");

            return true;
        }

        /// <summary>
        /// Emits the terminal-vs-body pattern where the first alternative is a single terminal
        /// (e.g., <c>;</c>) and the second alternative is a body with collection assignments or
        /// non-terminal elements (e.g., <c>{ NamespaceBodyElement* }</c>).
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="alternatives">The grammar alternatives to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        private void EmitTerminalVsBodyAlternatives(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
        {
            var firstAlt = alternatives.ElementAt(0);
            var secondAlt = alternatives.ElementAt(1);
            var collectionAssignments = secondAlt.Elements.OfType<AssignmentElement>().Where(x => x.Operator == "+=").ToList();

            var groupsWithCollectionAssignments = secondAlt.Elements.OfType<GroupElement>()
                .SelectMany(g => g.Alternatives.SelectMany(a => a.Elements.OfType<AssignmentElement>().Where(x => x.Operator == "+=")))
                .ToList();

            var allCollectionAssignments = collectionAssignments.Concat(groupsWithCollectionAssignments).ToList();

            if (allCollectionAssignments.Count > 0)
            {
                this.EmitTerminalVsBodyWithCollectionAssignments(writer, umlClass, firstAlt, secondAlt, allCollectionAssignments, ruleGenerationContext);
            }
            else
            {
                var collectionNonTerminals = secondAlt.Elements.OfType<NonTerminalElement>().Where(x => x.IsCollection).ToList();

                if (collectionNonTerminals.Count > 0)
                {
                    this.EmitTerminalVsBodyWithCollectionNonTerminals(writer, umlClass, firstAlt, secondAlt, collectionNonTerminals, ruleGenerationContext);
                }
                else
                {
                    var nonCollectionNonTerminals = secondAlt.Elements.OfType<NonTerminalElement>().Where(x => !x.IsCollection).ToList();

                    if (nonCollectionNonTerminals.Count > 0)
                    {
                        this.EmitTerminalVsBodyWithSingleNonTerminal(writer, umlClass, firstAlt, secondAlt, nonCollectionNonTerminals, ruleGenerationContext);
                    }
                    else
                    {
                        var handCodedRuleName = firstAlt.TextualNotationRule.RuleName;
                        this.EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
                    }
                }
            }
        }

        /// <summary>
        /// Emits the terminal-vs-body pattern when the body contains collection assignment elements.
        /// </summary>
        private void EmitTerminalVsBodyWithCollectionAssignments(EncodedTextWriter writer, IClass umlClass, Alternatives firstAlt, Alternatives secondAlt, List<AssignmentElement> allCollectionAssignments, RuleGenerationContext ruleGenerationContext)
        {
            var collectionProperty = allCollectionAssignments[0].Property;
            var targetProperty = umlClass.QueryAllProperties().SingleOrDefault(x => string.Equals(x.Name, collectionProperty, StringComparison.OrdinalIgnoreCase));
            var terminalValue = ((TerminalElement)firstAlt.Elements[0]).Value;

            if (targetProperty != null)
            {
                var bodyPropertyAccess = targetProperty.QueryPropertyNameBasedOnUmlProperties();
                writer.WriteSafeString($"if(writerContext.CursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{bodyPropertyAccess}).Current == null){Environment.NewLine}");

                writer.WriteSafeString($"{{{Environment.NewLine}");
                writer.WriteSafeString($"stringBuilder.AppendLine(\"{terminalValue}\");{Environment.NewLine}");
                writer.WriteSafeString($"}}{Environment.NewLine}");
                writer.WriteSafeString($"else{Environment.NewLine}");
                writer.WriteSafeString($"{{{Environment.NewLine}");

                this.DeclareAllRequiredCursors(writer, umlClass, secondAlt, ruleGenerationContext);

                foreach (var element in secondAlt.Elements)
                {
                    this.ProcessRuleElement(writer, umlClass, element, ruleGenerationContext);
                }

                writer.WriteSafeString($"}}{Environment.NewLine}");
            }
            else
            {
                var handCodedRuleName = firstAlt.TextualNotationRule.RuleName;
                this.EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
            }
        }

        /// <summary>
        /// Emits the terminal-vs-body pattern when the body contains collection non-terminal elements
        /// (e.g., <c>; | { Items* }</c>).
        /// </summary>
        private void EmitTerminalVsBodyWithCollectionNonTerminals(EncodedTextWriter writer, IClass umlClass, Alternatives firstAlt, Alternatives secondAlt, List<NonTerminalElement> collectionNonTerminals, RuleGenerationContext ruleGenerationContext)
        {
            var nonTerminalRule = ruleGenerationContext.FindRule(collectionNonTerminals[0].Name);

            if (nonTerminalRule == null)
            {
                var handCodedRuleName = firstAlt.TextualNotationRule.RuleName;
                this.EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
                return;
            }

            var collectionPropertyNames = nonTerminalRule.QueryCollectionPropertyNames(ruleGenerationContext.AllRules);

            if (collectionPropertyNames.Count == 0)
            {
                var handCodedRuleName = firstAlt.TextualNotationRule.RuleName;
                this.EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
                return;
            }

            var collectionPropertyName = collectionPropertyNames.First();
            var targetProperty = umlClass.QueryAllProperties().SingleOrDefault(x => string.Equals(x.Name, collectionPropertyName, StringComparison.OrdinalIgnoreCase));
            var terminalValue = ((TerminalElement)firstAlt.Elements[0]).Value;

            if (targetProperty == null)
            {
                var handCodedRuleName = firstAlt.TextualNotationRule.RuleName;
                this.EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
                return;
            }

            var propertyAccessName = targetProperty.QueryPropertyNameBasedOnUmlProperties();

            writer.WriteSafeString($"if(writerContext.CursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{propertyAccessName}).Current == null){Environment.NewLine}");

            writer.WriteSafeString($"{{{Environment.NewLine}");
            writer.WriteSafeString($"stringBuilder.AppendLine(\"{terminalValue}\");{Environment.NewLine}");
            writer.WriteSafeString($"}}{Environment.NewLine}");
            writer.WriteSafeString($"else{Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");

            foreach (var element in secondAlt.Elements)
            {
                if (element is NonTerminalElement { IsCollection: true })
                {
                    var cursorVarName = $"{targetProperty.Name.LowerCaseFirstLetter()}Cursor";
                    writer.WriteSafeString($"var {cursorVarName} = writerContext.CursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{propertyAccessName});{Environment.NewLine}");

                    var collectionNonTerminal = (NonTerminalElement)element;
                    var referencedRule = ruleGenerationContext.FindRule(collectionNonTerminal.Name);

                    var typeTarget = referencedRule != null
                        ? referencedRule.EffectiveTarget
                        : umlClass.Name;

                    var perItemCall = this.ResolveBuilderCall(umlClass, collectionNonTerminal, typeTarget, ruleGenerationContext);

                    writer.WriteSafeString($"while ({cursorVarName}.Current != null){Environment.NewLine}");
                    writer.WriteSafeString($"{{{Environment.NewLine}");

                    if (perItemCall != null)
                    {
                        writer.WriteSafeString(perItemCall);
                    }
                    else
                    {
                        var previousCaller = ruleGenerationContext.CallerRule;
                        var previousName = ruleGenerationContext.CurrentVariableName;
                        ruleGenerationContext.CallerRule = collectionNonTerminal;
                        this.ProcessAlternatives(writer, umlClass, referencedRule?.Alternatives, ruleGenerationContext);
                        ruleGenerationContext.CallerRule = previousCaller;
                        ruleGenerationContext.CurrentVariableName = previousName;
                    }

                    writer.WriteSafeString($"{Environment.NewLine}}}{Environment.NewLine}");
                }
                else
                {
                    this.ProcessRuleElement(writer, umlClass, element, ruleGenerationContext);
                }
            }

            writer.WriteSafeString($"}}{Environment.NewLine}");
        }

        /// <summary>
        /// Emits the terminal-vs-body pattern when the body contains a single (non-collection)
        /// non-terminal element (e.g., <c>; | { CalculationBodyPart }</c>).
        /// </summary>
        private void EmitTerminalVsBodyWithSingleNonTerminal(EncodedTextWriter writer, IClass umlClass, Alternatives firstAlt, Alternatives secondAlt, List<NonTerminalElement> nonCollectionNonTerminals, RuleGenerationContext ruleGenerationContext)
        {
            var nonTerminalRule = ruleGenerationContext.FindRule(nonCollectionNonTerminals[0].Name);

            if (nonTerminalRule == null)
            {
                var handCodedRuleName = firstAlt.TextualNotationRule.RuleName;
                this.EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
                return;
            }

            var collectionPropertyNames = nonTerminalRule.QueryCollectionPropertyNames(ruleGenerationContext.AllRules);

            if (collectionPropertyNames.Count == 0)
            {
                var handCodedRuleName = firstAlt.TextualNotationRule.RuleName;
                this.EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
                return;
            }

            var collectionPropertyName = collectionPropertyNames.First();
            var targetProperty = umlClass.QueryAllProperties().SingleOrDefault(x => string.Equals(x.Name, collectionPropertyName, StringComparison.OrdinalIgnoreCase));
            var terminalValue = ((TerminalElement)firstAlt.Elements[0]).Value;

            if (targetProperty == null)
            {
                var handCodedRuleName = firstAlt.TextualNotationRule.RuleName;
                this.EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
                return;
            }

            var propertyAccessName = targetProperty.QueryPropertyNameBasedOnUmlProperties();

            writer.WriteSafeString($"if(writerContext.CursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{propertyAccessName}).Current == null){Environment.NewLine}");

            writer.WriteSafeString($"{{{Environment.NewLine}");
            writer.WriteSafeString($"stringBuilder.AppendLine(\"{terminalValue}\");{Environment.NewLine}");
            writer.WriteSafeString($"}}{Environment.NewLine}");
            writer.WriteSafeString($"else{Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");

            foreach (var element in secondAlt.Elements)
            {
                if (element is NonTerminalElement { IsCollection: false } singleNonTerminal)
                {
                    var cursorVarName = $"{targetProperty.Name.LowerCaseFirstLetter()}Cursor";
                    writer.WriteSafeString($"var {cursorVarName} = writerContext.CursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{propertyAccessName});{Environment.NewLine}");

                    var referencedRule = ruleGenerationContext.FindRule(singleNonTerminal.Name);

                    var typeTarget = referencedRule != null
                        ? referencedRule.EffectiveTarget
                        : umlClass.Name;

                    var perItemCall = this.ResolveBuilderCall(umlClass, singleNonTerminal, typeTarget, ruleGenerationContext);

                    writer.WriteSafeString($"if ({cursorVarName}.Current != null){Environment.NewLine}");
                    writer.WriteSafeString($"{{{Environment.NewLine}");

                    if (perItemCall != null)
                    {
                        writer.WriteSafeString(perItemCall);
                    }
                    else
                    {
                        var previousCaller = ruleGenerationContext.CallerRule;
                        var previousName = ruleGenerationContext.CurrentVariableName;
                        ruleGenerationContext.CallerRule = singleNonTerminal;
                        this.ProcessAlternatives(writer, umlClass, referencedRule?.Alternatives, ruleGenerationContext);
                        ruleGenerationContext.CallerRule = previousCaller;
                        ruleGenerationContext.CurrentVariableName = previousName;
                    }

                    writer.WriteSafeString($"{Environment.NewLine}}}{Environment.NewLine}");
                }
                else
                {
                    this.ProcessRuleElement(writer, umlClass, element, ruleGenerationContext);
                }
            }

            writer.WriteSafeString($"}}{Environment.NewLine}");
        }
    }
}
