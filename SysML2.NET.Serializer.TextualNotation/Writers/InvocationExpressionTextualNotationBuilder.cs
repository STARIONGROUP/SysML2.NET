// -------------------------------------------------------------------------------------------------
// <copyright file="InvocationExpressionTextualNotationBuilder.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// Hand-coded part of the <see cref="InvocationExpressionTextualNotationBuilder"/>
    /// </summary>
    public static partial class InvocationExpressionTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule FunctionOperationExpression.
        /// <para><c>FunctionOperationExpression : InvocationExpression =
        /// ownedRelationship += PrimaryArgumentMember '-&gt;'
        /// ownedRelationship += InstantiatedTypeMember
        /// ( ownedRelationship += BodyArgumentMember
        /// | ownedRelationship += FunctionReferenceArgumentMember
        /// | ArgumentList )
        /// ownedRelationship += EmptyResultMember</c></para>
        /// <para>Hand-coded because the trailing choice is not discriminable from the parsed rule body:
        /// <c>BodyArgumentMember</c> and <c>FunctionReferenceArgumentMember</c> both target
        /// <c>ParameterMembership</c>, and <c>ArgumentList</c> is a bare non-terminal with no assignment.
        /// The generated form gave all three the guard <c>Current != null</c>, so branches 2 and 3 were dead
        /// — the mandatory <c>()</c> was never emitted and the trailing <c>EmptyResultMember</c> was
        /// captured by branch 1 and then rendered twice.</para>
        /// <para>The three are separated by the <c>FeatureValue</c> the argument carries: a
        /// <c>BodyExpression</c> and a <c>FunctionReferenceExpression</c> are both
        /// <see cref="IFeatureReferenceExpression"/>, and differ in that a <c>FunctionReference</c> owns a
        /// <c>ReferenceTyping</c> (a <see cref="IFeatureTyping"/>) whereas an <c>ExpressionBody</c> does not.
        /// Anything else — including the zero-argument case, where only the <c>EmptyResultMember</c> remains
        /// — is an <c>ArgumentList</c>, whose parentheses are unconditional in the grammar.</para>
        /// </summary>
        /// <param name="poco">The <see cref="IInvocationExpression"/> from which the rule should be built</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext"/> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder"/> that accumulates the entire textual notation with indentation</param>
        private static void BuildFunctionOperationExpressionHandCoded(IInvocationExpression poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current is IParameterMembership primaryArgumentMember)
            {
                ParameterMembershipTextualNotationBuilder.BuildPrimaryArgumentMember(primaryArgumentMember, writerContext, stringBuilder);
                ownedRelationshipCursor.Move();
            }

            stringBuilder.Append("->");

            if (ownedRelationshipCursor.Current is IMembership instantiatedTypeMember)
            {
                MembershipTextualNotationBuilder.BuildInstantiatedTypeMember(instantiatedTypeMember, writerContext, stringBuilder);
                ownedRelationshipCursor.Move();
            }

            // A ReturnParameterMembership IS an IParameterMembership, so the rule's own trailing
            // EmptyResultMember must be excluded here or it is mistaken for the argument.
            var argumentMember = ownedRelationshipCursor.Current is IReturnParameterMembership
                ? null
                : ownedRelationshipCursor.Current as IParameterMembership;

            if (argumentMember is not null && QueryArgumentValue(argumentMember) is IFeatureReferenceExpression argumentValue)
            {
                if (IsFunctionReference(argumentValue))
                {
                    ParameterMembershipTextualNotationBuilder.BuildFunctionReferenceArgumentMember(argumentMember, writerContext, stringBuilder);
                }
                else
                {
                    ParameterMembershipTextualNotationBuilder.BuildBodyArgumentMember(argumentMember, writerContext, stringBuilder);
                }

                ownedRelationshipCursor.Move();
            }
            else
            {
                FeatureTextualNotationBuilder.BuildArgumentList(poco, writerContext, stringBuilder);
            }

            if (ownedRelationshipCursor.Current is IReturnParameterMembership emptyResultMember)
            {
                ReturnParameterMembershipTextualNotationBuilder.BuildEmptyResultMember(emptyResultMember, writerContext, stringBuilder);
                ownedRelationshipCursor.Move();
            }
        }

        /// <summary>
        /// Queries the value expression carried by an argument membership's parameter.
        /// </summary>
        /// <param name="parameterMembership">The <see cref="IParameterMembership"/> holding the argument</param>
        /// <returns>The argument's value expression, or null when the parameter carries no FeatureValue</returns>
        private static IExpression QueryArgumentValue(IParameterMembership parameterMembership)
        {
            return parameterMembership.ownedMemberParameter?
                .OwnedRelationship
                .OfType<IFeatureValue>()
                .FirstOrDefault()?
                .value;
        }

        /// <summary>
        /// Determines whether an argument value is a FunctionReferenceExpression rather than a BodyExpression.
        /// </summary>
        /// <param name="expression">The argument's value expression</param>
        /// <returns>True when the referenced feature owns a ReferenceTyping</returns>
        private static bool IsFunctionReference(IExpression expression)
        {
            return expression.OwnedRelationship
                .OfType<IFeatureMembership>()
                .Select(featureMembership => featureMembership.ownedMemberFeature)
                .OfType<IExpression>()
                .Any(functionReference => functionReference.OwnedRelationship.OfType<IFeatureTyping>().Any());
        }
    }
}
