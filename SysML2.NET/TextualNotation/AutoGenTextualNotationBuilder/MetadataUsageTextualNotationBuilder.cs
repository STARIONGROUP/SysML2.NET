// -------------------------------------------------------------------------------------------------
// <copyright file="MetadataUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="MetadataUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Metadata.IMetadataUsage" /> element
    /// </summary>
    public static partial class MetadataUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule PrefixMetadataUsage
        /// <para>PrefixMetadataUsage:MetadataUsage=ownedRelationship+=OwnedFeatureTyping</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Metadata.IMetadataUsage" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildPrefixMetadataUsage(SysML2.NET.Core.POCO.Systems.Metadata.IMetadataUsage poco, int elementIndex, StringBuilder stringBuilder)
        {
            if (elementIndex < poco.OwnedRelationship.Count)
            {
                var elementForOwnedRelationship = poco.OwnedRelationship[elementIndex];

                if (elementForOwnedRelationship is SysML2.NET.Core.POCO.Core.Features.IFeatureTyping elementAsFeatureTyping)
                {
                    FeatureTypingTextualNotationBuilder.BuildOwnedFeatureTyping(elementAsFeatureTyping, stringBuilder);
                }
            }

            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataUsageDeclaration
        /// <para>MetadataUsageDeclaration:MetadataUsage=(Identification(':'|'typed''by'))?ownedRelationship+=OwnedFeatureTyping</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Metadata.IMetadataUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataUsageDeclaration(SysML2.NET.Core.POCO.Systems.Metadata.IMetadataUsage poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureTypingIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.FeatureTyping>().GetEnumerator();

            if (BuildGroupConditionForMetadataUsageDeclaration(poco))
            {
                ElementTextualNotationBuilder.BuildIdentification(poco, stringBuilder);
                throw new System.NotSupportedException("Multiple alternatives not implemented yet");
                stringBuilder.Append(' ');
                stringBuilder.Append(' ');
            }

            ownedRelationshipOfFeatureTypingIterator.MoveNext();

            if (ownedRelationshipOfFeatureTypingIterator.Current != null)
            {
                FeatureTypingTextualNotationBuilder.BuildOwnedFeatureTyping(ownedRelationshipOfFeatureTypingIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataUsage
        /// <para>MetadataUsage=UsageExtensionKeyword*('@'|'metadata')MetadataUsageDeclaration('about'ownedRelationship+=Annotation(','ownedRelationship+=Annotation)*)?MetadataBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Metadata.IMetadataUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataUsage(SysML2.NET.Core.POCO.Systems.Metadata.IMetadataUsage poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfAnnotationIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Annotations.Annotation>().GetEnumerator();
            // Handle collection Non Terminal 
            for (var ownedRelationshipIndex = 0; ownedRelationshipIndex < poco.OwnedRelationship.Count; ownedRelationshipIndex++)
            {
                ownedRelationshipIndex = UsageTextualNotationBuilder.BuildUsageExtensionKeyword(poco, ownedRelationshipIndex, stringBuilder);
            }
            stringBuilder.Append(" @ ");
            stringBuilder.Append(' ');
            BuildMetadataUsageDeclaration(poco, stringBuilder);

            if (ownedRelationshipOfAnnotationIterator.MoveNext())
            {
                stringBuilder.Append("about ");

                if (ownedRelationshipOfAnnotationIterator.Current != null)
                {
                    AnnotationTextualNotationBuilder.BuildAnnotation(ownedRelationshipOfAnnotationIterator.Current, stringBuilder);
                }

                while (ownedRelationshipOfAnnotationIterator.MoveNext())
                {
                    stringBuilder.Append(",");

                    if (ownedRelationshipOfAnnotationIterator.Current != null)
                    {
                        AnnotationTextualNotationBuilder.BuildAnnotation(ownedRelationshipOfAnnotationIterator.Current, stringBuilder);
                    }

                }
                stringBuilder.Append(' ');
            }

            TypeTextualNotationBuilder.BuildMetadataBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
