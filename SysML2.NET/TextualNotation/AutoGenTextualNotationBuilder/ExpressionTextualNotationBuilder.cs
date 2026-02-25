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
            stringBuilder.Append("( ");
            // non Terminal : SequenceExpressionList; Found rule SequenceExpressionList:Expression=OwnedExpression','?|SequenceOperatorExpression 
            BuildSequenceExpressionList(poco, stringBuilder);
            stringBuilder.Append(") ");

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
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement

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
            stringBuilder.Append("{ ");
            // non Terminal : FunctionBodyPart; Found rule FunctionBodyPart:Type=(TypeBodyElement|ownedRelationship+=ReturnFeatureMember)*(ownedRelationship+=ResultExpressionMember)? 
            TypeTextualNotationBuilder.BuildFunctionBodyPart(poco, stringBuilder);
            stringBuilder.Append("} ");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Expression
        /// <para>Expression=FeaturePrefix'expr'FeatureDeclarationValuePart?FunctionBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildExpression(SysML2.NET.Core.POCO.Kernel.Functions.IExpression poco, StringBuilder stringBuilder)
        {
            // non Terminal : FeaturePrefix; Found rule FeaturePrefix=(EndFeaturePrefix(ownedRelationship+=OwnedCrossFeatureMember)?|BasicFeaturePrefix)(ownedRelationship+=PrefixMetadataMember)* 
            // Group Element
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            // Group Element
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement


            stringBuilder.Append("expr ");
            // non Terminal : FeatureDeclaration; Found rule FeatureDeclaration:Feature=(isSufficient?='all')?(FeatureIdentification(FeatureSpecializationPart|ConjugationPart)?|FeatureSpecializationPart|ConjugationPart)FeatureRelationshipPart* 
            FeatureTextualNotationBuilder.BuildFeatureDeclaration(poco, stringBuilder);
            // non Terminal : ValuePart; Found rule ValuePart:Feature=ownedRelationship+=FeatureValue 
            FeatureTextualNotationBuilder.BuildValuePart(poco, stringBuilder);
            // non Terminal : FunctionBody; Found rule FunctionBody:Type=';'|'{'FunctionBodyPart'}' 
            TypeTextualNotationBuilder.BuildFunctionBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
