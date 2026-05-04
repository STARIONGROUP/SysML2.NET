// -------------------------------------------------------------------------------------------------
// <copyright file="ParameterMembershipTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="ParameterMembershipTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership" /> element
    /// </summary>
    public static partial class ParameterMembershipTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule MessageEventMember
        /// <para>MessageEventMember:ParameterMembership=ownedRelatedElement+=MessageEvent</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMessageEventMember(SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.Occurrences.IEventOccurrenceUsage elementAsEventOccurrenceUsage)
                {
                    EventOccurrenceUsageTextualNotationBuilder.BuildMessageEvent(elementAsEventOccurrenceUsage, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PayloadParameterMember
        /// <para>PayloadParameterMember:ParameterMembership=ownedRelatedElement+=PayloadParameter</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPayloadParameterMember(SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage elementAsReferenceUsage)
                {
                    ReferenceUsageTextualNotationBuilder.BuildPayloadParameter(elementAsReferenceUsage, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ArgumentMember
        /// <para>ArgumentMember:ParameterMembership=ownedMemberParameter=Argument</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildArgumentMember(SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

            if (poco.ownedMemberParameter != null)
            {
                FeatureTextualNotationBuilder.BuildArgument(poco.ownedMemberParameter, cursorCache, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ArgumentExpressionMember
        /// <para>ArgumentExpressionMember:ParameterMembership=ownedRelatedElement+=ArgumentExpression</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildArgumentExpressionMember(SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeature elementAsFeature)
                {
                    FeatureTextualNotationBuilder.BuildArgumentExpression(elementAsFeature, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NodeParameterMember
        /// <para>NodeParameterMember:ParameterMembership=ownedRelatedElement+=NodeParameter</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNodeParameterMember(SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage elementAsReferenceUsage)
                {
                    ReferenceUsageTextualNotationBuilder.BuildNodeParameter(elementAsReferenceUsage, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EmptyParameterMember
        /// <para>EmptyParameterMember:ParameterMembership=ownedRelatedElement+=EmptyUsage</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildEmptyParameterMember(SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage elementAsReferenceUsage)
                {
                    ReferenceUsageTextualNotationBuilder.BuildEmptyUsage(elementAsReferenceUsage, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule AssignmentTargetMember
        /// <para>AssignmentTargetMember:ParameterMembership=ownedRelatedElement+=AssignmentTargetParameter</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildAssignmentTargetMember(SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage elementAsReferenceUsage)
                {
                    ReferenceUsageTextualNotationBuilder.BuildAssignmentTargetParameter(elementAsReferenceUsage, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ExpressionParameterMember
        /// <para>ExpressionParameterMember:ParameterMembership=ownedRelatedElement+=OwnedExpression</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildExpressionParameterMember(SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Kernel.Functions.IExpression elementAsExpression)
                {
                    ExpressionTextualNotationBuilder.BuildOwnedExpression(elementAsExpression, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionBodyParameterMember
        /// <para>ActionBodyParameterMember:ParameterMembership=ownedRelatedElement+=ActionBodyParameter</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionBodyParameterMember(SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.Actions.IActionUsage elementAsActionUsage)
                {
                    ActionUsageTextualNotationBuilder.BuildActionBodyParameter(elementAsActionUsage, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule IfNodeParameterMember
        /// <para>IfNodeParameterMember:ParameterMembership=ownedRelatedElement+=IfNode</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildIfNodeParameterMember(SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.Actions.IIfActionUsage elementAsIfActionUsage)
                {
                    IfActionUsageTextualNotationBuilder.BuildIfNode(elementAsIfActionUsage, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataArgumentMember
        /// <para>MetadataArgumentMember:ParameterMembership=ownedRelatedElement+=MetadataArgument</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataArgumentMember(SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeature elementAsFeature)
                {
                    FeatureTextualNotationBuilder.BuildMetadataArgument(elementAsFeature, cursorCache, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeReferenceMember
        /// <para>TypeReferenceMember:ParameterMembership=ownedMemberFeature=TypeReference</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypeReferenceMember(SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

            if (poco.ownedMemberFeature != null)
            {
                FeatureTextualNotationBuilder.BuildTypeReference(poco.ownedMemberFeature, cursorCache, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PrimaryArgumentMember
        /// <para>PrimaryArgumentMember:ParameterMembership=ownedMemberParameter=PrimaryArgument</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPrimaryArgumentMember(SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

            if (poco.ownedMemberParameter != null)
            {
                FeatureTextualNotationBuilder.BuildPrimaryArgument(poco.ownedMemberParameter, cursorCache, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NonFeatureChainPrimaryArgumentMember
        /// <para>NonFeatureChainPrimaryArgumentMember:ParameterMembership=ownedMemberParameter=PrimaryArgument</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNonFeatureChainPrimaryArgumentMember(SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

            if (poco.ownedMemberParameter != null)
            {
                FeatureTextualNotationBuilder.BuildPrimaryArgument(poco.ownedMemberParameter, cursorCache, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BodyArgumentMember
        /// <para>BodyArgumentMember:ParameterMembership=ownedMemberParameter=BodyArgument</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBodyArgumentMember(SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

            if (poco.ownedMemberParameter != null)
            {
                FeatureTextualNotationBuilder.BuildBodyArgument(poco.ownedMemberParameter, cursorCache, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FunctionReferenceArgumentMember
        /// <para>FunctionReferenceArgumentMember:ParameterMembership=ownedMemberParameter=FunctionReferenceArgument</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFunctionReferenceArgumentMember(SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

            if (poco.ownedMemberParameter != null)
            {
                FeatureTextualNotationBuilder.BuildFunctionReferenceArgument(poco.ownedMemberParameter, cursorCache, stringBuilder);
            }

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
