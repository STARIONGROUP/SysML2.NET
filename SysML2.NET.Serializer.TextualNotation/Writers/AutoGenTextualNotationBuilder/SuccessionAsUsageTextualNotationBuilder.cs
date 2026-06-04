// -------------------------------------------------------------------------------------------------
// <copyright file="SuccessionAsUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="SuccessionAsUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Connections.ISuccessionAsUsage" /> element
    /// </summary>
    public static partial class SuccessionAsUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule SourceSuccession
        /// <para>SourceSuccession:SuccessionAsUsage=ownedRelationship+=SourceEndMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Connections.ISuccessionAsUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildSourceSuccession(SysML2.NET.Core.POCO.Systems.Connections.ISuccessionAsUsage poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildSourceEndMember(elementAsEndFeatureMembership, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                }
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TargetSuccession
        /// <para>TargetSuccession:SuccessionAsUsage=ownedRelationship+=SourceEndMember'then'ownedRelationship+=ConnectorEndMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Connections.ISuccessionAsUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildTargetSuccession(SysML2.NET.Core.POCO.Systems.Connections.ISuccessionAsUsage poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildSourceEndMember(elementAsEndFeatureMembership, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                }
            }
            stringBuilder.Append("then ");

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
        /// Builds the Textual Notation string for the rule SuccessionAsUsage
        /// <para>SuccessionAsUsage=UsagePrefix('succession'UsageDeclaration)?'first's.ownedRelationship+=ConnectorEndMember'then's.ownedRelationship+=ConnectorEndMemberUsageBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Connections.ISuccessionAsUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildSuccessionAsUsage(SysML2.NET.Core.POCO.Systems.Connections.ISuccessionAsUsage poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            UsageTextualNotationBuilder.BuildUsagePrefix(poco, writerContext, stringBuilder);

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureTyping || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.ISubsetting || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IReferenceSubsetting || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.ICrossSubsetting || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IRedefinition || (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership owningMembership0 && owningMembership0.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Kernel.Multiplicities.IMultiplicityRange>().Any())) || poco.IsOrdered)
            {
                stringBuilder.Append("succession ");
                UsageTextualNotationBuilder.BuildUsageDeclaration(poco, writerContext, stringBuilder);
                stringBuilder.Append(' ');
            }

            stringBuilder.Append("first ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(elementAsEndFeatureMembership, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                }
            }
            stringBuilder.Append("then ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(elementAsEndFeatureMembership, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                }
            }
            UsageTextualNotationBuilder.BuildUsageBody(poco, writerContext, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
