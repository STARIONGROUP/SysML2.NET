// -------------------------------------------------------------------------------------------------
// <copyright file="ImpliedGuardEmitter.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Translates a parsed guard expression into the C# predicate the generated guard evaluates.
    /// </summary>
    /// <remarks>
    /// The emitted predicate is a single boolean expression over a parameter named <c>element</c>. A shape
    /// the emitter cannot render — because a referenced metaclass is unknown, for instance — yields
    /// <c>null</c>, which keeps the constraint in the hand-coded set rather than emitting something that
    /// does not compile or, worse, compiles and is wrong.
    /// </remarks>
    public static class ImpliedGuardEmitter
    {
        /// <summary>
        /// Emits the C# predicate for a parsed guard expression.
        /// </summary>
        /// <param name="expression">The parsed guard expression.</param>
        /// <param name="declaringInterfaceFqn">The fully qualified interface of the declaring metaclass.</param>
        /// <param name="interfaceFqnByName">The fully qualified interface of every known metaclass, by name.</param>
        /// <param name="enumerationFqnByName">The fully qualified name of every known enumeration, by name.</param>
        /// <returns>The predicate over <c>element</c>, or <c>null</c> when it cannot be rendered.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="expression" /> is null.</exception>
        public static string Emit(ImpliedGuardExpression expression, string declaringInterfaceFqn, IReadOnlyDictionary<string, string> interfaceFqnByName, IReadOnlyDictionary<string, string> enumerationFqnByName)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            if (string.IsNullOrWhiteSpace(declaringInterfaceFqn))
            {
                return null;
            }

            var subject = $"element is {declaringInterfaceFqn} guardSubject";

            return expression.Shape switch
            {
                ImpliedGuardShape.BooleanProperty => $"element is {declaringInterfaceFqn} {{ {PascalCase(expression.MemberName)}: true }}",
                ImpliedGuardShape.OperationCall => EmitOperationCall(expression, subject),
                ImpliedGuardShape.OwningTypeKind => EmitOwningTypeKind(expression, declaringInterfaceFqn, interfaceFqnByName),
                ImpliedGuardShape.OwnedEndFeatureCount => EmitOwnedEndFeatureCount(expression, subject),
                ImpliedGuardShape.OwnedTypingKind => EmitOwnedTypingKind(expression, subject, interfaceFqnByName),
                ImpliedGuardShape.OwningFeatureMembershipKind => EmitOwningFeatureMembershipKind(expression, declaringInterfaceFqn, interfaceFqnByName),
                ImpliedGuardShape.EnumerationComparison => EmitEnumerationComparison(expression, declaringInterfaceFqn, enumerationFqnByName),
                _ => null
            };
        }

        /// <summary>
        /// Emits a boolean operation call, honouring negation and an optional boolean argument.
        /// </summary>
        /// <param name="expression">The parsed guard expression.</param>
        /// <param name="subject">The type-pattern prefix binding <c>guardSubject</c>.</param>
        /// <returns>The predicate.</returns>
        private static string EmitOperationCall(ImpliedGuardExpression expression, string subject)
        {
            var argument = expression.Literal ?? string.Empty;
            var call = $"guardSubject.{PascalCase(expression.MemberName)}({argument})";

            return $"{subject} && {(expression.IsNegated ? "!" : string.Empty)}{call}";
        }

        /// <summary>
        /// Emits an owning-Type kind test, optionally conjoined with the composite flag.
        /// </summary>
        /// <param name="expression">The parsed guard expression.</param>
        /// <param name="declaringInterfaceFqn">The fully qualified interface of the declaring metaclass.</param>
        /// <param name="interfaceFqnByName">The fully qualified interface of every known metaclass, by name.</param>
        /// <returns>The predicate, or <c>null</c> when a metaclass is unknown.</returns>
        /// <remarks>
        /// Emitted as one merged property pattern rather than a chain of conjuncts, so the whole condition
        /// reads as a single shape test.
        /// </remarks>
        private static string EmitOwningTypeKind(ImpliedGuardExpression expression, string declaringInterfaceFqn, IReadOnlyDictionary<string, string> interfaceFqnByName)
        {
            if (!TryQueryInterfaces(expression.TypeNames, interfaceFqnByName, out var alternatives))
            {
                return null;
            }

            var composite = expression.RequiresComposite ? "IsComposite: true, " : string.Empty;

            return $"element is {declaringInterfaceFqn} {{ {composite}owningType: {string.Join(" or ", alternatives)} }}";
        }

        /// <summary>
        /// Emits an owned-end-Feature cardinality test.
        /// </summary>
        /// <param name="expression">The parsed guard expression.</param>
        /// <param name="subject">The type-pattern prefix binding <c>guardSubject</c>.</param>
        /// <returns>The predicate.</returns>
        /// <remarks>
        /// The abstract syntax spells the property both <c>ownedEndFeature</c> and <c>ownedEndFeatures</c>;
        /// only the singular exists, so the emitted code always uses it.
        /// </remarks>
        private static string EmitOwnedEndFeatureCount(ImpliedGuardExpression expression, string subject)
        {
            var comparison = expression.Literal == null
                ? "Count > 0"
                : $"Count == {expression.Literal}";

            return $"{subject} && ((SysML2.NET.Core.POCO.Core.Types.IType)guardSubject).ownedEndFeature.{comparison}";
        }

        /// <summary>
        /// Emits an owned-typing kind test.
        /// </summary>
        /// <param name="expression">The parsed guard expression.</param>
        /// <param name="subject">The type-pattern prefix binding <c>guardSubject</c>.</param>
        /// <param name="interfaceFqnByName">The fully qualified interface of every known metaclass, by name.</param>
        /// <returns>The predicate, or <c>null</c> when the metaclass is unknown.</returns>
        private static string EmitOwnedTypingKind(ImpliedGuardExpression expression, string subject, IReadOnlyDictionary<string, string> interfaceFqnByName)
        {
            return TryQueryInterfaces(expression.TypeNames, interfaceFqnByName, out var alternatives)
                ? $"{subject} && guardSubject.ownedTyping.Any(featureTyping => featureTyping.Type is {alternatives[0]})"
                : null;
        }

        /// <summary>
        /// Emits an owning-FeatureMembership kind test.
        /// </summary>
        /// <param name="expression">The parsed guard expression.</param>
        /// <param name="declaringInterfaceFqn">The fully qualified interface of the declaring metaclass.</param>
        /// <param name="interfaceFqnByName">The fully qualified interface of every known metaclass, by name.</param>
        /// <returns>The predicate, or <c>null</c> when the metaclass is unknown.</returns>
        private static string EmitOwningFeatureMembershipKind(ImpliedGuardExpression expression, string declaringInterfaceFqn, IReadOnlyDictionary<string, string> interfaceFqnByName)
        {
            return TryQueryInterfaces(expression.TypeNames, interfaceFqnByName, out var alternatives)
                ? $"element is {declaringInterfaceFqn} {{ owningFeatureMembership: {alternatives[0]} }}"
                : null;
        }

        /// <summary>
        /// Emits an enumeration-literal comparison.
        /// </summary>
        /// <param name="expression">The parsed guard expression.</param>
        /// <param name="declaringInterfaceFqn">The fully qualified interface of the declaring metaclass.</param>
        /// <param name="enumerationFqnByName">The fully qualified name of every known enumeration, by name.</param>
        /// <returns>The predicate, or <c>null</c> when the enumeration is unknown.</returns>
        private static string EmitEnumerationComparison(ImpliedGuardExpression expression, string declaringInterfaceFqn, IReadOnlyDictionary<string, string> enumerationFqnByName)
        {
            return enumerationFqnByName.TryGetValue(expression.TypeNames[0], out var enumerationFqn)
                ? $"element is {declaringInterfaceFqn} {{ {PascalCase(expression.MemberName)}: {enumerationFqn}.{PascalCase(expression.Literal)} }}"
                : null;
        }

        /// <summary>
        /// Resolves metaclass names to their fully qualified interfaces.
        /// </summary>
        /// <param name="typeNames">The metaclass names to resolve.</param>
        /// <param name="interfaceFqnByName">The fully qualified interface of every known metaclass, by name.</param>
        /// <param name="interfaces">The resolved interfaces, when every name resolved and at least one was given.</param>
        /// <returns><see langword="true" /> when the guard can be emitted from these names.</returns>
        /// <remarks>
        /// A Try pattern rather than a nullable collection: "a metaclass name is unknown" is an OUTCOME —
        /// the guard then falls back to hand-coding — not an empty result, and the two must not be
        /// conflated by a caller that iterates what it gets back.
        /// </remarks>
        private static bool TryQueryInterfaces(IReadOnlyList<string> typeNames, IReadOnlyDictionary<string, string> interfaceFqnByName, out List<string> interfaces)
        {
            var resolved = new List<string>();

            foreach (var typeName in typeNames)
            {
                if (!interfaceFqnByName.TryGetValue(typeName, out var interfaceFqn))
                {
                    interfaces = null;

                    return false;
                }

                resolved.Add(interfaceFqn);
            }

            interfaces = resolved;

            return resolved.Count != 0;
        }

        /// <summary>
        /// Upper-cases the first character, turning an OCL member name into its C# counterpart.
        /// </summary>
        /// <param name="name">The OCL member name.</param>
        /// <returns>The C# member name.</returns>
        private static string PascalCase(string name) => string.IsNullOrEmpty(name) ? name : char.ToUpperInvariant(name[0]) + name[1..];
    }
}
