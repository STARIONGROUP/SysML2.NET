// -------------------------------------------------------------------------------------------------
// <copyright file="AnnotatingElementTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="AnnotatingElementTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Root.Annotations.AnnotatingElement" /> element
    /// </summary>
    public class AnnotatingElementTextualNotationBuilder : TextualNotationBuilder<SysML2.NET.Core.POCO.Root.Annotations.AnnotatingElement>
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="AnnotatingElementTextualNotationBuilder"/>
        /// </summary>
        /// <param name="facade">The <see cref="ITextualNotationBuilderFacade"/> used to query textual notation of referenced <see cref="IElement"/></param>
        public AnnotatingElementTextualNotationBuilder(ITextualNotationBuilderFacade facade) : base(facade)
        {
        }

        /// <summary>
        /// Builds the Textual Notation string for the provided <see cref="SysML2.NET.Core.POCO.Root.Annotations.AnnotatingElement"/>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Annotations.AnnotatingElement"/> from which the textual notation should be build</param>
        /// <returns>The built textual notation string</returns>
        public override string BuildTextualNotation(SysML2.NET.Core.POCO.Root.Annotations.AnnotatingElement poco)
        {
            var stringBuilder = new StringBuilder();
            // Rule definition : AnnotatingElement=Comment|Documentation|TextualRepresentation|MetadataFeature



            // non Terminal : Comment; Found rule Comment=('comment'Identification('about'ownedRelationship+=Annotation(','ownedRelationship+=Annotation)*)?)?('locale'locale=STRING_VALUE)?body=REGULAR_COMMENT


            // non Terminal : Documentation; Found rule Documentation='doc'Identification('locale'locale=STRING_VALUE)?body=REGULAR_COMMENT




            // non Terminal : TextualRepresentation; Found rule TextualRepresentation=('rep'Identification)?'language'language=STRING_VALUEbody=REGULAR_COMMENT






            // non Terminal : MetadataFeature; Found rule MetadataFeature=(ownedRelationship+=PrefixMetadataMember)*('@'|'metadata')MetadataFeatureDeclaration('about'ownedRelationship+=Annotation(','ownedRelationship+=Annotation)*)?MetadataBody



            return stringBuilder.ToString();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
