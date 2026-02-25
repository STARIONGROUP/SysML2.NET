// -------------------------------------------------------------------------------------------------
// <copyright file="SendActionUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="SendActionUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage" /> element
    /// </summary>
    public static partial class SendActionUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule SendNode
        /// <para>SendNode:SendActionUsage=OccurrenceUsagePrefixActionUsageDeclaration?'send'(ownedRelationship+=NodeParameterMemberSenderReceiverPart?|ownedRelationship+=EmptyParameterMemberSenderReceiverPart)?ActionBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSendNode(SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : OccurrenceUsagePrefix; Found rule OccurrenceUsagePrefix:OccurrenceUsage=BasicUsagePrefix(isIndividual?='individual')?(portionKind=PortionKind{isPortion=true})?UsageExtensionKeyword* 
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, stringBuilder);
            // non Terminal : ActionUsageDeclaration; Found rule ActionUsageDeclaration:ActionUsage=UsageDeclarationValuePart? 
            ActionUsageTextualNotationBuilder.BuildActionUsageDeclaration(poco, stringBuilder);
            stringBuilder.Append("send ");
            // Group Element
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            // non Terminal : ActionBody; Found rule ActionBody:Type=';'|'{'ActionBodyItem*'}' 
            TypeTextualNotationBuilder.BuildActionBody(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SendNodeDeclaration
        /// <para>SendNodeDeclaration:SendActionUsage=ActionNodeUsageDeclaration?'send'ownedRelationship+=NodeParameterMemberSenderReceiverPart?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSendNodeDeclaration(SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : ActionNodeUsageDeclaration; Found rule ActionNodeUsageDeclaration:ActionUsage='action'UsageDeclaration? 
            ActionUsageTextualNotationBuilder.BuildActionNodeUsageDeclaration(poco, stringBuilder);
            stringBuilder.Append("send ");
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // non Terminal : SenderReceiverPart; Found rule SenderReceiverPart:SendActionUsage='via'ownedRelationship+=NodeParameterMember('to'ownedRelationship+=NodeParameterMember)?|ownedRelationship+=EmptyParameterMember'to'ownedRelationship+=NodeParameterMember 
            BuildSenderReceiverPart(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SenderReceiverPart
        /// <para>SenderReceiverPart:SendActionUsage='via'ownedRelationship+=NodeParameterMember('to'ownedRelationship+=NodeParameterMember)?|ownedRelationship+=EmptyParameterMember'to'ownedRelationship+=NodeParameterMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSenderReceiverPart(SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule StateSendActionUsage
        /// <para>StateSendActionUsage:SendActionUsage=SendNodeDeclarationActionBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildStateSendActionUsage(SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : SendNodeDeclaration; Found rule SendNodeDeclaration:SendActionUsage=ActionNodeUsageDeclaration?'send'ownedRelationship+=NodeParameterMemberSenderReceiverPart? 
            BuildSendNodeDeclaration(poco, stringBuilder);
            // non Terminal : ActionBody; Found rule ActionBody:Type=';'|'{'ActionBodyItem*'}' 
            TypeTextualNotationBuilder.BuildActionBody(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TransitionSendActionUsage
        /// <para>TransitionSendActionUsage:SendActionUsage=SendNodeDeclaration('{'ActionBodyItem*'}')?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTransitionSendActionUsage(SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : SendNodeDeclaration; Found rule SendNodeDeclaration:SendActionUsage=ActionNodeUsageDeclaration?'send'ownedRelationship+=NodeParameterMemberSenderReceiverPart? 
            BuildSendNodeDeclaration(poco, stringBuilder);
            // Group Element
            stringBuilder.Append("{ ");
            // non Terminal : ActionBodyItem; Found rule ActionBodyItem:Type=NonBehaviorBodyItem|ownedRelationship+=InitialNodeMember(ownedRelationship+=ActionTargetSuccessionMember)*|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=ActionBehaviorMember(ownedRelationship+=ActionTargetSuccessionMember)*|ownedRelationship+=GuardedSuccessionMember 
            TypeTextualNotationBuilder.BuildActionBodyItem(poco, stringBuilder);
            stringBuilder.Append("} ");


        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
