// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreLexicalRulesGenerator.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.Grammar.Model;

    using uml4net.xmi.Readers;

    /// <summary>
    /// A UML Handlebars generator that produces the lexical-rule support classes
    /// (<c>Keywords</c>, <c>SymbolicKeywordKind</c>, <c>SymbolicKeywordKindExtensions</c>) from
    /// the SysML v2 textual-notation KEBNF grammar. The output lands in the
    /// <c>LexicalRules/AutoGenLexicalRules</c> folder of the runtime project.
    /// </summary>
    public class UmlCoreLexicalRulesGenerator : UmlHandleBarsGenerator
    {
        /// <summary>
        /// Gets the name of the template for the <c>Keywords</c> static class.
        /// </summary>
        private const string KeywordsTemplateName = "core-lexical-keywords-template";

        /// <summary>
        /// Gets the name of the template for the <c>SymbolicKeywordKind</c> enum.
        /// </summary>
        private const string SymbolicKeywordKindTemplateName = "core-lexical-symbolic-keyword-kind-template";

        /// <summary>
        /// Gets the name of the template for the <c>SymbolicKeywordKindExtensions</c> class.
        /// </summary>
        private const string SymbolicKeywordKindExtensionsTemplateName = "core-lexical-symbolic-keyword-kind-extensions-template";

        /// <summary>
        /// Register the custom helpers
        /// </summary>
        protected override void RegisterHelpers()
        {
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate(KeywordsTemplateName);
            this.RegisterTemplate(SymbolicKeywordKindTemplateName);
            this.RegisterTemplate(SymbolicKeywordKindExtensionsTemplateName);
        }

        /// <summary>
        /// Not supported — this generator requires the <see cref="TextualNotationSpecification"/>.
        /// Use <see cref="GenerateAsync(XmiReaderResult, TextualNotationSpecification, DirectoryInfo)"/>.
        /// </summary>
        /// <param name="xmiReaderResult">The <see cref="XmiReaderResult"/></param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo"/></param>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        /// <returns>nothing — always throws</returns>
        public override Task GenerateAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            throw new NotSupportedException("The generator needs TextualNotationSpecification access");
        }

        /// <summary>
        /// Generates the lexical-rule support classes from the supplied
        /// <see cref="TextualNotationSpecification"/>.
        /// </summary>
        /// <param name="xmiReaderResult">The <see cref="XmiReaderResult"/> (unused; retained for interface symmetry)</param>
        /// <param name="textualNotationSpecification">The grammar specification to project from</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo"/></param>
        /// <returns>an awaitable <see cref="Task"/></returns>
        public async Task GenerateAsync(XmiReaderResult xmiReaderResult, TextualNotationSpecification textualNotationSpecification, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(textualNotationSpecification);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            await this.GenerateKeywordsAsync(textualNotationSpecification, outputDirectory);
            await this.GenerateSymbolicKeywordKindAsync(textualNotationSpecification, outputDirectory);
        }

        /// <summary>
        /// Generates the <c>Keywords</c> static class from the <c>RESERVED_KEYWORD</c> rule.
        /// </summary>
        /// <param name="textualNotationSpecification">The grammar specification</param>
        /// <param name="outputDirectory">The target directory</param>
        /// <returns>an awaitable <see cref="Task"/></returns>
        private async Task GenerateKeywordsAsync(TextualNotationSpecification textualNotationSpecification, DirectoryInfo outputDirectory)
        {
            var reservedKeywordRule = textualNotationSpecification.Rules.FirstOrDefault(x => x.RuleName == "RESERVED_KEYWORD");

            if (reservedKeywordRule == null)
            {
                return;
            }

            var keywords = reservedKeywordRule.Alternatives
                .SelectMany(alternative => alternative.Elements.OfType<TerminalElement>())
                .Select(terminal => terminal.Value)
                .Where(value => !string.IsNullOrWhiteSpace(value))
                .ToList();

            if (keywords.Count == 0)
            {
                return;
            }

            var template = this.Templates[KeywordsTemplateName];
            var generated = template(new { Keywords = keywords });
            generated = this.CodeCleanup(generated);

            await WriteAsync(generated, outputDirectory, "Keywords.cs");
        }

        /// <summary>
        /// Generates the <c>SymbolicKeywordKind</c> enum and its <c>SymbolicKeywordKindExtensions</c>
        /// companion. A rule qualifies as "symbolic-keyword" when its name is all uppercase, it has
        /// exactly two alternatives, the first alternative is a single non-alphabetic terminal, and
        /// the second alternative starts with an alphabetic keyword terminal.
        /// </summary>
        /// <param name="textualNotationSpecification">The grammar specification</param>
        /// <param name="outputDirectory">The target directory</param>
        /// <returns>an awaitable <see cref="Task"/></returns>
        private async Task GenerateSymbolicKeywordKindAsync(TextualNotationSpecification textualNotationSpecification, DirectoryInfo outputDirectory)
        {
            var kinds = CollectSymbolicKeywordKinds(textualNotationSpecification);

            if (kinds.Count == 0)
            {
                return;
            }

            var enumTemplate = this.Templates[SymbolicKeywordKindTemplateName];
            var enumGenerated = enumTemplate(new { Kinds = kinds });
            enumGenerated = this.CodeCleanup(enumGenerated);

            await WriteAsync(enumGenerated, outputDirectory, "SymbolicKeywordKind.cs");

            var extensionsTemplate = this.Templates[SymbolicKeywordKindExtensionsTemplateName];
            var extensionsGenerated = extensionsTemplate(new { Kinds = kinds });
            extensionsGenerated = this.CodeCleanup(extensionsGenerated);

            await WriteAsync(extensionsGenerated, outputDirectory, "SymbolicKeywordKindExtensions.cs");
        }

        /// <summary>
        /// Projects every grammar rule that matches the "symbolic-keyword" shape into an entry
        /// consumed by both the enum and extensions templates.
        /// </summary>
        /// <param name="textualNotationSpecification">The grammar specification</param>
        /// <returns>The ordered set of projected entries</returns>
        private static List<object> CollectSymbolicKeywordKinds(TextualNotationSpecification textualNotationSpecification)
        {
            var kinds = new List<object>();

            foreach (var rule in textualNotationSpecification.Rules)
            {
                if (!TryResolveSymbolicKeywordRule(rule, out var symbolicNotation, out var keywordNotation))
                {
                    continue;
                }

                kinds.Add(new
                {
                    RuleName = rule.RuleName,
                    PascalName = rule.RuleName.ToPascalCaseFromSnake(),
                    SymbolicNotation = symbolicNotation,
                    KeywordNotation = keywordNotation
                });
            }

            return kinds;
        }

        /// <summary>
        /// Tests whether a rule matches the <c>RULE_NAME = '&lt;sym&gt;' | '&lt;keyword&gt;' …</c>
        /// shape and, when it does, extracts the symbolic and keyword notations.
        /// </summary>
        /// <param name="rule">The candidate rule</param>
        /// <param name="symbolicNotation">The pure-symbol alternative value when matched</param>
        /// <param name="keywordNotation">The keyword alternative value (space-joined if multi-token) when matched</param>
        /// <returns><c>true</c> if the rule qualifies</returns>
        private static bool TryResolveSymbolicKeywordRule(TextualNotationRule rule, out string symbolicNotation, out string keywordNotation)
        {
            symbolicNotation = null;
            keywordNotation = null;

            if (rule == null || !string.IsNullOrWhiteSpace(rule.TargetElementName))
            {
                return false;
            }

            if (!rule.RuleName.IsAllUpperSnake())
            {
                return false;
            }

            if (rule.Alternatives.Count != 2)
            {
                return false;
            }

            var firstAlternative = rule.Alternatives[0].Elements.OfType<TerminalElement>().ToList();
            var secondAlternative = rule.Alternatives[1].Elements.OfType<TerminalElement>().ToList();

            if (firstAlternative.Count != 1 || secondAlternative.Count == 0)
            {
                return false;
            }

            if (firstAlternative[0].Value.ContainsAnyLetter())
            {
                return false;
            }

            if (!secondAlternative[0].Value.ContainsAnyLetter())
            {
                return false;
            }

            symbolicNotation = firstAlternative[0].Value;
            keywordNotation = string.Join(' ', secondAlternative.Select(x => x.Value));
            return true;
        }
    }
}
