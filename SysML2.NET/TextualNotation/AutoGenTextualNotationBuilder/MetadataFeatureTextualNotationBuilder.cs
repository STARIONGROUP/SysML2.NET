// -------------------------------------------------------------------------------------------------
// <copyright file="MetadataFeatureTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="MetadataFeatureTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Kernel.Metadata.IMetadataFeature" /> element
    /// </summary>
    public static partial class MetadataFeatureTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule PrefixMetadataFeature
        /// <para>PrefixMetadataFeature:MetadataFeature=ownedRelationship+=OwnedFeatureTyping</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Metadata.IMetadataFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPrefixMetadataFeature(SysML2.NET.Core.POCO.Kernel.Metadata.IMetadataFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureTypingIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.FeatureTyping>().GetEnumerator();
            ownedRelationshipOfFeatureTypingIterator.MoveNext();

            if (ownedRelationshipOfFeatureTypingIterator.Current != null)
            {
                FeatureTypingTextualNotationBuilder.BuildOwnedFeatureTyping(ownedRelationshipOfFeatureTypingIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataFeatureDeclaration
        /// <para>MetadataFeatureDeclaration:MetadataFeature=(Identification(':'|'typed''by'))?ownedRelationship+=OwnedFeatureTyping</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Metadata.IMetadataFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataFeatureDeclaration(SysML2.NET.Core.POCO.Kernel.Metadata.IMetadataFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureTypingIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.FeatureTyping>().GetEnumerator();

            if (BuildGroupConditionForMetadataFeatureDeclaration(poco))
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
        /// Builds the Textual Notation string for the rule MetadataFeature
        /// <para>MetadataFeature=(ownedRelationship+=PrefixMetadataMember)*('@'|'metadata')MetadataFeatureDeclaration('about'ownedRelationship+=Annotation(','ownedRelationship+=Annotation)*)?MetadataBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Metadata.IMetadataFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataFeature(SysML2.NET.Core.POCO.Kernel.Metadata.IMetadataFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfOwningMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership>().GetEnumerator();
            using var ownedRelationshipOfAnnotationIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Annotations.Annotation>().GetEnumerator();

            while (ownedRelationshipOfOwningMembershipIterator.MoveNext())
            {

                if (ownedRelationshipOfOwningMembershipIterator.Current != null)
                {
                    OwningMembershipTextualNotationBuilder.BuildPrefixMetadataMember(ownedRelationshipOfOwningMembershipIterator.Current, stringBuilder);
                }

            }
            stringBuilder.Append(" @ ");
            stringBuilder.Append(' ');
            BuildMetadataFeatureDeclaration(poco, stringBuilder);

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
