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
    using SysML2.NET.Extensions;
    using SysML2.NET.Semantics.Implied;

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
        /// Shares inheritance resolution across the whole write, so the Types being written resolve the
        /// library supertype chain they have in common once rather than once each.
        /// </summary>
        private readonly InheritanceScope inheritanceScope;

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
        /// <param name="globalNamespaces">
        /// The other root <see cref="INamespace"/>s available to <paramref name="contextNamespace"/> —
        /// typically <c>XmiReadResult.ReferencedNamespaces</c>, i.e. the model libraries and any other
        /// resource read alongside it. Per KerML §8.2.3.5.2 these form the global <see cref="INamespace"/>
        /// that terminates qualified-name resolution, so supplying them lets the writer emit a name routed
        /// through a library the model does not itself import.
        /// <para>Optional: when omitted, resolution is confined to <paramref name="contextNamespace"/>'s own
        /// containment and import graph, which can only yield a longer — never an invalid — name.</para>
        /// </param>
        /// <param name="impliedRelationshipProvider">
        /// The provider supplying the implied <c>Relationships</c> (KerML §8.4.2) that a model exported
        /// without them omits. Optional: when omitted, a name reachable ONLY through an implied
        /// <c>Specialization</c> degrades to a longer — never an invalid — form.
        /// </param>
        public TextualNotationWriterContext(INamespace contextNamespace, IEnumerable<INamespace> globalNamespaces = null, IImpliedRelationshipProvider impliedRelationshipProvider = null)
        {
            this.ContextNamespace = contextNamespace ?? throw new ArgumentNullException(nameof(contextNamespace));

            // Opened before the name-resolution index is built, so the index and the write pass that
            // follows it share one inheritance memo; closed by Dispose.
            this.inheritanceScope = InheritanceScope.Begin();

            // Building the index walks the whole reachable model and can therefore raise on a malformed
            // one. A constructor that throws leaves the caller's `using` with nothing to dispose, so the
            // scope has to be closed here or it would stay open on this thread for good.
            try
            {
                this.CursorCache = new CursorCache();
                this.ImpliedRelationshipProvider = impliedRelationshipProvider ?? NullImpliedRelationshipProvider.Instance;
                this.NameResolutionCache = new NameResolutionCache(contextNamespace, globalNamespaces, this.ImpliedRelationshipProvider);
                this.OperatorContextStack = new Stack<IExpression>();
                this.EmitOperatorParentheses = true;
            }
            catch
            {
                this.CursorCache?.Dispose();
                this.inheritanceScope.Dispose();

                throw;
            }
        }

        /// <summary>
        /// Gets the provider supplying the implied <c>Relationships</c> (KerML §8.4.2) omitted by a model
        /// exported without them; never <c>null</c>.
        /// </summary>
        public IImpliedRelationshipProvider ImpliedRelationshipProvider { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the writer should emit precedence-aware
        /// parentheses around operator-expression operands. Defaults to <c>true</c>, which
        /// produces the spec-canonical form (KerML §8.2.5.8.1 / SysML §8.4.3.2) where
        /// nested operator expressions are wrapped in <c>(…)</c> to guarantee round-trip
        /// fidelity against the precedence-climbing parser.
        /// <para>
        /// In addition to the parens that fidelity requires, this mode emits clarifying parens
        /// where precedence already settles the grouping but the mix reads ambiguously —
        /// two different logical connectives (<c>(a and b) xor (c and d)</c>) and arithmetic
        /// across precedence tiers (<c>a + (b * c)</c>). See <see cref="OperatorPrecedence"/>.
        /// </para>
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
        /// Suspends the operator context for the lifetime of the returned scope, so the next operand
        /// emission behaves as though its expression were not nested in an operator and adds no
        /// parentheses of its own.
        /// </summary>
        /// <returns>A scope that restores the operator context when disposed.</returns>
        /// <remarks>
        /// For a rule that has ALREADY emitted delimiters around a single content expression —
        /// <c>SequenceExpression</c> (<c>'(' … ')'</c>), <c>BracketExpression</c> (<c>'[' … ']'</c>) and
        /// <c>IndexExpression</c> (<c>'#' '(' … ')'</c>) — the operand-parenthesisation layer would
        /// double the delimiters it already wrote, giving <c>((as Safety))</c> or
        /// <c>25[(mi / gallon)]</c>.
        /// <para>Suspension is scoped rather than global because it must apply to the IMMEDIATE operand
        /// only. Every operator builder pushes its own poco onto the stack on entry, so an operand
        /// nested deeper inside the suspended expression sees a non-empty context again and
        /// parenthesises normally.</para>
        /// <para>It deliberately does NOT extend to <c>ArgumentList</c> (<c>'(' ( PositionalArgumentList |
        /// NamedArgumentList )? ')'</c>), whose parentheses delimit a comma-separated LIST rather than a
        /// single operand — an argument that is itself a sequence needs its own parentheses there, which
        /// is why the pilot writes <c>sum((a, b, c))</c>. <c>ArgumentList</c> does not route through
        /// <c>SequenceExpressionList</c>, so it never opens this scope.</para>
        /// </remarks>
        public IDisposable SuspendOperatorContext()
        {
            return new SuspendedOperatorContext(this.OperatorContextStack);
        }

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
            this.inheritanceScope.Dispose();
        }

        /// <summary>
        /// The scope opened by <see cref="SuspendOperatorContext"/>: drains the operator context on
        /// construction and restores it, in its original order, on disposal.
        /// </summary>
        private sealed class SuspendedOperatorContext : IDisposable
        {
            /// <summary>
            /// The suspended stack.
            /// </summary>
            private readonly Stack<IExpression> operatorContextStack;

            /// <summary>
            /// The drained entries, top of stack first.
            /// </summary>
            private readonly IExpression[] suspendedEntries;

            /// <summary>
            /// Initializes a new instance of the <see cref="SuspendedOperatorContext"/> class.
            /// </summary>
            /// <param name="operatorContextStack">The stack to suspend.</param>
            internal SuspendedOperatorContext(Stack<IExpression> operatorContextStack)
            {
                this.operatorContextStack = operatorContextStack;
                this.suspendedEntries = operatorContextStack.ToArray();
                operatorContextStack.Clear();
            }

            /// <summary>
            /// Restores the suspended operator context.
            /// </summary>
            public void Dispose()
            {
                // ToArray yields top-first, so pushing in reverse restores the original ordering.
                for (var entryIndex = this.suspendedEntries.Length - 1; entryIndex >= 0; entryIndex--)
                {
                    this.operatorContextStack.Push(this.suspendedEntries[entryIndex]);
                }
            }
        }
    }
}
