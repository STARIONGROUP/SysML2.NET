// -------------------------------------------------------------------------------------------------
// <copyright file="ClassifierTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="ClassifierTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Core.Classifiers.IClassifier" /> element
    /// </summary>
    public static partial class ClassifierTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule SubclassificationPart
        /// <para>SubclassificationPart:Classifier=SPECIALIZESownedRelationship+=OwnedSubclassification(','ownedRelationship+=OwnedSubclassification)*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Classifiers.IClassifier" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSubclassificationPart(SysML2.NET.Core.POCO.Core.Classifiers.IClassifier poco, StringBuilder stringBuilder)
        {
            // non Terminal : SPECIALIZES; Found rule SPECIALIZES=':>'|'specializes' 
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // Group Element
            stringBuilder.Append(", ");
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ClassifierDeclaration
        /// <para>ClassifierDeclaration:Classifier=(isSufficient?='all')?Identification(ownedRelationship+=OwnedMultiplicity)?(SuperclassingPart|ConjugationPart)?TypeRelationshipPart*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Classifiers.IClassifier" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildClassifierDeclaration(SysML2.NET.Core.POCO.Core.Classifiers.IClassifier poco, StringBuilder stringBuilder)
        {
            // Group Element
            // Assignment Element : isSufficient ?= SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement
            // If property isSufficient value is set, print SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement

            // non Terminal : Identification; Found rule Identification:Element=('<'declaredShortName=NAME'>')?(declaredName=NAME)? 
            ElementTextualNotationBuilder.BuildIdentification(poco, stringBuilder);
            // Group Element
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement

            // Group Element
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            // non Terminal : TypeRelationshipPart; Found rule TypeRelationshipPart:Type=DisjoiningPart|UnioningPart|IntersectingPart|DifferencingPart 
            TypeTextualNotationBuilder.BuildTypeRelationshipPart(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SuperclassingPart
        /// <para>SuperclassingPart:Classifier=SPECIALIZESownedRelationship+=OwnedSubclassification(','ownedRelationship+=OwnedSubclassification)*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Classifiers.IClassifier" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSuperclassingPart(SysML2.NET.Core.POCO.Core.Classifiers.IClassifier poco, StringBuilder stringBuilder)
        {
            // non Terminal : SPECIALIZES; Found rule SPECIALIZES=':>'|'specializes' 
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // Group Element
            stringBuilder.Append(", ");
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Classifier
        /// <para>Classifier=TypePrefix'classifier'ClassifierDeclarationTypeBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Classifiers.IClassifier" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildClassifier(SysML2.NET.Core.POCO.Core.Classifiers.IClassifier poco, StringBuilder stringBuilder)
        {
            // non Terminal : TypePrefix; Found rule TypePrefix:Type=(isAbstract?='abstract')?(ownedRelationship+=PrefixMetadataMember)* 
            TypeTextualNotationBuilder.BuildTypePrefix(poco, stringBuilder);
            stringBuilder.Append("classifier ");
            // non Terminal : ClassifierDeclaration; Found rule ClassifierDeclaration:Classifier=(isSufficient?='all')?Identification(ownedRelationship+=OwnedMultiplicity)?(SuperclassingPart|ConjugationPart)?TypeRelationshipPart* 
            BuildClassifierDeclaration(poco, stringBuilder);
            // non Terminal : TypeBody; Found rule TypeBody:Type=';'|'{'TypeBodyElement*'}' 
            TypeTextualNotationBuilder.BuildTypeBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
