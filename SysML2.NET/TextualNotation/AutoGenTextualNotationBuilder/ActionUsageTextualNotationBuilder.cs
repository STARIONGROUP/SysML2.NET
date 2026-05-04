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
    using System.Linq;
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
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionUsageDeclaration(SysML2.NET.Core.POCO.Systems.Actions.IActionUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            UsageTextualNotationBuilder.BuildUsageDeclaration(poco, writerContext, stringBuilder);

            if (poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.Direction.HasValue || poco.IsDerived || poco.IsAbstract || poco.IsConstant || poco.IsOrdered || poco.IsEnd || poco.importedMembership.Count != 0 || poco.IsComposite || poco.IsPortion || poco.IsVariable || poco.IsSufficient || poco.unioningType.Count != 0 || poco.intersectingType.Count != 0 || poco.differencingType.Count != 0 || poco.featuringType.Count != 0 || poco.ownedTypeFeaturing.Count != 0)
            {
                FeatureTextualNotationBuilder.BuildValuePart(poco, writerContext, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionNode
        /// <para>ActionNode:ActionUsage=ControlNode|SendNode|AcceptNode|AssignmentNode|TerminateNode|IfNode|WhileLoopNode|ForLoopNode</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IActionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionNode(SysML2.NET.Core.POCO.Systems.Actions.IActionUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Systems.Actions.IWhileLoopActionUsage pocoWhileLoopActionUsage:
                    WhileLoopActionUsageTextualNotationBuilder.BuildWhileLoopNode(pocoWhileLoopActionUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.IForLoopActionUsage pocoForLoopActionUsage:
                    ForLoopActionUsageTextualNotationBuilder.BuildForLoopNode(pocoForLoopActionUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.IControlNode pocoControlNode:
                    ControlNodeTextualNotationBuilder.BuildControlNode(pocoControlNode, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage pocoSendActionUsage:
                    SendActionUsageTextualNotationBuilder.BuildSendNode(pocoSendActionUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage pocoAcceptActionUsage:
                    AcceptActionUsageTextualNotationBuilder.BuildAcceptNode(pocoAcceptActionUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.IAssignmentActionUsage pocoAssignmentActionUsage:
                    AssignmentActionUsageTextualNotationBuilder.BuildAssignmentNode(pocoAssignmentActionUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.ITerminateActionUsage pocoTerminateActionUsage:
                    TerminateActionUsageTextualNotationBuilder.BuildTerminateNode(pocoTerminateActionUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.IIfActionUsage pocoIfActionUsage:
                    IfActionUsageTextualNotationBuilder.BuildIfNode(pocoIfActionUsage, writerContext, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionNodeUsageDeclaration
        /// <para>ActionNodeUsageDeclaration:ActionUsage='action'UsageDeclaration?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IActionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionNodeUsageDeclaration(SysML2.NET.Core.POCO.Systems.Actions.IActionUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            stringBuilder.Append("action ");

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || poco.IsOrdered)
            {
                UsageTextualNotationBuilder.BuildUsageDeclaration(poco, writerContext, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionNodePrefix
        /// <para>ActionNodePrefix:ActionUsage=OccurrenceUsagePrefixActionNodeUsageDeclaration?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IActionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionNodePrefix(SysML2.NET.Core.POCO.Systems.Actions.IActionUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, writerContext, stringBuilder);

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || poco.IsOrdered)
            {
                BuildActionNodeUsageDeclaration(poco, writerContext, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule AssignmentNodeDeclaration
        /// <para>AssignmentNodeDeclaration:ActionUsage=(ActionNodeUsageDeclaration)?'assign'ownedRelationship+=AssignmentTargetMemberownedRelationship+=FeatureChainMember':='ownedRelationship+=NodeParameterMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IActionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildAssignmentNodeDeclaration(SysML2.NET.Core.POCO.Systems.Actions.IActionUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || poco.IsOrdered)
            {
                BuildActionNodeUsageDeclaration(poco, writerContext, stringBuilder);
                stringBuilder.Append(' ');
            }

            stringBuilder.Append("assign ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                {
                    ParameterMembershipTextualNotationBuilder.BuildAssignmentTargetMember(elementAsParameterMembership, writerContext, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IMembership elementAsMembership)
                {
                    MembershipTextualNotationBuilder.BuildFeatureChainMember(elementAsMembership, writerContext, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            stringBuilder.Append(":= ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                {
                    ParameterMembershipTextualNotationBuilder.BuildNodeParameterMember(elementAsParameterMembership, writerContext, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionBodyParameter
        /// <para>ActionBodyParameter:ActionUsage=('action'UsageDeclaration?)?'{'ActionBodyItem*'}'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IActionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionBodyParameter(SysML2.NET.Core.POCO.Systems.Actions.IActionUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || poco.IsOrdered)
            {
                stringBuilder.Append("action ");

                if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || poco.IsOrdered)
                {
                    UsageTextualNotationBuilder.BuildUsageDeclaration(poco, writerContext, stringBuilder);
                }
            }

            stringBuilder.Append(' ');
            stringBuilder.AppendLine("{");
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            while (ownedRelationshipCursor.Current != null)
            {
                TypeTextualNotationBuilder.BuildActionBodyItem(poco, writerContext, stringBuilder);
            }

            stringBuilder.AppendLine("}");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule StateActionUsage
        /// <para>StateActionUsage:ActionUsage=EmptyActionUsage';'|StatePerformActionUsage|StateAcceptActionUsage|StateSendActionUsage|StateAssignmentActionUsage</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IActionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildStateActionUsage(SysML2.NET.Core.POCO.Systems.Actions.IActionUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage pocoPerformActionUsage:
                    PerformActionUsageTextualNotationBuilder.BuildStatePerformActionUsage(pocoPerformActionUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage pocoAcceptActionUsage:
                    AcceptActionUsageTextualNotationBuilder.BuildStateAcceptActionUsage(pocoAcceptActionUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage pocoSendActionUsage:
                    SendActionUsageTextualNotationBuilder.BuildStateSendActionUsage(pocoSendActionUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.IAssignmentActionUsage pocoAssignmentActionUsage:
                    AssignmentActionUsageTextualNotationBuilder.BuildStateAssignmentActionUsage(pocoAssignmentActionUsage, writerContext, stringBuilder);
                    break;
                default:
                    BuildEmptyActionUsage(poco, writerContext, stringBuilder);
                    stringBuilder.AppendLine(";");
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EmptyActionUsage
        /// <para>EmptyActionUsage:ActionUsage={}</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IActionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildEmptyActionUsage(SysML2.NET.Core.POCO.Systems.Actions.IActionUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EffectBehaviorUsage
        /// <para>EffectBehaviorUsage:ActionUsage=EmptyActionUsage|TransitionPerformActionUsage|TransitionAcceptActionUsage|TransitionSendActionUsage|TransitionAssignmentActionUsage</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IActionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildEffectBehaviorUsage(SysML2.NET.Core.POCO.Systems.Actions.IActionUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage pocoPerformActionUsage:
                    PerformActionUsageTextualNotationBuilder.BuildTransitionPerformActionUsage(pocoPerformActionUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage pocoAcceptActionUsage:
                    AcceptActionUsageTextualNotationBuilder.BuildTransitionAcceptActionUsage(pocoAcceptActionUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage pocoSendActionUsage:
                    SendActionUsageTextualNotationBuilder.BuildTransitionSendActionUsage(pocoSendActionUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.IAssignmentActionUsage pocoAssignmentActionUsage:
                    AssignmentActionUsageTextualNotationBuilder.BuildTransitionAssignmentActionUsage(pocoAssignmentActionUsage, writerContext, stringBuilder);
                    break;
                default:
                    BuildEmptyActionUsage(poco, writerContext, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionUsage
        /// <para>ActionUsage=OccurrenceUsagePrefix'action'ActionUsageDeclarationActionBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IActionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionUsage(SysML2.NET.Core.POCO.Systems.Actions.IActionUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, writerContext, stringBuilder);
            stringBuilder.Append("action ");
            BuildActionUsageDeclaration(poco, writerContext, stringBuilder);
            TypeTextualNotationBuilder.BuildActionBody(poco, writerContext, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
