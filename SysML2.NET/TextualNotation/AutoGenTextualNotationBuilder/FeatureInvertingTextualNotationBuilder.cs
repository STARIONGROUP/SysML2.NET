// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureInvertingTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="FeatureInvertingTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Core.Features.IFeatureInverting" /> element
    /// </summary>
    public static partial class FeatureInvertingTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedFeatureInverting
        /// <para>OwnedFeatureInverting:FeatureInverting=invertingFeature=[QualifiedName]|invertingFeature=OwnedFeatureChain{ownedRelatedElement+=invertingFeature}</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeatureInverting" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedFeatureInverting(SysML2.NET.Core.POCO.Core.Features.IFeatureInverting poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureInverting
        /// <para>FeatureInverting=('inverting'Identification?)?'inverse'(featureInverted=[QualifiedName]|featureInverted=OwnedFeatureChain{ownedRelatedElement+=featureInverted})'of'(invertingFeature=[QualifiedName]|ownedRelatedElement+=OwnedFeatureChain{ownedRelatedElement+=invertingFeature})RelationshipBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeatureInverting" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureInverting(SysML2.NET.Core.POCO.Core.Features.IFeatureInverting poco, StringBuilder stringBuilder)
        {
            // Group Element
            stringBuilder.Append("inverting ");
            // non Terminal : Identification; Found rule Identification:Element=('<'declaredShortName=NAME'>')?(declaredName=NAME)? 
            ElementTextualNotationBuilder.BuildIdentification(poco, stringBuilder);

            stringBuilder.Append("inverse ");
            // Group Element
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            stringBuilder.Append("of ");
            // Group Element
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            // non Terminal : RelationshipBody; Found rule RelationshipBody:Relationship=';'|'{'(ownedRelationship+=OwnedAnnotation)*'}' 
            RelationshipTextualNotationBuilder.BuildRelationshipBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
