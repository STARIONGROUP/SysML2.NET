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

namespace SysML2.NET.Serializer.TextualNotation.Writers
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
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedExpression(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var operatorParensNeeded = writerContext.EmitOperatorParentheses && writerContext.OperatorContextStack.Count > 0 && SysML2.NET.Serializer.TextualNotation.Writers.OperatorPrecedence.NeedsParenthesesAsOperand(writerContext.OperatorContextStack.Peek(), poco);
            if (operatorParensNeeded) { stringBuilder.Append('('); }
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression pocoOperatorExpressionConditionalExpression when pocoOperatorExpressionConditionalExpression.IsValidForConditionalExpression(writerContext):
                    OperatorExpressionTextualNotationBuilder.BuildConditionalExpression(pocoOperatorExpressionConditionalExpression, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression pocoOperatorExpressionConditionalBinaryOperatorExpression when pocoOperatorExpressionConditionalBinaryOperatorExpression.IsValidForConditionalBinaryOperatorExpression(writerContext):
                    OperatorExpressionTextualNotationBuilder.BuildConditionalBinaryOperatorExpression(pocoOperatorExpressionConditionalBinaryOperatorExpression, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression pocoOperatorExpressionBinaryOperatorExpression when pocoOperatorExpressionBinaryOperatorExpression.IsValidForBinaryOperatorExpression(writerContext):
                    OperatorExpressionTextualNotationBuilder.BuildBinaryOperatorExpression(pocoOperatorExpressionBinaryOperatorExpression, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression pocoOperatorExpressionUnaryOperatorExpression when pocoOperatorExpressionUnaryOperatorExpression.IsValidForUnaryOperatorExpression(writerContext):
                    OperatorExpressionTextualNotationBuilder.BuildUnaryOperatorExpression(pocoOperatorExpressionUnaryOperatorExpression, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression pocoOperatorExpressionClassificationExpression when pocoOperatorExpressionClassificationExpression.IsValidForClassificationExpression(writerContext):
                    OperatorExpressionTextualNotationBuilder.BuildClassificationExpression(pocoOperatorExpressionClassificationExpression, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression pocoOperatorExpressionMetaclassificationExpression when pocoOperatorExpressionMetaclassificationExpression.IsValidForMetaclassificationExpression(writerContext):
                    OperatorExpressionTextualNotationBuilder.BuildMetaclassificationExpression(pocoOperatorExpressionMetaclassificationExpression, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression pocoOperatorExpressionExtentExpression when (pocoOperatorExpressionExtentExpression.Operator == "all" && writerContext.CursorCache.GetOrCreateCursor(pocoOperatorExpressionExtentExpression.Id, "ownedRelationship", pocoOperatorExpressionExtentExpression.OwnedRelationship).Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership):
                    OperatorExpressionTextualNotationBuilder.BuildExtentExpression(pocoOperatorExpressionExtentExpression, writerContext, stringBuilder);
                    break;
                default:
                    BuildPrimaryExpression(poco, writerContext, stringBuilder);
                    break;
            }
            if (operatorParensNeeded) { stringBuilder.Append(')'); }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PrimaryExpression
        /// <para>PrimaryExpression:Expression=FeatureChainExpression|NonFeatureChainPrimaryExpression</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPrimaryExpression(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureChainExpression pocoFeatureChainExpressionFeatureChainExpression when writerContext.CursorCache.GetOrCreateCursor(pocoFeatureChainExpressionFeatureChainExpression.Id, "ownedRelationship", pocoFeatureChainExpressionFeatureChainExpression.OwnedRelationship).Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership:
                    FeatureChainExpressionTextualNotationBuilder.BuildFeatureChainExpression(pocoFeatureChainExpressionFeatureChainExpression, writerContext, stringBuilder);
                    break;
                default:
                    BuildNonFeatureChainPrimaryExpression(poco, writerContext, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NonFeatureChainPrimaryExpression
        /// <para>NonFeatureChainPrimaryExpression:Expression=BracketExpression|IndexExpression|SequenceExpression|SelectExpression|CollectExpression|FunctionOperationExpression|BaseExpression</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNonFeatureChainPrimaryExpression(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Kernel.Expressions.IIndexExpression pocoIndexExpressionIndexExpression when writerContext.CursorCache.GetOrCreateCursor(pocoIndexExpressionIndexExpression.Id, "ownedRelationship", pocoIndexExpressionIndexExpression.OwnedRelationship).Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership:
                    IndexExpressionTextualNotationBuilder.BuildIndexExpression(pocoIndexExpressionIndexExpression, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.ISelectExpression pocoSelectExpressionSelectExpression when writerContext.CursorCache.GetOrCreateCursor(pocoSelectExpressionSelectExpression.Id, "ownedRelationship", pocoSelectExpressionSelectExpression.OwnedRelationship).Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership:
                    SelectExpressionTextualNotationBuilder.BuildSelectExpression(pocoSelectExpressionSelectExpression, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.ICollectExpression pocoCollectExpressionCollectExpression when writerContext.CursorCache.GetOrCreateCursor(pocoCollectExpressionCollectExpression.Id, "ownedRelationship", pocoCollectExpressionCollectExpression.OwnedRelationship).Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership:
                    CollectExpressionTextualNotationBuilder.BuildCollectExpression(pocoCollectExpressionCollectExpression, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression pocoOperatorExpressionBracketExpression when (writerContext.CursorCache.GetOrCreateCursor(pocoOperatorExpressionBracketExpression.Id, "ownedRelationship", pocoOperatorExpressionBracketExpression.OwnedRelationship).Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership && pocoOperatorExpressionBracketExpression.Operator == "["):
                    OperatorExpressionTextualNotationBuilder.BuildBracketExpression(pocoOperatorExpressionBracketExpression, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IInvocationExpression pocoInvocationExpressionFunctionOperationExpression when writerContext.CursorCache.GetOrCreateCursor(pocoInvocationExpressionFunctionOperationExpression.Id, "ownedRelationship", pocoInvocationExpressionFunctionOperationExpression.OwnedRelationship).Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership:
                    InvocationExpressionTextualNotationBuilder.BuildFunctionOperationExpression(pocoInvocationExpressionFunctionOperationExpression, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.IExpression pocoExpressionSequenceExpression when pocoExpressionSequenceExpression.IsValidForSequenceExpression(writerContext):
                    BuildSequenceExpression(pocoExpressionSequenceExpression, writerContext, stringBuilder);
                    break;
                default:
                    BuildBaseExpression(poco, writerContext, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SequenceExpression
        /// <para>SequenceExpression:Expression='('SequenceExpressionList')'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSequenceExpression(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            stringBuilder.Append("(");
            BuildSequenceExpressionList(poco, writerContext, stringBuilder);
            stringBuilder.Append(") ");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SequenceExpressionList
        /// <para>SequenceExpressionList:Expression=OwnedExpression','?|SequenceOperatorExpression</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSequenceExpressionList(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            BuildSequenceExpressionListHandCoded(poco, writerContext, stringBuilder);
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FunctionReference
        /// <para>FunctionReference:Expression=ownedRelationship+=ReferenceTyping</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFunctionReference(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureTyping elementAsFeatureTyping)
                {
                    FeatureTypingTextualNotationBuilder.BuildReferenceTyping(elementAsFeatureTyping, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                }
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BaseExpression
        /// <para>BaseExpression:Expression=NullExpression|LiteralExpression|FeatureReferenceExpression|MetadataAccessExpression|InvocationExpression|ConstructorExpression|BodyExpression</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBaseExpression(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Kernel.Expressions.IInvocationExpression pocoInvocationExpression:
                    InvocationExpressionTextualNotationBuilder.BuildInvocationExpression(pocoInvocationExpression, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IConstructorExpression pocoConstructorExpression:
                    ConstructorExpressionTextualNotationBuilder.BuildConstructorExpression(pocoConstructorExpression, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.INullExpression pocoNullExpression:
                    NullExpressionTextualNotationBuilder.BuildNullExpression(pocoNullExpression, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.ILiteralExpression pocoLiteralExpression:
                    LiteralExpressionTextualNotationBuilder.BuildLiteralExpression(pocoLiteralExpression, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureReferenceExpression pocoFeatureReferenceExpressionFeatureReferenceExpression when pocoFeatureReferenceExpressionFeatureReferenceExpression.IsValidForFeatureReferenceExpression(writerContext):
                    FeatureReferenceExpressionTextualNotationBuilder.BuildFeatureReferenceExpression(pocoFeatureReferenceExpressionFeatureReferenceExpression, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureReferenceExpression pocoFeatureReferenceExpression:
                    FeatureReferenceExpressionTextualNotationBuilder.BuildBodyExpression(pocoFeatureReferenceExpression, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IMetadataAccessExpression pocoMetadataAccessExpression:
                    MetadataAccessExpressionTextualNotationBuilder.BuildMetadataAccessExpression(pocoMetadataAccessExpression, writerContext, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ExpressionBody
        /// <para>ExpressionBody:Expression='{'FunctionBodyPart'}'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildExpressionBody(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            stringBuilder.Append(' ');
            stringBuilder.AppendLine("{");
            TypeTextualNotationBuilder.BuildFunctionBodyPart(poco, writerContext, stringBuilder);
            stringBuilder.AppendLine("}");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Expression
        /// <para>Expression=FeaturePrefix'expr'FeatureDeclarationValuePart?FunctionBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildExpression(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            SharedTextualNotationBuilder.BuildFeaturePrefix(poco, writerContext, stringBuilder);
            stringBuilder.Append("expr ");
            FeatureTextualNotationBuilder.BuildFeatureDeclaration(poco, writerContext, stringBuilder);
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue || !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.Direction.HasValue || poco.IsDerived || poco.IsAbstract || poco.IsConstant || poco.IsOrdered || poco.IsEnd || poco.IsComposite || poco.IsPortion || poco.IsVariable || poco.IsSufficient)
            {
                FeatureTextualNotationBuilder.BuildValuePart(poco, writerContext, stringBuilder);
            }
            TypeTextualNotationBuilder.BuildFunctionBody(poco, writerContext, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
