// -------------------------------------------------------------------------------------------------
// <copyright file="SubclassificationTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="SubclassificationTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Core.Classifiers.ISubclassification" /> element
    /// </summary>
    public static partial class SubclassificationTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedSubclassification
        /// <para>OwnedSubclassification:Subclassification=superClassifier=[QualifiedName]</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Classifiers.ISubclassification" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedSubclassification(SysML2.NET.Core.POCO.Core.Classifiers.ISubclassification poco, StringBuilder stringBuilder)
        {
            // Assignment Element : superClassifier = SysML2.NET.CodeGenerator.Grammar.Model.ValueLiteralElement
            // If property superclassifier value is set, print SysML2.NET.CodeGenerator.Grammar.Model.ValueLiteralElement

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Subclassification
        /// <para>Subclassification=('specialization'Identification)?'subclassifier'subclassifier=[QualifiedName]SPECIALIZESsuperclassifier=[QualifiedName]RelationshipBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Classifiers.ISubclassification" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSubclassification(SysML2.NET.Core.POCO.Core.Classifiers.ISubclassification poco, StringBuilder stringBuilder)
        {
            // Group Element
            stringBuilder.Append("specialization ");
            // non Terminal : Identification; Found rule Identification:Element=('<'declaredShortName=NAME'>')?(declaredName=NAME)? 
            ElementTextualNotationBuilder.BuildIdentification(poco, stringBuilder);

            stringBuilder.Append("subclassifier ");
            // Assignment Element : subclassifier = SysML2.NET.CodeGenerator.Grammar.Model.ValueLiteralElement
            // If property subclassifier value is set, print SysML2.NET.CodeGenerator.Grammar.Model.ValueLiteralElement
            // non Terminal : SPECIALIZES; Found rule SPECIALIZES=':>'|'specializes' 
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            // Assignment Element : superclassifier = SysML2.NET.CodeGenerator.Grammar.Model.ValueLiteralElement
            // If property superclassifier value is set, print SysML2.NET.CodeGenerator.Grammar.Model.ValueLiteralElement
            // non Terminal : RelationshipBody; Found rule RelationshipBody:Relationship=';'|'{'(ownedRelationship+=OwnedAnnotation)*'}' 
            RelationshipTextualNotationBuilder.BuildRelationshipBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
