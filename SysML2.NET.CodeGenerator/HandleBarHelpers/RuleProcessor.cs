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
    using uml4net.SimpleClassifiers;
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
            this.EmitElements(writer, umlClass, alternative.Elements, ruleGenerationContext);
        }

        /// <summary>
        /// Emits <paramref name="elements" /> in order while maintaining the sibling/index context,
        /// optionally restoring <see cref="RuleGenerationContext.CallerRule" /> after each element.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The current <see cref="IClass" /></param>
        /// <param name="elements">The elements to emit</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <param name="restoreCallerPerElement">Whether to restore the caller rule after each element</param>
        /// <param name="isPartOfMultipleAlternative">Whether this is part of a multi-alternative context</param>
        private void EmitElements(EncodedTextWriter writer, IClass umlClass, List<RuleElement> elements, RuleGenerationContext ruleGenerationContext, bool restoreCallerPerElement = false, bool isPartOfMultipleAlternative = false)
        {
            elements = HoistSingleNonNotationalConsumption(elements, ruleGenerationContext);

            var previousSiblings = ruleGenerationContext.CurrentSiblingElements;
            var previousIndex = ruleGenerationContext.CurrentElementIndex;
            ruleGenerationContext.CurrentSiblingElements = elements;

            for (var elementIndex = 0; elementIndex < elements.Count; elementIndex++)
            {
                ruleGenerationContext.CurrentElementIndex = elementIndex;
                var previousCaller = ruleGenerationContext.CallerRule;
                this.ProcessRuleElement(writer, umlClass, elements[elementIndex], ruleGenerationContext, isPartOfMultipleAlternative);

                if (restoreCallerPerElement)
                {
                    ruleGenerationContext.CallerRule = previousCaller;
                }
            }

            ruleGenerationContext.CurrentSiblingElements = previousSiblings;
            ruleGenerationContext.CurrentElementIndex = previousIndex;
        }

        /// <summary>
        /// Text-free members the pilot is KNOWN to store before the elements the production declares ahead of
        /// them, verified against real pilot output.
        /// <para>Deliberately an allowlist, not a structural rule. Emitting no text does NOT imply the model
        /// may store the element anywhere: storage order is a per-rule implementation detail and it goes BOTH
        /// ways. <c>EmptyMultiplicityMember</c> is stored FIRST though declared last, while
        /// <c>EmptyResultMember</c> / <c>ReturnParameterMembership</c> is stored LAST as declared (in the
        /// <c>OperatorExpression</c> family, <c>InvocationExpression</c> and <c>FeatureReferenceExpression</c>).
        /// Hoisting the latter would strand the cursor on it. Only add a name here after checking real output.</para>
        /// </summary>
        private static readonly HashSet<string> HoistableTextFreeMembers = new(StringComparer.Ordinal)
        {
            "EmptyMultiplicityMember",
        };

        /// <summary>
        /// Moves a lone <c>+=</c> element whose production emits NO text to the front of the alternative.
        /// <para>Such an element has no observable position in the notation, so the grammar cannot constrain
        /// where the parser puts it in the collection — and the pilot does not always put it where the
        /// production does. Consuming it first keeps the cursor aligned for the elements that DO emit text;
        /// leaving it in place strands the cursor on it (e.g. <c>IndividualDefinition</c> declares
        /// <c>EmptyMultiplicityMember</c> last but the model stores it first, hiding the Subclassification
        /// that <c>Definition</c> must read).</para>
        /// <para>Only a LONE such element is hoisted: when several appear (e.g. <c>TransitionUsage</c>'s two
        /// <c>EmptyParameterMember</c>s) their relative order decides which pairs with which sibling, so
        /// moving them would change meaning.</para>
        /// </summary>
        /// <param name="elements">The alternative's elements in grammar order.</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" />.</param>
        /// <returns>The elements, reordered when a lone text-free consumption is present.</returns>
        private static List<RuleElement> HoistSingleNonNotationalConsumption(List<RuleElement> elements, RuleGenerationContext ruleGenerationContext)
        {
            var textFree = elements
                .OfType<AssignmentElement>()
                .Where(assignment => assignment.Operator == "+="
                                     && assignment.Value is NonTerminalElement nonTerminal
                                     && HoistableTextFreeMembers.Contains(nonTerminal.Name)
                                     && EmitsNoNotation(ruleGenerationContext.FindRule(nonTerminal.Name), ruleGenerationContext, []))
                .ToList();

            // Only a TRAILING text-free element is hoisted. Declared last, it has nothing after it whose
            // position it could encode, so moving it is meaning-preserving; declared mid-sequence its order
            // relative to the following elements is significant (TransitionUsage's EmptyParameterMember
            // pairs with the TriggerActionMember that follows it).
            if (textFree.Count != 1
                || !ReferenceEquals(elements[^1], textFree[0])
                || ReferenceEquals(elements[0], textFree[0]))
            {
                return elements;
            }

            var reordered = new List<RuleElement> { textFree[0] };
            reordered.AddRange(elements.Where(element => !ReferenceEquals(element, textFree[0])));

            return reordered;
        }

        /// <summary>
        /// Determines whether <paramref name="rule" /> produces no textual notation at all — no terminal and
        /// no value-bearing assignment, transitively. <c>EmptyMultiplicity</c>, <c>EmptyUsage</c> and their
        /// wrappers are the canonical cases.
        /// </summary>
        /// <param name="rule">The rule to inspect; may be <see langword="null" />.</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" />.</param>
        /// <param name="visited">Rules already inspected, guarding against recursive productions.</param>
        /// <returns><see langword="true" /> when the rule emits nothing.</returns>
        private static bool EmitsNoNotation(TextualNotationRule rule, RuleGenerationContext ruleGenerationContext, HashSet<string> visited)
        {
            if (rule == null || !visited.Add(rule.RuleName))
            {
                return false;
            }

            return rule.Alternatives.SelectMany(alternative => alternative.Elements).All(element => element switch
            {
                NonParsingAssignmentElement => true,
                AssignmentElement { Value: NonTerminalElement nested } => EmitsNoNotation(ruleGenerationContext.FindRule(nested.Name), ruleGenerationContext, visited),
                NonTerminalElement nonTerminal => EmitsNoNotation(ruleGenerationContext.FindRule(nonTerminal.Name), ruleGenerationContext, visited),
                _ => false,
            });
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
        /// Emits a <c>Build{ruleName}HandCoded(…)</c> fallback call, optionally deduplicated per
        /// generation scope.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="ruleName">The grammar rule name used to form the method name</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <param name="deduplicate">When <c>true</c>, suppress duplicate emissions for the same rule name</param>
        private static void EmitHandCodedFallback(EncodedTextWriter writer, string ruleName, RuleGenerationContext ruleGenerationContext, bool deduplicate = false)
        {
            if (deduplicate && !ruleGenerationContext.EmittedHandCodedCalls.Add(ruleName))
            {
                return;
            }

            writer.WriteSafeString($"Build{ruleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, writerContext, stringBuilder);");
        }

        /// <summary>
        /// Collects, in runtime consumption order, the <c>+=</c> assignments targeting
        /// <paramref name="targetPropertyName" />: the optional group's own, then the parent
        /// alternative's tail. The tail walk stops at the first sibling whose cursor contribution is
        /// not statically determinable, so callers never guard positions they cannot prove.
        /// </summary>
        /// <param name="optionalGroupElements">The optional group's own elements</param>
        /// <param name="siblingElements">The parent alternative's elements</param>
        /// <param name="optionalElementIndex">The index of the optional group within <paramref name="siblingElements" /></param>
        /// <param name="targetPropertyName">The property name whose <c>+=</c> consumptions are collected</param>
        /// <returns>The ordered list of cursor consumptions the optional path requires</returns>
        private static List<AssignmentElement> CollectCursorConsumptions(IReadOnlyList<RuleElement> optionalGroupElements, IReadOnlyList<RuleElement> siblingElements, int optionalElementIndex, string targetPropertyName)
        {
            var consumptionAssignments = new List<AssignmentElement>();

            if (optionalGroupElements != null)
            {
                foreach (var ruleElement in optionalGroupElements)
                {
                    if (ruleElement is AssignmentElement { Operator: "+=" } assignmentElement
                        && string.Equals(assignmentElement.Property, targetPropertyName, StringComparison.OrdinalIgnoreCase))
                    {
                        consumptionAssignments.Add(assignmentElement);
                    }
                }
            }

            if (siblingElements == null)
            {
                return consumptionAssignments;
            }

            for (var siblingIndex = optionalElementIndex + 1; siblingIndex < siblingElements.Count; siblingIndex++)
            {
                switch (siblingElements[siblingIndex])
                {
                    case TerminalElement:
                    case NonParsingAssignmentElement:
                    case ValueLiteralElement:
                        continue;

                    case AssignmentElement { Operator: "+=" } cursorAssignment
                        when string.Equals(cursorAssignment.Property, targetPropertyName, StringComparison.OrdinalIgnoreCase):
                        consumptionAssignments.Add(cursorAssignment);
                        continue;

                    case AssignmentElement:
                        continue;

                    case GroupElement groupElement when !GroupCanConsumeTargetCursor(groupElement, targetPropertyName):
                        continue;

                    default:
                        return consumptionAssignments;
                }
            }

            return consumptionAssignments;
        }

        /// <summary>
        /// Determines recursively whether <paramref name="groupElement" /> contains a <c>+=</c> assignment
        /// targeting <paramref name="targetPropertyName" /> (i.e. could shift the runtime cursor offset).
        /// </summary>
        /// <param name="groupElement">The <see cref="GroupElement" /> to inspect</param>
        /// <param name="targetPropertyName">The property name whose consumption is detected</param>
        /// <returns><c>true</c> if the group could consume the target cursor; <c>false</c> otherwise</returns>
        private static bool GroupCanConsumeTargetCursor(GroupElement groupElement, string targetPropertyName)
        {
            foreach (var alternative in groupElement.Alternatives)
            {
                foreach (var element in alternative.Elements)
                {
                    switch (element)
                    {
                        case AssignmentElement { Operator: "+=" } cursorAssignment
                            when string.Equals(cursorAssignment.Property, targetPropertyName, StringComparison.OrdinalIgnoreCase):
                            return true;

                        case GroupElement nestedGroup when GroupCanConsumeTargetCursor(nestedGroup, targetPropertyName):
                            return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Resolves the fully-qualified runtime type the assignment's consumed cursor element must
        /// satisfy, or <c>null</c> when the referenced rule has no resolvable target class.
        /// </summary>
        /// <param name="assignmentElement">The <c>+=</c> assignment to resolve the target type of</param>
        /// <param name="umlClass">The class hosting the current rule (provides the UML cache)</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <returns>The fully-qualified target type name, or <c>null</c> if it cannot be resolved</returns>
        private static string ResolveAssignmentTargetTypeName(AssignmentElement assignmentElement, IClass umlClass, RuleGenerationContext ruleGenerationContext)
        {
            if (assignmentElement.Value is not NonTerminalElement nonTerminalElement)
            {
                return null;
            }

            var referencedRule = ruleGenerationContext.FindRule(nonTerminalElement.Name);
            var typeTarget = referencedRule?.EffectiveTarget;

            if (typeTarget == null)
            {
                return null;
            }

            var targetClass = RuleQueryUtilities.FindClass(umlClass.Cache, typeTarget);
            var targetTypeName = targetClass?.QueryFullyQualifiedTypeName();

            if (targetTypeName == null)
            {
                return null;
            }

            // A rule may PIN a property to a constant through a non-parsing assignment, e.g.
            // `GuardExpressionMember : TransitionFeatureMembership = 'if' { kind = 'guard' } …`.
            // Sibling rules then share one target type and are distinguishable ONLY by that constant,
            // so it has to be part of the guard or the first sibling swallows them all.
            var pinnedConstantPattern = ResolvePinnedConstantPattern(referencedRule, targetClass);

            return pinnedConstantPattern == null ? targetTypeName : $"{targetTypeName} {pinnedConstantPattern}";
        }

        /// <summary>
        /// Builds a C# property pattern for a constant a rule pins via a non-parsing assignment
        /// (<c>{ kind = 'guard' }</c>), used to tell apart sibling rules that share a target type.
        /// </summary>
        /// <param name="referencedRule">The rule whose pinned constant is sought.</param>
        /// <param name="targetClass">The rule's target <see cref="IClass" />.</param>
        /// <returns>The property pattern, or <see langword="null" /> when nothing enum-typed is pinned.</returns>
        private static string ResolvePinnedConstantPattern(TextualNotationRule referencedRule, IClass targetClass)
        {
            var pinnedAssignment = referencedRule.Alternatives
                .SelectMany(alternative => alternative.Elements)
                .OfType<NonParsingAssignmentElement>()
                .FirstOrDefault(assignment => assignment.Operator == "=" && !string.IsNullOrWhiteSpace(assignment.Value));

            if (pinnedAssignment == null)
            {
                return null;
            }

            var property = targetClass.QueryAllProperties()
                .FirstOrDefault(x => string.Equals(x.Name, pinnedAssignment.PropertyName, StringComparison.OrdinalIgnoreCase));

            if (property?.Type is not IEnumeration)
            {
                return null;
            }

            var literalName = pinnedAssignment.Value.Trim('\'').CapitalizeFirstLetter();
            return $"{{ {property.Name.CapitalizeFirstLetter()}: {property.Type.QueryFullyQualifiedTypeName()}.{literalName} }}";
        }

        /// <summary>
        /// Resolves the inner element type of a "thin owning wrapper" rule
        /// (<c>X : OwningMembership = ownedRelatedElement += Y</c>). The wrapper type is too coarse a
        /// discriminator — every <c>OwningMembership</c> subtype satisfies it — so the guard narrows to
        /// the wrapped type. Returns <see langword="null"/> when the rule is not a thin wrapper.
        /// </summary>
        /// <param name="assignmentElement">The <c>+=</c> assignment whose referenced rule is inspected.</param>
        /// <param name="umlClass">The class hosting the current rule (provides the UML cache).</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" />.</param>
        /// <returns>The fully-qualified inner-element type name, or <see langword="null" /> if the rule is not a thin wrapper.</returns>
        private static string TryResolveWrappedInnerTypeName(AssignmentElement assignmentElement, IClass umlClass, RuleGenerationContext ruleGenerationContext)
        {
            if (assignmentElement.Value is not NonTerminalElement nonTerminalElement)
            {
                return null;
            }

            var referencedRule = ruleGenerationContext.FindRule(nonTerminalElement.Name);

            if (referencedRule == null
                || !string.Equals(referencedRule.EffectiveTarget, "OwningMembership", StringComparison.Ordinal)
                || referencedRule.Alternatives.Count != 1)
            {
                return null;
            }

            var alternative = referencedRule.Alternatives[0];

            if (alternative.Elements.Count != 1
                || alternative.Elements[0] is not AssignmentElement innerAssignment
                || !string.Equals(innerAssignment.Property, "ownedRelatedElement", StringComparison.OrdinalIgnoreCase)
                || innerAssignment.Value is not NonTerminalElement innerNonTerminal)
            {
                return null;
            }

            var innerRule = ruleGenerationContext.FindRule(innerNonTerminal.Name);
            var innerTypeTarget = innerRule?.EffectiveTarget ?? innerNonTerminal.Name;

            if (string.IsNullOrWhiteSpace(innerTypeTarget))
            {
                return null;
            }

            var innerClass = RuleQueryUtilities.FindClass(umlClass.Cache, innerTypeTarget);
            return innerClass?.QueryFullyQualifiedTypeName();
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

                            if (iterator == null)
                            {
                                ifStatementContent.Add($"BuildGroupConditionFor{assigment.TextualNotationRule.RuleName}(poco)");
                            }
                            else
                            {
                                var consumptionAssignments = CollectCursorConsumptions(elements, ruleGenerationContext.CurrentSiblingElements, ruleGenerationContext.CurrentElementIndex, targetPropertyName);

                                if (consumptionAssignments.Count > 1)
                                {
                                    var conditionParts = new List<string>();

                                    for (var consumptionIndex = 0; consumptionIndex < consumptionAssignments.Count; consumptionIndex++)
                                    {
                                        var cursorAccess = consumptionIndex == 0
                                            ? $"{iterator.CursorVariableName}.Current"
                                            : $"{iterator.CursorVariableName}.GetNext({consumptionIndex})";

                                        var typeName = ResolveAssignmentTargetTypeName(consumptionAssignments[consumptionIndex], umlClass, ruleGenerationContext);

                                        conditionParts.Add(typeName == null
                                            ? $"{cursorAccess} != null"
                                            : $"{cursorAccess} is {typeName}");
                                    }

                                    ifStatementContent.Add(string.Join(" && ", conditionParts));
                                }
                                else
                                {
                                    // Guard on the TYPE the assignment consumes, not on mere cursor non-emptiness —
                                    // a bare non-null test also passes for the next UNRELATED relationship and emits
                                    // the group's terminals spuriously (e.g. AcceptParameterPart's `via`).
                                    var singleTypeName = ResolveAssignmentTargetTypeName(assigment, umlClass, ruleGenerationContext);

                                    ifStatementContent.Add(singleTypeName == null
                                        ? property.QueryIfStatementContentForNonEmpty(iterator.CursorVariableName)
                                        : $"{iterator.CursorVariableName}.Current is {singleTypeName}");
                                }
                            }
                        }
                        else
                        {
                            var condition = property.QueryIfStatementContentForNonEmpty("poco");

                            // For `Prop ?= 'literal'` in an optional group, exclude subtypes whose metamodel
                            // default already equals the trigger value — the keyword is redundant there
                            // (e.g. `attribute X`, not `ref attribute X`).
                            if (property.QueryIsBool())
                            {
                                var exclusionTypes = property.QuerySubclassesWithMatchingDefault(umlClass, "true");

                                if (exclusionTypes.Count > 0)
                                {
                                    condition += $" && poco is not ({string.Join(" or ", exclusionTypes.Select(c => c.QueryFullyQualifiedTypeName()))})";
                                }

                                // A DERIVED property cannot record whether the keyword was written (e.g.
                                // Usage::isReference = not isComposite is true in contexts with no `ref` at all),
                                // and the redundancy is per-INSTANCE — delegate to a hand-coded IsValidFor… guard.
                                if (property.IsDerived || property.IsDerivedUnion)
                                {
                                    condition += $" && poco.IsValidFor{ruleGenerationContext.NamedElementToGenerate?.Name}{property.Name.CapitalizeFirstLetter()}(writerContext)";
                                }
                            }

                            ifStatementContent.Add(condition);
                        }
                    }

                    writer.WriteSafeString(string.Join(" && ", ifStatementContent));
                    writer.WriteSafeString($"){Environment.NewLine}");
                    writer.WriteSafeString($"{{{Environment.NewLine}");

                    this.EmitElements(writer, umlClass, elements, ruleGenerationContext, restoreCallerPerElement: true);
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
                            var condition = GenerateInlineOptionalCondition(writer, referencedRule, umlClass, ruleGenerationContext, "poco");

                            if (condition != null)
                            {
                                inlineConditionParts.Add(condition);
                            }
                        }
                    }

                    var optionalCollectionCondition = TryResolveOptionalCollectionGroupCondition(umlClass, elements, ruleGenerationContext);

                    if (optionalCollectionCondition != null)
                    {
                        writer.WriteSafeString($"{Environment.NewLine}if ({optionalCollectionCondition}){Environment.NewLine}");
                    }
                    else if (inlineConditionParts.Count > 0)
                    {
                        writer.WriteSafeString($"{Environment.NewLine}if ({string.Join(" || ", inlineConditionParts)}){Environment.NewLine}");
                    }
                    else
                    {
                        writer.WriteSafeString($"{Environment.NewLine}if (BuildGroupConditionFor{alternative.TextualNotationRule.RuleName}(poco)){Environment.NewLine}");
                    }

                    writer.WriteSafeString($"{{{Environment.NewLine}");

                    this.EmitElements(writer, umlClass, elements, ruleGenerationContext);
                }

                if (!ruleGenerationContext.IsNextElementNewLineTerminal() && !ruleGenerationContext.IsLastElement())
                {
                    writer.WriteSafeString($"stringBuilder.Append(' ');{Environment.NewLine}");
                }

                writer.WriteSafeString($"}}{Environment.NewLine}");
            }
            else
            {
                this.EmitElements(writer, umlClass, elements, ruleGenerationContext, restoreCallerPerElement: true, isPartOfMultipleAlternative);
            }
        }

        /// <summary>
        /// Resolves the guard for an optional group whose only variable content is a <c>*</c>-quantified
        /// bare non-terminal — e.g. <c>( '{' ActionBodyItem* '}' )?</c>. Such a group must be emitted only
        /// when its loop would iterate at least once: the group's own terminals carry no information, so a
        /// property-based condition wrongly emits an empty <c>{ }</c> whenever any unrelated property is set.
        /// </summary>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="elements">The optional group's elements</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <returns>The cursor-based condition, or <see langword="null" /> when the group is not that shape.</returns>
        private static string TryResolveOptionalCollectionGroupCondition(IClass umlClass, List<RuleElement> elements, RuleGenerationContext ruleGenerationContext)
        {
            var nonTerminals = elements.OfType<NonTerminalElement>().ToList();

            if (nonTerminals.Count != 1 || !nonTerminals[0].IsCollection || elements.Any(element => element is AssignmentElement or GroupElement))
            {
                return null;
            }

            var referencedRule = ruleGenerationContext.FindRule(nonTerminals[0].Name);
            var collectionPropertyNames = referencedRule?.QueryCollectionPropertyNames(ruleGenerationContext.AllRules);

            if (collectionPropertyNames?.Count != 1)
            {
                return null;
            }

            var targetProperty = umlClass.QueryAllProperties().SingleOrDefault(x => string.Equals(x.Name, collectionPropertyNames.Single(), StringComparison.OrdinalIgnoreCase));

            if (targetProperty == null || !targetProperty.QueryIsEnumerable())
            {
                return null;
            }

            // The cursor is declared up-front by DeclareAllRequiredCursors; if it is absent this is not the
            // shape we handle, so fall back rather than emit a second declaration.
            var existingCursor = ruleGenerationContext.DefinedCursors.SingleOrDefault(x => x.IsCursorValidForProperty(targetProperty));

            if (existingCursor == null)
            {
                return null;
            }

            return IsGuardedBodyItemRule(nonTerminals[0].Name)
                ? $"{existingCursor.CursorVariableName}.Current is SysML2.NET.Core.POCO.Root.Elements.IRelationship optionalBodyCandidate && optionalBodyCandidate.IsValidFor{nonTerminals[0].Name}(writerContext)"
                : $"{existingCursor.CursorVariableName}.Current != null";
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
                ProcessMultiCollectionAssignment(writer, alternatives, ruleGenerationContext);
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
            if (this.TryEmitSubclassRuleDispatchAlternatives(writer, umlClass, alternatives, ruleGenerationContext))
            {
                return;
            }

            if (this.TryEmitSingleElementOrSameClassRuleAlternatives(writer, umlClass, alternatives, ruleGenerationContext))
            {
                return;
            }

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

                EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext, true);
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
                EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
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

            var nonTerminalCall = ResolveBuilderCall(umlClass, nonTerminalElement, nonTerminalTypeTarget, ruleGenerationContext);

            if (nonTerminalCall != null)
            {
                writer.WriteSafeString(nonTerminalCall);
            }
            else
            {
                this.ProcessReferencedRuleAlternatives(writer, umlClass, nonTerminalElement, nonTerminalReferencedRule, ruleGenerationContext);
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
                EmitTerminalOnlyAlternatives(writer, umlClass, alternatives, ruleGenerationContext);
                return;
            }

            // Detect pattern: property=[QualifiedName] | property=NonTerminal{containment+=property}
            if (alternatives.Count == 2 && TryEmitQualifiedNameOrChainAlternatives(writer, umlClass, alternatives, ruleGenerationContext))
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

                if (TryHandlePocoTypeDispatchWithCompoundAlternatives(writer, umlClass, alternatives, ruleGenerationContext))
                {
                    return;
                }

                if (this.TryHandleReferenceOrInline(writer, umlClass, alternatives, ruleGenerationContext))
                {
                    return;
                }

                var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;

                EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext, true);
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
        private static void EmitTerminalOnlyAlternatives(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
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

                    foreach (var alternativeElements in alternatives.Select(x => x.Elements))
                    {
                        var nonParsingAssignment = alternativeElements.OfType<NonParsingAssignmentElement>().Single();
                        var terminals = alternativeElements.OfType<TerminalElement>().ToList();
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
        private static bool TryEmitQualifiedNameOrChainAlternatives(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
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
            writer.WriteSafeString($"SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder,{variableName}.{resolvedPropertyName}, writerContext, poco);{Environment.NewLine}");

            // Both alternatives denote the same notational prefix, so when the chain rule ends in a
            // terminal (FeatureChainPrefix's trailing '.') the [QualifiedName] branch must emit it too.
            // The kebnf omits that '.' on the reference alternative of FlowEndSubsetting; the pilot's
            // Xtext grammar spells it out on both, and the kebnf is immutable, so it is recovered here.
            var chainTrailingTerminal = QueryTrailingTerminal(referencedRule);

            if (chainTrailingTerminal == null)
            {
                writer.WriteSafeString($"stringBuilder.Append(' ');{Environment.NewLine}");
            }
            else
            {
                TerminalWriter.WriteTerminalAppend(writer, chainTrailingTerminal);
                writer.WriteSafeString(Environment.NewLine);
            }

            writer.WriteSafeString($"}}{Environment.NewLine}");

            return true;
        }

        /// <summary>
        /// Returns the terminal value <paramref name="rule" /> ends with, or <see langword="null" /> when
        /// it has multiple alternatives or does not end in a terminal.
        /// </summary>
        /// <param name="rule">The referenced <see cref="TextualNotationRule" />; may be <see langword="null" />.</param>
        /// <returns>The trailing terminal's value, or <see langword="null" />.</returns>
        private static string QueryTrailingTerminal(TextualNotationRule rule)
        {
            if (rule == null || rule.Alternatives.Count != 1)
            {
                return null;
            }

            var elements = rule.Alternatives[0].Elements;

            return elements.Count > 0 && elements[^1] is TerminalElement trailingTerminal
                ? trailingTerminal.Value
                : null;
        }

        /// <summary>
        /// Attempts the subclass-rule dispatch pattern <c>property = X | SubclassRule</c>, where
        /// <c>SubclassRule</c> targets a strict specialization of the current metaclass (e.g.
        /// <c>FeatureChainMember</c>). Dispatches on the runtime subtype FIRST — grammar order would put
        /// a never-null derived-property check in front and make the subclass alternative unreachable.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="alternatives">The grammar alternatives to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <returns><c>true</c> if the pattern matched and code was emitted; <c>false</c> otherwise</returns>
        private bool TryEmitSubclassRuleDispatchAlternatives(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
        {
            if (alternatives.Count != 2)
            {
                return false;
            }

            var assignmentAlt = alternatives.FirstOrDefault(alt =>
                alt.Elements.Count == 1
                && alt.Elements[0] is AssignmentElement { Operator: "=" });

            var subclassRuleAlt = alternatives.FirstOrDefault(alt =>
                alt.Elements.Count == 1
                && alt.Elements[0] is NonTerminalElement);

            if (assignmentAlt == null || subclassRuleAlt == null)
            {
                return false;
            }

            var assignmentElement = (AssignmentElement)assignmentAlt.Elements[0];
            var nonTerminalElement = (NonTerminalElement)subclassRuleAlt.Elements[0];

            var targetProperty = umlClass.QueryAllProperties().SingleOrDefault(x =>
                string.Equals(x.Name, assignmentElement.Property, StringComparison.OrdinalIgnoreCase));

            if (targetProperty == null || targetProperty.QueryIsEnumerable())
            {
                return false;
            }

            var referencedRule = ruleGenerationContext.FindRule(nonTerminalElement.Name);

            if (referencedRule == null)
            {
                return false;
            }

            var subclass = RuleQueryUtilities.FindClass(umlClass.Cache, referencedRule.EffectiveTarget);

            if (subclass == null || subclass == umlClass || !subclass.QueryAllGeneralClassifiers().Contains(umlClass))
            {
                return false;
            }

            var variableName = ruleGenerationContext.CurrentVariableName ?? "poco";
            var subclassTypeName = subclass.QueryFullyQualifiedTypeName();
            var patternVariableName = $"{subclass.Name.LowerCaseFirstLetter()}{ruleGenerationContext.NarrowedTypeCheckCounter}";
            ruleGenerationContext.NarrowedTypeCheckCounter++;

            writer.WriteSafeString($"if ({variableName} is {subclassTypeName} {patternVariableName}){Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");
            writer.WriteSafeString($"{subclass.Name}TextualNotationBuilder.Build{nonTerminalElement.Name}({patternVariableName}, writerContext, stringBuilder);{Environment.NewLine}");
            writer.WriteSafeString($"}}{Environment.NewLine}");

            // A NonTerminal-valued assignment emits its own null guard inside ProcessAssignmentElement;
            // only a value-literal assignment (e.g. [QualifiedName]) needs the guard supplied here.
            if (assignmentElement.Value is ValueLiteralElement)
            {
                writer.WriteSafeString($"else if ({targetProperty.QueryIfStatementContentForNonEmpty(variableName)}){Environment.NewLine}");
            }
            else
            {
                writer.WriteSafeString($"else{Environment.NewLine}");
            }

            writer.WriteSafeString($"{{{Environment.NewLine}");
            this.ProcessAssignmentElement(writer, umlClass, ruleGenerationContext, assignmentElement, true);
            writer.WriteSafeString($"{Environment.NewLine}}}{Environment.NewLine}");

            return true;
        }

        /// <summary>
        /// Attempts the pattern <c>collection += X | SameClassRule</c>, where the same-class rule
        /// re-consumes the same collection (e.g. <c>ChainingPart</c> vs <c>FeatureChain</c>). The
        /// discriminator is the element COUNT: exactly one match selects the single <c>+=</c> alternative
        /// (with its <c>Move()</c>); otherwise the same-class rule manages the shared cursor itself.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="alternatives">The grammar alternatives to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <returns><c>true</c> if the pattern matched and code was emitted; <c>false</c> otherwise</returns>
        private bool TryEmitSingleElementOrSameClassRuleAlternatives(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
        {
            if (alternatives.Count != 2)
            {
                return false;
            }

            var collectionAlt = alternatives.FirstOrDefault(alt =>
                alt.Elements.Count == 1
                && alt.Elements[0] is AssignmentElement { Operator: "+=", Value: NonTerminalElement });

            var sameClassRuleAlt = alternatives.FirstOrDefault(alt =>
                alt.Elements.Count == 1
                && alt.Elements[0] is NonTerminalElement);

            if (collectionAlt == null || sameClassRuleAlt == null)
            {
                return false;
            }

            var assignmentElement = (AssignmentElement)collectionAlt.Elements[0];
            var nonTerminalElement = (NonTerminalElement)sameClassRuleAlt.Elements[0];

            var referencedRule = ruleGenerationContext.FindRule(nonTerminalElement.Name);

            if (referencedRule == null || !string.Equals(referencedRule.EffectiveTarget, umlClass.Name, StringComparison.Ordinal))
            {
                return false;
            }

            // Only when the same-class rule re-consumes the same property THROUGH THE SAME sub-rule do
            // the alternatives compete for one element type and need the count discriminator.
            var elementValueNonTerminal = (NonTerminalElement)assignmentElement.Value;

            var reconsumesSameElements = referencedRule.Alternatives
                .SelectMany(alt => alt.Elements)
                .OfType<AssignmentElement>()
                .Any(innerAssignment => innerAssignment is { Operator: "+=", Value: NonTerminalElement innerNonTerminal }
                    && string.Equals(innerAssignment.Property, assignmentElement.Property, StringComparison.OrdinalIgnoreCase)
                    && string.Equals(innerNonTerminal.Name, elementValueNonTerminal.Name, StringComparison.Ordinal));

            if (!reconsumesSameElements)
            {
                return false;
            }

            var targetProperty = umlClass.QueryAllProperties().SingleOrDefault(x =>
                string.Equals(x.Name, assignmentElement.Property, StringComparison.OrdinalIgnoreCase));

            if (targetProperty == null || !targetProperty.QueryIsEnumerable())
            {
                return false;
            }

            var elementTypeName = ResolveAssignmentTargetTypeName(assignmentElement, umlClass, ruleGenerationContext);
            var elementRule = ruleGenerationContext.FindRule(elementValueNonTerminal.Name);
            var elementTypeTarget = elementRule?.EffectiveTarget;
            var sameClassRuleCall = ResolveBuilderCall(umlClass, nonTerminalElement, referencedRule.EffectiveTarget, ruleGenerationContext);

            if (elementTypeName == null || elementTypeTarget == null || sameClassRuleCall == null)
            {
                return false;
            }

            this.DeclareAllRequiredCursors(writer, umlClass, collectionAlt, ruleGenerationContext);
            var cursor = ruleGenerationContext.DefinedCursors.Single(x => x.ApplicableRuleElements.Contains(assignmentElement));

            var variableName = ruleGenerationContext.CurrentVariableName ?? "poco";
            var propertyAccessor = targetProperty.QueryPropertyNameBasedOnUmlProperties();
            var elementVariableName = $"elementAs{elementTypeTarget}";

            var singleElementBuilderCall = elementTypeTarget == ruleGenerationContext.NamedElementToGenerate.Name
                ? $"Build{elementValueNonTerminal.Name}({elementVariableName}, writerContext, stringBuilder);"
                : $"{elementTypeTarget}TextualNotationBuilder.Build{elementValueNonTerminal.Name}({elementVariableName}, writerContext, stringBuilder);";

            writer.WriteSafeString($"if ({variableName}.{propertyAccessor}.OfType<{elementTypeName}>().Count() == 1 && {cursor.CursorVariableName}.Current is {elementTypeName} {elementVariableName}){Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");
            writer.WriteSafeString($"{singleElementBuilderCall}{Environment.NewLine}");
            writer.WriteSafeString($"{cursor.CursorVariableName}.Move();{Environment.NewLine}");
            writer.WriteSafeString($"}}{Environment.NewLine}");
            writer.WriteSafeString($"else{Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");
            writer.WriteSafeString($"{sameClassRuleCall}{Environment.NewLine}");
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
                        EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
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
                EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
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
                EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
                return;
            }

            var collectionPropertyNames = nonTerminalRule.QueryCollectionPropertyNames(ruleGenerationContext.AllRules);

            if (collectionPropertyNames.Count == 0)
            {
                var handCodedRuleName = firstAlt.TextualNotationRule.RuleName;
                EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
                return;
            }

            var collectionPropertyName = collectionPropertyNames.First();
            var targetProperty = umlClass.QueryAllProperties().SingleOrDefault(x => string.Equals(x.Name, collectionPropertyName, StringComparison.OrdinalIgnoreCase));
            var terminalValue = ((TerminalElement)firstAlt.Elements[0]).Value;

            if (targetProperty == null)
            {
                var handCodedRuleName = firstAlt.TextualNotationRule.RuleName;
                EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
                return;
            }

            var propertyAccessName = targetProperty.QueryPropertyNameBasedOnUmlProperties();

            // For body item rules that can encounter elements legitimately belonging to a parent rule
            // (e.g. PortDefinition's trailing ConjugatedPortDefinitionMember), the `;` choice and the
            // `*` loop must defer to an IsValidFor{XBodyItem} predicate instead of a bare non-null test.
            var requiresIsValidForGuard = IsGuardedBodyItemRule(collectionNonTerminals[0].Name);
            var guardCallSuffix = requiresIsValidForGuard
                ? $".IsValidFor{collectionNonTerminals[0].Name}(writerContext)"
                : string.Empty;

            if (requiresIsValidForGuard)
            {
                writer.WriteSafeString($"if (writerContext.CursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{propertyAccessName}).Current is not SysML2.NET.Core.POCO.Root.Elements.IRelationship emptyBodyCandidate || !emptyBodyCandidate{guardCallSuffix}){Environment.NewLine}");
            }
            else
            {
                writer.WriteSafeString($"if(writerContext.CursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{propertyAccessName}).Current == null){Environment.NewLine}");
            }

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

                    var perItemCall = ResolveBuilderCall(umlClass, collectionNonTerminal, typeTarget, ruleGenerationContext);

                    if (requiresIsValidForGuard)
                    {
                        writer.WriteSafeString($"while ({cursorVarName}.Current is SysML2.NET.Core.POCO.Root.Elements.IRelationship loopBodyItem && loopBodyItem{guardCallSuffix}){Environment.NewLine}");
                    }
                    else
                    {
                        writer.WriteSafeString($"while ({cursorVarName}.Current != null){Environment.NewLine}");
                    }

                    writer.WriteSafeString($"{{{Environment.NewLine}");

                    var positionVariableName = EmitLoopProgressCapture(writer, cursorVarName, ruleGenerationContext);

                    if (perItemCall != null)
                    {
                        writer.WriteSafeString(perItemCall);
                    }
                    else
                    {
                        this.ProcessReferencedRuleAlternatives(writer, umlClass, collectionNonTerminal, referencedRule, ruleGenerationContext);
                    }

                    writer.WriteSafeString(Environment.NewLine);
                    EmitLoopProgressAssertion(writer, cursorVarName, positionVariableName, collectionNonTerminal.Name);
                    writer.WriteSafeString($"}}{Environment.NewLine}");
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
                EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
                return;
            }

            var collectionPropertyNames = nonTerminalRule.QueryCollectionPropertyNames(ruleGenerationContext.AllRules);

            if (collectionPropertyNames.Count == 0)
            {
                var handCodedRuleName = firstAlt.TextualNotationRule.RuleName;
                EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
                return;
            }

            var collectionPropertyName = collectionPropertyNames.First();
            var targetProperty = umlClass.QueryAllProperties().SingleOrDefault(x => string.Equals(x.Name, collectionPropertyName, StringComparison.OrdinalIgnoreCase));
            var terminalValue = ((TerminalElement)firstAlt.Elements[0]).Value;

            if (targetProperty == null)
            {
                var handCodedRuleName = firstAlt.TextualNotationRule.RuleName;
                EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
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

                    var perItemCall = ResolveBuilderCall(umlClass, singleNonTerminal, typeTarget, ruleGenerationContext);

                    writer.WriteSafeString($"if ({cursorVarName}.Current != null){Environment.NewLine}");
                    writer.WriteSafeString($"{{{Environment.NewLine}");

                    if (perItemCall != null)
                    {
                        writer.WriteSafeString(perItemCall);
                    }
                    else
                    {
                        this.ProcessReferencedRuleAlternatives(writer, umlClass, singleNonTerminal, referencedRule, ruleGenerationContext);
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
        /// Returns true when the body-item rule can have cursor elements that legitimately belong to a
        /// parent rule and must not be consumed by the body's <c>*</c> loop, so the loop must be bounded
        /// by an <c>IsValidFor{Rule}</c> predicate instead of a bare null-test.
        /// </summary>
        /// <remarks>
        /// Allowlisted by name, deliberately, because the four entries encode TWO unrelated concerns and no
        /// single predicate can derive both:
        /// <list type="table">
        /// <item>
        /// <term><c>CaseBodyItem</c>, <c>DefinitionBodyItem</c></term>
        /// <description>A trailing consumer reads the SAME cursor after the loop, so an unguarded loop
        /// swallows it — <c>CaseBody</c>'s own <c>( ownedRelationship += ResultExpressionMember )?</c>, and
        /// <c>PortDefinition</c>'s trailing <c>ConjugatedPortDefinitionMember</c> reached through
        /// <c>Definition → DefinitionBody</c>. This is a property OF THE GRAMMAR and
        /// <see cref="GuardedBodyItemRuleAnalysis" /> derives it.</description>
        /// </item>
        /// <item>
        /// <term><c>InterfaceBodyItem</c>, <c>ActionBodyItem</c></term>
        /// <description>The item builder declines an element it cannot render WITHOUT advancing the cursor
        /// — <c>SharedTextualNotationBuilder</c>'s <c>default:</c> arm, and <c>ActionBodyItem</c>'s outer
        /// <c>if (IsValidForActionBodyItem)</c>. The guard is what keeps such an element from ever reaching
        /// the dispatcher. <c>InterfaceBody</c> is the clean witness that this is NOT the grammar concern:
        /// it is the last element of both <c>InterfaceDefinition</c> and <c>InterfaceUsage</c>, so no
        /// trailing consumer exists, yet the guard is still load-bearing.</description>
        /// </item>
        /// </list>
        /// <para>The second concern is not derivable. It depends on the internal control flow of a
        /// hand-written method: <c>BuildStateBodyItemHandCoded</c> is equally hand-coded yet DRAINS the
        /// cursor in its own <c>while</c>, so it can never stall its caller — a "the item builder is
        /// hand-coded" heuristic would over-guard it. Deciding it would mean analysing that C#.</para>
        /// <para>What removes the risk instead is <c>CollectionCursor.AssertAdvancedSince</c>, emitted by
        /// <see cref="EmitLoopProgressAssertion" /> at the foot of every generated cursor loop: a stalled
        /// iteration now throws immediately instead of hanging. That matters because a hang is invisible to
        /// a corpus that compares output — dropping <c>InterfaceBodyItem</c> from this list once produced a
        /// fully green 33-case run. With the assertion in place this allowlist governs OUTPUT CORRECTNESS
        /// (do not swallow the result expression, do not emit a bare <c>ref;</c>) rather than termination.</para>
        /// </remarks>
        /// <summary>
        /// Emits the capture of a cursor's position immediately before a loop body, and returns the name of
        /// the local it wrote to. Pair with <see cref="EmitLoopProgressAssertion" /> at the end of the body.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> to emit to</param>
        /// <param name="cursorVariableName">The cursor driving the loop</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <returns>The name of the emitted position local</returns>
        private static string EmitLoopProgressCapture(EncodedTextWriter writer, string cursorVariableName, RuleGenerationContext ruleGenerationContext)
        {
            var positionVariableName = $"positionBeforeItem{ruleGenerationContext.LoopProgressCheckCounter++}";

            writer.WriteSafeString($"var {positionVariableName} = {cursorVariableName}.Position;{Environment.NewLine}");

            return positionVariableName;
        }

        /// <summary>
        /// Emits the forward-progress assertion closing a cursor loop body, so an iteration that consumes
        /// nothing fails immediately instead of spinning forever.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> to emit to</param>
        /// <param name="cursorVariableName">The cursor driving the loop</param>
        /// <param name="positionVariableName">The local returned by <see cref="EmitLoopProgressCapture" /></param>
        /// <param name="ruleName">The KEBNF rule the loop body builds, named in the failure message</param>
        private static void EmitLoopProgressAssertion(EncodedTextWriter writer, string cursorVariableName, string positionVariableName, string ruleName)
        {
            writer.WriteSafeString($"{cursorVariableName}.AssertAdvancedSince({positionVariableName}, \"{ruleName}\");{Environment.NewLine}");
        }

        /// <param name="bodyItemRuleName">The KEBNF rule name of the body item (e.g. <c>DefinitionBodyItem</c>)</param>
        /// <returns><c>true</c> if the codegen should emit the guarded form</returns>
        private static bool IsGuardedBodyItemRule(string bodyItemRuleName)
        {
            return string.Equals(bodyItemRuleName, "DefinitionBodyItem", StringComparison.Ordinal)
                || string.Equals(bodyItemRuleName, "InterfaceBodyItem", StringComparison.Ordinal)
                || string.Equals(bodyItemRuleName, "ActionBodyItem", StringComparison.Ordinal)
                || string.Equals(bodyItemRuleName, "CaseBodyItem", StringComparison.Ordinal);
        }

        /// <summary>
        /// Returns true when an alternative's discriminator cannot be derived from its rule body and must
        /// be supplied by a hand-coded <c>IsValidFor{Rule}</c> guard.
        /// </summary>
        /// <remarks>
        /// Currently <c>FunctionOperationExpression</c>. Its arm in <c>NonFeatureChainPrimaryExpression</c>
        /// targets <c>InvocationExpression</c> and sits above the <c>SequenceExpression</c> arm, which
        /// targets the supertype <c>Expression</c>. A sequence <c>(a, b, c)</c> is an <c>OperatorExpression</c>
        /// with <c>operator = ","</c> — hence an <c>InvocationExpression</c> whose first owned relationship is
        /// an <c>IParameterMembership</c>, which is all the unguarded arm tested — so every sequence was
        /// swallowed and rendered with a spurious <c>-&gt;</c>. Telling the two apart needs the SECOND owned
        /// relationship (<c>Membership</c> for <c>x-&gt;f()</c>, <c>ParameterMembership</c> for a sequence),
        /// i.e. cursor lookahead, which no body-shape analysis can produce.
        /// </remarks>
        /// <param name="alternativeRuleName">The KEBNF rule name of the alternative</param>
        /// <returns><c>true</c> if the codegen should emit a hand-coded <c>IsValidFor{Rule}</c> guard</returns>
        private static bool RequiresHandCodedAlternativeGuard(string alternativeRuleName)
        {
            return string.Equals(alternativeRuleName, "FunctionOperationExpression", StringComparison.Ordinal);
        }

        /// <summary>
        /// Returns true when a collection loop's CONTENT guard cannot be derived from the referenced rule's
        /// body shape and must be supplied by a hand-coded <c>IsValidFor{Rule}</c> guard.
        /// </summary>
        /// <remarks>
        /// Currently <c>PrefixMetadataMember</c>. The synthesised guard tests only the shape the rule states
        /// — an <c>OwningMembership</c> whose <c>ownedRelatedElement</c> contains a <c>MetadataUsage</c> — but
        /// what makes the prefix form applicable is what the usage does NOT own. <c>PrefixMetadataUsage :
        /// MetadataUsage = ownedRelationship += OwnedFeatureTyping</c> has no <c>MetadataUsageDeclaration</c>
        /// and no <c>MetadataBody</c>, so a usage carrying a body (<c>@Safety { ref :&gt;&gt; isMandatory =
        /// false; }</c>) cannot be written with <c>#</c> and must fall through to the body form. Body-shape
        /// analysis cannot express an ABSENCE constraint on the referenced element's own contents, so the
        /// predicate is hand-coded.
        /// </remarks>
        /// <param name="contentRuleName">The KEBNF rule name supplying the loop's content</param>
        /// <returns><c>true</c> if the codegen should emit a hand-coded <c>IsValidFor{Rule}</c> guard</returns>
        private static bool RequiresHandCodedContentGuard(string contentRuleName)
        {
            return string.Equals(contentRuleName, "PrefixMetadataMember", StringComparison.Ordinal);
        }
    }
}
