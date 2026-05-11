// -------------------------------------------------------------------------------------------------
// <copyright file="DefinitionExtensions.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

namespace SysML2.NET.Core.POCO.Systems.DefinitionAndUsage
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.Allocations;
    using SysML2.NET.Core.POCO.Systems.AnalysisCases;
    using SysML2.NET.Core.POCO.Systems.Attributes;
    using SysML2.NET.Core.POCO.Systems.Calculations;
    using SysML2.NET.Core.POCO.Systems.Cases;
    using SysML2.NET.Core.POCO.Systems.Connections;
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.Enumerations;
    using SysML2.NET.Core.POCO.Systems.Flows;
    using SysML2.NET.Core.POCO.Systems.Interfaces;
    using SysML2.NET.Core.POCO.Systems.Items;
    using SysML2.NET.Core.POCO.Systems.Metadata;
    using SysML2.NET.Core.POCO.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Core.POCO.Systems.Views;

    /// <summary>
    /// The <see cref="DefinitionExtensions"/> class provides extensions methods for
    /// the <see cref="IDefinition"/> interface
    /// </summary>
    internal static class DefinitionExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// directedUsage = directedFeature-&gt;selectByKind(Usage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IUsage> ComputeDirectedUsage(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedAction = ownedUsage-&gt;selectByKind(ActionUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IActionUsage> ComputeOwnedAction(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedAllocation = ownedUsage-&gt;selectByKind(AllocationUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IAllocationUsage> ComputeOwnedAllocation(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedAnalysisCase = ownedUsage-&gt;selectByKind(AnalysisCaseUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IAnalysisCaseUsage> ComputeOwnedAnalysisCase(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedAttribute = ownedUsage-&gt;selectByKind(AttributeUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IAttributeUsage> ComputeOwnedAttribute(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedCalculation = ownedUsage-&gt;selectByKind(CalculationUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<ICalculationUsage> ComputeOwnedCalculation(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedCase = ownedUsage-&gt;selectByKind(CaseUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<ICaseUsage> ComputeOwnedCase(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedConcern = ownedUsage-&gt;selectByKind(ConcernUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IConcernUsage> ComputeOwnedConcern(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedConnection = ownedUsage-&gt;selectByKind(ConnectorAsUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IConnectorAsUsage> ComputeOwnedConnection(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedConstraint = ownedUsage-&gt;selectByKind(ConstraintUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IConstraintUsage> ComputeOwnedConstraint(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedEnumeration = ownedUsage-&gt;selectByKind(EnumerationUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IEnumerationUsage> ComputeOwnedEnumeration(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedFlow = ownedUsage-&gt;selectByKind(FlowConnectionUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IFlowUsage> ComputeOwnedFlow(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedInterface = ownedUsage-&gt;selectByKind(ReferenceUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IInterfaceUsage> ComputeOwnedInterface(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedItem = ownedUsage-&gt;selectByKind(ItemUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IItemUsage> ComputeOwnedItem(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedMetadata = ownedUsage-&gt;selectByKind(MetadataUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IMetadataUsage> ComputeOwnedMetadata(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedOccurrence = ownedUsage-&gt;selectByKind(OccurrenceUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IOccurrenceUsage> ComputeOwnedOccurrence(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedPart = ownedUsage-&gt;selectByKind(PartUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IPartUsage> ComputeOwnedPart(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedPort = ownedUsage-&gt;selectByKind(PortUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IPortUsage> ComputeOwnedPort(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedReference = ownedUsage-&gt;selectByKind(ReferenceUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IReferenceUsage> ComputeOwnedReference(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedRendering = ownedUsage-&gt;selectByKind(RenderingUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IRenderingUsage> ComputeOwnedRendering(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedRequirement = ownedUsage-&gt;selectByKind(RequirementUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IRequirementUsage> ComputeOwnedRequirement(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedState = ownedUsage-&gt;selectByKind(StateUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IStateUsage> ComputeOwnedState(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedTransition = ownedUsage-&gt;selectByKind(TransitionUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<ITransitionUsage> ComputeOwnedTransition(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedUsage = ownedFeature-&gt;selectByKind(Usage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IUsage> ComputeOwnedUsage(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedUseCase = ownedUsage-&gt;selectByKind(UseCaseUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IUseCaseUsage> ComputeOwnedUseCase(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedVerificationCase = ownedUsage-&gt;selectByKind(VerificationCaseUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IVerificationCaseUsage> ComputeOwnedVerificationCase(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedView = ownedUsage-&gt;selectByKind(ViewUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IViewUsage> ComputeOwnedView(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedViewpoint = ownedUsage-&gt;selectByKind(ViewpointUsage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IViewpointUsage> ComputeOwnedViewpoint(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// usage = feature-&gt;selectByKind(Usage)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IUsage> ComputeUsage(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// variant = variantMembership.ownedVariantUsage
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IUsage> ComputeVariant(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// variantMembership = ownedMembership-&gt;selectByKind(VariantMembership)
        /// </code>
        /// </remarks>
        /// <param name="definitionSubject">
        /// The subject <see cref="IDefinition"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IVariantMembership> ComputeVariantMembership(this IDefinition definitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

    }
}
