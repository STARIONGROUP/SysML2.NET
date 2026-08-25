// -------------------------------------------------------------------------------------------------
// <copyright file="RuleProcessor.PatternHandlers.cs" company="Starion Group S.A.">
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
    using System.Text.RegularExpressions;

    using HandlebarsDotNet;

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.Grammar.Model;

    using uml4net;
    using uml4net.Classification;
    using uml4net.CommonStructure;
    using uml4net.Extensions;
    using uml4net.StructuredClassifiers;
    using uml4net.Values;

    /// <summary>
    /// Pattern detection and specialized code emission for grammar alternatives
    /// </summary>
    internal sealed partial class RuleProcessor
    {
        /// <summary>
        /// Upper bound on a single regular-expression match, guarding against catastrophic backtracking
        /// on a pathological OCL body.
        /// </summary>
        private const int MatchTimeoutMilliseconds = 1000;

        /// <summary>
        /// Pattern B: detects operator-literal alternations and generates a switch on the operator property.
        /// </summary>
        private bool TryHandleOperatorLiteralAlternation(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
        {
            if (alternatives.Count < 2)
            {
                return false;
            }

            var operatorBranches = new List<(Alternatives Alternative, List<string> Literals)>();

            foreach (var alternative in alternatives)
            {
                if (alternative.Elements.Count == 0
                    || alternative.Elements[0] is not AssignmentElement { Property: "operator", Operator: "=" } operatorAssignment)
                {
                    return false;
                }

                var literals = ExtractLiteralAlternation(operatorAssignment.Value, ruleGenerationContext.AllRules);

                if (literals == null || literals.Count == 0)
                {
                    return false;
                }

                if (!AreAlternativeTailElementsProcessable(alternative.Elements, umlClass, ruleGenerationContext))
                {
                    return false;
                }

                operatorBranches.Add((alternative, literals));
            }

            foreach (var alternative in alternatives)
            {
                this.DeclareAllRequiredCursors(writer, umlClass, alternative, ruleGenerationContext);
            }

            var variableName = ruleGenerationContext.CurrentVariableName ?? "poco";
            writer.WriteSafeString($"switch ({variableName}.Operator){Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");

            foreach (var (alternative, literals) in operatorBranches)
            {
                foreach (var literal in literals)
                {
                    writer.WriteSafeString($"case \"{literal}\":{Environment.NewLine}");
                }

                writer.WriteSafeString($"stringBuilder.Append({variableName}.Operator);{Environment.NewLine}");
                writer.WriteSafeString($"stringBuilder.Append(' ');{Environment.NewLine}");

                var previousSiblings = ruleGenerationContext.CurrentSiblingElements;
                var previousIndex = ruleGenerationContext.CurrentElementIndex;
                ruleGenerationContext.CurrentSiblingElements = alternative.Elements;

                for (var elementIndex = 1; elementIndex < alternative.Elements.Count; elementIndex++)
                {
                    ruleGenerationContext.CurrentElementIndex = elementIndex;
                    this.ProcessRuleElement(writer, umlClass, alternative.Elements[elementIndex], ruleGenerationContext);
                }

                ruleGenerationContext.CurrentSiblingElements = previousSiblings;
                ruleGenerationContext.CurrentElementIndex = previousIndex;

                writer.WriteSafeString($"break;{Environment.NewLine}");
            }

            writer.WriteSafeString($"}}{Environment.NewLine}");
            return true;
        }

        /// <summary>
        /// Pattern A: detects empty vs non-empty membership alternations.
        /// </summary>
        private bool TryHandleEmptyVsNonEmptyMembership(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
        {
            if (alternatives.Count != 2)
            {
                return false;
            }

            var branches = new List<(Alternatives Alternative, AssignmentElement Assignment, NonTerminalElement NonTerminal, bool IsEmpty)>();
            string sharedProperty = null;

            foreach (var alternative in alternatives)
            {
                if (alternative.Elements.Any(element => element is not TerminalElement && element is not AssignmentElement))
                {
                    return false;
                }

                var collectionAssignments = alternative.Elements
                    .OfType<AssignmentElement>()
                    .Where(assignment => assignment.Operator == "+=")
                    .ToList();

                if (collectionAssignments.Count != 1)
                {
                    return false;
                }

                if (alternative.Elements.OfType<AssignmentElement>().Count() != 1)
                {
                    return false;
                }

                var assignment = collectionAssignments[0];

                if (assignment.Value is not NonTerminalElement nonTerminal)
                {
                    return false;
                }

                if (sharedProperty == null)
                {
                    sharedProperty = assignment.Property;
                }
                else if (sharedProperty != assignment.Property)
                {
                    return false;
                }

                var isEmpty = RuleQueryUtilities.IsEmptyMembershipRule(nonTerminal.Name, ruleGenerationContext.AllRules);
                branches.Add((alternative, assignment, nonTerminal, isEmpty));
            }

            if (branches.Count(b => b.IsEmpty) != 1 || branches.Count(b => !b.IsEmpty) != 1)
            {
                return false;
            }

            var emptyBranch = branches.Single(b => b.IsEmpty);
            var nonEmptyBranch = branches.Single(b => !b.IsEmpty);

            var emptyTarget = RuleQueryUtilities.ResolveRuleTargetClass(emptyBranch.NonTerminal, umlClass.Cache, ruleGenerationContext.AllRules);
            var nonEmptyTarget = RuleQueryUtilities.ResolveRuleTargetClass(nonEmptyBranch.NonTerminal, umlClass.Cache, ruleGenerationContext.AllRules);

            if (emptyTarget == null || nonEmptyTarget == null || emptyTarget != nonEmptyTarget)
            {
                return false;
            }

            var emptyRule = ruleGenerationContext.FindRule(emptyBranch.NonTerminal.Name);

            var emptyRuleAssignment = emptyRule?.Alternatives
                .SelectMany(alternative => alternative.Elements)
                .OfType<AssignmentElement>()
                .FirstOrDefault(assignment => assignment.Operator == "+=");

            if (emptyRuleAssignment == null)
            {
                return false;
            }

            var discriminatorProperty = emptyTarget.QueryAllProperties()
                .SingleOrDefault(property => string.Equals(property.Name, emptyRuleAssignment.Property, StringComparison.OrdinalIgnoreCase));

            if (discriminatorProperty == null)
            {
                return false;
            }

            var discriminatorPropertyName = discriminatorProperty.QueryPropertyNameBasedOnUmlProperties();

            foreach (var alternative in alternatives)
            {
                this.DeclareAllRequiredCursors(writer, umlClass, alternative, ruleGenerationContext);
            }

            var cursor = ruleGenerationContext.DefinedCursors.FirstOrDefault(c => c.ApplicableRuleElements.Contains(emptyBranch.Assignment));

            if (cursor == null)
            {
                return false;
            }

            var typeName = emptyTarget.QueryFullyQualifiedTypeName();
            var cursorVarName = cursor.CursorVariableName;

            // An "Empty" wrapper rule usually still ASSIGNS its collection (EmptyUsage = {}), so a
            // Count-based discriminator can never select the empty branch. When the branches wrap
            // different classes, discriminate on the WRAPPED element type instead.
            var wrappedNonEmptyTypeName = QueryWrappedElementTypeName(nonEmptyBranch.NonTerminal, umlClass, ruleGenerationContext);
            var wrappedEmptyTypeName = QueryWrappedElementTypeName(emptyBranch.NonTerminal, umlClass, ruleGenerationContext);

            if (wrappedNonEmptyTypeName != null && wrappedEmptyTypeName != null && wrappedNonEmptyTypeName != wrappedEmptyTypeName)
            {
                var payloadVarName = $"{emptyTarget.Name.LowerCaseFirstLetter()}Payload{ruleGenerationContext.NarrowedTypeCheckCounter}";
                ruleGenerationContext.NarrowedTypeCheckCounter++;

                writer.WriteSafeString($"if ({cursorVarName}.Current is {typeName} {payloadVarName} && {payloadVarName}.{discriminatorPropertyName}.OfType<{wrappedNonEmptyTypeName}>().Any()){Environment.NewLine}");
                writer.WriteSafeString($"{{{Environment.NewLine}");
                this.EmitAlternativeBody(writer, umlClass, nonEmptyBranch.Alternative, ruleGenerationContext);
                writer.WriteSafeString($"}}{Environment.NewLine}");
                writer.WriteSafeString($"else if ({cursorVarName}.Current is {typeName}){Environment.NewLine}");
                writer.WriteSafeString($"{{{Environment.NewLine}");
                this.EmitAlternativeBody(writer, umlClass, emptyBranch.Alternative, ruleGenerationContext);
                writer.WriteSafeString($"}}{Environment.NewLine}");

                return true;
            }

            writer.WriteSafeString($"if ({cursorVarName}.Current is {typeName} {{ {discriminatorPropertyName}.Count: 0 }}){Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");
            this.EmitAlternativeBody(writer, umlClass, emptyBranch.Alternative, ruleGenerationContext);
            writer.WriteSafeString($"}}{Environment.NewLine}");
            writer.WriteSafeString($"else if ({cursorVarName}.Current is {typeName}){Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");
            this.EmitAlternativeBody(writer, umlClass, nonEmptyBranch.Alternative, ruleGenerationContext);
            writer.WriteSafeString($"}}{Environment.NewLine}");

            return true;
        }

        /// <summary>
        /// Resolves the runtime type a single-assignment wrapper rule puts into its collection
        /// (e.g. <c>IExpression</c> for <c>ExpressionParameterMember</c>), or <see langword="null" />
        /// when the rule does not have exactly one resolvable <c>+=</c> non-terminal assignment.
        /// </summary>
        /// <param name="wrapperNonTerminal">The <see cref="NonTerminalElement" /> naming the wrapper rule.</param>
        /// <param name="umlClass">The class hosting the current rule (provides the UML cache).</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" />.</param>
        /// <returns>The wrapped element's fully-qualified type name, or <see langword="null" />.</returns>
        private static string QueryWrappedElementTypeName(NonTerminalElement wrapperNonTerminal, IClass umlClass, RuleGenerationContext ruleGenerationContext)
        {
            var wrapperRule = ruleGenerationContext.FindRule(wrapperNonTerminal.Name);

            var wrappedAssignments = wrapperRule?.Alternatives
                .SelectMany(alternative => alternative.Elements)
                .OfType<AssignmentElement>()
                .Where(assignment => assignment is { Operator: "+=", Value: NonTerminalElement })
                .ToList();

            if (wrappedAssignments == null || wrappedAssignments.Count != 1)
            {
                return null;
            }

            var wrappedNonTerminal = (NonTerminalElement)wrappedAssignments[0].Value;
            var wrappedClass = RuleQueryUtilities.ResolveRuleTargetClass(wrappedNonTerminal, umlClass.Cache, ruleGenerationContext.AllRules);

            return wrappedClass?.QueryFullyQualifiedTypeName();
        }

        /// <summary>
        /// Pattern C: detects poco-runtime-type dispatch with compound alternatives.
        /// </summary>
        private static bool TryHandlePocoTypeDispatchWithCompoundAlternatives(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
        {
            if (alternatives.Count < 2)
            {
                return false;
            }

            if (alternatives.All(alternative => alternative.Elements.Count == 1))
            {
                return false;
            }

            var branches = new List<(Alternatives Alternative, NonTerminalElement NonTerminal, IClass TargetClass)>();

            foreach (var alternative in alternatives)
            {
                if (alternative.Elements.Count == 0
                    || alternative.Elements[0] is not NonTerminalElement leadingNonTerminal)
                {
                    return false;
                }

                for (var elementIndex = 1; elementIndex < alternative.Elements.Count; elementIndex++)
                {
                    if (alternative.Elements[elementIndex] is not TerminalElement trailingTerminal)
                    {
                        return false;
                    }

                    if (trailingTerminal.IsOptional)
                    {
                        return false;
                    }
                }

                var targetClass = RuleQueryUtilities.ResolveRuleTargetClass(leadingNonTerminal, umlClass.Cache, ruleGenerationContext.AllRules);

                if (targetClass == null)
                {
                    return false;
                }

                if (targetClass != umlClass && !targetClass.QueryAllGeneralClassifiers().Contains(umlClass))
                {
                    return false;
                }

                branches.Add((alternative, leadingNonTerminal, targetClass));
            }

            if (branches.Select(branch => branch.TargetClass).Distinct().Count() != branches.Count)
            {
                return false;
            }

            var defaultBranch = branches.FirstOrDefault(branch => branch.TargetClass == umlClass);

            var orderedBranches = branches
                .Where(branch => branch.TargetClass != umlClass)
                .OrderByDescending(branch => branch.TargetClass.QueryAllGeneralClassifiers().Count)
                .ToList();

            writer.WriteSafeString($"switch (poco){Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");

            foreach (var (alternative, leadingNonTerminal, targetClass) in orderedBranches)
            {
                var caseVarName = $"poco{targetClass.Name}";
                writer.WriteSafeString($"case {targetClass.QueryFullyQualifiedTypeName()} {caseVarName}:{Environment.NewLine}");
                EmitCompoundPocoTypeBranch(writer, umlClass, leadingNonTerminal, alternative, targetClass, caseVarName);
                writer.WriteSafeString($"break;{Environment.NewLine}");
            }

            if (defaultBranch.NonTerminal != null)
            {
                writer.WriteSafeString($"default:{Environment.NewLine}");
                EmitCompoundPocoTypeBranch(writer, umlClass, defaultBranch.NonTerminal, defaultBranch.Alternative, defaultBranch.TargetClass, "poco");
                writer.WriteSafeString($"break;{Environment.NewLine}");
            }

            writer.WriteSafeString($"}}{Environment.NewLine}");
            return true;
        }

        /// <summary>
        /// Pattern D: detects reference-or-inline two-alternative shape.
        /// </summary>
        private bool TryHandleReferenceOrInline(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
        {
            if (alternatives.Count != 2)
            {
                return false;
            }

            var altList = alternatives.ToList();
            var alt1 = altList[0];
            var alt2 = altList[1];

            if (alt1.Elements.Count == 0
                || alt1.Elements[0] is not AssignmentElement { Operator: "+=", Value: NonTerminalElement firstNonTerminal } firstAssignment)
            {
                return false;
            }

            var referenceTargetClass = RuleQueryUtilities.ResolveRuleTargetClass(firstNonTerminal, umlClass.Cache, ruleGenerationContext.AllRules);

            if (referenceTargetClass == null)
            {
                return false;
            }

            var collectionProperty = umlClass.QueryAllProperties()
                .SingleOrDefault(property => string.Equals(property.Name, firstAssignment.Property, StringComparison.OrdinalIgnoreCase));

            if (collectionProperty == null || !collectionProperty.QueryIsEnumerable())
            {
                return false;
            }

            for (var elementIndex = 1; elementIndex < alt1.Elements.Count; elementIndex++)
            {
                if (alt1.Elements[elementIndex] is not NonTerminalElement)
                {
                    return false;
                }
            }

            if (alt2.Elements.Count == 0)
            {
                return false;
            }

            var alt2NonTerminalIndex = -1;

            for (var elementIndex = 0; elementIndex < alt2.Elements.Count; elementIndex++)
            {
                var element = alt2.Elements[elementIndex];

                switch (element)
                {
                    case TerminalElement terminalElement:
                        if (terminalElement.IsOptional)
                        {
                            return false;
                        }

                        if (alt2NonTerminalIndex >= 0)
                        {
                            return false;
                        }

                        break;
                    case NonTerminalElement:
                        if (alt2NonTerminalIndex >= 0)
                        {
                            return false;
                        }

                        alt2NonTerminalIndex = elementIndex;
                        break;
                    default:
                        return false;
                }
            }

            if (alt2NonTerminalIndex < 0)
            {
                return false;
            }

            this.DeclareAllRequiredCursors(writer, umlClass, alt1, ruleGenerationContext);

            var variableName = ruleGenerationContext.CurrentVariableName ?? "poco";
            var collectionPropertyAccessor = collectionProperty.QueryPropertyNameBasedOnUmlProperties();
            var referenceTypeName = referenceTargetClass.QueryFullyQualifiedTypeName();

            writer.WriteSafeString($"if ({variableName}.{collectionPropertyAccessor}.OfType<{referenceTypeName}>().Any()){Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");
            this.EmitAlternativeBody(writer, umlClass, alt1, ruleGenerationContext);
            writer.WriteSafeString($"}}{Environment.NewLine}");
            writer.WriteSafeString($"else{Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");
            this.EmitAlternativeBody(writer, umlClass, alt2, ruleGenerationContext);
            writer.WriteSafeString($"}}{Environment.NewLine}");

            return true;
        }

        /// <summary>
        /// Validates that tail elements of an alternative resolve to existing target classes.
        /// </summary>
        private static bool AreAlternativeTailElementsProcessable(IReadOnlyList<RuleElement> elements, IClass umlClass, RuleGenerationContext ruleGenerationContext)
        {
            for (var elementIndex = 1; elementIndex < elements.Count; elementIndex++)
            {
                if (elements[elementIndex] is not AssignmentElement { Operator: "+=", Value: NonTerminalElement nonTerminal })
                {
                    continue;
                }

                var referencedRule = ruleGenerationContext.FindRule(nonTerminal.Name);

                var typeTarget = referencedRule != null
                    ? referencedRule.EffectiveTarget
                    : nonTerminal.Name;

                if (typeTarget == ruleGenerationContext.NamedElementToGenerate.Name)
                {
                    continue;
                }

                var targetClassExists = umlClass.Cache.Values.OfType<INamedElement>().Any(x => x.Name == typeTarget);

                if (!targetClassExists)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Extracts literal terminal values from a grammar value element.
        /// </summary>
        private static List<string> ExtractLiteralAlternation(RuleElement value, IReadOnlyList<TextualNotationRule> allRules)
        {
            switch (value)
            {
                case TerminalElement terminal:
                    return [terminal.Value];

                case NonTerminalElement nonTerminal:
                    var referencedRule = allRules.SingleOrDefault(x => x.RuleName == nonTerminal.Name);

                    if (referencedRule == null)
                    {
                        return null;
                    }

                    var literals = new List<string>();

                    foreach (var ruleAlternativeElements in referencedRule.Alternatives.Select(x => x.Elements))
                    {
                        if (ruleAlternativeElements.Count != 1 || ruleAlternativeElements[0] is not TerminalElement nestedTerminal)
                        {
                            return null;
                        }

                        literals.Add(nestedTerminal.Value);
                    }

                    return literals.Count == 0 ? null : literals;

                default:
                    return null;
            }
        }

        /// <summary>
        /// Handles alternatives targeting multiple collection properties by falling back to HandCoded.
        /// </summary>
        private static void ProcessMultiCollectionAssignment(EncodedTextWriter writer, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
        {
            var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
            EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
        }

        /// <summary>
        /// Process multiple alternatives when all have one element of the same type.
        /// </summary>
        private void ProcessUnitypedAlternativesWithOneElement(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
        {
            var firstRuleElement = alternatives.ElementAt(0).Elements[0];

            switch (firstRuleElement)
            {
                case TerminalElement terminalElement:
                    TerminalWriter.WriteTerminalAppendWithLeadingSpace(writer, terminalElement.Value);
                    break;
                case NonTerminalElement:
                {
                    var nonTerminalElements = alternatives.SelectMany(x => x.Elements).OfType<NonTerminalElement>().ToList();
                    var mappedNonTerminalElements = RuleQueryUtilities.OrderElementsByInheritance(nonTerminalElements, umlClass.Cache, ruleGenerationContext);

                    var duplicateClasses = mappedNonTerminalElements
                        .GroupBy(x => x.UmlClass)
                        .Where(g => g.Count() > 1)
                        .ToDictionary(g => g.Key, g => g.ToList());

                    var whenGuards = new Dictionary<NonTerminalElement, string>();

                    foreach (var duplicateGroup in duplicateClasses)
                    {
                        var allProperties = duplicateGroup.Key.QueryAllProperties();

                        var elementBoolProps = new List<(NonTerminalElement RuleElement, List<string> BoolProps)>();

                        foreach (var ruleElement in duplicateGroup.Value.Select(x => x.RuleElement))
                        {
                            var referencedRule = ruleGenerationContext.AllRules.Single(x => x.RuleName == ruleElement.Name);
                            var booleanProperties = RuleQueryUtilities.QueryBooleanAssignmentProperties(referencedRule, ruleGenerationContext.AllRules);
                            elementBoolProps.Add((ruleElement, booleanProperties));
                        }

                        for (var elementIndex = 0; elementIndex < elementBoolProps.Count; elementIndex++)
                        {
                            var current = elementBoolProps[elementIndex];
                            var othersProperties = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                            for (var otherIndex = 0; otherIndex < elementBoolProps.Count; otherIndex++)
                            {
                                if (otherIndex != elementIndex)
                                {
                                    foreach (var prop in elementBoolProps[otherIndex].BoolProps)
                                    {
                                        othersProperties.Add(prop);
                                    }
                                }
                            }

                            var uniqueProperties = current.BoolProps.Where(p => !othersProperties.Contains(p)).ToList();

                            if (uniqueProperties.Count > 0)
                            {
                                var guardParts = new List<string>();

                                foreach (var boolProp in uniqueProperties)
                                {
                                    var umlProperty = allProperties.FirstOrDefault(x => string.Equals(x.Name, boolProp, StringComparison.OrdinalIgnoreCase));

                                    if (umlProperty != null)
                                    {
                                        guardParts.Add($"{{0}}.{umlProperty.QueryPropertyNameBasedOnUmlProperties()}");
                                    }
                                }

                                if (guardParts.Count > 0)
                                {
                                    whenGuards[current.RuleElement] = guardParts[0];
                                }
                            }
                        }

                        var elementsWithGuards = duplicateGroup.Value.Where(e => whenGuards.ContainsKey(e.RuleElement)).ToList();

                        if (elementsWithGuards.Count == duplicateGroup.Value.Count)
                        {
                            var fallbackElement = duplicateGroup.Value
                                .OrderBy(element => elementBoolProps
                                    .Single(bp => bp.RuleElement == element.RuleElement).BoolProps.Count)
                                .First();

                            whenGuards.Remove(fallbackElement.RuleElement);
                        }

                        duplicateGroup.Value.Sort((a, b) =>
                        {
                            var aHasGuard = whenGuards.ContainsKey(a.RuleElement);
                            var bHasGuard = whenGuards.ContainsKey(b.RuleElement);

                            if (aHasGuard && !bHasGuard)
                            {
                                return -1;
                            }

                            if (!aHasGuard && bHasGuard)
                            {
                                return 1;
                            }

                            return 0;
                        });
                    }

                    var hasUnresolvableDuplicates = duplicateClasses.Any(group =>
                        group.Value.Count(element => !whenGuards.ContainsKey(element.RuleElement)) > 1);

                    if (hasUnresolvableDuplicates)
                    {
                        foreach (var duplicateGroup in duplicateClasses)
                        {
                            var unguardedElements = duplicateGroup.Value
                                .Where(element => !whenGuards.ContainsKey(element.RuleElement))
                                .ToList();

                            if (unguardedElements.Count > 1)
                            {
                                for (var elementIndex = 0; elementIndex < unguardedElements.Count - 1; elementIndex++)
                                {
                                    var element = unguardedElements[elementIndex];
                                    whenGuards[element.RuleElement] = $"{{0}}.IsValidFor{element.RuleElement.Name}(writerContext)";
                                }
                            }
                        }
                    }

                    // Subtype-overlap guard synthesis: a duplicate group's unguarded fall-through case
                    // is only safe when no sibling alternative targets a SUPERTYPE of the group's class
                    // (whose dispatcher may handle this group's subtypes internally). When overlap is
                    // detected, synthesise a `when` guard from the rule's parsed body.
                    foreach (var duplicateGroup in duplicateClasses)
                    {
                        var stillUnguarded = duplicateGroup.Value
                            .Where(element => !whenGuards.ContainsKey(element.RuleElement))
                            .ToList();

                        if (stillUnguarded.Count == 0)
                        {
                            continue;
                        }

                        var groupTargetClass = duplicateGroup.Key;

                        var hasSubtypeOverlap = mappedNonTerminalElements
                            .Where(other => other.UmlClass != groupTargetClass)
                            .Any(other => groupTargetClass.QueryAllGeneralClassifiers().Contains(other.UmlClass));

                        if (!hasSubtypeOverlap)
                        {
                            continue;
                        }

                        foreach (var unguarded in stillUnguarded.Select(x => x.RuleElement))
                        {
                            var referencedRule = ruleGenerationContext.AllRules
                                .SingleOrDefault(rule => rule.RuleName == unguarded.Name);

                            if (referencedRule == null)
                            {
                                continue;
                            }

                            var synthesisedGuard = SynthesiseGuardFromRuleBody(referencedRule, groupTargetClass, umlClass.Cache, ruleGenerationContext.AllRules);

                            if (!string.IsNullOrEmpty(synthesisedGuard))
                            {
                                whenGuards[unguarded] = synthesisedGuard;
                            }
                        }
                    }

                    // Hand-coded alternative guards: an alternative whose discriminator cannot be derived
                    // from the rule body at all (it needs cursor lookahead, not a property test) is
                    // allowlisted by rule name and delegates to a hand-coded IsValidFor{Rule}.
                    foreach (var unguarded in mappedNonTerminalElements
                        .Select(element => element.RuleElement)
                        .Where(ruleElement => RequiresHandCodedAlternativeGuard(ruleElement.Name)
                            && !whenGuards.ContainsKey(ruleElement)))
                    {
                        whenGuards[unguarded] = $"{{0}}.IsValidFor{unguarded.Name}(writerContext)";
                    }

                    // Self-default guard synthesis: when the rule uses its own target class as one
                    // alternative (e.g. `FeatureElement : Feature = Feature | Step | …`), that arm is the
                    // catch-all for inline subclass forms — sibling arms need property-derived `when`
                    // guards (e.g. `DeclaredName != null`) so an anonymous subclass instance declines the
                    // match and falls through. Without the self-default shape, most-derived-first
                    // ordering alone suffices and no guards are emitted.
                    var generatingClassForSelfDefault = ruleGenerationContext.NamedElementToGenerate as IClass;
                    var isSelfDefault = generatingClassForSelfDefault != null
                        && mappedNonTerminalElements.Any(element => element.UmlClass == generatingClassForSelfDefault);

                    if (isSelfDefault)
                    {
                        var ambiguousElements = mappedNonTerminalElements
                            .Where(element => !duplicateClasses.ContainsKey(element.UmlClass))
                            .Where(element => element.UmlClass != generatingClassForSelfDefault)
                            .Where(element => !whenGuards.ContainsKey(element.RuleElement))
                            .Where(element => !IsTypeDispatcherRule(ruleGenerationContext.AllRules.SingleOrDefault(rule => rule.RuleName == element.RuleElement.Name)))
                            .ToList();

                        foreach (var ambiguous in ambiguousElements)
                        {
                            var referencedRule = ruleGenerationContext.AllRules
                                .SingleOrDefault(rule => rule.RuleName == ambiguous.RuleElement.Name);

                            if (referencedRule == null)
                            {
                                continue;
                            }

                            var clauses = CollectGuardClausesForRule(referencedRule, ambiguous.UmlClass, umlClass.Cache, ruleGenerationContext.AllRules, 3);

                            if (clauses.Count > 0)
                            {
                                whenGuards[ambiguous.RuleElement] = string.Join(" && ", clauses);
                            }
                        }
                    }

                    var reorderedElements = new List<(NonTerminalElement RuleElement, IClass UmlClass)>();
                    var processedDuplicateClasses = new HashSet<IClass>();

                    foreach (var element in mappedNonTerminalElements)
                    {
                        if (duplicateClasses.TryGetValue(element.UmlClass, out var duplicateGroup))
                        {
                            if (processedDuplicateClasses.Add(element.UmlClass))
                            {
                                reorderedElements.AddRange(duplicateGroup);
                            }
                        }
                        else
                        {
                            reorderedElements.Add(element);
                        }
                    }

                    mappedNonTerminalElements = reorderedElements;

                    var defaultElement = mappedNonTerminalElements
                        .LastOrDefault(x => x.UmlClass == ruleGenerationContext.NamedElementToGenerate && !whenGuards.ContainsKey(x.RuleElement));

                    mappedNonTerminalElements.Sort((a, b) =>
                    {
                        var aIsDefault = defaultElement.RuleElement != null && a.RuleElement == defaultElement.RuleElement;
                        var bIsDefault = defaultElement.RuleElement != null && b.RuleElement == defaultElement.RuleElement;

                        if (aIsDefault && !bIsDefault)
                        {
                            return 1;
                        }

                        if (bIsDefault && !aIsDefault)
                        {
                            return -1;
                        }

                        var depthA = a.UmlClass.QueryAllGeneralClassifiers().Count;
                        var depthB = b.UmlClass.QueryAllGeneralClassifiers().Count;

                        return depthB.CompareTo(depthA);
                    });

                    var variableName = "poco";

                    if (ruleGenerationContext.CallerRule is AssignmentElement assignmentElement)
                    {
                        var cursorToUse = ruleGenerationContext.DefinedCursors.Single(x => x.ApplicableRuleElements.Contains(assignmentElement));
                        variableName = $"{cursorToUse.CursorVariableName}.Current";
                    }

                    var generatingClass = ruleGenerationContext.NamedElementToGenerate as IClass;

                    if (variableName == "poco" && generatingClass != null)
                    {
                        var firstNonDefault = mappedNonTerminalElements.FirstOrDefault(x =>
                            defaultElement.RuleElement == null || x.RuleElement != defaultElement.RuleElement);

                        if (firstNonDefault.UmlClass != null
                            && firstNonDefault.UmlClass != generatingClass
                            && generatingClass.QueryAllGeneralClassifiers().Contains(firstNonDefault.UmlClass)
                            && !whenGuards.Any())
                        {
                            var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule?.RuleName ?? "Unknown";

                            EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext, true);

                            break;
                        }
                    }

                    var hoistedElements = CollectShadowedNestedRuleTargets(mappedNonTerminalElements, whenGuards, defaultElement.RuleElement, umlClass.Cache, ruleGenerationContext);

                    writer.WriteSafeString($"switch({variableName}){Environment.NewLine}");
                    writer.WriteSafeString("{");

                    foreach (var hoistedElement in hoistedElements)
                    {
                        var previousHoistedVariableName = ruleGenerationContext.CurrentVariableName;
                        var hoistedCaseVarName = $"poco{hoistedElement.UmlClass.Name}";
                        writer.WriteSafeString($"case {hoistedElement.UmlClass.QueryFullyQualifiedTypeName()} {hoistedCaseVarName}:{Environment.NewLine}");
                        ruleGenerationContext.CurrentVariableName = hoistedCaseVarName;

                        var previousHoistedCaller = ruleGenerationContext.CallerRule;
                        ruleGenerationContext.CallerRule = hoistedElement.RuleElement;

                        this.ProcessNonTerminalElement(writer, hoistedElement.UmlClass, hoistedElement.RuleElement, ruleGenerationContext);

                        ruleGenerationContext.CallerRule = previousHoistedCaller;
                        ruleGenerationContext.CurrentVariableName = previousHoistedVariableName;
                        writer.WriteSafeString($"{Environment.NewLine}break;{Environment.NewLine}");
                    }

                    foreach (var orderedNonTerminalElement in mappedNonTerminalElements)
                    {
                        var previousVariableName = ruleGenerationContext.CurrentVariableName;
                        var hasWhenGuard = whenGuards.TryGetValue(orderedNonTerminalElement.RuleElement, out var guardTemplate);

                        if (orderedNonTerminalElement.RuleElement == defaultElement.RuleElement && !hasWhenGuard)
                        {
                            writer.WriteSafeString($"default:{Environment.NewLine}");
                            ruleGenerationContext.CurrentVariableName = variableName;
                        }
                        else if (hasWhenGuard)
                        {
                            var guardVarName = $"poco{orderedNonTerminalElement.UmlClass.Name}{orderedNonTerminalElement.RuleElement.Name}";
                            var resolvedGuard = string.Format(guardTemplate, guardVarName);
                            writer.WriteSafeString($"case {orderedNonTerminalElement.UmlClass.QueryFullyQualifiedTypeName()} {guardVarName} when {resolvedGuard}:{Environment.NewLine}");
                            ruleGenerationContext.CurrentVariableName = guardVarName;
                        }
                        else
                        {
                            var caseVarName = $"poco{orderedNonTerminalElement.UmlClass.Name}";
                            writer.WriteSafeString($"case {orderedNonTerminalElement.UmlClass.QueryFullyQualifiedTypeName()} {caseVarName}:{Environment.NewLine}");
                            ruleGenerationContext.CurrentVariableName = caseVarName;
                        }

                        var previousCaller = ruleGenerationContext.CallerRule;
                        ruleGenerationContext.CallerRule = orderedNonTerminalElement.RuleElement;

                        this.ProcessNonTerminalElement(writer, orderedNonTerminalElement.UmlClass, orderedNonTerminalElement.RuleElement, ruleGenerationContext);

                        ruleGenerationContext.CallerRule = previousCaller;
                        ruleGenerationContext.CurrentVariableName = previousVariableName;
                        writer.WriteSafeString($"{Environment.NewLine}break;{Environment.NewLine}");
                    }

                    writer.WriteSafeString($"}}{Environment.NewLine}");

                    if (ruleGenerationContext.CallerRule is AssignmentElement assignmentElementForMove)
                    {
                        var cursorForMove = ruleGenerationContext.DefinedCursors.Single(x => x.ApplicableRuleElements.Contains(assignmentElementForMove));
                        writer.WriteSafeString($"{cursorForMove.CursorVariableName}.Move();{Environment.NewLine}");
                    }

                    break;
                }
                case AssignmentElement:
                    var assignmentElements = alternatives.SelectMany(x => x.Elements).OfType<AssignmentElement>().ToList();
                    var propertiesTarget = assignmentElements.Select(x => x.Property).Distinct().ToList();

                    if (propertiesTarget.Count == 1)
                    {
                        var targetProperty = umlClass.QueryAllProperties().Single(x => string.Equals(x.Name, propertiesTarget[0]));

                        if (assignmentElements.All(x => x.Operator == "+=") && assignmentElements.All(x => x.Value is NonTerminalElement))
                        {
                            var orderElementsByInheritance = RuleQueryUtilities.OrderElementsByInheritance(assignmentElements.Select(x => x.Value).OfType<NonTerminalElement>().ToList(), umlClass.Cache, ruleGenerationContext);

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

                                foreach (var assignmentElement in assignmentElements)
                                {
                                    existingCursor.ApplicableRuleElements.Add(assignmentElement);
                                }
                            }

                            writer.WriteSafeString($"switch({cursorVarName}.Current){Environment.NewLine}");
                            writer.WriteSafeString($"{{{Environment.NewLine}");

                            foreach (var orderedElement in orderElementsByInheritance)
                            {
                                var numberOfElementOfSameType = orderElementsByInheritance.Count(x => x.UmlClass == orderedElement.UmlClass);

                                if (numberOfElementOfSameType == 1)
                                {
                                    writer.WriteSafeString($"case {orderedElement.UmlClass.QueryFullyQualifiedTypeName()} {orderedElement.UmlClass.Name.LowerCaseFirstLetter()}:{Environment.NewLine}");
                                }
                                else
                                {
                                    writer.WriteSafeString($"case {orderedElement.UmlClass.QueryFullyQualifiedTypeName()} {orderedElement.UmlClass.Name.LowerCaseFirstLetter()} when {orderedElement.UmlClass.Name.LowerCaseFirstLetter()}.IsValidFor{orderedElement.RuleElement.Name}(writerContext):{Environment.NewLine}");
                                }

                                var previousVariableName = ruleGenerationContext.CurrentVariableName;
                                ruleGenerationContext.CurrentVariableName = orderedElement.UmlClass.Name.LowerCaseFirstLetter();
                                this.ProcessNonTerminalElement(writer, orderedElement.UmlClass, orderedElement.RuleElement, ruleGenerationContext);
                                ruleGenerationContext.CurrentVariableName = previousVariableName;
                                writer.WriteSafeString($"{Environment.NewLine}{cursorVarName}.Move();{Environment.NewLine}");
                                writer.WriteSafeString($"break;{Environment.NewLine}");
                            }

                            writer.WriteSafeString($"default:{Environment.NewLine}");
                            writer.WriteSafeString($"{cursorVarName}.Move();{Environment.NewLine}");
                            writer.WriteSafeString($"break;{Environment.NewLine}");
                            writer.WriteSafeString($"}}{Environment.NewLine}");
                        }
                        else
                        {
                            var orderElementsByInheritance = RuleQueryUtilities.OrderElementsByInheritance(assignmentElements.Select(x => x.Value).OfType<NonTerminalElement>().ToList(), umlClass.Cache, ruleGenerationContext);

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

                            writer.WriteSafeString($"if({cursorVarName}.Current != null){Environment.NewLine}");
                            writer.WriteSafeString($"{{{Environment.NewLine}");
                            writer.WriteSafeString($"switch({cursorVarName}.Current){Environment.NewLine}");
                            writer.WriteSafeString($"{{{Environment.NewLine}");

                            foreach (var orderedElement in orderElementsByInheritance)
                            {
                                if (orderedElement.UmlClass.Name == "Element")
                                {
                                    writer.WriteSafeString($"case {{ }} {orderedElement.UmlClass.Name.LowerCaseFirstLetter()}:{Environment.NewLine}");
                                }
                                else
                                {
                                    writer.WriteSafeString($"case {orderedElement.UmlClass.QueryFullyQualifiedTypeName()} {orderedElement.UmlClass.Name.LowerCaseFirstLetter()}:{Environment.NewLine}");
                                }

                                var previousVariableName = ruleGenerationContext.CurrentVariableName;
                                ruleGenerationContext.CurrentVariableName = orderedElement.UmlClass.Name.LowerCaseFirstLetter();
                                this.ProcessNonTerminalElement(writer, orderedElement.UmlClass, orderedElement.RuleElement, ruleGenerationContext);
                                ruleGenerationContext.CurrentVariableName = previousVariableName;
                                writer.WriteSafeString($"{Environment.NewLine}break;{Environment.NewLine}");
                            }

                            writer.WriteSafeString($"}}{Environment.NewLine}");
                            writer.WriteSafeString($"{cursorVarName}.Move();{Environment.NewLine}");
                            writer.WriteSafeString($"}}{Environment.NewLine}");
                        }
                    }
                    else
                    {
                        foreach (var alternative in alternatives)
                        {
                            this.DeclareAllRequiredCursors(writer, umlClass, alternative, ruleGenerationContext);
                        }

                        var properties = umlClass.QueryAllProperties();
                        var orderedAssignments = OrderAssignmentsByImplicationConstraints(assignmentElements, umlClass);

                        for (var alternativeIndex = 0; alternativeIndex < alternatives.Count; alternativeIndex++)
                        {
                            if (alternativeIndex != 0)
                            {
                                writer.WriteSafeString("else ");
                            }

                            var assignment = orderedAssignments[alternativeIndex];
                            var targetProperty = properties.Single(x => string.Equals(x.Name, assignment.Property));

                            var iterator = ruleGenerationContext.DefinedCursors.SingleOrDefault(x => x.ApplicableRuleElements.Contains(assignment));

                            if (assignment.Operator != "+=")
                            {
                                writer.WriteSafeString($"if({targetProperty.QueryIfStatementContentForNonEmpty(iterator?.CursorVariableName ?? "poco")}){Environment.NewLine}");
                            }

                            writer.WriteSafeString($"{{{Environment.NewLine}");
                            this.ProcessAssignmentElement(writer, umlClass, ruleGenerationContext, assignment, true);
                            writer.WriteSafeString($"{Environment.NewLine}}}{Environment.NewLine}");
                        }
                    }

                    break;
                default:
                {
                    var defaultHandCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                    EmitHandCodedFallback(writer, defaultHandCodedRuleName, ruleGenerationContext);
                    break;
                }
            }
        }

        /// <summary>
        /// Matches an OCL constraint body of the shape <c>&lt;antecedent&gt; implies &lt;consequent&gt;</c>
        /// where both operands are bare property names.
        /// </summary>
        /// <returns>The compile-time generated <see cref="Regex" />.</returns>
        [GeneratedRegex(@"^\s*(\w+)\s+implies\s+(\w+)\s*$", RegexOptions.None, MatchTimeoutMilliseconds)]
        private static partial Regex SimpleImplicationConstraint();

        /// <summary>
        /// Reorders mutually-exclusive boolean <c>?=</c> assignment alternatives so that, whenever the
        /// rule's target class carries an OCL invariant of the shape <c>A implies B</c> over two of the
        /// assigned properties, the implying property <c>A</c> is tested before the implied property
        /// <c>B</c>.
        /// </summary>
        /// <remarks>
        /// A grammar alternation such as <c>( isAbstract ?= 'abstract' | isVariation ?= 'variation' )?</c>
        /// compiles to an <c>if</c>/<c>else if</c> chain in grammar order. When the model satisfies
        /// <c>isVariation implies isAbstract</c> (SysML v2 <c>validateDefinitionVariationIsAbstract</c> /
        /// <c>validateUsageVariationIsAbstract</c>) a variation always carries <c>isAbstract</c> too, so the
        /// grammar-ordered chain would always take the <c>abstract</c> arm and the stronger <c>variation</c>
        /// keyword would never be emitted. Testing the antecedent first keeps the chain exhaustive while
        /// selecting the most specific keyword. Ordering is stable — alternatives not related by an
        /// implication keep their grammar order.
        /// </remarks>
        /// <param name="assignmentElements">The single-element alternatives' <see cref="AssignmentElement"/>s, in grammar order</param>
        /// <param name="umlClass">The rule's target <see cref="IClass"/>, whose invariants (own and inherited) are consulted</param>
        /// <returns>The reordered assignments; the input order when no implication applies</returns>
        private static List<AssignmentElement> OrderAssignmentsByImplicationConstraints(List<AssignmentElement> assignmentElements, IClass umlClass)
        {
            if (assignmentElements.Count < 2 || umlClass == null)
            {
                return assignmentElements;
            }

            var ordered = new List<AssignmentElement>(assignmentElements);

            var namespaces = umlClass.QueryAllGeneralClassifiers()
                .OfType<INamespace>()
                .Prepend(umlClass);

            var implications = namespaces
                .SelectMany(owner => owner.OwnedRule)
                .SelectMany(rule => rule.Specification?.OfType<IOpaqueExpression>() ?? [])
                .SelectMany(specification => specification.Body ?? [])
                .Select(body => SimpleImplicationConstraint().Match(body ?? string.Empty))
                .Where(match => match.Success)
                .Select(match => (Antecedent: match.Groups[1].Value, Consequent: match.Groups[2].Value));

            foreach (var implication in implications)
            {
                var antecedentIndex = ordered.FindIndex(assignment => IsBooleanAssignmentTo(assignment, implication.Antecedent));
                var consequentIndex = ordered.FindIndex(assignment => IsBooleanAssignmentTo(assignment, implication.Consequent));

                if (antecedentIndex < 0 || consequentIndex < 0 || antecedentIndex < consequentIndex)
                {
                    continue;
                }

                var antecedentAssignment = ordered[antecedentIndex];
                ordered.RemoveAt(antecedentIndex);
                ordered.Insert(consequentIndex, antecedentAssignment);
            }

            return ordered;
        }

        /// <summary>
        /// Determines whether <paramref name="assignment"/> is a boolean-flag assignment
        /// (<c>?=</c>) targeting the property named <paramref name="propertyName"/>.
        /// </summary>
        /// <param name="assignment">The <see cref="AssignmentElement"/> to test</param>
        /// <param name="propertyName">The property name an OCL implication operand refers to</param>
        /// <returns><c>true</c> when the assignment is a <c>?=</c> assignment to that property</returns>
        private static bool IsBooleanAssignmentTo(AssignmentElement assignment, string propertyName)
        {
            return string.Equals(assignment.Operator, "?=", StringComparison.Ordinal)
                   && string.Equals(assignment.Property, propertyName, StringComparison.Ordinal);
        }

        /// <summary>
        /// Emits a single type-dispatched branch body.
        /// </summary>
        private static void EmitCompoundPocoTypeBranch(EncodedTextWriter writer, IClass umlClass, NonTerminalElement leadingNonTerminal, Alternatives alternative, IClass targetClass, string variableName)
        {
            string builderCall;

            if (targetClass == umlClass)
            {
                builderCall = $"Build{leadingNonTerminal.Name}({variableName}, writerContext, stringBuilder);";
            }
            else
            {
                builderCall = $"{targetClass.Name}TextualNotationBuilder.Build{leadingNonTerminal.Name}({variableName}, writerContext, stringBuilder);";
            }

            writer.WriteSafeString($"{builderCall}{Environment.NewLine}");

            for (var elementIndex = 1; elementIndex < alternative.Elements.Count; elementIndex++)
            {
                if (alternative.Elements[elementIndex] is TerminalElement trailingTerminal)
                {
                    TerminalWriter.WriteTerminalAppend(writer, trailingTerminal.Value);
                    writer.WriteSafeString(Environment.NewLine);
                }
            }
        }

        /// <summary>
        /// Synthesises a <c>when</c>-guard template (with <c>{0}</c> as the case-variable placeholder)
        /// from the rule's parsed body, one predicate per <see cref="AssignmentElement"/>. Non-parsing
        /// <c>{ prop = X }</c> assignments are ignored per GRAMMAR.md. Returns <c>null</c> when the body
        /// carries no usable parsed assignments.
        /// </summary>
        /// <param name="rule">The <see cref="TextualNotationRule"/> whose body to inspect</param>
        /// <param name="targetClass">The duplicate group's target <see cref="IClass"/></param>
        /// <param name="cache">The <see cref="IXmiElementCache"/> used to resolve referenced rule targets</param>
        /// <param name="allRules">All available rules for NonTerminal resolution</param>
        /// <returns>The guard template, or <c>null</c>.</returns>
        private static string SynthesiseGuardFromRuleBody(TextualNotationRule rule, IClass targetClass, IXmiElementCache cache, IReadOnlyList<TextualNotationRule> allRules, int maxDepth = 0)
        {
            if (rule == null || targetClass == null)
            {
                return null;
            }

            var clauses = CollectGuardClausesForRule(rule, targetClass, cache, allRules, maxDepth);

            return clauses.Count == 0 ? null : string.Join(" && ", clauses);
        }

        /// <summary>
        /// Determines whether <paramref name="rule"/>'s body is exclusively a union of
        /// single-NonTerminal alternatives (<c>Subclass1 | Subclass2 | …</c>). Such a rule routes
        /// subclasses internally (e.g. <c>LiteralExpression</c>), so its switch arm needs no guard —
        /// unlike a concrete construction rule that renders only the base form.
        /// </summary>
        /// <param name="rule">The rule to test, or <c>null</c></param>
        /// <returns><c>true</c> when <paramref name="rule"/> is a multi-alternative type-dispatcher</returns>
        private static bool IsTypeDispatcherRule(TextualNotationRule rule)
        {
            if (rule == null || rule.Alternatives.Count < 2)
            {
                return false;
            }

            return rule.Alternatives.All(alternative =>
                alternative.Elements.Count == 1
                && alternative.Elements[0] is NonTerminalElement);
        }

        /// <summary>
        /// Collects the switch arms that must be hoisted ABOVE the depth-sorted arms so a metaclass that is
        /// only reachable THROUGH a nested alternation rule is not captured by an earlier arm typed on one
        /// of its supertypes.
        /// <para>The depth sort ranks a nested-rule arm by the RULE's own declared target (e.g.
        /// <c>BehaviorUsageElement : Usage</c>, shallow), never by the deepest metaclass reachable through it
        /// (e.g. <c>PerformActionUsage</c>, deep) — and when that target IS the generating class the arm is
        /// forced last as the <c>default:</c> case, where no sort key can rescue it. C# pattern matching is
        /// first-match-wins on runtime type, so <c>case IEventOccurrenceUsage</c> would swallow every
        /// <c>PerformActionUsage</c> before the <c>BehaviorUsageElement</c> arm is ever reached.</para>
        /// <para>Only UNGUARDED arms are treated as shadowing: a <c>when</c>-guarded arm declines runtime
        /// types its guard excludes, which is precisely what those guards exist for.</para>
        /// </summary>
        /// <param name="orderedElements">The depth-sorted arms, in emission order</param>
        /// <param name="whenGuards">The synthesized <c>when</c> guards, keyed by rule element</param>
        /// <param name="defaultRuleElement">The rule element emitted as <c>default:</c>, or <c>null</c></param>
        /// <param name="cache">The <see cref="IXmiElementCache" /> used to resolve rule targets</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <returns>The arms to emit first, most-derived first; empty when no arm is shadowed</returns>
        private static List<(NonTerminalElement RuleElement, IClass UmlClass)> CollectShadowedNestedRuleTargets(List<(NonTerminalElement RuleElement, IClass UmlClass)> orderedElements, Dictionary<NonTerminalElement, string> whenGuards, NonTerminalElement defaultRuleElement, IXmiElementCache cache, RuleGenerationContext ruleGenerationContext)
        {
            var hoisted = new List<(NonTerminalElement RuleElement, IClass UmlClass)>();

            for (var elementIndex = 0; elementIndex < orderedElements.Count; elementIndex++)
            {
                var element = orderedElements[elementIndex];
                var reachableClasses = QueryReachableTargetClasses(element.RuleElement, cache, ruleGenerationContext.AllRules);

                foreach (var reachableClass in reachableClasses.Where(reachableClass => reachableClass != element.UmlClass))
                {
                    var shadowingArm = orderedElements
                        .Take(elementIndex)
                        .Where(arm => !whenGuards.ContainsKey(arm.RuleElement) && arm.RuleElement != defaultRuleElement)
                        .FirstOrDefault(arm => reachableClass.QueryAllGeneralClassifiers().Contains(arm.UmlClass));

                    if (shadowingArm.RuleElement == null)
                    {
                        continue;
                    }

                    if (QueryReachableTargetClasses(shadowingArm.RuleElement, cache, ruleGenerationContext.AllRules).Contains(reachableClass))
                    {
                        continue;
                    }

                    // Hoisted arms are emitted at the very top of the switch, so a hoist is only safe when no
                    // existing arm is typed on a STRICT SUBTYPE of the hoisted class — hoisting above such an
                    // arm would trade one shadowing defect for another.
                    if (orderedElements.Any(arm => arm.UmlClass != reachableClass && arm.UmlClass.QueryAllGeneralClassifiers().Contains(reachableClass)))
                    {
                        continue;
                    }

                    if (hoisted.All(alreadyHoisted => alreadyHoisted.UmlClass != reachableClass))
                    {
                        hoisted.Add((element.RuleElement, reachableClass));
                    }
                }
            }

            hoisted.Sort((a, b) => b.UmlClass.QueryAllGeneralClassifiers().Count.CompareTo(a.UmlClass.QueryAllGeneralClassifiers().Count));

            return hoisted;
        }

        /// <summary>
        /// Resolves every UML <see cref="IClass" /> that <paramref name="nonTerminal" /> can dispatch to,
        /// flattening nested type-dispatcher rules transitively.
        /// </summary>
        /// <param name="nonTerminal">The <see cref="NonTerminalElement" /> to resolve</param>
        /// <param name="cache">The <see cref="IXmiElementCache" /> used to resolve rule targets</param>
        /// <param name="allRules">All available rules for recursive lookup</param>
        /// <returns>The reachable classes, including the non-terminal's own target class</returns>
        private static List<IClass> QueryReachableTargetClasses(NonTerminalElement nonTerminal, IXmiElementCache cache, IReadOnlyList<TextualNotationRule> allRules)
        {
            var reachableClasses = new List<IClass>();
            CollectReachableTargetClasses(nonTerminal.Name, cache, allRules, reachableClasses, new HashSet<string>(StringComparer.Ordinal));

            return reachableClasses;
        }

        /// <summary>
        /// Recursively accumulates the target classes reachable from <paramref name="ruleName" />, descending
        /// only into rules that are pure type-dispatchers (see <see cref="IsTypeDispatcherRule" />).
        /// </summary>
        /// <param name="ruleName">The non-terminal rule name to resolve</param>
        /// <param name="cache">The <see cref="IXmiElementCache" /> used to resolve rule targets</param>
        /// <param name="allRules">All available rules for recursive lookup</param>
        /// <param name="reachableClasses">The accumulated classes</param>
        /// <param name="visitedRules">The already-visited rule names, preventing infinite recursion</param>
        private static void CollectReachableTargetClasses(string ruleName, IXmiElementCache cache, IReadOnlyList<TextualNotationRule> allRules, List<IClass> reachableClasses, HashSet<string> visitedRules)
        {
            if (!visitedRules.Add(ruleName))
            {
                return;
            }

            var rule = allRules.SingleOrDefault(x => x.RuleName == ruleName);
            var targetClass = RuleQueryUtilities.FindClass(cache, rule?.EffectiveTarget ?? ruleName);

            if (targetClass != null && !reachableClasses.Contains(targetClass))
            {
                reachableClasses.Add(targetClass);
            }

            if (rule == null || !IsTypeDispatcherRule(rule))
            {
                return;
            }

            foreach (var nestedNonTerminal in rule.Alternatives.Select(alternative => alternative.Elements[0]).OfType<NonTerminalElement>())
            {
                CollectReachableTargetClasses(nestedNonTerminal.Name, cache, allRules, reachableClasses, visitedRules);
            }
        }

        /// <summary>
        /// Runs the structural-predicate walk for <paramref name="rule"/> and returns the raw clause
        /// list, seeding the per-walk visited-rules set and cursor-state bookkeeping.
        /// </summary>
        /// <param name="rule">The entry <see cref="TextualNotationRule"/> to walk.</param>
        /// <param name="targetClass">The dispatch-time target <see cref="IClass"/>.</param>
        /// <param name="cache">The <see cref="IXmiElementCache"/> for resolving NonTerminal RHS targets.</param>
        /// <param name="allRules">All available rules for NonTerminal resolution.</param>
        /// <param name="maxDepth">Recursion budget: 0 = shallow, N&gt;0 = recurse N references deep.</param>
        /// <returns>The per-clause list (unjoined) in source order.</returns>
        private static List<string> CollectGuardClausesForRule(TextualNotationRule rule, IClass targetClass, IXmiElementCache cache, IReadOnlyList<TextualNotationRule> allRules, int maxDepth)
        {
            var targetProperties = targetClass.QueryAllProperties();
            var perAlternativeClauses = new List<List<string>>();

            foreach (var alternative in rule.Alternatives)
            {
                var altClauses = new List<string>();
                var firstCursorEmitted = false;
                var cursorMayHaveAdvanced = false;
                var visitedRules = new HashSet<string>(StringComparer.Ordinal);

                if (!string.IsNullOrEmpty(rule.RuleName))
                {
                    visitedRules.Add(rule.RuleName);
                }

                CollectGuardClauses(alternative.Elements, targetProperties, cache, allRules, altClauses, ref firstCursorEmitted, ref cursorMayHaveAdvanced, visitedRules, maxDepth);

                perAlternativeClauses.Add(altClauses);
            }

            var combined = CombineAlternativesAsOr(perAlternativeClauses);

            return combined == null
                ? new List<string>()
                : new List<string> { combined };
        }

        /// <summary>
        /// Combines per-alternative clause lists into one predicate: each alternative's clauses joined
        /// by <c>&amp;&amp;</c>, alternatives joined by <c>||</c> (exactly one parses at runtime).
        /// Empty alternatives are dropped and identical ones collapse.
        /// </summary>
        /// <param name="perAlternativeClauses">One list of synthesized clauses per grammar alternative.</param>
        /// <returns>A single combined predicate string, or <c>null</c> when every alternative is empty.</returns>
        private static string CombineAlternativesAsOr(List<List<string>> perAlternativeClauses)
        {
            var nonEmpty = perAlternativeClauses
                .Where(altClauses => altClauses.Count > 0)
                .Select(altClauses => altClauses.Count == 1
                    ? altClauses[0]
                    : "(" + string.Join(" && ", altClauses) + ")")
                .ToList();

            if (nonEmpty.Count == 0)
            {
                return null;
            }

            var distinct = nonEmpty.Distinct(StringComparer.Ordinal).ToList();

            if (distinct.Count == 1)
            {
                return distinct[0];
            }

            return "(" + string.Join(" || ", distinct) + ")";
        }

        /// <summary>
        /// Recursively walks a sequence of <see cref="RuleElement"/> values produced by the rule
        /// parser, accumulating one structural-predicate clause per parsed
        /// <see cref="AssignmentElement"/> via <see cref="TryBuildClauseForAssignment"/>.
        /// </summary>
        /// <param name="elements">The elements to walk</param>
        /// <param name="targetProperties">Properties of the duplicate group's target class</param>
        /// <param name="cache">The <see cref="IXmiElementCache"/> used to resolve referenced rule targets</param>
        /// <param name="allRules">All available rules for NonTerminal resolution</param>
        /// <param name="clauses">Accumulator into which non-null clauses are appended in source order</param>
        /// <param name="firstCursorEmitted">Whether an <c>ownedRelationship</c> cursor predicate was already emitted — only the first assignment can be expressed at dispatch time</param>
        /// <param name="cursorMayHaveAdvanced">Whether a prior element may have advanced the dispatch cursor, invalidating position-0 predicates for the rest of the walk</param>
        private static void CollectGuardClauses(IEnumerable<RuleElement> elements, IEnumerable<IProperty> targetProperties, IXmiElementCache cache, IReadOnlyList<TextualNotationRule> allRules, List<string> clauses, ref bool firstCursorEmitted, ref bool cursorMayHaveAdvanced, HashSet<string> visitedRules, int remainingDepth)
        {
            foreach (var element in elements)
            {
                switch (element)
                {
                    case AssignmentElement assignment:
                    {
                        // Optional assignments may legitimately be unset on a valid POCO — only
                        // mandatory ones yield guard clauses.
                        if (assignment.IsOptional)
                        {
                            break;
                        }

                        var clause = TryBuildClauseForAssignment(assignment, targetProperties, cache, allRules, ref firstCursorEmitted, cursorMayHaveAdvanced);

                        if (!string.IsNullOrEmpty(clause))
                        {
                            clauses.Add(clause);
                        }

                        if (assignment.Operator == "+="
                            && string.Equals(assignment.Property, "ownedRelationship", StringComparison.OrdinalIgnoreCase))
                        {
                            cursorMayHaveAdvanced = true;
                        }

                        break;
                    }

                    case NonTerminalElement nonTerminal:
                    {
                        // Deep walk while depth budget lasts: descend into unvisited referenced rules,
                        // OR-combining per-alternative clauses. Any walked NonTerminal may advance the
                        // dispatch cursor, so later cursor predicates are suppressed.
                        if (remainingDepth > 0)
                        {
                            var referencedRule = allRules.SingleOrDefault(rule => rule.RuleName == nonTerminal.Name);

                            if (referencedRule != null && visitedRules.Add(nonTerminal.Name))
                            {
                                cursorMayHaveAdvanced = true;

                                var perReferencedAlternativeClauses = new List<List<string>>();

                                foreach (var referencedAlternative in referencedRule.Alternatives)
                                {
                                    var altClauses = new List<string>();
                                    CollectGuardClauses(referencedAlternative.Elements, targetProperties, cache, allRules, altClauses, ref firstCursorEmitted, ref cursorMayHaveAdvanced, visitedRules, remainingDepth - 1);
                                    perReferencedAlternativeClauses.Add(altClauses);
                                }

                                var combinedReferenced = CombineAlternativesAsOr(perReferencedAlternativeClauses);

                                if (!string.IsNullOrEmpty(combinedReferenced))
                                {
                                    clauses.Add(combinedReferenced);
                                }

                                break;
                            }
                        }

                        // Depth exhausted, rule not found, or already visited — conservatively
                        // suppress later cursor predicates.
                        cursorMayHaveAdvanced = true;
                        break;
                    }

                    case GroupElement group:
                    {
                        // Optional groups may not fire at parse time — only walk groups guaranteed to
                        // execute (no suffix or `+`).
                        if (group.IsOptional)
                        {
                            break;
                        }

                        // OR-combine per branch; cursor state is shared across branches since any
                        // branch may consume the dispatch cursor.
                        var perGroupAlternativeClauses = new List<List<string>>();

                        foreach (var groupAlternative in group.Alternatives)
                        {
                            var altClauses = new List<string>();
                            CollectGuardClauses(groupAlternative.Elements, targetProperties, cache, allRules, altClauses, ref firstCursorEmitted, ref cursorMayHaveAdvanced, visitedRules, remainingDepth);
                            perGroupAlternativeClauses.Add(altClauses);
                        }

                        var combined = CombineAlternativesAsOr(perGroupAlternativeClauses);

                        if (!string.IsNullOrEmpty(combined))
                        {
                            clauses.Add(combined);
                        }

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Builds a single <c>when</c>-clause predicate for one <see cref="AssignmentElement"/>,
        /// per the synthesis table in <c>SysML2.NET.CodeGenerator/GRAMMAR.md</c>:
        /// <list type="bullet">
        ///   <item><c>prop = 'literal'</c> → <c>{0}.{Prop} == "literal"</c></item>
        ///   <item><c>prop = [QualifiedName]</c> → <c>{0}.{Prop} != null</c></item>
        ///   <item><c>prop = NonTerminal</c> → <c>{0}.{Prop} is I{RHS-target}</c> (narrows when possible, else <c>!= null</c>)</item>
        ///   <item><c>ownedRelationship += NonTerminal</c> (first occurrence) → cursor predicate</item>
        ///   <item><c>prop += NonTerminal</c> for any other collection → <c>{0}.{Prop}.OfType&lt;I{RHS-target}&gt;().Any()</c></item>
        ///   <item><c>prop ?= 'kw'</c> → <c>null</c> (already handled by the boolean discriminator pass)</item>
        /// </list>
        /// </summary>
        /// <param name="assignment">The <see cref="AssignmentElement"/> to translate</param>
        /// <param name="targetProperties">Properties of the duplicate group's target class</param>
        /// <param name="cache">The <see cref="IXmiElementCache"/> used to resolve referenced rule targets</param>
        /// <param name="allRules">All available rules for NonTerminal resolution</param>
        /// <param name="firstCursorEmitted">Tracks whether a cursor predicate has been emitted yet for this rule.</param>
        /// <param name="cursorMayHaveAdvanced">When true, suppresses any new cursor predicate because cursor position 0 no longer corresponds to the upcoming <c>ownedRelationship +=</c>.</param>
        /// <returns>A <see cref="string.Format(string, object?)"/> template clause, or <c>null</c> when no clause applies.</returns>
        private static string TryBuildClauseForAssignment(AssignmentElement assignment, IEnumerable<IProperty> targetProperties, IXmiElementCache cache, IReadOnlyList<TextualNotationRule> allRules, ref bool firstCursorEmitted, bool cursorMayHaveAdvanced)
        {
            if (assignment?.Property == null || assignment.Operator == "?=")
            {
                return null;
            }

            var matchingProperty = targetProperties.FirstOrDefault(property => string.Equals(property.Name, assignment.Property, StringComparison.OrdinalIgnoreCase));

            if (matchingProperty == null)
            {
                // The deep walk can cross into rules whose properties do not exist on the OUTER class
                // (e.g. Comment's `body`) — emitting `{0}.{Prop}` would not compile. Skip the clause.
                return null;
            }

            // Likewise a `+=` property may be a SCALAR on the outer class (e.g. ISubsetting.Specific);
            // an OfType<…>().Any() clause would not compile there.
            if (assignment.Operator == "+=" && !matchingProperty.QueryIsEnumerable())
            {
                return null;
            }

            var propertyAccessor = matchingProperty.QueryPropertyNameBasedOnUmlProperties();

            switch (assignment.Operator)
            {
                case "=":
                    return BuildScalarAssignmentClause(assignment, matchingProperty, propertyAccessor, cache, allRules);
                case "+=":
                    return BuildCollectionAssignmentClause(assignment, propertyAccessor, cache, allRules, ref firstCursorEmitted, cursorMayHaveAdvanced);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Determines whether <paramref name="property"/> can hold <c>null</c> in C# — used to suppress
        /// tautological <c>!= null</c> guards on non-nullable value types.
        /// </summary>
        /// <param name="property">The <see cref="IProperty"/> resolved against the outer alternative class.</param>
        /// <returns><c>true</c> if a runtime instance of the property could be <c>null</c>.</returns>
        private static bool IsPropertyNullableInCSharp(IProperty property)
        {
            return property.QueryIsReferenceType()
                   || property.QueryIsNullableAndNotString()
                   || property.QueryIsString();
        }

        /// <summary>
        /// Translates a scalar <c>=</c> assignment into a <c>when</c> predicate: literal RHS → equality,
        /// <c>[QualifiedName]</c> → non-null, NonTerminal → type narrowing. Predicates that would be
        /// tautological or uncompilable on non-nullable value types are dropped.
        /// </summary>
        /// <param name="assignment">The <see cref="AssignmentElement"/> with <see cref="AssignmentElement.Operator"/> <c>=</c></param>
        /// <param name="matchingProperty">The <see cref="IProperty"/> on the outer alternative class that this assignment binds — consulted for C# nullability.</param>
        /// <param name="propertyAccessor">The C# property name resolved via <see cref="PropertyExtension.QueryPropertyNameBasedOnUmlProperties"/></param>
        /// <param name="cache">The <see cref="IXmiElementCache"/> for resolving NonTerminal RHS targets</param>
        /// <param name="allRules">All available rules for NonTerminal resolution</param>
        /// <returns>A template clause, or <c>null</c> when no useful clause can be synthesised.</returns>
        private static string BuildScalarAssignmentClause(AssignmentElement assignment, IProperty matchingProperty, string propertyAccessor, IXmiElementCache cache, IReadOnlyList<TextualNotationRule> allRules)
        {
            switch (assignment.Value)
            {
                case TerminalElement terminal when !string.IsNullOrEmpty(terminal.Value):
                    return $"{{0}}.{propertyAccessor} == \"{terminal.Value}\"";

                case ValueLiteralElement valueLiteral when valueLiteral.QueryIsQualifiedName():
                    return IsPropertyNullableInCSharp(matchingProperty)
                        ? $"{{0}}.{propertyAccessor} != null"
                        : null;

                case NonTerminalElement nonTerminal:
                {
                    var rhsTargetClass = RuleQueryUtilities.ResolveRuleTargetClass(nonTerminal, cache, allRules);

                    if (rhsTargetClass != null)
                    {
                        // `is I{Rhs}` needs a reference-typed property to compile.
                        return matchingProperty.QueryIsReferenceType()
                            ? $"{{0}}.{propertyAccessor} is {rhsTargetClass.QueryFullyQualifiedTypeName()}"
                            : null;
                    }

                    return IsPropertyNullableInCSharp(matchingProperty)
                        ? $"{{0}}.{propertyAccessor} != null"
                        : null;
                }

                default:
                    return null;
            }
        }

        /// <summary>
        /// Translates a collection <c>+=</c> assignment into a cursor-based <c>when</c> predicate
        /// (<c>GetOrCreateCursor(…).Current is …</c>). For <c>ownedRelationship</c> the predicate is
        /// suppressed once the dispatch cursor may have advanced past position 0.
        /// </summary>
        /// <param name="assignment">The <see cref="AssignmentElement"/> with operator <c>+=</c></param>
        /// <param name="propertyAccessor">The resolved C# collection-property name</param>
        /// <param name="cache">The <see cref="IXmiElementCache"/> for resolving NonTerminal RHS targets</param>
        /// <param name="allRules">All available rules for NonTerminal resolution</param>
        /// <param name="firstCursorEmitted">Whether the first <c>ownedRelationship</c> cursor predicate was already emitted</param>
        /// <param name="cursorMayHaveAdvanced">When true, suppresses <c>ownedRelationship</c> cursor predicates (stale position)</param>
        /// <returns>A template clause, or <c>null</c> when no useful clause can be synthesised.</returns>
        private static string BuildCollectionAssignmentClause(AssignmentElement assignment, string propertyAccessor, IXmiElementCache cache, IReadOnlyList<TextualNotationRule> allRules, ref bool firstCursorEmitted, bool cursorMayHaveAdvanced)
        {
            if (assignment.Value is not NonTerminalElement nonTerminal)
            {
                return null;
            }

            var rhsTargetClass = RuleQueryUtilities.ResolveRuleTargetClass(nonTerminal, cache, allRules);

            if (rhsTargetClass == null)
            {
                return null;
            }

            var rhsTargetTypeName = rhsTargetClass.QueryFullyQualifiedTypeName();

            if (string.Equals(assignment.Property, "ownedRelationship", StringComparison.OrdinalIgnoreCase))
            {
                if (firstCursorEmitted || cursorMayHaveAdvanced)
                {
                    return null;
                }

                firstCursorEmitted = true;
            }

            return $"writerContext.CursorCache.GetOrCreateCursor({{0}}.Id, \"{assignment.Property}\", {{0}}.{propertyAccessor}).Current is {rhsTargetTypeName}";
        }
    }
}
