// -------------------------------------------------------------------------------------------------
// <copyright file="ConjugationTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="ConjugationTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Core.Types.IConjugation" /> element
    /// </summary>
    public static partial class ConjugationTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedConjugation
        /// <para>OwnedConjugation:Conjugation=originalType=[QualifiedName]|originalType=FeatureChain{ownedRelatedElement+=originalType}</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IConjugation" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedConjugation(SysML2.NET.Core.POCO.Core.Types.IConjugation poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Conjugation
        /// <para>Conjugation=('conjugation'Identification)?'conjugate'(conjugatedType=[QualifiedName]|conjugatedType=FeatureChain{ownedRelatedElement+=conjugatedType})CONJUGATES(originalType=[QualifiedName]|originalType=FeatureChain{ownedRelatedElement+=originalType})RelationshipBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IConjugation" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildConjugation(SysML2.NET.Core.POCO.Core.Types.IConjugation poco, StringBuilder stringBuilder)
        {
            // Group Element
            stringBuilder.Append("conjugation ");
            // non Terminal : Identification; Found rule Identification:Element=('<'declaredShortName=NAME'>')?(declaredName=NAME)? 
            ElementTextualNotationBuilder.BuildIdentification(poco, stringBuilder);

            stringBuilder.Append("conjugate ");
            // Group Element
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            // non Terminal : CONJUGATES; Found rule CONJUGATES='~'|'conjugates' 
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
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
