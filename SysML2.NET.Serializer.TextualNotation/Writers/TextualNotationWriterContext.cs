// -------------------------------------------------------------------------------------------------
// <copyright file="TextualNotationWriterContext.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.TextualNotation.Writers
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// Provides the serialization context for the textual notation builders. Carries the
    /// <see cref="ICursorCache"/> for cursor-based element traversal, the root
    /// <see cref="INamespace"/> being serialized, and the <see cref="NameResolutionCache"/>
    /// that owns all name-resolution state used by
    /// <see cref="SharedTextualNotationBuilder.AppendQualifiedName"/>.
    /// </summary>
    public class TextualNotationWriterContext : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextualNotationWriterContext"/> class.
        /// Eagerly builds the per-namespace simple-name index for every namespace reachable
        /// from <paramref name="contextNamespace"/> via owned-relationship containment and
        /// direct imports.
        /// </summary>
        /// <param name="contextNamespace">
        /// The root <see cref="INamespace"/> being serialized. Used as the upper-bound of the
        /// upward-walk in qualified name resolution per KerML §8.2.3.5 and as the fallback
        /// local scope when a source POCO has no <c>owningNamespace</c>.
        /// </param>
        public TextualNotationWriterContext(INamespace contextNamespace)
        {
            this.CursorCache = new CursorCache();
            this.ContextNamespace = contextNamespace ?? throw new ArgumentNullException(nameof(contextNamespace));
            this.NameResolutionCache = new NameResolutionCache(contextNamespace);
            this.OperatorContextStack = new Stack<IExpression>();
            this.EmitOperatorParentheses = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the writer should emit precedence-aware
        /// parentheses around operator-expression operands. Defaults to <c>true</c>, which
        /// produces the spec-canonical form (KerML §8.2.5.8.1 / SysML §8.4.3.2) where
        /// nested operator expressions are wrapped in <c>(…)</c> to guarantee round-trip
        /// fidelity against the precedence-climbing parser.
        /// <para>
        /// Set to <c>false</c> to suppress the writer-side disambiguation parens entirely.
        /// The resulting output is more compact and matches the idiomatic shorthand used
        /// throughout the SysML tutorials (e.g. <c>a and b or c</c>), but a model whose
        /// operand nesting does not align with the parser's default precedence ordering may
        /// re-parse to a structurally-different AST in that mode.
        /// </para>
        /// </summary>
        public bool EmitOperatorParentheses { get; set; }

        /// <summary>
        /// Gets the stack of currently-active enclosing operator expressions. The textual
        /// notation builder for each <c>IOperatorExpression</c>-typed rule pushes its
        /// <see cref="IExpression"/> poco on entry and pops it on exit (try/finally), so
        /// operand-emission paths can peek the top to obtain the enclosing operator and
        /// consult <see cref="OperatorPrecedence.NeedsParenthesesAsOperand"/> to decide
        /// whether the operand needs to be wrapped in <c>(…)</c>.
        /// </summary>
        public Stack<IExpression> OperatorContextStack { get; }

        /// <summary>
        /// Gets the <see cref="ICursorCache"/> used for cursor-based element traversal.
        /// </summary>
        public ICursorCache CursorCache { get; }

        /// <summary>
        /// Gets the root <see cref="INamespace"/> being serialized, providing the upper bound
        /// for the upward-walk performed in qualified name resolution per KerML §8.2.3.5.
        /// </summary>
        public INamespace ContextNamespace { get; }

        /// <summary>
        /// Gets the <see cref="NameResolutionCache"/> that owns all name-resolution state for
        /// this serialization context: eager per-namespace simple-name indices, lazy
        /// source-POCO scope chains, and lazy memoised resolved-emission strings.
        /// </summary>
        public NameResolutionCache NameResolutionCache { get; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.CursorCache.Dispose();
        }
    }
}
