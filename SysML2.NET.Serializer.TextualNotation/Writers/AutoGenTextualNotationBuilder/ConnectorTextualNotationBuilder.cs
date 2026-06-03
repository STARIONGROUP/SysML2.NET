// -------------------------------------------------------------------------------------------------
// <copyright file="ConnectorTextualNotationBuilder.cs" company="Starion Group S.A.">
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.TextualNotation.Writers
{
    using System.Linq;

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
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildConnectorDeclaration(SysML2.NET.Core.POCO.Kernel.Connectors.IConnector poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Kernel.Connectors.IConnector pocoConnectorBinaryConnectorDeclaration when pocoConnectorBinaryConnectorDeclaration.IsValidForBinaryConnectorDeclaration(writerContext):
                    BuildBinaryConnectorDeclaration(pocoConnectorBinaryConnectorDeclaration, writerContext, stringBuilder);
                    break;
                default:
                    BuildNaryConnectorDeclaration(poco, writerContext, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BinaryConnectorDeclaration
        /// <para>BinaryConnectorDeclaration:Connector=(FeatureDeclaration?'from'|isSufficient?='all''from'?)?ownedRelationship+=ConnectorEndMember'to'ownedRelationship+=ConnectorEndMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.IConnector" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildBinaryConnectorDeclaration(SysML2.NET.Core.POCO.Kernel.Connectors.IConnector poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            BuildBinaryConnectorDeclarationHandCoded(poco, writerContext, stringBuilder);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(elementAsEndFeatureMembership, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                }
            }
            stringBuilder.Append("to ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(elementAsEndFeatureMembership, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                }
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NaryConnectorDeclaration
        /// <para>NaryConnectorDeclaration:Connector=FeatureDeclaration?'('ownedRelationship+=ConnectorEndMember','ownedRelationship+=ConnectorEndMember(','ownedRelationship+=ConnectorEndMember)*')'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.IConnector" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildNaryConnectorDeclaration(SysML2.NET.Core.POCO.Kernel.Connectors.IConnector poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            var ownedTypeFeaturingCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedTypeFeaturing", poco.ownedTypeFeaturing);

            if (poco.IsSufficient || !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureTyping || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.ISubsetting || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IReferenceSubsetting || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.ICrossSubsetting || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IRedefinition || (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership owningMembership0 && owningMembership0.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Kernel.Multiplicities.IMultiplicityRange>().Any()) || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IConjugation || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IDisjoining || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IUnioning || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IIntersecting || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IDifferencing || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureChaining || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureInverting || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.ITypeFeaturing) || poco.IsOrdered || ownedTypeFeaturingCursor.Current is SysML2.NET.Core.POCO.Core.Features.ITypeFeaturing)
            {
                FeatureTextualNotationBuilder.BuildFeatureDeclaration(poco, writerContext, stringBuilder);
            }
            stringBuilder.Append("(");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(elementAsEndFeatureMembership, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                }
            }
            stringBuilder.Append(", ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(elementAsEndFeatureMembership, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                }
            }

            while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership endFeatureMembershipGuard && endFeatureMembershipGuard.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage>().Any())
            {
                stringBuilder.Append(", ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                    {
                        EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(elementAsEndFeatureMembership, writerContext, stringBuilder);
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
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildConnector(SysML2.NET.Core.POCO.Kernel.Connectors.IConnector poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            SharedTextualNotationBuilder.BuildFeaturePrefix(poco, writerContext, stringBuilder);
            stringBuilder.Append("connector ");
            BuildConnectorHandCoded(poco, writerContext, stringBuilder);
            stringBuilder.Append(' ');
            TypeTextualNotationBuilder.BuildTypeBody(poco, writerContext, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
