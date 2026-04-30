// -------------------------------------------------------------------------------------------------
// <copyright file="OperatorExpressionTextualNotationBuilder.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.TextualNotation
{
    using System.Text;

    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Expressions;

    /// <summary>
    /// Hand-coded part of the <see cref="OperatorExpressionTextualNotationBuilder" />
    /// </summary>
    public static partial class OperatorExpressionTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the <c>(…)</c> alternation inside the
        /// <c>ClassificationExpression</c> rule.
        /// <para><c>( operator = ClassificationTestOperator ownedRelationship += TypeReferenceMember
        /// | operator = CastOperator ownedRelationship += TypeResultMember )</c></para>
        /// <para>The AutoGen caller handles the leading optional <c>ArgumentMember</c> and trailing
        /// <c>EmptyResultMember</c>. This HandCoded method emits the operator literal (one of
        /// <c>'istype'</c>, <c>'hastype'</c>, <c>'@'</c>, <c>'as'</c>) and dispatches the cursor
        /// element to <see cref="ParameterMembershipTextualNotationBuilder.BuildTypeReferenceMember"/>.
        /// Both <c>TypeReferenceMember</c> (targeting <c>ParameterMembership</c>) and
        /// <c>TypeResultMember</c> (targeting <c>ReturnParameterMembership</c>, which IS-A
        /// <c>ParameterMembership</c>) have the same body (<c>ownedMemberFeature = TypeReference</c>),
        /// so a single call suffices for both operator branches.</para>
        /// </summary>
        /// <param name="poco">The <see cref="IOperatorExpression" /> being serialised</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildClassificationExpressionHandCoded(IOperatorExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            stringBuilder.Append(poco.Operator);
            stringBuilder.Append(' ');

            if (ownedRelationshipCursor.Current is IParameterMembership parameterMembership)
            {
                ParameterMembershipTextualNotationBuilder.BuildTypeReferenceMember(parameterMembership, cursorCache, stringBuilder);
            }

            ownedRelationshipCursor.Move();
        }

        /// <summary>
        /// Builds the Textual Notation string for the <c>(…)</c> alternation inside the
        /// <c>MetaclassificationExpression</c> rule.
        /// <para><c>( operator = ClassificationTestOperator ownedRelationship += TypeReferenceMember
        /// | operator = MetaCastOperator ownedRelationship += TypeResultMember )</c></para>
        /// <para>Identical structure to <see cref="BuildClassificationExpressionHandCoded"/> — the
        /// operator literal is one of <c>'istype'</c>, <c>'hastype'</c>, <c>'@'</c>, <c>'meta'</c>.
        /// Both membership alternatives share the same body and runtime type hierarchy, so a single
        /// <see cref="ParameterMembershipTextualNotationBuilder.BuildTypeReferenceMember"/> call
        /// handles both branches.</para>
        /// </summary>
        /// <param name="poco">The <see cref="IOperatorExpression" /> being serialised</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildMetaclassificationExpressionHandCoded(IOperatorExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            stringBuilder.Append(poco.Operator);
            stringBuilder.Append(' ');

            if (ownedRelationshipCursor.Current is IParameterMembership parameterMembership)
            {
                ParameterMembershipTextualNotationBuilder.BuildTypeReferenceMember(parameterMembership, cursorCache, stringBuilder);
            }

            ownedRelationshipCursor.Move();
        }
    }
}
