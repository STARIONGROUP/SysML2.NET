// -------------------------------------------------------------------------------------------------
// <copyright file="TypeTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;

    /// <summary>
    /// Hand-coded part of the <see cref="TypeTextualNotationBuilder" />
    /// </summary>
    public static partial class TypeTextualNotationBuilder
    {
        /// <summary>
        /// Build the complex DefinitionBodyItem:Type=ownedRelationship+=DefinitionMember|ownedRelationship+=VariantUsageMember|ownedRelationship+=NonOccurrenceUsageMember|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=OccurrenceUsageMember|ownedRelationship+=AliasMember|ownedRelationship+=Import rule
        /// </summary>
        /// <param name="poco">The <see cref="IType" /></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /></param>
        internal static void BuildDefinitionBodyItemInternal(IType poco, StringBuilder stringBuilder)
        {
            var relationships = poco.OwnedRelationship.ToList();

            for (var ownedRelationshipIndex = 0; ownedRelationshipIndex < relationships.Count; ownedRelationshipIndex++)
            {
                ownedRelationshipIndex = BuildDefinitionBodyItem(ownedRelationshipIndex, relationships, stringBuilder);
            }
        }

        /// <summary>
        /// Build the logic for the DefinitionBodyItem:Type=ownedRelationship+=DefinitionMember|ownedRelationship+=VariantUsageMember|ownedRelationship+=NonOccurrenceUsageMember|
        /// (ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=OccurrenceUsageMember|ownedRelationship+=AliasMember|ownedRelationship+=Import rule
        /// </summary>
        /// <param name="relationshipIndex">The index of the <see cref="IRelationship"/> inside the <paramref name="relationships"/> to process</param>
        /// <param name="relationships">A collection of <see cref="IRelationship"/> to process</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /></param>
        /// <returns>The current index that could have been modified during the process</returns>
        internal static int BuildDefinitionBodyItem(int relationshipIndex, IReadOnlyList<IRelationship> relationships, StringBuilder stringBuilder)
        {
            var ownedRelationship = relationships[relationshipIndex];

            switch (ownedRelationship)
            {
                case OwningMembership owningMembership:
                    OwningMembershipTextualNotationBuilder.BuildDefinitionMember(owningMembership, stringBuilder);
                    break;

                case VariantMembership variantMembership:
                    VariantMembershipTextualNotationBuilder.BuildVariantUsageMember(variantMembership, stringBuilder);
                    break;

                case FeatureMembership featureMembershipForSuccession when featureMembershipForSuccession.IsValidForSourceSuccessionMember():
                {
                    var nextElement = relationshipIndex + 1 < relationships.Count ? relationships[relationshipIndex + 1] : null;

                    if (nextElement is FeatureMembership featureMembership && featureMembership.IsValidForOccurrenceUsageMember())
                    {
                        FeatureMembershipTextualNotationBuilder.BuildSourceSuccessionMember(featureMembershipForSuccession, stringBuilder);
                        FeatureMembershipTextualNotationBuilder.BuildOccurrenceUsageMember(featureMembership, stringBuilder);
                        return relationshipIndex + 1;
                    }

                    break;
                }

                case FeatureMembership featureMembershipForOccurenceUsageMember when featureMembershipForOccurenceUsageMember.IsValidForOccurrenceUsageMember():
                    FeatureMembershipTextualNotationBuilder.BuildOccurrenceUsageMember(featureMembershipForOccurenceUsageMember, stringBuilder);
                    break;

                case FeatureMembership featureMembershipForNonOccurenceUsageMember when featureMembershipForNonOccurenceUsageMember.IsValidForNonOccurrenceUsageMember():
                    FeatureMembershipTextualNotationBuilder.BuildNonOccurrenceUsageMember(featureMembershipForNonOccurenceUsageMember, stringBuilder);
                    break;

                case Membership membership:
                    MembershipTextualNotationBuilder.BuildAliasMember(membership, stringBuilder);
                    break;

                case IImport import:
                    ImportTextualNotationBuilder.BuildImport(import, stringBuilder);
                    break;
            }

            return relationshipIndex;
        }

        /// <summary>
        /// Build the logic for the ActionBodyItem:Type=NonBehaviorBodyItem|ownedRelationship+=InitialNodeMember(ownedRelationship+=ActionTargetSuccessionMember)*|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=ActionBehaviorMember(ownedRelationship+=ActionTargetSuccessionMember)*|ownedRelationship+=GuardedSuccessionMember rule
        /// </summary>
        /// <param name="relationshipIndex">The index of the <see cref="IRelationship"/> inside the <paramref name="relationships"/> to process</param>
        /// <param name="relationships">A collection of <see cref="IRelationship"/> to process</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /></param>
        /// <returns>The current index that could have been modified during the process</returns>
        private static int BuildActionBodyItem(int relationshipIndex, List<IRelationship> relationships, StringBuilder stringBuilder)
        {
            return relationshipIndex;
        }

        /// <summary>
        /// Build the logic for the ypeBodyElement:Type=ownedRelationship+=NonFeatureMember|ownedRelationship+=FeatureMember|ownedRelationship+=AliasMember|ownedRelationship+=Import rule
        /// <remarks>This implementation is a copy paste from the other one but required for Other rules</remarks>
        /// </summary>
        /// <param name="relationshipIndex">The index of the <see cref="IRelationship"/> inside the <paramref name="relationships"/> to process</param>
        /// <param name="relationships">A collection of <see cref="IRelationship"/> to process</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /></param>
        /// <returns>The current index that could have been modified during the process</returns>
        private static int BuildTypeBodyElement(int relationshipIndex, List<IRelationship> relationships, StringBuilder stringBuilder)
        {
            var elementInOwnedRelationship = relationships[relationshipIndex];
            
            switch (elementInOwnedRelationship)
            {
                case SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership owningMembership when owningMembership.IsValidForNonFeatureMember():
                    OwningMembershipTextualNotationBuilder.BuildNonFeatureMember(owningMembership, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership owningMembership when owningMembership.IsValidForFeatureMember():
                    OwningMembershipTextualNotationBuilder.BuildFeatureMember(owningMembership, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.Membership membership:
                    MembershipTextualNotationBuilder.BuildAliasMember(membership, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.IImport import:
                    ImportTextualNotationBuilder.BuildImport(import, stringBuilder);
                    break;
            }
            
            return relationshipIndex;
        }
    }
}
