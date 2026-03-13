// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureMembership.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Core.Types
{
    using System.Linq;

    using SysML2.NET.Core.POCO.Root.Elements;
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
    /// A FeatureMembership is an OwningMembership between an ownedMemberFeature and an owningType. If the
    /// ownedMemberFeature has isVariable = false, then the FeatureMembership implies that the owningType is
    /// also a featuringType of the ownedMemberFeature. If the ownedMemberFeature has isVariable = true,
    /// then the FeatureMembership implies that the ownedMemberFeature is featured by the snapshots of the
    /// owningType, which must specialize the Kernel Semantic Library base class Occurrence.
    /// </summary>
    public partial class FeatureMembership
    {
        /// <summary>
        /// Asserts that this <see cref="FeatureMembership" /> contais at least <see cref="ISuccessionAsUsage" /> element into the
        /// <see cref="OwnedRelatedElement" /> collection
        /// </summary>
        /// <returns>True if it contains one <see cref="ISuccessionAsUsage" /></returns>
        internal bool IsValidForSourceSuccessionMember()
        {
            return this.HasRelatedElementOfType<ISuccessionAsUsage>();
        }

        /// <summary>
        /// Asserts that this <see cref="FeatureMembership" /> contais at least <see cref="IUsage" /> element into the
        /// <see cref="OwnedRelatedElement" /> collection but none of them are <see cref="IOccurrenceUsage" />
        /// </summary>
        /// <returns>True if it contains one <see cref="IUsage" /> but no <see cref="IOccurrenceUsage" /></returns>
        internal bool IsValidForNonOccurrenceUsageMember()
        {
            return !this.IsValidForOccurrenceUsageMember() && this.HasRelatedElementOfType<IUsage>();
        }

        /// <summary>
        /// Asserts that this <see cref="FeatureMembership" /> contais at least <see cref="IOccurrenceUsage" /> element into the
        /// <see cref="OwnedRelatedElement" /> collection
        /// </summary>
        /// <returns>True if it contains one <see cref="IOccurrenceUsage" /></returns>
        internal bool IsValidForOccurrenceUsageMember()
        {
            return this.HasRelatedElementOfType<IOccurrenceUsage>();
        }

        /// <summary>
        /// Asserts that the <see cref="OwnedRelatedElement" /> contains at least one <typeparamref name="T" /> element
        /// </summary>
        /// <typeparam name="T">Any <see cref="IElement" /></typeparam>
        /// <returns>True if the <see cref="OwnedRelatedElement" /> contains one <typeparamref name="T" /> element </returns>
        private bool HasRelatedElementOfType<T>() where T : IElement
        {
            return this.OwnedRelatedElement.OfType<T>().Any();
        }

        /// <summary>
        /// Asserts that the current <see cref="FeatureMembership"/> have valid <see cref="IElement" /> typeinto the
        /// <see cref="OwnedRelatedElement" /> collection for the StructureUsageMember
        /// </summary>
        /// <returns>True if contains any of the required element type</returns>
        internal bool IsValidForStructureUsageMember()
        {
            return this.HasRelatedElementOfType<OccurrenceUsage>() 
                   || this.HasRelatedElementOfType<EventOccurrenceUsage>()
                   || this.HasRelatedElementOfType<ItemUsage>()
                   || this.HasRelatedElementOfType<PartUsage>()
                   || this.HasRelatedElementOfType<ViewUsage>()
                   || this.HasRelatedElementOfType<RenderingUsage>()
                   || this.HasRelatedElementOfType<PortUsage>()
                   || this.HasRelatedElementOfType<ConnectionUsage>()
                   || this.HasRelatedElementOfType<InterfaceUsage>()
                   || this.HasRelatedElementOfType<AllocationUsage>()
                   || this.HasRelatedElementOfType<FlowUsage>()
                   || this.HasRelatedElementOfType<SuccessionFlowUsage>();
        }
    }
}
