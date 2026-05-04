// -------------------------------------------------------------------------------------------------
// <copyright file="RulesHelper.cs" company="Starion Group S.A.">
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

    using SysML2.NET.CodeGenerator.Grammar.Model;

    using uml4net.CommonStructure;
    using uml4net.StructuredClassifiers;

    /// <summary>
    /// Provides textual notation rules related helper for <see cref="IHandlebars" />
    /// </summary>
    public static class RulesHelper
    {
        /// <summary>
        /// The name of the shared builder class that hosts all no-target rules that do not
        /// have a matching UML class (e.g. <c>FeaturePrefix</c>).
        /// </summary>
        public const string SharedBuilderClassName = "SharedTextualNotationBuilder";

        /// <summary>
        /// Register this helper
        /// </summary>
        /// <param name="handlebars">The <see cref="IHandlebars" /> context with which the helper needs to be registered</param>
        public static void RegisterRulesHelper(this IHandlebars handlebars)
        {
            var processor = new RuleProcessor();

            handlebars.RegisterHelper("RulesHelper.ContainsAnyDispatcherRules", (_, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new ArgumentException("RulesHelper.ContainsAnyDispatcherRules expects to have 3 arguments");
                }

                return arguments[0] is not List<TextualNotationRule> allRules
                    ? throw new ArgumentException("RulesHelper.ContainsAnyDispatcherRules expects a list of TextualNotationRule as only argument")
                    : allRules.Any(x => x.IsDispatcherRule);
            });

            handlebars.RegisterHelper("RulesHelper.WriteRule", (writer, _, arguments) =>
            {
                if (arguments.Length != 3)
                {
                    throw new ArgumentException("RulesHelper.WriteRule expects to have 3 arguments");
                }

                if (arguments[0] is not TextualNotationRule textualRule)
                {
                    throw new ArgumentException("RulesHelper.WriteRule expects TextualNotationRule as first argument");
                }

                if (arguments[1] is not INamedElement namedElement)
                {
                    throw new ArgumentException("RulesHelper.WriteRule expects INamedElement as second argument");
                }

                if (arguments[2] is not List<TextualNotationRule> allRules)
                {
                    throw new ArgumentException("RulesHelper.WriteRule expects a list of TextualNotationRule as third argument");
                }

                if (namedElement is IClass umlClass)
                {
                    var ruleGenerationContext = new RuleGenerationContext(namedElement)
                    {
                        CurrentVariableName = "poco"
                    };

                    ruleGenerationContext.AllRules.AddRange(allRules);
                    processor.ProcessAlternatives(writer, umlClass, textualRule.Alternatives, ruleGenerationContext);
                }
            });
        }

        /// <summary>
        /// Resolves the effective target class for a no-target rule by analyzing its assignments.
        /// </summary>
        /// <param name="rule">The <see cref="TextualNotationRule" /> to analyze</param>
        /// <param name="allRules">All available grammar rules</param>
        /// <param name="cacheSource">An <see cref="IClass" /> providing access to the UML model cache</param>
        /// <returns>The resolved <see cref="IClass" />, or null if not resolvable</returns>
        public static IClass ResolveNoTargetRuleEffectiveTarget(TextualNotationRule rule, IReadOnlyList<TextualNotationRule> allRules, IClass cacheSource)
        {
            return NoTargetRuleResolver.ResolveEffectiveTarget(rule, allRules, cacheSource);
        }

        /// <summary>
        /// Determines whether a no-target rule should be lifted into the shared builder class.
        /// </summary>
        /// <param name="rule">The rule to test</param>
        /// <param name="cacheSource">Any <see cref="IClass" /> from the loaded model used to access <c>Cache</c></param>
        /// <returns><c>true</c> when the rule should be generated into the shared builder</returns>
        public static bool IsSharedNoTargetRule(TextualNotationRule rule, IClass cacheSource)
        {
            return NoTargetRuleResolver.IsSharedRule(rule, cacheSource);
        }
    }
}
