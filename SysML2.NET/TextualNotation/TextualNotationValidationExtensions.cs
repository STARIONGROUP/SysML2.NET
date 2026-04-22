// -------------------------------------------------------------------------------------------------
// <copyright file="TextualNotationValidationExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.TextualNotation
{
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Connectors;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Systems.Connections;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Flows;
    using SysML2.NET.Core.POCO.Systems.Interfaces;
    using SysML2.NET.Core.POCO.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Systems.States;

    /// <summary>
    /// Extension methods providing IsValidFor guards used in textual notation switch dispatchers.
    /// These allow disambiguation when multiple grammar rule alternatives map to the same UML class.
    /// </summary>
    public static class TextualNotationValidationExtensions
    {
        /// <summary>
        /// Asserts that the <see cref="IFeature"/> is valid for the Typings rule
        /// </summary>
        /// <param name="feature">The <see cref="IFeature"/></param>
        /// <returns>True if the feature matches the Typings rule</returns>
        public static bool IsValidForTypings(this IFeature feature)
        {
            throw new System.NotSupportedException("IsValidForTypings requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IFeature"/> is valid for the Subsettings rule
        /// </summary>
        /// <param name="feature">The <see cref="IFeature"/></param>
        /// <returns>True if the feature matches the Subsettings rule</returns>
        public static bool IsValidForSubsettings(this IFeature feature)
        {
            throw new System.NotSupportedException("IsValidForSubsettings requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IFeature"/> is valid for the References rule
        /// </summary>
        /// <param name="feature">The <see cref="IFeature"/></param>
        /// <returns>True if the feature matches the References rule</returns>
        public static bool IsValidForReferences(this IFeature feature)
        {
            throw new System.NotSupportedException("IsValidForReferences requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IFeature"/> is valid for the Crosses rule
        /// </summary>
        /// <param name="feature">The <see cref="IFeature"/></param>
        /// <returns>True if the feature matches the Crosses rule</returns>
        public static bool IsValidForCrosses(this IFeature feature)
        {
            throw new System.NotSupportedException("IsValidForCrosses requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IFeature"/> is valid for the ChainingPart rule
        /// </summary>
        /// <param name="feature">The <see cref="IFeature"/></param>
        /// <returns>True if the feature matches the ChainingPart rule</returns>
        public static bool IsValidForChainingPart(this IFeature feature)
        {
            throw new System.NotSupportedException("IsValidForChainingPart requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IFeature"/> is valid for the InvertingPart rule
        /// </summary>
        /// <param name="feature">The <see cref="IFeature"/></param>
        /// <returns>True if the feature matches the InvertingPart rule</returns>
        public static bool IsValidForInvertingPart(this IFeature feature)
        {
            throw new System.NotSupportedException("IsValidForInvertingPart requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IFeature"/> is valid for the PositionalArgumentList rule
        /// </summary>
        /// <param name="feature">The <see cref="IFeature"/></param>
        /// <returns>True if the feature matches the PositionalArgumentList rule</returns>
        public static bool IsValidForPositionalArgumentList(this IFeature feature)
        {
            throw new System.NotSupportedException("IsValidForPositionalArgumentList requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IType"/> is valid for the DisjoiningPart rule
        /// </summary>
        /// <param name="type">The <see cref="IType"/></param>
        /// <returns>True if the type matches the DisjoiningPart rule</returns>
        public static bool IsValidForDisjoiningPart(this IType type)
        {
            throw new System.NotSupportedException("IsValidForDisjoiningPart requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IType"/> is valid for the UnioningPart rule
        /// </summary>
        /// <param name="type">The <see cref="IType"/></param>
        /// <returns>True if the type matches the UnioningPart rule</returns>
        public static bool IsValidForUnioningPart(this IType type)
        {
            throw new System.NotSupportedException("IsValidForUnioningPart requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IType"/> is valid for the IntersectingPart rule
        /// </summary>
        /// <param name="type">The <see cref="IType"/></param>
        /// <returns>True if the type matches the IntersectingPart rule</returns>
        public static bool IsValidForIntersectingPart(this IType type)
        {
            throw new System.NotSupportedException("IsValidForIntersectingPart requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> is valid for the BehaviorUsageMember rule
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <returns>True if the membership matches the BehaviorUsageMember rule</returns>
        public static bool IsValidForBehaviorUsageMember(this IFeatureMembership featureMembership)
        {
            throw new System.NotSupportedException("IsValidForBehaviorUsageMember requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IConnector"/> is valid for the BinaryConnectorDeclaration rule
        /// </summary>
        /// <param name="connector">The <see cref="IConnector"/></param>
        /// <returns>True if the connector matches the BinaryConnectorDeclaration rule</returns>
        public static bool IsValidForBinaryConnectorDeclaration(this IConnector connector)
        {
            throw new System.NotSupportedException("IsValidForBinaryConnectorDeclaration requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IConnectionUsage"/> is valid for the BinaryConnectorPart rule
        /// </summary>
        /// <param name="connectionUsage">The <see cref="IConnectionUsage"/></param>
        /// <returns>True if the connection usage matches the BinaryConnectorPart rule</returns>
        public static bool IsValidForBinaryConnectorPart(this IConnectionUsage connectionUsage)
        {
            throw new System.NotSupportedException("IsValidForBinaryConnectorPart requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IInterfaceUsage"/> is valid for the BinaryInterfacePart rule
        /// </summary>
        /// <param name="interfaceUsage">The <see cref="IInterfaceUsage"/></param>
        /// <returns>True if the interface usage matches the BinaryInterfacePart rule</returns>
        public static bool IsValidForBinaryInterfacePart(this IInterfaceUsage interfaceUsage)
        {
            throw new System.NotSupportedException("IsValidForBinaryInterfacePart requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IUsage"/> is valid for the StructureUsageElement rule
        /// </summary>
        /// <param name="usage">The <see cref="IUsage"/></param>
        /// <returns>True if the usage matches the StructureUsageElement rule</returns>
        public static bool IsValidForStructureUsageElement(this IUsage usage)
        {
            throw new System.NotSupportedException("IsValidForStructureUsageElement requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IOccurrenceUsage"/> is valid for the OccurrenceUsage rule
        /// </summary>
        /// <param name="occurrenceUsage">The <see cref="IOccurrenceUsage"/></param>
        /// <returns>True if the occurrence usage matches the OccurrenceUsage rule</returns>
        public static bool IsValidForOccurrenceUsage(this IOccurrenceUsage occurrenceUsage)
        {
            throw new System.NotSupportedException("IsValidForOccurrenceUsage requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IOccurrenceUsage"/> is valid for the IndividualUsage rule
        /// </summary>
        /// <param name="occurrenceUsage">The <see cref="IOccurrenceUsage"/></param>
        /// <returns>True if the occurrence usage matches the IndividualUsage rule</returns>
        public static bool IsValidForIndividualUsage(this IOccurrenceUsage occurrenceUsage)
        {
            throw new System.NotSupportedException("IsValidForIndividualUsage requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IOccurrenceDefinition"/> is valid for the OccurrenceDefinition rule
        /// </summary>
        /// <param name="occurrenceDefinition">The <see cref="IOccurrenceDefinition"/></param>
        /// <returns>True if the occurrence definition matches the OccurrenceDefinition rule</returns>
        public static bool IsValidForOccurrenceDefinition(this IOccurrenceDefinition occurrenceDefinition)
        {
            throw new System.NotSupportedException("IsValidForOccurrenceDefinition requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IFlowUsage"/> is valid for the FlowUsage rule
        /// </summary>
        /// <param name="flowUsage">The <see cref="IFlowUsage"/></param>
        /// <returns>True if the flow usage matches the FlowUsage rule</returns>
        public static bool IsValidForFlowUsage(this IFlowUsage flowUsage)
        {
            throw new System.NotSupportedException("IsValidForFlowUsage requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IFlowUsage"/> is valid for the Message rule
        /// </summary>
        /// <param name="flowUsage">The <see cref="IFlowUsage"/></param>
        /// <returns>True if the flow usage matches the Message rule</returns>
        public static bool IsValidForMessage(this IFlowUsage flowUsage)
        {
            throw new System.NotSupportedException("IsValidForMessage requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="ITransitionUsage"/> is valid for the GuardedTargetSuccession rule
        /// </summary>
        /// <param name="transitionUsage">The <see cref="ITransitionUsage"/></param>
        /// <returns>True if the transition usage matches the GuardedTargetSuccession rule</returns>
        public static bool IsValidForGuardedTargetSuccession(this ITransitionUsage transitionUsage)
        {
            throw new System.NotSupportedException("IsValidForGuardedTargetSuccession requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IParameterMembership"/> is valid for the ActionBodyParameterMember rule
        /// </summary>
        /// <param name="parameterMembership">The <see cref="IParameterMembership"/></param>
        /// <returns>True if the parameter membership matches the ActionBodyParameterMember rule</returns>
        public static bool IsValidForActionBodyParameterMember(this IParameterMembership parameterMembership)
        {
            throw new System.NotSupportedException("IsValidForActionBodyParameterMember requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IOperatorExpression"/> is valid for the ConditionalExpression rule
        /// </summary>
        /// <param name="operatorExpression">The <see cref="IOperatorExpression"/></param>
        /// <returns>True if the expression matches the ConditionalExpression rule</returns>
        public static bool IsValidForConditionalExpression(this IOperatorExpression operatorExpression)
        {
            throw new System.NotSupportedException("IsValidForConditionalExpression requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IOperatorExpression"/> is valid for the ConditionalBinaryOperatorExpression rule
        /// </summary>
        /// <param name="operatorExpression">The <see cref="IOperatorExpression"/></param>
        /// <returns>True if the expression matches the ConditionalBinaryOperatorExpression rule</returns>
        public static bool IsValidForConditionalBinaryOperatorExpression(this IOperatorExpression operatorExpression)
        {
            throw new System.NotSupportedException("IsValidForConditionalBinaryOperatorExpression requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IOperatorExpression"/> is valid for the BinaryOperatorExpression rule
        /// </summary>
        /// <param name="operatorExpression">The <see cref="IOperatorExpression"/></param>
        /// <returns>True if the expression matches the BinaryOperatorExpression rule</returns>
        public static bool IsValidForBinaryOperatorExpression(this IOperatorExpression operatorExpression)
        {
            throw new System.NotSupportedException("IsValidForBinaryOperatorExpression requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IOperatorExpression"/> is valid for the UnaryOperatorExpression rule
        /// </summary>
        /// <param name="operatorExpression">The <see cref="IOperatorExpression"/></param>
        /// <returns>True if the expression matches the UnaryOperatorExpression rule</returns>
        public static bool IsValidForUnaryOperatorExpression(this IOperatorExpression operatorExpression)
        {
            throw new System.NotSupportedException("IsValidForUnaryOperatorExpression requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IOperatorExpression"/> is valid for the ClassificationExpression rule
        /// </summary>
        /// <param name="operatorExpression">The <see cref="IOperatorExpression"/></param>
        /// <returns>True if the expression matches the ClassificationExpression rule</returns>
        public static bool IsValidForClassificationExpression(this IOperatorExpression operatorExpression)
        {
            throw new System.NotSupportedException("IsValidForClassificationExpression requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IOperatorExpression"/> is valid for the MetaclassificationExpression rule
        /// </summary>
        /// <param name="operatorExpression">The <see cref="IOperatorExpression"/></param>
        /// <returns>True if the expression matches the MetaclassificationExpression rule</returns>
        public static bool IsValidForMetaclassificationExpression(this IOperatorExpression operatorExpression)
        {
            throw new System.NotSupportedException("IsValidForMetaclassificationExpression requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IExpression"/> is valid for the SequenceExpression rule
        /// </summary>
        /// <param name="expression">The <see cref="IExpression"/></param>
        /// <returns>True if the expression matches the SequenceExpression rule</returns>
        public static bool IsValidForSequenceExpression(this IExpression expression)
        {
            throw new System.NotSupportedException("IsValidForSequenceExpression requires manual implementation");
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureReferenceExpression"/> is valid for the FeatureReferenceExpression rule
        /// </summary>
        /// <param name="featureReferenceExpression">The <see cref="IFeatureReferenceExpression"/></param>
        /// <returns>True if the expression matches the FeatureReferenceExpression rule</returns>
        public static bool IsValidForFeatureReferenceExpression(this IFeatureReferenceExpression featureReferenceExpression)
        {
            throw new System.NotSupportedException("IsValidForFeatureReferenceExpression requires manual implementation");
        }
    }
}
