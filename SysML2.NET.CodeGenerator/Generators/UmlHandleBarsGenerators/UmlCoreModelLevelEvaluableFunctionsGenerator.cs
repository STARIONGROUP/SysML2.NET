// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreModelLevelEvaluableFunctionsGenerator.cs" company="Starion Group S.A.">
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

    using SysML2.NET.CodeGenerator.Grammar.Model;
    using SysML2.NET.CodeGenerator.Library;

    using uml4net.Values;
    using uml4net.xmi.Readers;

    /// <summary>
    /// A UML Handlebars generator that produces the <c>ModelLevelEvaluableFunctions</c> membership set
    /// backing <c>Function::isModelLevelEvaluable</c>, per KerML 1.0 Table 5 (§8.2.5.8.1) and Table 7
    /// (§8.2.5.8.2). The output lands in the <c>KernelFunctions/AutoGenKernelFunctions</c> folder of
    /// the runtime project.
    /// </summary>
    /// <remarks>
    /// The two tables are reproduced rather than transcribed: the operator symbols come from the KEBNF
    /// grammar and the UML model, and the owning library package comes from the Kernel Function Library
    /// itself. Only the three rows the tables mark as not model-level evaluable are curated.
    /// </remarks>
    public class UmlCoreModelLevelEvaluableFunctionsGenerator : UmlHandleBarsGenerator
    {
        /// <summary>
        /// The name of the template for the <c>ModelLevelEvaluableFunctions</c> static class.
        /// </summary>
        private const string ModelLevelEvaluableFunctionsTemplateName = "core-model-level-evaluable-functions-template";

        /// <summary>
        /// The name of the <c>OperatorExpression::operator</c> property, both as a KEBNF assignment
        /// target and as a UML owned attribute.
        /// </summary>
        private const string OperatorPropertyName = "operator";

        /// <summary>
        /// The number of distinct operator symbols the KerML specification maps to a library
        /// <c>Function</c> — Table 5 contributes 33 and Table 7 contributes 6.
        /// </summary>
        private const int ExpectedOperatorSymbolCount = 39;

        /// <summary>
        /// The library packages an <c>OperatorExpression</c> operator resolves against, in the probe
        /// order mandated by the <c>OperatorExpression::instantiatedType</c> derivation. The order
        /// disambiguates <c>'=='</c> and <c>'==='</c>, which both packages declare.
        /// </summary>
        private static readonly string[] OperatorFunctionPackages = ["BaseFunctions", "DataFunctions", "ControlFunctions"];

        /// <summary>
        /// The only operators that KerML 1.0 Table 5 and Table 7 mark as NOT model-level evaluable.
        /// Abstractness does not discriminate them — <c>BaseFunctions::'=='</c> is abstract and is
        /// model-level evaluable — so they are curated rather than derived.
        /// </summary>
        private static readonly string[] NonModelLevelEvaluableFunctions = ["BaseFunctions::all", "BaseFunctions::[", "DataFunctions::~"];

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
            this.RegisterTemplate(ModelLevelEvaluableFunctionsTemplateName);
        }

        /// <summary>
        /// Not supported — this generator requires the KEBNF grammar and the Kernel Function Library.
        /// Use <see cref="GenerateAsync(XmiReaderResult, TextualNotationSpecification, string, DirectoryInfo)"/>.
        /// </summary>
        /// <param name="xmiReaderResult">The <see cref="XmiReaderResult"/></param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo"/></param>
        /// <returns>nothing — always throws</returns>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        public override Task GenerateAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            throw new NotSupportedException("The generator needs TextualNotationSpecification and Kernel Function Library access");
        }

        /// <summary>
        /// Generates the <c>ModelLevelEvaluableFunctions</c> membership set.
        /// </summary>
        /// <param name="xmiReaderResult">The UML model supplying the defaulted <c>operator</c> values</param>
        /// <param name="textualNotationSpecification">The KerML grammar supplying the operator terminals</param>
        /// <param name="kernelFunctionLibraryPath">The path of the Kernel Function Library <c>.kermlx</c> folder</param>
        /// <param name="outputDirectory">The target <see cref="DirectoryInfo"/></param>
        /// <returns>an awaitable <see cref="Task"/></returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the derived operator set drifts from the specification — an unexpected symbol
        /// count, a symbol no library package declares, or a curated exclusion that is not derived.
        /// </exception>
        public async Task GenerateAsync(XmiReaderResult xmiReaderResult, TextualNotationSpecification textualNotationSpecification, string kernelFunctionLibraryPath, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(textualNotationSpecification);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            var operatorSymbols = CollectOperatorSymbols(xmiReaderResult, textualNotationSpecification);

            if (operatorSymbols.Count != ExpectedOperatorSymbolCount)
            {
                throw new InvalidOperationException($"Expected {ExpectedOperatorSymbolCount} operator symbols per KerML Table 5 and Table 7 but derived {operatorSymbols.Count}: {string.Join(", ", operatorSymbols)}");
            }

            var library = KernelFunctionLibraryReader.Read(kernelFunctionLibraryPath);

            var resolutions = operatorSymbols
                .Select(symbol => (Symbol: symbol, QualifiedName: ResolveQualifiedName(symbol, library)))
                .ToList();

            var unresolvedSymbols = resolutions
                .Where(resolution => resolution.QualifiedName == null)
                .Select(resolution => resolution.Symbol)
                .ToList();

            if (unresolvedSymbols.Count != 0)
            {
                throw new InvalidOperationException($"No Kernel Function Library package declares a Function for the operator(s): {string.Join(", ", unresolvedSymbols)}");
            }

            var qualifiedNames = resolutions.Select(resolution => resolution.QualifiedName).ToList();

            var undrivenExclusions = NonModelLevelEvaluableFunctions.Where(exclusion => !qualifiedNames.Contains(exclusion)).ToList();

            if (undrivenExclusions.Count != 0)
            {
                throw new InvalidOperationException($"The curated exclusion(s) {string.Join(", ", undrivenExclusions)} are not present in the derived operator set");
            }

            var modelLevelEvaluableFunctions = qualifiedNames
                .Where(qualifiedName => !NonModelLevelEvaluableFunctions.Contains(qualifiedName))
                .ToList();

            var template = this.Templates[ModelLevelEvaluableFunctionsTemplateName];
            var generated = template(new { QualifiedNames = modelLevelEvaluableFunctions });
            generated = this.CodeCleanup(generated);

            await WriteAsync(generated, outputDirectory, "ModelLevelEvaluableFunctions.cs");
        }

        /// <summary>
        /// Collects every operator symbol that the specification maps to a Kernel Function Library
        /// <c>Function</c>, from the KEBNF <c>operator = …</c> assignments and from the UML classes that
        /// default the <c>operator</c> attribute instead.
        /// </summary>
        /// <param name="xmiReaderResult">The UML model</param>
        /// <param name="textualNotationSpecification">The KerML grammar</param>
        /// <returns>The ordinal-sorted set of distinct operator symbols</returns>
        private static IReadOnlyList<string> CollectOperatorSymbols(XmiReaderResult xmiReaderResult, TextualNotationSpecification textualNotationSpecification)
        {
            var symbols = new SortedSet<string>(StringComparer.Ordinal);

            var operatorAssignments = textualNotationSpecification.Rules
                .SelectMany(rule => rule.Alternatives)
                .SelectMany(alternative => EnumerateAssignments(alternative.Elements))
                .Where(assignment => assignment.Property == OperatorPropertyName);

            foreach (var operatorAssignment in operatorAssignments)
            {
                symbols.UnionWith(ResolveAssignedSymbols(operatorAssignment, textualNotationSpecification));
            }

            symbols.UnionWith(CollectDefaultedOperatorSymbols(xmiReaderResult));

            return [.. symbols];
        }

        /// <summary>
        /// Collects the operator symbols carried as the default value of an <c>operator</c> owned
        /// attribute, which is how <c>IndexExpression</c>, <c>FeatureChainExpression</c>,
        /// <c>CollectExpression</c> and <c>SelectExpression</c> fix their operator.
        /// </summary>
        /// <param name="xmiReaderResult">The UML model</param>
        /// <returns>The defaulted operator symbols</returns>
        private static IEnumerable<string> CollectDefaultedOperatorSymbols(XmiReaderResult xmiReaderResult)
        {
            return CreateHandlebarsPayload(xmiReaderResult).Classes
                .SelectMany(@class => @class.OwnedAttribute)
                .Where(ownedAttribute => ownedAttribute.Name == OperatorPropertyName)
                .SelectMany(ownedAttribute => ownedAttribute.DefaultValue.OfType<ILiteralString>())
                .Select(literalString => literalString.Value)
                .Where(value => !string.IsNullOrWhiteSpace(value));
        }

        /// <summary>
        /// Flattens the assignments of a rule alternative, descending into grouped alternatives.
        /// </summary>
        /// <param name="elements">The rule elements to flatten</param>
        /// <returns>Every <see cref="AssignmentElement"/> reachable from the supplied elements</returns>
        private static IEnumerable<AssignmentElement> EnumerateAssignments(IEnumerable<RuleElement> elements)
        {
            return elements.SelectMany(EnumerateAssignmentsOf);
        }

        /// <summary>
        /// Flattens the assignments of a single rule element, descending into grouped alternatives.
        /// </summary>
        /// <param name="element">The rule element to flatten</param>
        /// <returns>Every <see cref="AssignmentElement"/> reachable from the supplied element</returns>
        private static IEnumerable<AssignmentElement> EnumerateAssignmentsOf(RuleElement element)
        {
            switch (element)
            {
                case AssignmentElement assignment:
                    return [assignment];

                case GroupElement group:
                    return group.Alternatives.SelectMany(alternative => EnumerateAssignments(alternative.Elements));

                default:
                    return [];
            }
        }

        /// <summary>
        /// Resolves the operator symbols an <c>operator = …</c> assignment can take — either the
        /// assigned terminal itself, or every terminal of the referenced token rule.
        /// </summary>
        /// <param name="assignment">The <c>operator</c> assignment</param>
        /// <param name="textualNotationSpecification">The KerML grammar</param>
        /// <returns>The operator symbols the assignment admits</returns>
        private static IEnumerable<string> ResolveAssignedSymbols(AssignmentElement assignment, TextualNotationSpecification textualNotationSpecification)
        {
            switch (assignment.Value)
            {
                case TerminalElement terminal:
                    return [terminal.Value];

                case NonTerminalElement nonTerminal:
                    var referencedRule = textualNotationSpecification.Rules.FirstOrDefault(rule => rule.RuleName == nonTerminal.Name);

                    return referencedRule == null
                        ? []
                        : referencedRule.Alternatives
                            .SelectMany(alternative => alternative.Elements.OfType<TerminalElement>())
                            .Select(terminalElement => terminalElement.Value);

                default:
                    return [];
            }
        }

        /// <summary>
        /// Resolves the library <c>Function</c> an operator symbol denotes, by probing the operator
        /// packages in specification order and taking the first that declares the symbol.
        /// </summary>
        /// <param name="symbol">The operator symbol</param>
        /// <param name="library">The Kernel Function Library, keyed by package name</param>
        /// <returns>The raw <c>Package::Function</c> name, or <c>null</c> when no package declares it</returns>
        private static string ResolveQualifiedName(string symbol, IReadOnlyDictionary<string, IReadOnlyList<string>> library)
        {
            var declaringPackage = OperatorFunctionPackages
                .FirstOrDefault(package => library.TryGetValue(package, out var functions) && functions.Contains(symbol));

            return declaringPackage == null ? null : $"{declaringPackage}::{symbol}";
        }
    }
}
