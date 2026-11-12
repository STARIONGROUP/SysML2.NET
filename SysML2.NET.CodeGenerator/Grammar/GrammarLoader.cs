// ------------------------------------------------------------------------------------------------
// <copyright file="GrammarLoader.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Grammar
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Antlr4.Runtime;

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.Grammar.Model;

    /// <summary>
    /// Provides direct access to <see cref="TextualNotationSpecification"/> by providing file path
    /// </summary>
    public static class GrammarLoader
    {
        /// <summary>
        /// Loads the <see cref="TextualNotationSpecification" /> 
        /// </summary>
        /// <param name="fileUri">The string uri that locates the KEBNF file to load</param>
        /// <returns>The loaded <see cref="TextualNotationSpecification"/></returns>
        /// <exception cref="FileNotFoundException">If the <paramref name="fileUri"/> does not locate an existing <see cref="File"/></exception>
        public static TextualNotationSpecification LoadTextualNotationSpecification(string fileUri)
        {
            if (!File.Exists(fileUri))
            {
                throw new FileNotFoundException("File not found", fileUri);
            }
            
            // The KEBNF files are OMG source and are never edited, so a defective production is corrected
            // here, on the way into the parser — see GrammarErrata.ApplyProductions.
            var stream = CharStreams.fromString(GrammarErrata.ApplyProductions(File.ReadAllText(fileUri)));
            var lexer = new kebnfLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new kebnfParser(tokens);

            var tree = parser.specification();
            var explorer = new TextualNotationSpecificationVisitor();
            return (TextualNotationSpecification)explorer.Visit(tree);
        }

        /// <summary>
        /// Loads the <see cref="GraphicalNotationSpecification" />, with each production's definitions merged
        /// </summary>
        /// <param name="fileUri">The string uri that locates the KGBNF file to load</param>
        /// <returns>The loaded <see cref="GraphicalNotationSpecification" />, holding one <see cref="GraphicalNotationRule" /> per production name</returns>
        /// <exception cref="FileNotFoundException">If <paramref name="fileUri" /> does not locate an existing <see cref="File" /></exception>
        /// <exception cref="InvalidDataException">If the grammar is not valid graphical notation</exception>
        public static GraphicalNotationSpecification LoadGraphicalNotationSpecification(string fileUri)
        {
            if (!File.Exists(fileUri))
            {
                throw new FileNotFoundException("File not found", fileUri);
            }

            var syntaxErrors = new SyntaxErrorCollector();
            var tree = CreateGraphicalParser(GraphicalGrammarErrata.ApplyProductions(File.ReadAllText(fileUri)), syntaxErrors).specification();
            syntaxErrors.ThrowIfAny(fileUri);

            return MergeProductions((GraphicalNotationSpecification)new GraphicalNotationSpecificationVisitor().Visit(tree));
        }

        /// <summary>
        /// Creates a <see cref="kgbnfParser" /> over graphical notation text that reports every lexer and parser error to a collector
        /// </summary>
        /// <param name="source">The graphical notation text</param>
        /// <param name="syntaxErrors">The <see cref="SyntaxErrorCollector" /> receiving the errors</param>
        /// <returns>The <see cref="kgbnfParser" /></returns>
        private static kgbnfParser CreateGraphicalParser(string source, SyntaxErrorCollector syntaxErrors)
        {
            var lexer = new kgbnfLexer(CharStreams.fromString(source));
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(syntaxErrors);

            var parser = new kgbnfParser(new CommonTokenStream(lexer));
            parser.RemoveErrorListeners();
            parser.AddErrorListener(syntaxErrors);

            return parser;
        }

        /// <summary>
        /// Merges every definition of a production name into one <see cref="GraphicalNotationRule" />, in declaration order
        /// </summary>
        /// <param name="specification">The <see cref="GraphicalNotationSpecification" /> holding one rule per definition</param>
        /// <returns>The <see cref="GraphicalNotationSpecification" /> holding one rule per production name</returns>
        /// <remarks>
        /// Partial productions (<c>=|</c>) distribute their alternatives across definitions (SysML 2.0 §8.2.3.1).
        /// </remarks>
        private static GraphicalNotationSpecification MergeProductions(GraphicalNotationSpecification specification)
        {
            var merged = new GraphicalNotationSpecification();

            merged.Rules.AddRange(specification.Rules.GroupBy(rule => rule.RuleName).Select(definitions =>
            {
                var production = new GraphicalNotationRule
                {
                    RuleName = definitions.Key,
                    IsPartial = definitions.Any(definition => definition.IsPartial)
                };

                production.Alternatives.AddRange(definitions.SelectMany(definition => definition.Alternatives));
                return production;
            }));

            return merged;
        }

        /// <summary>
        /// Collects the lexer and parser errors raised while reading graphical notation
        /// </summary>
        private sealed class SyntaxErrorCollector : IAntlrErrorListener<int>, IAntlrErrorListener<IToken>
        {
            /// <summary>
            /// Gets the collected error messages, each prefixed with its line and column
            /// </summary>
            private List<string> Messages { get; } = [];

            /// <summary>
            /// Records a lexer error
            /// </summary>
            /// <param name="output">The output the error would be written to</param>
            /// <param name="recognizer">The lexer</param>
            /// <param name="offendingSymbol">The offending character</param>
            /// <param name="line">The line of the error</param>
            /// <param name="charPositionInLine">The column of the error</param>
            /// <param name="msg">The error message</param>
            /// <param name="e">The recognition exception, if any</param>
            public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
                => this.Messages.Add($"{line}:{charPositionInLine} {msg}");

            /// <summary>
            /// Records a parser error
            /// </summary>
            /// <param name="output">The output the error would be written to</param>
            /// <param name="recognizer">The parser</param>
            /// <param name="offendingSymbol">The offending token</param>
            /// <param name="line">The line of the error</param>
            /// <param name="charPositionInLine">The column of the error</param>
            /// <param name="msg">The error message</param>
            /// <param name="e">The recognition exception, if any</param>
            public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
                => this.Messages.Add($"{line}:{charPositionInLine} {msg}");

            /// <summary>
            /// Rejects text that could not be read in full
            /// </summary>
            /// <param name="source">The name of the read source, reported in the exception</param>
            /// <exception cref="InvalidDataException">If any error was collected</exception>
            public void ThrowIfAny(string source)
            {
                if (this.Messages.Count > 0)
                {
                    throw new InvalidDataException($"'{source}' is not valid graphical notation: {string.Join("; ", this.Messages)}");
                }
            }
        }
    }
}
