// -------------------------------------------------------------------------------------------------
// <copyright file="UsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="UsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> element
    /// </summary>
    public static partial class UsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule UsageElement
        /// <para>UsageElement:Usage=NonOccurrenceUsageElement|OccurrenceUsageElement</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule RefPrefix
        /// <para>RefPrefix:Usage=(direction=FeatureDirection)?(isDerived?='derived')?(isAbstract?='abstract'|isVariation?='variation')?(isConstant?='constant')?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRefPrefix(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            // Group Element
            // Assignment Element : direction = SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property direction value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement

            // Group Element
            // Assignment Element : isDerived ?= SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement
            // If property isDerived value is set, print SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement

            // Group Element
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            // Group Element
            // Assignment Element : isConstant ?= SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement
            // If property isConstant value is set, print SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BasicUsagePrefix
        /// <para>BasicUsagePrefix:Usage=RefPrefix(isReference?='ref')?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBasicUsagePrefix(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : RefPrefix; Found rule RefPrefix:Usage=(direction=FeatureDirection)?(isDerived?='derived')?(isAbstract?='abstract'|isVariation?='variation')?(isConstant?='constant')? 
            BuildRefPrefix(poco, stringBuilder);
            // Group Element
            // Assignment Element : isReference ?= SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement
            // If property isReference value is set, print SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EndUsagePrefix
        /// <para>EndUsagePrefix:Usage=isEnd?='end'(ownedRelationship+=OwnedCrossFeatureMember)?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildEndUsagePrefix(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            // Assignment Element : isEnd ?= SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement
            // If property isEnd value is set, print SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement
            // Group Element
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule UsageExtensionKeyword
        /// <para>UsageExtensionKeyword:Usage=ownedRelationship+=PrefixMetadataMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUsageExtensionKeyword(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule UnextendedUsagePrefix
        /// <para>UnextendedUsagePrefix:Usage=EndUsagePrefix|BasicUsagePrefix</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUnextendedUsagePrefix(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule UsagePrefix
        /// <para>UsagePrefix:Usage=UnextendedUsagePrefixUsageExtensionKeyword*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUsagePrefix(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : UnextendedUsagePrefix; Found rule UnextendedUsagePrefix:Usage=EndUsagePrefix|BasicUsagePrefix 
            BuildUnextendedUsagePrefix(poco, stringBuilder);
            // non Terminal : UsageExtensionKeyword; Found rule UsageExtensionKeyword:Usage=ownedRelationship+=PrefixMetadataMember 
            BuildUsageExtensionKeyword(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule UsageDeclaration
        /// <para>UsageDeclaration:Usage=IdentificationFeatureSpecializationPart?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUsageDeclaration(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : Identification; Found rule Identification:Element=('<'declaredShortName=NAME'>')?(declaredName=NAME)? 
            ElementTextualNotationBuilder.BuildIdentification(poco, stringBuilder);
            // non Terminal : FeatureSpecializationPart; Found rule FeatureSpecializationPart:Feature=FeatureSpecialization+MultiplicityPart?FeatureSpecialization*|MultiplicityPartFeatureSpecialization* 
            FeatureTextualNotationBuilder.BuildFeatureSpecializationPart(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule UsageCompletion
        /// <para>UsageCompletion:Usage=ValuePart?UsageBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUsageCompletion(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : ValuePart; Found rule ValuePart:Feature=ownedRelationship+=FeatureValue 
            FeatureTextualNotationBuilder.BuildValuePart(poco, stringBuilder);
            // non Terminal : UsageBody; Found rule UsageBody:Usage=DefinitionBody 
            BuildUsageBody(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule UsageBody
        /// <para>UsageBody:Usage=DefinitionBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUsageBody(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : DefinitionBody; Found rule DefinitionBody:Type=';'|'{'DefinitionBodyItem*'}' 
            TypeTextualNotationBuilder.BuildDefinitionBody(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NonOccurrenceUsageElement
        /// <para>NonOccurrenceUsageElement:Usage=DefaultReferenceUsage|ReferenceUsage|AttributeUsage|EnumerationUsage|BindingConnectorAsUsage|SuccessionAsUsage|ExtendedUsage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNonOccurrenceUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OccurrenceUsageElement
        /// <para>OccurrenceUsageElement:Usage=StructureUsageElement|BehaviorUsageElement</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOccurrenceUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule StructureUsageElement
        /// <para>StructureUsageElement:Usage=OccurrenceUsage|IndividualUsage|PortionUsage|EventOccurrenceUsage|ItemUsage|PartUsage|ViewUsage|RenderingUsage|PortUsage|ConnectionUsage|InterfaceUsage|AllocationUsage|Message|FlowUsage|SuccessionFlowUsage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildStructureUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BehaviorUsageElement
        /// <para>BehaviorUsageElement:Usage=ActionUsage|CalculationUsage|StateUsage|ConstraintUsage|RequirementUsage|ConcernUsage|CaseUsage|AnalysisCaseUsage|VerificationCaseUsage|UseCaseUsage|ViewpointUsage|PerformActionUsage|ExhibitStateUsage|IncludeUseCaseUsage|AssertConstraintUsage|SatisfyRequirementUsage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBehaviorUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule VariantUsageElement
        /// <para>VariantUsageElement:Usage=VariantReference|ReferenceUsage|AttributeUsage|BindingConnectorAsUsage|SuccessionAsUsage|OccurrenceUsage|IndividualUsage|PortionUsage|EventOccurrenceUsage|ItemUsage|PartUsage|ViewUsage|RenderingUsage|PortUsage|ConnectionUsage|InterfaceUsage|AllocationUsage|Message|FlowUsage|SuccessionFlowUsage|BehaviorUsageElement</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildVariantUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceNonOccurrenceUsageElement
        /// <para>InterfaceNonOccurrenceUsageElement:Usage=ReferenceUsage|AttributeUsage|EnumerationUsage|BindingConnectorAsUsage|SuccessionAsUsage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInterfaceNonOccurrenceUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceOccurrenceUsageElement
        /// <para>InterfaceOccurrenceUsageElement:Usage=DefaultInterfaceEnd|StructureUsageElement|BehaviorUsageElement</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInterfaceOccurrenceUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionTargetSuccession
        /// <para>ActionTargetSuccession:Usage=(TargetSuccession|GuardedTargetSuccession|DefaultTargetSuccession)UsageBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionTargetSuccession(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            // Group Element
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            // non Terminal : UsageBody; Found rule UsageBody:Usage=DefinitionBody 
            BuildUsageBody(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ExtendedUsage
        /// <para>ExtendedUsage:Usage=UnextendedUsagePrefixUsageExtensionKeyword+Usage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildExtendedUsage(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : UnextendedUsagePrefix; Found rule UnextendedUsagePrefix:Usage=EndUsagePrefix|BasicUsagePrefix 
            BuildUnextendedUsagePrefix(poco, stringBuilder);
            // non Terminal : UsageExtensionKeyword; Found rule UsageExtensionKeyword:Usage=ownedRelationship+=PrefixMetadataMember 
            BuildUsageExtensionKeyword(poco, stringBuilder);
            // non Terminal : Usage; Found rule Usage=UsageDeclarationUsageCompletion 
            BuildUsage(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Usage
        /// <para>Usage=UsageDeclarationUsageCompletion</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUsage(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : UsageDeclaration; Found rule UsageDeclaration:Usage=IdentificationFeatureSpecializationPart? 
            BuildUsageDeclaration(poco, stringBuilder);
            // non Terminal : UsageCompletion; Found rule UsageCompletion:Usage=ValuePart?UsageBody 
            BuildUsageCompletion(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
