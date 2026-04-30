// -------------------------------------------------------------------------------------------------
// <copyright file="RulesHelper.TypeResolution.cs" company="Starion Group S.A.">
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
    /// Type resolution, builder call generation, and optional condition logic
    /// </summary>
    public static partial class RulesHelper
    {
        /// <summary>
        /// The name of the shared builder class that hosts all no-target rules that do not
        /// have a matching UML class (e.g. <c>FeaturePrefix</c>).
        /// </summary>
        public const string SharedBuilderClassName = "SharedTextualNotationBuilder";

        /// <summary>
        /// Resolves a content-aware type guard for collection while loops by discovering
        /// complementary composite properties and validating inner content types.
        /// </summary>
        /// <param name="cursorVariableName">The cursor variable name</param>
        /// <param name="referencedRule">The grammar rule being referenced</param>
        /// <param name="outerPropertyName">The outer assignment property name</param>
        /// <param name="umlClass">The UML class providing the type cache</param>
        /// <param name="ruleGenerationContext">The current generation context</param>
        /// <returns>A compound while condition string, or null if not resolvable</returns>
        private static string ResolveContentTypeGuard(string cursorVariableName, TextualNotationRule referencedRule, string outerPropertyName, IClass umlClass, RuleGenerationContext ruleGenerationContext)
        {
            if (referencedRule == null)
            {
                return null;
            }

            // Resolve the outer target type (the rule's own target, e.g., OwningMembership)
            var outerTargetName = referencedRule.TargetElementName ?? referencedRule.RuleName;
            var outerTargetClass = umlClass.Cache.Values.OfType<INamedElement>().SingleOrDefault(x => x.Name == outerTargetName) as IClass;

            if (outerTargetClass == null)
            {
                return null;
            }

            // Discover composite aggregation properties from the outer target class AND its
            // full inheritance hierarchy. Properties like ownedRelatedElement are defined on
            // base types (e.g., Relationship) and may not appear on the direct class.
            var allClassesInHierarchy = new List<IClass> { outerTargetClass };
            allClassesInHierarchy.AddRange(outerTargetClass.QueryAllGeneralClassifiers().OfType<IClass>());

            var compositeProperties = allClassesInHierarchy
                .SelectMany(c => c.OwnedAttribute)
                .Where(p => p.IsComposite && !p.IsDerived)
                .ToList();

            var complementaryProperty = compositeProperties
                .FirstOrDefault(p => !string.Equals(p.Name, outerPropertyName, StringComparison.OrdinalIgnoreCase));

            if (complementaryProperty == null)
            {
                // No complementary composite property exists on this class (e.g., Usage is not
                // a Relationship). Follow the rule's same-property assignment one level deeper:
                // if the rule has ownedRelationship += PrefixMetadataMember, recurse into
                // PrefixMetadataMember which IS a Relationship and has ownedRelatedElement.
                var samePropertyAssignment = referencedRule.Alternatives
                    .SelectMany(alt => alt.Elements)
                    .OfType<AssignmentElement>()
                    .FirstOrDefault(a => (a.Operator == "+=" || a.Operator == "=")
                                         && string.Equals(a.Property, outerPropertyName, StringComparison.OrdinalIgnoreCase)
                                         && a.Value is NonTerminalElement);

                if (samePropertyAssignment?.Value is NonTerminalElement innerNonTerminal)
                {
                    var innerRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == innerNonTerminal.Name);

                    if (innerRule != null)
                    {
                        return ResolveContentTypeGuard(cursorVariableName, innerRule, outerPropertyName, umlClass, ruleGenerationContext);
                    }
                }

                return null;
            }

            // Look at the referenced rule's body for += assignments on the complementary property
            var contentAssignment = referencedRule.Alternatives
                .SelectMany(alt => alt.Elements)
                .OfType<AssignmentElement>()
                .FirstOrDefault(a => (a.Operator == "+=" || a.Operator == "=")
                                     && string.Equals(a.Property, complementaryProperty.Name, StringComparison.OrdinalIgnoreCase)
                                     && a.Value is NonTerminalElement);

            if (contentAssignment == null)
            {
                return null;
            }

            // Resolve the content type by following the assignment's value rule
            var contentNonTerminal = (NonTerminalElement)contentAssignment.Value;
            var contentRule = ruleGenerationContext.AllRules.SingleOrDefault(x => x.RuleName == contentNonTerminal.Name);

            var contentTargetName = contentRule != null
                ? contentRule.TargetElementName ?? contentRule.RuleName
                : contentNonTerminal.Name;

            var contentTargetClass = umlClass.Cache.Values.OfType<INamedElement>().SingleOrDefault(x => x.Name == contentTargetName) as IClass;

            if (contentTargetClass == null)
            {
                return null;
            }

            var outerTypeName = outerTargetClass.QueryFullyQualifiedTypeName();
            var contentTypeName = contentTargetClass.QueryFullyQualifiedTypeName();
            var complementaryAccessor = complementaryProperty.QueryPropertyNameBasedOnUmlProperties();
            var guardVarName = $"{outerTargetClass.Name.LowerCaseFirstLetter()}Guard";

            return $"{cursorVariableName}.Current is {outerTypeName} {guardVarName} && {guardVarName}.{complementaryAccessor}.OfType<{contentTypeName}>().Any()";
        }

        /// <summary>
        /// Resolves a type condition clause for the while loop in a collection NonTerminal loop.
        /// When the collection has sibling elements after it, the while loop must stop before consuming those elements.
        /// <para>
        /// The primary approach is a <b>positive</b> condition based on the collection item's own assignment target type
        /// (e.g., <c>UsageExtensionKeyword</c> assigns to <c>PrefixMetadataMember:OwningMembership</c> →
        /// <c>while (cursor.Current is IOwningMembership)</c>).
        /// </para>
        /// <para>
        /// Falls back to a <b>negative</b> exclusion based on the next sibling's target type when the positive approach
        /// is not available (e.g., <c>CalculationBodyItem* ResultExpressionMember</c> →
        /// <c>while (cursor.Current is not IResultExpressionMembership)</c>).
        /// </para>
        /// </summary>
        /// <param name="cursorVariableName">The cursor variable name used in the while condition</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="collectionRule">The resolved <see cref="TextualNotationRule" /> for the collection NonTerminal</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <returns>A type condition clause string, or empty string if no condition is needed</returns>
        /// <summary>
        /// Resolves the builder method call string for a non-terminal element.
        /// </summary>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="nonTerminalElement">The <see cref="NonTerminalElement" /> to resolve</param>
        /// <param name="typeTarget">The resolved target type name</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <returns>The builder call string, or null if not resolvable</returns>
        private static string ResolveBuilderCall(IClass umlClass, NonTerminalElement nonTerminalElement, string typeTarget, RuleGenerationContext ruleGenerationContext)
        {
            if (typeTarget == ruleGenerationContext.NamedElementToGenerate.Name)
            {
                return $"Build{nonTerminalElement.Name}({ruleGenerationContext.CurrentVariableName}, cursorCache, stringBuilder);";
            }

            var targetType = umlClass.Cache.Values.OfType<INamedElement>().SingleOrDefault(x => x.Name == typeTarget);

            if (targetType is IClass targetClass)
            {
                // Check type compatibility: the target class must be the same as or a superclass of the current class
                // e.g., Type is a superclass of AcceptActionUsage → compatible (IAcceptActionUsage IS an IType)
                // e.g., Package is a subclass of Namespace → NOT compatible (INamespace is NOT an IPackage)
                if (umlClass.QueryAllGeneralClassifiers().Contains(targetClass))
                {
                    return $"{targetType.Name}TextualNotationBuilder.Build{nonTerminalElement.Name}({ruleGenerationContext.CurrentVariableName}, cursorCache, stringBuilder);";
                }

                // Type is not compatible — caller should inline the rule alternatives
                return null;
            }

            return $"Build{nonTerminalElement.Name}({ruleGenerationContext.CurrentVariableName}, cursorCache, stringBuilder);";
        }

        /// <summary>
        /// Declares a cursor to perform iteration over a collection, if required to declare it.
        /// Uses <c>cursorCache.GetOrCreateCursor</c> to share cursor state across method calls.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write into output content</param>
        /// <param name="umlClass">The related <see cref="IClass" /></param>
        /// <param name="assignmentElement">The <see cref="AssignmentElement" /> to process</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <summary>
        /// Generates an inline condition expression for an optional non-terminal reference.
        /// </summary>
        /// <param name="referencedRule">The referenced grammar rule</param>
        /// <param name="targetClass">The target <see cref="IClass" /></param>
        /// <param name="allRules">All available grammar rules</param>
        /// <param name="variableName">The variable name for the condition</param>
        /// <returns>A condition expression string, or null if not resolvable</returns>
        private static string GenerateInlineOptionalCondition(TextualNotationRule referencedRule, IClass targetClass, IReadOnlyList<TextualNotationRule> allRules, string variableName)
        {
            var propertyNames = referencedRule.QueryAllReferencedPropertyNames(allRules);

            if (propertyNames.Count == 0)
            {
                return null;
            }

            var allProperties = targetClass.QueryAllProperties();
            var conditionParts = new List<string>();

            foreach (var propertyName in propertyNames)
            {
                var property = allProperties.FirstOrDefault(x => string.Equals(x.Name, propertyName, StringComparison.OrdinalIgnoreCase));

                if (property == null)
                {
                    continue;
                }

                var umlPropertyName = property.QueryPropertyNameBasedOnUmlProperties();

                if (property.QueryIsEnumerable())
                {
                    conditionParts.Add($"{variableName}.{umlPropertyName}.Count != 0");
                }
                else
                {
                    conditionParts.Add(property.QueryIfStatementContentForNonEmpty(variableName));
                }
            }

            return conditionParts.Count != 0 ? string.Join(" || ", conditionParts) : null;
        }

        /// <summary>
        /// Emits an optional condition wrapping block for an optional NonTerminal element.
        /// When the referenced rule has resolvable properties, emits an inline condition.
        /// Otherwise emits no condition (the call proceeds unconditionally).
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write into output content</param>
        /// <param name="nonTerminalElement">The optional <see cref="NonTerminalElement" /></param>
        /// <param name="referencedRule">The resolved <see cref="TextualNotationRule" />, or <c>null</c> for handcoded stubs</param>
        /// <param name="targetClass">The <see cref="IClass" /> on which properties are resolved (the class the referenced rule targets)</param>
        /// <param name="ruleGenerationContext">The current <see cref="RuleGenerationContext" /></param>
        /// <param name="variableName">
        /// The variable name to use in the condition (e.g., <c>poco</c> or
        /// <c>poco.ownedMemberFeature</c>)
        /// </param>
        /// <returns><c>true</c> if a condition block was opened (caller must close it), <c>false</c> otherwise</returns>
        private static bool TryEmitOptionalCondition(EncodedTextWriter writer, NonTerminalElement nonTerminalElement, TextualNotationRule referencedRule, IClass targetClass, RuleGenerationContext ruleGenerationContext, string variableName)
        {
            if (!nonTerminalElement.IsOptional || nonTerminalElement.IsCollection)
            {
                return false;
            }

            if (referencedRule == null)
            {
                return false;
            }

            var condition = GenerateInlineOptionalCondition(referencedRule, targetClass, ruleGenerationContext.AllRules, variableName);

            if (condition == null)
            {
                return false;
            }

            writer.WriteSafeString($"{Environment.NewLine}if ({condition}){Environment.NewLine}");
            writer.WriteSafeString($"{{{Environment.NewLine}");
            return true;
        }
    }
}
