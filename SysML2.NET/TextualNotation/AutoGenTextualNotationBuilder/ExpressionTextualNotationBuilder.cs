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
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PrimaryExpression
        /// <para>PrimaryExpression:Expression=FeatureChainExpression|NonFeatureChainPrimaryExpression</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPrimaryExpression(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NonFeatureChainPrimaryExpression
        /// <para>NonFeatureChainPrimaryExpression:Expression=BracketExpression|IndexExpression|SequenceExpression|SelectExpression|CollectExpression|FunctionOperationExpression|BaseExpression</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNonFeatureChainPrimaryExpression(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
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
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFunctionReference(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BaseExpression
        /// <para>BaseExpression:Expression=NullExpression|LiteralExpression|FeatureReferenceExpression|MetadataAccessExpression|InvocationExpression|ConstructorExpression|BodyExpression</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBaseExpression(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
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
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            stringBuilder.Append(' ');
            if (poco.OwnedRelationship.Count != 0)
            {
                throw new System.NotSupportedException("Assigment of enumerable not supported yet");
                stringBuilder.Append(' ');
            }


            stringBuilder.Append("expr ");
            FeatureTextualNotationBuilder.BuildFeatureDeclaration(poco, stringBuilder);
            FeatureTextualNotationBuilder.BuildValuePart(poco, stringBuilder);
            TypeTextualNotationBuilder.BuildFunctionBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
