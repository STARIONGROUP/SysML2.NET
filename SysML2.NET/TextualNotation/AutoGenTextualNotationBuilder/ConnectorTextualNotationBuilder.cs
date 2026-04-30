// -------------------------------------------------------------------------------------------------
// <copyright file="ConnectorTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="ConnectorTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.IConnector" /> element
    /// </summary>
    public static partial class ConnectorTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule ConnectorDeclaration
        /// <para>ConnectorDeclaration:Connector=BinaryConnectorDeclaration|NaryConnectorDeclaration</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.IConnector" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildConnectorDeclaration(SysML2.NET.Core.POCO.Kernel.Connectors.IConnector poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Kernel.Connectors.IConnector pocoConnectorBinaryConnectorDeclaration when pocoConnectorBinaryConnectorDeclaration.IsValidForBinaryConnectorDeclaration():
                    BuildBinaryConnectorDeclaration(pocoConnectorBinaryConnectorDeclaration, cursorCache, stringBuilder);
                    break;
                default:
                    BuildNaryConnectorDeclaration(poco, cursorCache, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BinaryConnectorDeclaration
        /// <para>BinaryConnectorDeclaration:Connector=(FeatureDeclaration?'from'|isSufficient?='all''from'?)?ownedRelationship+=ConnectorEndMember'to'ownedRelationship+=ConnectorEndMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.IConnector" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBinaryConnectorDeclaration(SysML2.NET.Core.POCO.Kernel.Connectors.IConnector poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            BuildBinaryConnectorDeclarationHandCoded(poco, cursorCache, stringBuilder);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(elementAsEndFeatureMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            stringBuilder.Append("to ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(elementAsEndFeatureMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NaryConnectorDeclaration
        /// <para>NaryConnectorDeclaration:Connector=FeatureDeclaration?'('ownedRelationship+=ConnectorEndMember','ownedRelationship+=ConnectorEndMember(','ownedRelationship+=ConnectorEndMember)*')'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.IConnector" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNaryConnectorDeclaration(SysML2.NET.Core.POCO.Kernel.Connectors.IConnector poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (poco.IsSufficient || !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || poco.IsOrdered || poco.unioningType.Count != 0 || poco.intersectingType.Count != 0 || poco.differencingType.Count != 0 || poco.featuringType.Count != 0 || poco.ownedTypeFeaturing.Count != 0)
            {
                FeatureTextualNotationBuilder.BuildFeatureDeclaration(poco, cursorCache, stringBuilder);
            }
            stringBuilder.Append("(");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(elementAsEndFeatureMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            stringBuilder.Append(", ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(elementAsEndFeatureMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership endFeatureMembershipGuard && endFeatureMembershipGuard.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage>().Any())
            {
                stringBuilder.Append(", ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                    {
                        EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(elementAsEndFeatureMembership, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }
            stringBuilder.Append(") ");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Connector
        /// <para>Connector=FeaturePrefix'connector'(FeatureDeclaration?ValuePart?|ConnectorDeclaration)TypeBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.IConnector" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildConnector(SysML2.NET.Core.POCO.Kernel.Connectors.IConnector poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            SharedTextualNotationBuilder.BuildFeaturePrefix(poco, cursorCache, stringBuilder);
            stringBuilder.Append("connector ");
            BuildConnectorHandCoded(poco, cursorCache, stringBuilder);
            stringBuilder.Append(' ');
            TypeTextualNotationBuilder.BuildTypeBody(poco, cursorCache, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
