// -------------------------------------------------------------------------------------------------
// <copyright file="AcceptActionUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="AcceptActionUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage" /> element
    /// </summary>
    public static partial class AcceptActionUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule AcceptNode
        /// <para>AcceptNode:AcceptActionUsage=OccurrenceUsagePrefixAcceptNodeDeclarationActionBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildAcceptNode(SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, writerContext, stringBuilder);
            BuildAcceptNodeDeclaration(poco, writerContext, stringBuilder);
            TypeTextualNotationBuilder.BuildActionBody(poco, writerContext, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule AcceptNodeDeclaration
        /// <para>AcceptNodeDeclaration:AcceptActionUsage=ActionNodeUsageDeclaration?'accept'AcceptParameterPart</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildAcceptNodeDeclaration(SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureTyping || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.ISubsetting || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IReferenceSubsetting || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.ICrossSubsetting || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IRedefinition || (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership owningMembership0 && owningMembership0.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Kernel.Multiplicities.IMultiplicityRange>().Any())) || poco.IsOrdered)
            {
                ActionUsageTextualNotationBuilder.BuildActionNodeUsageDeclaration(poco, writerContext, stringBuilder);
            }
            stringBuilder.Append("accept ");
            BuildAcceptParameterPart(poco, writerContext, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule AcceptParameterPart
        /// <para>AcceptParameterPart:AcceptActionUsage=ownedRelationship+=PayloadParameterMember('via'ownedRelationship+=NodeParameterMember)?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildAcceptParameterPart(SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                {
                    ParameterMembershipTextualNotationBuilder.BuildPayloadParameterMember(elementAsParameterMembership, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                }
            }

            if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership)
            {
                stringBuilder.Append("via ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                    {
                        ParameterMembershipTextualNotationBuilder.BuildNodeParameterMember(elementAsParameterMembership, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();

                    }
                }
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule StateAcceptActionUsage
        /// <para>StateAcceptActionUsage:AcceptActionUsage=AcceptNodeDeclarationActionBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildStateAcceptActionUsage(SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            BuildAcceptNodeDeclaration(poco, writerContext, stringBuilder);
            TypeTextualNotationBuilder.BuildActionBody(poco, writerContext, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TriggerAction
        /// <para>TriggerAction:AcceptActionUsage=AcceptParameterPart</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildTriggerAction(SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            BuildAcceptParameterPart(poco, writerContext, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TransitionAcceptActionUsage
        /// <para>TransitionAcceptActionUsage:AcceptActionUsage=AcceptNodeDeclaration('{'ActionBodyItem*'}')?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildTransitionAcceptActionUsage(SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            BuildAcceptNodeDeclaration(poco, writerContext, stringBuilder);
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if ((ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IImport || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IMembership || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IVariantMembership || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IFeatureMembership) || !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.Direction.HasValue || poco.IsDerived || poco.IsAbstract || poco.IsVariation || poco.IsConstant || poco.IsOrdered || poco.IsEnd || poco.isReference || poco.IsIndividual || poco.PortionKind.HasValue || poco.IsComposite || poco.IsPortion || poco.IsVariable || poco.IsSufficient)
            {
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");
                stringBuilder.IncreaseIndent();
                while (ownedRelationshipCursor.Current != null)
                {
                    TypeTextualNotationBuilder.BuildActionBodyItem(poco, writerContext, stringBuilder);
                }

                stringBuilder.DecreaseIndent();
                stringBuilder.AppendLine("}");
            }


        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
