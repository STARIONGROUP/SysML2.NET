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
    using System.Collections.Generic;
    using System.Linq;

    using HandlebarsDotNet;

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.Grammar.Model;

    using uml4net.CommonStructure;
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
                        EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
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
                        ProcessNonTerminalElement(writer, umlClass, nonTerminalElement, ruleGenerationContext);
                        ruleGenerationContext.CurrentVariableName = previousVariableName;
                        ruleGenerationContext.CallerRule = previousCaller;

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
                        EmitHandCodedFallback(writer, handCodedRuleName, ruleGenerationContext);
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
                EmitCollectionNonTerminalLoop(writer, umlClass, nonTerminalElement, referencedRule, typeTarget, ruleGenerationContext);

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
                    if (NoTargetRuleResolver.IsSharedRule(referencedRule, umlClass))
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

        /// <summary>
        /// Emits a call to the shared no-target rule builder.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write output</param>
        /// <param name="umlClass">The caller's <see cref="IClass" /></param>
        /// <param name="nonTerminalElement">The <see cref="NonTerminalElement" /> being processed</param>
        /// <param name="referencedRule">The referenced shared no-target <see cref="TextualNotationRule" /></param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        private void EmitSharedNoTargetRuleCall(EncodedTextWriter writer, IClass umlClass, NonTerminalElement nonTerminalElement, TextualNotationRule referencedRule, RuleGenerationContext ruleGenerationContext)
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

            writer.WriteSafeString($"{RulesHelper.SharedBuilderClassName}.Build{nonTerminalElement.Name}({variableExpression}, cursorCache, stringBuilder);");

            if (emittedCondition)
            {
                writer.WriteSafeString($"{Environment.NewLine}}}");
            }
        }
    }
}
