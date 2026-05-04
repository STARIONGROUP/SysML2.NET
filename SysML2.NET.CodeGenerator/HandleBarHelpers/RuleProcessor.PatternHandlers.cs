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

    using HandlebarsDotNet;

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.Grammar.Model;

    using uml4net.CommonStructure;
    using uml4net.Extensions;
    using uml4net.StructuredClassifiers;

    /// <summary>
    /// Pattern detection and specialized code emission for grammar alternatives
    /// </summary>
    internal sealed partial class RuleProcessor
    {
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
                DeclareAllRequiredCursors(writer, umlClass, alternative, ruleGenerationContext);
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
                    ProcessRuleElement(writer, umlClass, alternative.Elements[elementIndex], ruleGenerationContext);
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
                DeclareAllRequiredCursors(writer, umlClass, alternative, ruleGenerationContext);
            }

            var cursor = ruleGenerationContext.DefinedCursors.FirstOrDefault(c => c.ApplicableRuleElements.Contains(emptyBranch.Assignment));

            if (cursor == null)
            {
                return false;
            }

            var typeName = emptyTarget.QueryFullyQualifiedTypeName();
            var cursorVarName = cursor.CursorVariableName;

            writer.WriteSafeString($"if ({cursorVarName}.Current is {typeName} {{ {discriminatorPropertyName}.Count: 0 }}){Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");
            EmitAlternativeBody(writer, umlClass, emptyBranch.Alternative, ruleGenerationContext);
            writer.WriteSafeString($"}}{Environment.NewLine}");
            writer.WriteSafeString($"else if ({cursorVarName}.Current is {typeName}){Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");
            EmitAlternativeBody(writer, umlClass, nonEmptyBranch.Alternative, ruleGenerationContext);
            writer.WriteSafeString($"}}{Environment.NewLine}");

            return true;
        }

        /// <summary>
        /// Pattern C: detects poco-runtime-type dispatch with compound alternatives.
        /// </summary>
        private bool TryHandlePocoTypeDispatchWithCompoundAlternatives(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
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
                .OrderByDescending(branch => branch.TargetClass.QueryAllGeneralClassifiers().Count())
                .ToList();

            writer.WriteSafeString($"switch (poco){Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");

            foreach (var (alternative, leadingNonTerminal, targetClass) in orderedBranches)
            {
                var caseVarName = $"poco{targetClass.Name}";
                writer.WriteSafeString($"case {targetClass.QueryFullyQualifiedTypeName()} {caseVarName}:{Environment.NewLine}");
                EmitCompoundPocoTypeBranch(writer, umlClass, leadingNonTerminal, alternative, targetClass, caseVarName, ruleGenerationContext);
                writer.WriteSafeString($"break;{Environment.NewLine}");
            }

            if (defaultBranch.NonTerminal != null)
            {
                writer.WriteSafeString($"default:{Environment.NewLine}");
                EmitCompoundPocoTypeBranch(writer, umlClass, defaultBranch.NonTerminal, defaultBranch.Alternative, defaultBranch.TargetClass, "poco", ruleGenerationContext);
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

            DeclareAllRequiredCursors(writer, umlClass, alt1, ruleGenerationContext);

            var variableName = ruleGenerationContext.CurrentVariableName ?? "poco";
            var collectionPropertyAccessor = collectionProperty.QueryPropertyNameBasedOnUmlProperties();
            var referenceTypeName = referenceTargetClass.QueryFullyQualifiedTypeName();

            writer.WriteSafeString($"if ({variableName}.{collectionPropertyAccessor}.OfType<{referenceTypeName}>().Any()){Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");
            EmitAlternativeBody(writer, umlClass, alt1, ruleGenerationContext);
            writer.WriteSafeString($"}}{Environment.NewLine}");
            writer.WriteSafeString($"else{Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");
            EmitAlternativeBody(writer, umlClass, alt2, ruleGenerationContext);
            writer.WriteSafeString($"}}{Environment.NewLine}");

            return true;
        }

        /// <summary>
        /// Validates that tail elements of an alternative resolve to existing target classes.
        /// </summary>
        private bool AreAlternativeTailElementsProcessable(IReadOnlyList<RuleElement> elements, IClass umlClass, RuleGenerationContext ruleGenerationContext)
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
        private List<string> ExtractLiteralAlternation(RuleElement value, IReadOnlyList<TextualNotationRule> allRules)
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

                    foreach (var ruleAlternative in referencedRule.Alternatives)
                    {
                        if (ruleAlternative.Elements.Count != 1 || ruleAlternative.Elements[0] is not TerminalElement nestedTerminal)
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
        private void ProcessMultiCollectionAssignment(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext)
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

                        foreach (var element in duplicateGroup.Value)
                        {
                            var referencedRule = ruleGenerationContext.AllRules.Single(x => x.RuleName == element.RuleElement.Name);
                            var booleanProperties = RuleQueryUtilities.QueryBooleanAssignmentProperties(referencedRule, ruleGenerationContext.AllRules);
                            elementBoolProps.Add((element.RuleElement, booleanProperties));
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
                                    whenGuards[element.RuleElement] = $"{{0}}.IsValidFor{element.RuleElement.Name}()";
                                }
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

                        var depthA = a.UmlClass.QueryAllGeneralClassifiers().Count();
                        var depthB = b.UmlClass.QueryAllGeneralClassifiers().Count();

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

                            EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext, deduplicate: true);

                            break;
                        }
                    }

                    writer.WriteSafeString($"switch({variableName}){Environment.NewLine}");
                    writer.WriteSafeString("{");

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

                        ProcessNonTerminalElement(writer, orderedNonTerminalElement.UmlClass, orderedNonTerminalElement.RuleElement, ruleGenerationContext);

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
                                writer.WriteSafeString($"var {cursorVarName} = cursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{targetProperty.QueryPropertyNameBasedOnUmlProperties()});{Environment.NewLine}");
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
                                    writer.WriteSafeString($"case {orderedElement.UmlClass.QueryFullyQualifiedTypeName()} {orderedElement.UmlClass.Name.LowerCaseFirstLetter()} when {orderedElement.UmlClass.Name.LowerCaseFirstLetter()}.IsValidFor{orderedElement.RuleElement.Name}():{Environment.NewLine}");
                                }

                                var previousVariableName = ruleGenerationContext.CurrentVariableName;
                                ruleGenerationContext.CurrentVariableName = orderedElement.UmlClass.Name.LowerCaseFirstLetter();
                                ProcessNonTerminalElement(writer, orderedElement.UmlClass, orderedElement.RuleElement, ruleGenerationContext);
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
                                writer.WriteSafeString($"var {cursorVarName} = cursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{targetProperty.QueryPropertyNameBasedOnUmlProperties()});{Environment.NewLine}");
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
                                ProcessNonTerminalElement(writer, orderedElement.UmlClass, orderedElement.RuleElement, ruleGenerationContext);
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
                            DeclareAllRequiredCursors(writer, umlClass, alternative, ruleGenerationContext);
                        }

                        var properties = umlClass.QueryAllProperties();

                        for (var alternativeIndex = 0; alternativeIndex < alternatives.Count; alternativeIndex++)
                        {
                            if (alternativeIndex != 0)
                            {
                                writer.WriteSafeString("else ");
                            }

                            var assignment = assignmentElements[alternativeIndex];
                            var targetProperty = properties.Single(x => string.Equals(x.Name, assignment.Property));

                            var iterator = ruleGenerationContext.DefinedCursors.SingleOrDefault(x => x.ApplicableRuleElements.Contains(assignment));

                            if (assignment.Operator != "+=")
                            {
                                writer.WriteSafeString($"if({targetProperty.QueryIfStatementContentForNonEmpty(iterator?.CursorVariableName ?? "poco")}){Environment.NewLine}");
                            }

                            writer.WriteSafeString($"{{{Environment.NewLine}");
                            ProcessAssignmentElement(writer, umlClass, ruleGenerationContext, assignment, true);
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
        /// Emits a single type-dispatched branch body.
        /// </summary>
        private void EmitCompoundPocoTypeBranch(EncodedTextWriter writer, IClass umlClass, NonTerminalElement leadingNonTerminal, Alternatives alternative, IClass targetClass, string variableName, RuleGenerationContext ruleGenerationContext)
        {
            string builderCall;

            if (targetClass == umlClass)
            {
                builderCall = $"Build{leadingNonTerminal.Name}({variableName}, cursorCache, stringBuilder);";
            }
            else
            {
                builderCall = $"{targetClass.Name}TextualNotationBuilder.Build{leadingNonTerminal.Name}({variableName}, cursorCache, stringBuilder);";
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
    }
}
