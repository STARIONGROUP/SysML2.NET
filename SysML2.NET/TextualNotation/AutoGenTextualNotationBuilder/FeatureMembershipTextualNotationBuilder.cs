// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureMembershipTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="FeatureMembershipTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> element
    /// </summary>
    public static partial class FeatureMembershipTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule NonOccurrenceUsageMember
        /// <para>NonOccurrenceUsageMember:FeatureMembership=MemberPrefixownedRelatedElement+=NonOccurrenceUsageElement</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNonOccurrenceUsageMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, writerContext, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage elementAsUsage)
                {
                    UsageTextualNotationBuilder.BuildNonOccurrenceUsageElement(elementAsUsage, writerContext, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OccurrenceUsageMember
        /// <para>OccurrenceUsageMember:FeatureMembership=MemberPrefixownedRelatedElement+=OccurrenceUsageElement</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOccurrenceUsageMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, writerContext, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage elementAsUsage)
                {
                    UsageTextualNotationBuilder.BuildOccurrenceUsageElement(elementAsUsage, writerContext, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule StructureUsageMember
        /// <para>StructureUsageMember:FeatureMembership=MemberPrefixownedRelatedElement+=StructureUsageElement</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildStructureUsageMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, writerContext, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage elementAsUsage)
                {
                    UsageTextualNotationBuilder.BuildStructureUsageElement(elementAsUsage, writerContext, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BehaviorUsageMember
        /// <para>BehaviorUsageMember:FeatureMembership=MemberPrefixownedRelatedElement+=BehaviorUsageElement</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBehaviorUsageMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, writerContext, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage elementAsUsage)
                {
                    UsageTextualNotationBuilder.BuildBehaviorUsageElement(elementAsUsage, writerContext, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SourceSuccessionMember
        /// <para>SourceSuccessionMember:FeatureMembership='then'ownedRelatedElement+=SourceSuccession</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSourceSuccessionMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            stringBuilder.Append("then ");

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.Connections.ISuccessionAsUsage elementAsSuccessionAsUsage)
                {
                    SuccessionAsUsageTextualNotationBuilder.BuildSourceSuccession(elementAsSuccessionAsUsage, writerContext, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceNonOccurrenceUsageMember
        /// <para>InterfaceNonOccurrenceUsageMember:FeatureMembership=MemberPrefixownedRelatedElement+=InterfaceNonOccurrenceUsageElement</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInterfaceNonOccurrenceUsageMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, writerContext, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage elementAsUsage)
                {
                    UsageTextualNotationBuilder.BuildInterfaceNonOccurrenceUsageElement(elementAsUsage, writerContext, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceOccurrenceUsageMember
        /// <para>InterfaceOccurrenceUsageMember:FeatureMembership=MemberPrefixownedRelatedElement+=InterfaceOccurrenceUsageElement</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInterfaceOccurrenceUsageMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, writerContext, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage elementAsUsage)
                {
                    UsageTextualNotationBuilder.BuildInterfaceOccurrenceUsageElement(elementAsUsage, writerContext, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FlowPayloadFeatureMember
        /// <para>FlowPayloadFeatureMember:FeatureMembership=ownedRelatedElement+=FlowPayloadFeature</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFlowPayloadFeatureMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Kernel.Interactions.IPayloadFeature elementAsPayloadFeature)
                {
                    PayloadFeatureTextualNotationBuilder.BuildFlowPayloadFeature(elementAsPayloadFeature, writerContext, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FlowFeatureMember
        /// <para>FlowFeatureMember:FeatureMembership=ownedRelatedElement+=FlowFeature</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFlowFeatureMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage elementAsReferenceUsage)
                {
                    ReferenceUsageTextualNotationBuilder.BuildFlowFeature(elementAsReferenceUsage, writerContext, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionBehaviorMember
        /// <para>ActionBehaviorMember:FeatureMembership=BehaviorUsageMember|ActionNodeMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionBehaviorMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Core.Types.IFeatureMembership pocoFeatureMembershipBehaviorUsageMember when pocoFeatureMembershipBehaviorUsageMember.IsValidForBehaviorUsageMember():
                    BuildBehaviorUsageMember(pocoFeatureMembershipBehaviorUsageMember, writerContext, stringBuilder);
                    break;
                default:
                    BuildActionNodeMember(poco, writerContext, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InitialNodeMember
        /// <para>InitialNodeMember:FeatureMembership=MemberPrefix'first'memberFeature=[QualifiedName]RelationshipBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInitialNodeMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, writerContext, stringBuilder);
            stringBuilder.Append("first ");
            BuildMemberFeature(poco, writerContext, stringBuilder);
            RelationshipTextualNotationBuilder.BuildRelationshipBody(poco, writerContext, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionNodeMember
        /// <para>ActionNodeMember:FeatureMembership=MemberPrefixownedRelatedElement+=ActionNode</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionNodeMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, writerContext, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.Actions.IActionUsage elementAsActionUsage)
                {
                    ActionUsageTextualNotationBuilder.BuildActionNode(elementAsActionUsage, writerContext, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionTargetSuccessionMember
        /// <para>ActionTargetSuccessionMember:FeatureMembership=MemberPrefixownedRelatedElement+=ActionTargetSuccession</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionTargetSuccessionMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, writerContext, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage elementAsUsage)
                {
                    UsageTextualNotationBuilder.BuildActionTargetSuccession(elementAsUsage, writerContext, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule GuardedSuccessionMember
        /// <para>GuardedSuccessionMember:FeatureMembership=MemberPrefixownedRelatedElement+=GuardedSuccession</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildGuardedSuccessionMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, writerContext, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.States.ITransitionUsage elementAsTransitionUsage)
                {
                    TransitionUsageTextualNotationBuilder.BuildGuardedSuccession(elementAsTransitionUsage, writerContext, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ForVariableDeclarationMember
        /// <para>ForVariableDeclarationMember:FeatureMembership=ownedRelatedElement+=UsageDeclaration</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildForVariableDeclarationMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage elementAsUsage)
                {
                    UsageTextualNotationBuilder.BuildUsageDeclaration(elementAsUsage, writerContext, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EntryTransitionMember
        /// <para>EntryTransitionMember:FeatureMembership=MemberPrefix(ownedRelatedElement+=GuardedTargetSuccession|'then'ownedRelatedElement+=TargetSuccession)';'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildEntryTransitionMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, writerContext, stringBuilder);
            BuildEntryTransitionMemberHandCoded(poco, writerContext, stringBuilder);
            stringBuilder.AppendLine(";");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TransitionUsageMember
        /// <para>TransitionUsageMember:FeatureMembership=MemberPrefixownedRelatedElement+=TransitionUsage</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTransitionUsageMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, writerContext, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.States.ITransitionUsage elementAsTransitionUsage)
                {
                    TransitionUsageTextualNotationBuilder.BuildTransitionUsage(elementAsTransitionUsage, writerContext, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TargetTransitionUsageMember
        /// <para>TargetTransitionUsageMember:FeatureMembership=MemberPrefixownedRelatedElement+=TargetTransitionUsage</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTargetTransitionUsageMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, writerContext, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.States.ITransitionUsage elementAsTransitionUsage)
                {
                    TransitionUsageTextualNotationBuilder.BuildTargetTransitionUsage(elementAsTransitionUsage, writerContext, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataBodyUsageMember
        /// <para>MetadataBodyUsageMember:FeatureMembership=ownedMemberFeature=MetadataBodyUsage</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataBodyUsageMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {

            if (poco.ownedMemberFeature != null)
            {

                if (poco.ownedMemberFeature is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage elementAsReferenceUsage)
                {
                    ReferenceUsageTextualNotationBuilder.BuildMetadataBodyUsage(elementAsReferenceUsage, writerContext, stringBuilder);
                }
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedFeatureMember
        /// <para>OwnedFeatureMember:FeatureMembership=MemberPrefixownedRelatedElement+=FeatureElement</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedFeatureMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, writerContext, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeature elementAsFeature)
                {
                    FeatureTextualNotationBuilder.BuildFeatureElement(elementAsFeature, writerContext, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedExpressionReferenceMember
        /// <para>OwnedExpressionReferenceMember:FeatureMembership=ownedRelationship+=OwnedExpressionReference</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedExpressionReferenceMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureReferenceExpression elementAsFeatureReferenceExpression)
                {
                    FeatureReferenceExpressionTextualNotationBuilder.BuildOwnedExpressionReference(elementAsFeatureReferenceExpression, writerContext, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedExpressionMember
        /// <para>OwnedExpressionMember:FeatureMembership=ownedFeatureMember=OwnedExpression</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedExpressionMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            BuildOwnedFeatureMember(poco, writerContext, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SequenceExpressionListMember
        /// <para>SequenceExpressionListMember:FeatureMembership=ownedMemberFeature=SequenceExpressionList</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSequenceExpressionListMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {

            if (poco.ownedMemberFeature != null)
            {

                if (poco.ownedMemberFeature is SysML2.NET.Core.POCO.Kernel.Functions.IExpression elementAsExpression)
                {
                    ExpressionTextualNotationBuilder.BuildSequenceExpressionList(elementAsExpression, writerContext, stringBuilder);
                }
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FunctionReferenceMember
        /// <para>FunctionReferenceMember:FeatureMembership=ownedMemberFeature=FunctionReference</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFunctionReferenceMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {

            if (poco.ownedMemberFeature != null)
            {

                if (poco.ownedMemberFeature is SysML2.NET.Core.POCO.Kernel.Functions.IExpression elementAsExpression)
                {
                    ExpressionTextualNotationBuilder.BuildFunctionReference(elementAsExpression, writerContext, stringBuilder);
                }
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NamedArgumentMember
        /// <para>NamedArgumentMember:FeatureMembership=ownedMemberFeature=NamedArgument</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNamedArgumentMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {

            if (poco.ownedMemberFeature != null)
            {
                FeatureTextualNotationBuilder.BuildNamedArgument(poco.ownedMemberFeature, writerContext, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ExpressionBodyMember
        /// <para>ExpressionBodyMember:FeatureMembership=ownedMemberFeature=ExpressionBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildExpressionBodyMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {

            if (poco.ownedMemberFeature != null)
            {

                if (poco.ownedMemberFeature is SysML2.NET.Core.POCO.Kernel.Functions.IExpression elementAsExpression)
                {
                    ExpressionTextualNotationBuilder.BuildExpressionBody(elementAsExpression, writerContext, stringBuilder);
                }
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PayloadFeatureMember
        /// <para>PayloadFeatureMember:FeatureMembership=ownedRelatedElement=PayloadFeature</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPayloadFeatureMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeature elementAsFeature)
                {
                    FeatureTextualNotationBuilder.BuildPayloadFeature(elementAsFeature, writerContext, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataBodyFeatureMember
        /// <para>MetadataBodyFeatureMember:FeatureMembership=ownedMemberFeature=MetadataBodyFeature</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataBodyFeatureMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {

            if (poco.ownedMemberFeature != null)
            {
                FeatureTextualNotationBuilder.BuildMetadataBodyFeature(poco.ownedMemberFeature, writerContext, stringBuilder);
            }

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
