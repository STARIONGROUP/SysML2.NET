// -------------------------------------------------------------------------------------------------
// <copyright file="UsageExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Systems.DefinitionAndUsage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
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
    /// The <see cref="UsageExtensions" /> class provides extensions methods for
    /// the <see cref="IUsage" /> interface
    /// </summary>
    internal static class UsageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IClassifier> ComputeDefinition(this IUsage usageSubject)
        {
            // Walk OwnedRelationship → IFeatureTyping → Type directly. Cannot use
            // usageSubject.type because the Usage POCO's IFeature.type explicitly delegates
            // to .definition (the redefining property), which calls back into this method
            // → infinite recursion + stack overflow.
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.OwnedRelationship.OfType<IFeatureTyping>().Select(featureTyping => featureTyping.Type).OfType<IClassifier>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IUsage> ComputeDirectedUsage(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.directedFeature.OfType<IUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static bool ComputeIsReference(this IUsage usageSubject)
        {
            return !usageSubject?.IsComposite ?? throw new ArgumentNullException(nameof(usageSubject));
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static bool ComputeMayTimeVary(this IUsage usageSubject)
        {
            if (usageSubject == null)
            {
                throw new ArgumentNullException(nameof(usageSubject));
            }

            var owningType = usageSubject.owningType;

            if (owningType == null)
            {
                return false;
            }

            if (!owningType.SpecializesFromLibrary("Occurrences::Occurrence"))
            {
                return false;
            }

            if (usageSubject.IsPortion)
            {
                return false;
            }

            if (usageSubject.SpecializesFromLibrary("Links::SelfLink"))
            {
                return false;
            }

            if (usageSubject.SpecializesFromLibrary("Occurrences::HappensLink"))
            {
                return false;
            }

            return !usageSubject.IsComposite || !usageSubject.SpecializesFromLibrary("Actions::Action");
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IActionUsage> ComputeNestedAction(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IActionUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IAllocationUsage> ComputeNestedAllocation(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IAllocationUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IAnalysisCaseUsage> ComputeNestedAnalysisCase(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IAnalysisCaseUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IAttributeUsage> ComputeNestedAttribute(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IAttributeUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<ICalculationUsage> ComputeNestedCalculation(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<ICalculationUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<ICaseUsage> ComputeNestedCase(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<ICaseUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IConcernUsage> ComputeNestedConcern(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IConcernUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IConnectorAsUsage> ComputeNestedConnection(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IConnectorAsUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IConstraintUsage> ComputeNestedConstraint(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IConstraintUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IEnumerationUsage> ComputeNestedEnumeration(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IEnumerationUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IFlowUsage> ComputeNestedFlow(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IFlowUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IInterfaceUsage> ComputeNestedInterface(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IInterfaceUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IItemUsage> ComputeNestedItem(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IItemUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IMetadataUsage> ComputeNestedMetadata(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IMetadataUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IOccurrenceUsage> ComputeNestedOccurrence(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IOccurrenceUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IPartUsage> ComputeNestedPart(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IPartUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IPortUsage> ComputeNestedPort(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IPortUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IReferenceUsage> ComputeNestedReference(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IReferenceUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IRenderingUsage> ComputeNestedRendering(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IRenderingUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IRequirementUsage> ComputeNestedRequirement(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IRequirementUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IStateUsage> ComputeNestedState(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IStateUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<ITransitionUsage> ComputeNestedTransition(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<ITransitionUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IUsage> ComputeNestedUsage(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.ownedFeature.OfType<IUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IUseCaseUsage> ComputeNestedUseCase(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IUseCaseUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IVerificationCaseUsage> ComputeNestedVerificationCase(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IVerificationCaseUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IViewUsage> ComputeNestedView(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IViewUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IViewpointUsage> ComputeNestedViewpoint(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.nestedUsage.OfType<IViewpointUsage>()];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IDefinition ComputeOwningDefinition(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : usageSubject.owningType as IDefinition;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="usageSubject">
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IUsage ComputeOwningUsage(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : usageSubject.owningType as IUsage;
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IUsage> ComputeUsage(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.feature.OfType<IUsage>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IUsage> ComputeVariant(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                :
                [
                    ..usageSubject.variantMembership
                        .Select(membership => membership.ownedVariantUsage)
                        .Where(usage => usage != null)
                ];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IVariantMembership> ComputeVariantMembership(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : [..usageSubject.ownedMembership.OfType<IVariantMembership>()];
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// The expected <see cref="IFeature" />
        /// </returns>
        internal static IFeature ComputeRedefinedNamingFeatureOperation(this IUsage usageSubject)
        {
            if (usageSubject == null)
            {
                throw new ArgumentNullException(nameof(usageSubject));
            }

            if (usageSubject.owningMembership is not IVariantMembership)
            {
                // OCL: self.oclAsType(Feature).namingFeature() — call the Feature-level
                // definition directly via the static extension to bypass the Usage-level
                // virtual override (which would recurse back into this method).
                return usageSubject.ComputeNamingFeatureOperation();
            }

            return usageSubject.ownedReferenceSubsetting?.ReferencedFeature;
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
        /// The subject <see cref="IUsage" />
        /// </param>
        /// <returns>
        /// The expected <see cref="IFeature" />
        /// </returns>
        internal static IFeature ComputeReferencedFeatureTargetOperation(this IUsage usageSubject)
        {
            return usageSubject == null
                ? throw new ArgumentNullException(nameof(usageSubject))
                : usageSubject.ownedReferenceSubsetting?.ReferencedFeature?.featureTarget;
        }
    }
}
