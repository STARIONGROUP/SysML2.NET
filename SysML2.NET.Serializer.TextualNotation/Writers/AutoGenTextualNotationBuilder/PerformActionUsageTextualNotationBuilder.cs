// -------------------------------------------------------------------------------------------------
// <copyright file="PerformActionUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.TextualNotation.Writers
{
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="PerformActionUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage" /> element
    /// </summary>
    public static partial class PerformActionUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule PerformActionUsageDeclaration
        /// <para>PerformActionUsageDeclaration:PerformActionUsage=(ownedRelationship+=OwnedReferenceSubsettingFeatureSpecializationPart?|'action'UsageDeclaration)ValuePart?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPerformActionUsageDeclaration(SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            if (poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.IReferenceSubsetting>().Any())
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IReferenceSubsetting elementAsReferenceSubsetting)
                    {
                        ReferenceSubsettingTextualNotationBuilder.BuildOwnedReferenceSubsetting(elementAsReferenceSubsetting, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();

                    }
                }

                if ((ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureTyping || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.ISubsetting || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IReferenceSubsetting || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.ICrossSubsetting || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IRedefinition || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership) || poco.IsOrdered)
                {
                    FeatureTextualNotationBuilder.BuildFeatureSpecializationPart(poco, writerContext, stringBuilder);
                }
            }
            else
            {
                stringBuilder.Append("action ");
                UsageTextualNotationBuilder.BuildUsageDeclaration(poco, writerContext, stringBuilder);
            }

            stringBuilder.Append(' ');

            if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue || !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.Direction.HasValue || poco.IsDerived || poco.IsAbstract || poco.IsConstant || poco.IsOrdered || poco.IsEnd || poco.IsComposite || poco.IsPortion || poco.IsVariable || poco.IsSufficient)
            {
                FeatureTextualNotationBuilder.BuildValuePart(poco, writerContext, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule StatePerformActionUsage
        /// <para>StatePerformActionUsage:PerformActionUsage=PerformActionUsageDeclarationActionBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildStatePerformActionUsage(SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            BuildPerformActionUsageDeclaration(poco, writerContext, stringBuilder);
            TypeTextualNotationBuilder.BuildActionBody(poco, writerContext, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TransitionPerformActionUsage
        /// <para>TransitionPerformActionUsage:PerformActionUsage=PerformActionUsageDeclaration('{'ActionBodyItem*'}')?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTransitionPerformActionUsage(SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            BuildPerformActionUsageDeclaration(poco, writerContext, stringBuilder);
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if ((ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IImport || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IMembership || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IVariantMembership || ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IFeatureMembership) || !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.Direction.HasValue || poco.IsDerived || poco.IsAbstract || poco.IsVariation || poco.IsConstant || poco.IsOrdered || poco.IsEnd || poco.isReference || poco.IsIndividual || poco.PortionKind.HasValue || poco.IsComposite || poco.IsPortion || poco.IsVariable || poco.IsSufficient)
            {
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");
                while (ownedRelationshipCursor.Current != null)
                {
                    TypeTextualNotationBuilder.BuildActionBodyItem(poco, writerContext, stringBuilder);
                }

                stringBuilder.AppendLine("}");
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PerformActionUsage
        /// <para>PerformActionUsage=OccurrenceUsagePrefix'perform'PerformActionUsageDeclarationActionBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPerformActionUsage(SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, writerContext, stringBuilder);
            stringBuilder.Append("perform ");
            BuildPerformActionUsageDeclaration(poco, writerContext, stringBuilder);
            TypeTextualNotationBuilder.BuildActionBody(poco, writerContext, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
