// -------------------------------------------------------------------------------------------------
// <copyright file="AnnotationTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="AnnotationTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Root.Annotations.IAnnotation" /> element
    /// </summary>
    public static partial class AnnotationTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedAnnotation
        /// <para>OwnedAnnotation:Annotation=ownedRelatedElement+=AnnotatingElement</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Annotations.IAnnotation" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedAnnotation(SysML2.NET.Core.POCO.Root.Annotations.IAnnotation poco, StringBuilder stringBuilder)
        {
            using var ownedRelatedElementOfAnnotatingElementIterator = poco.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Root.Annotations.AnnotatingElement>().GetEnumerator();
            ownedRelatedElementOfAnnotatingElementIterator.MoveNext();

            if (ownedRelatedElementOfAnnotatingElementIterator.Current != null)
            {
                AnnotatingElementTextualNotationBuilder.BuildAnnotatingElement(ownedRelatedElementOfAnnotatingElementIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PrefixMetadataAnnotation
        /// <para>PrefixMetadataAnnotation:Annotation='#'annotatingElement=PrefixMetadataUsage{ownedRelatedElement+=annotatingElement}</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Annotations.IAnnotation" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPrefixMetadataAnnotation(SysML2.NET.Core.POCO.Root.Annotations.IAnnotation poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append("#");

            if (poco.annotatingElement != null)
            {
                using var ownedRelationshipOfFeatureTypingIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.FeatureTyping>().GetEnumerator();
                ownedRelationshipOfFeatureTypingIterator.MoveNext();

                if (ownedRelationshipOfFeatureTypingIterator.Current != null)
                {
                    FeatureTypingTextualNotationBuilder.BuildOwnedFeatureTyping(ownedRelationshipOfFeatureTypingIterator.Current, stringBuilder);
                }

            }
            // NonParsing Assignment Element : ownedRelatedElement += annotatingElement => Does not have to be process

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Annotation
        /// <para>Annotation=annotatedElement=[QualifiedName]</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Annotations.IAnnotation" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildAnnotation(SysML2.NET.Core.POCO.Root.Annotations.IAnnotation poco, StringBuilder stringBuilder)
        {

            if (poco.AnnotatedElement != null)
            {
                stringBuilder.Append(poco.AnnotatedElement.qualifiedName);
                stringBuilder.Append(' ');
            }

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
