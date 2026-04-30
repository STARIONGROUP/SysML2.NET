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
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedExpression(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression pocoOperatorExpressionConditionalExpression when pocoOperatorExpressionConditionalExpression.IsValidForConditionalExpression():
                    OperatorExpressionTextualNotationBuilder.BuildConditionalExpression(pocoOperatorExpressionConditionalExpression, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression pocoOperatorExpressionConditionalBinaryOperatorExpression when pocoOperatorExpressionConditionalBinaryOperatorExpression.IsValidForConditionalBinaryOperatorExpression():
                    OperatorExpressionTextualNotationBuilder.BuildConditionalBinaryOperatorExpression(pocoOperatorExpressionConditionalBinaryOperatorExpression, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression pocoOperatorExpressionBinaryOperatorExpression when pocoOperatorExpressionBinaryOperatorExpression.IsValidForBinaryOperatorExpression():
                    OperatorExpressionTextualNotationBuilder.BuildBinaryOperatorExpression(pocoOperatorExpressionBinaryOperatorExpression, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression pocoOperatorExpressionUnaryOperatorExpression when pocoOperatorExpressionUnaryOperatorExpression.IsValidForUnaryOperatorExpression():
                    OperatorExpressionTextualNotationBuilder.BuildUnaryOperatorExpression(pocoOperatorExpressionUnaryOperatorExpression, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression pocoOperatorExpressionClassificationExpression when pocoOperatorExpressionClassificationExpression.IsValidForClassificationExpression():
                    OperatorExpressionTextualNotationBuilder.BuildClassificationExpression(pocoOperatorExpressionClassificationExpression, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression pocoOperatorExpressionMetaclassificationExpression when pocoOperatorExpressionMetaclassificationExpression.IsValidForMetaclassificationExpression():
                    OperatorExpressionTextualNotationBuilder.BuildMetaclassificationExpression(pocoOperatorExpressionMetaclassificationExpression, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression pocoOperatorExpression:
                    OperatorExpressionTextualNotationBuilder.BuildExtentExpression(pocoOperatorExpression, cursorCache, stringBuilder);
                    break;
                default:
                    BuildPrimaryExpression(poco, cursorCache, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PrimaryExpression
        /// <para>PrimaryExpression:Expression=FeatureChainExpression|NonFeatureChainPrimaryExpression</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPrimaryExpression(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureChainExpression pocoFeatureChainExpression:
                    FeatureChainExpressionTextualNotationBuilder.BuildFeatureChainExpression(pocoFeatureChainExpression, cursorCache, stringBuilder);
                    break;
                default:
                    BuildNonFeatureChainPrimaryExpression(poco, cursorCache, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NonFeatureChainPrimaryExpression
        /// <para>NonFeatureChainPrimaryExpression:Expression=BracketExpression|IndexExpression|SequenceExpression|SelectExpression|CollectExpression|FunctionOperationExpression|BaseExpression</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNonFeatureChainPrimaryExpression(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Kernel.Expressions.IIndexExpression pocoIndexExpression:
                    IndexExpressionTextualNotationBuilder.BuildIndexExpression(pocoIndexExpression, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.ISelectExpression pocoSelectExpression:
                    SelectExpressionTextualNotationBuilder.BuildSelectExpression(pocoSelectExpression, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.ICollectExpression pocoCollectExpression:
                    CollectExpressionTextualNotationBuilder.BuildCollectExpression(pocoCollectExpression, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression pocoOperatorExpression:
                    OperatorExpressionTextualNotationBuilder.BuildBracketExpression(pocoOperatorExpression, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IInvocationExpression pocoInvocationExpression:
                    InvocationExpressionTextualNotationBuilder.BuildFunctionOperationExpression(pocoInvocationExpression, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.IExpression pocoExpressionSequenceExpression when pocoExpressionSequenceExpression.IsValidForSequenceExpression():
                    BuildSequenceExpression(pocoExpressionSequenceExpression, cursorCache, stringBuilder);
                    break;
                default:
                    BuildBaseExpression(poco, cursorCache, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SequenceExpression
        /// <para>SequenceExpression:Expression='('SequenceExpressionList')'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSequenceExpression(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            stringBuilder.Append("(");
            BuildSequenceExpressionList(poco, cursorCache, stringBuilder);
            stringBuilder.Append(")");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SequenceExpressionList
        /// <para>SequenceExpressionList:Expression=OwnedExpression','?|SequenceOperatorExpression</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSequenceExpressionList(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildSequenceExpressionListHandCoded(poco, cursorCache, stringBuilder);
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FunctionReference
        /// <para>FunctionReference:Expression=ownedRelationship+=ReferenceTyping</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFunctionReference(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureTyping elementAsFeatureTyping)
                {
                    FeatureTypingTextualNotationBuilder.BuildReferenceTyping(elementAsFeatureTyping, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BaseExpression
        /// <para>BaseExpression:Expression=NullExpression|LiteralExpression|FeatureReferenceExpression|MetadataAccessExpression|InvocationExpression|ConstructorExpression|BodyExpression</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBaseExpression(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Kernel.Expressions.IInvocationExpression pocoInvocationExpression:
                    InvocationExpressionTextualNotationBuilder.BuildInvocationExpression(pocoInvocationExpression, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IConstructorExpression pocoConstructorExpression:
                    ConstructorExpressionTextualNotationBuilder.BuildConstructorExpression(pocoConstructorExpression, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.INullExpression pocoNullExpression:
                    NullExpressionTextualNotationBuilder.BuildNullExpression(pocoNullExpression, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.ILiteralExpression pocoLiteralExpression:
                    LiteralExpressionTextualNotationBuilder.BuildLiteralExpression(pocoLiteralExpression, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureReferenceExpression pocoFeatureReferenceExpressionFeatureReferenceExpression when pocoFeatureReferenceExpressionFeatureReferenceExpression.IsValidForFeatureReferenceExpression():
                    FeatureReferenceExpressionTextualNotationBuilder.BuildFeatureReferenceExpression(pocoFeatureReferenceExpressionFeatureReferenceExpression, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureReferenceExpression pocoFeatureReferenceExpression:
                    FeatureReferenceExpressionTextualNotationBuilder.BuildBodyExpression(pocoFeatureReferenceExpression, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IMetadataAccessExpression pocoMetadataAccessExpression:
                    MetadataAccessExpressionTextualNotationBuilder.BuildMetadataAccessExpression(pocoMetadataAccessExpression, cursorCache, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ExpressionBody
        /// <para>ExpressionBody:Expression='{'FunctionBodyPart'}'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildExpressionBody(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            stringBuilder.Append(' ');
            stringBuilder.AppendLine("{");
            TypeTextualNotationBuilder.BuildFunctionBodyPart(poco, cursorCache, stringBuilder);
            stringBuilder.AppendLine("}");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Expression
        /// <para>Expression=FeaturePrefix'expr'FeatureDeclarationValuePart?FunctionBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildExpression(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            SharedTextualNotationBuilder.BuildFeaturePrefix(poco, cursorCache, stringBuilder);
            stringBuilder.Append("expr ");
            FeatureTextualNotationBuilder.BuildFeatureDeclaration(poco, cursorCache, stringBuilder);

            if (poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.Direction.HasValue || poco.IsDerived || poco.IsAbstract || poco.IsConstant || poco.IsOrdered || poco.IsEnd || poco.importedMembership.Count != 0 || poco.IsComposite || poco.IsPortion || poco.IsVariable || poco.IsSufficient || poco.unioningType.Count != 0 || poco.intersectingType.Count != 0 || poco.differencingType.Count != 0 || poco.featuringType.Count != 0 || poco.ownedTypeFeaturing.Count != 0)
            {
                FeatureTextualNotationBuilder.BuildValuePart(poco, cursorCache, stringBuilder);
            }
            TypeTextualNotationBuilder.BuildFunctionBody(poco, cursorCache, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
