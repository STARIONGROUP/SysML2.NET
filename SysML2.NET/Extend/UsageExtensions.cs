// -------------------------------------------------------------------------------------------------
// <copyright file="UsageExtensions.cs" company="Starion Group S.A.">
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
    /// The <see cref="UsageExtensions"/> class provides extensions methods for
    /// the <see cref="IUsage"/> interface
    /// </summary>
    internal static class UsageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IClassifier> ComputeDefinition(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// directedUsage = directedFeature-&gt;selectByKind(Usage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IUsage> ComputeDirectedUsage(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// isReference = not isComposite
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static bool ComputeIsReference(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// mayTimeVary =
        ///                             owningType &lt;&gt; null and
        ///                             owningType.specializesFromLibrary('Occurrences::Occurrence') and
        ///                             not (
        ///                             isPortion or
        ///                             specializesFromLibrary('Links::SelfLink') or
        ///                             specializesFromLibrary('Occurrences::HappensLink') or
        ///                             isComposite and specializesFromLibrary('Actions::Action')
        ///                             )
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static bool ComputeMayTimeVary(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedAction = nestedUsage-&gt;selectByKind(ActionUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IActionUsage> ComputeNestedAction(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedAllocation = nestedUsage-&gt;selectByKind(AllocationUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IAllocationUsage> ComputeNestedAllocation(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedAnalysisCase = nestedUsage-&gt;selectByKind(AnalysisCaseUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IAnalysisCaseUsage> ComputeNestedAnalysisCase(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedAttribute = nestedUsage-&gt;selectByKind(AttributeUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IAttributeUsage> ComputeNestedAttribute(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedCalculation = nestedUsage-&gt;selectByKind(CalculationUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<ICalculationUsage> ComputeNestedCalculation(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedCase = nestedUsage-&gt;selectByKind(CaseUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<ICaseUsage> ComputeNestedCase(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedConcern = nestedUsage-&gt;selectByKind(ConcernUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IConcernUsage> ComputeNestedConcern(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedConnection = nestedUsage-&gt;selectByKind(ConnectorAsUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IConnectorAsUsage> ComputeNestedConnection(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedConstraint = nestedUsage-&gt;selectByKind(ConstraintUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IConstraintUsage> ComputeNestedConstraint(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// ownedNested = nestedUsage-&gt;selectByKind(EnumerationUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IEnumerationUsage> ComputeNestedEnumeration(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedFlow = nestedUsage-&gt;selectByKind(FlowConnectionUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IFlowUsage> ComputeNestedFlow(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedInterface = nestedUsage-&gt;selectByKind(ReferenceUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IInterfaceUsage> ComputeNestedInterface(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedItem = nestedUsage-&gt;selectByKind(ItemUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IItemUsage> ComputeNestedItem(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedMetadata = nestedUsage-&gt;selectByKind(MetadataUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IMetadataUsage> ComputeNestedMetadata(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedOccurrence = nestedUsage-&gt;selectByKind(OccurrenceUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IOccurrenceUsage> ComputeNestedOccurrence(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedPart = nestedUsage-&gt;selectByKind(PartUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IPartUsage> ComputeNestedPart(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedPort = nestedUsage-&gt;selectByKind(PortUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IPortUsage> ComputeNestedPort(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedReference = nestedUsage-&gt;selectByKind(ReferenceUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IReferenceUsage> ComputeNestedReference(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedRendering = nestedUsage-&gt;selectByKind(RenderingUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IRenderingUsage> ComputeNestedRendering(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedRequirement = nestedUsage-&gt;selectByKind(RequirementUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IRequirementUsage> ComputeNestedRequirement(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedState = nestedUsage-&gt;selectByKind(StateUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IStateUsage> ComputeNestedState(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedTransition = nestedUsage-&gt;selectByKind(TransitionUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<ITransitionUsage> ComputeNestedTransition(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedUsage = ownedFeature-&gt;selectByKind(Usage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IUsage> ComputeNestedUsage(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedUseCase = nestedUsage-&gt;selectByKind(UseCaseUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IUseCaseUsage> ComputeNestedUseCase(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedVerificationCase = nestedUsage-&gt;selectByKind(VerificationCaseUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IVerificationCaseUsage> ComputeNestedVerificationCase(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedView = nestedUsage-&gt;selectByKind(ViewUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IViewUsage> ComputeNestedView(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// nestedViewpoint = nestedUsage-&gt;selectByKind(ViewpointUsage)
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IViewpointUsage> ComputeNestedViewpoint(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IDefinition ComputeOwningDefinition(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IUsage ComputeOwningUsage(this IUsage usageSubject)
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
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IUsage> ComputeUsage(this IUsage usageSubject)
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
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IUsage> ComputeVariant(this IUsage usageSubject)
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
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IVariantMembership> ComputeVariantMembership(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// If this Usage is a variant, then its naming Feature is the referencedFeature of its
        /// ownedReferenceSubsetting.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if not owningMembership.oclIsKindOf(VariantMembership) then
        ///                                 self.oclAsType(Feature).namingFeature()
        ///                                 else if ownedReferenceSubsetting = null then null
        ///                                 else ownedReferenceSubsetting.referencedFeature
        ///                                 endif endif
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="IFeature" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IFeature ComputeRedefinedNamingFeatureOperation(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// If ownedReferenceSubsetting is not null, return the featureTarget of the referencedFeature of the
        /// ownedReferenceSubsetting.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if ownedReferenceSubsetting = null then null
        ///                                 else ownedReferenceSubsetting.referencedFeature.featureTarget
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="IFeature" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IFeature ComputeReferencedFeatureTargetOperation(this IUsage usageSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }
    }
}
