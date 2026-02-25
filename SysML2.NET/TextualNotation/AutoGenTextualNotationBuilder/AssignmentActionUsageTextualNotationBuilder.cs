// -------------------------------------------------------------------------------------------------
// <copyright file="AssignmentActionUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="AssignmentActionUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Actions.IAssignmentActionUsage" /> element
    /// </summary>
    public static partial class AssignmentActionUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule AssignmentNode
        /// <para>AssignmentNode:AssignmentActionUsage=OccurrenceUsagePrefixAssignmentNodeDeclarationActionBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IAssignmentActionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildAssignmentNode(SysML2.NET.Core.POCO.Systems.Actions.IAssignmentActionUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : OccurrenceUsagePrefix; Found rule OccurrenceUsagePrefix:OccurrenceUsage=BasicUsagePrefix(isIndividual?='individual')?(portionKind=PortionKind{isPortion=true})?UsageExtensionKeyword* 
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, stringBuilder);
            // non Terminal : AssignmentNodeDeclaration; Found rule AssignmentNodeDeclaration:ActionUsage=(ActionNodeUsageDeclaration)?'assign'ownedRelationship+=AssignmentTargetMemberownedRelationship+=FeatureChainMember':='ownedRelationship+=NodeParameterMember 
            ActionUsageTextualNotationBuilder.BuildAssignmentNodeDeclaration(poco, stringBuilder);
            // non Terminal : ActionBody; Found rule ActionBody:Type=';'|'{'ActionBodyItem*'}' 
            TypeTextualNotationBuilder.BuildActionBody(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule StateAssignmentActionUsage
        /// <para>StateAssignmentActionUsage:AssignmentActionUsage=AssignmentNodeDeclarationActionBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IAssignmentActionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildStateAssignmentActionUsage(SysML2.NET.Core.POCO.Systems.Actions.IAssignmentActionUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : AssignmentNodeDeclaration; Found rule AssignmentNodeDeclaration:ActionUsage=(ActionNodeUsageDeclaration)?'assign'ownedRelationship+=AssignmentTargetMemberownedRelationship+=FeatureChainMember':='ownedRelationship+=NodeParameterMember 
            ActionUsageTextualNotationBuilder.BuildAssignmentNodeDeclaration(poco, stringBuilder);
            // non Terminal : ActionBody; Found rule ActionBody:Type=';'|'{'ActionBodyItem*'}' 
            TypeTextualNotationBuilder.BuildActionBody(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TransitionAssignmentActionUsage
        /// <para>TransitionAssignmentActionUsage:AssignmentActionUsage=AssignmentNodeDeclaration('{'ActionBodyItem*'}')?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IAssignmentActionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTransitionAssignmentActionUsage(SysML2.NET.Core.POCO.Systems.Actions.IAssignmentActionUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : AssignmentNodeDeclaration; Found rule AssignmentNodeDeclaration:ActionUsage=(ActionNodeUsageDeclaration)?'assign'ownedRelationship+=AssignmentTargetMemberownedRelationship+=FeatureChainMember':='ownedRelationship+=NodeParameterMember 
            ActionUsageTextualNotationBuilder.BuildAssignmentNodeDeclaration(poco, stringBuilder);
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
