// -------------------------------------------------------------------------------------------------
// <copyright file="InvocationExpressionTextualNotationBuilder.cs" company="Starion Group S.A.">
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.TextualNotation
{
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="InvocationExpressionTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.IInvocationExpression" /> element
    /// </summary>
    public static partial class InvocationExpressionTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule FunctionOperationExpression
        /// <para>FunctionOperationExpression:InvocationExpression=ownedRelationship+=PrimaryArgumentMember'-&gt;'ownedRelationship+=InvocationTypeMember(ownedRelationship+=BodyArgumentMember|ownedRelationship+=FunctionReferenceArgumentMember|ArgumentList)ownedRelationship+=EmptyResultMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.IInvocationExpression" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFunctionOperationExpression(SysML2.NET.Core.POCO.Kernel.Expressions.IInvocationExpression poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                {
                    ParameterMembershipTextualNotationBuilder.BuildPrimaryArgumentMember(elementAsParameterMembership, writerContext, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            stringBuilder.Append("-> ");

            if (ownedRelationshipCursor.Current != null)
            {
                BuildInvocationTypeMember(poco, writerContext, stringBuilder);
            }
            ownedRelationshipCursor.Move();

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                    {
                        ParameterMembershipTextualNotationBuilder.BuildBodyArgumentMember(elementAsParameterMembership, writerContext, stringBuilder);
                    }
                }
            }
            else if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                    {
                        ParameterMembershipTextualNotationBuilder.BuildFunctionReferenceArgumentMember(elementAsParameterMembership, writerContext, stringBuilder);
                    }
                }
            }
            else
            {
                FeatureTextualNotationBuilder.BuildArgumentList(poco, writerContext, stringBuilder);
            }
            stringBuilder.Append(' ');

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Functions.IReturnParameterMembership elementAsReturnParameterMembership)
                {
                    ReturnParameterMembershipTextualNotationBuilder.BuildEmptyResultMember(elementAsReturnParameterMembership, writerContext, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InvocationExpression
        /// <para>InvocationExpression:InvocationExpression=ownedRelationship+=InstantiatedTypeMemberArgumentListownedRelationship+=EmptyResultMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.IInvocationExpression" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInvocationExpression(SysML2.NET.Core.POCO.Kernel.Expressions.IInvocationExpression poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IMembership elementAsMembership)
                {
                    MembershipTextualNotationBuilder.BuildInstantiatedTypeMember(elementAsMembership, writerContext, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            FeatureTextualNotationBuilder.BuildArgumentList(poco, writerContext, stringBuilder);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Functions.IReturnParameterMembership elementAsReturnParameterMembership)
                {
                    ReturnParameterMembershipTextualNotationBuilder.BuildEmptyResultMember(elementAsReturnParameterMembership, writerContext, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
