// -------------------------------------------------------------------------------------------------
// <copyright file="RulesHelper.AlternativesProcessor.cs" company="Starion Group S.A.">
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
    /// Core alternatives processing pipeline and element dispatch
    /// </summary>
    public static partial class RulesHelper
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
        private static void ProcessAlternatives(EncodedTextWriter writer, IClass umlClass, IReadOnlyCollection<Alternatives> alternatives, RuleGenerationContext ruleGenerationContext, bool isPartOfMultipleAlternative = false)
        {
            ruleGenerationContext.DefinedCursors ??= [];

            if (alternatives.Count == 1)
            {
                var alternative = alternatives.ElementAt(0);
                var elements = alternative.Elements;
                DeclareAllRequiredCursors(writer, umlClass, alternative, ruleGenerationContext);

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
                            ProcessRuleElement(writer, umlClass, elements[elementIndex], ruleGenerationContext);
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
                            var referencedRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == nonTerminal.Name);

                            if (referencedRule != null)
                            {
                                var condition = GenerateInlineOptionalCondition(referencedRule, umlClass, ruleGenerationContext.AllRules, "poco");

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
                            ProcessRuleElement(writer, umlClass, elements[elementIndex], ruleGenerationContext);
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
                        ProcessRuleElement(writer, umlClass, elements[elementIndex], ruleGenerationContext, isPartOfMultipleAlternative);
                        ruleGenerationContext.CallerRule = previousCaller;
                    }

                    ruleGenerationContext.CurrentSiblingElements = previousSiblings;
                    ruleGenerationContext.CurrentElementIndex = previousIndex;
                }
            }
            else
            {
                if (alternatives.All(x => x.Elements.Count == 1))
                {
                    if (alternatives.ElementAt(0).Elements[0].TextualNotationRule.IsMultiCollectionAssignment)
                    {
                        ProcessMultiCollectionAssignment(writer, umlClass, alternatives, ruleGenerationContext);
                        return;
                    }

                    var types = alternatives.SelectMany(x => x.Elements).Select(x => x.GetType()).Distinct().ToList();

                    if (types.Count == 1)
                    {
                        ProcessUnitypedAlternativesWithOneElement(writer, umlClass, alternatives, ruleGenerationContext);
                    }
                    else
                    {
                        if (types.SequenceEqual([typeof(AssignmentElement), typeof(NonTerminalElement)]))
                        {
                            foreach (var alternative in alternatives)
                            {
                                DeclareAllRequiredCursors(writer, umlClass, alternative, ruleGenerationContext);
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
                                            DeclareAllRequiredCursors(writer, umlClass, alternatives.ElementAt(0), ruleGenerationContext);

                                            var iterator = ruleGenerationContext.DefinedCursors.Single(x => x.ApplicableRuleElements.Contains(assignmentElement));

                                            writer.WriteSafeString($"if({targetProperty.QueryIfStatementContentForNonEmpty(iterator.CursorVariableName)}){Environment.NewLine}");
                                            writer.WriteSafeString($"{{{Environment.NewLine}");
                                        }
                                        else
                                        {
                                            writer.WriteSafeString($"{Environment.NewLine}if({targetProperty.QueryIfStatementContentForNonEmpty("poco")}){Environment.NewLine}");
                                            writer.WriteSafeString($"{{{Environment.NewLine}");
                                        }

                                        ProcessAssignmentElement(writer, umlClass, ruleGenerationContext, assignmentElement, true);

                                        writer.WriteSafeString($"{Environment.NewLine}}}");
                                        break;

                                    case NonTerminalElement nonTerminalElement:
                                        writer.WriteSafeString($"{{{Environment.NewLine}");
                                        ProcessNonTerminalElement(writer, umlClass, nonTerminalElement, ruleGenerationContext);
                                        writer.WriteSafeString($"{Environment.NewLine}}}");
                                        break;
                                }
                            }
                        }
                        else if (types.SequenceEqual([typeof(NonTerminalElement), typeof(AssignmentElement)]))
                        {
                            var nonTerminalElement = (NonTerminalElement)alternatives.ElementAt(0).Elements[0];
                            var assignmentElements = alternatives.SelectMany(x => x.Elements).OfType<AssignmentElement>().ToList();

                            var referencedAssignmentNonTerminals = assignmentElements.Select(x => x.Value).OfType<NonTerminalElement>().ToList();

                            if (referencedAssignmentNonTerminals.Count != assignmentElements.Count)
                            {
                                var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                                writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                                return;
                            }

                            // Mixed NonTerminal + AssignmentElement dispatch:
                            // - AssignmentElements (+=) target cursor elements → if (cursor.Current is {Type}) { process + Move() }
                            // - NonTerminal targets the declaring class → else { BuildXxx(poco, ...) }
                            var targetProperty = umlClass.QueryAllProperties().Single(x => string.Equals(x.Name, assignmentElements[0].Property));
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

                            // Generate if/else chain: assignment types checked on cursor, NonTerminal called with poco
                            var assignmentMappedElements = OrderElementsByInheritance(referencedAssignmentNonTerminals, umlClass.Cache, ruleGenerationContext);

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
                                ProcessNonTerminalElement(writer, mappedElement.UmlClass, mappedElement.RuleElement, ruleGenerationContext);
                                ruleGenerationContext.CurrentVariableName = previousVariableName;

                                writer.WriteSafeString($"{Environment.NewLine}{cursorVarName}.Move();{Environment.NewLine}");
                                writer.WriteSafeString($"}}{Environment.NewLine}");
                            }

                            // NonTerminal fallback: called with poco, not cursor element
                            writer.WriteSafeString($"else{Environment.NewLine}");
                            writer.WriteSafeString($"{{{Environment.NewLine}");

                            var nonTerminalReferencedRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == nonTerminalElement.Name);

                            var nonTerminalTypeTarget = nonTerminalReferencedRule != null
                                ? nonTerminalReferencedRule.TargetElementName ?? nonTerminalReferencedRule.RuleName
                                : umlClass.Name;

                            var nonTerminalCall = ResolveBuilderCall(umlClass, nonTerminalElement, nonTerminalTypeTarget, ruleGenerationContext);

                            if (nonTerminalCall != null)
                            {
                                writer.WriteSafeString(nonTerminalCall);
                            }
                            else
                            {
                                var previousCaller = ruleGenerationContext.CallerRule;
                                var previousName = ruleGenerationContext.CurrentVariableName;
                                ruleGenerationContext.CallerRule = nonTerminalElement;
                                ProcessAlternatives(writer, umlClass, nonTerminalReferencedRule?.Alternatives, ruleGenerationContext);
                                ruleGenerationContext.CallerRule = previousCaller;
                                ruleGenerationContext.CurrentVariableName = previousName;
                            }

                            writer.WriteSafeString($"{Environment.NewLine}}}{Environment.NewLine}");
                        }
                        else if (alternatives.ElementAt(0).Elements[0] is TerminalElement terminalElement && alternatives.ElementAt(1).Elements[0] is AssignmentElement assignmentElement)
                        {
                            var targetProperty = umlClass.QueryAllProperties().Single(x => string.Equals(x.Name, assignmentElement.Property));

                            writer.WriteSafeString($"if(!{targetProperty.QueryIfStatementContentForNonEmpty("poco")}){Environment.NewLine}");
                            writer.WriteSafeString($"{{{Environment.NewLine}");
                            ProcessRuleElement(writer, umlClass, terminalElement, ruleGenerationContext);
                            writer.WriteSafeString($"{Environment.NewLine}}}");
                            writer.WriteSafeString("else");
                            writer.WriteSafeString($"{Environment.NewLine}{{{Environment.NewLine}");
                            ProcessAssignmentElement(writer, umlClass, ruleGenerationContext, assignmentElement, true);
                            writer.WriteSafeString($"{Environment.NewLine}}}");
                        }
                        else
                        {
                            var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;

                            if (ruleGenerationContext.EmittedHandCodedCalls.Add(handCodedRuleName))
                            {
                                writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                            }
                        }
                    }
                }
                else
                {
                    // When all alternatives consist exclusively of terminal elements (and optionally non-parsing assignments), handle via code-gen
                    if (alternatives.All(alt => alt.Elements.Count > 0 && alt.Elements.All(element => element is TerminalElement or NonParsingAssignmentElement)))
                    {
                        var nonParsingAssignments = alternatives
                            .SelectMany(alt => alt.Elements.OfType<NonParsingAssignmentElement>())
                            .ToList();

                        if (nonParsingAssignments.Count == 0)
                        {
                            // Pure terminal alternatives — emit the first alternative
                            var firstAlternative = alternatives.ElementAt(0);

                            foreach (var terminalOnly in firstAlternative.Elements.Cast<TerminalElement>())
                            {
                                WriteTerminalAppend(writer, terminalOnly.Value);
                            }
                        }
                        else
                        {
                            // Terminal + non-parsing assignment alternatives — generate a switch on the assigned property
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
                                        WriteTerminalAppend(writer, terminal.Value);
                                    }

                                    writer.WriteSafeString($"{Environment.NewLine}break;{Environment.NewLine}");
                                }

                                writer.WriteSafeString($"}}{Environment.NewLine}");
                            }
                            else
                            {
                                // Fallback: emit first alternative terminals
                                var firstAlternative = alternatives.ElementAt(0);

                                foreach (var terminalOnly in firstAlternative.Elements.OfType<TerminalElement>())
                                {
                                    WriteTerminalAppend(writer, terminalOnly.Value);
                                }
                            }
                        }

                        return;
                    }

                    // Detect pattern: property=[QualifiedName] | property=NonTerminal{containment+=property}
                    // e.g., type=[QualifiedName]|type=OwnedFeatureChain{ownedRelatedElement+=type}
                    // Runtime: if the referenced value is owned → call the chain builder, else → output qualifiedName
                    if (alternatives.Count == 2)
                    {
                        var qualifiedNameAlt = alternatives.FirstOrDefault(alt =>
                            alt.Elements.Count == 1
                            && alt.Elements[0] is AssignmentElement { Value: ValueLiteralElement qualifiedNameLiteral }
                            && qualifiedNameLiteral.QueryIsQualifiedName());

                        var chainAlt = alternatives.FirstOrDefault(alt =>
                            alt.Elements.Count >= 2
                            && alt.Elements[0] is AssignmentElement { Value: NonTerminalElement }
                            && alt.Elements.OfType<NonParsingAssignmentElement>().Any());

                        if (qualifiedNameAlt != null && chainAlt != null)
                        {
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

                            if (targetProperty != null && containmentProperty != null)
                            {
                                var variableName = ruleGenerationContext.CurrentVariableName ?? "poco";
                                var resolvedPropertyName = targetProperty.QueryPropertyNameBasedOnUmlProperties();
                                var resolvedContainmentName = containmentProperty.QueryPropertyNameBasedOnUmlProperties();

                                // Resolve the chain NonTerminal's target type
                                var referencedRule = ruleGenerationContext.AllRules
                                    .SingleOrDefault(x => x.RuleName == chainNonTerminal.Name);

                                var typeTarget = referencedRule != null
                                    ? referencedRule.TargetElementName ?? referencedRule.RuleName
                                    : umlClass.Name;

                                var chainTargetClass = umlClass.Cache.Values.OfType<INamedElement>()
                                    .SingleOrDefault(x => x.Name == typeTarget) as IClass;

                                if (chainTargetClass != null)
                                {
                                    var chainTypeName = chainTargetClass.QueryFullyQualifiedTypeName();
                                    var chainVarName = $"chained{resolvedPropertyName}As{chainTargetClass.Name}";

                                    string builderCallString;

                                    if (typeTarget == ruleGenerationContext.NamedElementToGenerate.Name)
                                    {
                                        builderCallString = $"Build{chainNonTerminal.Name}({chainVarName}, cursorCache, stringBuilder);";
                                    }
                                    else
                                    {
                                        builderCallString = $"{typeTarget}TextualNotationBuilder.Build{chainNonTerminal.Name}({chainVarName}, cursorCache, stringBuilder);";
                                    }

                                    writer.WriteSafeString($"if ({variableName}.{resolvedContainmentName}.Contains({variableName}.{resolvedPropertyName}) && {variableName}.{resolvedPropertyName} is {chainTypeName} {chainVarName}){Environment.NewLine}");
                                    writer.WriteSafeString($"{{{Environment.NewLine}");
                                    writer.WriteSafeString($"{builderCallString}{Environment.NewLine}");
                                    writer.WriteSafeString($"}}{Environment.NewLine}");
                                    writer.WriteSafeString($"else if ({variableName}.{resolvedPropertyName} != null){Environment.NewLine}");
                                    writer.WriteSafeString($"{{{Environment.NewLine}");
                                    writer.WriteSafeString($"stringBuilder.Append({variableName}.{resolvedPropertyName}.qualifiedName);{Environment.NewLine}");
                                    writer.WriteSafeString($"stringBuilder.Append(' ');{Environment.NewLine}");
                                    writer.WriteSafeString($"}}{Environment.NewLine}");

                                    return;
                                }
                            }
                        }
                    }

                    // Multi-element alternatives (e.g., ';' | '{' NamespaceBodyElement* '}')
                    // Detect pattern: first alternative is terminal-only, second has collection assignment
                    var firstAlt = alternatives.ElementAt(0);
                    var hasTerminalOnlyFirstAlt = firstAlt.Elements.Count == 1 && firstAlt.Elements[0] is TerminalElement;

                    if (hasTerminalOnlyFirstAlt && alternatives.Count == 2)
                    {
                        var secondAlt = alternatives.ElementAt(1);
                        var collectionAssignments = secondAlt.Elements.OfType<AssignmentElement>().Where(x => x.Operator == "+=").ToList();

                        var groupsWithCollectionAssignments = secondAlt.Elements.OfType<GroupElement>()
                            .SelectMany(g => g.Alternatives.SelectMany(a => a.Elements.OfType<AssignmentElement>().Where(x => x.Operator == "+=")))
                            .ToList();

                        var allCollectionAssignments = collectionAssignments.Concat(groupsWithCollectionAssignments).ToList();

                        if (allCollectionAssignments.Count > 0)
                        {
                            // Determine the collection property to check for emptiness
                            var collectionProperty = allCollectionAssignments[0].Property;
                            var targetProperty = umlClass.QueryAllProperties().SingleOrDefault(x => string.Equals(x.Name, collectionProperty, StringComparison.OrdinalIgnoreCase));
                            var terminalValue = ((TerminalElement)firstAlt.Elements[0]).Value;

                            if (targetProperty != null)
                            {
                                // Use cursor state for ';' vs '{' decision. By the time the body
                                // is reached, previous methods (prefix, declaration) have already
                                // consumed typing, multiplicity, etc. from the shared cursor.
                                // GetOrCreateCursor returns the same cursor at its current position.
                                // Use cursor state for ';' vs '{' decision. Previous methods
                                // (prefix, declaration) have consumed typing/multiplicity from
                                // the shared cursor via GetOrCreateCursor. If cursor.Current is
                                // null, no body members remain → emit ';'.
                                var bodyPropertyAccess = targetProperty.QueryPropertyNameBasedOnUmlProperties();
                                writer.WriteSafeString($"if(cursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{bodyPropertyAccess}).Current == null){Environment.NewLine}");

                                writer.WriteSafeString($"{{{Environment.NewLine}");
                                writer.WriteSafeString($"stringBuilder.AppendLine(\"{terminalValue}\");{Environment.NewLine}");
                                writer.WriteSafeString($"}}{Environment.NewLine}");
                                writer.WriteSafeString($"else{Environment.NewLine}");
                                writer.WriteSafeString($"{{{Environment.NewLine}");

                                // Process the second alternative elements
                                DeclareAllRequiredCursors(writer, umlClass, secondAlt, ruleGenerationContext);

                                foreach (var element in secondAlt.Elements)
                                {
                                    ProcessRuleElement(writer, umlClass, element, ruleGenerationContext);
                                }

                                writer.WriteSafeString($"}}{Environment.NewLine}");
                            }
                            else
                            {
                                var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                                writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                            }
                        }
                        else
                        {
                            // Check for collection NonTerminal elements (e.g., ';' | '{' Items* '}')
                            // The body items are expressed as a standalone NonTerminal with IsCollection, not as AssignmentElements
                            var collectionNonTerminals = secondAlt.Elements.OfType<NonTerminalElement>().Where(x => x.IsCollection).ToList();

                            if (collectionNonTerminals.Count > 0)
                            {
                                // Resolve the collection property through the NonTerminal's rule tree
                                var nonTerminalRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == collectionNonTerminals[0].Name);

                                if (nonTerminalRule != null)
                                {
                                    var collectionPropertyNames = nonTerminalRule.QueryCollectionPropertyNames(ruleGenerationContext.AllRules);

                                    if (collectionPropertyNames.Count > 0)
                                    {
                                        var collectionPropertyName = collectionPropertyNames.First();
                                        var targetProperty = umlClass.QueryAllProperties().SingleOrDefault(x => string.Equals(x.Name, collectionPropertyName, StringComparison.OrdinalIgnoreCase));
                                        var terminalValue = ((TerminalElement)firstAlt.Elements[0]).Value;

                                        if (targetProperty != null)
                                        {
                                            var propertyAccessName = targetProperty.QueryPropertyNameBasedOnUmlProperties();

                                            writer.WriteSafeString($"if(cursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{propertyAccessName}).Current == null){Environment.NewLine}");

                                            writer.WriteSafeString($"{{{Environment.NewLine}");
                                            writer.WriteSafeString($"stringBuilder.AppendLine(\"{terminalValue}\");{Environment.NewLine}");
                                            writer.WriteSafeString($"}}{Environment.NewLine}");
                                            writer.WriteSafeString($"else{Environment.NewLine}");
                                            writer.WriteSafeString($"{{{Environment.NewLine}");

                                            // Process non-collection elements (terminals like '{', '}' and optional assignments like isParallel?='parallel')
                                            // and handle the collection NonTerminal via cursor-based dispatch (builder internally advances)
                                            foreach (var element in secondAlt.Elements)
                                            {
                                                if (element is NonTerminalElement { IsCollection: true })
                                                {
                                                    var cursorVarName = $"{targetProperty.Name.LowerCaseFirstLetter()}Cursor";
                                                    writer.WriteSafeString($"var {cursorVarName} = cursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{propertyAccessName});{Environment.NewLine}");

                                                    var collectionNonTerminal = (NonTerminalElement)element;
                                                    var referencedRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == collectionNonTerminal.Name);

                                                    var typeTarget = referencedRule != null
                                                        ? referencedRule.TargetElementName ?? referencedRule.RuleName
                                                        : umlClass.Name;

                                                    var perItemCall = ResolveBuilderCall(umlClass, collectionNonTerminal, typeTarget, ruleGenerationContext);

                                                    writer.WriteSafeString($"while ({cursorVarName}.Current != null){Environment.NewLine}");
                                                    writer.WriteSafeString($"{{{Environment.NewLine}");

                                                    if (perItemCall != null)
                                                    {
                                                        // Dispatcher builder internally advances the cursor — no outer Move()
                                                        writer.WriteSafeString(perItemCall);
                                                    }
                                                    else
                                                    {
                                                        var previousCaller = ruleGenerationContext.CallerRule;
                                                        var previousName = ruleGenerationContext.CurrentVariableName;
                                                        ruleGenerationContext.CallerRule = collectionNonTerminal;
                                                        ProcessAlternatives(writer, umlClass, referencedRule?.Alternatives, ruleGenerationContext);
                                                        ruleGenerationContext.CallerRule = previousCaller;
                                                        ruleGenerationContext.CurrentVariableName = previousName;
                                                    }

                                                    writer.WriteSafeString($"{Environment.NewLine}}}{Environment.NewLine}");
                                                }
                                                else
                                                {
                                                    ProcessRuleElement(writer, umlClass, element, ruleGenerationContext);
                                                }
                                            }

                                            writer.WriteSafeString($"}}{Environment.NewLine}");
                                        }
                                        else
                                        {
                                            var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                                            writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                                        }
                                    }
                                    else
                                    {
                                        var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                                        writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                                    }
                                }
                                else
                                {
                                    var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                                    writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                                }
                            }
                            else
                            {
                                // Check for non-collection NonTerminal elements (e.g., ';' | '{' CalculationBodyPart '}')
                                // Same body pattern but with a single sub-rule call instead of a while loop
                                var nonCollectionNonTerminals = secondAlt.Elements.OfType<NonTerminalElement>().Where(x => !x.IsCollection).ToList();

                                if (nonCollectionNonTerminals.Count > 0)
                                {
                                    var nonTerminalRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == nonCollectionNonTerminals[0].Name);

                                    if (nonTerminalRule != null)
                                    {
                                        var collectionPropertyNames = nonTerminalRule.QueryCollectionPropertyNames(ruleGenerationContext.AllRules);

                                        if (collectionPropertyNames.Count > 0)
                                        {
                                            var collectionPropertyName = collectionPropertyNames.First();
                                            var targetProperty = umlClass.QueryAllProperties().SingleOrDefault(x => string.Equals(x.Name, collectionPropertyName, StringComparison.OrdinalIgnoreCase));
                                            var terminalValue = ((TerminalElement)firstAlt.Elements[0]).Value;

                                            if (targetProperty != null)
                                            {
                                                var propertyAccessName = targetProperty.QueryPropertyNameBasedOnUmlProperties();

                                                writer.WriteSafeString($"if(cursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{propertyAccessName}).Current == null){Environment.NewLine}");

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
                                                        writer.WriteSafeString($"var {cursorVarName} = cursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{propertyAccessName});{Environment.NewLine}");

                                                        var referencedRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == singleNonTerminal.Name);

                                                        var typeTarget = referencedRule != null
                                                            ? referencedRule.TargetElementName ?? referencedRule.RuleName
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
                                                            var previousCaller = ruleGenerationContext.CallerRule;
                                                            var previousName = ruleGenerationContext.CurrentVariableName;
                                                            ruleGenerationContext.CallerRule = singleNonTerminal;
                                                            ProcessAlternatives(writer, umlClass, referencedRule?.Alternatives, ruleGenerationContext);
                                                            ruleGenerationContext.CallerRule = previousCaller;
                                                            ruleGenerationContext.CurrentVariableName = previousName;
                                                        }

                                                        writer.WriteSafeString($"{Environment.NewLine}}}{Environment.NewLine}");
                                                    }
                                                    else
                                                    {
                                                        ProcessRuleElement(writer, umlClass, element, ruleGenerationContext);
                                                    }
                                                }

                                                writer.WriteSafeString($"}}{Environment.NewLine}");
                                            }
                                            else
                                            {
                                                var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                                                writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                                            }
                                        }
                                        else
                                        {
                                            var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                                            writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                                        }
                                    }
                                    else
                                    {
                                        var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                                        writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                                    }
                                }
                                else
                                {
                                    var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;
                                    writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (TryHandleOperatorLiteralAlternation(writer, umlClass, alternatives, ruleGenerationContext))
                        {
                            return;
                        }

                        if (TryHandleEmptyVsNonEmptyMembership(writer, umlClass, alternatives, ruleGenerationContext))
                        {
                            return;
                        }

                        if (TryHandlePocoTypeDispatchWithCompoundAlternatives(writer, umlClass, alternatives, ruleGenerationContext))
                        {
                            return;
                        }

                        if (TryHandleReferenceOrInline(writer, umlClass, alternatives, ruleGenerationContext))
                        {
                            return;
                        }

                        var handCodedRuleName = alternatives.ElementAt(0).TextualNotationRule.RuleName;

                        if (ruleGenerationContext.EmittedHandCodedCalls.Add(handCodedRuleName))
                        {
                            writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Pattern B — detects and emits an alternation where each alternative starts with
        /// <c>operator = X</c> and <c>X</c> is either a literal terminal or a non-terminal whose
        /// RHS is a pure literal alternation (e.g. <c>ClassificationTestOperator = 'istype'|'hastype'|'@'</c>,
        /// <c>CastOperator = 'as'</c>, <c>MetaCastOperator = 'meta'</c>). Emits a <c>switch</c> on
        /// <c>poco.Operator</c> with one <c>case</c> per literal, processing each alternative's tail
        /// elements (the trailing <c>ownedRelationship+=...</c> assignment, etc.) inside the matching branch.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The current <see cref="IClass" /></param>
        /// <param name="alternatives">The collection of alternatives to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <returns><c>true</c> if the pattern matched and code was emitted; <c>false</c> otherwise</returns>
        /// <summary>
        /// Emits the body of a single alternative by iterating its elements and delegating to <see cref="ProcessRuleElement" />.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The current <see cref="IClass" /></param>
        /// <param name="alternative">The <see cref="Alternatives" /> whose elements are emitted</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        private static void EmitAlternativeBody(EncodedTextWriter writer, IClass umlClass, Alternatives alternative, RuleGenerationContext ruleGenerationContext)
        {
            var previousSiblings = ruleGenerationContext.CurrentSiblingElements;
            var previousIndex = ruleGenerationContext.CurrentElementIndex;
            ruleGenerationContext.CurrentSiblingElements = alternative.Elements;

            for (var elementIndex = 0; elementIndex < alternative.Elements.Count; elementIndex++)
            {
                ruleGenerationContext.CurrentElementIndex = elementIndex;
                ProcessRuleElement(writer, umlClass, alternative.Elements[elementIndex], ruleGenerationContext);
            }

            ruleGenerationContext.CurrentSiblingElements = previousSiblings;
            ruleGenerationContext.CurrentElementIndex = previousIndex;
        }

        /// <summary>
        /// Determines whether a grammar rule represents an "empty membership" — one whose body
        /// dispatches to a single <c>Empty*</c>-named non-terminal that wraps an <see cref="EmptyUsage" />.
        /// The naming convention (e.g. <c>EmptyParameterMember</c>, <c>EmptyResultMember</c>,
        /// <c>EmptyFeatureMember</c>) is a stable contract in both KEBNF files; the implementation
        /// also verifies the structural shape so that an accidentally-named rule in the future
        /// would not be mis-classified.
        /// </summary>
        /// <param name="ruleName">The non-terminal name to test</param>
        /// <param name="allRules">All available rules, used to look up <paramref name="ruleName" /></param>
        /// <returns><c>true</c> if the rule conforms to the empty-membership shape</returns>
        /// <summary>
        /// Declares cursor variables for all enumerable properties referenced by assignment elements in the given alternative.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="alternatives">The <see cref="Alternatives" /> containing assignment elements</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        private static void DeclareAllRequiredCursors(EncodedTextWriter writer, IClass umlClass, Alternatives alternatives, RuleGenerationContext ruleGenerationContext)
        {
            foreach (var ruleElement in alternatives.Elements)
            {
                switch (ruleElement)
                {
                    case AssignmentElement assignmentElement:
                        DeclareCursorIfRequired(writer, umlClass, assignmentElement, ruleGenerationContext);
                        break;
                    case GroupElement groupElement:
                        foreach (var groupElementAlternative in groupElement.Alternatives)
                        {
                            DeclareAllRequiredCursors(writer, umlClass, groupElementAlternative, ruleGenerationContext);
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Processes a <see cref="RuleElement" />
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="textualRuleElement">The <see cref="RuleElement" /> to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <param name="isPartOfMultipleAlternative"></param>
        /// <exception cref="ArgumentException">If the type of the <see cref="RuleElement" /> is not supported</exception>
        private static void ProcessRuleElement(EncodedTextWriter writer, IClass umlClass, RuleElement textualRuleElement, RuleGenerationContext ruleGenerationContext, bool isPartOfMultipleAlternative = false)
        {
            switch (textualRuleElement)
            {
                case TerminalElement terminalElement:
                    WriteTerminalAppend(writer, terminalElement.Value);
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

                    ProcessNonTerminalElement(writer, umlClass, nonTerminalElement, ruleGenerationContext, isPartOfMultipleAlternative);

                    break;
                case GroupElement groupElement:
                    ruleGenerationContext.CallerRule = groupElement;

                    if (groupElement.IsCollection && groupElement.Alternatives.Count == 1)
                    {
                        var assignmentRule = groupElement.Alternatives.SelectMany(x => x.Elements).FirstOrDefault(x => x is AssignmentElement { Value: NonTerminalElement } || x is AssignmentElement { Value: ValueLiteralElement });

                        if (assignmentRule is AssignmentElement assignmentElement)
                        {
                            var cursorToUse = ruleGenerationContext.DefinedCursors.Single(x => x.ApplicableRuleElements.Contains(assignmentElement));

                            // Resolve the assignment's target type to emit a type-guarded while condition.
                            // This prevents greedy consumption of non-matching elements (Golden Rule: Move() ↔ += of matching type only).
                            var groupTypeGuard = "";

                            if (assignmentElement.Value is NonTerminalElement valueNonTerminal)
                            {
                                var referencedRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == valueNonTerminal.Name);
                                var typeTarget = referencedRule != null ? referencedRule.TargetElementName ?? referencedRule.RuleName : null;

                                if (typeTarget != null)
                                {
                                    var targetClass = umlClass.Cache.Values.OfType<INamedElement>().SingleOrDefault(x => x.Name == typeTarget) as IClass;

                                    if (targetClass != null)
                                    {
                                        // Try content-aware guard: when the outer assignment targets one composite
                                        // property (e.g., ownedRelationship), look at the referenced rule for assignments
                                        // on the complementary property (e.g., ownedRelatedElement) to validate content.

                                        var contentGuard = ResolveContentTypeGuard(cursorToUse.CursorVariableName, referencedRule, assignmentElement.Property, umlClass, ruleGenerationContext);

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

                            if (groupTypeGuard.StartsWith("__FULL_GUARD__"))
                            {
                                var fullGuard = groupTypeGuard.Substring("__FULL_GUARD__".Length);
                                writer.WriteSafeString($"{Environment.NewLine}while({fullGuard}){Environment.NewLine}");
                            }
                            else
                            {
                                writer.WriteSafeString($"{Environment.NewLine}while({cursorToUse.CursorVariableName}.Current != null{groupTypeGuard}){Environment.NewLine}");
                            }
                        }

                        writer.WriteSafeString($"{{{Environment.NewLine}");
                        ProcessAlternatives(writer, umlClass, groupElement.Alternatives, ruleGenerationContext);

                        if (assignmentRule is AssignmentElement assignmentElementForMove)
                        {
                            var cursorToUse = ruleGenerationContext.DefinedCursors.Single(x => x.ApplicableRuleElements.Contains(assignmentElementForMove));
                            writer.WriteSafeString($"{cursorToUse.CursorVariableName}.Move();{Environment.NewLine}");
                        }

                        writer.WriteSafeString($"{Environment.NewLine}}}");
                    }
                    else if (groupElement.IsCollection)
                    {
                        // Collection group with += assignments: generate a while loop with cursor-based switch
                        // e.g., (ownedRelationship+=DefinitionMember|ownedRelationship+=AliasMember|ownedRelationship+=Import)*
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
                                    writer.WriteSafeString($"var {groupCursorVarName} = cursorCache.GetOrCreateCursor(poco.Id, \"{groupTargetProperty.Name}\", poco.{groupPropertyAccessName});{Environment.NewLine}");
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

                                var groupOrderedElements = OrderElementsByInheritance(groupNonTerminals, umlClass.Cache, ruleGenerationContext);

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
                                    ProcessNonTerminalElement(writer, groupOrderedElement.UmlClass, groupOrderedElement.RuleElement, ruleGenerationContext);
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
                                writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                            }
                        }
                        else
                        {
                            var handCodedRuleName = groupElement.TextualNotationRule?.RuleName ?? "Unknown";
                            writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                        }
                    }
                    else
                    {
                        ProcessAlternatives(writer, umlClass, groupElement.Alternatives, ruleGenerationContext);
                    }

                    if (!groupElement.IsOptional && !ruleGenerationContext.IsNextElementNewLineTerminal() && !ruleGenerationContext.IsLastElement())
                    {
                        writer.WriteSafeString($"{Environment.NewLine}stringBuilder.Append(' ');");
                    }

                    break;
                case AssignmentElement assignmentElement:
                    ProcessAssignmentElement(writer, umlClass, ruleGenerationContext, assignmentElement, isPartOfMultipleAlternative);
                    break;
                case NonParsingAssignmentElement nonParsingAssignmentElement:
                    writer.WriteSafeString($"// NonParsing Assignment Element : {nonParsingAssignmentElement.PropertyName} {nonParsingAssignmentElement.Operator} {nonParsingAssignmentElement.Value} => Does not have to be process");
                    break;
                case ValueLiteralElement valueLiteralElement:
                    if (valueLiteralElement.QueryIsQualifiedName())
                    {
                        writer.WriteSafeString($"stringBuilder.Append({ruleGenerationContext.CurrentVariableName}.qualifiedName);{Environment.NewLine}");

                        if (!ruleGenerationContext.IsNextElementNewLineTerminal())
                        {
                            writer.WriteSafeString("stringBuilder.Append(' ');");
                        }
                    }
                    else
                    {
                        var handCodedRuleName = textualRuleElement.TextualNotationRule?.RuleName ?? "Unknown";
                        writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                    }

                    break;
                default:
                    throw new ArgumentException("Unknown element type");
            }

            writer.WriteSafeString(Environment.NewLine);
        }

        /// <summary>
        /// Processes an <see cref="AssignmentElement" />
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="assignmentElement">The <see cref="AssignmentElement" /> to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <param name="isPartOfMultipleAlternative">Asserts that the current <see cref="AssignmentElement" /> is part of a multiple alternative process</param>
        private static void ProcessAssignmentElement(EncodedTextWriter writer, IClass umlClass, RuleGenerationContext ruleGenerationContext, AssignmentElement assignmentElement, bool isPartOfMultipleAlternative = false)
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
                        ProcessNonTerminalElement(writer, umlClass, nonTerminalElement, ruleGenerationContext);
                        ruleGenerationContext.CurrentVariableName = previousVariableName;
                        ruleGenerationContext.CallerRule = previousCaller;

                        // Golden Rule: Move() ↔ +=. Suppress only for collection groups (the loop emits its own Move).
                        // For optional groups, the surrounding optional handler already wraps the body in
                        // `if (cursor.Current is X) { … }`, so emitting Move() here keeps the advance inside that
                        // guard — it only fires when the element was actually consumed.
                        if (!isPartOfMultipleAlternative && assignmentElement.Container is not GroupElement { IsCollection: true })
                        {
                            writer.WriteSafeString($"{cursorToUse.CursorVariableName}.Move();{Environment.NewLine}");
                        }
                    }
                    else if (assignmentElement.Value is GroupElement groupElement)
                    {
                        var previousCaller = ruleGenerationContext.CallerRule;
                        ruleGenerationContext.CallerRule = assignmentElement;
                        ProcessAlternatives(writer, umlClass, groupElement.Alternatives, ruleGenerationContext);
                        ruleGenerationContext.CallerRule = previousCaller;
                    }
                    else if (assignmentElement.Value is ValueLiteralElement valueLiteralElement && valueLiteralElement.QueryIsQualifiedName())
                    {
                        var cursorToUse = ruleGenerationContext.DefinedCursors.Single(x => x.ApplicableRuleElements.Contains(assignmentElement));

                        writer.WriteSafeString($"{Environment.NewLine}if({cursorToUse.CursorVariableName}.Current != null){Environment.NewLine}");
                        writer.WriteSafeString($"{{{Environment.NewLine}");
                        writer.WriteSafeString($"stringBuilder.Append({cursorToUse.CursorVariableName}.Current.qualifiedName);{Environment.NewLine}");
                        writer.WriteSafeString($"{cursorToUse.CursorVariableName}.Move();{Environment.NewLine}");
                        writer.WriteSafeString("}");
                    }
                    else
                    {
                        var handCodedRuleName = assignmentElement.TextualNotationRule?.RuleName ?? "Unknown";
                        writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
                    }
                }
                else
                {
                    if (assignmentElement.IsOptional)
                    {
                        writer.WriteSafeString($"{Environment.NewLine}if({targetProperty.QueryIfStatementContentForNonEmpty("poco")}){Environment.NewLine}");
                        writer.WriteSafeString($"{{{Environment.NewLine}");
                        writer.WriteSafeString($"stringBuilder.Append(poco.{targetProperty.Name.CapitalizeFirstLetter()});{Environment.NewLine}");
                        writer.WriteSafeString("}");
                    }
                    else
                    {
                        var targetPropertyName = targetProperty.QueryPropertyNameBasedOnUmlProperties();

                        if (targetProperty.QueryIsString())
                        {
                            if (assignmentElement.Value is NonTerminalElement { Name: "REGULAR_COMMENT" })
                            {
                                writer.WriteSafeString($"SharedTextualNotationBuilder.AppendRegularComment(stringBuilder, poco.{targetPropertyName});");
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
                                    writer.WriteSafeString($"if({targetProperty.QueryIfStatementContentForNonEmpty("poco")}){Environment.NewLine}");
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
                                    ruleGenerationContext.CurrentVariableName = $"poco.{targetPropertyName}";
                                    ProcessNonTerminalElement(writer, targetProperty.Type as IClass, nonTerminalElement, ruleGenerationContext, isPartOfMultipleAlternative);
                                    ruleGenerationContext.CurrentVariableName = "poco";
                                    ruleGenerationContext.CallerRule = previousCaller;
                                    break;
                                }
                                case ValueLiteralElement valueLiteralElement when valueLiteralElement.QueryIsQualifiedName():
                                    if (isPartOfMultipleAlternative)
                                    {
                                        writer.WriteSafeString($"stringBuilder.Append(poco.{targetPropertyName}.qualifiedName);{Environment.NewLine}");

                                        if (!ruleGenerationContext.IsNextElementNewLineTerminal())
                                        {
                                            writer.WriteSafeString("stringBuilder.Append(' ');");
                                        }
                                    }
                                    else
                                    {
                                        writer.WriteSafeString($"{Environment.NewLine}if (poco.{targetPropertyName} != null){Environment.NewLine}");
                                        writer.WriteSafeString($"{{{Environment.NewLine}");
                                        writer.WriteSafeString($"stringBuilder.Append(poco.{targetPropertyName}.qualifiedName);{Environment.NewLine}");

                                        if (!ruleGenerationContext.IsNextElementNewLineTerminal())
                                        {
                                            writer.WriteSafeString("stringBuilder.Append(' ');");
                                        }

                                        writer.WriteSafeString($"{Environment.NewLine}}}");
                                    }

                                    break;
                                default:
                                    var handCodedRuleName = assignmentElement.TextualNotationRule?.RuleName ?? "Unknown";
                                    writer.WriteSafeString($"Build{handCodedRuleName}HandCoded({ruleGenerationContext.CurrentVariableName ?? "poco"}, cursorCache, stringBuilder);");
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
                writer.WriteSafeString($"Build{assignmentElement.Property.CapitalizeFirstLetter()}(poco, cursorCache, stringBuilder);");
            }
        }

        /// <summary>
        /// Process a <see cref="NonTerminalElement" />
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="nonTerminalElement">The <see cref="NonTerminalElement" /> to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <param name="isPartOfMultipleAlternative">Asserts that the current <see cref="NonTerminalElement" /> is part of a multiple alternative process</param>
        private static void ProcessNonTerminalElement(EncodedTextWriter writer, IClass umlClass, NonTerminalElement nonTerminalElement, RuleGenerationContext ruleGenerationContext, bool isPartOfMultipleAlternative = false)
        {
            var referencedRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == nonTerminalElement.Name);

            string typeTarget;

            if (referencedRule == null)
            {
                typeTarget = umlClass.Name;
            }
            else
            {
                typeTarget = referencedRule.TargetElementName ?? referencedRule.RuleName;
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
                EmitCollectionNonTerminalLoop(writer, umlClass, nonTerminalElement, referencedRule, typeTarget, ruleGenerationContext);

                if (emitPropertyNullGuard)
                {
                    writer.WriteSafeString($"{Environment.NewLine}}}");
                }

                return;
            }

            if (typeTarget != ruleGenerationContext.NamedElementToGenerate.Name)
            {
                var targetType = umlClass.Cache.Values.OfType<INamedElement>().SingleOrDefault(x => x.Name == typeTarget);

                if (targetType != null)
                {
                    if (targetType is IClass targetClass && (
                            umlClass.QueryAllGeneralClassifiers().Contains(targetClass) // upward cast: umlClass IS-A targetClass
                            || (targetClass.QueryAllGeneralClassifiers().Contains(umlClass) // downward cast: targetClass IS-A umlClass
                                && ruleGenerationContext.CurrentVariableName.Contains('.')) //   ONLY for property access; direct poco access is handled by inlining the body (else branch)
                            || targetClass == umlClass // same type
                            || !ruleGenerationContext.CurrentVariableName.Contains("poco")))
                    {
                        // Emit a cast when the current type is not guaranteed to be the target type:
                        // - CallerRule is AssignmentElement (processing an element of a += collection)
                        // - Downward cast via property access (e.g., poco.Prop is ITarget x)
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

                        writer.WriteSafeString($"{targetType.Name}TextualNotationBuilder.Build{nonTerminalElement.Name}({ruleGenerationContext.CurrentVariableName}, cursorCache, stringBuilder);");

                        if (emittedCondition)
                        {
                            writer.WriteSafeString($"{Environment.NewLine}}}");
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

                        ProcessAlternatives(writer, umlClass, referencedRule?.Alternatives, ruleGenerationContext, isPartOfMultipleAlternative);
                        ruleGenerationContext.CallerRule = previousCaller;
                        ruleGenerationContext.CurrentVariableName = previousName;
                    }
                }
                else
                {
                    if (IsSharedNoTargetRule(referencedRule, umlClass))
                    {
                        EmitSharedNoTargetRuleCall(writer, umlClass, nonTerminalElement, referencedRule, ruleGenerationContext);
                    }
                    else
                    {
                        var previousCaller = ruleGenerationContext.CallerRule;
                        ruleGenerationContext.CallerRule = nonTerminalElement;
                        var previousName = ruleGenerationContext.CurrentVariableName;

                        ProcessAlternatives(writer, umlClass, referencedRule?.Alternatives, ruleGenerationContext, isPartOfMultipleAlternative);
                        ruleGenerationContext.CallerRule = previousCaller;
                        ruleGenerationContext.CurrentVariableName = previousName;
                    }
                }
            }
            else
            {
                // When referencedRule is null, this is a handcoded stub (non-existing grammar rule).
                // Handcoded stubs take the parent POCO, not the cursor element.
                var variableToUse = referencedRule != null ? ruleGenerationContext.CurrentVariableName : "poco";

                var emittedSameClassCondition = TryEmitOptionalCondition(writer, nonTerminalElement, referencedRule, umlClass, ruleGenerationContext, variableToUse);

                writer.WriteSafeString($"Build{nonTerminalElement.Name}({variableToUse}, cursorCache, stringBuilder);");

                if (emittedSameClassCondition)
                {
                    writer.WriteSafeString($"{Environment.NewLine}}}");
                }
            }

            if (emitPropertyNullGuard)
            {
                writer.WriteSafeString($"{Environment.NewLine}}}");
            }
        }

        /// <summary>
        /// Emits a while loop for a collection NonTerminal (e.g., <c>ActionBodyItem*</c>).
        /// Resolves which POCO collection property the rule consumes by recursively tracing through
        /// NonTerminal references, then emits cursor creation + while loop + per-item builder call.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="nonTerminalElement">The <see cref="NonTerminalElement" /> with <c>*</c> or <c>+</c> suffix</param>
        /// <param name="referencedRule">
        /// The resolved <see cref="TextualNotationRule" /> for the NonTerminal, or <c>null</c>
        /// </param>
        /// <param name="typeTarget">The resolved target type name for the builder class</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <summary>
        /// Declares a single cursor for an enumerable assignment property if not already declared.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="assignmentElement">The <see cref="AssignmentElement" /> requiring a cursor</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        private static void DeclareCursorIfRequired(EncodedTextWriter writer, IClass umlClass, AssignmentElement assignmentElement, RuleGenerationContext ruleGenerationContext)
        {
            var allProperties = umlClass.QueryAllProperties();
            var targetProperty = allProperties.SingleOrDefault(x => string.Equals(x.Name, assignmentElement.Property, StringComparison.OrdinalIgnoreCase));

            if (targetProperty == null || !targetProperty.QueryIsEnumerable())
            {
                return;
            }

            // Check if a cursor already exists for this property
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
                    writer.WriteSafeString($"var {cursorToUse.CursorVariableName} = cursorCache.GetOrCreateCursor(poco.Id, \"{targetProperty.Name}\", poco.{propertyAccessName});");
                    writer.WriteSafeString(Environment.NewLine);
                    ruleGenerationContext.DefinedCursors.Add(cursorToUse);
                    break;
                }
                case AssignmentElement containedAssignment:
                    DeclareCursorIfRequired(writer, umlClass, containedAssignment, ruleGenerationContext);
                    break;
            }
        }
    }
}
