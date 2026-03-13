// -------------------------------------------------------------------------------------------------
// <copyright file="SuccessionTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="SuccessionTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.ISuccession" /> element
    /// </summary>
    public static partial class SuccessionTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule TransitionSuccession
        /// <para>TransitionSuccession:Succession=ownedRelationship+=EmptyEndMemberownedRelationship+=ConnectorEndMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.ISuccession" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTransitionSuccession(SysML2.NET.Core.POCO.Kernel.Connectors.ISuccession poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfEndFeatureMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.EndFeatureMembership>().GetEnumerator();
            ownedRelationshipOfEndFeatureMembershipIterator.MoveNext();

            if (ownedRelationshipOfEndFeatureMembershipIterator.Current != null)
            {
                EndFeatureMembershipTextualNotationBuilder.BuildEmptyEndMember(ownedRelationshipOfEndFeatureMembershipIterator.Current, stringBuilder);
            }
            ownedRelationshipOfEndFeatureMembershipIterator.MoveNext();

            if (ownedRelationshipOfEndFeatureMembershipIterator.Current != null)
            {
                EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(ownedRelationshipOfEndFeatureMembershipIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SuccessionDeclaration
        /// <para>SuccessionDeclaration:Succession=FeatureDeclaration('first'ownedRelationship+=ConnectorEndMember'then'ownedRelationship+=ConnectorEndMember)?|(s.isSufficient?='all')?('first'?ownedRelationship+=ConnectorEndMember'then'ownedRelationship+=ConnectorEndMember)?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.ISuccession" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSuccessionDeclaration(SysML2.NET.Core.POCO.Kernel.Connectors.ISuccession poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Succession
        /// <para>Succession=FeaturePrefix'succession'SuccessionDeclarationTypeBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.ISuccession" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSuccession(SysML2.NET.Core.POCO.Kernel.Connectors.ISuccession poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfOwningMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership>().GetEnumerator();
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            stringBuilder.Append(' ');

            while (ownedRelationshipOfOwningMembershipIterator.MoveNext())
            {

                if (ownedRelationshipOfOwningMembershipIterator.Current != null)
                {
                    OwningMembershipTextualNotationBuilder.BuildPrefixMetadataMember(ownedRelationshipOfOwningMembershipIterator.Current, stringBuilder);
                }

            }

            stringBuilder.Append("succession ");
            BuildSuccessionDeclaration(poco, stringBuilder);
            TypeTextualNotationBuilder.BuildTypeBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
