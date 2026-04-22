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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.TextualNotation
{
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="OperatorExpressionTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression" /> element
    /// </summary>
    public static partial class OperatorExpressionTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule ConditionalExpression
        /// <para>ConditionalExpression:OperatorExpression=operator='if'ownedRelationship+=ArgumentMember'?'ownedRelationship+=ArgumentExpressionMember'else'ownedRelationship+=ArgumentExpressionMemberownedRelationship+=EmptyResultMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildConditionalExpression(SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append(poco.Operator);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                {
                    ParameterMembershipTextualNotationBuilder.BuildArgumentMember(elementAsParameterMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            stringBuilder.Append("?");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                {
                    ParameterMembershipTextualNotationBuilder.BuildArgumentExpressionMember(elementAsParameterMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            stringBuilder.Append("else ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                {
                    ParameterMembershipTextualNotationBuilder.BuildArgumentExpressionMember(elementAsParameterMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Functions.IReturnParameterMembership elementAsReturnParameterMembership)
                {
                    ReturnParameterMembershipTextualNotationBuilder.BuildEmptyResultMember(elementAsReturnParameterMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ConditionalBinaryOperatorExpression
        /// <para>ConditionalBinaryOperatorExpression:OperatorExpression=ownedRelationship+=ArgumentMemberoperator=ConditionalBinaryOperatorownedRelationship+=ArgumentExpressionMemberownedRelationship+=EmptyResultMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildConditionalBinaryOperatorExpression(SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                {
                    ParameterMembershipTextualNotationBuilder.BuildArgumentMember(elementAsParameterMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            stringBuilder.Append(poco.Operator);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                {
                    ParameterMembershipTextualNotationBuilder.BuildArgumentExpressionMember(elementAsParameterMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Functions.IReturnParameterMembership elementAsReturnParameterMembership)
                {
                    ReturnParameterMembershipTextualNotationBuilder.BuildEmptyResultMember(elementAsReturnParameterMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BinaryOperatorExpression
        /// <para>BinaryOperatorExpression:OperatorExpression=ownedRelationship+=ArgumentMemberoperator=BinaryOperatorownedRelationship+=ArgumentMemberownedRelationship+=EmptyResultMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBinaryOperatorExpression(SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                {
                    ParameterMembershipTextualNotationBuilder.BuildArgumentMember(elementAsParameterMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            stringBuilder.Append(poco.Operator);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                {
                    ParameterMembershipTextualNotationBuilder.BuildArgumentMember(elementAsParameterMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Functions.IReturnParameterMembership elementAsReturnParameterMembership)
                {
                    ReturnParameterMembershipTextualNotationBuilder.BuildEmptyResultMember(elementAsReturnParameterMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule UnaryOperatorExpression
        /// <para>UnaryOperatorExpression:OperatorExpression=operator=UnaryOperatorownedRelationship+=ArgumentMemberownedRelationship+=EmptyResultMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUnaryOperatorExpression(SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append(poco.Operator);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                {
                    ParameterMembershipTextualNotationBuilder.BuildArgumentMember(elementAsParameterMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Functions.IReturnParameterMembership elementAsReturnParameterMembership)
                {
                    ReturnParameterMembershipTextualNotationBuilder.BuildEmptyResultMember(elementAsReturnParameterMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ClassificationExpression
        /// <para>ClassificationExpression:OperatorExpression=(ownedRelationship+=ArgumentMember)?(operator=ClassificationTestOperatorownedRelationship+=TypeReferenceMember|operator=CastOperatorownedRelationship+=TypeResultMember)ownedRelationship+=EmptyResultMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildClassificationExpression(SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                    {
                        ParameterMembershipTextualNotationBuilder.BuildArgumentMember(elementAsParameterMembership, cursorCache, stringBuilder);
                    }
                }
                stringBuilder.Append(' ');
            }

            BuildClassificationExpressionHandCoded(poco, cursorCache, stringBuilder);
            stringBuilder.Append(' ');

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Functions.IReturnParameterMembership elementAsReturnParameterMembership)
                {
                    ReturnParameterMembershipTextualNotationBuilder.BuildEmptyResultMember(elementAsReturnParameterMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetaclassificationExpression
        /// <para>MetaclassificationExpression:OperatorExpression=ownedRelationship+=MetadataArgumentMember(operator=ClassificationTestOperatorownedRelationship+=TypeReferenceMember|operator=MetaCastOperatorownedRelationship+=TypeResultMember)ownedRelationship+=EmptyResultMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetaclassificationExpression(SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                {
                    ParameterMembershipTextualNotationBuilder.BuildMetadataArgumentMember(elementAsParameterMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            BuildMetaclassificationExpressionHandCoded(poco, cursorCache, stringBuilder);
            stringBuilder.Append(' ');

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Functions.IReturnParameterMembership elementAsReturnParameterMembership)
                {
                    ReturnParameterMembershipTextualNotationBuilder.BuildEmptyResultMember(elementAsReturnParameterMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ExtentExpression
        /// <para>ExtentExpression:OperatorExpression=operator='all'ownedRelationship+=TypeReferenceMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildExtentExpression(SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append(poco.Operator);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                {
                    ParameterMembershipTextualNotationBuilder.BuildTypeReferenceMember(elementAsParameterMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BracketExpression
        /// <para>BracketExpression:OperatorExpression=ownedRelationship+=PrimaryArgumentMemberoperator='['ownedRelationship+=SequenceExpressionListMember']'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBracketExpression(SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                {
                    ParameterMembershipTextualNotationBuilder.BuildPrimaryArgumentMember(elementAsParameterMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            stringBuilder.Append(poco.Operator);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IFeatureMembership elementAsFeatureMembership)
                {
                    FeatureMembershipTextualNotationBuilder.BuildSequenceExpressionListMember(elementAsFeatureMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            stringBuilder.Append("]");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SequenceOperatorExpression
        /// <para>SequenceOperatorExpression:OperatorExpression=ownedRelationship+=OwnedExpressionMemberoperator=','ownedRelationship+=SequenceExpressionListMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSequenceOperatorExpression(SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IFeatureMembership elementAsFeatureMembership)
                {
                    FeatureMembershipTextualNotationBuilder.BuildOwnedExpressionMember(elementAsFeatureMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            stringBuilder.Append(poco.Operator);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IFeatureMembership elementAsFeatureMembership)
                {
                    FeatureMembershipTextualNotationBuilder.BuildSequenceExpressionListMember(elementAsFeatureMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
