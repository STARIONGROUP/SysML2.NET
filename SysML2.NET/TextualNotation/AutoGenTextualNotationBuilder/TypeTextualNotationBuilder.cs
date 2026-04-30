// -------------------------------------------------------------------------------------------------
// <copyright file="TypeTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="TypeTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> element
    /// </summary>
    public static partial class TypeTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule DefinitionBody
        /// <para>DefinitionBody:Type=';'|'{'DefinitionBodyItem*'}'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDefinitionBody(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            if (cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship).Current == null)
            {
                stringBuilder.AppendLine(";");
            }
            else
            {
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");
                var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                while (ownedRelationshipCursor.Current != null)
                {
                    BuildDefinitionBodyItem(poco, cursorCache, stringBuilder);
                }
                stringBuilder.AppendLine("}");
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule DefinitionBodyItem
        /// <para>DefinitionBodyItem:Type=ownedRelationship+=DefinitionMember|ownedRelationship+=VariantUsageMember|ownedRelationship+=NonOccurrenceUsageMember|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=OccurrenceUsageMember|ownedRelationship+=AliasMember|ownedRelationship+=Import</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDefinitionBodyItem(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildDefinitionBodyItemHandCoded(poco, cursorCache, stringBuilder);
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceBody
        /// <para>InterfaceBody:Type=';'|'{'InterfaceBodyItem*'}'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInterfaceBody(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            if (cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship).Current == null)
            {
                stringBuilder.AppendLine(";");
            }
            else
            {
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");
                var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                while (ownedRelationshipCursor.Current != null)
                {
                    BuildInterfaceBodyItem(poco, cursorCache, stringBuilder);
                }
                stringBuilder.AppendLine("}");
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceBodyItem
        /// <para>InterfaceBodyItem:Type=ownedRelationship+=DefinitionMember|ownedRelationship+=VariantUsageMember|ownedRelationship+=InterfaceNonOccurrenceUsageMember|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=InterfaceOccurrenceUsageMember|ownedRelationship+=AliasMember|ownedRelationship+=Import</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInterfaceBodyItem(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildInterfaceBodyItemHandCoded(poco, cursorCache, stringBuilder);
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionBody
        /// <para>ActionBody:Type=';'|'{'ActionBodyItem*'}'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionBody(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            if (cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship).Current == null)
            {
                stringBuilder.AppendLine(";");
            }
            else
            {
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");
                var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                while (ownedRelationshipCursor.Current != null)
                {
                    BuildActionBodyItem(poco, cursorCache, stringBuilder);
                }
                stringBuilder.AppendLine("}");
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionBodyItem
        /// <para>ActionBodyItem:Type=NonBehaviorBodyItem|ownedRelationship+=InitialNodeMember(ownedRelationship+=ActionTargetSuccessionMember)*|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=ActionBehaviorMember(ownedRelationship+=ActionTargetSuccessionMember)*|ownedRelationship+=GuardedSuccessionMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionBodyItem(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildActionBodyItemHandCoded(poco, cursorCache, stringBuilder);
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule StateBodyItem
        /// <para>StateBodyItem:Type=NonBehaviorBodyItem|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=BehaviorUsageMember(ownedRelationship+=TargetTransitionUsageMember)*|ownedRelationship+=TransitionUsageMember|ownedRelationship+=EntryActionMember(ownedRelationship+=EntryTransitionMember)*|ownedRelationship+=DoActionMember|ownedRelationship+=ExitActionMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildStateBodyItem(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildStateBodyItemHandCoded(poco, cursorCache, stringBuilder);
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule CalculationBody
        /// <para>CalculationBody:Type=';'|'{'CalculationBodyPart'}'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildCalculationBody(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            if (cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship).Current == null)
            {
                stringBuilder.AppendLine(";");
            }
            else
            {
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");
                var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                if (ownedRelationshipCursor.Current != null)
                {
                    BuildCalculationBodyPart(poco, cursorCache, stringBuilder);
                }
                stringBuilder.AppendLine("}");
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule CalculationBodyPart
        /// <para>CalculationBodyPart:Type=CalculationBodyItem*(ownedRelationship+=ResultExpressionMember)?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildCalculationBodyPart(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            while (ownedRelationshipCursor.Current is not null and not SysML2.NET.Core.POCO.Kernel.Functions.IResultExpressionMembership)
            {
                BuildCalculationBodyItem(poco, cursorCache, stringBuilder);
            }


            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Functions.IResultExpressionMembership elementAsResultExpressionMembership)
                    {
                        ResultExpressionMembershipTextualNotationBuilder.BuildResultExpressionMember(elementAsResultExpressionMembership, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule CalculationBodyItem
        /// <para>CalculationBodyItem:Type=ActionBodyItem|ownedRelationship+=ReturnParameterMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildCalculationBodyItem(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Functions.IReturnParameterMembership returnParameterMembership)
            {
                ReturnParameterMembershipTextualNotationBuilder.BuildReturnParameterMember(returnParameterMembership, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else
            {
                BuildActionBodyItem(poco, cursorCache, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule RequirementBody
        /// <para>RequirementBody:Type=';'|'{'RequirementBodyItem*'}'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRequirementBody(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            if (cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship).Current == null)
            {
                stringBuilder.AppendLine(";");
            }
            else
            {
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");
                var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                while (ownedRelationshipCursor.Current != null)
                {
                    BuildRequirementBodyItem(poco, cursorCache, stringBuilder);
                }
                stringBuilder.AppendLine("}");
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule RequirementBodyItem
        /// <para>RequirementBodyItem:Type=DefinitionBodyItem|ownedRelationship+=SubjectMember|ownedRelationship+=RequirementConstraintMember|ownedRelationship+=FramedConcernMember|ownedRelationship+=RequirementVerificationMember|ownedRelationship+=ActorMember|ownedRelationship+=StakeholderMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRequirementBodyItem(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.Requirements.ISubjectMembership subjectMembership)
            {
                SubjectMembershipTextualNotationBuilder.BuildSubjectMember(subjectMembership, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.Requirements.IFramedConcernMembership framedConcernMembership)
            {
                FramedConcernMembershipTextualNotationBuilder.BuildFramedConcernMember(framedConcernMembership, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.VerificationCases.IRequirementVerificationMembership requirementVerificationMembership)
            {
                RequirementVerificationMembershipTextualNotationBuilder.BuildRequirementVerificationMember(requirementVerificationMembership, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.Requirements.IActorMembership actorMembership)
            {
                ActorMembershipTextualNotationBuilder.BuildActorMember(actorMembership, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.Requirements.IStakeholderMembership stakeholderMembership)
            {
                StakeholderMembershipTextualNotationBuilder.BuildStakeholderMember(stakeholderMembership, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.Requirements.IRequirementConstraintMembership requirementConstraintMembership)
            {
                RequirementConstraintMembershipTextualNotationBuilder.BuildRequirementConstraintMember(requirementConstraintMembership, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else
            {
                BuildDefinitionBodyItem(poco, cursorCache, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule CaseBody
        /// <para>CaseBody:Type=';'|'{'CaseBodyItem*(ownedRelationship+=ResultExpressionMember)?'}'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildCaseBody(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            if (cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship).Current == null)
            {
                stringBuilder.AppendLine(";");
            }
            else
            {
                var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");
                while (ownedRelationshipCursor.Current != null)
                {
                    BuildCaseBodyItem(poco, cursorCache, stringBuilder);
                }


                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current != null)
                    {

                        if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Functions.IResultExpressionMembership elementAsResultExpressionMembership)
                        {
                            ResultExpressionMembershipTextualNotationBuilder.BuildResultExpressionMember(elementAsResultExpressionMembership, cursorCache, stringBuilder);
                        }
                    }
                    ownedRelationshipCursor.Move();

                    stringBuilder.Append(' ');
                }

                stringBuilder.AppendLine("}");
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule CaseBodyItem
        /// <para>CaseBodyItem:Type=ActionBodyItem|ownedRelationship+=SubjectMember|ownedRelationship+=ActorMember|ownedRelationship+=ObjectiveMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildCaseBodyItem(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.Requirements.ISubjectMembership subjectMembership)
            {
                SubjectMembershipTextualNotationBuilder.BuildSubjectMember(subjectMembership, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.Requirements.IActorMembership actorMembership)
            {
                ActorMembershipTextualNotationBuilder.BuildActorMember(actorMembership, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.Cases.IObjectiveMembership objectiveMembership)
            {
                ObjectiveMembershipTextualNotationBuilder.BuildObjectiveMember(objectiveMembership, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else
            {
                BuildActionBodyItem(poco, cursorCache, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataBody
        /// <para>MetadataBody:Type=';'|'{'(ownedRelationship+=DefinitionMember|ownedRelationship+=MetadataBodyUsageMember|ownedRelationship+=AliasMember|ownedRelationship+=Import)*'}'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataBody(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            if (cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship).Current == null)
            {
                stringBuilder.AppendLine(";");
            }
            else
            {
                var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");
                while (ownedRelationshipCursor.Current != null)
                {
                    switch (ownedRelationshipCursor.Current)
                    {
                        case SysML2.NET.Core.POCO.Core.Types.IFeatureMembership featureMembership:
                            FeatureMembershipTextualNotationBuilder.BuildMetadataBodyUsageMember(featureMembership, cursorCache, stringBuilder);
                            break;
                        case SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership owningMembership:
                            OwningMembershipTextualNotationBuilder.BuildDefinitionMember(owningMembership, cursorCache, stringBuilder);
                            break;
                        case SysML2.NET.Core.POCO.Root.Namespaces.IMembership membership:
                            MembershipTextualNotationBuilder.BuildAliasMember(membership, cursorCache, stringBuilder);
                            break;
                        case SysML2.NET.Core.POCO.Root.Namespaces.IImport import:
                            ImportTextualNotationBuilder.BuildImport(import, cursorCache, stringBuilder);
                            break;
                    }
                    ownedRelationshipCursor.Move();
                }

                stringBuilder.AppendLine("}");
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypePrefix
        /// <para>TypePrefix:Type=(isAbstract?='abstract')?(ownedRelationship+=PrefixMetadataMember)*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypePrefix(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (poco.IsAbstract)
            {
                stringBuilder.Append(" abstract ");
                stringBuilder.Append(' ');
            }


            while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership owningMembershipGuard && owningMembershipGuard.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Systems.Metadata.IMetadataUsage>().Any())
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership elementAsOwningMembership)
                    {
                        OwningMembershipTextualNotationBuilder.BuildPrefixMetadataMember(elementAsOwningMembership, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeDeclaration
        /// <para>TypeDeclaration:Type=(isSufficient?='all')?Identification(ownedRelationship+=OwnedMultiplicity)?(SpecializationPart|ConjugationPart)+TypeRelationshipPart*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypeDeclaration(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (poco.IsSufficient)
            {
                stringBuilder.Append(" all ");
                stringBuilder.Append(' ');
            }

            ElementTextualNotationBuilder.BuildIdentification(poco, cursorCache, stringBuilder);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership elementAsOwningMembership)
                    {
                        OwningMembershipTextualNotationBuilder.BuildOwnedMultiplicity(elementAsOwningMembership, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

                stringBuilder.Append(' ');
            }

            BuildTypeDeclarationHandCoded(poco, cursorCache, stringBuilder);
            stringBuilder.Append(' ');
            while (ownedRelationshipCursor.Current != null)
            {
                BuildTypeRelationshipPart(poco, cursorCache, stringBuilder);
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SpecializationPart
        /// <para>SpecializationPart:Type=SPECIALIZESownedRelationship+=OwnedSpecialization(','ownedRelationship+=OwnedSpecialization)*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSpecializationPart(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append(" :> ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.ISpecialization elementAsSpecialization)
                {
                    SpecializationTextualNotationBuilder.BuildOwnedSpecialization(elementAsSpecialization, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            while (ownedRelationshipCursor.Current != null && ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.ISpecialization)
            {
                stringBuilder.Append(", ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.ISpecialization elementAsSpecialization)
                    {
                        SpecializationTextualNotationBuilder.BuildOwnedSpecialization(elementAsSpecialization, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ConjugationPart
        /// <para>ConjugationPart:Type=CONJUGATESownedRelationship+=OwnedConjugation</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildConjugationPart(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append(" ~");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IConjugation elementAsConjugation)
                {
                    ConjugationTextualNotationBuilder.BuildOwnedConjugation(elementAsConjugation, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeRelationshipPart
        /// <para>TypeRelationshipPart:Type=DisjoiningPart|UnioningPart|IntersectingPart|DifferencingPart</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypeRelationshipPart(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Core.Types.IType pocoTypeDisjoiningPart when pocoTypeDisjoiningPart.IsValidForDisjoiningPart():
                    BuildDisjoiningPart(pocoTypeDisjoiningPart, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.IType pocoTypeUnioningPart when pocoTypeUnioningPart.IsValidForUnioningPart():
                    BuildUnioningPart(pocoTypeUnioningPart, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.IType pocoTypeIntersectingPart when pocoTypeIntersectingPart.IsValidForIntersectingPart():
                    BuildIntersectingPart(pocoTypeIntersectingPart, cursorCache, stringBuilder);
                    break;
                default:
                    BuildDifferencingPart(poco, cursorCache, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule DisjoiningPart
        /// <para>DisjoiningPart:Type='disjoint''from'ownedRelationship+=OwnedDisjoining(','ownedRelationship+=OwnedDisjoining)*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDisjoiningPart(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append("disjoint ");
            stringBuilder.Append("from ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IDisjoining elementAsDisjoining)
                {
                    DisjoiningTextualNotationBuilder.BuildOwnedDisjoining(elementAsDisjoining, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            while (ownedRelationshipCursor.Current != null && ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IDisjoining)
            {
                stringBuilder.Append(", ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IDisjoining elementAsDisjoining)
                    {
                        DisjoiningTextualNotationBuilder.BuildOwnedDisjoining(elementAsDisjoining, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule UnioningPart
        /// <para>UnioningPart:Type='unions'ownedRelationship+=Unioning(','ownedRelationship+=Unioning)*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUnioningPart(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append("unions ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IUnioning elementAsUnioning)
                {
                    UnioningTextualNotationBuilder.BuildUnioning(elementAsUnioning, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IUnioning unioningGuard && unioningGuard.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Core.Features.IFeature>().Any())
            {
                stringBuilder.Append(", ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IUnioning elementAsUnioning)
                    {
                        UnioningTextualNotationBuilder.BuildUnioning(elementAsUnioning, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule IntersectingPart
        /// <para>IntersectingPart:Type='intersects'ownedRelationship+=Intersecting(','ownedRelationship+=Intersecting)*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildIntersectingPart(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append("intersects ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IIntersecting elementAsIntersecting)
                {
                    IntersectingTextualNotationBuilder.BuildIntersecting(elementAsIntersecting, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IIntersecting intersectingGuard && intersectingGuard.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Core.Features.IFeature>().Any())
            {
                stringBuilder.Append(", ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IIntersecting elementAsIntersecting)
                    {
                        IntersectingTextualNotationBuilder.BuildIntersecting(elementAsIntersecting, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule DifferencingPart
        /// <para>DifferencingPart:Type='differences'ownedRelationship+=Differencing(','ownedRelationship+=Differencing)*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDifferencingPart(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append("differences ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IDifferencing elementAsDifferencing)
                {
                    DifferencingTextualNotationBuilder.BuildDifferencing(elementAsDifferencing, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IDifferencing differencingGuard && differencingGuard.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Core.Features.IFeature>().Any())
            {
                stringBuilder.Append(", ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IDifferencing elementAsDifferencing)
                    {
                        DifferencingTextualNotationBuilder.BuildDifferencing(elementAsDifferencing, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeBody
        /// <para>TypeBody:Type=';'|'{'TypeBodyElement*'}'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypeBody(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            if (cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship).Current == null)
            {
                stringBuilder.AppendLine(";");
            }
            else
            {
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");
                var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                while (ownedRelationshipCursor.Current != null)
                {
                    BuildTypeBodyElement(poco, cursorCache, stringBuilder);
                }
                stringBuilder.AppendLine("}");
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeBodyElement
        /// <para>TypeBodyElement:Type=ownedRelationship+=NonFeatureMember|ownedRelationship+=FeatureMember|ownedRelationship+=AliasMember|ownedRelationship+=Import</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypeBodyElement(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            switch (ownedRelationshipCursor.Current)
            {
                case SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership owningMembership when owningMembership.IsValidForNonFeatureMember():
                    OwningMembershipTextualNotationBuilder.BuildNonFeatureMember(owningMembership, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership owningMembership when owningMembership.IsValidForFeatureMember():
                    OwningMembershipTextualNotationBuilder.BuildFeatureMember(owningMembership, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.IMembership membership:
                    MembershipTextualNotationBuilder.BuildAliasMember(membership, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.IImport import:
                    ImportTextualNotationBuilder.BuildImport(import, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                    break;
                default:
                    ownedRelationshipCursor.Move();
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FunctionBody
        /// <para>FunctionBody:Type=';'|'{'FunctionBodyPart'}'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFunctionBody(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            if (cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship).Current == null)
            {
                stringBuilder.AppendLine(";");
            }
            else
            {
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");
                var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                if (ownedRelationshipCursor.Current != null)
                {
                    BuildFunctionBodyPart(poco, cursorCache, stringBuilder);
                }
                stringBuilder.AppendLine("}");
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FunctionBodyPart
        /// <para>FunctionBodyPart:Type=(TypeBodyElement|ownedRelationship+=ReturnFeatureMember)*(ownedRelationship+=ResultExpressionMember)?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFunctionBodyPart(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            while (ownedRelationshipCursor.Current != null)
            {
                switch (ownedRelationshipCursor.Current)
                {
                    case SysML2.NET.Core.POCO.Kernel.Functions.IReturnParameterMembership returnParameterMembership:
                        ReturnParameterMembershipTextualNotationBuilder.BuildReturnFeatureMember(returnParameterMembership, cursorCache, stringBuilder);
                        break;
                }
                ownedRelationshipCursor.Move();
            }


            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Functions.IResultExpressionMembership elementAsResultExpressionMembership)
                    {
                        ResultExpressionMembershipTextualNotationBuilder.BuildResultExpressionMember(elementAsResultExpressionMembership, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InstantiatedTypeReference
        /// <para>InstantiatedTypeReference:Type=[QualifiedName]</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInstantiatedTypeReference(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            stringBuilder.Append(poco.qualifiedName);
            stringBuilder.Append(' ');

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Type
        /// <para>Type=TypePrefix'type'TypeDeclarationTypeBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildType(SysML2.NET.Core.POCO.Core.Types.IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildTypePrefix(poco, cursorCache, stringBuilder);
            stringBuilder.Append("type ");
            BuildTypeDeclaration(poco, cursorCache, stringBuilder);
            BuildTypeBody(poco, cursorCache, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
