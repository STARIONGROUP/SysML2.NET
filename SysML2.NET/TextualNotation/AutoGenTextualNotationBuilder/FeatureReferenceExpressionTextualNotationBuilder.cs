// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureReferenceExpressionTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="FeatureReferenceExpressionTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureReferenceExpression" /> element
    /// </summary>
    public static partial class FeatureReferenceExpressionTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule SatisfactionReferenceExpression
        /// <para>SatisfactionReferenceExpression:FeatureReferenceExpression=ownedRelationship+=FeatureChainMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureReferenceExpression" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSatisfactionReferenceExpression(SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureReferenceExpression poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Namespaces.Membership>().GetEnumerator();
            ownedRelationshipOfMembershipIterator.MoveNext();
            MembershipTextualNotationBuilder.BuildFeatureChainMember(ownedRelationshipOfMembershipIterator.Current, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedExpressionReference
        /// <para>OwnedExpressionReference:FeatureReferenceExpression=ownedRelationship+=OwnedExpressionMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureReferenceExpression" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedExpressionReference(SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureReferenceExpression poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Types.FeatureMembership>().GetEnumerator();
            ownedRelationshipOfFeatureMembershipIterator.MoveNext();
            FeatureMembershipTextualNotationBuilder.BuildOwnedExpressionMember(ownedRelationshipOfFeatureMembershipIterator.Current, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FunctionReferenceExpression
        /// <para>FunctionReferenceExpression:FeatureReferenceExpression=ownedRelationship+=FunctionReferenceMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureReferenceExpression" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFunctionReferenceExpression(SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureReferenceExpression poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Types.FeatureMembership>().GetEnumerator();
            ownedRelationshipOfFeatureMembershipIterator.MoveNext();
            FeatureMembershipTextualNotationBuilder.BuildFunctionReferenceMember(ownedRelationshipOfFeatureMembershipIterator.Current, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureReferenceExpression
        /// <para>FeatureReferenceExpression:FeatureReferenceExpression=ownedRelationship+=FeatureReferenceMemberownedRelationship+=EmptyResultMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureReferenceExpression" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureReferenceExpression(SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureReferenceExpression poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Namespaces.Membership>().GetEnumerator();
            using var ownedRelationshipOfReturnParameterMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Kernel.Functions.ReturnParameterMembership>().GetEnumerator();
            ownedRelationshipOfMembershipIterator.MoveNext();
            MembershipTextualNotationBuilder.BuildFeatureReferenceMember(ownedRelationshipOfMembershipIterator.Current, stringBuilder);
            ownedRelationshipOfReturnParameterMembershipIterator.MoveNext();
            ReturnParameterMembershipTextualNotationBuilder.BuildEmptyResultMember(ownedRelationshipOfReturnParameterMembershipIterator.Current, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BodyExpression
        /// <para>BodyExpression:FeatureReferenceExpression=ownedRelationship+=ExpressionBodyMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureReferenceExpression" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBodyExpression(SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureReferenceExpression poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Types.FeatureMembership>().GetEnumerator();
            ownedRelationshipOfFeatureMembershipIterator.MoveNext();
            FeatureMembershipTextualNotationBuilder.BuildExpressionBodyMember(ownedRelationshipOfFeatureMembershipIterator.Current, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
