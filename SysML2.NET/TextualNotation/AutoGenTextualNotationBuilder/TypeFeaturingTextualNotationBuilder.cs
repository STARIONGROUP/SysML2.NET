// -------------------------------------------------------------------------------------------------
// <copyright file="TypeFeaturingTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="TypeFeaturingTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Core.Features.ITypeFeaturing" /> element
    /// </summary>
    public static partial class TypeFeaturingTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedTypeFeaturing
        /// <para>OwnedTypeFeaturing:TypeFeaturing=featuringType=[QualifiedName]</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.ITypeFeaturing" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedTypeFeaturing(SysML2.NET.Core.POCO.Core.Features.ITypeFeaturing poco, StringBuilder stringBuilder)
        {
            // Assignment Element : featuringType = SysML2.NET.CodeGenerator.Grammar.Model.ValueLiteralElement
            // If property featuringType value is set, print SysML2.NET.CodeGenerator.Grammar.Model.ValueLiteralElement

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeFeaturing
        /// <para>TypeFeaturing='featuring'(Identification'of')?featureOfType=[QualifiedName]'by'featuringType=[QualifiedName]RelationshipBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.ITypeFeaturing" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypeFeaturing(SysML2.NET.Core.POCO.Core.Features.ITypeFeaturing poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append("featuring ");
            // Group Element
            // non Terminal : Identification; Found rule Identification:Element=('<'declaredShortName=NAME'>')?(declaredName=NAME)? 
            ElementTextualNotationBuilder.BuildIdentification(poco, stringBuilder);
            stringBuilder.Append("of ");

            // Assignment Element : featureOfType = SysML2.NET.CodeGenerator.Grammar.Model.ValueLiteralElement
            // If property featureOfType value is set, print SysML2.NET.CodeGenerator.Grammar.Model.ValueLiteralElement
            stringBuilder.Append("by ");
            // Assignment Element : featuringType = SysML2.NET.CodeGenerator.Grammar.Model.ValueLiteralElement
            // If property featuringType value is set, print SysML2.NET.CodeGenerator.Grammar.Model.ValueLiteralElement
            // non Terminal : RelationshipBody; Found rule RelationshipBody:Relationship=';'|'{'(ownedRelationship+=OwnedAnnotation)*'}' 
            RelationshipTextualNotationBuilder.BuildRelationshipBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
