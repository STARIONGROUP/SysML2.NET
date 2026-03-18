// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureValueTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="FeatureValueTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue" /> element
    /// </summary>
    public static partial class FeatureValueTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule TriggerFeatureValue
        /// <para>TriggerFeatureValue:FeatureValue=ownedRelatedElement+=TriggerExpression</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildTriggerFeatureValue(SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue poco, int elementIndex, StringBuilder stringBuilder)
        {
            if (elementIndex < poco.OwnedRelatedElement.Count)
            {
                var elementForOwnedRelatedElement = poco.OwnedRelatedElement[elementIndex];

                if (elementForOwnedRelatedElement is SysML2.NET.Core.POCO.Systems.Actions.ITriggerInvocationExpression elementAsTriggerInvocationExpression)
                {
                    TriggerInvocationExpressionTextualNotationBuilder.BuildTriggerExpression(elementAsTriggerInvocationExpression, 0, stringBuilder);
                }
            }

            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ArgumentValue
        /// <para>ArgumentValue:FeatureValue=value=OwnedExpression</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildArgumentValue(SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue poco, StringBuilder stringBuilder)
        {

            if (poco.value != null)
            {
                ExpressionTextualNotationBuilder.BuildOwnedExpression(poco.value, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ArgumentExpressionValue
        /// <para>ArgumentExpressionValue:FeatureValue=ownedRelatedElement+=OwnedExpressionReference</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildArgumentExpressionValue(SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue poco, int elementIndex, StringBuilder stringBuilder)
        {
            if (elementIndex < poco.OwnedRelatedElement.Count)
            {
                var elementForOwnedRelatedElement = poco.OwnedRelatedElement[elementIndex];

                if (elementForOwnedRelatedElement is SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureReferenceExpression elementAsFeatureReferenceExpression)
                {
                    FeatureReferenceExpressionTextualNotationBuilder.BuildOwnedExpressionReference(elementAsFeatureReferenceExpression, 0, stringBuilder);
                }
            }

            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureBinding
        /// <para>FeatureBinding:FeatureValue=ownedRelatedElement+=OwnedExpression</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildFeatureBinding(SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue poco, int elementIndex, StringBuilder stringBuilder)
        {
            if (elementIndex < poco.OwnedRelatedElement.Count)
            {
                var elementForOwnedRelatedElement = poco.OwnedRelatedElement[elementIndex];

                if (elementForOwnedRelatedElement is SysML2.NET.Core.POCO.Kernel.Functions.IExpression elementAsExpression)
                {
                    ExpressionTextualNotationBuilder.BuildOwnedExpression(elementAsExpression, stringBuilder);
                }
            }

            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule AssignmentTargetBinding
        /// <para>AssignmentTargetBinding:FeatureValue=ownedRelatedElement+=NonFeatureChainPrimaryExpression</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildAssignmentTargetBinding(SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue poco, int elementIndex, StringBuilder stringBuilder)
        {
            if (elementIndex < poco.OwnedRelatedElement.Count)
            {
                var elementForOwnedRelatedElement = poco.OwnedRelatedElement[elementIndex];

                if (elementForOwnedRelatedElement is SysML2.NET.Core.POCO.Kernel.Functions.IExpression elementAsExpression)
                {
                    ExpressionTextualNotationBuilder.BuildNonFeatureChainPrimaryExpression(elementAsExpression, stringBuilder);
                }
            }

            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SatisfactionFeatureValue
        /// <para>SatisfactionFeatureValue:FeatureValue=ownedRelatedElement+=SatisfactionReferenceExpression</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildSatisfactionFeatureValue(SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue poco, int elementIndex, StringBuilder stringBuilder)
        {
            if (elementIndex < poco.OwnedRelatedElement.Count)
            {
                var elementForOwnedRelatedElement = poco.OwnedRelatedElement[elementIndex];

                if (elementForOwnedRelatedElement is SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureReferenceExpression elementAsFeatureReferenceExpression)
                {
                    FeatureReferenceExpressionTextualNotationBuilder.BuildSatisfactionReferenceExpression(elementAsFeatureReferenceExpression, 0, stringBuilder);
                }
            }

            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataValue
        /// <para>MetadataValue:FeatureValue=value=MetadataReference</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataValue(SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue poco, StringBuilder stringBuilder)
        {

            if (poco.value != null)
            {
                var elementForOwnedRelationship = poco.value.OwnedRelationship[0];

                if (elementForOwnedRelationship is SysML2.NET.Core.POCO.Root.Namespaces.IMembership elementAsMembership)
                {
                    MembershipTextualNotationBuilder.BuildElementReferenceMember(elementAsMembership, stringBuilder);
                }

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PrimaryArgumentValue
        /// <para>PrimaryArgumentValue:FeatureValue=value=PrimaryExpression</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPrimaryArgumentValue(SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue poco, StringBuilder stringBuilder)
        {

            if (poco.value != null)
            {
                ExpressionTextualNotationBuilder.BuildPrimaryExpression(poco.value, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NonFeatureChainPrimaryArgumentValue
        /// <para>NonFeatureChainPrimaryArgumentValue:FeatureValue=value=NonFeatureChainPrimaryExpression</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNonFeatureChainPrimaryArgumentValue(SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue poco, StringBuilder stringBuilder)
        {

            if (poco.value != null)
            {
                ExpressionTextualNotationBuilder.BuildNonFeatureChainPrimaryExpression(poco.value, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BodyArgumentValue
        /// <para>BodyArgumentValue:FeatureValue=value=BodyExpression</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBodyArgumentValue(SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue poco, StringBuilder stringBuilder)
        {

            if (poco.value != null)
            {
                var elementForOwnedRelationship = poco.value.OwnedRelationship[0];

                if (elementForOwnedRelationship is SysML2.NET.Core.POCO.Core.Types.IFeatureMembership elementAsFeatureMembership)
                {
                    FeatureMembershipTextualNotationBuilder.BuildExpressionBodyMember(elementAsFeatureMembership, stringBuilder);
                }

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FunctionReferenceArgumentValue
        /// <para>FunctionReferenceArgumentValue:FeatureValue=value=FunctionReferenceExpression</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFunctionReferenceArgumentValue(SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue poco, StringBuilder stringBuilder)
        {

            if (poco.value != null)
            {
                var elementForOwnedRelationship = poco.value.OwnedRelationship[0];

                if (elementForOwnedRelationship is SysML2.NET.Core.POCO.Core.Types.IFeatureMembership elementAsFeatureMembership)
                {
                    FeatureMembershipTextualNotationBuilder.BuildFunctionReferenceMember(elementAsFeatureMembership, stringBuilder);
                }

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureValue
        /// <para>FeatureValue=('='|isInitial?=':='|isDefault?='default'('='|isInitial?=':=')?)ownedRelatedElement+=OwnedExpression</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureValue(SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue poco, StringBuilder stringBuilder)
        {
            using var ownedRelatedElementOfExpressionIterator = poco.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Kernel.Functions.Expression>().GetEnumerator();
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            stringBuilder.Append(' ');
            ownedRelatedElementOfExpressionIterator.MoveNext();

            if (ownedRelatedElementOfExpressionIterator.Current != null)
            {
                ExpressionTextualNotationBuilder.BuildOwnedExpression(ownedRelatedElementOfExpressionIterator.Current, stringBuilder);
            }

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
