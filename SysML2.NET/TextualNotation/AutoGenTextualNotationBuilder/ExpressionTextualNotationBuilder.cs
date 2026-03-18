// -------------------------------------------------------------------------------------------------
// <copyright file="ExpressionTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="ExpressionTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> element
    /// </summary>
    public static partial class ExpressionTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedExpression
        /// <para>OwnedExpression:Expression=ConditionalExpression|ConditionalBinaryOperatorExpression|BinaryOperatorExpression|UnaryOperatorExpression|ClassificationExpression|MetaclassificationExpression|ExtentExpression|PrimaryExpression</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedExpression(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives with same referenced rule type not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PrimaryExpression
        /// <para>PrimaryExpression:Expression=FeatureChainExpression|NonFeatureChainPrimaryExpression</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPrimaryExpression(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Kernel.Expressions.FeatureChainExpression pocoFeatureChainExpression:
                    FeatureChainExpressionTextualNotationBuilder.BuildFeatureChainExpression(pocoFeatureChainExpression, stringBuilder);
                    break;
                default:
                    BuildNonFeatureChainPrimaryExpression(poco, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NonFeatureChainPrimaryExpression
        /// <para>NonFeatureChainPrimaryExpression:Expression=BracketExpression|IndexExpression|SequenceExpression|SelectExpression|CollectExpression|FunctionOperationExpression|BaseExpression</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNonFeatureChainPrimaryExpression(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives with same referenced rule type not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SequenceExpression
        /// <para>SequenceExpression:Expression='('SequenceExpressionList')'</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSequenceExpression(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append("(");
            BuildSequenceExpressionList(poco, stringBuilder);
            stringBuilder.Append(")");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SequenceExpressionList
        /// <para>SequenceExpressionList:Expression=OwnedExpression','?|SequenceOperatorExpression</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSequenceExpressionList(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FunctionReference
        /// <para>FunctionReference:Expression=ownedRelationship+=ReferenceTyping</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildFunctionReference(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, int elementIndex, StringBuilder stringBuilder)
        {
            if (elementIndex < poco.OwnedRelationship.Count)
            {
                var elementForOwnedRelationship = poco.OwnedRelationship[elementIndex];

                if (elementForOwnedRelationship is SysML2.NET.Core.POCO.Core.Features.IFeatureTyping elementAsFeatureTyping)
                {
                    FeatureTypingTextualNotationBuilder.BuildReferenceTyping(elementAsFeatureTyping, stringBuilder);
                }
            }

            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BaseExpression
        /// <para>BaseExpression:Expression=NullExpression|LiteralExpression|FeatureReferenceExpression|MetadataAccessExpression|InvocationExpression|ConstructorExpression|BodyExpression</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBaseExpression(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives with same referenced rule type not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ExpressionBody
        /// <para>ExpressionBody:Expression='{'FunctionBodyPart'}'</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildExpressionBody(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append("{");
            TypeTextualNotationBuilder.BuildFunctionBodyPart(poco, stringBuilder);
            stringBuilder.Append("}");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Expression
        /// <para>Expression=FeaturePrefix'expr'FeatureDeclarationValuePart?FunctionBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildExpression(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfOwningMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership>().GetEnumerator();
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            stringBuilder.Append(' ');

            while (ownedRelationshipOfOwningMembershipIterator.MoveNext())
            {

                if (ownedRelationshipOfOwningMembershipIterator.Current != null)
                {
                    OwningMembershipTextualNotationBuilder.BuildPrefixMetadataMember(ownedRelationshipOfOwningMembershipIterator.Current, stringBuilder);
                }

            }

            stringBuilder.Append("expr ");
            FeatureTextualNotationBuilder.BuildFeatureDeclaration(poco, stringBuilder);
            FeatureTextualNotationBuilder.BuildValuePart(poco, 0, stringBuilder);
            TypeTextualNotationBuilder.BuildFunctionBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
