// -------------------------------------------------------------------------------------------------
// <copyright file="ReferenceUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="ReferenceUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> element
    /// </summary>
    public static partial class ReferenceUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedCrossFeature
        /// <para>OwnedCrossFeature:ReferenceUsage=BasicUsagePrefixUsageDeclaration</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedCrossFeature(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : BasicUsagePrefix; Found rule BasicUsagePrefix:Usage=RefPrefix(isReference?='ref')? 
            UsageTextualNotationBuilder.BuildBasicUsagePrefix(poco, stringBuilder);
            // non Terminal : UsageDeclaration; Found rule UsageDeclaration:Usage=IdentificationFeatureSpecializationPart? 
            UsageTextualNotationBuilder.BuildUsageDeclaration(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule DefaultReferenceUsage
        /// <para>DefaultReferenceUsage:ReferenceUsage=RefPrefixUsage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDefaultReferenceUsage(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : RefPrefix; Found rule RefPrefix:Usage=(direction=FeatureDirection)?(isDerived?='derived')?(isAbstract?='abstract'|isVariation?='variation')?(isConstant?='constant')? 
            UsageTextualNotationBuilder.BuildRefPrefix(poco, stringBuilder);
            // non Terminal : Usage; Found rule Usage=UsageDeclarationUsageCompletion 
            UsageTextualNotationBuilder.BuildUsage(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule VariantReference
        /// <para>VariantReference:ReferenceUsage=ownedRelationship+=OwnedReferenceSubsettingFeatureSpecialization*UsageBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildVariantReference(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // non Terminal : FeatureSpecialization; Found rule FeatureSpecialization:Feature=Typings|Subsettings|References|Crosses|Redefinitions 
            FeatureTextualNotationBuilder.BuildFeatureSpecialization(poco, stringBuilder);
            // non Terminal : UsageBody; Found rule UsageBody:Usage=DefinitionBody 
            UsageTextualNotationBuilder.BuildUsageBody(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SourceEnd
        /// <para>SourceEnd:ReferenceUsage=(ownedRelationship+=OwnedMultiplicity)?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSourceEnd(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            // Group Element
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ConnectorEnd
        /// <para>ConnectorEnd:ReferenceUsage=(ownedRelationship+=OwnedCrossMultiplicityMember)?(declaredName=NAMEREFERENCES)?ownedRelationship+=OwnedReferenceSubsetting</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildConnectorEnd(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            // Group Element
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement

            // Group Element
            // Assignment Element : declaredName = SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property declaredName value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // non Terminal : REFERENCES; Found rule REFERENCES='::>'|'references' 
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");

            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FlowFeature
        /// <para>FlowFeature:ReferenceUsage=ownedRelationship+=FlowFeatureRedefinition</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFlowFeature(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PayloadParameter
        /// <para>PayloadParameter:ReferenceUsage=PayloadFeature|IdentificationPayloadFeatureSpecializationPart?TriggerValuePart</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPayloadParameter(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NodeParameter
        /// <para>NodeParameter:ReferenceUsage=ownedRelationship+=FeatureBinding</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNodeParameter(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EmptyUsage
        /// <para>EmptyUsage:ReferenceUsage={}</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildEmptyUsage(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule AssignmentTargetParameter
        /// <para>AssignmentTargetParameter:ReferenceUsage=(ownedRelationship+=AssignmentTargetBinding'.')?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildAssignmentTargetParameter(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            // Group Element
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            stringBuilder.Append(". ");


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ForVariableDeclaration
        /// <para>ForVariableDeclaration:ReferenceUsage=UsageDeclaration</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildForVariableDeclaration(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : UsageDeclaration; Found rule UsageDeclaration:Usage=IdentificationFeatureSpecializationPart? 
            UsageTextualNotationBuilder.BuildUsageDeclaration(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EmptyFeature
        /// <para>EmptyFeature:ReferenceUsage={}</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildEmptyFeature(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SubjectUsage
        /// <para>SubjectUsage:ReferenceUsage='subject'UsageExtensionKeyword*Usage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSubjectUsage(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append("subject ");
            // non Terminal : UsageExtensionKeyword; Found rule UsageExtensionKeyword:Usage=ownedRelationship+=PrefixMetadataMember 
            UsageTextualNotationBuilder.BuildUsageExtensionKeyword(poco, stringBuilder);
            // non Terminal : Usage; Found rule Usage=UsageDeclarationUsageCompletion 
            UsageTextualNotationBuilder.BuildUsage(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SatisfactionParameter
        /// <para>SatisfactionParameter:ReferenceUsage=ownedRelationship+=SatisfactionFeatureValue</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSatisfactionParameter(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataBodyUsage
        /// <para>MetadataBodyUsage:ReferenceUsage='ref'?(':&gt;&gt;'|'redefines')?ownedRelationship+=OwnedRedefinitionFeatureSpecializationPart?ValuePart?MetadataBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataBodyUsage(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append("ref ");
            // Group Element
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // non Terminal : FeatureSpecializationPart; Found rule FeatureSpecializationPart:Feature=FeatureSpecialization+MultiplicityPart?FeatureSpecialization*|MultiplicityPartFeatureSpecialization* 
            FeatureTextualNotationBuilder.BuildFeatureSpecializationPart(poco, stringBuilder);
            // non Terminal : ValuePart; Found rule ValuePart:Feature=ownedRelationship+=FeatureValue 
            FeatureTextualNotationBuilder.BuildValuePart(poco, stringBuilder);
            // non Terminal : MetadataBody; Found rule MetadataBody:Type=';'|'{'(ownedRelationship+=DefinitionMember|ownedRelationship+=MetadataBodyUsageMember|ownedRelationship+=AliasMember|ownedRelationship+=Import)*'}' 
            TypeTextualNotationBuilder.BuildMetadataBody(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ReferenceUsage
        /// <para>ReferenceUsage=(EndUsagePrefix|RefPrefix)'ref'Usage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildReferenceUsage(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            // Group Element
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            stringBuilder.Append("ref ");
            // non Terminal : Usage; Found rule Usage=UsageDeclarationUsageCompletion 
            UsageTextualNotationBuilder.BuildUsage(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
