// -------------------------------------------------------------------------------------------------
// <copyright file="FlowUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="FlowUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Flows.IFlowUsage" /> element
    /// </summary>
    public static partial class FlowUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule Message
        /// <para>Message:FlowUsage=OccurrenceUsagePrefix'message'MessageDeclarationDefinitionBody{isAbstract=true}</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Flows.IFlowUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMessage(SysML2.NET.Core.POCO.Systems.Flows.IFlowUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : OccurrenceUsagePrefix; Found rule OccurrenceUsagePrefix:OccurrenceUsage=BasicUsagePrefix(isIndividual?='individual')?(portionKind=PortionKind{isPortion=true})?UsageExtensionKeyword* 
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, stringBuilder);
            stringBuilder.Append("message ");
            // non Terminal : MessageDeclaration; Found rule MessageDeclaration:FlowUsage=UsageDeclarationValuePart?('of'ownedRelationship+=FlowPayloadFeatureMember)?('from'ownedRelationship+=MessageEventMember'to'ownedRelationship+=MessageEventMember)?|ownedRelationship+=MessageEventMember'to'ownedRelationship+=MessageEventMember 
            BuildMessageDeclaration(poco, stringBuilder);
            // non Terminal : DefinitionBody; Found rule DefinitionBody:Type=';'|'{'DefinitionBodyItem*'}' 
            TypeTextualNotationBuilder.BuildDefinitionBody(poco, stringBuilder);
            // Assignment Element : isAbstract = true

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MessageDeclaration
        /// <para>MessageDeclaration:FlowUsage=UsageDeclarationValuePart?('of'ownedRelationship+=FlowPayloadFeatureMember)?('from'ownedRelationship+=MessageEventMember'to'ownedRelationship+=MessageEventMember)?|ownedRelationship+=MessageEventMember'to'ownedRelationship+=MessageEventMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Flows.IFlowUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMessageDeclaration(SysML2.NET.Core.POCO.Systems.Flows.IFlowUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FlowDeclaration
        /// <para>FlowDeclaration:FlowUsage=UsageDeclarationValuePart?('of'ownedRelationship+=FlowPayloadFeatureMember)?('from'ownedRelationship+=FlowEndMember'to'ownedRelationship+=FlowEndMember)?|ownedRelationship+=FlowEndMember'to'ownedRelationship+=FlowEndMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Flows.IFlowUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFlowDeclaration(SysML2.NET.Core.POCO.Systems.Flows.IFlowUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FlowUsage
        /// <para>FlowUsage=OccurrenceUsagePrefix'flow'FlowDeclarationDefinitionBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Flows.IFlowUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFlowUsage(SysML2.NET.Core.POCO.Systems.Flows.IFlowUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : OccurrenceUsagePrefix; Found rule OccurrenceUsagePrefix:OccurrenceUsage=BasicUsagePrefix(isIndividual?='individual')?(portionKind=PortionKind{isPortion=true})?UsageExtensionKeyword* 
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, stringBuilder);
            stringBuilder.Append("flow ");
            // non Terminal : FlowDeclaration; Found rule FlowDeclaration:FlowUsage=UsageDeclarationValuePart?('of'ownedRelationship+=FlowPayloadFeatureMember)?('from'ownedRelationship+=FlowEndMember'to'ownedRelationship+=FlowEndMember)?|ownedRelationship+=FlowEndMember'to'ownedRelationship+=FlowEndMember 
            BuildFlowDeclaration(poco, stringBuilder);
            // non Terminal : DefinitionBody; Found rule DefinitionBody:Type=';'|'{'DefinitionBodyItem*'}' 
            TypeTextualNotationBuilder.BuildDefinitionBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
