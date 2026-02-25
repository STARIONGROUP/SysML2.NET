// -------------------------------------------------------------------------------------------------
// <copyright file="ActionUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="ActionUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Actions.IActionUsage" /> element
    /// </summary>
    public static partial class ActionUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule ActionUsageDeclaration
        /// <para>ActionUsageDeclaration:ActionUsage=UsageDeclarationValuePart?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IActionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionUsageDeclaration(SysML2.NET.Core.POCO.Systems.Actions.IActionUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : UsageDeclaration; Found rule UsageDeclaration:Usage=IdentificationFeatureSpecializationPart? 
            UsageTextualNotationBuilder.BuildUsageDeclaration(poco, stringBuilder);
            // non Terminal : ValuePart; Found rule ValuePart:Feature=ownedRelationship+=FeatureValue 
            FeatureTextualNotationBuilder.BuildValuePart(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionNode
        /// <para>ActionNode:ActionUsage=ControlNode|SendNode|AcceptNode|AssignmentNode|TerminateNode|IfNode|WhileLoopNode|ForLoopNode</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IActionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionNode(SysML2.NET.Core.POCO.Systems.Actions.IActionUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionNodeUsageDeclaration
        /// <para>ActionNodeUsageDeclaration:ActionUsage='action'UsageDeclaration?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IActionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionNodeUsageDeclaration(SysML2.NET.Core.POCO.Systems.Actions.IActionUsage poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append("action ");
            // non Terminal : UsageDeclaration; Found rule UsageDeclaration:Usage=IdentificationFeatureSpecializationPart? 
            UsageTextualNotationBuilder.BuildUsageDeclaration(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionNodePrefix
        /// <para>ActionNodePrefix:ActionUsage=OccurrenceUsagePrefixActionNodeUsageDeclaration?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IActionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionNodePrefix(SysML2.NET.Core.POCO.Systems.Actions.IActionUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : OccurrenceUsagePrefix; Found rule OccurrenceUsagePrefix:OccurrenceUsage=BasicUsagePrefix(isIndividual?='individual')?(portionKind=PortionKind{isPortion=true})?UsageExtensionKeyword* 
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, stringBuilder);
            // non Terminal : ActionNodeUsageDeclaration; Found rule ActionNodeUsageDeclaration:ActionUsage='action'UsageDeclaration? 
            BuildActionNodeUsageDeclaration(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule AssignmentNodeDeclaration
        /// <para>AssignmentNodeDeclaration:ActionUsage=(ActionNodeUsageDeclaration)?'assign'ownedRelationship+=AssignmentTargetMemberownedRelationship+=FeatureChainMember':='ownedRelationship+=NodeParameterMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IActionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildAssignmentNodeDeclaration(SysML2.NET.Core.POCO.Systems.Actions.IActionUsage poco, StringBuilder stringBuilder)
        {
            // Group Element
            // non Terminal : ActionNodeUsageDeclaration; Found rule ActionNodeUsageDeclaration:ActionUsage='action'UsageDeclaration? 
            BuildActionNodeUsageDeclaration(poco, stringBuilder);

            stringBuilder.Append("assign ");
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            stringBuilder.Append(":= ");
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionBodyParameter
        /// <para>ActionBodyParameter:ActionUsage=('action'UsageDeclaration?)?'{'ActionBodyItem*'}'</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IActionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionBodyParameter(SysML2.NET.Core.POCO.Systems.Actions.IActionUsage poco, StringBuilder stringBuilder)
        {
            // Group Element
            stringBuilder.Append("action ");
            // non Terminal : UsageDeclaration; Found rule UsageDeclaration:Usage=IdentificationFeatureSpecializationPart? 
            UsageTextualNotationBuilder.BuildUsageDeclaration(poco, stringBuilder);

            stringBuilder.Append("{ ");
            // non Terminal : ActionBodyItem; Found rule ActionBodyItem:Type=NonBehaviorBodyItem|ownedRelationship+=InitialNodeMember(ownedRelationship+=ActionTargetSuccessionMember)*|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=ActionBehaviorMember(ownedRelationship+=ActionTargetSuccessionMember)*|ownedRelationship+=GuardedSuccessionMember 
            TypeTextualNotationBuilder.BuildActionBodyItem(poco, stringBuilder);
            stringBuilder.Append("} ");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule StateActionUsage
        /// <para>StateActionUsage:ActionUsage=EmptyActionUsage';'|StatePerformActionUsage|StateAcceptActionUsage|StateSendActionUsage|StateAssignmentActionUsage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IActionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildStateActionUsage(SysML2.NET.Core.POCO.Systems.Actions.IActionUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EmptyActionUsage
        /// <para>EmptyActionUsage:ActionUsage={}</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IActionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildEmptyActionUsage(SysML2.NET.Core.POCO.Systems.Actions.IActionUsage poco, StringBuilder stringBuilder)
        {

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EffectBehaviorUsage
        /// <para>EffectBehaviorUsage:ActionUsage=EmptyActionUsage|TransitionPerformActionUsage|TransitionAcceptActionUsage|TransitionSendActionUsage|TransitionAssignmentActionUsage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IActionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildEffectBehaviorUsage(SysML2.NET.Core.POCO.Systems.Actions.IActionUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionUsage
        /// <para>ActionUsage=OccurrenceUsagePrefix'action'ActionUsageDeclarationActionBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IActionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionUsage(SysML2.NET.Core.POCO.Systems.Actions.IActionUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : OccurrenceUsagePrefix; Found rule OccurrenceUsagePrefix:OccurrenceUsage=BasicUsagePrefix(isIndividual?='individual')?(portionKind=PortionKind{isPortion=true})?UsageExtensionKeyword* 
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, stringBuilder);
            stringBuilder.Append("action ");
            // non Terminal : ActionUsageDeclaration; Found rule ActionUsageDeclaration:ActionUsage=UsageDeclarationValuePart? 
            BuildActionUsageDeclaration(poco, stringBuilder);
            // non Terminal : ActionBody; Found rule ActionBody:Type=';'|'{'ActionBodyItem*'}' 
            TypeTextualNotationBuilder.BuildActionBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
