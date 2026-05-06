// -------------------------------------------------------------------------------------------------
// <copyright file="OccurrenceDefinitionTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="OccurrenceDefinitionTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceDefinition" /> element
    /// </summary>
    public static partial class OccurrenceDefinitionTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule OccurrenceDefinitionPrefix
        /// <para>OccurrenceDefinitionPrefix:OccurrenceDefinition=BasicDefinitionPrefix?(isIndividual?='individual'ownedRelationship+=EmptyMultiplicityMember)?DefinitionExtensionKeyword*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceDefinition" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOccurrenceDefinitionPrefix(SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceDefinition poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (poco.IsAbstract || poco.IsVariation)
            {
                SharedTextualNotationBuilder.BuildBasicDefinitionPrefix(poco, writerContext, stringBuilder);
            }

            if (poco.IsIndividual && ownedRelationshipCursor.Current != null)
            {
                stringBuilder.Append(" individual ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership elementAsOwningMembership)
                    {
                        OwningMembershipTextualNotationBuilder.BuildEmptyMultiplicityMember(elementAsOwningMembership, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();

                    }
                }
                stringBuilder.Append(' ');
            }

            while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership owningMembershipGuard && owningMembershipGuard.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Systems.Metadata.IMetadataUsage>().Any())
            {
                DefinitionTextualNotationBuilder.BuildDefinitionExtensionKeyword(poco, writerContext, stringBuilder);
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule IndividualDefinition
        /// <para>IndividualDefinition:OccurrenceDefinition=BasicDefinitionPrefix?isIndividual?='individual'DefinitionExtensionKeyword*'def'DefinitionownedRelationship+=EmptyMultiplicityMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceDefinition" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildIndividualDefinition(SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceDefinition poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (poco.IsAbstract || poco.IsVariation)
            {
                SharedTextualNotationBuilder.BuildBasicDefinitionPrefix(poco, writerContext, stringBuilder);
            }
            if (poco.IsIndividual)
            {
                stringBuilder.Append(" individual ");
            }
            while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership)
            {
                DefinitionTextualNotationBuilder.BuildDefinitionExtensionKeyword(poco, writerContext, stringBuilder);
            }

            stringBuilder.Append("def ");
            DefinitionTextualNotationBuilder.BuildDefinition(poco, writerContext, stringBuilder);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership elementAsOwningMembership)
                {
                    OwningMembershipTextualNotationBuilder.BuildEmptyMultiplicityMember(elementAsOwningMembership, writerContext, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OccurrenceDefinition
        /// <para>OccurrenceDefinition=OccurrenceDefinitionPrefix'occurrence''def'Definition</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceDefinition" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOccurrenceDefinition(SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceDefinition poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            BuildOccurrenceDefinitionPrefix(poco, writerContext, stringBuilder);
            stringBuilder.Append("occurrence ");
            stringBuilder.Append("def ");
            DefinitionTextualNotationBuilder.BuildDefinition(poco, writerContext, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
