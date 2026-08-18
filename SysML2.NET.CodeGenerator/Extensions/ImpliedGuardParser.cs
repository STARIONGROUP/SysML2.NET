// -------------------------------------------------------------------------------------------------
// <copyright file="ImpliedGuardParser.cs" company="Starion Group S.A.">
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
    using System.Text.RegularExpressions;

    /// <summary>
    /// Parses the guard expression of a semantic constraint into one of the mechanically translatable
    /// shapes.
    /// </summary>
    /// <remarks>
    /// The parser is deliberately strict: an expression it does not recognise EXACTLY is reported as
    /// <see cref="ImpliedGuardShape.RequiresHandCoding" /> rather than approximated. A guard that silently
    /// mistranslates would inject Specializations a model does not require, corrupting every inheritance
    /// result computed from it.
    /// </remarks>
    public static partial class ImpliedGuardParser
    {
        /// <summary>
        /// Upper bound on a single match, guarding against catastrophic backtracking.
        /// </summary>
        private const int MatchTimeoutMilliseconds = 1000;

        /// <summary>
        /// The capture group holding the boolean argument of an operation call.
        /// </summary>
        private const string LiteralGroup = "literal";

        /// <summary>
        /// Matches a bare boolean property, e.g. <c>isIndividual</c>.
        /// </summary>
        private static readonly Regex BooleanPropertyPattern =
            new(@"^(?<member>is[A-Za-z]+)$", RegexOptions.Compiled, System.TimeSpan.FromMilliseconds(MatchTimeoutMilliseconds));

        /// <summary>
        /// Matches a boolean operation call, optionally negated and optionally with a boolean argument.
        /// </summary>
        private static readonly Regex OperationCallPattern =
            new(@"^(?<not>not\s+)?(?<member>is[A-Za-z]+)\((?<literal>true|false)?\)$", RegexOptions.Compiled, System.TimeSpan.FromMilliseconds(MatchTimeoutMilliseconds));

        /// <summary>
        /// Matches an owning-Type kind test over two alternatives, optionally conjoined with isComposite.
        /// </summary>
        private static readonly Regex OwningTypeKindPattern =
            new(@"^(?<composite>isComposite\s+and\s+)?owningType\s*<>\s*null\s+and\s*\(\s*owningType\.oclIsKindOf\((?<first>[A-Za-z]+)\)\s+or\s+owningType\.oclIsKindOf\((?<second>[A-Za-z]+)\)\s*\)$", RegexOptions.Compiled, System.TimeSpan.FromMilliseconds(MatchTimeoutMilliseconds));

        /// <summary>
        /// Matches an owned-end-Feature cardinality test.
        /// </summary>
        private static readonly Regex OwnedEndFeatureCountPattern =
            new(@"^ownedEndFeatures?->(?:size\(\)\s*=\s*(?<literal>[0-9]+)|(?<notEmpty>notEmpty\(\)))$", RegexOptions.Compiled, System.TimeSpan.FromMilliseconds(MatchTimeoutMilliseconds));

        /// <summary>
        /// Matches an owned-typing kind test.
        /// </summary>
        private static readonly Regex OwnedTypingKindPattern =
            new(@"^ownedTyping\.type->exists\(selectByKind\((?<first>[A-Za-z]+)\)\)$", RegexOptions.Compiled, System.TimeSpan.FromMilliseconds(MatchTimeoutMilliseconds));

        /// <summary>
        /// Matches an owning-FeatureMembership kind test.
        /// </summary>
        private static readonly Regex OwningFeatureMembershipKindPattern =
            new(@"^owningFeatureMembership\s*<>\s*null\s+and\s+owningFeatureMembership\.oclIsKindOf\((?<first>[A-Za-z]+)\)$", RegexOptions.Compiled, System.TimeSpan.FromMilliseconds(MatchTimeoutMilliseconds));

        /// <summary>
        /// Matches an enumeration-literal comparison.
        /// </summary>
        private static readonly Regex EnumerationComparisonPattern =
            new(@"^(?<member>[a-z][A-Za-z]*)\s*=\s*(?<enumeration>[A-Za-z]+)::(?<literal>[a-zA-Z]+)$", RegexOptions.Compiled, System.TimeSpan.FromMilliseconds(MatchTimeoutMilliseconds));

        /// <summary>
        /// Parses a guard expression into the operands a C# predicate needs.
        /// </summary>
        /// <param name="guardOcl">The guard OCL, i.e. the antecedent of the <c>implies</c>.</param>
        /// <returns>The parsed expression; its shape is RequiresHandCoding when unrecognised.</returns>
        public static ImpliedGuardExpression Parse(string guardOcl)
        {
            var normalised = Normalise(guardOcl);

            if (string.IsNullOrWhiteSpace(normalised))
            {
                return new ImpliedGuardExpression { Shape = ImpliedGuardShape.RequiresHandCoding, Ocl = guardOcl };
            }

            var owningTypeKind = OwningTypeKindPattern.Match(normalised);

            if (owningTypeKind.Success)
            {
                return new ImpliedGuardExpression
                {
                    Shape = ImpliedGuardShape.OwningTypeKind,
                    Ocl = normalised,
                    TypeNames = [owningTypeKind.Groups["first"].Value, owningTypeKind.Groups["second"].Value],
                    RequiresComposite = owningTypeKind.Groups["composite"].Success
                };
            }

            var operationCall = OperationCallPattern.Match(normalised);

            if (operationCall.Success)
            {
                return new ImpliedGuardExpression
                {
                    Shape = ImpliedGuardShape.OperationCall,
                    Ocl = normalised,
                    MemberName = operationCall.Groups["member"].Value,
                    IsNegated = operationCall.Groups["not"].Success,
                    Literal = operationCall.Groups[LiteralGroup].Success ? operationCall.Groups[LiteralGroup].Value : null
                };
            }

            var ownedEndFeatureCount = OwnedEndFeatureCountPattern.Match(normalised);

            if (ownedEndFeatureCount.Success)
            {
                return new ImpliedGuardExpression
                {
                    Shape = ImpliedGuardShape.OwnedEndFeatureCount,
                    Ocl = normalised,
                    Literal = ownedEndFeatureCount.Groups["notEmpty"].Success ? null : ownedEndFeatureCount.Groups[LiteralGroup].Value
                };
            }

            var ownedTypingKind = OwnedTypingKindPattern.Match(normalised);

            if (ownedTypingKind.Success)
            {
                return new ImpliedGuardExpression
                {
                    Shape = ImpliedGuardShape.OwnedTypingKind,
                    Ocl = normalised,
                    TypeNames = [ownedTypingKind.Groups["first"].Value]
                };
            }

            var owningFeatureMembershipKind = OwningFeatureMembershipKindPattern.Match(normalised);

            if (owningFeatureMembershipKind.Success)
            {
                return new ImpliedGuardExpression
                {
                    Shape = ImpliedGuardShape.OwningFeatureMembershipKind,
                    Ocl = normalised,
                    TypeNames = [owningFeatureMembershipKind.Groups["first"].Value]
                };
            }

            var enumerationComparison = EnumerationComparisonPattern.Match(normalised);

            if (enumerationComparison.Success)
            {
                return new ImpliedGuardExpression
                {
                    Shape = ImpliedGuardShape.EnumerationComparison,
                    Ocl = normalised,
                    MemberName = enumerationComparison.Groups["member"].Value,
                    TypeNames = [enumerationComparison.Groups["enumeration"].Value],
                    Literal = enumerationComparison.Groups[LiteralGroup].Value
                };
            }

            var booleanProperty = BooleanPropertyPattern.Match(normalised);

            return booleanProperty.Success
                ? new ImpliedGuardExpression
                {
                    Shape = ImpliedGuardShape.BooleanProperty,
                    Ocl = normalised,
                    MemberName = booleanProperty.Groups["member"].Value
                }
                : new ImpliedGuardExpression { Shape = ImpliedGuardShape.RequiresHandCoding, Ocl = normalised };
        }

        /// <summary>
        /// Collapses the whitespace an XMI body carries across lines into single spaces.
        /// </summary>
        /// <param name="guardOcl">The raw guard OCL.</param>
        /// <returns>The single-line form, or <c>null</c> when the input is null.</returns>
        private static string Normalise(string guardOcl) => guardOcl == null ? null : WhitespaceRunPattern().Replace(guardOcl, " ").Trim();

        /// <summary>
        /// Matches a run of whitespace, including the line breaks an XMI body carries.
        /// </summary>
        /// <returns>The source-generated pattern.</returns>
        [GeneratedRegex(@"\s+", RegexOptions.None, MatchTimeoutMilliseconds)]
        private static partial Regex WhitespaceRunPattern();
    }
}
