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
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IClassifier> ComputeDefinition(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IUsage> ComputeDirectedUsage(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static bool ComputeIsReference(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static bool ComputeMayTimeVary(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IActionUsage> ComputeNestedAction(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IAllocationUsage> ComputeNestedAllocation(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IAnalysisCaseUsage> ComputeNestedAnalysisCase(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IAttributeUsage> ComputeNestedAttribute(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<ICalculationUsage> ComputeNestedCalculation(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<ICaseUsage> ComputeNestedCase(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IConcernUsage> ComputeNestedConcern(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IConnectorAsUsage> ComputeNestedConnection(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IConstraintUsage> ComputeNestedConstraint(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IEnumerationUsage> ComputeNestedEnumeration(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IFlowUsage> ComputeNestedFlow(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IInterfaceUsage> ComputeNestedInterface(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IItemUsage> ComputeNestedItem(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IMetadataUsage> ComputeNestedMetadata(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IOccurrenceUsage> ComputeNestedOccurrence(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IPartUsage> ComputeNestedPart(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IPortUsage> ComputeNestedPort(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IReferenceUsage> ComputeNestedReference(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IRenderingUsage> ComputeNestedRendering(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IRequirementUsage> ComputeNestedRequirement(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IStateUsage> ComputeNestedState(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<ITransitionUsage> ComputeNestedTransition(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IUsage> ComputeNestedUsage(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IUseCaseUsage> ComputeNestedUseCase(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IVerificationCaseUsage> ComputeNestedVerificationCase(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IViewUsage> ComputeNestedView(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IViewpointUsage> ComputeNestedViewpoint(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IDefinition ComputeOwningDefinition(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IUsage ComputeOwningUsage(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IUsage> ComputeUsage(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IUsage> ComputeVariant(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usage">
        /// The subject <see cref="IUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IVariantMembership> ComputeVariantMembership(this IUsage usage)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

    }
}
