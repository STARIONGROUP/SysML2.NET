// -------------------------------------------------------------------------------------------------
// <copyright file="RuleProcessor.ElementProcessing.cs" company="Starion Group S.A.">
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
    using System.Linq;

    using HandlebarsDotNet;

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.Grammar.Model;

    using uml4net.Extensions;
    using uml4net.StructuredClassifiers;

    /// <summary>
    /// Element-level processing methods for the rule processor
    /// </summary>
    internal sealed partial class RuleProcessor
    {
        /// <summary>
        /// Processes a <see cref="RuleElement" />
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="textualRuleElement">The <see cref="RuleElement" /> to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <param name="isPartOfMultipleAlternative">Whether the element is part of a multiple alternative</param>
        /// <exception cref="ArgumentException">If the type of the <see cref="RuleElement" /> is not supported</exception>
        internal void ProcessRuleElement(EncodedTextWriter writer, IClass umlClass, RuleElement textualRuleElement, RuleGenerationContext ruleGenerationContext, bool isPartOfMultipleAlternative = false)
        {
            switch (textualRuleElement)
            {
                case TerminalElement terminalElement:
                    TerminalWriter.WriteTerminalAppend(writer, terminalElement.Value);
                    break;
                case NonTerminalElement nonTerminalElement:
                    if (ruleGenerationContext.CallerRule is NonTerminalElement { Container: AssignmentElement assignmentElementContainer })
                    {
                        var textualBuilderClass = ruleGenerationContext.NamedElementToGenerate as IClass;
                        var assignedProperty = textualBuilderClass.QueryAllProperties().SingleOrDefault(x => x.Name == assignmentElementContainer.Property);
                        ruleGenerationContext.CurrentVariableName = assignedProperty == null ? "poco" : $"poco.{assignedProperty.QueryPropertyNameBasedOnUmlProperties()}";
                    }
                    else
                    {
                        ruleGenerationContext.CurrentVariableName = "poco";
                    }

                    this.ProcessNonTerminalElement(writer, umlClass, nonTerminalElement, ruleGenerationContext, isPartOfMultipleAlternative);

                    break;
                case GroupElement groupElement:
                    ruleGenerationContext.CallerRule = groupElement;

                    if (groupElement.IsCollection && groupElement.Alternatives.Count == 1)
                    {
                        var assignmentRule = groupElement.Alternatives.SelectMany(x => x.Elements).FirstOrDefault(x => x is AssignmentElement { Value: NonTerminalElement } || x is AssignmentElement { Value: ValueLiteralElement });

                        if (assignmentRule is AssignmentElement assignmentElement)
                        {
                            var cursorToUse = ruleGenerationContext.DefinedCursors.Single(x => x.ApplicableRuleElements.Contains(assignmentElement));

                            var groupTypeGuard = "";

                            if (assignmentElement.Value is NonTerminalElement valueNonTerminal)
                            {
                                var referencedRule = ruleGenerationContext.FindRule(valueNonTerminal.Name);
                                var typeTarget = referencedRule != null ? referencedRule.EffectiveTarget : null;

                                if (typeTarget != null)
                                {
                                    var targetClass = RuleQueryUtilities.FindClass(umlClass.Cache, typeTarget);

                                    if (targetClass != null)
                                    {
                                        var contentGuard = this.ResolveContentTypeGuard(cursorToUse.CursorVariableName, referencedRule, assignmentElement.Property, umlClass, ruleGenerationContext);

                                        if (!string.IsNullOrWhiteSpace(contentGuard))
                                        {
                                            groupTypeGuard = $"__FULL_GUARD__{contentGuard}";
                                        }
                                        else
                                        {
                                            groupTypeGuard = $" && {cursorToUse.CursorVariableName}.Current is {targetClass.QueryFullyQualifiedTypeName()}";
                                        }
                                    }
                                }
                            }

                            // A repeated group followed by a MANDATORY consumption of the same element type
                            // from the same cursor — the `( X )+ X` shape of e.g.
                            // FeatureChainPrefix = ( ownedRelationship += OwnedFeatureChaining '.' )+
                            //                        ownedRelationship += OwnedFeatureChaining '.'
                            // — must leave one element for that trailing consumption. Without the
                            // reservation the loop eats every element and the mandatory tail emits its
                            // terminals against an exhausted cursor (`a.b.` became `a.b..`).
                            var reservationGuard = this.ResolveTrailingConsumptionReservation(cursorToUse, umlClass, ruleGenerationContext);

                            if (groupTypeGuard.StartsWith("__FULL_GUARD__"))
                            {
                                var fullGuard = groupTypeGuard.Substring("__FULL_GUARD__".Length);
                                writer.WriteSafeString($"{Environment.NewLine}while({fullGuard}{reservationGuard}){Environment.NewLine}");
                            }
                            else
                            {
                                writer.WriteSafeString($"{Environment.NewLine}while({cursorToUse.CursorVariableName}.Current != null{groupTypeGuard}{reservationGuard}){Environment.NewLine}");
                            }
                        }

                        writer.WriteSafeString($"{{{Environment.NewLine}");
                        this.ProcessAlternatives(writer, umlClass, groupElement.Alternatives, ruleGenerationContext);

                        if (assignmentRule is AssignmentElement assignmentElementForMove)
                        {
                            var cursorToUse = ruleGenerationContext.DefinedCursors.Single(x => x.ApplicableRuleElements.Contains(assignmentElementForMove));
                            writer.WriteSafeString($"{cursorToUse.CursorVariableName}.Move();{Environment.NewLine}");
                        }

                        writer.WriteSafeString($"{Environment.NewLine}}}");
                    }
                    else if (groupElement.IsCollection)
                    {
                        var groupAssignments = groupElement.Alternatives
                            .SelectMany(alternative => alternative.Elements)
                            .OfType<AssignmentElement>()
                            .Where(assignment => assignment.Operator == "+=")
                            .ToList();

                        var groupNonTerminals = groupAssignments
                            .Select(assignment => assignment.Value)
                            .OfType<NonTerminalElement>()
                            .ToList();

                        if (groupAssignments.Count > 0 && groupNonTerminals.Count == groupAssignments.Count)
                        {
                            var groupPropertyName = groupAssignments[0].Property;
                            var groupTargetProperty = umlClass.QueryAllProperties().SingleOrDefault(x => string.Equals(x.Name, groupPropertyName, StringComparison.OrdinalIgnoreCase));

                            if (groupTargetProperty != null)
                            {
                                var groupCursorVarName = $"{groupTargetProperty.Name.LowerCaseFirstLetter()}Cursor";
                                var existingGroupCursor = ruleGenerationContext.DefinedCursors.FirstOrDefault(x => x.IsCursorValidForProperty(groupTargetProperty));

                                if (existingGroupCursor == null)
                                {
                                    var groupPropertyAccessName = groupTargetProperty.QueryPropertyNameBasedOnUmlProperties();
                                    writer.WriteSafeString($"var {groupCursorVarName} = writerContext.CursorCache.GetOrCreateCursor(poco.Id, \"{groupTargetProperty.Name}\", poco.{groupPropertyAccessName});{Environment.NewLine}");
                                    var groupCursorDef = new CursorDefinition { DefinedForProperty = groupTargetProperty };

                                    foreach (var groupAssignment in groupAssignments)
                                    {
                                        groupCursorDef.ApplicableRuleElements.Add(groupAssignment);
                                    }

                                    ruleGenerationContext.DefinedCursors.Add(groupCursorDef);
                                }
                                else
                                {
                                    groupCursorVarName = existingGroupCursor.CursorVariableName;
                                }

                                var groupOrderedElements = RuleQueryUtilities.OrderElementsByInheritance(groupNonTerminals, umlClass.Cache, ruleGenerationContext);

                                writer.WriteSafeString($"while ({groupCursorVarName}.Current != null){Environment.NewLine}");
                                writer.WriteSafeString($"{{{Environment.NewLine}");
                                writer.WriteSafeString($"switch ({groupCursorVarName}.Current){Environment.NewLine}");
                                writer.WriteSafeString($"{{{Environment.NewLine}");

                                foreach (var groupOrderedElement in groupOrderedElements)
                                {
                                    var groupCaseVarName = groupOrderedElement.UmlClass.Name.LowerCaseFirstLetter();
                                    writer.WriteSafeString($"case {groupOrderedElement.UmlClass.QueryFullyQualifiedTypeName()} {groupCaseVarName}:{Environment.NewLine}");

                                    var previousVariableName = ruleGenerationContext.CurrentVariableName;
                                    var previousCaller = ruleGenerationContext.CallerRule;
                                    ruleGenerationContext.CurrentVariableName = groupCaseVarName;
                                    ruleGenerationContext.CallerRule = groupOrderedElement.RuleElement;
                                    this.ProcessNonTerminalElement(writer, groupOrderedElement.UmlClass, groupOrderedElement.RuleElement, ruleGenerationContext);
                                    ruleGenerationContext.CurrentVariableName = previousVariableName;
                                    ruleGenerationContext.CallerRule = previousCaller;

                                    writer.WriteSafeString($"{Environment.NewLine}break;{Environment.NewLine}");
                                }

                                writer.WriteSafeString($"}}{Environment.NewLine}");
                                writer.WriteSafeString($"{groupCursorVarName}.Move();{Environment.NewLine}");
                                writer.WriteSafeString($"}}{Environment.NewLine}");
                            }
                            else
                            {
                                var handCodedRuleName = groupElement.TextualNotationRule?.RuleName ?? "Unknown";
                                EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
                            }
                        }
                        else
                        {
                            var handCodedRuleName = groupElement.TextualNotationRule?.RuleName ?? "Unknown";
                            EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
                        }
                    }
                    else
                    {
                        this.ProcessAlternatives(writer, umlClass, groupElement.Alternatives, ruleGenerationContext);
                    }

                    if (!groupElement.IsOptional && !ruleGenerationContext.IsNextElementNewLineTerminal() && !ruleGenerationContext.IsLastElement())
                    {
                        writer.WriteSafeString($"{Environment.NewLine}stringBuilder.Append(' ');");
                    }

                    break;
                case AssignmentElement assignmentElement:
                    this.ProcessAssignmentElement(writer, umlClass, ruleGenerationContext, assignmentElement, isPartOfMultipleAlternative);
                    break;
                case NonParsingAssignmentElement nonParsingAssignmentElement:
                    writer.WriteSafeString($"// NonParsing Assignment Element : {nonParsingAssignmentElement.PropertyName} {nonParsingAssignmentElement.Operator} {nonParsingAssignmentElement.Value} => Does not have to be process");
                    break;
                case ValueLiteralElement valueLiteralElement:
                    if (valueLiteralElement.QueryIsQualifiedName())
                    {
                        writer.WriteSafeString($"SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder,{ruleGenerationContext.CurrentVariableName}, writerContext, poco);{Environment.NewLine}");

                        if (!ruleGenerationContext.IsNextElementNewLineTerminal())
                        {
                            writer.WriteSafeString("stringBuilder.Append(' ');");
                        }
                    }
                    else
                    {
                        var handCodedRuleName = textualRuleElement.TextualNotationRule?.RuleName ?? "Unknown";
                        EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
                    }

                    break;
                default:
                    throw new ArgumentException("Unknown element type");
            }

            writer.WriteSafeString(Environment.NewLine);
        }

        /// <summary>
        /// Builds the extra <c>while</c> clause that reserves one element for a MANDATORY consumption
        /// following a repeated group, or an empty string when no reservation is needed.
        /// <para>KEBNF rules of the shape <c>( prop += X )+ prop += X</c> — <c>FeatureChainPrefix</c> being
        /// the canonical case — consume from a single shared cursor. Emitted naively the loop is greedy: it
        /// takes every element, and the mandatory trailing assignment then emits its terminals with nothing
        /// left to consume, duplicating them. The guard <c>cursor.GetNext(1) is T</c> stops the loop one
        /// element short, which is exactly the arity the grammar asks for.</para>
        /// <para>Applies only when the element immediately following the group in the SAME alternative is a
        /// non-optional <c>+=</c> assignment drawing on the same cursor and the same sub-rule; any other
        /// successor consumes different elements and needs no reservation.</para>
        /// </summary>
        /// <param name="cursorDefinition">The cursor the group consumes from.</param>
        /// <param name="umlClass">The class hosting the current rule (provides the UML cache).</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" />.</param>
        /// <returns>The additional guard clause, or an empty string.</returns>
        private string ResolveTrailingConsumptionReservation(CursorDefinition cursorDefinition, IClass umlClass, RuleGenerationContext ruleGenerationContext)
        {
            var siblings = ruleGenerationContext.CurrentSiblingElements;

            if (siblings == null || ruleGenerationContext.CurrentElementIndex + 1 >= siblings.Count)
            {
                return string.Empty;
            }

            if (siblings[ruleGenerationContext.CurrentElementIndex + 1] is not AssignmentElement { Operator: "+=", IsOptional: false, IsCollection: false } trailingAssignment
                || !cursorDefinition.IsCursorValidForProperty(cursorDefinition.DefinedForProperty)
                || !string.Equals(trailingAssignment.Property, cursorDefinition.DefinedForProperty.Name, StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            var trailingTypeName = ResolveAssignmentTargetTypeName(trailingAssignment, umlClass, ruleGenerationContext);

            return trailingTypeName == null
                ? string.Empty
                : $" && {cursorDefinition.CursorVariableName}.GetNext(1) is {trailingTypeName}";
        }

        /// <summary>
        /// Processes an <see cref="AssignmentElement" />
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <param name="assignmentElement">The <see cref="AssignmentElement" /> to process</param>
        /// <param name="isPartOfMultipleAlternative">Whether this is part of a multiple alternative</param>
        internal void ProcessAssignmentElement(EncodedTextWriter writer, IClass umlClass, RuleGenerationContext ruleGenerationContext, AssignmentElement assignmentElement, bool isPartOfMultipleAlternative = false)
        {
            var properties = umlClass.QueryAllProperties();
            var targetProperty = properties.SingleOrDefault(x => string.Equals(x.Name, assignmentElement.Property, StringComparison.OrdinalIgnoreCase));

            if (targetProperty != null)
            {
                if (targetProperty.QueryIsEnumerable())
                {
                    if (assignmentElement.Value is NonTerminalElement nonTerminalElement)
                    {
                        var cursorToUse = ruleGenerationContext.DefinedCursors.Single(x => x.ApplicableRuleElements.Contains(assignmentElement));
                        var usedVariable = $"{cursorToUse.CursorVariableName}.Current";

                        var previousVariableName = ruleGenerationContext.CurrentVariableName;
                        ruleGenerationContext.CurrentVariableName = usedVariable;
                        var previousCaller = ruleGenerationContext.CallerRule;
                        ruleGenerationContext.CallerRule = assignmentElement;

                        // Route the cursor Move() through PendingCursorMove so that ProcessNonTerminalElement
                        // emits it INSIDE the type-discrimination block — Move() then fires only when the
                        // runtime cast actually matches (cursor advances only on real += consumption,
                        // honouring the Move() ↔ += Golden Rule). When the assignment is inside a collection
                        // group `(...)*` / `(...)+`, the loop body's own emitter handles the move; when it is
                        // part of a multi-alternative dispatch, the dispatcher handles the move.
                        var shouldEmitCursorMove = !isPartOfMultipleAlternative
                            && assignmentElement.Container is not GroupElement { IsCollection: true };

                        if (shouldEmitCursorMove)
                        {
                            ruleGenerationContext.PendingCursorMove = $"{Environment.NewLine}{cursorToUse.CursorVariableName}.Move();{Environment.NewLine}";
                        }

                        this.ProcessNonTerminalElement(writer, umlClass, nonTerminalElement, ruleGenerationContext);
                        ruleGenerationContext.CurrentVariableName = previousVariableName;
                        ruleGenerationContext.CallerRule = previousCaller;
                    }
                    else if (assignmentElement.Value is GroupElement groupElement)
                    {
                        var previousCaller = ruleGenerationContext.CallerRule;
                        ruleGenerationContext.CallerRule = assignmentElement;
                        this.ProcessAlternatives(writer, umlClass, groupElement.Alternatives, ruleGenerationContext);
                        ruleGenerationContext.CallerRule = previousCaller;
                    }
                    else if (assignmentElement.Value is ValueLiteralElement valueLiteralElement && valueLiteralElement.QueryIsQualifiedName())
                    {
                        var cursorToUse = ruleGenerationContext.DefinedCursors.Single(x => x.ApplicableRuleElements.Contains(assignmentElement));

                        writer.WriteSafeString($"{Environment.NewLine}if({cursorToUse.CursorVariableName}.Current != null){Environment.NewLine}");
                        writer.WriteSafeString($"{{{Environment.NewLine}");
                        writer.WriteSafeString($"SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder,{cursorToUse.CursorVariableName}.Current, writerContext, poco);{Environment.NewLine}");
                        writer.WriteSafeString($"{cursorToUse.CursorVariableName}.Move();{Environment.NewLine}");
                        writer.WriteSafeString("}");
                    }
                    else
                    {
                        var handCodedRuleName = assignmentElement.TextualNotationRule?.RuleName ?? "Unknown";
                        EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
                    }
                }
                else
                {
                    if (assignmentElement.IsOptional)
                    {
                        writer.WriteSafeString($"{Environment.NewLine}if({targetProperty.QueryIfStatementContentForNonEmpty("poco")}){Environment.NewLine}");
                        writer.WriteSafeString($"{{{Environment.NewLine}");

                        if (assignmentElement.Value is NonTerminalElement { Name: "NAME" })
                        {
                            writer.WriteSafeString($"SharedTextualNotationBuilder.AppendName(stringBuilder, poco.{targetProperty.Name.CapitalizeFirstLetter()});{Environment.NewLine}");
                        }
                        else
                        {
                            writer.WriteSafeString($"stringBuilder.Append(poco.{targetProperty.Name.CapitalizeFirstLetter()});{Environment.NewLine}");
                        }

                        writer.WriteSafeString("}");
                    }
                    else
                    {
                        var targetPropertyName = targetProperty.QueryPropertyNameBasedOnUmlProperties();

                        if (targetProperty.QueryIsString())
                        {
                            if (assignmentElement.Value is NonTerminalElement { Name: "REGULAR_COMMENT" })
                            {
                                // Documentation rule (`doc /* … */`) surrounds the comment with
                                // blank lines so doc blocks are visually separated from their
                                // owning members. Every other rule that assigns a REGULAR_COMMENT
                                // body (currently only `Comment`) renders adjacent to its
                                // neighbouring statements per the SST convention.
                                var surroundWithBlankLines = string.Equals(ruleGenerationContext.NamedElementToGenerate?.Name, "Documentation", StringComparison.Ordinal);
                                writer.WriteSafeString($"SharedTextualNotationBuilder.AppendRegularComment(stringBuilder, poco.{targetPropertyName}, surroundWithBlankLines: {(surroundWithBlankLines ? "true" : "false")});");
                            }
                            else if (assignmentElement.Value is NonTerminalElement { Name: "NAME" })
                            {
                                writer.WriteSafeString($"SharedTextualNotationBuilder.AppendName(stringBuilder, poco.{targetPropertyName});");
                            }
                            else if (string.Equals(targetPropertyName, "Operator", StringComparison.Ordinal))
                            {
                                // Operator tokens (binary, unary, conditional) need a trailing
                                // space to separate them from the next operand. Matches the
                                // convention used by the operator-switch dispatch path
                                // (RuleProcessor.PatternHandlers.cs:94-95).
                                writer.WriteSafeString($"stringBuilder.Append(poco.{targetPropertyName});{Environment.NewLine}");
                                writer.WriteSafeString("stringBuilder.Append(' ');");
                            }
                            else
                            {
                                writer.WriteSafeString($"stringBuilder.Append(poco.{targetPropertyName});");
                            }
                        }
                        else if (targetProperty.QueryIsBool())
                        {
                            if (assignmentElement.Value is TerminalElement terminalElement)
                            {
                                if (!isPartOfMultipleAlternative && assignmentElement.Container is not GroupElement { IsOptional: true })
                                {
                                    // KEBNF `Prop ?= 'literal'` — emit the literal when the runtime
                                    // value is truthy, but suppress it for concrete subtypes whose
                                    // metamodel default already equals the literal-trigger value.
                                    // For those subtypes the keyword is structurally redundant and
                                    // the canonical idiomatic source omits it (see e.g. SysML
                                    // `attribute X` rather than `ref attribute X` because
                                    // AttributeUsage's `isReference` default is `true`).
                                    var exclusionTypes = targetProperty.QuerySubclassesWithMatchingDefault(umlClass, "true");
                                    var exclusionClause = exclusionTypes.Count == 0
                                        ? string.Empty
                                        : $" && poco is not ({string.Join(" or ", exclusionTypes.Select(c => c.QueryFullyQualifiedTypeName()))})";

                                    writer.WriteSafeString($"if({targetProperty.QueryIfStatementContentForNonEmpty("poco")}{exclusionClause}){Environment.NewLine}");
                                    writer.WriteSafeString($"{{{Environment.NewLine}");
                                    writer.WriteSafeString($"stringBuilder.Append(\" {terminalElement.Value} \");{Environment.NewLine}");
                                    writer.WriteSafeString('}');
                                }
                                else
                                {
                                    writer.WriteSafeString($"stringBuilder.Append(\" {terminalElement.Value} \");");
                                }
                            }
                            else
                            {
                                writer.WriteSafeString($"stringBuilder.Append(poco.{targetPropertyName}.ToString().ToLower());");
                            }
                        }
                        else if (targetProperty.QueryIsEnum())
                        {
                            writer.WriteSafeString($"stringBuilder.Append(poco.{targetPropertyName}.ToString().ToLower());{Environment.NewLine}");
                            writer.WriteSafeString("stringBuilder.Append(' ');");
                        }
                        else if (targetProperty.QueryIsReferenceType())
                        {
                            switch (assignmentElement.Value)
                            {
                                case NonTerminalElement nonTerminalElement:
                                {
                                    var previousCaller = ruleGenerationContext.CallerRule;
                                    ruleGenerationContext.CallerRule = nonTerminalElement;

                                    // Thin `[QualifiedName]` wrapper inlining: when the referenced rule's body is
                                    // just `[QualifiedName]` (e.g. FeatureReference, InstantiatedTypeReference) the
                                    // generated `Build{Wrapper}` method receives the target POCO as both target AND
                                    // source for name resolution, which loses the reference site. Inline the
                                    // AppendQualifiedName call here with the OUTER `poco` as the source context so
                                    // imports declared in the source's enclosing namespace can resolve to the
                                    // short / unqualified name.
                                    var referencedRule = ruleGenerationContext.FindRule(nonTerminalElement.Name);

                                    if (IsThinQualifiedNameWrapperRule(referencedRule))
                                    {
                                        writer.WriteSafeString($"{Environment.NewLine}if (poco.{targetPropertyName} != null){Environment.NewLine}{{{Environment.NewLine}");
                                        writer.WriteSafeString($"SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder, poco.{targetPropertyName}, writerContext, poco);{Environment.NewLine}");
                                        writer.WriteSafeString($"stringBuilder.Append(' ');{Environment.NewLine}");
                                        writer.WriteSafeString($"}}{Environment.NewLine}");
                                        ruleGenerationContext.CallerRule = previousCaller;
                                        break;
                                    }

                                    // Polymorphic `ownedMemberFeature` access on IFeatureMembership: the runtime
                                    // POCO may also be an IParameterMembership (the form used to model operands
                                    // of every InvocationExpression / OperatorExpression per KerML §8.2.5.8.2
                                    // Notes 1-2 — see Resources/KerML-textual-bnf.kebnf:1176-1178). In that
                                    // shape the operand expression lives under ownedMemberFeature → FeatureValue
                                    // → value rather than directly under ownedMemberFeature. Route the access
                                    // through SharedTextualNotationBuilder.QueryEffectiveOwnedMemberFeature
                                    // which transparently normalises both runtime shapes into a single feature
                                    // reference downstream code can type-test as before.
                                    if (string.Equals(targetProperty.Name, "ownedMemberFeature", StringComparison.Ordinal)
                                        && QueryIsAssignableToFeatureMembership(umlClass))
                                    {
                                        const string effectiveVariableName = "effectiveOwnedMemberFeature";
                                        writer.WriteSafeString($"var {effectiveVariableName} = SysML2.NET.Serializer.TextualNotation.Writers.SharedTextualNotationBuilder.QueryEffectiveOwnedMemberFeature(poco);{Environment.NewLine}");
                                        ruleGenerationContext.CurrentVariableName = effectiveVariableName;
                                    }
                                    else
                                    {
                                        ruleGenerationContext.CurrentVariableName = $"poco.{targetPropertyName}";
                                    }

                                    this.ProcessNonTerminalElement(writer, targetProperty.Type as IClass, nonTerminalElement, ruleGenerationContext, isPartOfMultipleAlternative);
                                    ruleGenerationContext.CurrentVariableName = "poco";
                                    ruleGenerationContext.CallerRule = previousCaller;
                                    break;
                                }
                                case ValueLiteralElement valueLiteralElement when valueLiteralElement.QueryIsQualifiedName():
                                    if (isPartOfMultipleAlternative)
                                    {
                                        writer.WriteSafeString($"SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder,poco.{targetPropertyName}, writerContext, poco);{Environment.NewLine}");

                                        if (!ruleGenerationContext.IsNextElementNewLineTerminal())
                                        {
                                            writer.WriteSafeString("stringBuilder.Append(' ');");
                                        }
                                    }
                                    else
                                    {
                                        writer.WriteSafeString($"{Environment.NewLine}if (poco.{targetPropertyName} != null){Environment.NewLine}");
                                        writer.WriteSafeString($"{{{Environment.NewLine}");
                                        writer.WriteSafeString($"SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder,poco.{targetPropertyName}, writerContext, poco);{Environment.NewLine}");

                                        if (!ruleGenerationContext.IsNextElementNewLineTerminal())
                                        {
                                            writer.WriteSafeString("stringBuilder.Append(' ');");
                                        }

                                        writer.WriteSafeString($"{Environment.NewLine}}}");
                                    }

                                    break;
                                default:
                                    var handCodedRuleName = assignmentElement.TextualNotationRule?.RuleName ?? "Unknown";
                                    EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
                                    break;
                            }
                        }
                        else
                        {
                            writer.WriteSafeString($"stringBuilder.Append(poco.{targetPropertyName}.ToString());");
                        }
                    }
                }
            }
            else
            {
                // The grammar's assignment property does not resolve against the target
                // metamodel class (e.g. the OMG kebnf carries a one-off `ownedFeatureMember`
                // vs. metamodel `ownedMemberFeature` typo for `OwnedExpressionMember`).
                // Delegate to the HandCoded sibling per the documented convention rather
                // than emitting a name-collision-prone `Build{Property}(poco, …)` call.
                var handCodedRuleName = assignmentElement.TextualNotationRule?.RuleName ?? "Unknown";
                EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
            }
        }

        /// <summary>
        /// Process a <see cref="NonTerminalElement" />
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="nonTerminalElement">The <see cref="NonTerminalElement" /> to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <param name="isPartOfMultipleAlternative">Whether this is part of a multiple alternative</param>
        internal void ProcessNonTerminalElement(EncodedTextWriter writer, IClass umlClass, NonTerminalElement nonTerminalElement, RuleGenerationContext ruleGenerationContext, bool isPartOfMultipleAlternative = false)
        {
            var referencedRule = ruleGenerationContext.FindRule(nonTerminalElement.Name);

            string typeTarget;

            if (referencedRule == null)
            {
                typeTarget = umlClass.Name;
            }
            else
            {
                typeTarget = referencedRule.EffectiveTarget;
            }

            var isForProperty = ruleGenerationContext.CurrentVariableName.Contains('.');

            var emitPropertyNullGuard = isForProperty && !isPartOfMultipleAlternative;

            if (emitPropertyNullGuard)
            {
                writer.WriteSafeString($"{Environment.NewLine}if ({ruleGenerationContext.CurrentVariableName} != null){Environment.NewLine}");
                writer.WriteSafeString($"{{{Environment.NewLine}");
            }

            if (nonTerminalElement.IsCollection)
            {
                this.EmitCollectionNonTerminalLoop(writer, umlClass, nonTerminalElement, referencedRule, typeTarget, ruleGenerationContext);

                if (emitPropertyNullGuard)
                {
                    writer.WriteSafeString($"{Environment.NewLine}}}");
                }

                return;
            }

            if (typeTarget != ruleGenerationContext.NamedElementToGenerate.Name)
            {
                var targetType = RuleQueryUtilities.FindNamedElement(umlClass.Cache, typeTarget);

                if (targetType != null)
                {
                    if (targetType is IClass targetClass && (
                            umlClass.QueryAllGeneralClassifiers().Contains(targetClass)
                            || (targetClass.QueryAllGeneralClassifiers().Contains(umlClass)
                                && ruleGenerationContext.CurrentVariableName.Contains('.'))
                            || targetClass == umlClass
                            || !ruleGenerationContext.CurrentVariableName.Contains("poco")))
                    {
                        var needsDownwardCast = targetClass != umlClass && !umlClass.QueryAllGeneralClassifiers().Contains(targetClass);
                        var emitCast = ruleGenerationContext.CallerRule is AssignmentElement || needsDownwardCast;

                        if (emitCast)
                        {
                            var castedVariableName = $"elementAs{targetClass.Name}";
                            writer.WriteSafeString($"{Environment.NewLine}if ({ruleGenerationContext.CurrentVariableName} is {targetClass.QueryFullyQualifiedTypeName()} {castedVariableName}){Environment.NewLine}");
                            ruleGenerationContext.CurrentVariableName = castedVariableName;
                            writer.WriteSafeString($"{{{Environment.NewLine}");
                        }

                        var emittedCondition = TryEmitOptionalCondition(writer, nonTerminalElement, referencedRule, targetClass, ruleGenerationContext, ruleGenerationContext.CurrentVariableName);

                        writer.WriteSafeString($"{targetType.Name}TextualNotationBuilder.Build{nonTerminalElement.Name}({ruleGenerationContext.CurrentVariableName}, writerContext, stringBuilder);");

                        if (emittedCondition)
                        {
                            writer.WriteSafeString($"{Environment.NewLine}}}");
                        }

                        if (!string.IsNullOrEmpty(ruleGenerationContext.PendingCursorMove))
                        {
                            writer.WriteSafeString(ruleGenerationContext.PendingCursorMove);
                            ruleGenerationContext.PendingCursorMove = null;
                        }

                        if (emitCast)
                        {
                            writer.WriteSafeString($"{Environment.NewLine}}}");
                        }
                    }
                    else
                    {
                        var previousCaller = ruleGenerationContext.CallerRule;
                        ruleGenerationContext.CallerRule = nonTerminalElement;
                        var previousName = ruleGenerationContext.CurrentVariableName;

                        this.ProcessAlternatives(writer, umlClass, referencedRule?.Alternatives, ruleGenerationContext, isPartOfMultipleAlternative);
                        ruleGenerationContext.CallerRule = previousCaller;
                        ruleGenerationContext.CurrentVariableName = previousName;
                    }
                }
                else
                {
                    if (NoTargetRuleResolver.IsSharedRule(referencedRule, umlClass))
                    {
                        EmitSharedNoTargetRuleCall(writer, umlClass, nonTerminalElement, referencedRule, ruleGenerationContext);
                    }
                    else
                    {
                        var previousCaller = ruleGenerationContext.CallerRule;
                        ruleGenerationContext.CallerRule = nonTerminalElement;
                        var previousName = ruleGenerationContext.CurrentVariableName;

                        this.ProcessAlternatives(writer, umlClass, referencedRule?.Alternatives, ruleGenerationContext, isPartOfMultipleAlternative);
                        ruleGenerationContext.CallerRule = previousCaller;
                        ruleGenerationContext.CurrentVariableName = previousName;
                    }
                }
            }
            else
            {
                var variableToUse = referencedRule != null ? ruleGenerationContext.CurrentVariableName : "poco";

                var emittedSameClassCondition = TryEmitOptionalCondition(writer, nonTerminalElement, referencedRule, umlClass, ruleGenerationContext, variableToUse);

                writer.WriteSafeString($"Build{nonTerminalElement.Name}({variableToUse}, writerContext, stringBuilder);");

                if (emittedSameClassCondition)
                {
                    writer.WriteSafeString($"{Environment.NewLine}}}");
                }
            }

            if (!string.IsNullOrEmpty(ruleGenerationContext.PendingCursorMove))
            {
                writer.WriteSafeString(ruleGenerationContext.PendingCursorMove);
                ruleGenerationContext.PendingCursorMove = null;
            }

            if (emitPropertyNullGuard)
            {
                writer.WriteSafeString($"{Environment.NewLine}}}");
            }
        }

        /// <summary>
        /// Declares a single cursor for an enumerable assignment property if not already declared.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="assignmentElement">The <see cref="AssignmentElement" /> requiring a cursor</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        internal void DeclareCursorIfRequired(EncodedTextWriter writer, IClass umlClass, AssignmentElement assignmentElement, RuleGenerationContext ruleGenerationContext)
        {
            var allProperties = umlClass.QueryAllProperties();
            var targetProperty = allProperties.SingleOrDefault(x => string.Equals(x.Name, assignmentElement.Property, StringComparison.OrdinalIgnoreCase));

            if (targetProperty == null || !targetProperty.QueryIsEnumerable())
            {
                return;
            }

            if (ruleGenerationContext.DefinedCursors.SingleOrDefault(x => x.IsCursorValidForProperty(targetProperty) || x.ApplicableRuleElements.Contains(assignmentElement)) is { } alreadyDefinedCursor)
            {
                alreadyDefinedCursor.ApplicableRuleElements.Add(assignmentElement);
                return;
            }

            switch (assignmentElement.Value)
            {
                case NonTerminalElement:
                case ValueLiteralElement:
                case GroupElement:
                {
                    var cursorToUse = new CursorDefinition
                    {
                        DefinedForProperty = targetProperty
                    };

                    cursorToUse.ApplicableRuleElements.Add(assignmentElement);

                    var propertyAccessName = targetProperty.QueryPropertyNameBasedOnUmlProperties();
                    writer.WriteSafeString($"var {cursorToUse.CursorVariableName} = writerContext.CursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{propertyAccessName});");
                    writer.WriteSafeString(Environment.NewLine);
                    ruleGenerationContext.DefinedCursors.Add(cursorToUse);
                    break;
                }
                case AssignmentElement containedAssignment:
                    this.DeclareCursorIfRequired(writer, umlClass, containedAssignment, ruleGenerationContext);
                    break;
            }
        }

        /// <summary>
        /// Emits a call to the shared no-target rule builder.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The caller's <see cref="IClass" /></param>
        /// <param name="nonTerminalElement">The <see cref="NonTerminalElement" /> being processed</param>
        /// <param name="referencedRule">The referenced shared no-target <see cref="TextualNotationRule" /></param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        private static void EmitSharedNoTargetRuleCall(EncodedTextWriter writer, IClass umlClass, NonTerminalElement nonTerminalElement, TextualNotationRule referencedRule, RuleGenerationContext ruleGenerationContext)
        {
            var effectiveTarget = NoTargetRuleResolver.ResolveEffectiveTarget(referencedRule, ruleGenerationContext.AllRules, umlClass);

            string variableExpression;

            if (effectiveTarget == null || effectiveTarget == umlClass || umlClass.QueryAllGeneralClassifiers().Contains(effectiveTarget))
            {
                variableExpression = ruleGenerationContext.CurrentVariableName;
            }
            else
            {
                variableExpression = $"({effectiveTarget.QueryFullyQualifiedTypeName()}){ruleGenerationContext.CurrentVariableName}";
            }

            var emittedCondition = effectiveTarget != null
                                   && TryEmitOptionalCondition(writer, nonTerminalElement, referencedRule, effectiveTarget, ruleGenerationContext, ruleGenerationContext.CurrentVariableName);

            writer.WriteSafeString($"{RulesHelper.SharedBuilderClassName}.Build{nonTerminalElement.Name}({variableExpression}, writerContext, stringBuilder);");

            if (emittedCondition)
            {
                writer.WriteSafeString($"{Environment.NewLine}}}");
            }
        }

        /// <summary>
        /// Returns <see langword="true"/> when <paramref name="umlClass"/> IS-A
        /// <c>FeatureMembership</c> — used to gate the polymorphic <c>ownedMemberFeature</c>
        /// access path that normalises pure <c>IFeatureMembership</c> and
        /// <c>IParameterMembership</c> runtime shapes.
        /// </summary>
        /// <param name="umlClass">The <see cref="IClass"/> under test.</param>
        /// <returns><see langword="true"/> if the class IS-A FeatureMembership.</returns>
        private static bool QueryIsAssignableToFeatureMembership(IClass umlClass)
        {
            if (umlClass == null)
            {
                return false;
            }

            return string.Equals(umlClass.Name, "FeatureMembership", StringComparison.Ordinal)
                   || umlClass.QueryAllGeneralClassifiers().Any(c => string.Equals(c.Name, "FeatureMembership", StringComparison.Ordinal));
        }

        /// <summary>
        /// Returns <see langword="true"/> when <paramref name="rule"/> is a "thin
        /// <c>[QualifiedName]</c> wrapper" — i.e. its body is a single alternative containing a
        /// single element that is a <c>[QualifiedName]</c> resolution. Examples in the KerML
        /// grammar (<c>Resources/KerML-textual-bnf.kebnf</c>): <c>FeatureReference : Feature =
        /// [QualifiedName]</c> (line 1201) and <c>InstantiatedTypeReference : Type =
        /// [QualifiedName]</c> (line 1229).
        /// <para>
        /// When a caller-rule's assignment-element references such a wrapper as its value, the
        /// generated <c>Build{Wrapper}</c> method receives the target as both target AND source
        /// for name resolution, which loses the syntactic reference site (the caller's
        /// <c>poco</c>) needed to honour imports declared in the source's enclosing scope chain.
        /// The codegen inlines the <c>AppendQualifiedName</c> call at the caller's emission point
        /// instead so the OUTER <c>poco</c> serves as the resolution source.
        /// </para>
        /// </summary>
        /// <param name="rule">The <see cref="TextualNotationRule"/> under test; may be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the rule is a thin <c>[QualifiedName]</c> wrapper.</returns>
        private static bool IsThinQualifiedNameWrapperRule(TextualNotationRule rule)
        {
            if (rule == null || rule.Alternatives.Count != 1)
            {
                return false;
            }

            var alternative = rule.Alternatives[0];

            if (alternative.Elements.Count != 1)
            {
                return false;
            }

            return alternative.Elements[0] is ValueLiteralElement valueLiteralElement
                   && valueLiteralElement.QueryIsQualifiedName();
        }
    }
}
