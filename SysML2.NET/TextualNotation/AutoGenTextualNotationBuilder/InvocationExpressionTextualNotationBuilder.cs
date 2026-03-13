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
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFunctionOperationExpression(SysML2.NET.Core.POCO.Kernel.Expressions.IInvocationExpression poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfParameterMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Kernel.Behaviors.ParameterMembership>().GetEnumerator();
            using var ownedRelationshipOfInvocationExpressionIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Kernel.Expressions.InvocationExpression>().GetEnumerator();
            using var ownedRelationshipOfReturnParameterMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Kernel.Functions.ReturnParameterMembership>().GetEnumerator();
            ownedRelationshipOfParameterMembershipIterator.MoveNext();

            if (ownedRelationshipOfParameterMembershipIterator.Current != null)
            {
                ParameterMembershipTextualNotationBuilder.BuildPrimaryArgumentMember(ownedRelationshipOfParameterMembershipIterator.Current, stringBuilder);
            }
            stringBuilder.Append("-> ");
            ownedRelationshipOfInvocationExpressionIterator.MoveNext();

            if (ownedRelationshipOfInvocationExpressionIterator.Current != null)
            {
                BuildInvocationTypeMember(ownedRelationshipOfInvocationExpressionIterator.Current, stringBuilder);
            }
            if (ownedRelationshipOfParameterMembershipIterator.MoveNext())
            {

                if (ownedRelationshipOfParameterMembershipIterator.Current != null)
                {
                    ParameterMembershipTextualNotationBuilder.BuildBodyArgumentMember(ownedRelationshipOfParameterMembershipIterator.Current, stringBuilder);
                }
            }
            else if (ownedRelationshipOfParameterMembershipIterator.MoveNext())
            {

                if (ownedRelationshipOfParameterMembershipIterator.Current != null)
                {
                    ParameterMembershipTextualNotationBuilder.BuildFunctionReferenceArgumentMember(ownedRelationshipOfParameterMembershipIterator.Current, stringBuilder);
                }
            }
            else
            {
                FeatureTextualNotationBuilder.BuildArgumentList(poco, stringBuilder);
            }
            stringBuilder.Append(' ');
            ownedRelationshipOfReturnParameterMembershipIterator.MoveNext();

            if (ownedRelationshipOfReturnParameterMembershipIterator.Current != null)
            {
                ReturnParameterMembershipTextualNotationBuilder.BuildEmptyResultMember(ownedRelationshipOfReturnParameterMembershipIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InvocationExpression
        /// <para>InvocationExpression:InvocationExpression=ownedRelationship+=InstantiatedTypeMemberArgumentListownedRelationship+=EmptyResultMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.IInvocationExpression" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInvocationExpression(SysML2.NET.Core.POCO.Kernel.Expressions.IInvocationExpression poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Namespaces.Membership>().GetEnumerator();
            using var ownedRelationshipOfReturnParameterMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Kernel.Functions.ReturnParameterMembership>().GetEnumerator();
            ownedRelationshipOfMembershipIterator.MoveNext();

            if (ownedRelationshipOfMembershipIterator.Current != null)
            {
                MembershipTextualNotationBuilder.BuildInstantiatedTypeMember(ownedRelationshipOfMembershipIterator.Current, stringBuilder);
            }
            FeatureTextualNotationBuilder.BuildArgumentList(poco, stringBuilder);
            ownedRelationshipOfReturnParameterMembershipIterator.MoveNext();

            if (ownedRelationshipOfReturnParameterMembershipIterator.Current != null)
            {
                ReturnParameterMembershipTextualNotationBuilder.BuildEmptyResultMember(ownedRelationshipOfReturnParameterMembershipIterator.Current, stringBuilder);
            }

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
