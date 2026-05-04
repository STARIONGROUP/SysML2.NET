// -------------------------------------------------------------------------------------------------
// <copyright file="UsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="UsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> element
    /// </summary>
    public static partial class UsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule UsageElement
        /// <para>UsageElement:Usage=NonOccurrenceUsageElement|OccurrenceUsageElement</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage pocoUsageNonOccurrenceUsageElement when pocoUsageNonOccurrenceUsageElement.IsEnd:
                    BuildNonOccurrenceUsageElement(pocoUsageNonOccurrenceUsageElement, writerContext, stringBuilder);
                    break;
                default:
                    BuildOccurrenceUsageElement(poco, writerContext, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule RefPrefix
        /// <para>RefPrefix:Usage=(direction=FeatureDirection)?(isDerived?='derived')?(isAbstract?='abstract'|isVariation?='variation')?(isConstant?='constant')?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRefPrefix(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {

            if (poco.Direction.HasValue)
            {
                stringBuilder.Append(poco.Direction.ToString().ToLower());
                stringBuilder.Append(' ');
                stringBuilder.Append(' ');
            }


            if (poco.IsDerived)
            {
                stringBuilder.Append(" derived ");
                stringBuilder.Append(' ');
            }

            if (poco.IsAbstract)
            {
                stringBuilder.Append(" abstract ");
            }
            else if (poco.IsVariation)
            {
                stringBuilder.Append(" variation ");
            }


            if (poco.IsConstant)
            {
                stringBuilder.Append(" constant ");
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BasicUsagePrefix
        /// <para>BasicUsagePrefix:Usage=RefPrefix(isReference?='ref')?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBasicUsagePrefix(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            BuildRefPrefix(poco, writerContext, stringBuilder);

            if (poco.isReference)
            {
                stringBuilder.Append(" ref ");
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EndUsagePrefix
        /// <para>EndUsagePrefix:Usage=isEnd?='end'(ownedRelationship+=OwnedCrossFeatureMember)?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildEndUsagePrefix(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            if (poco.IsEnd)
            {
                stringBuilder.Append(" end ");
            }

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership elementAsOwningMembership)
                    {
                        OwningMembershipTextualNotationBuilder.BuildOwnedCrossFeatureMember(elementAsOwningMembership, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();

                    }
                }
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule UsageExtensionKeyword
        /// <para>UsageExtensionKeyword:Usage=ownedRelationship+=PrefixMetadataMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUsageExtensionKeyword(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership elementAsOwningMembership)
                {
                    OwningMembershipTextualNotationBuilder.BuildPrefixMetadataMember(elementAsOwningMembership, writerContext, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule UnextendedUsagePrefix
        /// <para>UnextendedUsagePrefix:Usage=EndUsagePrefix|BasicUsagePrefix</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUnextendedUsagePrefix(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage pocoUsageBasicUsagePrefix when pocoUsageBasicUsagePrefix.IsDerived:
                    BuildBasicUsagePrefix(pocoUsageBasicUsagePrefix, writerContext, stringBuilder);
                    break;
                default:
                    BuildEndUsagePrefix(poco, writerContext, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule UsagePrefix
        /// <para>UsagePrefix:Usage=UnextendedUsagePrefixUsageExtensionKeyword*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUsagePrefix(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            BuildUnextendedUsagePrefix(poco, writerContext, stringBuilder);
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership owningMembershipGuard && owningMembershipGuard.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Systems.Metadata.IMetadataUsage>().Any())
            {
                BuildUsageExtensionKeyword(poco, writerContext, stringBuilder);
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule UsageDeclaration
        /// <para>UsageDeclaration:Usage=IdentificationFeatureSpecializationPart?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUsageDeclaration(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            ElementTextualNotationBuilder.BuildIdentification(poco, writerContext, stringBuilder);

            if (poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || poco.IsOrdered)
            {
                FeatureTextualNotationBuilder.BuildFeatureSpecializationPart(poco, writerContext, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule UsageCompletion
        /// <para>UsageCompletion:Usage=ValuePart?UsageBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUsageCompletion(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {

            if (poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.Direction.HasValue || poco.IsDerived || poco.IsAbstract || poco.IsConstant || poco.IsOrdered || poco.IsEnd || poco.importedMembership.Count != 0 || poco.IsComposite || poco.IsPortion || poco.IsVariable || poco.IsSufficient || poco.unioningType.Count != 0 || poco.intersectingType.Count != 0 || poco.differencingType.Count != 0 || poco.featuringType.Count != 0 || poco.ownedTypeFeaturing.Count != 0)
            {
                FeatureTextualNotationBuilder.BuildValuePart(poco, writerContext, stringBuilder);
            }
            BuildUsageBody(poco, writerContext, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule UsageBody
        /// <para>UsageBody:Usage=DefinitionBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUsageBody(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            TypeTextualNotationBuilder.BuildDefinitionBody(poco, writerContext, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NonOccurrenceUsageElement
        /// <para>NonOccurrenceUsageElement:Usage=DefaultReferenceUsage|ReferenceUsage|AttributeUsage|EnumerationUsage|BindingConnectorAsUsage|SuccessionAsUsage|ExtendedUsage</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNonOccurrenceUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Systems.Connections.IBindingConnectorAsUsage pocoBindingConnectorAsUsage:
                    BindingConnectorAsUsageTextualNotationBuilder.BuildBindingConnectorAsUsage(pocoBindingConnectorAsUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Connections.ISuccessionAsUsage pocoSuccessionAsUsage:
                    SuccessionAsUsageTextualNotationBuilder.BuildSuccessionAsUsage(pocoSuccessionAsUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Enumerations.IEnumerationUsage pocoEnumerationUsage:
                    EnumerationUsageTextualNotationBuilder.BuildEnumerationUsage(pocoEnumerationUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage pocoReferenceUsageReferenceUsage when pocoReferenceUsageReferenceUsage.IsEnd:
                    ReferenceUsageTextualNotationBuilder.BuildReferenceUsage(pocoReferenceUsageReferenceUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage pocoReferenceUsage:
                    ReferenceUsageTextualNotationBuilder.BuildDefaultReferenceUsage(pocoReferenceUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Attributes.IAttributeUsage pocoAttributeUsage:
                    AttributeUsageTextualNotationBuilder.BuildAttributeUsage(pocoAttributeUsage, writerContext, stringBuilder);
                    break;
                default:
                    BuildExtendedUsage(poco, writerContext, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OccurrenceUsageElement
        /// <para>OccurrenceUsageElement:Usage=StructureUsageElement|BehaviorUsageElement</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOccurrenceUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage pocoUsageStructureUsageElement when pocoUsageStructureUsageElement.IsValidForStructureUsageElement():
                    BuildStructureUsageElement(pocoUsageStructureUsageElement, writerContext, stringBuilder);
                    break;
                default:
                    BuildBehaviorUsageElement(poco, writerContext, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule StructureUsageElement
        /// <para>StructureUsageElement:Usage=OccurrenceUsage|IndividualUsage|PortionUsage|EventOccurrenceUsage|ItemUsage|PartUsage|ViewUsage|RenderingUsage|PortUsage|ConnectionUsage|InterfaceUsage|AllocationUsage|Message|FlowUsage|SuccessionFlowUsage</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildStructureUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Systems.Flows.ISuccessionFlowUsage pocoSuccessionFlowUsage:
                    SuccessionFlowUsageTextualNotationBuilder.BuildSuccessionFlowUsage(pocoSuccessionFlowUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage pocoInterfaceUsage:
                    InterfaceUsageTextualNotationBuilder.BuildInterfaceUsage(pocoInterfaceUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Allocations.IAllocationUsage pocoAllocationUsage:
                    AllocationUsageTextualNotationBuilder.BuildAllocationUsage(pocoAllocationUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Flows.IFlowUsage pocoFlowUsageMessage when pocoFlowUsageMessage.IsValidForMessage():
                    FlowUsageTextualNotationBuilder.BuildMessage(pocoFlowUsageMessage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Flows.IFlowUsage pocoFlowUsage:
                    FlowUsageTextualNotationBuilder.BuildFlowUsage(pocoFlowUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Connections.IConnectionUsage pocoConnectionUsage:
                    ConnectionUsageTextualNotationBuilder.BuildConnectionUsage(pocoConnectionUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.IViewUsage pocoViewUsage:
                    ViewUsageTextualNotationBuilder.BuildViewUsage(pocoViewUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.IRenderingUsage pocoRenderingUsage:
                    RenderingUsageTextualNotationBuilder.BuildRenderingUsage(pocoRenderingUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Parts.IPartUsage pocoPartUsage:
                    PartUsageTextualNotationBuilder.BuildPartUsage(pocoPartUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Occurrences.IEventOccurrenceUsage pocoEventOccurrenceUsage:
                    EventOccurrenceUsageTextualNotationBuilder.BuildEventOccurrenceUsage(pocoEventOccurrenceUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Items.IItemUsage pocoItemUsage:
                    ItemUsageTextualNotationBuilder.BuildItemUsage(pocoItemUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Ports.IPortUsage pocoPortUsage:
                    PortUsageTextualNotationBuilder.BuildPortUsage(pocoPortUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage pocoOccurrenceUsageOccurrenceUsage when pocoOccurrenceUsageOccurrenceUsage.IsValidForOccurrenceUsage():
                    OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsage(pocoOccurrenceUsageOccurrenceUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage pocoOccurrenceUsageIndividualUsage when pocoOccurrenceUsageIndividualUsage.IsValidForIndividualUsage():
                    OccurrenceUsageTextualNotationBuilder.BuildIndividualUsage(pocoOccurrenceUsageIndividualUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage pocoOccurrenceUsage:
                    OccurrenceUsageTextualNotationBuilder.BuildPortionUsage(pocoOccurrenceUsage, writerContext, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BehaviorUsageElement
        /// <para>BehaviorUsageElement:Usage=ActionUsage|CalculationUsage|StateUsage|ConstraintUsage|RequirementUsage|ConcernUsage|CaseUsage|AnalysisCaseUsage|VerificationCaseUsage|UseCaseUsage|ViewpointUsage|PerformActionUsage|ExhibitStateUsage|IncludeUseCaseUsage|AssertConstraintUsage|SatisfyRequirementUsage</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBehaviorUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Systems.UseCases.IIncludeUseCaseUsage pocoIncludeUseCaseUsage:
                    IncludeUseCaseUsageTextualNotationBuilder.BuildIncludeUseCaseUsage(pocoIncludeUseCaseUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.ISatisfyRequirementUsage pocoSatisfyRequirementUsage:
                    SatisfyRequirementUsageTextualNotationBuilder.BuildSatisfyRequirementUsage(pocoSatisfyRequirementUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.IConcernUsage pocoConcernUsage:
                    ConcernUsageTextualNotationBuilder.BuildConcernUsage(pocoConcernUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.AnalysisCases.IAnalysisCaseUsage pocoAnalysisCaseUsage:
                    AnalysisCaseUsageTextualNotationBuilder.BuildAnalysisCaseUsage(pocoAnalysisCaseUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.VerificationCases.IVerificationCaseUsage pocoVerificationCaseUsage:
                    VerificationCaseUsageTextualNotationBuilder.BuildVerificationCaseUsage(pocoVerificationCaseUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.UseCases.IUseCaseUsage pocoUseCaseUsage:
                    UseCaseUsageTextualNotationBuilder.BuildUseCaseUsage(pocoUseCaseUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.IViewpointUsage pocoViewpointUsage:
                    ViewpointUsageTextualNotationBuilder.BuildViewpointUsage(pocoViewpointUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.States.IExhibitStateUsage pocoExhibitStateUsage:
                    ExhibitStateUsageTextualNotationBuilder.BuildExhibitStateUsage(pocoExhibitStateUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Constraints.IAssertConstraintUsage pocoAssertConstraintUsage:
                    AssertConstraintUsageTextualNotationBuilder.BuildAssertConstraintUsage(pocoAssertConstraintUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage pocoRequirementUsage:
                    RequirementUsageTextualNotationBuilder.BuildRequirementUsage(pocoRequirementUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Cases.ICaseUsage pocoCaseUsage:
                    CaseUsageTextualNotationBuilder.BuildCaseUsage(pocoCaseUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Calculations.ICalculationUsage pocoCalculationUsage:
                    CalculationUsageTextualNotationBuilder.BuildCalculationUsage(pocoCalculationUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Constraints.IConstraintUsage pocoConstraintUsage:
                    ConstraintUsageTextualNotationBuilder.BuildConstraintUsage(pocoConstraintUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage pocoPerformActionUsage:
                    PerformActionUsageTextualNotationBuilder.BuildPerformActionUsage(pocoPerformActionUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.States.IStateUsage pocoStateUsage:
                    StateUsageTextualNotationBuilder.BuildStateUsage(pocoStateUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.IActionUsage pocoActionUsage:
                    ActionUsageTextualNotationBuilder.BuildActionUsage(pocoActionUsage, writerContext, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule VariantUsageElement
        /// <para>VariantUsageElement:Usage=VariantReference|ReferenceUsage|AttributeUsage|BindingConnectorAsUsage|SuccessionAsUsage|OccurrenceUsage|IndividualUsage|PortionUsage|EventOccurrenceUsage|ItemUsage|PartUsage|ViewUsage|RenderingUsage|PortUsage|ConnectionUsage|InterfaceUsage|AllocationUsage|Message|FlowUsage|SuccessionFlowUsage|BehaviorUsageElement</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildVariantUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Systems.Flows.ISuccessionFlowUsage pocoSuccessionFlowUsage:
                    SuccessionFlowUsageTextualNotationBuilder.BuildSuccessionFlowUsage(pocoSuccessionFlowUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage pocoInterfaceUsage:
                    InterfaceUsageTextualNotationBuilder.BuildInterfaceUsage(pocoInterfaceUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Allocations.IAllocationUsage pocoAllocationUsage:
                    AllocationUsageTextualNotationBuilder.BuildAllocationUsage(pocoAllocationUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Flows.IFlowUsage pocoFlowUsageFlowUsage when pocoFlowUsageFlowUsage.IsValidForFlowUsage():
                    FlowUsageTextualNotationBuilder.BuildFlowUsage(pocoFlowUsageFlowUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Flows.IFlowUsage pocoFlowUsage:
                    FlowUsageTextualNotationBuilder.BuildMessage(pocoFlowUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Connections.IConnectionUsage pocoConnectionUsage:
                    ConnectionUsageTextualNotationBuilder.BuildConnectionUsage(pocoConnectionUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Connections.IBindingConnectorAsUsage pocoBindingConnectorAsUsage:
                    BindingConnectorAsUsageTextualNotationBuilder.BuildBindingConnectorAsUsage(pocoBindingConnectorAsUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Connections.ISuccessionAsUsage pocoSuccessionAsUsage:
                    SuccessionAsUsageTextualNotationBuilder.BuildSuccessionAsUsage(pocoSuccessionAsUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.IViewUsage pocoViewUsage:
                    ViewUsageTextualNotationBuilder.BuildViewUsage(pocoViewUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.IRenderingUsage pocoRenderingUsage:
                    RenderingUsageTextualNotationBuilder.BuildRenderingUsage(pocoRenderingUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Parts.IPartUsage pocoPartUsage:
                    PartUsageTextualNotationBuilder.BuildPartUsage(pocoPartUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Occurrences.IEventOccurrenceUsage pocoEventOccurrenceUsage:
                    EventOccurrenceUsageTextualNotationBuilder.BuildEventOccurrenceUsage(pocoEventOccurrenceUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Items.IItemUsage pocoItemUsage:
                    ItemUsageTextualNotationBuilder.BuildItemUsage(pocoItemUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Ports.IPortUsage pocoPortUsage:
                    PortUsageTextualNotationBuilder.BuildPortUsage(pocoPortUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage pocoReferenceUsageReferenceUsage when pocoReferenceUsageReferenceUsage.IsEnd:
                    ReferenceUsageTextualNotationBuilder.BuildReferenceUsage(pocoReferenceUsageReferenceUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage pocoReferenceUsage:
                    ReferenceUsageTextualNotationBuilder.BuildVariantReference(pocoReferenceUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Attributes.IAttributeUsage pocoAttributeUsage:
                    AttributeUsageTextualNotationBuilder.BuildAttributeUsage(pocoAttributeUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage pocoOccurrenceUsageIndividualUsage when pocoOccurrenceUsageIndividualUsage.IsValidForIndividualUsage():
                    OccurrenceUsageTextualNotationBuilder.BuildIndividualUsage(pocoOccurrenceUsageIndividualUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage pocoOccurrenceUsageOccurrenceUsage when pocoOccurrenceUsageOccurrenceUsage.IsValidForOccurrenceUsage():
                    OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsage(pocoOccurrenceUsageOccurrenceUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage pocoOccurrenceUsage:
                    OccurrenceUsageTextualNotationBuilder.BuildPortionUsage(pocoOccurrenceUsage, writerContext, stringBuilder);
                    break;
                default:
                    BuildBehaviorUsageElement(poco, writerContext, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceNonOccurrenceUsageElement
        /// <para>InterfaceNonOccurrenceUsageElement:Usage=ReferenceUsage|AttributeUsage|EnumerationUsage|BindingConnectorAsUsage|SuccessionAsUsage</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInterfaceNonOccurrenceUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Systems.Connections.IBindingConnectorAsUsage pocoBindingConnectorAsUsage:
                    BindingConnectorAsUsageTextualNotationBuilder.BuildBindingConnectorAsUsage(pocoBindingConnectorAsUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Connections.ISuccessionAsUsage pocoSuccessionAsUsage:
                    SuccessionAsUsageTextualNotationBuilder.BuildSuccessionAsUsage(pocoSuccessionAsUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Enumerations.IEnumerationUsage pocoEnumerationUsage:
                    EnumerationUsageTextualNotationBuilder.BuildEnumerationUsage(pocoEnumerationUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage pocoReferenceUsage:
                    ReferenceUsageTextualNotationBuilder.BuildReferenceUsage(pocoReferenceUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Attributes.IAttributeUsage pocoAttributeUsage:
                    AttributeUsageTextualNotationBuilder.BuildAttributeUsage(pocoAttributeUsage, writerContext, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceOccurrenceUsageElement
        /// <para>InterfaceOccurrenceUsageElement:Usage=DefaultInterfaceEnd|StructureUsageElement|BehaviorUsageElement</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInterfaceOccurrenceUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Systems.Ports.IPortUsage pocoPortUsage:
                    PortUsageTextualNotationBuilder.BuildDefaultInterfaceEnd(pocoPortUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage pocoUsageStructureUsageElement when pocoUsageStructureUsageElement.IsValidForStructureUsageElement():
                    BuildStructureUsageElement(pocoUsageStructureUsageElement, writerContext, stringBuilder);
                    break;
                default:
                    BuildBehaviorUsageElement(poco, writerContext, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionTargetSuccession
        /// <para>ActionTargetSuccession:Usage=(TargetSuccession|GuardedTargetSuccession|DefaultTargetSuccession)UsageBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionTargetSuccession(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Systems.Connections.ISuccessionAsUsage pocoSuccessionAsUsage:
                    SuccessionAsUsageTextualNotationBuilder.BuildTargetSuccession(pocoSuccessionAsUsage, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.States.ITransitionUsage pocoTransitionUsageGuardedTargetSuccession when pocoTransitionUsageGuardedTargetSuccession.IsValidForGuardedTargetSuccession():
                    TransitionUsageTextualNotationBuilder.BuildGuardedTargetSuccession(pocoTransitionUsageGuardedTargetSuccession, writerContext, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.States.ITransitionUsage pocoTransitionUsage:
                    TransitionUsageTextualNotationBuilder.BuildDefaultTargetSuccession(pocoTransitionUsage, writerContext, stringBuilder);
                    break;
            }

            stringBuilder.Append(' ');
            BuildUsageBody(poco, writerContext, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ExtendedUsage
        /// <para>ExtendedUsage:Usage=UnextendedUsagePrefixUsageExtensionKeyword+Usage</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildExtendedUsage(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            BuildUnextendedUsagePrefix(poco, writerContext, stringBuilder);
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership)
            {
                BuildUsageExtensionKeyword(poco, writerContext, stringBuilder);
            }

            BuildUsage(poco, writerContext, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Usage
        /// <para>Usage=UsageDeclarationUsageCompletion</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUsage(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            BuildUsageDeclaration(poco, writerContext, stringBuilder);
            BuildUsageCompletion(poco, writerContext, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
