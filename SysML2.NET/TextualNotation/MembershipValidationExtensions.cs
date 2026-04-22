// -------------------------------------------------------------------------------------------------
// <copyright file="MembershipValidationExtensions.cs" company="Starion Group S.A.">
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
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Allocations;
    using SysML2.NET.Core.POCO.Systems.Connections;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Flows;
    using SysML2.NET.Core.POCO.Systems.Interfaces;
    using SysML2.NET.Core.POCO.Systems.Items;
    using SysML2.NET.Core.POCO.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Core.POCO.Systems.Views;

    /// <summary>
    /// Extension methods for membership interfaces used in textual notation switch guards.
    /// These provide the same validation logic as the concrete class methods but operate on interfaces.
    /// </summary>
    public static class MembershipValidationExtensions
    {
        /// <summary>
        /// Asserts that the <see cref="IOwningMembership"/> contains at least one <see cref="IFeature"/>
        /// inside the <see cref="IRelationship.OwnedRelatedElement"/> collection
        /// </summary>
        /// <param name="owningMembership">The <see cref="IOwningMembership"/></param>
        /// <returns>True if one <see cref="IFeature"/> is contained in the <see cref="IRelationship.OwnedRelatedElement"/></returns>
        public static bool IsValidForNonFeatureMember(this IOwningMembership owningMembership)
        {
            return owningMembership.OwnedRelatedElement.OfType<IFeature>().Any();
        }

        /// <summary>
        /// Asserts that the <see cref="IOwningMembership"/> does not contain any <see cref="IFeature"/>
        /// inside the <see cref="IRelationship.OwnedRelatedElement"/> collection
        /// </summary>
        /// <param name="owningMembership">The <see cref="IOwningMembership"/></param>
        /// <returns>True if no <see cref="IFeature"/> is contained in the <see cref="IRelationship.OwnedRelatedElement"/></returns>
        public static bool IsValidForFeatureMember(this IOwningMembership owningMembership)
        {
            return !owningMembership.IsValidForNonFeatureMember();
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> contains at least one <see cref="ISuccessionAsUsage"/>
        /// inside the <see cref="IRelationship.OwnedRelatedElement"/> collection
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <returns>True if it contains one <see cref="ISuccessionAsUsage"/></returns>
        public static bool IsValidForSourceSuccessionMember(this IFeatureMembership featureMembership)
        {
            return featureMembership.OwnedRelatedElement.OfType<ISuccessionAsUsage>().Any();
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> contains at least one <see cref="IOccurrenceUsage"/>
        /// inside the <see cref="IRelationship.OwnedRelatedElement"/> collection
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <returns>True if it contains one <see cref="IOccurrenceUsage"/></returns>
        public static bool IsValidForOccurrenceUsageMember(this IFeatureMembership featureMembership)
        {
            return featureMembership.OwnedRelatedElement.OfType<IOccurrenceUsage>().Any();
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> contains at least one <see cref="IUsage"/>
        /// but no <see cref="IOccurrenceUsage"/> inside the <see cref="IRelationship.OwnedRelatedElement"/> collection
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <returns>True if it contains one <see cref="IUsage"/> but no <see cref="IOccurrenceUsage"/></returns>
        public static bool IsValidForNonOccurrenceUsageMember(this IFeatureMembership featureMembership)
        {
            return !featureMembership.IsValidForOccurrenceUsageMember() && featureMembership.OwnedRelatedElement.OfType<IUsage>().Any();
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> has valid element types for StructureUsageMember
        /// inside the <see cref="IRelationship.OwnedRelatedElement"/> collection
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <returns>True if contains any of the required element types</returns>
        public static bool IsValidForStructureUsageMember(this IFeatureMembership featureMembership)
        {
            return featureMembership.OwnedRelatedElement.OfType<OccurrenceUsage>().Any()
                   || featureMembership.OwnedRelatedElement.OfType<EventOccurrenceUsage>().Any()
                   || featureMembership.OwnedRelatedElement.OfType<ItemUsage>().Any()
                   || featureMembership.OwnedRelatedElement.OfType<PartUsage>().Any()
                   || featureMembership.OwnedRelatedElement.OfType<ViewUsage>().Any()
                   || featureMembership.OwnedRelatedElement.OfType<RenderingUsage>().Any()
                   || featureMembership.OwnedRelatedElement.OfType<PortUsage>().Any()
                   || featureMembership.OwnedRelatedElement.OfType<ConnectionUsage>().Any()
                   || featureMembership.OwnedRelatedElement.OfType<InterfaceUsage>().Any()
                   || featureMembership.OwnedRelatedElement.OfType<AllocationUsage>().Any()
                   || featureMembership.OwnedRelatedElement.OfType<FlowUsage>().Any()
                   || featureMembership.OwnedRelatedElement.OfType<SuccessionFlowUsage>().Any();
        }
    }
}
