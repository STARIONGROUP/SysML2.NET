// -------------------------------------------------------------------------------------------------
// <copyright file="TypeTextualNotationBuilder.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

namespace SysML2.NET.Serializer.TextualNotation.Writers
{
    using System.Linq;

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
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildDefinitionBody(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            if (writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship).Current is not SysML2.NET.Core.POCO.Root.Elements.IRelationship emptyBodyCandidate || !emptyBodyCandidate.IsValidForDefinitionBodyItem(writerContext))
            {
                stringBuilder.AppendLine(";");
            }
            else
            {
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");
                stringBuilder.IncreaseIndent();
                var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Elements.IRelationship loopBodyItem && loopBodyItem.IsValidForDefinitionBodyItem(writerContext))
                {
                    var positionBeforeItem0 = ownedRelationshipCursor.Position;
                    BuildDefinitionBodyItem(poco, writerContext, stringBuilder);
                    ownedRelationshipCursor.AssertAdvancedSince(positionBeforeItem0, "DefinitionBodyItem");
                }
                stringBuilder.DecreaseIndent();
                stringBuilder.AppendLine("}");
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule DefinitionBodyItem
        /// <para>DefinitionBodyItem:Type=ownedRelationship+=DefinitionMember|ownedRelationship+=VariantUsageMember|ownedRelationship+=NonOccurrenceUsageMember|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=OccurrenceUsageMember|ownedRelationship+=AliasMember|ownedRelationship+=Import</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildDefinitionBodyItem(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            BuildDefinitionBodyItemHandCoded(poco, writerContext, stringBuilder);
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceBody
        /// <para>InterfaceBody:Type=';'|'{'InterfaceBodyItem*'}'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildInterfaceBody(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            if (writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship).Current is not SysML2.NET.Core.POCO.Root.Elements.IRelationship emptyBodyCandidate || !emptyBodyCandidate.IsValidForInterfaceBodyItem(writerContext))
            {
                stringBuilder.AppendLine(";");
            }
            else
            {
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");
                stringBuilder.IncreaseIndent();
                var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Elements.IRelationship loopBodyItem && loopBodyItem.IsValidForInterfaceBodyItem(writerContext))
                {
                    var positionBeforeItem0 = ownedRelationshipCursor.Position;
                    BuildInterfaceBodyItem(poco, writerContext, stringBuilder);
                    ownedRelationshipCursor.AssertAdvancedSince(positionBeforeItem0, "InterfaceBodyItem");
                }
                stringBuilder.DecreaseIndent();
                stringBuilder.AppendLine("}");
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceBodyItem
        /// <para>InterfaceBodyItem:Type=ownedRelationship+=DefinitionMember|ownedRelationship+=VariantUsageMember|ownedRelationship+=InterfaceNonOccurrenceUsageMember|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=InterfaceOccurrenceUsageMember|ownedRelationship+=AliasMember|ownedRelationship+=Import</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildInterfaceBodyItem(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            BuildInterfaceBodyItemHandCoded(poco, writerContext, stringBuilder);
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionBody
        /// <para>ActionBody:Type=';'|'{'ActionBodyItem*'}'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildActionBody(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            if (writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship).Current is not SysML2.NET.Core.POCO.Root.Elements.IRelationship emptyBodyCandidate || !emptyBodyCandidate.IsValidForActionBodyItem(writerContext))
            {
                stringBuilder.AppendLine(";");
            }
            else
            {
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");
                stringBuilder.IncreaseIndent();
                var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Elements.IRelationship loopBodyItem && loopBodyItem.IsValidForActionBodyItem(writerContext))
                {
                    var positionBeforeItem0 = ownedRelationshipCursor.Position;
                    BuildActionBodyItem(poco, writerContext, stringBuilder);
                    ownedRelationshipCursor.AssertAdvancedSince(positionBeforeItem0, "ActionBodyItem");
                }
                stringBuilder.DecreaseIndent();
                stringBuilder.AppendLine("}");
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionBodyItem
        /// <para>ActionBodyItem:Type=NonBehaviorBodyItem|ownedRelationship+=InitialNodeMember(ownedRelationship+=ActionTargetSuccessionMember)*|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=ActionBehaviorMember(ownedRelationship+=ActionTargetSuccessionMember)*|ownedRelationship+=GuardedSuccessionMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildActionBodyItem(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            BuildActionBodyItemHandCoded(poco, writerContext, stringBuilder);
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule StateBodyItem
        /// <para>StateBodyItem:Type=NonBehaviorBodyItem|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=BehaviorUsageMember(ownedRelationship+=TargetTransitionUsageMember)*|ownedRelationship+=TransitionUsageMember|ownedRelationship+=EntryActionMember(ownedRelationship+=EntryTransitionMember)*|ownedRelationship+=DoActionMember|ownedRelationship+=ExitActionMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildStateBodyItem(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            BuildStateBodyItemHandCoded(poco, writerContext, stringBuilder);
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule CalculationBody
        /// <para>CalculationBody:Type=';'|'{'CalculationBodyPart'}'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildCalculationBody(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            stringBuilder.EnterInlineBlock();
            try
            {
                if (writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship).Current == null)
                {
                    stringBuilder.AppendLine(";");
                }
                else
                {
                    stringBuilder.Append(' ');
                    stringBuilder.AppendLine("{");
                    stringBuilder.IncreaseIndent();
                    var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                    if (ownedRelationshipCursor.Current != null)
                    {
                        BuildCalculationBodyPart(poco, writerContext, stringBuilder);
                    }
                    stringBuilder.DecreaseIndent();
                    stringBuilder.AppendLine("}");
                }
            }
            finally
            {
                stringBuilder.ExitInlineBlock();
                stringBuilder.AppendLine();
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule CalculationBodyPart
        /// <para>CalculationBodyPart:Type=CalculationBodyItem*(ownedRelationship+=ResultExpressionMember)?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildCalculationBodyPart(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            while (ownedRelationshipCursor.Current is not null and not SysML2.NET.Core.POCO.Kernel.Functions.IResultExpressionMembership)
            {
                var positionBeforeItem0 = ownedRelationshipCursor.Position;
                BuildCalculationBodyItem(poco, writerContext, stringBuilder);
                ownedRelationshipCursor.AssertAdvancedSince(positionBeforeItem0, "CalculationBodyItem");
            }


            if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Functions.IResultExpressionMembership)
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Functions.IResultExpressionMembership elementAsResultExpressionMembership)
                    {
                        ResultExpressionMembershipTextualNotationBuilder.BuildResultExpressionMember(elementAsResultExpressionMembership, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();

                    }
                }
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule CalculationBodyItem
        /// <para>CalculationBodyItem:Type=ActionBodyItem|ownedRelationship+=ReturnParameterMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildCalculationBodyItem(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Functions.IReturnParameterMembership returnParameterMembership)
            {
                ReturnParameterMembershipTextualNotationBuilder.BuildReturnParameterMember(returnParameterMembership, writerContext, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else
            {
                BuildActionBodyItem(poco, writerContext, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule RequirementBody
        /// <para>RequirementBody:Type=';'|'{'RequirementBodyItem*'}'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildRequirementBody(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            if (writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship).Current == null)
            {
                stringBuilder.AppendLine(";");
            }
            else
            {
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");
                stringBuilder.IncreaseIndent();
                var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                while (ownedRelationshipCursor.Current != null)
                {
                    var positionBeforeItem0 = ownedRelationshipCursor.Position;
                    BuildRequirementBodyItem(poco, writerContext, stringBuilder);
                    ownedRelationshipCursor.AssertAdvancedSince(positionBeforeItem0, "RequirementBodyItem");
                }
                stringBuilder.DecreaseIndent();
                stringBuilder.AppendLine("}");
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule RequirementBodyItem
        /// <para>RequirementBodyItem:Type=DefinitionBodyItem|ownedRelationship+=SubjectMember|ownedRelationship+=RequirementConstraintMember|ownedRelationship+=FramedConcernMember|ownedRelationship+=RequirementVerificationMember|ownedRelationship+=ActorMember|ownedRelationship+=StakeholderMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildRequirementBodyItem(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.Requirements.ISubjectMembership subjectMembership)
            {
                SubjectMembershipTextualNotationBuilder.BuildSubjectMember(subjectMembership, writerContext, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.Requirements.IFramedConcernMembership framedConcernMembership)
            {
                FramedConcernMembershipTextualNotationBuilder.BuildFramedConcernMember(framedConcernMembership, writerContext, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.VerificationCases.IRequirementVerificationMembership requirementVerificationMembership)
            {
                RequirementVerificationMembershipTextualNotationBuilder.BuildRequirementVerificationMember(requirementVerificationMembership, writerContext, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.Requirements.IActorMembership actorMembership)
            {
                ActorMembershipTextualNotationBuilder.BuildActorMember(actorMembership, writerContext, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.Requirements.IStakeholderMembership stakeholderMembership)
            {
                StakeholderMembershipTextualNotationBuilder.BuildStakeholderMember(stakeholderMembership, writerContext, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.Requirements.IRequirementConstraintMembership requirementConstraintMembership)
            {
                RequirementConstraintMembershipTextualNotationBuilder.BuildRequirementConstraintMember(requirementConstraintMembership, writerContext, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else
            {
                BuildDefinitionBodyItem(poco, writerContext, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule CaseBody
        /// <para>CaseBody:Type=';'|'{'CaseBodyItem*(ownedRelationship+=ResultExpressionMember)?'}'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildCaseBody(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            if (writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship).Current == null)
            {
                stringBuilder.AppendLine(";");
            }
            else
            {
                var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");
                stringBuilder.IncreaseIndent();
                while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Elements.IRelationship ownedRelationshipBodyItem && ownedRelationshipBodyItem.IsValidForCaseBodyItem(writerContext))
                {
                    var positionBeforeItem0 = ownedRelationshipCursor.Position;
                    BuildCaseBodyItem(poco, writerContext, stringBuilder);
                    ownedRelationshipCursor.AssertAdvancedSince(positionBeforeItem0, "CaseBodyItem");
                }


                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Functions.IResultExpressionMembership)
                {

                    if (ownedRelationshipCursor.Current != null)
                    {

                        if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Functions.IResultExpressionMembership elementAsResultExpressionMembership)
                        {
                            ResultExpressionMembershipTextualNotationBuilder.BuildResultExpressionMember(elementAsResultExpressionMembership, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();

                        }
                    }
                    stringBuilder.Append(' ');
                }

                stringBuilder.DecreaseIndent();
                stringBuilder.AppendLine("}");
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule CaseBodyItem
        /// <para>CaseBodyItem:Type=CalculationBodyItem|ownedRelationship+=SubjectMember|ownedRelationship+=ActorMember|ownedRelationship+=ObjectiveMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildCaseBodyItem(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.Requirements.ISubjectMembership subjectMembership)
            {
                SubjectMembershipTextualNotationBuilder.BuildSubjectMember(subjectMembership, writerContext, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.Requirements.IActorMembership actorMembership)
            {
                ActorMembershipTextualNotationBuilder.BuildActorMember(actorMembership, writerContext, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.Cases.IObjectiveMembership objectiveMembership)
            {
                ObjectiveMembershipTextualNotationBuilder.BuildObjectiveMember(objectiveMembership, writerContext, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else
            {
                BuildCalculationBodyItem(poco, writerContext, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataBody
        /// <para>MetadataBody:Type=';'|'{'(ownedRelationship+=DefinitionMember|ownedRelationship+=MetadataBodyUsageMember|ownedRelationship+=AliasMember|ownedRelationship+=Import)*'}'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildMetadataBody(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            if (writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship).Current == null)
            {
                stringBuilder.AppendLine(";");
            }
            else
            {
                var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");
                stringBuilder.IncreaseIndent();
                while (ownedRelationshipCursor.Current != null)
                {
                    var positionBeforeItem0 = ownedRelationshipCursor.Position;
                    switch (ownedRelationshipCursor.Current)
                    {
                        case SysML2.NET.Core.POCO.Core.Types.IFeatureMembership featureMembership:
                            FeatureMembershipTextualNotationBuilder.BuildMetadataBodyUsageMember(featureMembership, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();
                            break;
                        case SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership owningMembership:
                            OwningMembershipTextualNotationBuilder.BuildDefinitionMember(owningMembership, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();
                            break;
                        case SysML2.NET.Core.POCO.Root.Namespaces.IMembership membership:
                            MembershipTextualNotationBuilder.BuildAliasMember(membership, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();
                            break;
                        case SysML2.NET.Core.POCO.Root.Namespaces.IImport import:
                            ImportTextualNotationBuilder.BuildImport(import, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();
                            break;
                        default:
                            ownedRelationshipCursor.Move();
                            break;
                    }
                    ownedRelationshipCursor.AssertAdvancedSince(positionBeforeItem0, "MetadataBody");
                }

                stringBuilder.DecreaseIndent();
                stringBuilder.AppendLine("}");
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypePrefix
        /// <para>TypePrefix:Type=(isAbstract?='abstract')?(ownedRelationship+=PrefixMetadataMember)*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildTypePrefix(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (poco.IsAbstract)
            {
                stringBuilder.Append(" abstract ");
                stringBuilder.Append(' ');
            }


            while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Elements.IRelationship prefixMetadataMemberGuard0 && prefixMetadataMemberGuard0.IsValidForPrefixMetadataMember(writerContext))
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership elementAsOwningMembership)
                    {
                        OwningMembershipTextualNotationBuilder.BuildPrefixMetadataMember(elementAsOwningMembership, writerContext, stringBuilder);
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
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildTypeDeclaration(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (poco.IsSufficient && poco is not (SysML2.NET.Core.POCO.Systems.Connections.IConnectionDefinition))
            {
                stringBuilder.Append(" all ");
                stringBuilder.Append(' ');
            }

            ElementTextualNotationBuilder.BuildIdentification(poco, writerContext, stringBuilder);

            if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership)
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership elementAsOwningMembership)
                    {
                        OwningMembershipTextualNotationBuilder.BuildOwnedMultiplicity(elementAsOwningMembership, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();

                    }
                }
                stringBuilder.Append(' ');
            }

            BuildTypeDeclarationHandCoded(poco, writerContext, stringBuilder);
            stringBuilder.Append(' ');
            while (ownedRelationshipCursor.Current != null)
            {
                var positionBeforeItem0 = ownedRelationshipCursor.Position;
                BuildTypeRelationshipPart(poco, writerContext, stringBuilder);
                ownedRelationshipCursor.AssertAdvancedSince(positionBeforeItem0, "TypeRelationshipPart");
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SpecializationPart
        /// <para>SpecializationPart:Type=SPECIALIZESownedRelationship+=OwnedSpecialization(','ownedRelationship+=OwnedSpecialization)*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildSpecializationPart(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append(" :> ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.ISpecialization elementAsSpecialization)
                {
                    SpecializationTextualNotationBuilder.BuildOwnedSpecialization(elementAsSpecialization, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                }
            }

            while (ownedRelationshipCursor.Current != null && ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.ISpecialization)
            {
                stringBuilder.Append(", ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.ISpecialization elementAsSpecialization)
                    {
                        SpecializationTextualNotationBuilder.BuildOwnedSpecialization(elementAsSpecialization, writerContext, stringBuilder);
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
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildConjugationPart(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append(" ~");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IConjugation elementAsConjugation)
                {
                    ConjugationTextualNotationBuilder.BuildOwnedConjugation(elementAsConjugation, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                }
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeRelationshipPart
        /// <para>TypeRelationshipPart:Type=DisjoiningPart|UnioningPart|IntersectingPart|DifferencingPart</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildTypeRelationshipPart(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Core.Types.IType pocoTypeDisjoiningPart when pocoTypeDisjoiningPart.IsValidForDisjoiningPart(writerContext):
                    BuildDisjoiningPart(pocoTypeDisjoiningPart, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.IType pocoTypeUnioningPart when pocoTypeUnioningPart.IsValidForUnioningPart(writerContext):
                    BuildUnioningPart(pocoTypeUnioningPart, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.IType pocoTypeIntersectingPart when pocoTypeIntersectingPart.IsValidForIntersectingPart(writerContext):
                    BuildIntersectingPart(pocoTypeIntersectingPart, writerContext, stringBuilder);
                    break;
                default:
                    BuildDifferencingPart(poco, writerContext, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule DisjoiningPart
        /// <para>DisjoiningPart:Type='disjoint''from'ownedRelationship+=OwnedDisjoining(','ownedRelationship+=OwnedDisjoining)*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildDisjoiningPart(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append("disjoint ");
            stringBuilder.Append("from ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IDisjoining elementAsDisjoining)
                {
                    DisjoiningTextualNotationBuilder.BuildOwnedDisjoining(elementAsDisjoining, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                }
            }

            while (ownedRelationshipCursor.Current != null && ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IDisjoining)
            {
                stringBuilder.Append(", ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IDisjoining elementAsDisjoining)
                    {
                        DisjoiningTextualNotationBuilder.BuildOwnedDisjoining(elementAsDisjoining, writerContext, stringBuilder);
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
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildUnioningPart(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append("unions ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IUnioning elementAsUnioning)
                {
                    UnioningTextualNotationBuilder.BuildUnioning(elementAsUnioning, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                }
            }

            while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IUnioning unioningGuard && unioningGuard.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Core.Features.IFeature>().Any())
            {
                stringBuilder.Append(", ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IUnioning elementAsUnioning)
                    {
                        UnioningTextualNotationBuilder.BuildUnioning(elementAsUnioning, writerContext, stringBuilder);
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
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildIntersectingPart(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append("intersects ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IIntersecting elementAsIntersecting)
                {
                    IntersectingTextualNotationBuilder.BuildIntersecting(elementAsIntersecting, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                }
            }

            while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IIntersecting intersectingGuard && intersectingGuard.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Core.Features.IFeature>().Any())
            {
                stringBuilder.Append(", ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IIntersecting elementAsIntersecting)
                    {
                        IntersectingTextualNotationBuilder.BuildIntersecting(elementAsIntersecting, writerContext, stringBuilder);
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
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildDifferencingPart(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append("differences ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IDifferencing elementAsDifferencing)
                {
                    DifferencingTextualNotationBuilder.BuildDifferencing(elementAsDifferencing, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                }
            }

            while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IDifferencing differencingGuard && differencingGuard.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Core.Features.IFeature>().Any())
            {
                stringBuilder.Append(", ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IDifferencing elementAsDifferencing)
                    {
                        DifferencingTextualNotationBuilder.BuildDifferencing(elementAsDifferencing, writerContext, stringBuilder);
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
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildTypeBody(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            if (writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship).Current == null)
            {
                stringBuilder.AppendLine(";");
            }
            else
            {
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");
                stringBuilder.IncreaseIndent();
                var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                while (ownedRelationshipCursor.Current != null)
                {
                    var positionBeforeItem0 = ownedRelationshipCursor.Position;
                    BuildTypeBodyElement(poco, writerContext, stringBuilder);
                    ownedRelationshipCursor.AssertAdvancedSince(positionBeforeItem0, "TypeBodyElement");
                }
                stringBuilder.DecreaseIndent();
                stringBuilder.AppendLine("}");
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeBodyElement
        /// <para>TypeBodyElement:Type=ownedRelationship+=NonFeatureMember|ownedRelationship+=FeatureMember|ownedRelationship+=AliasMember|ownedRelationship+=Import</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildTypeBodyElement(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            switch (ownedRelationshipCursor.Current)
            {
                case SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership owningMembership when owningMembership.IsValidForNonFeatureMember(writerContext):
                    OwningMembershipTextualNotationBuilder.BuildNonFeatureMember(owningMembership, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership owningMembership when owningMembership.IsValidForFeatureMember(writerContext):
                    OwningMembershipTextualNotationBuilder.BuildFeatureMember(owningMembership, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.IMembership membership:
                    MembershipTextualNotationBuilder.BuildAliasMember(membership, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.IImport import:
                    ImportTextualNotationBuilder.BuildImport(import, writerContext, stringBuilder);
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
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildFunctionBody(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            stringBuilder.EnterInlineBlock();
            try
            {
                if (writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship).Current == null)
                {
                    stringBuilder.AppendLine(";");
                }
                else
                {
                    stringBuilder.Append(' ');
                    stringBuilder.AppendLine("{");
                    stringBuilder.IncreaseIndent();
                    var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                    if (ownedRelationshipCursor.Current != null)
                    {
                        BuildFunctionBodyPart(poco, writerContext, stringBuilder);
                    }
                    stringBuilder.DecreaseIndent();
                    stringBuilder.AppendLine("}");
                }
            }
            finally
            {
                stringBuilder.ExitInlineBlock();
                stringBuilder.AppendLine();
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FunctionBodyPart
        /// <para>FunctionBodyPart:Type=(TypeBodyElement|ownedRelationship+=ReturnFeatureMember)*(ownedRelationship+=ResultExpressionMember)?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildFunctionBodyPart(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            while (ownedRelationshipCursor.Current is not null and not SysML2.NET.Core.POCO.Kernel.Functions.IResultExpressionMembership)
            {
                var positionBeforeItem0 = ownedRelationshipCursor.Position;
                switch (ownedRelationshipCursor.Current)
                {
                    case SysML2.NET.Core.POCO.Kernel.Functions.IReturnParameterMembership returnParameterMembership:
                        ReturnParameterMembershipTextualNotationBuilder.BuildReturnFeatureMember(returnParameterMembership, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;
                    default:
                        BuildTypeBodyElement(poco, writerContext, stringBuilder);
                        break;
                }
                ownedRelationshipCursor.AssertAdvancedSince(positionBeforeItem0, "FunctionBodyPart");
            }


            if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Functions.IResultExpressionMembership)
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Functions.IResultExpressionMembership elementAsResultExpressionMembership)
                    {
                        ResultExpressionMembershipTextualNotationBuilder.BuildResultExpressionMember(elementAsResultExpressionMembership, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();

                    }
                }
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InstantiatedTypeReference
        /// <para>InstantiatedTypeReference:Type=[QualifiedName]</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildInstantiatedTypeReference(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder, poco, writerContext, poco);
            stringBuilder.Append(' ');

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Type
        /// <para>Type=TypePrefix'type'TypeDeclarationTypeBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildType(SysML2.NET.Core.POCO.Core.Types.IType poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            BuildTypePrefix(poco, writerContext, stringBuilder);
            stringBuilder.Append("type ");
            BuildTypeDeclaration(poco, writerContext, stringBuilder);
            BuildTypeBody(poco, writerContext, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
