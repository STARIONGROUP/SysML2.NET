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
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNonOccurrenceUsageMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, cursorCache, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage elementAsUsage)
                {
                    UsageTextualNotationBuilder.BuildNonOccurrenceUsageElement(elementAsUsage, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OccurrenceUsageMember
        /// <para>OccurrenceUsageMember:FeatureMembership=MemberPrefixownedRelatedElement+=OccurrenceUsageElement</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOccurrenceUsageMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, cursorCache, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage elementAsUsage)
                {
                    UsageTextualNotationBuilder.BuildOccurrenceUsageElement(elementAsUsage, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule StructureUsageMember
        /// <para>StructureUsageMember:FeatureMembership=MemberPrefixownedRelatedElement+=StructureUsageElement</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildStructureUsageMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, cursorCache, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage elementAsUsage)
                {
                    UsageTextualNotationBuilder.BuildStructureUsageElement(elementAsUsage, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BehaviorUsageMember
        /// <para>BehaviorUsageMember:FeatureMembership=MemberPrefixownedRelatedElement+=BehaviorUsageElement</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBehaviorUsageMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, cursorCache, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage elementAsUsage)
                {
                    UsageTextualNotationBuilder.BuildBehaviorUsageElement(elementAsUsage, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SourceSuccessionMember
        /// <para>SourceSuccessionMember:FeatureMembership='then'ownedRelatedElement+=SourceSuccession</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSourceSuccessionMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            stringBuilder.Append("then ");

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.Connections.ISuccessionAsUsage elementAsSuccessionAsUsage)
                {
                    SuccessionAsUsageTextualNotationBuilder.BuildSourceSuccession(elementAsSuccessionAsUsage, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceNonOccurrenceUsageMember
        /// <para>InterfaceNonOccurrenceUsageMember:FeatureMembership=MemberPrefixownedRelatedElement+=InterfaceNonOccurrenceUsageElement</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInterfaceNonOccurrenceUsageMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, cursorCache, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage elementAsUsage)
                {
                    UsageTextualNotationBuilder.BuildInterfaceNonOccurrenceUsageElement(elementAsUsage, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceOccurrenceUsageMember
        /// <para>InterfaceOccurrenceUsageMember:FeatureMembership=MemberPrefixownedRelatedElement+=InterfaceOccurrenceUsageElement</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInterfaceOccurrenceUsageMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, cursorCache, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage elementAsUsage)
                {
                    UsageTextualNotationBuilder.BuildInterfaceOccurrenceUsageElement(elementAsUsage, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FlowPayloadFeatureMember
        /// <para>FlowPayloadFeatureMember:FeatureMembership=ownedRelatedElement+=FlowPayloadFeature</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFlowPayloadFeatureMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Kernel.Interactions.IPayloadFeature elementAsPayloadFeature)
                {
                    PayloadFeatureTextualNotationBuilder.BuildFlowPayloadFeature(elementAsPayloadFeature, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FlowFeatureMember
        /// <para>FlowFeatureMember:FeatureMembership=ownedRelatedElement+=FlowFeature</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFlowFeatureMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage elementAsReferenceUsage)
                {
                    ReferenceUsageTextualNotationBuilder.BuildFlowFeature(elementAsReferenceUsage, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionBehaviorMember
        /// <para>ActionBehaviorMember:FeatureMembership=BehaviorUsageMember|ActionNodeMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionBehaviorMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildActionBehaviorMemberHandCoded(poco, cursorCache, stringBuilder);
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InitialNodeMember
        /// <para>InitialNodeMember:FeatureMembership=MemberPrefix'first'memberFeature=[QualifiedName]RelationshipBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInitialNodeMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, cursorCache, stringBuilder);
            stringBuilder.Append("first ");
            BuildMemberFeature(poco, cursorCache, stringBuilder);
            RelationshipTextualNotationBuilder.BuildRelationshipBody(poco, cursorCache, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionNodeMember
        /// <para>ActionNodeMember:FeatureMembership=MemberPrefixownedRelatedElement+=ActionNode</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionNodeMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, cursorCache, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.Actions.IActionUsage elementAsActionUsage)
                {
                    ActionUsageTextualNotationBuilder.BuildActionNode(elementAsActionUsage, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionTargetSuccessionMember
        /// <para>ActionTargetSuccessionMember:FeatureMembership=MemberPrefixownedRelatedElement+=ActionTargetSuccession</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionTargetSuccessionMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, cursorCache, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage elementAsUsage)
                {
                    UsageTextualNotationBuilder.BuildActionTargetSuccession(elementAsUsage, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule GuardedSuccessionMember
        /// <para>GuardedSuccessionMember:FeatureMembership=MemberPrefixownedRelatedElement+=GuardedSuccession</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildGuardedSuccessionMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, cursorCache, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.States.ITransitionUsage elementAsTransitionUsage)
                {
                    TransitionUsageTextualNotationBuilder.BuildGuardedSuccession(elementAsTransitionUsage, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ForVariableDeclarationMember
        /// <para>ForVariableDeclarationMember:FeatureMembership=ownedRelatedElement+=UsageDeclaration</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildForVariableDeclarationMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage elementAsUsage)
                {
                    UsageTextualNotationBuilder.BuildUsageDeclaration(elementAsUsage, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EntryTransitionMember
        /// <para>EntryTransitionMember:FeatureMembership=MemberPrefix(ownedRelatedElement+=GuardedTargetSuccession|'then'ownedRelatedElement+=TargetSuccession)';'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildEntryTransitionMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, cursorCache, stringBuilder);
            BuildEntryTransitionMemberHandCoded(poco, cursorCache, stringBuilder);
            stringBuilder.AppendLine(";");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TransitionUsageMember
        /// <para>TransitionUsageMember:FeatureMembership=MemberPrefixownedRelatedElement+=TransitionUsage</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTransitionUsageMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, cursorCache, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.States.ITransitionUsage elementAsTransitionUsage)
                {
                    TransitionUsageTextualNotationBuilder.BuildTransitionUsage(elementAsTransitionUsage, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TargetTransitionUsageMember
        /// <para>TargetTransitionUsageMember:FeatureMembership=MemberPrefixownedRelatedElement+=TargetTransitionUsage</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTargetTransitionUsageMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, cursorCache, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.States.ITransitionUsage elementAsTransitionUsage)
                {
                    TransitionUsageTextualNotationBuilder.BuildTargetTransitionUsage(elementAsTransitionUsage, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataBodyUsageMember
        /// <para>MetadataBodyUsageMember:FeatureMembership=ownedMemberFeature=MetadataBodyUsage</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataBodyUsageMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

            if (poco.ownedMemberFeature != null)
            {
                var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                stringBuilder.Append("ref ");
                stringBuilder.Append(" :>> ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IRedefinition elementAsRedefinition)
                    {
                        RedefinitionTextualNotationBuilder.BuildOwnedRedefinition(elementAsRedefinition, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();


                if (poco.ownedMemberFeature != null)
                {

                    if (poco.ownedMemberFeature.OwnedRelationship.Count != 0 || poco.ownedMemberFeature.type.Count != 0 || poco.ownedMemberFeature.chainingFeature.Count != 0 || poco.ownedMemberFeature.IsOrdered)
                    {
                        FeatureTextualNotationBuilder.BuildFeatureSpecializationPart(poco.ownedMemberFeature, cursorCache, stringBuilder);
                    }
                }

                if (poco.ownedMemberFeature != null)
                {

                    if (poco.ownedMemberFeature.OwnedRelationship.Count != 0 || poco.ownedMemberFeature.type.Count != 0 || poco.ownedMemberFeature.chainingFeature.Count != 0 || !string.IsNullOrWhiteSpace(poco.ownedMemberFeature.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.ownedMemberFeature.DeclaredName) || poco.ownedMemberFeature.Direction.HasValue || poco.ownedMemberFeature.IsDerived || poco.ownedMemberFeature.IsAbstract || poco.ownedMemberFeature.IsConstant || poco.ownedMemberFeature.IsOrdered || poco.ownedMemberFeature.IsEnd || poco.ownedMemberFeature.importedMembership.Count != 0 || poco.ownedMemberFeature.IsComposite || poco.ownedMemberFeature.IsPortion || poco.ownedMemberFeature.IsVariable || poco.ownedMemberFeature.IsSufficient || poco.ownedMemberFeature.unioningType.Count != 0 || poco.ownedMemberFeature.intersectingType.Count != 0 || poco.ownedMemberFeature.differencingType.Count != 0 || poco.ownedMemberFeature.featuringType.Count != 0 || poco.ownedMemberFeature.ownedTypeFeaturing.Count != 0)
                    {
                        FeatureTextualNotationBuilder.BuildValuePart(poco.ownedMemberFeature, cursorCache, stringBuilder);
                    }
                }

                if (poco.ownedMemberFeature != null)
                {
                    TypeTextualNotationBuilder.BuildMetadataBody(poco.ownedMemberFeature, cursorCache, stringBuilder);
                }

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedFeatureMember
        /// <para>OwnedFeatureMember:FeatureMembership=MemberPrefixownedRelatedElement+=FeatureElement</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedFeatureMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, cursorCache, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeature elementAsFeature)
                {
                    FeatureTextualNotationBuilder.BuildFeatureElement(elementAsFeature, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedExpressionReferenceMember
        /// <para>OwnedExpressionReferenceMember:FeatureMembership=ownedRelationship+=OwnedExpressionReference</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedExpressionReferenceMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureReferenceExpression elementAsFeatureReferenceExpression)
                {
                    FeatureReferenceExpressionTextualNotationBuilder.BuildOwnedExpressionReference(elementAsFeatureReferenceExpression, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedExpressionMember
        /// <para>OwnedExpressionMember:FeatureMembership=ownedFeatureMember=OwnedExpression</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedExpressionMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildOwnedFeatureMember(poco, cursorCache, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SequenceExpressionListMember
        /// <para>SequenceExpressionListMember:FeatureMembership=ownedMemberFeature=SequenceExpressionList</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSequenceExpressionListMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

            if (poco.ownedMemberFeature != null)
            {
                BuildSequenceExpressionListHandCoded(poco.ownedMemberFeature, cursorCache, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FunctionReferenceMember
        /// <para>FunctionReferenceMember:FeatureMembership=ownedMemberFeature=FunctionReference</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFunctionReferenceMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

            if (poco.ownedMemberFeature != null)
            {
                var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureTyping elementAsFeatureTyping)
                    {
                        FeatureTypingTextualNotationBuilder.BuildReferenceTyping(elementAsFeatureTyping, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();


            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NamedArgumentMember
        /// <para>NamedArgumentMember:FeatureMembership=ownedMemberFeature=NamedArgument</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNamedArgumentMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

            if (poco.ownedMemberFeature != null)
            {
                FeatureTextualNotationBuilder.BuildNamedArgument(poco.ownedMemberFeature, cursorCache, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ExpressionBodyMember
        /// <para>ExpressionBodyMember:FeatureMembership=ownedMemberFeature=ExpressionBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildExpressionBodyMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

            if (poco.ownedMemberFeature != null)
            {
                stringBuilder.AppendLine("{");

                if (poco.ownedMemberFeature != null)
                {
                    TypeTextualNotationBuilder.BuildFunctionBodyPart(poco.ownedMemberFeature, cursorCache, stringBuilder);
                }
                stringBuilder.AppendLine("}");

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PayloadFeatureMember
        /// <para>PayloadFeatureMember:FeatureMembership=ownedRelatedElement=PayloadFeature</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPayloadFeatureMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeature elementAsFeature)
                {
                    FeatureTextualNotationBuilder.BuildPayloadFeature(elementAsFeature, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataBodyFeatureMember
        /// <para>MetadataBodyFeatureMember:FeatureMembership=ownedMemberFeature=MetadataBodyFeature</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataBodyFeatureMember(SysML2.NET.Core.POCO.Core.Types.IFeatureMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

            if (poco.ownedMemberFeature != null)
            {
                FeatureTextualNotationBuilder.BuildMetadataBodyFeature(poco.ownedMemberFeature, cursorCache, stringBuilder);
            }

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
